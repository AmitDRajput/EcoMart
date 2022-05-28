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
    public partial class UclLinkProductScheduleH1 : BaseControl
    {
        # region Declaration
        private ProductSchedulH1 _ProductSchedulH1;     
        # endregion

        # region Constructor
        public UclLinkProductScheduleH1()
        {
            InitializeComponent();
            _ProductSchedulH1 = new ProductSchedulH1();
        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            mcbProduct.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                
               _ProductSchedulH1.Initialise();
                ClearControls();               
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
                headerLabel1.Text = "PRODUCT SCHEDULE H1 -> NEW";
                FillProductCombo();
                ConstructProdColumns();
                FillProdData();
                tsBtnTrueFalse(); 
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
                tsBtnTrueFalse();
                headerLabel1.Text = "PRODUCT SCHEDULE H1 -> VIEW";
                FillProductCombo();              
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
                tsBtnCancel.Visible = false;

            }
        }
        public override bool Save()
        {
            _ProductSchedulH1.ProductID = 0;
            _ProductSchedulH1.RemoveH1TagInProductMaster();
            if (dgvProduct.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvProduct.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _ProductSchedulH1.ProductID = Convert.ToInt32(dr.Cells["Col_ID"].Value.ToString());
                        _ProductSchedulH1.SetProdScheduleCode(_ProductSchedulH1.ProductID);
                        
                    //    EcoMartCache.RefreshProductData(_ProductSchedulH1.ProductID);
                        
                    }
                }
            }
            return true;
          
        }

        public bool saveproduct()
        {            
            return true;           
        }

        #endregion IDetail Control

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclProduct)
                    FillProductCombo();
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

       
       
      
        private void ClearControls()
        {
            try
            {
                mcbProduct.SelectedID = "";
                txtNoOfRows.Text = "";
            //    FillProdData();
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
               
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
             }
             catch (Exception Ex) { Log.WriteException(Ex); }
         }
        #endregion

        # region contruct grid

         private void ConstructProdColumns()
         {
             dgvProduct.Columns.Clear();
             try
             {
                 DataGridViewTextBoxColumn column;

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_ID";
                 column.DataPropertyName = "ProductID";
                 column.HeaderText = "ID";
                 column.Visible = false;
                 dgvProduct.Columns.Add(column);

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_ProductName";
                 column.DataPropertyName = "ProdName";
                 column.HeaderText = "Product Name";
                 column.Width = 400;
                 column.ToolTipText = "Use Delete Key To Remove Product";
                 dgvProduct.Columns.Add(column);

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_UOM";
                 column.DataPropertyName = "ProdLoosePack";
                 column.HeaderText = "UOM";
                 column.Width = 100;                
                 dgvProduct.Columns.Add(column);
                 //3
                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_Pack";
                 column.DataPropertyName = "ProdPack";
                 column.HeaderText = "Pack";
                 column.Width = 100;                 
                 dgvProduct.Columns.Add(column);
                 //4
                 //column = new DataGridViewTextBoxColumn();
                 //column.Name = "Col_CompShortName";
                 //column.DataPropertyName = "ProdCompShortName";
                 //column.HeaderText = "Comp";
                 //column.Width = 100;
                 //column.ReadOnly = true;
                 //dgvProduct.ColumnsMain.Add(column);

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_CreatedBy";
                 column.DataPropertyName = "CreatedUserID";
                 column.Visible = false;
                 dgvProduct.Columns.Add(column);

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_CreatedDate";
                 column.DataPropertyName = "CreatedDate";
                 column.Visible = false;
                 dgvProduct.Columns.Add(column);

                 column = new DataGridViewTextBoxColumn();
                 column.Name = "Col_CreatedTime";
                 column.DataPropertyName = "CreatedTime";
                 column.Visible = false;
                 dgvProduct.Columns.Add(column);

             }
             catch (Exception Ex) { Log.WriteException(Ex); }
         }

         private void FillProdData()
         {
             try
             {
                 dgvProduct.Refresh();
                 DataTable dtable = new DataTable();
                 dtable = _ProductSchedulH1.ReadProductDetailsById();
                 dgvProduct.Rows.Clear();
                 for (int index = 0; index < dtable.Rows.Count; index++)
                 {
                     int rowIndex = dgvProduct.Rows.Add();
                     DataGridViewRow dr = dgvProduct.Rows[rowIndex];
                     dr.Cells[0].Value = dtable.Rows[index]["ProductID"].ToString();
                     dr.Cells[1].Value = dtable.Rows[index]["ProdName"].ToString();
                     dr.Cells[2].Value = dtable.Rows[index]["ProdLoosePack"].ToString();
                     dr.Cells[3].Value = dtable.Rows[index]["ProdPack"].ToString();
                    
                 }
                 if (dtable.Rows.Count > 0)
                     dgvProduct.Sort(dgvProduct.Columns[1], ListSortDirection.Ascending);
                 NoofRows();
             }
             catch (Exception Ex) { Log.WriteException(Ex); }
         }


        # endregion

        # region Events      

        private void mpmsvc1_OnRowDeleted(object sender, EventArgs e)
        {
            NoofRows();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAddClick();
        }

        private void btnAddClick()
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                {
                    if (IsProductAlreadyAdded(Convert.ToInt32(mcbProduct.SelectedID)) == false)
                    {
                        int index = dgvProduct.Rows.Add();
                        DataGridViewRow dr = dgvProduct.Rows[index];
                        dr.Cells[0].Value = mcbProduct.SelectedID;
                        dr.Cells[1].Value = mcbProduct.SeletedItem.Text;
                        dr.Cells[2].Value = mcbProduct.SeletedItem.ItemData[2].ToString();
                        dr.Cells[3].Value = mcbProduct.SeletedItem.ItemData[3].ToString();
                        mcbProduct.SelectedID = "";
                    }
                    else
                    {
                        MessageBox.Show("Product Already Linked", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mcbProduct.SelectedID = "";
                    }
                }
                NoofRows();
                mcbProduct.Focus();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private bool IsProductAlreadyAdded(int ProductID)
        {
            bool retValue = false;
            DataGridViewRow dr;
            try
            {
                for (int index = 0; index < dgvProduct.Rows.Count; index++)
                {
                    dr = dgvProduct.Rows[index];
                    if (Convert.ToInt32(dr.Cells[0].Value.ToString()) == ProductID)
                    {
                        retValue = true;
                        break;
                    }
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }      
      

        # endregion

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            btnAdd.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            btnRemoveClick();
        }

         private void btnRemoveClick()
        {
            _ProductSchedulH1.ProductID = 0;
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
            {
                _ProductSchedulH1.ProductID = Convert.ToInt32(mcbProduct.SelectedID);
                if (dgvProduct.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgvProduct.Rows)
                    {
                        if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                        {
                            if (_ProductSchedulH1.ProductID == Convert.ToInt32(dr.Cells["Col_ID"].Value.ToString()))
                            {
                                dgvProduct.Rows.Remove(dr);
                                NoofRows();
                                break;                               
                            }                                
                        }
                    }
                }
            }
            mcbProduct.SelectedID = "";
            mcbProduct.Focus();
        }

         private void btnAdd_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
                 btnAddClick();
             else if (e.KeyCode == Keys.Up)
                 btnRemove.Focus();
         }

         private void btnRemove_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
                 btnRemoveClick();
             else if (e.KeyCode == Keys.Up)
                 mcbProduct.Focus();
         }

        #region tooltip
        //private void AddToolTip()
        //{
        //    try
        //    {
        //        ttDebtorProduct.SetToolTip(mcbCreditor, "Select Debtor");  
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        #endregion ToolTip       
      
    }
    
}
