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

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPartyCompany : BaseControl 
    {
        # region declaration
        private DataTable _BindingSource;
        private PartyCompany _PartyCompany;
        private string _IfReverse = "N";
        private int norn = 0;
        private int nory = 0;    
        #endregion

        # region Constructor
        public UclPartyCompany()
        {
            try
            {
                InitializeComponent();
                _PartyCompany = new PartyCompany();
                SearchControl = new UclPartyCompanySearch();
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
            mcbParty.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _PartyCompany.Initialise();
                ClearControls();
                mcbParty.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Add()
        {
            bool retValue = false;
            try
            {
                retValue = base.Edit();
                ClearData();
                btnAdd.Visible = true;
                btnViewAll.Visible = false;
                headerLabel1.Text = "PARTY COMPANY -> NEW";
                AddToolTip();
                FillPartyCombo();
                FillCompanyCombo();
                ConstructCompanyColumns();
                //FillCompanyData();
                dgvCompany.Enabled = false;
                tsBtnTrueFalse();
                mcbParty.Enabled = true;
                //mcbParty.Focus();
                rdbPartyWise.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = false;
            try
            {
                retValue = base.Edit();
                ClearData();
                btnAdd.Visible = true;
                btnViewAll.Visible = false;
                headerLabel1.Text = "PARTY COMPANY -> EDIT";
                AddToolTip();
                FillPartyCombo();
                FillCompanyCombo();
                ConstructCompanyColumns();
                FillCompanyData();
                dgvCompany.Enabled = false;
                mcbParty.Enabled = true;
                mcbParty.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
            bool retValue = false;
            try
            {
                retValue = base.View();
                btnAdd.Visible = false;
                btnViewAll.Visible = true;
                ClearData();
                FillPartyCombo();
                tsBtnTrueFalse();
                headerLabel1.Text = "PARTY COMPANY -> VIEW";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void tsBtnTrueFalse()
        {
            if (_Mode == OperationMode.View)
            {
                tsBtnDelete.Visible = false;
                tsBtnEdit.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSearch.Visible = false;
                tsBtnAdd.Visible = false;
                tsBtnCancel.Visible = false;
                tsBtnFifth.Visible = false;
                tsBtnFirst.Visible = false;
                tsBtnLast.Visible = false;
                tsBtnNext.Visible = false;
                tsBtnPrevious.Visible = false;
            }
            else
            {
                tsBtnSavenPrint.Visible = false;

            }
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                DataGridViewRow dr;
                _PartyCompany.Id = mcbParty.SelectedID;
                retValue = _PartyCompany.DeleteDetails();
                _PartyCompany.CompanyId = dgvCompany.Rows.Count.ToString();
                _PartyCompany.Validate();
                if (_PartyCompany.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _PartyCompany.CreatedBy = General.CurrentUser.Id;
                        _PartyCompany.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _PartyCompany.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        if (IsPartyAlreadyLinked(mcbParty.SelectedID) == false)
                        {
                            for (int index = 0; index < dgvCompany.Rows.Count; index++)
                            {
                                dr = dgvCompany.Rows[index];
                                if (dr.Cells[0].Value.ToString().Trim() != null && dr.Cells[0].Value.ToString().Trim() != "")
                                {
                                    _PartyCompany.Id = mcbParty.SelectedID;
                                    _PartyCompany.CompanyId = dr.Cells[0].Value.ToString();
                                    _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _PartyCompany.AddDetails();
                                }
                            }

                            MessageBox.Show("PartyCompany information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _PartyCompany.Id;
                            mcbCompany.SelectedID = "";
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Party Already Linked.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mcbParty.SelectedID = "";
                        }
                        ClearData();
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _PartyCompany.Id = mcbParty.SelectedID;
                        retValue = _PartyCompany.DeleteDetails();
                        _PartyCompany.ModifiedBy = General.CurrentUser.Id;
                        _PartyCompany.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _PartyCompany.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        for (int index = 0; index < dgvCompany.Rows.Count; index++)
                        {
                            _PartyCompany.CreatedBy = "";
                            _PartyCompany.CreatedDate = "";
                            _PartyCompany.CreatedTime = "";
                            dr = dgvCompany.Rows[index];
                            _PartyCompany.Id = mcbParty.SelectedID;
                            _PartyCompany.CompanyId = dr.Cells[0].Value.ToString();
                            if (dr.Cells[2].Value != null)
                                _PartyCompany.CreatedBy = dr.Cells[2].Value.ToString();
                            if (dr.Cells[3].Value != null)
                                _PartyCompany.CreatedDate = dr.Cells[3].Value.ToString();
                            if (dr.Cells[4].Value != null)
                                _PartyCompany.CreatedTime = dr.Cells[4].Value.ToString();
                            if (_PartyCompany.CreatedBy == "")
                                _PartyCompany.CreatedBy = General.CurrentUser.Id;
                            if (_PartyCompany.CreatedDate == "")
                                _PartyCompany.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            if (_PartyCompany.CreatedTime == "")
                                _PartyCompany.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            retValue = _PartyCompany.AddDetails();
                        }
                        MessageBox.Show("PartyCompany information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _PartyCompany.Id;
                        mcbParty.SelectedID = "";
                        retValue = true;
                        ClearData();
                    }

                    else // Show Validation Messages
                    {
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _PartyCompany.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        mcbParty.Enabled = true;
                    }
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
                mcbParty.SelectedID = ID;
                FillData(ID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        # endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclAccount)
                    FillPartyCombo();
                else if (closedControl is UclCompany)
                    FillCompanyCombo();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    btnAdd.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    mcbCompany.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    mcbParty.Focus();
                    retValue = true;
                }               
              
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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

        #endregion IDetail Members

        #region other private methods
        private bool FillData(string ID)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _PartyCompany.Id = ID;
                    _PartyCompany.ReadDetailsByID();                   
                    _PartyCompany.Name = _PartyCompany.PartyName;
                    AddToolTip();
                    FillCompanyData();
                    dgvCompany.Enabled = true;
                    mcbParty.Enabled = false;
                    mcbCompany.Focus();
                }                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbParty.SelectedID = null;
                mcbParty.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbParty.ColumnWidth = new string[4] { "0", "10", "200", "200" };
                mcbParty.DisplayColumnNo = 2;
                mcbParty.ValueColumnNo = 0;                
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewData(FixAccounts.AccCodeForCreditor);
                mcbParty.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillCompanyCombo()
        {
            try
            {
                mcbCompany.SelectedID = null;
                mcbCompany.SourceDataString = new string[3] { "CompID", "CompName","CompTelephone" };
                mcbCompany.ColumnWidth = new string[3] { "0", "300","100" };
                mcbCompany.ValueColumnNo = 0;
                //*mcbCompany.UserControlToShow = new UclPartyCompany(true);
                Company _Company = new Company();
                DataTable dtable = _Company.GetCompany();
                mcbCompany.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ClearControls()
        {
            try
            {
                mcbParty.Enabled = true;
                mcbParty.SelectedID = "";
                mcbCompany.SelectedID = "";
                _PartyCompany.Id = "";
                pnlList.SendToBack();
                pnlList.Visible = false;
                tsBtnPrint.Enabled = true;
                tsBtnCancel.Enabled = true;
                tsBtnSearch.Enabled = true;
                tsBtnFifth.Visible = false;            
                tsBtnSavenPrint.Visible = false;
                txtNoOfRows.Text = "";
                //FillCompanyData();
                if (_Mode == OperationMode.View)
                {
                    mcbCompany.Visible = false;
                    mPlbl2.Visible = false;
                    btnAdd.Visible = false;
                }
                else
                {
                    mcbCompany.Visible = true;
                    mPlbl2.Visible = true;
                    btnAdd.Visible = true;
                }
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructCompanyColumns()
        {

            dgvCompany.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CompId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvCompany.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company Name";
                column.Width = 450;
                column.ToolTipText = "Use Delete Key To Remove Company";
                dgvCompany.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "CompTelephone";
                column.HeaderText = "Telephone";
                column.Width = 150;
                column.ToolTipText = "Use Delete Key To Remove Company";
                dgvCompany.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedBy";
                column.DataPropertyName = "CreatedUserID";
                column.Visible = false;
                dgvCompany.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedDate";
                column.DataPropertyName = "CreatedDate";
                column.Visible = false;
                dgvCompany.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedTime";
                column.DataPropertyName = "CreatedTime";
                column.Visible = false;
                dgvCompany.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillCompanyData()
        {            
            DataTable dtable = new DataTable();

            try
            {
                if (mcbParty.SelectedID != null)
                    dtable = _PartyCompany.GetOverviewCompanyData(mcbParty.SelectedID);
                ConstructCompanyColumns();
                dgvCompany.Rows.Clear();
                //string compnm = "";
                for (int index = 0; index < dtable.Rows.Count; index++)
                {
                    int rowIndex = dgvCompany.Rows.Add();
                    DataGridViewRow dr = dgvCompany.Rows[rowIndex];
                    dr.Cells[0].Value = dtable.Rows[index]["CompId"].ToString();
                    dr.Cells[1].Value = dtable.Rows[index]["CompName"].ToString();
                    if (dtable.Rows[index]["Telephone"] != DBNull.Value)
                    {
                        dr.Cells[2].Value = dtable.Rows[index]["Telephone"].ToString();
                        //string tel = dtable.Rows[index]["Telephone"].ToString();
                        //compnm = compnm + dtable.Rows[index]["Telephone"].ToString();
                    }
                    else
                        dr.Cells[2].Value = "";
                    //  dr.Cells[1].Value = dtable.Rows[index]["CompName"].ToString();
                    //dr.Cells[3].Value = dtable.Rows[index]["CreatedUserID"].ToString();
                    //dr.Cells[4].Value = dtable.Rows[index]["CreatedDate"].ToString();
                    //dr.Cells[5].Value = dtable.Rows[index]["CreatedTime"].ToString();
                }
                if (dtable.Rows.Count > 0)
                    dgvCompany.Sort(dgvCompany.Columns[1], ListSortDirection.Ascending);
                dgvCompany.Refresh();
                NoofRows();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }     

        private bool IsCompanyAlreadyAdded(string companyID)
        {
            bool retValue = false;
            DataGridViewRow dr;
            try
            {
                for (int index = 0; index < dgvCompany.Rows.Count; index++)
                {
                    dr = dgvCompany.Rows[index];
                    if (dr.Cells[0].Value.ToString() == companyID)
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

        private bool IsPartyAlreadyLinked(string Id)
        {
            bool retValue = false;
            try
            {
                if (Id != null && Id != "" && _Mode != OperationMode.Edit)
                {
                    DataTable dt = new DataTable();
                    dt = null;
                    dt = _PartyCompany.IsPartyAlreadyLinked(Id);
                    if (dt.Rows.Count > 0)
                        retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void NoofRows()
        {
            int noofr = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvCompany.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                        noofr += 1;
                }
                txtNoOfRows.Text = noofr.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
                {
                    if (IsCompanyAlreadyAdded(mcbCompany.SelectedID) == false)
                    {
                        int index = dgvCompany.Rows.Add();
                        DataGridViewRow dr = dgvCompany.Rows[index];
                        dr.Cells[0].Value = mcbCompany.SelectedID;
                        dr.Cells[1].Value = mcbCompany.SeletedItem.Text;
                        dr.Cells[2].Value = mcbCompany.SeletedItem.ItemData[2].ToString();
                        mcbCompany.SelectedID = "";
                        dgvCompany.Sort(dgvCompany.Columns[1], ListSortDirection.Ascending);
                    }
                    else
                    {
                        MessageBox.Show("Company Already Linked", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mcbCompany.SelectedID = "";
                    }                   
                }
                NoofRows();
                mcbCompany.Focus();                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbParty_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                FillData(mcbParty.SelectedID);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mcbParty_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbParty.SelectedID != null && mcbParty.SelectedID != "")
                    FillData(mcbParty.SelectedID);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            btnAdd.Focus();
        }  
        #endregion

        # region ViewAll
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoOfRows.Text = "";
                pnlList.BringToFront();
                tsBtnPrint.Enabled = false;
                tsBtnCancel.Enabled = false;
                tsBtnSearch.Enabled = false;
                pnlList.Visible = true;
                dgvUpperListY.Visible = false;
                dgvLowerListY.Visible = false;
                dgvUpperList.Visible = true;
                dgvLowerList.Visible = true;
                ConstructUpperColumnsN();
                ConstructLowerColumnsN();
                FillUpperGridN();
                ConstructUpperColumnsY();
                ConstructLowerColumnsY();
                FillUpperGridY();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructUpperColumnsN()
        {
            dgvUpperList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvUpperList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party Name";
                column.Width = 430;
                dgvUpperList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 200;
                dgvUpperList.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillUpperGridN()
        {
            try
            {
                FillUpperDataN();
                dgvUpperList.DataSource = _BindingSource;
                dgvUpperList.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }       
        private void FillUpperDataN()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _PartyCompany.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructLowerColumnsN()
        {
            dgvLowerList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CompId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvLowerList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company Name";
                column.Width = 430;
                dgvLowerList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 200;
                dgvLowerList.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }        
        private void FillLowerGridN()
        {
            try
            {
                FillLowerDataN();
                dgvLowerList.DataSource = _BindingSource;
                dgvLowerList.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }      
        private void FillLowerDataN()
        {
            norn = 0;
            try
            {
                DataTable dtable = new DataTable();
                dtable = _PartyCompany.GetOverviewCompanyData(_PartyCompany.CurrentPartyId);
                _BindingSource = dtable;
                norn = dtable.Rows.Count;
                if (_IfReverse == "N")
                    txtNoOfRows.Text = norn.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void dgvUpperList_SelectedRowChanged(object sender, EventArgs e)
        {
            try
            {
                _PartyCompany.CurrentPartyId = dgvUpperList.SelectedRow.Cells[0].Value.ToString().Trim();
                FillLowerGridN();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void dgvUpperListY_SelectedRowChanged(object sender, EventArgs e)
        {

            try
            {
                _PartyCompany.CurrentPartyId = dgvUpperListY.SelectedRow.Cells[0].Value.ToString().Trim();
                FillLowerGridY();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnReverse_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IfReverse == "N")
                {
                    //ConstructGroupColumnsY();
                    //ConstructProductColumnsY();
                    //FillGroupGridY();
                    dgvUpperListY.Visible = true;
                    dgvLowerListY.Visible = true;
                    dgvUpperList.Visible = false;
                    dgvLowerList.Visible = false;
                    txtNoOfRows.Text = "";
                    txtNoOfRows.Text = nory.ToString();
                    _IfReverse = "Y";
                }
                else
                {
                    //ConstructGroupColumnsN();
                    //ConstructProductColumnsN();
                    //FillGroupGridN();
                    dgvUpperListY.Visible = false;
                    dgvLowerListY.Visible = false;
                    dgvUpperList.Visible = true;
                    dgvLowerList.Visible = true;
                    txtNoOfRows.Text = "";
                    txtNoOfRows.Text = norn.ToString();
                    _IfReverse = "N";
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructUpperColumnsY()
        {
            dgvUpperListY.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CompId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvUpperListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company Name";
                column.Width = 430;
                dgvUpperListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 200;
                dgvUpperListY.Columns.Add(column);
            
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }      
        private void FillUpperGridY()
        {
            try
            {
                FillUpperDataY();
                dgvUpperListY.DataSource = _BindingSource;
                dgvUpperListY.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }        
        private void FillUpperDataY()
        {
            try
            {
                DataTable dtable = new DataTable();               
                dtable = _PartyCompany.GetOverviewDataY();               
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructLowerColumnsY()
        {
            dgvLowerListY.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvLowerListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DrugName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party Name";
                column.Width = 430;
                dgvLowerListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 200;
                dgvLowerListY.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }        
        private void FillLowerGridY()
        {
            try
            {
                FillLowerDataY();
                dgvLowerListY.DataSource = _BindingSource;
                dgvLowerListY.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }      
        private void FillLowerDataY()
        {
            nory = 0;
            try
            {
                DataTable dtable = new DataTable();
                dtable = _PartyCompany.GetOverviewCompanyDataY(_PartyCompany.CurrentPartyId);
                _BindingSource = dtable;
                nory = dtable.Rows.Count;
                if (_IfReverse == "Y")
                    txtNoOfRows.Text = nory.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion ViewAll

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttPartyCompany.SetToolTip(mcbCompany, "Select Company");
                ttPartyCompany.SetToolTip(mcbParty, "Select Party");
                ttPartyCompany.SetToolTip(btnAdd, "Assign Selected Company to Party");                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion ToolTip

        private void rdbPartyWise_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlPartyCompany.Location = new System.Drawing.Point(79, 10);
            this.panelComapanyName.Location = new System.Drawing.Point(74, 82);
        }

        private void rdbCompanyWise_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlPartyCompany.Location = new System.Drawing.Point(74, 82);
            this.panelComapanyName.Location = new System.Drawing.Point(79, 10);
        }
    }
}

