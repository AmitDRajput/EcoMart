using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.Common;
using System.Collections;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPatient : BaseControl
    {
        # region Declaration
        private Patient _Patient;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion Declaration

        # region constructor
        public UclPatient()
        {
            try
            {
                InitializeComponent();
                _Patient = new Patient();
                SearchControl = new UclPatientSearch();

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.UclPatient>>" + Ex.Message);
            }
        }
        # endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Patient.Initialise();
                ClearControls();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ClearData>>" + Ex.Message);
            }
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            try
            {
                ClearData();
                pnlCenter.Enabled = true;
                FilltxtName();
                InitializeMainSubViewControl();
                FillDoctorCombo();
                AddToolTip();
                headerLabel1.Text = "PATIENT -> NEW";
                GetAccTokenNumber();
                txtName.Focus();
                txtTokenNumber.Text = _Patient.AccTokenNumber.ToString("#0");

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Add>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                pnlCenter.Enabled = true;
                headerLabel1.Text = "PATIENT -> EDIT";
                AddToolTip();
                FilltxtName();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Edit>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                pnlCenter.Enabled = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Cancel>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Delete()
        {
            try
            {
                bool retValue = base.Delete();
                headerLabel1.Text = "PATIENT -> DELETE";
                ClearData();
                FilltxtName();
                txtName.Focus();
                pnlCenter.Enabled = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Delete>>" + Ex.Message);
            }
            return true;
        }
        public override bool ProcessDelete()
        {
            bool retValue = true;
            try
            {
                _Patient = new Patient();
                _Patient.Id = txtName.SelectedID;
                if (_Patient.CanBeDeleted())
                {
                    _Patient.DeleteProductsById();
                    _Patient.DeleteDetails();
                    MessageBox.Show("Patient information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    retValue = true;
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                pnlCenter.Enabled = true;
                ClearData();
                FilltxtName();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ProcessDelete>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool View()
        {
            bool retValue = false;
            try
            {
                htTableList = General.GetTableListByCode("PatientID", "PatientName", "MasterPatient");
                retValue = base.View();
                pnlCenter.Enabled = true;
                ClearData();
                FilltxtName();
                headerLabel1.Text = "PATIENT -> VIEW";
                MoveLast();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.View>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Patient.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Patient.Id, "");

            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            try
            {
                CurrentNumber = htTableList.Count;
                if (htTableList.Contains(CurrentNumber))
                    _Patient.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Patient.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            CurrentNumber -= 1;
            if (htTableList.Contains(CurrentNumber))
                _Patient.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Patient.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Patient.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Patient.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _Patient.Name = txtName.Text.Trim();
                _Patient.Address1 = txtAutoAddress1.Text;
                _Patient.Address2 = txtAutoAddress2.Text;
                if (mcbDoctor.SelectedID != null)
                    _Patient.DoctorID = mcbDoctor.SelectedID.Trim();
                if (txtTelephone.Text.Trim() != null)
                    _Patient.Telephone = txtTelephone.Text.Trim();
                if (txtEmailId.Text.Trim() != null)
                    _Patient.Email = txtEmailId.Text.Trim();
                //if (txtNameAddress.Text.Trim() != null)
                //    _Patient.ShortNameAddress = txtNameAddress.Text.Trim();
                if (_Patient.ShortNameAddress.ToString().Length > 50)
                    _Patient.ShortNameAddress = _Patient.ShortNameAddress.ToString().Substring(0, 50);
                if (txtdd.Text.Trim() != "")
                    _Patient.Bday = Convert.ToInt32(txtdd.Text.Trim());
                if (txtmm.Text.Trim() != "")
                    _Patient.Bmonth = Convert.ToInt32(txtmm.Text.Trim());
                if (txtyy.Text.Trim() != null & txtyy.Text.Trim() != "")
                    _Patient.Byear = Convert.ToInt32(txtyy.Text.Trim());
                if (txtvisit1.Text.Trim() != "")
                    _Patient.Visit1 = Convert.ToInt32(txtvisit1.Text.Trim());
                if (txtvisit2.Text.Trim() != "")
                    _Patient.Visit2 = Convert.ToInt32(txtvisit2.Text.Trim());
                if (txtvisit3.Text.Trim() != "")
                    _Patient.Visit3 = Convert.ToInt32(txtvisit3.Text.Trim());
                if (rbtFemale.Checked == true)
                    _Patient.Gender = "F";
                else
                    _Patient.Gender = "M";
                if (txtRemark1.Text.Trim() != null)
                    _Patient.Remark1 = txtRemark1.Text.Trim();
                if (txtRemark2.Text.Trim() != null)
                    _Patient.Remark2 = txtRemark2.Text.Trim();
                if (txtRemark3.Text.Trim() != null)
                    _Patient.Remark3 = txtRemark3.Text.Trim();
                if (string.IsNullOrEmpty(txtTokenNumber.Text) == false)
                    _Patient.AccTokenNumber = Convert.ToInt32(txtTokenNumber.Text.Trim());
                //if (txtMobileNumberForSMS.Text != null)
                //    _Patient
                if (txtDiscPercent.Text != null)
                    _Patient.DiscountOffered = Convert.ToDouble(txtDiscPercent.Text.ToString());
                if (cbPutInBlackList.Checked == true)
                    _Patient.PutInBlackList = "Y";
                else
                    _Patient.PutInBlackList = "N";
                if (txtMobileNumber.Text != null)
                    _Patient.MobileNumberForSMS = txtMobileNumber.Text.ToString();
                if (_Mode == OperationMode.Edit)
                    _Patient.IFEdit = "Y";
                _Patient.Validate();
                if (_Patient.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _Patient.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Patient.CreatedBy = General.CurrentUser.Id;
                        _Patient.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Patient.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        if (_Patient.AccTokenNumber > 0 && _Patient.AccTokenNumber >= _Patient.CurrentTokenNumber)
                        {
                            retValue = _Patient.UpdateTokenNumber();
                        }
                        retValue = _Patient.AddDetails();

                        if (retValue == true)
                        {
                            saveproduct();
                            MessageBox.Show("Saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _Patient.Id;
                        }
                        else
                        {
                            MessageBox.Show("Unable to save Patient information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ClearControls();
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _Patient.ModifiedBy = General.CurrentUser.Id;
                        _Patient.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Patient.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Patient.DeleteProductsById();
                        retValue = _Patient.UpdateDetails();
                        saveproduct();
                        FilltxtName();
                        if (_Patient.AccTokenNumber > 0)
                        {
                            // retValue =  _Patient.UpdateTokenNumberInEditMode();
                        }
                        MessageBox.Show("Patient information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Patient.Id;
                        retValue = true;
                    }
                }
                else
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Patient.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Save>>" + Ex.Message);
            }
            return retValue;
        }
        public bool saveproduct()
        {
            bool returnVal = false;
            try
            {
                foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _Patient.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        _Patient.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _Patient.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        returnVal = _Patient.AddProductDetails();
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.SaveProduct>>" + Ex.Message);
            }
            return returnVal;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Patient.Id = ID;

                    _Patient.ReadDetailsByID();
                    InitializeMainSubViewControl();
                    FillDoctorCombo();

                    txtName.Text = _Patient.Name.Trim();
                    txtName.SelectedID = ID;
                    txtAutoAddress1.Text = _Patient.Address1.Trim();
                    txtAutoAddress2.Text = _Patient.Address2.Trim();
                    txtTelephone.Text = _Patient.Telephone.Trim();
                    txtEmailId.Text = _Patient.Email.Trim();
                    txtMobileNumber.Text = _Patient.MobileNumberForSMS.Trim();
                    txtdd.Text = _Patient.Bday.ToString().Trim();
                    txtmm.Text = _Patient.Bmonth.ToString().Trim();
                    txtyy.Text = _Patient.Byear.ToString().Trim();
                    txtvisit1.Text = _Patient.Visit1.ToString().Trim();
                    txtvisit2.Text = _Patient.Visit2.ToString().Trim();
                    txtvisit3.Text = _Patient.Visit3.ToString().Trim();
                    //txtNameAddress.Text = _Patient.ShortNameAddress.Trim();
                    txtRemark1.Text = _Patient.Remark1.Trim();
                    txtRemark2.Text = _Patient.Remark2.Trim();
                    txtRemark3.Text = _Patient.Remark3.Trim();
                    txtDiscPercent.Text = _Patient.DiscountOffered.ToString("#0.00");
                    if (_Patient.Gender == "F")
                        rbtFemale.Checked = true;
                    else
                        rbtMale.Checked = true;
                    txtTokenNumber.Text = _Patient.AccTokenNumber.ToString();
                    if (_Mode == OperationMode.Edit)
                    {
                        if (_Patient.AccTokenNumber == 0)
                            txtTokenNumber.ReadOnly = false;
                        else
                            txtTokenNumber.ReadOnly = true;
                    }
                    else
                        txtTokenNumber.ReadOnly = false;
                    mcbDoctor.SelectedID = _Patient.DoctorID;
                    if (_Patient.PutInBlackList == "Y")
                        cbPutInBlackList.Checked = true;
                    else
                        cbPutInBlackList.Checked = false;
                    mpMainSubViewControl.SetFocus(1);


                }
                if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                    pnlCenter.Enabled = false;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FillSearchData>>" + Ex.Message);
            }
            return true;
        }
        public void FilltxtName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[6] { "PatientID", "PatientName", "PatientAddress1", "PatientAddress2", "TelephoneNumber", "TokenNumber" };
                txtName.ColumnWidth = new string[6] { "0", "200", "150", "150", "100", "50" };
                Patient _Pat = new Patient();
                DataTable dtable = _Pat.GetOverviewData();
                txtName.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FilltxtName>>" + Ex.Message);
            }
        }
        #endregion IDetail Control

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclDoctor)
            {
                string preselectedID = "";
                if (mcbDoctor.SelectedID != null)
                    preselectedID = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = preselectedID;
            }

        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                txtAutoAddress1.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.B && modifier == Keys.Alt)
            {
                txtdd.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.D && modifier == Keys.Alt)
            {
                mcbDoctor.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.I && modifier == Keys.Alt)
            {
                txtEmailId.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.K && modifier == Keys.Alt)
            {
                txtTokenNumber.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.R && modifier == Keys.Alt)
            {
                txtRemark1.Focus();
                retValue = true;
            }
            //if (keyPressed == Keys.S && modifier == Keys.Alt)
            //{
            //    txtNameAddress.Focus();
            //    retValue = true;
            //}
            if (keyPressed == Keys.T && modifier == Keys.Alt)
            {
                txtTelephone.Focus();
                retValue = true;
            }

            if (keyPressed == Keys.V && modifier == Keys.Alt)
            {
                txtvisit1.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.Escape)
            {
                if (mpMainSubViewControl.VisibleProductGrid() == true) //kiran
                {
                    retValue = true;
                }
                else
                    retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        #endregion IDetail Members

        #region Other private methods

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[5] { "DocID", "DocName", "DocAddress", "DocTelephone", "DocEmailID" };
                mcbDoctor.ColumnWidth = new string[5] { "0", "350", "0", "0", "0" };    // [ansuman] [28.12.2016]
                mcbDoctor.ValueColumnNo = 0;
                UclDoctor uclDoc = new UclDoctor();
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _doctor = new Doctor();
                DataTable ddoctortable = _doctor.GetOverviewData();
                mcbDoctor.FillData(ddoctortable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FillDoctorCombo>>" + Ex.Message);
            }
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
                txtMobileNumber.Text = "";
                txtName.Text = "";
                txtName.SelectedID = "";
                txtAutoAddress1.Text = "";
                txtAutoAddress2.Text = "";
                txtTelephone.Clear();
                txtEmailId.Clear();
                //txtNameAddress.Clear();
                txtdd.Clear();
                txtmm.Clear();
                txtyy.Clear();
                txtAgeYears.Clear();
                rbtMale.Checked = true;
                txtDoctorAddress.Clear();
                txtDoctorTelephone.Clear();
                txtDoctorEmail.Clear();
                txtRemark1.Clear();
                txtRemark2.Clear();
                txtRemark3.Clear();
                txtvisit1.Clear();
                txtvisit2.Clear();
                txtvisit3.Clear();
                mcbDoctor.SelectedID = "";
                //  mpMainSubViewControl.ColumnsMain.Clear();
                txtTokenNumber.Text = "";
                txtTokenNumber.ReadOnly = false;
                txtNoOfRows.Text = "";
                cbPutInBlackList.Checked = false;
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ClearControls>>" + Ex.Message);
            }
        }

        private void NoofRows()
        {
            int itemCount = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }
                }
                if (mpMainSubViewControl.Rows[0].Cells["Col_ID"].Value != null && mpMainSubViewControl.Rows.Count == itemCount)
                    mpMainSubViewControl.Rows.Add();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.NoofRows>>" + Ex.Message);
            }
            txtNoOfRows.Text = itemCount.ToString().Trim();
        }

        private void ConstructMainColumns()
        {
            try
            {
                mpMainSubViewControl.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 300;
                column.ToolTipText = "Press Enter at Quantity for New Row";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 150;
                column.ReadOnly = true;
                column.ToolTipText = "Press Enter at Quantity for New Row";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 150;
                column.ReadOnly = true;
                column.ToolTipText = "Press Enter at Quantity for New Row";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 150;
                column.ReadOnly = true;
                column.ToolTipText = "Press Enter at Quantity for New Row";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Stock";
                column.Width = 80;
                column.Visible = false;
                column.ToolTipText = "Press Enter for New Row";
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 250;
                column.ToolTipText = "Press Enter for New Row";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsMain.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ConstructMainColumns>>" + Ex.Message);
            }

        }

        private void ConstructSubColumns()
        {
            try
            {
                mpMainSubViewControl.ColumnsSub.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProdName";
                column.Width = 300;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 52;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Stock";
                column.Width = 52;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpMainSubViewControl.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ConstructSubColumns>>" + Ex.Message);
            }
        }

        private void GetAccTokenNumber()
        {
            try
            {
                _Patient.GetCurrentTokenNumber();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.GetAccTokenNumber>" + Ex.Message);
            }
        }

        //private void GetAccTokenNumberForEdit()
        //{
        //    try
        //    {
        //       // _Patient.AccTokenNumber = _Patient.GetAccTokenNumber();              
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPatient.GetAccTokenNumber>" + Ex.Message);
        //    }
        //}
        #endregion

        # region Events

        private void InitializeMainSubViewControl()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                DataTable dtable = new DataTable();
                dtable = _Patient.ReadProductDetailsById(_Patient.Id);
                mpMainSubViewControl.DataSourceMain = dtable;
                mpMainSubViewControl.NumericColumnNames.Add("Col_Quantity");
                Product prod = new Product();
                DataTable dt = prod.GetOverviewData();
                //  DataTable dt = General.ProductList;
                mpMainSubViewControl.DataSource = dt;

                mpMainSubViewControl.Bind();
                NoofRows();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.InitializeMainSubViewControl>>" + Ex.Message);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtAutoAddress1.Focus();
                        //txtNameAddress.Text = txtName.Text.Trim() + " " + txtAddress1.Text.Trim();
                        break;
                    case Keys.Down:
                        txtAutoAddress1.Focus();
                        break;
                    case Keys.Up:
                        txtName.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtName_KeyDown>>" + Ex.Message);
            }

        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        //if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelephone.Text, @"^[0-9,]+$"))
                        //{
                        //    txtTelephone.Focus();
                        //    return;
                        //}
                        //else
                        txtMobileNumber.Focus();
                        break;
                    case Keys.Down:
                        txtMobileNumber.Focus();
                        break;
                    case Keys.Up:
                        txtAutoAddress1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTelephone_KeyDown>>" + Ex.Message);
            }
        }
        private void txtMobileNumberForSMS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtEmailId.Focus();
                        break;
                    case Keys.Down:
                        txtEmailId.Focus();
                        break;
                    case Keys.Up:
                        txtTelephone.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTelephone_KeyDown>>" + Ex.Message);
            }
        }


        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtdd.Focus();
                        break;
                    case Keys.Down:
                        txtdd.Focus();
                        break;
                    case Keys.Up:
                        txtMobileNumber.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtEmailId_KeyDown>>" + Ex.Message);
            }
        }



        private void txtdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtmm.Focus();
                        break;
                    case Keys.Down:
                        txtmm.Focus();
                        break;
                    case Keys.Up:
                        txtEmailId.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtdd_KeyDown>>" + Ex.Message);
            }
        }

        private void txtmm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtyy.Focus();
                        break;
                    case Keys.Down:
                        txtyy.Focus();
                        break;
                    case Keys.Up:
                        txtdd.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtmm_KeyDown>>" + Ex.Message);
            }
        }

        private void txtyy_KeyDown(object sender, KeyEventArgs e) // [ansuman][28.12.2016]
        {
            try
            {
                int CurrentYear = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            if (string.IsNullOrEmpty(txtyy.Text) == true || int.Parse(txtyy.Text) == 0)
                            {
                                txtAgeYears.Focus();
                            }
                            else if (txtyy.Text.Length == 4 && CurrentYear >= Convert.ToInt32(txtyy.Text))
                            {
                                txtAgeYears.Text = (CurrentYear - Convert.ToInt32(txtyy.Text)).ToString();
                                txtAgeYears.Focus();
                            }
                            else
                            {
                                txtyy.Focus();
                            }
                        }
                        break;
                    case Keys.Down:
                        txtAgeYears.Focus();
                        break;
                    case Keys.Up:
                        txtmm.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void txtAgeYears_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtvisit1.Focus();
                        break;
                    case Keys.Down:
                        txtvisit1.Focus();
                        break;
                    case Keys.Up:
                        txtyy.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void txtvisit1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtvisit2.Focus();
                        break;
                    case Keys.Down:
                        txtvisit2.Focus();
                        break;
                    case Keys.Up:
                        txtAgeYears.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtvisit1_KeyDown>>" + Ex.Message);
            }
        }

        private void txtvisit2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtvisit3.Focus();
                        break;
                    case Keys.Down:
                        txtvisit3.Focus();
                        break;
                    case Keys.Up:
                        txtvisit1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtvisit2_KeyDown>>" + Ex.Message);
            }
        }

        private void txtvisit3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtDiscPercent.Focus();
                        break;
                    case Keys.Down:
                        txtDiscPercent.Focus();
                        break;
                    case Keys.Up:
                        txtvisit2.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtvisit3_KeyDown>>" + Ex.Message);
            }
        }
        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        rbtMale.Focus();
                        break;
                    case Keys.Down:
                        rbtMale.Focus();
                        break;
                    case Keys.Up:
                        txtvisit3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtvisit3_KeyDown>>" + Ex.Message);
            }
        }


        private void rbtMale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtTokenNumber.Focus();
                        break;
                    case Keys.Down:
                        txtTokenNumber.Focus();
                        break;
                    case Keys.Up:
                        txtvisit3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTokenNumber_KeyDown>>" + Ex.Message);
            }
        }

        private void rbtFemale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtTokenNumber.Focus();
                        break;
                    case Keys.Down:
                        txtTokenNumber.Focus();
                        break;
                    case Keys.Up:
                        txtvisit3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTokenNumber_KeyDown>>" + Ex.Message);
            }
        }

        private void txtTokenNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        mcbDoctor.Focus();
                        break;
                    case Keys.Down:
                        mcbDoctor.Focus();
                        break;
                    case Keys.Up:
                        txtvisit3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTokenNumber_KeyDown>>" + Ex.Message);
            }
        }


        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtRemark1.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        txtRemark1.Focus();
                        break;
                    case Keys.Up:
                        txtTokenNumber.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }

        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbDoctor.SelectedID != "")
                {
                    txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                    txtDoctorTelephone.Text = mcbDoctor.SeletedItem.ItemData[3];
                    txtDoctorEmail.Text = mcbDoctor.SeletedItem.ItemData[4];

                    txtDoctorAddress.ReadOnly = true;
                    txtDoctorTelephone.ReadOnly = true;
                    txtDoctorEmail.ReadOnly = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_SeletectIndexChanged>>" + Ex.Message);
            }
        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string docId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = docId;
                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                txtDoctorTelephone.Text = mcbDoctor.SeletedItem.ItemData[3];
                txtDoctorEmail.Text = mcbDoctor.SeletedItem.ItemData[4];

                txtDoctorAddress.ReadOnly = true;
                txtDoctorTelephone.ReadOnly = true;
                txtDoctorEmail.ReadOnly = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_ItemAddedEdited>>" + Ex.Message);
            }
        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                txtRemark1.Focus();
                if (mcbDoctor.SelectedID != "")
                {
                    txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                    txtDoctorTelephone.Text = mcbDoctor.SeletedItem.ItemData[3];
                    txtDoctorEmail.Text = mcbDoctor.SeletedItem.ItemData[4];

                    txtDoctorAddress.ReadOnly = true;
                    txtDoctorTelephone.ReadOnly = true;
                    txtDoctorEmail.ReadOnly = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_EnterKeyPressed>>" + Ex.Message);
            }
        }
        private void txtRemark1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtRemark2.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        txtRemark2.Focus();
                        break;
                    case Keys.Up:
                        mcbDoctor.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }

        private void txtRemark2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtRemark3.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        txtRemark3.Focus();
                        break;
                    case Keys.Up:
                        txtRemark1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }

        private void txtRemark3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        mpMainSubViewControl.SetFocus(1);
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        mpMainSubViewControl.SetFocus(1);
                        break;
                    case Keys.Up:
                        mcbDoctor.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }
        //private void txtName_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPatient.txtName_TextChanged>>" + Ex.Message);
        //    }
        //}

        private void txtTokenNumber_Validating(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (txtTokenNumber.Text != null && Convert.ToInt32(txtTokenNumber.Text.ToString()) != 0)
            //    {
            //        if (_Mode == OperationMode.Add)
            //        {
            //            if (Convert.ToInt32(txtTokenNumber.Text.ToString()) != _Patient.CurrentTokenNumber)
            //            {
            //                lblMessage.Text = "Check Token Number";
            //              //  txtTokenNumber.Text = _Patient.CurrentTokenNumber.ToString("#0");
            //                txtTokenNumber.Focus();
            //            }
            //            else
            //                lblMessage.Text = "";
            //        }
            //        else
            //            if (_Mode == OperationMode.Edit)
            //            {
            //                if (Convert.ToInt32(txtTokenNumber.Text.ToString()) != _Patient.CurrentTokenNumber && Convert.ToInt32(txtTokenNumber.Text.ToString()) > 0)
            //                {
            //                  ////  GetAccTokenNumberForEdit();
            //                  //  if (Convert.ToInt32(txtTokenNumber.Text.ToString()) != _Patient.AccTokenNumber)
            //                  //  lblMessage.Text = "Current TokenNumber = " + Convert.ToString(_Patient.AccTokenNumber);                            
            //                }
            //                else
            //                    lblMessage.Text = "";
            //            }
            //    }
            //    else
            //        lblMessage.Text = "";
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteError("UclPatient.txtTokenNumber_Validating>>" + Ex.Message);
            //}
        }

        private void mpMainSubViewControl_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            string mprodID = "";
            int mrowindex = 0;
            int mcindex = 0;
            _Patient.DuplicateProduct = false;
            try
            {
                if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
                {
                    mprodID = mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                    mrowindex = mpMainSubViewControl.MainDataGridCurrentRow.Index;
                }
                foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        _Patient.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        mcindex = prodrow.Index;
                        if (_Patient.ProductID == mprodID && mrowindex != mcindex)
                        {
                            _Patient.DuplicateProduct = true;
                            mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.Rows[mrowindex]);
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mpMainSubViewControl_OnDetailsFilled>>" + Ex.Message);
            }
        }

        private void mpMainSubViewControl_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 5)
                {
                    int mqty = 0;
                    int.TryParse(mpMainSubViewControl.MainDataGridCurrentRow.Cells[5].Value.ToString(), out mqty);
                    if (mqty <= 0)
                    {
                        mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);
                        mpMainSubViewControl.Rows.Add();
                    }
                    else
                        lblMessage.Text = "";
                    NoofRows();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mpMainSubViewControl_OnCellValueChangeCommited>>" + Ex.Message);
            }
        }

        private void mpMainSubViewControl_OnRowDeleted(object sender, EventArgs e)
        {
            NoofRows();
        }

        private void headerLabel1_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                FillSearchData(txtName.SelectedID, "");
                if (_Mode == OperationMode.Add)
                {
                    txtName.SelectedID = "";
                    _Patient.Id = "";
                }
                txtAutoAddress1.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtName_EnterKeyPressed>>" + Ex.Message);
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        txtAddress2.Focus();
                        break;
                    case Keys.Enter:
                        txtAddress2.Focus();
                        break;
                    case Keys.Up:
                        txtName.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtAddress1_KeyDown>>" + Ex.Message);
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        txtTelephone.Focus();
                        break;
                    case Keys.Enter:
                        txtTelephone.Focus();
                        break;
                    case Keys.Up:
                        txtAddress1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtAddress2_KeyDown>>" + Ex.Message);
            }
        }

        //private void txtAddress1_TextChanged(object sender, EventArgs e)
        //{
        //    txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        //}

        # endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttPatient.SetToolTip(txtName, "use A-Z,0-9,space only");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

        #region UIEvents

        private void UclPatient_Load(object sender, EventArgs e)
        {
            FilltxtAutoAddress1();
            FilltxtAutoAddress2();
        }
        private void FilltxtAutoAddress1()
        {
            try
            {
                txtAutoAddress1.SelectedID = null;
                txtAutoAddress1.SourceDataString = new string[1] { "PatientAddress1" };
                txtAutoAddress1.ColumnWidth = new string[1] { "350" };
                txtAutoAddress1.DisplayColumnNo = 0;
                txtAutoAddress1.ValueColumnNo = 0;
                Patient _Pat = new Patient();
                DataTable dtable = _Pat.GettxtAddress1();
                txtAutoAddress1.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FilltxtAutoAddress1>>" + Ex.Message);
            }
        }
        private void FilltxtAutoAddress2()
        {
            try
            {
                txtAutoAddress2.SelectedID = null;
                txtAutoAddress2.SourceDataString = new string[1] { "PatientAddress2" };
                txtAutoAddress2.ColumnWidth = new string[1] { "350" };
                txtAutoAddress2.DisplayColumnNo = 0;
                txtAutoAddress2.ValueColumnNo = 0;
                Patient _Pat = new Patient();
                DataTable dtable = _Pat.GettxtAddress2();
                txtAutoAddress2.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FilltxtAutoAddress2>>" + Ex.Message);
            }
        }

        private void txtAutoAddress2_EnterKeyPressed(object sender, EventArgs e)
        {
            txtTelephone.Focus();
        }

        private void txtAutoAddress1_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAutoAddress2.Focus();
        }

        private void txtAutoAddress2_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtAutoAddress1.Focus();
        }

        private void txtAutoAddress1_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void mpMainSubViewControl_OnTABKeyPressed(object sender, EventArgs e)
        {
            if (mpMainSubViewControl.Rows.Count > 1 && string.IsNullOrEmpty(Convert.ToString(mpMainSubViewControl.MainDataGridCurrentRow.Cells[0].Value)) == true)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion UIEvents
    }
}
