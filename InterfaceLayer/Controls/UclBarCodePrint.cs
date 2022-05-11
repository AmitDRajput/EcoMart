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
    public partial class UclBarCodePrint : BaseControl
    {
        # region Declaration
        public string CorrectionID = string.Empty;
        private BarCode _BarCode;
        #endregion declaration

        #region Constructor
        public UclBarCodePrint()
        {
            try
            {
                InitializeComponent();
                _BarCode = new BarCode();
                SearchControl = new UclCorrectioninRateSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclBarCodePrint.Constructor>>" + Ex.Message);
            }
        }
        #endregion constructor

        #region IDetail Control
        public override void SetFocus()
        {
            mcbProduct.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _BarCode.Initialise();
                ClearControls();
                mcbProduct.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.ClearData>>" + Ex.Message);
            }
            return true;
        }
        public override bool Add()
        {
            bool retValue = true;
            try
            {
                base.Add();
                ClearData();
                headerLabel1.Text = "CORRECTION IN RATE -> ADD";
                FillProductCombo();
                ConstructBatchGrid();
                dgvBatchGrid.Visible = false;
                mcbProduct.Enabled = true;
                pnlOld.Enabled = true;
                dgvBatchGrid.Enabled = true;
                mcbProduct.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.Add>>" + Ex.Message);
            }

            return retValue;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (ID != null && ID != "")
                {
                    //_BarCode.Id = ID;
                    //_BarCode.ReadDetailsByID();
                    //mcbProduct.SelectedID = _BarCode.ProductId;
                    //txtbatchno.Text = _BarCode.BatchNo.ToString();
                    //txtexpiry.Text = _BarCode.Expiry.ToString();
                    //txtMrp.Text = _BarCode.Mrp.ToString("#0.00");
                    //txtPurchaseRate.Text = _BarCode.PurchaseRate.ToString("#0.00");
                    //txtSaleRate.Text = _BarCode.SaleRate.ToString("#0.00");
                    //txtQuantity.Text = _BarCode.Qty.ToString("#0");



                    pnlOld.Enabled = false;
                    mcbProduct.Enabled = false;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.FillSearchDate>>" + Ex.Message);
            }
            this.Cursor = Cursors.Default;
            return true;
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
        private void ClearControls()
        {
            try
            {
                txtbatchno.Text = "";
                txtexpiry.Text = "";
                txtSaleRate.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtQuantity.Text = "";
                txtMrp.Text = "0.00";
                mcbProduct.SelectedID = "";
                txtpack.Text = "";
                txtloosepack.Text = "";
                txtcompany.Text = "";

                dgvBatchGrid.Rows.Clear();



                pnlOld.Enabled = true;

                mcbProduct.Enabled = true;


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.ClearControl>>" + Ex.Message);
            }

        }
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[6] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName", "ProdClosingStock" };
                mcbProduct.ColumnWidth = new string[6] { "0", "250", "50", "50", "50", "50" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                Product prod = new Product();
                DataTable dtable = prod.GetForClosingStockNotZero();
                mcbProduct.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.FillProductCombo>>" + Ex.Message);
            }
        }
        private void ConstructBatchGrid()
        {

            dgvBatchGrid.Columns.Clear();
            DataGridViewTextBoxColumn column;

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_StockID";
            column.DataPropertyName = "StockID";
            column.Visible = false;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Batchno";
            column.DataPropertyName = "BatchNumber";
            column.HeaderText = "Batchno";
            dgvBatchGrid.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_MRP";
            column.DataPropertyName = "MRP";
            column.HeaderText = "MRP";
            column.Width = 80;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PurchaseRate";
            column.DataPropertyName = "PurchaseRate";
            column.HeaderText = "Pur.Rate";
            column.Width = 80;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Expiry";
            column.DataPropertyName = "Expiry";
            column.HeaderText = "Expiry";
            column.Width = 80;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SaleRate";
            column.DataPropertyName = "SaleRate";
            column.HeaderText = "SaleRate";
            column.Width = 80;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ClosingStock";
            column.HeaderText = "ClosingStock";
            column.Width = 80;
            dgvBatchGrid.Columns.Add(column);
        }
        private void FillBatchGird()
        {
            _BarCode.ProductId = mcbProduct.SelectedID;


            DataTable dt = _BarCode.GetBatchData(_BarCode.ProductId);
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBatchGrid.DataSource = dt;
                dgvBatchGrid.Bind();
                this.dgvBatchGrid.Visible = true;
                mcbProduct.Enabled = false;
                dgvBatchGrid.SetFocus();
            }
            else
            {
                ClearData();
            }
        }
        private void FillBatchDetails(string selectedid)
        {
            try
            {
                _BarCode.ReadDetailsByStockID(selectedid);
                txtbatchno.Text = _BarCode.BatchNo.ToString();
                txtMrp.Text = _BarCode.Mrp.ToString("#0.00");
                txtexpiry.Text = _BarCode.Expiry.ToString();
                txtSaleRate.Text = _BarCode.SaleRate.ToString("#0.00");
                txtPurchaseRate.Text = _BarCode.PurchaseRate.ToString("#0.00");
                txtQuantity.Text = _BarCode.ClosingStock.ToString();
                btnBarCodeNumber.Text = _BarCode.BarCodeNumber;
                //if (_BarCode.BarCodeNumber == string.Empty)
                //    General.GenerateBarcode(_BarCode.ProductId, _BarCode.BatchNo, _BarCode.Mrp);




                //txtnewbatchno.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.FillBatchDetails>>" + Ex.Message);
            }
        }
        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvBatchGridDoubleClicked();
        }
        private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                {
                    txtloosepack.Text = mcbProduct.SeletedItem.ItemData[2].ToString();
                    txtpack.Text = mcbProduct.SeletedItem.ItemData[3].ToString();
                    txtcompany.Text = mcbProduct.SeletedItem.ItemData[4].ToString();
                    dgvBatchGrid.Visible = true;
                    FillBatchGird();
                    dgvBatchGrid.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgvBatchGrid_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvBatchGrid.SelectedRow != null && dgvBatchGrid.Rows.Count > 0)
                {
                    dgvBatchGridDoubleClicked();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.DgvBatchGrid_DoubleClicked>>" + Ex.Message);
            }
        }
        private void dgvBatchGridDoubleClicked()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvBatchGrid.SelectedRow.Cells[0].Value.ToString();
                dgvBatchGrid.Visible = false;
                pnlOld.Enabled = true;
                FillBatchDetails(selectedID);
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.DgvBatchGrid_DoubleClicked>>" + Ex.Message);
            }
        }

        #endregion
    }
}