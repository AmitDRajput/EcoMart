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
using PharmaSYSPlus.CommonLibrary;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDebtorProduct : BaseControl
    {
        # region Declaration     
        private DebtorProduct _DebtorProduct;
        private DataTable _BindingSource;        
        private string _IfReverse = "N";
        private int norn = 0;
        private int nory = 0;    
        # endregion

        # region Constructor
        public UclDebtorProduct()
        {
            try
            {
                InitializeComponent();
                _DebtorProduct = new DebtorProduct();
                SearchControl = new UclDebtorProductSearch();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor

        # region Initialize
        public void RefreshData()
        {
            try
            {
                FillPartyCombo();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Initialize

        # region IDetail Control
        public override void SetFocus()
        {
            mcbCreditor.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _DebtorProduct.Initialise();
                ClearControls();
                mcbCreditor.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                headerLabel1.Text = "DEBTOR PRODUCT -> NEW";
                FillPartyCombo();
                InitializeMainSubViewControl();
                mcbCreditor.SelectedID = "";
                tsBtnTrueFalse();
                AddToolTip();
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Edit()
        {           
            return true;
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
                ClearData();
                headerLabel1.Text = "DEBTOR PRODUCT -> VIEW";
                FillPartyCombo();
                InitializeMainSubViewControl();
                mcbCreditor.SelectedID = "";
                AddToolTip();
               tsBtnTrueFalse();
                mcbCreditor.Focus();
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
                _DebtorProduct.Id = mcbCreditor.SelectedID;
                _DebtorProduct.Validate();

                if (_DebtorProduct.IsValid)
                {
                    LockTable.LockTableForLinkDebtorProduct();

                    General.BeginTransaction();
                    
                    retValue =  _DebtorProduct.DeleteProductsById();

                    if (retValue)
                        retValue = saveproduct();
                    if (retValue)
                        General.CommitTransaction();
                    else
                        General.RollbackTransaction();
                    LockTable.UnLockTables();
                    if (retValue)
                    {
                        MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _DebtorProduct.Id;
                        mcbCreditor.SelectedID = "";
                        retValue = true;
                    }
                    else
                    {
                        MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        retValue = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool saveproduct()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in mpMSVC1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _DebtorProduct.CreatedBy = "";
                        _DebtorProduct.CreatedDate = "";
                        _DebtorProduct.CreatedTime = "";
                        _DebtorProduct.ProductID = Convert.ToInt32(prodrow.Cells["Col_ID"].Value.ToString());
                        _DebtorProduct.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        if (prodrow.Cells["Col_CreatedBy"].Value != null)
                            _DebtorProduct.CreatedBy = prodrow.Cells["Col_CreatedBy"].Value.ToString();
                        if (prodrow.Cells["Col_CreatedDate"].Value != null)
                            _DebtorProduct.CreatedDate = prodrow.Cells["Col_CreatedDate"].Value.ToString();
                        if (prodrow.Cells["Col_CreatedTime"].Value != null)
                            _DebtorProduct.CreatedTime = prodrow.Cells["Col_CreatedTime"].Value.ToString();
                        if (prodrow.Cells["Col_ModifiedBy"].Value != null)
                            _DebtorProduct.CreatedBy = prodrow.Cells["Col_ModifiedBy"].Value.ToString();
                        if (prodrow.Cells["Col_ModifiedDate"].Value != null)
                            _DebtorProduct.CreatedDate = prodrow.Cells["Col_ModifiedDate"].Value.ToString();
                        if (prodrow.Cells["Col_ModifiedTime"].Value != null)
                            _DebtorProduct.CreatedTime = prodrow.Cells["Col_ModifiedTime"].Value.ToString();
                        if (_DebtorProduct.CreatedBy == "")
                            _DebtorProduct.CreatedBy = General.CurrentUser.Id;
                        if (_DebtorProduct.CreatedDate == "")
                            _DebtorProduct.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        if (_DebtorProduct.CreatedTime == "")
                            _DebtorProduct.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _DebtorProduct.ModifiedBy = General.CurrentUser.Id;
                        _DebtorProduct.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DebtorProduct.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        _DebtorProduct.DetailId  = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        returnVal = _DebtorProduct.AddProductDetails();
                        if (returnVal == false)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _DebtorProduct.Id = ID;

                    InitializeMainSubViewControl();               
                    mcbCreditor.Enabled = false;
                    mpMSVC1.SetFocus(1);
                }
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclAccount)
                {
                    string preselectedID = "";
                    if (mcbCreditor.SelectedID != null)
                        preselectedID = mcbCreditor.SelectedID;
                    FillPartyCombo();
                    mcbCreditor.SelectedID = preselectedID;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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

        #region other private methods

        private void InitializeMainSubViewControl()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                //Fill Main Grid
                DataTable dtable = new DataTable();
                dtable = _DebtorProduct.ReadProdDetailsById(_DebtorProduct.Id);
                mpMSVC1.DataSourceMain = dtable;
                mpMSVC1.NumericColumnNames.Add("Col_Quantity");

                Product prod = new Product();
                DataTable proddt = prod.GetOverviewData();
             //   DataTable dt = General.ProductList;
                mpMSVC1.DataSource = proddt;

                mpMSVC1.Bind();
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    if (_Mode == OperationMode.Add)
                    {
                        mpMSVC1.Rows.Add();
                        mpMSVC1.SetFocus(mpMSVC1.Rows.Count - 1, 1);
                    }                    
                }
                NoofRows();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbCreditor.ColumnWidth = new string[4] { "0", "20", "200", "200" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                //    mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForDebtor);
                mcbCreditor.FillData(dtable);
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
                mcbCreditor.Enabled = true;
                mcbCreditor.SelectedID = null;
                mpMSVC1.Rows.Clear();
                ConstructMainColumns();
                pnlList.SendToBack();
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
                if (_Mode == OperationMode.View)
                    btnViewAll.Visible = true;
                else
                    btnViewAll.Visible = false;
                mpMSVC1.Rows.Add();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }            
        }
     

        private bool IsPartyAlreadyLinked(string Id)
        {
            bool retValue = false;
            try
            {
                DataTable dt = new DataTable();
                dt = null;
                dt = _DebtorProduct.IsPartyAlreadyLinked(Id);
                if (dt.Rows.Count > 0)
                    retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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

         private void NoofRows()
         {
             int itemCount = 0;

             try
             {
                 foreach (DataGridViewRow dr in mpMSVC1.Rows)
                 {
                     if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                     {
                         itemCount += 1;
                     }

                 }
                 txtNoOfRows.Text = itemCount.ToString().Trim();
             }
             catch (Exception Ex)
             {
                 Log.WriteException(Ex);
             }
         }
        #endregion

        #region contruct grid

        private void ConstructMainColumns()
        {
           
            try
            {
                mpMSVC1.Rows.Clear();
                mpMSVC1.ColumnsMain.Clear();
                //0
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //1

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 200;
                if (_Mode == OperationMode.Add)
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
                column.ToolTipText = "Use Delete Key To Remove Product";
                mpMSVC1.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSVC1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSVC1.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedBy";
                column.DataPropertyName = "CreatedUserID";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedDate";
                column.DataPropertyName = "CreatedDate";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedTime";
                column.DataPropertyName = "CreatedTime";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ModifiedBy";
                column.DataPropertyName = "ModifiedUserID";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ModifiedDate";
                column.DataPropertyName = "ModifiedDate";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ModifiedTime";
                column.DataPropertyName = "ModifiedTime";
                //column.HeaderText = "Quantity";
                //column.Width = 100;
                column.Visible = false;
                mpMSVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 80;
                if (_Mode == OperationMode.Add)
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ValueType = typeof(int);
                mpMSVC1.ColumnsMain.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSubColumns()
        {
          
            mpMSVC1.ColumnsSub.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMSVC1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 200;
                column.ReadOnly = true;
                mpMSVC1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC1.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }    

        # endregion

        # region Events  
        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    FillSearchData(mcbCreditor.SelectedID,"");
                    mpMSVC1.SetFocus(1);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
      

        private void mpmsvc1_OnRowDeleted(object sender, EventArgs e)
        {
            NoofRows();
        }

        private void mpmsvc1_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            int mprodID = 0;
            int mrowindex = 0;
            int mcindex = 0;
            try
            {
                _DebtorProduct.DuplicateProduct = false;
                if (mpMSVC1.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
                {
                    mprodID = Convert.ToInt32(mpMSVC1.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString());
                    mrowindex = mpMSVC1.MainDataGridCurrentRow.Index;
                }
                foreach (DataGridViewRow prodrow in mpMSVC1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        _DebtorProduct.ProductID = Convert.ToInt32(prodrow.Cells["Col_ID"].Value.ToString());
                        mcindex = prodrow.Index;
                        if (_DebtorProduct.ProductID == mprodID && mrowindex != mcindex)
                        {
                            _DebtorProduct.DuplicateProduct = true;
                            mpMSVC1.Rows.Remove(mpMSVC1.Rows[mrowindex]);                          

                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpmsvc1_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 11)
                {
                    int mqty = 0;
                    int.TryParse(mpMSVC1.MainDataGridCurrentRow.Cells[11].Value.ToString(), out mqty);
                    if (mqty <= 0)
                    {
                        mpMSVC1.Rows.Remove(mpMSVC1.MainDataGridCurrentRow);
                        mpMSVC1.Rows.Add();
                    }
                    else
                        lblMessage.Text = "";
                    NoofRows();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        # endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttDebtorProduct.SetToolTip(mcbCreditor, "Select Debtor");  
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion ToolTip

        #region ViewAll
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
                ConstructUpperColumnsNForAccount();
                ConstructLowerColumnsNForProduct();
                FillUpperGridNForAccount();
                ConstructUpperColumnsYForProduct();
                ConstructLowerColumnsYForAccount();
                FillUpperGridY();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructUpperColumnsNForAccount()
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
                column.Width = 330;
                dgvUpperList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 300;
                dgvUpperList.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillUpperGridNForAccount()
        {
            try
            {
                FillUpperDataNForAccount();
                dgvUpperList.DataSource = _BindingSource;
                dgvUpperList.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillUpperDataNForAccount()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DebtorProduct.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructLowerColumnsNForProduct()
        {
            dgvLowerList.Columns.Clear();
            try
            {
                //0
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvLowerList.Columns.Add(column);
                //1

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 200;
                if (_Mode == OperationMode.Add)
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
                column.ToolTipText = "Use Delete Key To Remove Product";
                dgvLowerList.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 100;
                column.ReadOnly = true;
                dgvLowerList.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 100;
                column.ReadOnly = true;
                dgvLowerList.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 100;
                column.ReadOnly = true;
                dgvLowerList.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 80;
                if (_Mode == OperationMode.Add)
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ValueType = typeof(int);
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
                dtable = _DebtorProduct.GetOverviewDebtorData(_DebtorProduct.AccountID);
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
              //  _PartyCompany.CurrentPartyId = dgvUpperList.SelectedRow.Cells[0].Value.ToString().Trim();
                _DebtorProduct.AccountID = dgvUpperList.SelectedRow.Cells[0].Value.ToString();
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
               // _PartyCompany.CurrentPartyId = dgvUpperListY.SelectedRow.Cells[0].Value.ToString().Trim();
                _DebtorProduct.ProductID = Convert.ToInt32(dgvUpperListY.SelectedRow.Cells[0].Value.ToString());
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
                    FillUpperGridY();
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
                    FillLowerDataY();
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
        private void ConstructUpperColumnsYForProduct()
        {
            dgvUpperListY.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvUpperListY.Columns.Add(column);
                //1

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 200;
                if (_Mode == OperationMode.Add)
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
                column.ToolTipText = "Use Delete Key To Remove Product";
                dgvUpperListY.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 100;
                column.ReadOnly = true;
                dgvUpperListY.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 100;
                column.ReadOnly = true;
                dgvUpperListY.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 100;
                column.ReadOnly = true;
                dgvUpperListY.Columns.Add(column);
                //5
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Quantity";
                //column.DataPropertyName = "Quantity";
                //column.HeaderText = "Quantity";
                //column.Width = 80;
                //if (_Mode == OperationMode.Add)
                //    column.ReadOnly = false;
                //else
                //    column.ReadOnly = true;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.ValueType = typeof(int);
                //dgvUpperListY.Columns.Add(column);

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
              //  dtable = _PartyCompany.GetOverviewDataY();
                dtable = _DebtorProduct.GetOverviewProductDataY();
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructLowerColumnsYForAccount()
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
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party Name";
                column.Width = 330;
                dgvLowerListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 300;
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
             //   dtable = _PartyCompany.GetOverviewCompanyDataY(_PartyCompany.CurrentPartyId);
                dtable = _DebtorProduct.GetOverviewDebtorDataY(_DebtorProduct.ProductID);
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
    }
}