//Description : This class contains all methods required for Salesman master. 
//              This is user control required for Add/Update/Delete Salesman details


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

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSalesman : BaseControl 
    {
        # region Declaration        
        private Salesman _Salesman;      
        # endregion

        # region Constructor
        public UclSalesman()
        {
            InitializeComponent();
            _Salesman = new Salesman();
            SearchControl = new UclSalesmanSearch();
        }
        #endregion

       
        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Salesman.Initialise();
            ClearControls();
            txtName.Focus();
            return true;
        }

        public override bool Exit()
        {
            return base.Exit();
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            panel1.Enabled = true;
            FilltxtName();
            AddToolTip();
            headerLabel1.Text = "SALESMAN -> NEW";
            txtName.Focus();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "SALESMAN -> EDIT";
            FilltxtName();
            txtName.Focus();
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            panel1.Enabled = true;
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "SALESMAN -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;          
            if (_Salesman.Id != null && _Salesman.Id != "")
            {
                retValue = _Salesman.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Salesman.DeleteDetails();
                    MessageBox.Show("Salesman information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "SALESMAN -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Salesman.Name = txtName.Text;
            if (_Mode == OperationMode.Edit)
                _Salesman.IFEdit = "Y";
            _Salesman.Validate();
            if (_Salesman.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Salesman.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Salesman.CreatedBy = General.CurrentUser.Id;
                    _Salesman.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Salesman.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Salesman.AddDetails();
                    MessageBox.Show("Salesman information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Salesman.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Salesman.ModifiedBy = General.CurrentUser.Id;
                    _Salesman.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Salesman.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Salesman.UpdateDetails();
                    MessageBox.Show("Salesman information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Salesman.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Salesmans
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Salesman.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _Salesman.Id = ID;
                _Salesman.ReadDetailsByID();
                txtName.Text = _Salesman.Name;
                txtName.Focus();
            }

            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "SalesmanID", "SalesmanName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Salesman _txt = new Salesman();
            DataTable dtable = _txt.GetOverviewData();
            txtName.FillData(dtable);
        }

        # endregion Idetail Control


        #region IDetail Members
        public override void ReFillData()
        {
            FilltxtName();
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
        # endregion IDetail Members


        #region other Private Methods


        public override bool IsDetailChanged()
        {
            bool retValue = false;
            if (_Salesman.Name != null && _Salesman.Name != txtName.Text)
                retValue = true;
            return retValue;
        }
        private void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            txtName.Focus();
        }
        #endregion


        # region Events

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (txtName.SelectedID != null && txtName.SelectedID != "")
                FillSearchData(txtName.SelectedID,"");
        }

        #endregion


        #region tooltip
        private void AddToolTip()
        {
            ttSalesman.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion     
       
    }
}
