using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSettingsEmail : BaseControl
    {
        # region Declaration
        DataTable _SourceData;
        Settings _Settings;
        bool IfGetOverViewData;
        #endregion Declaration

        #region Constructor
        public UclSettingsEmail()
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
            txtEmailID.Focus();
        }
        public override bool ClearData()
        {
            ClearControls();
            return true;
        }

        public void ClearControls()
        {
            txtEmailID.Text = "";
            txtEmailPassword.Text = "";
            FillEmailType();
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "Email SETTINGS";
            IfGetOverViewData = GetOverviewData();           
            return retValue;
        }

        private void FillEmailType()
        {
            cbEmailType.Items.Clear();
            cbEmailType.Items.Add("Gmail");
            cbEmailType.Items.Add("Yahoo");
            cbEmailType.Items.Add("Rediffmail");
            cbEmailType.Items.Add("Hotmail");
            cbEmailType.Text = "Gmail";
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

                if (txtEmailID.Text != null)
                    _Settings.MsetEmailID = txtEmailID.Text.ToString();
                if (txtEmailPassword.Text != null)
                    _Settings.MsetEmailPassword = txtEmailPassword.Text.ToString();
                if (cbEmailType.Text != null)
                    _Settings.MsetEmailType = cbEmailType.Text.ToString();                              

                LockTable.LockTableForSettings();
                retValue = _Settings.UpdateDetailsEmail();
                LockTable.UnLockTables();

                
                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    General.CurrentSetting.FillSettings();
                    ClearData();
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
        public override void ReFillData()
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
            retValue = _Settings.GetOverviewDataEmail();
            if (retValue)
            {
                if (_Settings.MsetEmailID != null)
                    txtEmailID.Text = _Settings.MsetEmailID.ToString();

                if (_Settings.MsetEmailPassword != null)
                    txtEmailPassword.Text = _Settings.MsetEmailPassword.ToString();

                if (_Settings.MsetEmailType != null)
                    cbEmailType.Text = _Settings.MsetEmailType.ToString();
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
    }
}
