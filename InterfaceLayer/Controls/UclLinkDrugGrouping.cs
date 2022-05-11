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
    public partial class UclDrugGrouping : BaseControl 
    {
        # region Declaration   
        private DataTable _BindingSource;
        private DrugGrouping _DrugGrouping;
        private string _IfReverse = "N";
        private int norn = 0;
        private int nory = 0;
        # endregion

        # region Constructor
        public UclDrugGrouping()
        {
            try
            {

                InitializeComponent();
                _DrugGrouping = new DrugGrouping();
                SearchControl = new UclDrugGroupingSearch();
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        # endregion       

        # region IDetail Control

        public override void SetFocus()
        {
            mcbDrug.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _DrugGrouping.Initialise();
                ClearControls();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return true;
        }
        public override bool Add()
        {
            bool retValue = false;
            try
            {
                retValue = base.Add();
                ClearData();
                btnAdd.Visible = true;
                btnViewAll.Visible = false;
                headerLabel1.Text = "DRUG GROUPING -> NEW";
                AddToolTip();
                FillDrugCombo();
                FillProductCombo();
                ConstructProdColumns();
                tsBtnTrueFalse();
                dgvProduct.Enabled = false;
                mcbDrug.Enabled = true;
                ConstructLinkedProductsToAddColumns(dgLinkProductsToAdd); // [23.01.2017]
            } 
            catch (Exception Ex) { Log.WriteException(Ex); }
          
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
                headerLabel1.Text = "DRUG GROUPING -> EDIT";
                AddToolTip();
                FillDrugCombo();
                FillProductCombo();
                ConstructProdColumns();              
                dgvProduct.Enabled = false;
                mcbDrug.Enabled = true;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;          
        }
            
        public override  bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                ClearData();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
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
                this.Cursor = Cursors.Default;
                retValue = base.View();
                btnAdd.Visible = false;
                btnViewAll.Visible = true;
                ClearData();
                FillDrugCombo();
                lblProduct.Visible = false;
                mcbProduct.Visible = false;
                tsBtnTrueFalse();
                headerLabel1.Text = "DRUG GROUPING -> VIEW";
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
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
                _DrugGrouping.Id = mcbDrug.SelectedID;
               // retValue = _DrugGrouping.DeleteDetails();
                _DrugGrouping.ProductId = Convert.ToInt32( dgvProduct.Rows.Count.ToString());
                _DrugGrouping.Validate();
                if (_DrugGrouping.IsValid)
                {
                    LockTable.LockTableForLinkDrugGrouping();

                    General.BeginTransaction();

                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _DrugGrouping.CreatedBy = General.CurrentUser.Id;
                        _DrugGrouping.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DrugGrouping.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _DrugGrouping.ClearDrugIDInProductMaster();
                        //if (IsDrugAlreadyLinked(mcbDrug.SelectedID) == false)
                        //{
                            for (int index = 0; index < dgvProduct.Rows.Count; index++)
                            {
                                dr = dgvProduct.Rows[index];
                                if (dr.Cells[0].Value.ToString().Trim() != null && dr.Cells[0].Value.ToString().Trim() != "")
                                {
                                    _DrugGrouping.IntID  = Convert.ToInt32( mcbDrug.SelectedID.ToString());
                                    _DrugGrouping.ProductId = Convert.ToInt32(dr.Cells[0].Value.ToString());
                                //SS
                                 /* * _DrugGrouping.DetailId = _DrugGrouping.GetIntID();  Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/                                                                      // retValue = _DrugGrouping.AddDetails();
                                retValue = _DrugGrouping.UpdateProductMaster();
                                    if (retValue == false)
                                        break;
                                }
                            }
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                MessageBox.Show("DrugGrouping information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _SavedID = _DrugGrouping.Id;
                                mcbDrug.SelectedID = "";
                                retValue = true;
                            }
                            else
                            {
                                MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                retValue = false;
                            }
                        //}
                        //else
                        //{                            
                        //    General.RollbackTransaction();
                        //    LockTable.UnLockTables();
                        //    MessageBox.Show("Drug Already Linked.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    mcbDrug.SelectedID = "";
                        //}
                    }
                    //else if (_Mode == OperationMode.Edit)
                    //{
                    //    _DrugGrouping.Id = mcbDrug.SelectedID;
                    //    retValue = _DrugGrouping.DeleteDetails();
                    //    _DrugGrouping.ModifiedBy = General.CurrentUser.Id;
                    //    _DrugGrouping.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    //    _DrugGrouping.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    //    for (int index = 0; index < dgvProduct.Rows.Count; index++)
                    //    {
                    //        _DrugGrouping.CreatedBy = "";
                    //        _DrugGrouping.CreatedDate = "";
                    //        _DrugGrouping.CreatedTime = "";
                    //        dr = dgvProduct.Rows[index];
                    //        _DrugGrouping.Id = mcbDrug.SelectedID;
                    //        _DrugGrouping.ProductId = dr.Cells[0].Value.ToString();
                    //        if (dr.Cells[2].Value != null)
                    //            _DrugGrouping.CreatedBy = dr.Cells[2].Value.ToString();
                    //        if (dr.Cells[3].Value != null)
                    //            _DrugGrouping.CreatedDate = dr.Cells[3].Value.ToString();
                    //        if (dr.Cells[4].Value != null)
                    //            _DrugGrouping.CreatedTime = dr.Cells[4].Value.ToString();
                    //        if (_DrugGrouping.CreatedBy == "")
                    //            _DrugGrouping.CreatedBy = General.CurrentUser.Id;
                    //        if (_DrugGrouping.CreatedDate == "")
                    //            _DrugGrouping.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    //        if (_DrugGrouping.CreatedTime == "")
                    //            _DrugGrouping.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    //        _DrugGrouping.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    //        retValue = _DrugGrouping.AddDetails();
                    //        if (retValue == false)
                    //            break;
                    //    }
                    //    if (retValue)
                    //        General.CommitTransaction();
                    //    else
                    //        General.RollbackTransaction();
                    //    LockTable.UnLockTables();
                    //    if (retValue)
                    //    {
                    //        MessageBox.Show("DrugGrouping Information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        _SavedID = _DrugGrouping.Id;
                    //        mcbDrug.SelectedID = "";
                    //        retValue = true;
                    //    }
                    //    else
                    //    {                           
                    //        MessageBox.Show("Drug Already Linked.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        mcbDrug.SelectedID = "";
                    //        retValue = false;
                    //    }
                    //}

                    ClearData();
                }
                else
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _DrugGrouping.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mcbDrug.Enabled = true;
                    mcbDrug.Focus();
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _DrugGrouping.Id = ID;
                    _DrugGrouping.ReadDetailsByID();                  
                    AddToolTip();                  
                    FillProdData();
                    dgvProduct.Enabled = true;
                    mcbDrug.Enabled = false;
                    mcbProduct.Focus();
                }
                mcbProduct.Focus();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return true;
        }
        # endregion IDetail Control

        # region IDetail Members  
        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclProduct)
                    FillProdData();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
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
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDrug.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    mcbProduct.Focus();
                    retValue = true;
                }              
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    btnViewAll.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Control)
                {
                    if (dgLinkProductsToAdd.Focus())
                    {
                        for (int i = 0; i < dgLinkProductsToAdd.Rows.Count; i++)
                        {
                            if (dgLinkProductsToAdd.Rows[i].Cells["Col_ProdName"].Value.ToString() != "")
                                dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value = ((char)0x221A).ToString();
                        }
                    }
                }
                if (keyPressed == Keys.O && modifier == Keys.Control)
                {
                    if(dgLinkProductsToAdd.Focus())
                    {
                        btnAdd.Focus();
                    }
                }
                if (keyPressed == Keys.Escape) // [24.01.2017]
                {
                    if (dgLinkProductsToAdd.Focus())
                    {
                        
                    }
                    else
                        retValue = Exit();
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        } 
        #endregion IDetail Members

        #region other private methods

        private void FillDrugCombo()
        {
            try
            {
                mcbDrug.SelectedID = null;
                mcbDrug.SourceDataString = new string[2] { "GenericCategoryID", "GenericCategoryName" };
                mcbDrug.ColumnWidth = new string[2] { "0", "400" };
                mcbDrug.ValueColumnNo = 0;               
                GenericCategory _Drug = new GenericCategory();
                DataTable dtable = _Drug.GetOverviewData();
                mcbDrug.FillData(dtable);
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        public static DataTable DtProds;  // [23.01.2017]
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[4] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack" };
                mcbProduct.ColumnWidth = new string[4] { "0", "200", "30", "30" };
                mcbProduct.ValueColumnNo = 0;
                //    DataTable dtable = General.ProductList;
                Product prod = new Product();
                DataTable dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
                DtProds = dtable;  // [23.01.2017]

                //foreach (DataColumn col in DtProds.Columns)
                //{
                //    if (col.ColumnName != "ProductID" && col.ColumnName != "ProdName" && col.ColumnName != "ProdPack")
                //    {
                //        DtProds.Columns.Remove(col.ColumnName);
                //    }
                //}
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ClearControls()
        {
            try
            {
                mcbDrug.Enabled = true;
                mcbDrug.SelectedID = "";
                mcbProduct.SelectedID = "";
                _DrugGrouping.Id = "";
                pnlList.SendToBack();
                pnlList.Visible = false;
                tsBtnPrint.Enabled = true;
                tsBtnCancel.Enabled = true;
                tsBtnSearch.Enabled = true;
                txtNoOfRows.Text = "";
                mcbDrug.Focus();
                FillProdData();
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ConstructProdColumns()
        {
            dgvProduct.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvProduct.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 600;
                column.ToolTipText = "Use Delete Key To Remove Product";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvProduct.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedBy";
                column.DataPropertyName = "CreatedUserID";
                column.Visible = false;
                dgvProduct.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedDate";
                column.DataPropertyName = "CreatedDate";
                column.Visible = false;
                dgvProduct.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedTime";
                column.DataPropertyName = "CreatedTime";
                column.Visible = false;
                dgvProduct.Columns.Add(column);
                //5
                ////newly added

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PackType";
                column.HeaderText = "PackType";
                column.DataPropertyName = "ProdPack";
                column.Width = 140;
                column.Visible = true;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvProduct.Columns.Add(column);
                //6
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void FillProdData()
        {
            try
            {
                dgvProduct.Refresh();
                DataTable dtable = new DataTable();              
                if (mcbDrug.SelectedID != null && mcbDrug.SelectedID != "")
                    dtable = _DrugGrouping.GetOverviewProductData(mcbDrug.SelectedIntID);               
                dgvProduct.Rows.Clear();
                ConstructProdColumns();
                for (int index = 0; index < dtable.Rows.Count; index++)
                {
                    int rowIndex = dgvProduct.Rows.Add();
                    DataGridViewRow dr = dgvProduct.Rows[rowIndex];
                    dr.Cells[0].Value = dtable.Rows[index]["ProductId"].ToString();
                    dr.Cells[1].Value = dtable.Rows[index]["ProdName"].ToString();
                    dr.Cells[5].Value = dtable.Rows[index]["ProdPack"].ToString();               
                }
                if (dtable.Rows.Count > 0)
                    dgvProduct.Sort(dgvProduct.Columns[1], ListSortDirection.Ascending);
                NoofRows();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }     

        private bool IsProductAlreadyAdded(string ProductID)
        {
            bool retValue = false;
            DataGridViewRow dr;
            try
            {
                for (int index = 0; index < dgvProduct.Rows.Count; index++)
                {
                    dr = dgvProduct.Rows[index];
                    if (dr.Cells[0].Value.ToString() == ProductID)
                    {
                        retValue = true;
                        break;
                    }
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }

        private bool IsDrugAlreadyLinked(string Id)
        {
            bool retValue = false;
            try
            {
                if (Id != null && Id != "" && _Mode != OperationMode.Edit)
                {
                    DataTable dt = new DataTable();
                    dt = null;
                    dt = _DrugGrouping.IsDrugAlreadyLinked(Id);
                    if (dt.Rows.Count > 0)
                        retValue = true;
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }

        private void NoofRows()
        {
            int noofr = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvProduct.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                        noofr += 1;
                }
                txtNoOfRows.Text = noofr.ToString();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
    
         #endregion
      
        # region Events 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                //{
                //    if (IsProductAlreadyAdded(mcbProduct.SelectedID) == false)
                //    {
                //        int index = dgvProduct.Rows.Add();
                //        DataGridViewRow dr = dgvProduct.Rows[index];
                //        dr.Cells[0].Value =  mcbProduct.SelectedID;
                //        dr.Cells[1].Value =  mcbProduct.SeletedItem.Text;
                //        mcbProduct.SelectedID = "";
                //    }
                //    else
                //    {
                //        MessageBox.Show("Product Already Linked", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        mcbProduct.SelectedID = "";
                //    }
                //}
                
                dgvProduct.Visible = true;
                
                bool flag = false;

                for (int i = 0; i < dgLinkProductsToAdd.Rows.Count; i++)
                {
                    flag = false;
                    if (string.IsNullOrEmpty(Convert.ToString(dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value)) == false)
                    {
                        for (int j = 0; j < dgvProduct.Rows.Count; j++)
                        {
                            if (dgLinkProductsToAdd.Rows[i].Cells["Col_LinkProdID"].Value.ToString() == dgvProduct.Rows[j].Cells["Col_ID"].Value.ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value != null && dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value.ToString() == ((char)0x221A).ToString())
                    {
                        if (flag == false)
                        {
                            int index = dgvProduct.Rows.Add();
                            dgvProduct.Rows[index].Cells["Col_ID"].Value = dgLinkProductsToAdd.Rows[i].Cells["Col_LinkProdID"].Value;
                            dgvProduct.Rows[index].Cells["Col_ProductName"].Value = dgLinkProductsToAdd.Rows[i].Cells["Col_ProdName"].Value;
                            dgvProduct.Rows[index].Cells["Col_PackType"].Value = dgLinkProductsToAdd.Rows[i].Cells["Col_PackType"].Value;
                        }
                    }
                }
                dgLinkProductsToAdd.Visible = false;

                NoofRows();
                mcbProduct.Focus();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      

        private void mcbDrug_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                FillSearchData(mcbDrug.SelectedID,"");
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void mcbDrug_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbDrug.SelectedID != null && mcbDrug.SelectedID != "")
                FillSearchData(mcbDrug.SelectedID,"");
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            btnAdd.Focus();
        } 
        # endregion          

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
                ConstructGroupColumnsN();
                ConstructProductColumnsN();
                FillGroupGridN();
                ConstructGroupColumnsY();
                ConstructProductColumnsY();
                FillGroupGridY();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ConstructGroupColumnsN()
        {
            dgvUpperList.Columns.Clear();
            DataGridViewTextBoxColumn column;
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "GenericCategoryId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvUpperList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DrugName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "Drug Name";
                column.Width = 630;
                dgvUpperList.Columns.Add(column);

            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      
        private void FillGroupGridN()
        {
            try
            {
                FillGroupDataN();
                dgvUpperList.DataSource = _BindingSource;
                dgvUpperList.Bind();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void FillGroupDataN()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DrugGrouping.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ConstructProductColumnsN()
        {
            dgvLowerList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvLowerList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 630;
                dgvLowerList.Columns.Add(column);
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }       
        private void FillProductGridN()
        {
            try
            {
                FillProductDataN();
                dgvLowerList.DataSource = _BindingSource;
                dgvLowerList.Bind();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }     
        private void FillProductDataN()
        {
            norn = 0;
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DrugGrouping.GetOverviewProductData(_DrugGrouping.CurrentDrugId);
                _BindingSource = dtable;
                norn = dtable.Rows.Count;
                if (_IfReverse == "N")
                    txtNoOfRows.Text = norn.ToString();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void dgvUpperList_SelectedRowChanged(object sender, EventArgs e)
        {
            try
            {
                _DrugGrouping.CurrentDrugId = Convert.ToInt32(dgvUpperList.SelectedRow.Cells[0].Value.ToString().Trim());
                FillProductGridN();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void dgvUpperListY_SelectedRowChanged(object sender, EventArgs e)
        {
            try
            {
                _DrugGrouping.CurrentDrugId = Convert.ToInt32(dgvUpperListY.SelectedRow.Cells[0].Value.ToString().Trim());
                FillProductGridY();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }       
        private void btnReverse_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IfReverse == "N")
                {                   
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
                    dgvUpperListY.Visible = false;
                    dgvLowerListY.Visible = false;
                    dgvUpperList.Visible = true;
                    dgvLowerList.Visible = true;
                    txtNoOfRows.Text = "";
                    txtNoOfRows.Text = norn.ToString();
                    _IfReverse = "N";
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ConstructGroupColumnsY()
        {
            dgvUpperListY.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvUpperListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 630;
                dgvUpperListY.Columns.Add(column);
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      
        private void FillGroupGridY()
        {
            try
            {
                FillGroupDataY();
                dgvUpperListY.DataSource = _BindingSource;
                dgvUpperListY.Bind();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }       
        private void FillGroupDataY()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DrugGrouping.GetOverviewDataY();
                _BindingSource = dtable;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        private void ConstructProductColumnsY()
        {
            dgvLowerListY.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "GenericCategoryId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvLowerListY.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DrugName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "Drug Name";
                column.Width = 630;
                dgvLowerListY.Columns.Add(column);
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      
        private void FillProductGridY()
        {
            try
            {
                FillProductDataY();
                dgvLowerListY.DataSource = _BindingSource;
                dgvLowerListY.Bind();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }        
        private void FillProductDataY()
        {
            nory = 0;
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DrugGrouping.GetOverviewProductDataY(_DrugGrouping.CurrentDrugId);
                _BindingSource = dtable;
                nory = dtable.Rows.Count;
                if (_IfReverse == "Y")
                    txtNoOfRows.Text = nory.ToString();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }

        #endregion ViewAll

        #region ToolTip


        private void AddToolTip()
        {
            try
            {
                ttDrugGrouping.SetToolTip(mcbDrug, "Select Drug");
                ttDrugGrouping.SetToolTip(mcbProduct, "Select Product");
                ttDrugGrouping.SetToolTip(btnAdd, "Assign Product to Selected Drug");
                ttDrugGrouping.SetToolTip(btnViewAll, "Remove Product From the List");
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        #endregion

        public void ConstructLinkedProductsToAddColumns(DataGridView dgLinkProductsToAdd)
        {
            DataGridViewTextBoxColumn column;
            dgLinkProductsToAdd.Rows.Clear();

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_LinkProdID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ProductID";
            column.Width = 50;
            column.Visible = false;
            column.ReadOnly = true;
            dgLinkProductsToAdd.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Check";
            column.HeaderText = "Check";
            column.Width = 60;
            column.Visible = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLinkProductsToAdd.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdName";
            column.HeaderText = "ProdName";
            column.DataPropertyName = "ProdName";
            column.Width = 180;
            column.Visible = true;
            column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgLinkProductsToAdd.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PackType";
            column.HeaderText = "  PackType  ";
            column.DataPropertyName = "ProdPack";
            column.Width = 140;
            column.Visible = true;
            column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgLinkProductsToAdd.Columns.Add(column);
        }

        private void UclDrugGrouping_Load(object sender, EventArgs e)
        {
            //ConstructLinkedProductsToAddColumns(dgLinkProductsToAdd);
            //dgLinkProductsToAdd.Rows.Add();
            //dgLinkProductsToAdd.Rows[0].Cells[0].Value = ((char)0x221A).ToString();
            //dgLinkProductsToAdd.Rows[0].Cells[1].Value = "CROCIN";
            //dgLinkProductsToAdd.Rows[0].Cells[2].Value = "SYROP";

        }

        private void mcbProduct_TextValueChanged(object sender, EventArgs e)
        {
            

        }

        private void mcbProduct_KeyDown(object sender, KeyEventArgs e)
        {
            
        }


        private void mcbProduct_TabKeyPressed(object sender, EventArgs e)
        {
            try
            {
                dgvProduct.Visible = false;
                dgLinkProductsToAdd.Visible = true;
                dgLinkProductsToAdd.BringToFront();
                //mcbProduct.Enabled = false;
                //dgLinkProductsToAdd.Focus();
                dgLinkProductsToAdd.CurrentCell = dgLinkProductsToAdd.Rows[0].Cells[2];
                dgLinkProductsToAdd.BeginEdit(true);
                //mcbProduct.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbProduct_OnTextValueChanged(string Text)
        {
            try
            {
                //dgLinkProductsToAdd.Visible = true;
                //dgLinkProductsToAdd.BringToFront();
                DataRow[] NewDt = DtProds.Select("ProdName like '" + Text + "%' ");

                //DtProds = DtProds.DefaultView.ToTable(false, "ProductID", "ProdName", "ProdPack");

                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("ProductID", typeof(String));
                dt.Columns.Add(dc);

                dc = new DataColumn("ProdName", typeof(String));
                dt.Columns.Add(dc);

                dc = new DataColumn("ProdPack", typeof(String));
                dt.Columns.Add(dc);

                foreach (var items in NewDt)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = items["ProductID"];
                    dr[1] = items["ProdName"];
                    dr[2] = items["ProdPack"];
                    dt.Rows.Add(dr);
                }
                dgLinkProductsToAdd.DataSource = dt;
                dgvProduct.Visible = false;
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgLinkProductsToAdd_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgLinkProductsToAdd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int i = dgLinkProductsToAdd.CurrentRow.Index;
                    if (dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value == null || dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value.ToString() == "")
                        dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value = ((char)0x221A).ToString();
                    else
                        dgLinkProductsToAdd.Rows[i].Cells["Col_Check"].Value = "";
                }
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
