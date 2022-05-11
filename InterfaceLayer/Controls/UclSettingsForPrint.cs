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
    public partial class UclSettingsForPrint : BaseControl
    {
        # region Declaration
        DataTable _SourceData;
        Settings _Settings;
        bool IfGetOverViewData;
        #endregion Declaration

        #region Constructor
        public UclSettingsForPrint()
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
            cbBillPrintOnPrintedPaper.Focus();
        }
        public override bool ClearData()
        {
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "PRINT SETTINGS";
            IfGetOverViewData = GetOverviewData();
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            tsBtnExit.Visible = true;
            FillPrinterType();
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
            headerLabel1.Text = "SETTINGS PRINT -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;          

            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
            {
                if (rbtnNone.Checked == true)
                    _Settings.MsetSortOrder = FixAccounts.SortByNone;
                else if (rbtnShelf.Checked == true)
                    _Settings.MsetSortOrder = FixAccounts.SortByShelf;

                if (cbBillPrintOnPrintedPaper.Checked)
                    _Settings.MsetPrintSaleBill  = "Y";
                else
                    _Settings.MsetPrintSaleBill = "N";
                if (cbCRDBPrintOnPrintedPaper.Checked)
                    _Settings.MsetPrintCRDBNote = "Y";
                else
                    _Settings.MsetPrintCRDBNote = "N";
                if (cbCashBankPrintOnPrintedPaper.Checked)
                    _Settings.MsetPrintCashBankVoucher = "Y";
                else
                    _Settings.MsetPrintCashBankVoucher = "N";
                if (cbPOPrintOnPrintedPaper.Checked)
                    _Settings.MsetPrintPO = "Y";
                else
                    _Settings.MsetPrintPO = "N";

                if (cbFixNumberOfLines.Checked)
                    _Settings.MsetPrintFixNumberOfLines = "Y";
                else
                    _Settings.MsetPrintFixNumberOfLines = "N";

                if (txtNumberofLinesSaleBill.Text == null || txtNumberofLinesSaleBill.Text.ToString() == "")
                    txtNumberofLinesSaleBill.Text = _Settings.MsetNumberOfLinesSaleBill.ToString();

                if (txtNumberofBillsAtaTime.Text == null || txtNumberofBillsAtaTime.Text == string.Empty)
                    txtNumberofBillsAtaTime.Text = _Settings.MsetNumberOfBillsAtaTime.ToString();

                _Settings.MsetNumberOfLinesSaleBill = Convert.ToInt32(txtNumberofLinesSaleBill.Text.ToString());
                _Settings.MsetNumberOfBillsAtaTime = Convert.ToInt32(txtNumberofBillsAtaTime.Text.ToString());

                _Settings.MsetPrinterType = cbPrinterType.SelectedIndex;

                LockTable.LockTableForSettings();
                _Settings.UpdateDetailsPrint();
               

                LockTable.UnLockTables();
                retValue = true;
                if (retValue == true)
                {
                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    General.CurrentSetting.FillSettings();
                }
                else
                {
                    MessageBox.Show("Unable to save Information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                retValue = true;
            }

            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {

        }
        private void FillPrinterType()
        {
            cbPrinterType.Items.Clear();
            cbPrinterType.Items.Add(FixAccounts.PrinterTypeDotMatrix);
            cbPrinterType.Items.Add(FixAccounts.PrinterTypeLaserJet);
            cbPrinterType.Text = FixAccounts.PrinterTypeDotMatrix;
            cbPrinterType.SelectedIndex = cbPrinterType.Items.IndexOf(FixAccounts.PrinterTypeDotMatrix);
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
            retValue = _Settings.GetOverviewDataPrint();
            if (retValue)
            {
                if (_Settings.MsetSortOrder == FixAccounts.SortByNone)
                    rbtnNone.Checked = true;
                else if (_Settings.MsetSortOrder == FixAccounts.SortByShelf)
                    rbtnShelf.Checked = true;
                if (_Settings.MsetPrintSaleBill  == "Y")
                    cbBillPrintOnPrintedPaper.Checked  = true;
                else
                   cbBillPrintOnPrintedPaper.Checked = false;

                if (_Settings.MsetPrintCRDBNote  == "Y")
                    cbCRDBPrintOnPrintedPaper.Checked  = true;
                else
                    cbCRDBPrintOnPrintedPaper.Checked  = false;

                if (_Settings.MsetPrintCashBankVoucher == "Y")
                    cbCashBankPrintOnPrintedPaper.Checked  = true;
                else
                    cbCashBankPrintOnPrintedPaper.Checked = false;

                if (_Settings.MsetPrintPO == "Y")
                    cbPOPrintOnPrintedPaper.Checked  = true;
                else
                    cbPOPrintOnPrintedPaper.Checked = false;

                if (_Settings.MsetPrintFixNumberOfLines == "Y")
                    cbFixNumberOfLines.Checked = true;
                else
                    cbFixNumberOfLines.Checked = false;

                txtNumberofLinesSaleBill.Text = _Settings.MsetNumberOfLinesSaleBill.ToString();
                txtNumberofBillsAtaTime.Text = _Settings.MsetNumberOfBillsAtaTime.ToString();

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




        public object Printfont { get; set; }


    }
}
