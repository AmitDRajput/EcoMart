using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;
using PharmaSYSPlus.CommonLibrary;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPrescription : BaseControl
    {
        # region Declaration
        private Prescription _Prescription;
        # endregion Declaration

        # region Constructor
        public UclPrescription()
        {
            InitializeComponent();
            _Prescription = new Prescription();
            SearchControl = new UclPrescriptionSearch();
        }
        #endregion Constructor

      
        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Prescription.Initialise();
            ClearControls();
            txtName.Focus();
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            pnlCenter.Enabled = true;
            FilltxtName();
            InitializeMainSubViewControl();
            AddToolTip();
            headerLabel1.Text = "PRESCRIPTION -> NEW";
            txtName.Focus();
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            pnlCenter.Enabled = true;
            headerLabel1.Text = "PRESCRIPTION -> EDIT";
            AddToolTip();
            FilltxtName();
            txtName.Focus();
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            pnlCenter.Enabled = true;
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "PRESCRIPTION -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            pnlCenter.Enabled = true;
            return true;
        }


        public override bool ProcessDelete()
        {
            bool retValue = false;
            _Prescription = new Prescription();
            _Prescription.Id = txtName.SelectedID;
            if (_Prescription.CanBeDeleted())
            {
                _Prescription.DeleteProductsById();
                _Prescription.DeleteDetails();
                MessageBox.Show("Prescription information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                retValue = true;

            }
            else
            {
                MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            pnlCenter.Enabled = true;
            ClearData();
            FilltxtName();
            return retValue;
        }

        public override bool View()
        {
            bool retValue = base.View();
            pnlCenter.Enabled = true;
            ClearData();
            FilltxtName();
            headerLabel1.Text = "PRESCRIPTION -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Prescription.Name = txtName.Text.Trim();
            if (_Mode == OperationMode.Edit)
                _Prescription.IFEdit = "Y";
            _Prescription.Validate();
            if (_Prescription.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Prescription.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Prescription.CreatedBy = General.CurrentUser.Id;
                    _Prescription.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Prescription.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Prescription.AddDetails();
                    saveproduct();
                    if (retValue == true)
                    {
                        MessageBox.Show("Presciption information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Prescription.Id;
                        
                    }

                    else
                    {
                        MessageBox.Show("Unable to save Patient information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                    retValue = true;
                }

                else if (_Mode == OperationMode.Edit)
                {
                    _Prescription.ModifiedBy = General.CurrentUser.Id;
                    _Prescription.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Prescription.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Prescription.DeleteProductsById();
                    retValue = _Prescription.UpdateDetails();
                    saveproduct();
                    MessageBox.Show("Prescription information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Prescription.Id;                   
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Prescription.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retValue;
        }
        public bool saveproduct()
        {
            bool returnVal = false;
            int mrow = mpMainSubViewControl1.Rows.Count;
            try
            {
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _Prescription.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        _Prescription.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _Prescription.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        returnVal = _Prescription.AddProductDetails();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _Prescription.Id = ID;

                _Prescription.ReadDetailsByID();
                InitializeMainSubViewControl();
              //  txtName.Text = _Prescription.Name.Trim();
                NoofRows();
            }
            //if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                
            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "prescriptionID", "PrescriptionName"};
            txtName.ColumnWidth = new string[2] { "0", "300"};
            Prescription _Pre = new Prescription();
            DataTable dtable = _Pre.GetOverviewData();
            txtName.FillData(dtable);
        }
        #endregion IDetail Control

        #region IDetail Members

      

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

        #endregion IDetail Members 

        # region Other Private Methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void ClearControls()
        {
            txtName.Text = "";
            txtNoOfRows.Text = "";
            mpMainSubViewControl1.ColumnsMain.Clear();
        }

        private void NoofRows()
        {
            int itemCount = 0;

            foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
            {
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                {
                    itemCount += 1;
                }

            }
            txtNoOfRows.Text = itemCount.ToString().Trim();
        }

        private void ConstructMainColumns()
        {
            mpMainSubViewControl1.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product Name";
            column.Width = 200;
            if (_Mode == OperationMode.View)
                column.ReadOnly = true;
            else
                column.ReadOnly = false;
            column.ToolTipText = "Press Enter at Quantity for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_LoosePack";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 130;
            column.ReadOnly = true;
            column.ToolTipText = "Press Enter at Quantity for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 130;
            column.ReadOnly = true;
            column.ToolTipText = "Press Enter at Quantity for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 130;
            column.ReadOnly = true;
            column.ToolTipText = "Press Enter at Quantity for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Clstk";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "ClosingStock";
            column.Visible = false;
            column.Width = 110;
            column.ToolTipText = "Press Enter for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Quantity";
            if (_Mode == OperationMode.View)
                column.ReadOnly = true;
            else
                column.ReadOnly = false;
            column.Width = 110;
            column.ToolTipText = "Press Enter for New Row";
            mpMainSubViewControl1.ColumnsMain.Add(column);


        }
        private void ConstructSubColumns()
        {
            //ProductID,ProdName,ProdPack, ProdCompShortName
            mpMainSubViewControl1.ColumnsSub.Clear();

            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "ProdName";
            column.Width = 200;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "ProdLoosePack";
            column.Width = 60;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdPack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "ProdPack";
            column.Width = 60;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 60;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "Cl.Stock";
            column.Width = 60;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);
        }
        private void InitializeMainSubViewControl()
        {
            ConstructMainColumns();
            ConstructSubColumns();
            //Fill Main Grid
            DataTable dtable = new DataTable();
            
            dtable = _Prescription.ReadProductDetailsById(_Prescription.Id);
            mpMainSubViewControl1.DataSourceMain = dtable;
            mpMainSubViewControl1.NumericColumnNames.Add("Col_Quantity");

            Product prod = new Product();
            DataTable dt = General.ProductList;
            mpMainSubViewControl1.DataSource = dt;

            mpMainSubViewControl1.Bind();
        }
      
        # endregion
        # region Events

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtName.SelectedID != null && txtName.SelectedID != "")
            {
                FillSearchData(txtName.SelectedID,"");
               
            }
            mpMainSubViewControl1.SetFocus(1);
        }
     
        private void mpMainSubViewControl1_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 5)
            {
                int mqty = 0;
                int.TryParse(mpMainSubViewControl1.MainDataGridCurrentRow.Cells[5].Value.ToString(), out mqty);
                if (mqty <= 0)
                {
                    mpMainSubViewControl1.Rows.Remove(mpMainSubViewControl1.MainDataGridCurrentRow);                
                    mpMainSubViewControl1.Rows.Add();
                }
                else
                    lblMessage.Text = "";              

            }
            NoofRows();
        }
      

        private void mpMainSubViewControl1_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            string mprodID = "";
            int mrowindex = 0;
            int mcindex = 0;
            _Prescription.DuplicateProduct = false;
            if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
            {
                mprodID = mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                mrowindex = mpMainSubViewControl1.MainDataGridCurrentRow.Index;
            }
            foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
            {
                if (prodrow.Cells["Col_ID"].Value != null)
                {
                    _Prescription.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                    mcindex = prodrow.Index;
                    if (_Prescription.ProductID == mprodID && mrowindex != mcindex)
                    {
                        _Prescription.DuplicateProduct = true;
                        mpMainSubViewControl1.Rows.Remove(mpMainSubViewControl1.Rows[mrowindex]);
                        //   mpMainSubViewControl.Rows.Add();

                        break;
                    }
                }
            }
        }

        private void mpMainSubViewControl1_OnRowDeleted(object sender, EventArgs e)
        {
            NoofRows();

        }
        private void headerLabel1_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        # endregion Events

        #region tooltip
        private void AddToolTip()
        {

            ttPrescription.SetToolTip(mpMainSubViewControl1, "Press Enter at Quantity for new Row");
            ttPrescription.SetToolTip(txtName, "use A-Z,0-9,space only");
        }
        #endregion

              
    }
}

