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
    public partial class UclUserRights : BaseControl
    {
        #region Declaration
        private UserRights _Right;
        DataTable dtFormNames;
        DataTable dtReportNames;
        #endregion

        #region Constructor
        public UclUserRights()
        {
            try
            {
                InitializeComponent();
                _Right = new UserRights();
                SearchControl = new UclUserRightsSearch();
                dtFormNames = new DataTable();
                dtReportNames = new DataTable();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            mcbForm.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Right.Initialise();
                ClearControls();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            try
            {
                ClearData();
               
                headerLabel1.Text = "USER RIGHTS -> NEW";
                FillFormNameCombo();
                FillAddRightCombo();
                FillEditRightCombo();
                FillDeleteRightCombo();
                FillViewRightCombo();
                FillPrintRightCombo();
                mcbForm.Enabled = true;  
                mcbForm.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();         
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            try
            {
                mcbForm.SelectedID = "";
                mcbAddRight.SelectedID = "";
                mcbEditRight.SelectedID = "";
                mcbDeleteRight.SelectedID = "";
                mcbViewRight.SelectedID = "";
                mcbPrintRight.SelectedID = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "USER RIGHTS -> DELETE";
                ClearData();
                FillFormNameCombo();
                FillAddRightCombo();
                FillEditRightCombo();
                FillDeleteRightCombo();
                FillViewRightCombo();
                FillPrintRightCombo();
                mcbForm.Enabled = true;
                mcbForm.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool ProcessDelete()
        {
            try
            {
                if (_Right.Id != null && _Right.Id != "")
                {
                    if (_Right.CanBeDeleted())
                    {
                        _Right.DeleteDetails();
                        MessageBox.Show(" information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                ClearControls();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
           return true;
        }
        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            headerLabel1.Text = "USER RIGHTS -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;

            try
            {
                System.Text.StringBuilder _errorMessage;

                if (psRadioButtonModule.Checked)
                    _Right.IsFormName = true;
                else
                    _Right.IsFormName = false;

                if (mcbForm.SelectedID != null)
                    _Right.FormName = mcbForm.SelectedID;
                else
                {
                    _Right.FormName = "";
                }

                if (mcbAddRight.SelectedID != null && mcbAddRight.SelectedID != "")
                    _Right.AddLevel = Convert.ToInt16(mcbAddRight.SeletedItem.ItemData[0].ToString().Trim());
                else
                {
                    _Right.AddLevel = -1;
                }

                if (mcbEditRight.SelectedID != null && mcbEditRight.SelectedID != "")
                    _Right.EditLevel = Convert.ToInt16(mcbEditRight.SeletedItem.ItemData[0].ToString().Trim());
                else
                {
                   _Right.EditLevel = -1;
                }

                if (mcbDeleteRight.SelectedID != null && mcbDeleteRight.SelectedID != "")
                    _Right.DeleteLevel = Convert.ToInt16(mcbDeleteRight.SeletedItem.ItemData[0].ToString().Trim());
                else
                {
                    _Right.DeleteLevel = -1;
                }

                if (mcbViewRight.SelectedID != null && mcbViewRight.SelectedID != "")
                    _Right.ViewLevel = Convert.ToInt16(mcbViewRight.SeletedItem.ItemData[0].ToString().Trim());
                else
                {
                   _Right.ViewLevel = -1;

                }
                if (mcbPrintRight.SeletedItem != null && mcbPrintRight.SelectedID != "")
                    _Right.PrintLevel = Convert.ToInt16(mcbPrintRight.SeletedItem.ItemData[0].ToString().Trim());
                else
                {
                   _Right.PrintLevel = -1;
                }

                if (_Right.Id != null && _Right.Id != "")
                    _Mode = OperationMode.Edit;
                else
                    _Mode = OperationMode.Add;
                _Right.Validate();
                if (_Right.IsValid)
                {
                    if (_Mode == OperationMode.Add)
                    {
                        _Right.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Right.CreatedBy = General.CurrentUser.Id;
                        _Right.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Right.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Right.AddDetails(_Right.Id);
                        MessageBox.Show("Rights information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Right.Id;
                    }
                    else
                    {
                        _Right.ModifiedBy = General.CurrentUser.Id;
                        _Right.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Right.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Right.UpdateDetails();
                        MessageBox.Show("Rights information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Right.Id;
                    }
                    ClearControls();
                    _Mode = OperationMode.Add;
                    retValue = true;
                }
                else 
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Right.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Right.Id = ID;
                    _Right.ReadDetailsByID();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return true;
        }
        #endregion IDetail Control

        #region Idetail Members
        public override void ReFillData()
        {

        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
      
        public void ClearControls()
        {
            try
            {
                mcbForm.SelectedID = "";
                mcbAddRight.SelectedID = "";
                mcbDeleteRight.SelectedID = "";
                mcbEditRight.SelectedID = "";
                mcbPrintRight.SelectedID = "";
                mcbViewRight.SelectedID = "";
                _Right.Id = "";            
                mcbForm.Enabled = true;
                mcbForm.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        public void SetFormNameData(DataTable formNamedata)
        {
            try
            {
                dtFormNames = formNamedata;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void SetReportNameData(DataTable reportData)
        {
            try
            {
                dtReportNames = reportData;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region Other Private Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private bool ValidLevel()
        {
            bool retvalue = false;
            try
            {
                UserRights _Right = new UserRights();              
                DataTable table = _Right.GetLevel();

                if (table != null && table.Rows.Count > 0)
                {
                    retvalue = true;
                }
                else
                {
                    retvalue = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retvalue;


        }

        public void FillFormNameCombo()
        {
            try
            {
                mcbForm.SelectedID = null;
                mcbForm.SourceDataString = new string[2] { "ID", "ItemName" };
                mcbForm.ColumnWidth = new string[2] { "0", "450" };
                mcbForm.ValueColumnNo = 0;
                mcbForm.FillData(dtFormNames);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void FillReportNameCombo()
        {
            try
            {
                mcbForm.SelectedID = null;
                mcbForm.SourceDataString = new string[2] { "ID", "ItemName" };
                mcbForm.ColumnWidth = new string[2] { "0", "450" };
                mcbForm.ValueColumnNo = 0;
                mcbForm.FillData(dtReportNames);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillAddRightCombo()
        {
            try
            {
                mcbAddRight.SelectedID = null;
                mcbAddRight.SourceDataString = new string[2] { "ID", "Type" };
                mcbAddRight.ColumnWidth = new string[2] { "0", "200" };
                mcbAddRight.ValueColumnNo = 0;
                UserRights _Right = new UserRights();
                DataTable dt = _Right.GetComboLevelData();
                mcbAddRight.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillEditRightCombo()
        {
            try
            {
                mcbEditRight.SelectedID = null;
                mcbEditRight.SourceDataString = new string[2] { "ID", "Type" };
                mcbEditRight.ColumnWidth = new string[2] { "0", "200" };
                mcbEditRight.ValueColumnNo = 0;
                UserRights _Right = new UserRights();
                DataTable dt = _Right.GetComboLevelData();
                mcbEditRight.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillDeleteRightCombo()
        {
            try
            {
                mcbDeleteRight.SelectedID = null;
                mcbDeleteRight.SourceDataString = new string[2] { "ID", "Type" };
                mcbDeleteRight.ColumnWidth = new string[2] { "0", "200" };
                mcbDeleteRight.ValueColumnNo = 0;
                UserRights _Right = new UserRights();
                DataTable dt = _Right.GetComboLevelData();
                mcbDeleteRight.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillPrintRightCombo()
        {
            try
            {
                mcbPrintRight.SelectedID = null;
                mcbPrintRight.SourceDataString = new string[2] { "ID", "Type" };
                mcbPrintRight.ColumnWidth = new string[2] { "0", "200" };
                mcbPrintRight.ValueColumnNo = 0;
                UserRights _Right = new UserRights();
                DataTable dt = _Right.GetComboLevelData();
                mcbPrintRight.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillViewRightCombo()
        {
            try
            {
                mcbViewRight.SelectedID = null;
                mcbViewRight.SourceDataString = new string[2] { "ID", "Type" };
                mcbViewRight.ColumnWidth = new string[2] { "0", "200" };
                mcbViewRight.ValueColumnNo = 0;
                UserRights _Right = new UserRights();
                DataTable dt = _Right.GetComboLevelData();
                mcbViewRight.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ProcessFormSelection()
        {
            try
            {
                if (!string.IsNullOrEmpty(mcbForm.SelectedID))
                {
                    _Right.FormName = mcbForm.SelectedID;
                    if (_Right.ReadDetailsByID())
                    {                       
                        mcbAddRight.SelectedID = _Right.AddLevel.ToString();
                        mcbEditRight.SelectedID = _Right.EditLevel.ToString();
                        mcbDeleteRight.SelectedID = _Right.DeleteLevel.ToString();
                        mcbViewRight.SelectedID = _Right.ViewLevel.ToString();
                        mcbPrintRight.SelectedID = _Right.PrintLevel.ToString();

                    }
                    mcbForm.Enabled = false;
                    mcbAddRight.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region Events        

        private void mcbForm_SeletectIndexChanged(object sender, EventArgs e)
        {
            ProcessFormSelection();
        }

        private void mcbAddRight_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbEditRight.Focus();
        }

        private void mcbEditRight_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbDeleteRight.Focus();
        }

        private void mcbDeleteRight_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbViewRight.Focus();
        }

        private void mcbViewRight_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbPrintRight.Focus();
        }
        #endregion Events
     

        private void psRadioButtonReports_CheckedChanged(object sender, EventArgs e)
        {
            RadiobuttonsCheckedChanged();
        }

        private void psRadioButtonModule_CheckedChanged(object sender, EventArgs e)
        {
            RadiobuttonsCheckedChanged();
        }

        private void RadiobuttonsCheckedChanged()
        {
            ClearControls();
            if (psRadioButtonReports.Checked)
            {
                FillReportNameCombo();
                ShowReportControls();
            }
            else
            {
                FillFormNameCombo();
                ShowFormControls();
            }
        }

        private void ShowFormControls()
        {
            mPlblForm.Visible = true;
            mPlblAddRight.Visible = true;
            mPlblEditRight.Visible = true;
            mPlblDeleteRight.Visible = true;
            mPlblViewRight.Visible = true;
            mPlblPrintRight.Visible = true;

            mPlblForm.Text = "Form";
            mPlblViewRight.Location = new Point(39, 176);
            mPlblPrintRight.Location = new Point(39, 208);

            mcbForm.Visible = true;
            mcbAddRight.Visible = true;
            mcbEditRight.Visible = true;
            mcbDeleteRight.Visible = true;
            mcbViewRight.Visible = true;
            mcbPrintRight.Visible = true;

            mcbViewRight.Location = new Point(143, 175);
            mcbPrintRight.Location = new Point(143, 206);
            mcbForm.Focus();
        }

        private void ShowReportControls()
        {
            mPlblForm.Visible = true;
            mPlblAddRight.Visible = false;
            mPlblEditRight.Visible = false;
            mPlblDeleteRight.Visible = false;
            mPlblViewRight.Visible = true;
            mPlblPrintRight.Visible = true;

            mPlblForm.Text = "Report";
            mPlblViewRight.Location = new Point(46, 84);
            mPlblPrintRight.Location = new Point(46, 115);

            mcbForm.Visible = true;
            mcbAddRight.Visible = false;
            mcbEditRight.Visible = false;
            mcbDeleteRight.Visible = false;
            mcbViewRight.Visible = true;
            mcbPrintRight.Visible = true;

            mcbViewRight.Location = new Point(143, 83);
            mcbPrintRight.Location = new Point(143, 113);
            mcbForm.Focus();
        }

        private void UclUserRights_Load(object sender, EventArgs e)
        {
            psRadioButtonModule.Checked = true;
        }

        private void mcbForm_EnterKeyPressed(object sender, EventArgs e)
        {

        }

       

    }
}
