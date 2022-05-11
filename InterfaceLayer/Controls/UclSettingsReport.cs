using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using EcoMart.InterfaceLayer.CommonControls;
using EcoMart.Reporting.Controls;


namespace EcoMart.InterfaceLayer
{
       [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSettingsReport : BaseControl
    {

        # region Declaration
        DataTable _SourceData;
        Settings _Settings;
        bool IfGetOverViewData;
        #endregion Declaration
        #region Constructor
        public UclSettingsReport()
        {
            try
            {
                InitializeComponent();
                _SourceData = new DataTable();
                _Settings = new Settings();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            cbSaleDailySaleDoNotShowTotal.Focus();
        }
        public override bool ClearData()
        {
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "SETTINGS REPORT";
            IfGetOverViewData = GetOverviewData();
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            tsBtnExit.Visible = true;
            FillFontName();
            FillFontSize();
            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            return retValue;
        }

        public override bool Delete()
        {
            return true;
        }

        public override bool ProcessDelete()
        {
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            headerLabel1.Text = "SETTINGS REPRORT -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;

            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
            {
                if (cbSaleDailySaleDoNotShowTotal.Checked)
                    _Settings.MsetReportSaleDailySaleDoNotShowTotal= "Y";
                else
                    _Settings.MsetReportSaleDailySaleDoNotShowTotal = "N";

                if (cbSaleDailyProductsDoNotShowTotal.Checked)
                    _Settings.MsetReportSaleDailyProductsDoNotShowTotal = "Y";
                else
                    _Settings.MsetReportSaleDailyProductsDoNotShowTotal = "N";

                _Settings.MsetPrintFontName = cbFontName.Text.ToString();
                _Settings.MsetPrintFontSize = cbFontSize.Text.ToString();

                if (IfGetOverViewData)
                    retValue = _Settings.UpdateDetailsReport();
                 Printfont = new Font(_Settings.MsetPrintFontName, Convert.ToInt32(_Settings.MsetPrintFontSize));


                MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                General.CurrentSetting.FillSettings();

                retValue = true;
            }

            return retValue;
        }


        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }

        private void FillFontName()
        {

            cbFontName.Items.Clear();
            cbFontName.Items.Add("Arial");
            cbFontName.Items.Add("Verdana");
            cbFontName.Items.Add("Cambria");
            cbFontName.Items.Add("Kalinga");
            cbFontName.Items.Add("Segoe UI Light");
            cbFontName.Text = _Settings.MsetPrintFontName;
            cbFontName.SelectedIndex = cbFontName.Items.IndexOf(_Settings.MsetPrintFontName);
        }
        private void FillFontSize()
        {

            cbFontSize.Items.Clear();
            cbFontSize.Items.Add("7");
            cbFontSize.Items.Add("8");
            cbFontSize.Items.Add("9");
            cbFontSize.Items.Add("10");
            cbFontSize.Items.Add("11");
            cbFontSize.Text = _Settings.MsetPrintFontSize;
            cbFontSize.SelectedIndex = cbFontSize.Items.IndexOf(_Settings.MsetPrintFontSize);
        }
        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {

        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        public bool GetOverviewData()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            retValue = _Settings.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
            if (retValue)
            {
                if (_Settings.MsetReportSaleDailySaleDoNotShowTotal == "Y")
                    cbSaleDailySaleDoNotShowTotal.Checked = true;
                else
                    cbSaleDailySaleDoNotShowTotal.Checked = false;

                if (_Settings.MsetReportSaleDailyProductsDoNotShowTotal == "Y")
                    cbSaleDailyProductsDoNotShowTotal.Checked = true;
                else
                    cbSaleDailyProductsDoNotShowTotal.Checked = false;

                cbFontName.Text = _Settings.MsetPrintFontName;
                cbFontName.SelectedIndex = cbFontName.Items.IndexOf(cbFontName.Text.ToString());
                cbFontSize.Text = _Settings.MsetPrintFontSize;
                cbFontSize.SelectedIndex = cbFontSize.Items.IndexOf(cbFontSize.Text.ToString());
            }
            return retValue;
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }



        #endregion IDetail Members

        #region Events

        private void cbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Font myfont =   new  System.Drawing.Font(cbFontName.Text, Convert.ToInt32)cbFontSize.Text);
            string fname = cbFontName.Text.ToString();
            int fsize = 7;
            if (cbFontSize.Text.ToString() != string.Empty)
                fsize = Convert.ToInt32(cbFontSize.Text.ToString());

            txtFont.Font = new Font(fname, fsize);


        }

        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fname = cbFontName.Text.ToString();
            int fsize = 7;
            if (cbFontSize.Text.ToString() != string.Empty)
                fsize = Convert.ToInt32(cbFontSize.Text.ToString());

            txtFont.Font = new Font(fname, fsize);

        }
        #endregion Events

        public System.Drawing.Font Printfont { get; set; }
    }
}
