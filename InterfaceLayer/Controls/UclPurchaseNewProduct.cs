using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.DataLayer;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer.CommonControls;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class UclPurchaseNewProduct : UserControl
    {
        private PSMainSubViewControl _MainViewControl;

        public PSMainSubViewControl MainViewControl
        {
            get { return _MainViewControl; }
            set { _MainViewControl = value; }
        }

        Account _Party = new Account();
        private Product _Product = new Product();
        public UclPurchaseNewProduct()
        {
            InitializeComponent();
            FillCreditorCombo();
            FillProductCombo();
        }

        private void FillCreditorCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "20", "200", "150", "50", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
                mcbProduct.ColumnWidth = new string[5] { "0", "200", "50", "50", "50" };
                DBProduct _dbProduct = new DBProduct();
                DataTable dtable = _dbProduct.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception)
            {
            }
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
            {
                _Party.Id = mcbCreditor.SelectedID;
                _Party.ReadDetailsByID();
            }
            txtQuantity.Focus();
        }
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != string.Empty)
                    FillSearchData(mcbProduct.SelectedID, "");
                if (mcbCreditor.SeletedItem == null)
                    mcbCreditor.Focus();
                else
                    txtQuantity.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        public bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Product.Id = ID;
                    _Product.ReadDetailsByID();
                    if (string.IsNullOrEmpty(Convert.ToString(_Product.ProdCreditor1ID)) == false)
                    {
                        mcbCreditor.SelectedIntID  = Convert.ToInt32(_Product.ProdCreditor1ID.ToString()) ;
                        _Party.Id = mcbCreditor.SelectedID;
                        _Party.ReadDetailsByID();
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(_Product.ProdCreditor2ID)) == false)
                    {
                        mcbCreditor.SelectedIntID = Convert.ToInt32(_Product.ProdCreditor2ID.ToString()) ;
                        _Party.Id = mcbCreditor.SelectedID;
                        _Party.ReadDetailsByID();
                    }
                    txtQuantity.Text = Convert.ToString(_Product.ProdBoxQty);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
            return true;
        }

        private void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainViewControl.dgMainGrid != null && string.IsNullOrEmpty(_Party.Id) == false && string.IsNullOrEmpty(_Product.Id) == false && txtQuantity.Text != null && txtQuantity.Text.ToString() != string.Empty && Convert.ToInt32(txtQuantity.Text.ToString()) > 0)
                {
                    foreach (DataGridViewRow item in MainViewControl.dgMainGrid.Rows)
                    {
                        if (_Party.Id == Convert.ToString(item.Cells[0].Value) && _Product.Name == Convert.ToString(item.Cells[4].Value))
                        {
                            int qty1 = Convert.ToInt32(item.Cells[18].Value);
                            int qty2 = Convert.ToInt32(item.Cells[19].Value);
                            item.Cells[18].Value = qty1 + Convert.ToInt32(txtQuantity.Text);
                            item.Cells[19].Value = qty2 + Convert.ToInt32(txtQuantity.Text);
                            SetControltoDefault();
                            return;
                        }
                    }
                    int i = MainViewControl.dgMainGrid.Rows.Add();
                    DataGridViewRow dr = MainViewControl.dgMainGrid.Rows[i];
                    dr.Cells["Col_ACCID"].Value = _Party.Id;
                    dr.Cells["Col_AccName"].Value = _Party.AccName;
                    dr.Cells["Col_ShortListID"].Value = "0";
                    dr.Cells["Col_ProdID"].Value = _Product.Id;
                    dr.Cells["Col_ProductName"].Value = _Product.Name;
                    dr.Cells["Col_UOM"].Value = _Product.ProdLoosePack;
                    dr.Cells["Col_Pack"].Value = _Product.ProdPack;
                    dr.Cells["Col_ProdCompShortName"].Value = _Product.ProdCompShortName;
                    dr.Cells["Col_BoxQty"].Value = _Product.ProdBoxQty;
                    dr.Cells["Col_ClosingStock"].Value = _Product.ProdClosingStock;
                    dr.Cells["Col_ID1"].Value = "";
                    dr.Cells["Col_AccName1"].Value = "";
                    dr.Cells["Col_ID2"].Value = "";
                    dr.Cells["Col_Check1"].Value = "";
                    dr.Cells["Col_AccName2"].Value = "";
                    dr.Cells["Col_IfSave"].Value = "True";
                    dr.Cells["Col_PurchaseRate"].Value = _Product.ProdlastPurchaseRate;
                    dr.Cells["Col_OrderNumber"].Value = "0";
                    dr.Cells["Col_Quantity1"].Value = txtQuantity.Text;
                    dr.Cells["Col_Quantity"].Value = txtQuantity.Text;
                    dr.Cells["Col_SchemeQuantity"].Value = txtSchemeQuantity.Text;
                    dr.Cells["Col_AccAddress1"].Value = "";
                    dr.Cells["Col_AccAddress2"].Value = "";
                    SetControltoDefault();
                }
                else
                    SetControltoDefault();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSchemeQuantity.Focus();
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                mcbCreditor.Focus();
            }
        }
        private void txtSchemeQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddNewProduct.Focus();
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                txtQuantity.Focus();
            }
        }
        private void SetControltoDefault()
        {
            mcbProduct.Focus();
            mcbCreditor.SelectedID = string.Empty;
            mcbProduct.SelectedID = string.Empty;
            txtQuantity.Text = "";
            txtSchemeQuantity.Text = "";
        }

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            //if (MainViewControl.dgMainGrid != null)
            //{
            //    MainViewControl.dgMainGrid.Focus();
            //}
            mcbProduct.Focus();
        }

        private void mcbProduct_UpArrowPressed(object sender, EventArgs e)
        {
            if (MainViewControl.dgMainGrid != null)
            {
                MainViewControl.dgMainGrid.Focus();
            }
            //mcbCreditor.Focus();
        }

        private void btnAddNewProduct_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtQuantity.Focus();
            }
        }       
    }
}