using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;


namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCorrectioninRate : BaseControl
    {
        # region Declaration
        public string CorrectionID = string.Empty;
        private Correction _Correction;
        #endregion declaration

        #region Constructor
        public UclCorrectioninRate()
        {
            try
            {
                InitializeComponent();
                _Correction = new Correction();
                SearchControl = new UclCorrectioninRateSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.Constructor>>" + Ex.Message);
            }

        }
        #endregion

        #region IDetail Control
        public override void SetFocus()
        {
            mcbProduct.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Correction.Initialise();
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
                dgvBatchGrid.DoubleColumnNames.Add("Col_DistributorRate");
                dgvBatchGrid.DoubleColumnNames.Add("Col_MRP");
                dgvBatchGrid.DoubleColumnNames.Add("Col_PurchaseRate");
                dgvBatchGrid.DoubleColumnNames.Add("Col_SaleRate");
                dgvBatchGrid.Visible = false;
                mcbProduct.Enabled = true;
                pnlOld.Enabled = true;
                dgvBatchGrid.Enabled = true;
                AddToolTip();
                tsBtnExit.Visible = true;
                tsBtnSavenPrint.Visible = false;
                mcbProduct.Focus();

                mcbProduct.SelectedID = General.SubstituteProductID.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.Add>>" + Ex.Message);
            }

            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            return true;
        }

        public override bool Delete()
        {

            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                FillProductCombo();
                headerLabel1.Text = "CORRECTION IN RATE -> VIEW";
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                dgvBatchGrid.Enabled = false;
                txtVouchernumber.Focus();
                tsBtnEdit.Visible = false;
                tsBtnDelete.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
                //  GetLastRecord();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.View>>" + Ex.Message);
            }
            return retValue;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _Correction.VoucherType = FixAccounts.VoucherTypeForCorrectionInRate;
                }
                _Correction.GetLastRecord();
                FillSearchData(_Correction.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _Correction.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _Correction.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _Correction.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _Correction.GetFirstRecord();
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _Correction.Id = dr["ID"].ToString();
                FillSearchData(_Correction.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _Correction.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _Correction.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _Correction.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Correction.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _Correction.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _Correction.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _Correction.VoucherNumber = i;
                dr = _Correction.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _Correction.Id = dr["ID"].ToString();
                FillSearchData(_Correction.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _Correction.GetLastVoucherNumber(FixAccounts.VoucherTypeForCorrectionInRate, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Correction.VoucherType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _Correction.VoucherNumber = i;
                dr = _Correction.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _Correction.Id = dr["ID"].ToString();
                FillSearchData(_Correction.Id, "");
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
                    _Correction.Id = ID;
                    _Correction.ReadDetailsByID();
                    txtVouchernumber.Text = _Correction.VoucherNumber.ToString("#0");
                    txtVoucherSeries.Text = _Correction.VoucherSeries.ToString();
                    mcbProduct.SelectedID = _Correction.ProductID.ToString();
                    txtbatchno.Text = _Correction.BatchNo.ToString();
                    txtexpiry.Text = _Correction.Expiry.ToString();
                    txtMrp.Text = _Correction.Mrp.ToString("#0.00");
                    txtPurchaseRate.Text = _Correction.PurchaseRate.ToString("#0.00");
                    txtSaleRate.Text = _Correction.SaleRate.ToString("#0.00");
                    txtQuantity.Text = _Correction.Qty.ToString("#0");
                    txtnewbatchno.Text = _Correction.NewBatchNo.ToString();
                    txtnewexpiry.Text = _Correction.NewExpiry.ToString();
                    txtNewMrp.Text = _Correction.NewMrp.ToString("#0.00");
                    txtNewPurchaseRate.Text = _Correction.NewPurchRate.ToString("#0.00");
                    txtNewSaleRate.Text = _Correction.NewSaleRate.ToString("#0.00");
                    txtNewQuantity.Text = _Correction.NewQty.ToString("#0");
                    DateTime mydate = new DateTime(Convert.ToInt32(_Correction.VoucherDate.Substring(0, 4)), Convert.ToInt32(_Correction.VoucherDate.Substring(4, 2)), Convert.ToInt32(_Correction.VoucherDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    pnlNew.Enabled = false;
                    pnlOld.Enabled = false;
                    mcbProduct.Enabled = false;
                    datePickerBillDate.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.FillSearchDate>>" + Ex.Message);
            }
            this.Cursor = Cursors.Default;
            return true;
        }
        public override bool Save()
        {
            System.Text.StringBuilder _errorMessage;
            bool retValue = false;

            try
            {
                _Correction.BatchNo = txtbatchno.Text.Trim();
                _Correction.Expiry = txtexpiry.Text.Trim();
                if (txtMrp.Text.Trim() != "")
                    _Correction.Mrp = Convert.ToDouble(txtMrp.Text.Trim());
                if (txtPurchaseRate.Text.Trim() != "")
                    _Correction.PurchaseRate = Convert.ToDouble(txtPurchaseRate.Text.Trim());
                if (txtSaleRate.Text.Trim() != "")
                    _Correction.SaleRate = Convert.ToDouble(txtSaleRate.Text.Trim());
                if (txtQuantity.Text.Trim() != "")
                    _Correction.Qty = Convert.ToInt32(txtQuantity.Text.Trim());
                if (txtDistributorRate.Text.Trim() != "")
                    _Correction.DistributorRate = Convert.ToDouble(txtDistributorRate.Text.Trim());

                _Correction.NewBatchNo = txtnewbatchno.Text.Trim();
                //_Correction.NewExpiry = txtnewexpiry.Text.Trim();
                //_Correction.NewExpiry = General.GetValidExpiry(_Correction.NewExpiry);
                //_Correction.NewExpiryDate = General.GetValidExpiryDate(_Correction.NewExpiry);
                //_Correction.NewExpiryDate = General.GetExpiryInyyyymmddForm(_Correction.NewExpiry);
                //txtnewexpiry.Text = _Correction.NewExpiryDate;
                if (txtNewMrp.Text.Trim() != "")
                    _Correction.NewMrp = Convert.ToDouble(txtNewMrp.Text.Trim());
                if (txtNewPurchaseRate.Text.Trim() != "")
                    _Correction.NewPurchRate = Convert.ToDouble(txtNewPurchaseRate.Text.Trim());
                if (txtNewSaleRate.Text.Trim() != "")
                    _Correction.NewSaleRate = Convert.ToDouble(txtNewSaleRate.Text.Trim());
                if (txtNewQuantity.Text.Trim() != "")
                    _Correction.NewQty = Convert.ToInt32(txtNewQuantity.Text.Trim());
                if (txtNewDistributorRate.Text.Trim() != "")
                    _Correction.NewDistributorRate = Convert.ToDouble(txtNewDistributorRate.Text.Trim());
                _Correction.Validate();
                if (_Correction.IsValid)
                {
                    if (_Mode == OperationMode.Add)
                    {
                        LockTable.LockTablesForCorrectioninRate();

                        _Correction.VoucherNumber = _Correction.GetAndUpdateCorrectionNumber(General.ShopDetail.ShopVoucherSeries);
                        txtVouchernumber.Text = _Correction.VoucherNumber.ToString("#0");
                        if (_Correction.NewMrp != _Correction.Mrp || _Correction.NewBatchNo != _Correction.BatchNo)
                        {
                            DataRow dr = _Correction.SearchForNewBatchAndMrpIntblStock(_Correction.ProductID, _Correction.NewBatchNo, _Correction.NewMrp);
                            if (dr != null)
                            {
                                _Correction.NewStockId = dr["Stockid"].ToString();
                                retValue = _Correction.UpdateNewDetails(_Correction.NewStockId, _Correction.NewQty);
                                retValue = _Correction.UpdateOldDetails(_Correction.StockId, _Correction.NewQty);
                                MessageBox.Show("Product Batch & Mrp Is matches with the previous batch.Product Information Updated successfully.......", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                _Correction.NewStockId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _Correction.AddDetails(_Correction.NewStockId, _Correction.ProductID, _Correction.NewBatchNo, _Correction.NewExpiry,
                                           _Correction.NewMrp, _Correction.NewPurchRate, _Correction.NewSaleRate, _Correction.NewQty, _Correction.TradeRate, _Correction.ExpiryDate, _Correction.ProductVATPercent, _Correction.PurchaseVATPercent, _Correction.LastPurchaseDate, _Correction.LastPurchaseAccountID, _Correction.NewDistributorRate);

                                retValue = _Correction.UpdateOldDetails(_Correction.StockId, _Correction.NewQty);
                                MessageBox.Show("Product Information Added Successfuly.....", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _SavedID = _Correction.Id;
                                ClearControls();
                                retValue = true;

                            }
                        }
                        else
                        {
                            _Correction.NewStockId = _Correction.StockId;
                            retValue = _Correction.UpdateDetails();
                            MessageBox.Show("Product information has been Updated successfully.....", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _Correction.Id;
                            ClearControls();
                            retValue = true;
                        }

                        _Correction.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Correction.CreatedBy = General.CurrentUser.Id;
                        _Correction.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Correction.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _Correction.VoucherDate = _Correction.CreatedDate;
                        retValue = _Correction.AddDetailsInVoucherCorrection();

                    }

                }
                else
                {
                    LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Correction.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.Save>>" + Ex.Message);
            }
            LockTable.UnLockTables();
            return retValue;
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
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
        private void ClearControls()
        {
            try
            {
                if (General.ShopDetail.ShopDistributorSale != "Y")
                {
                    txtNewDistributorRate.Visible = false;
                    txtDistributorRate.Visible = false;
                    lblDistRate.Visible = false;
                    lblNewDistRate.Visible = false;
                }
                txtbatchno.Text = "";
                txtexpiry.Text = "";
                txtSaleRate.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtQuantity.Text = "";
                txtMrp.Text = "0.00";
                txtDistributorRate.Text = "0.00";

                txtnewbatchno.Clear();
                txtnewexpiry.Clear();
                txtNewSaleRate.Clear();
                txtNewPurchaseRate.Clear();
                txtNewQuantity.Clear();
                txtNewMrp.Clear();
                txtNewDistributorRate.Clear();

                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(_Correction.VoucherDate);
                mcbProduct.SelectedID = "";
                txtpack.Text = "";
                txtloosepack.Text = "";
                txtcompany.Text = "";
                txtVouType.Text = FixAccounts.VoucherTypeForCorrectionInRate;
                txtVouchernumber.Text = "";
                txtVoucherSeries.Text = _Correction.VoucherSeries;
                DataTable dt = new DataTable();
                dgvBatchGrid.DataSource = dt;
                dgvBatchGrid.Bind();



                pnlNew.Enabled = true;
                pnlOld.Enabled = true;
                if (_Mode == OperationMode.Add)
                    mcbProduct.Enabled = true;
                else
                    mcbProduct.Enabled = false;
                datePickerBillDate.Enabled = false;
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.ClearControl>>" + Ex.Message);
            }

        }
        #endregion

        #region Private Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
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
            column.Name = "Col_DistributorRate";
            column.DataPropertyName = "DistributorSaleRate";
            column.HeaderText = "Dist.Rate";
            if (General.ShopDetail.ShopDistributorSale == "Y")
                column.Visible = true;
            else
                column.Visible = false;
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
            _Correction.ProductID = Convert.ToInt32(mcbProduct.SelectedID);
            DataTable dt = _Correction.GetStockByProductID(_Correction.ProductID);
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBatchGrid.DataSource = dt;
                dgvBatchGrid.Bind();
                this.dgvBatchGrid.Visible = true;
                //  mcbProduct.Enabled = false;
                pnlNew.Enabled = false;
                //this.ActiveControl = dgvBatchGrid;
                //dgvBatchGrid.Select();
                //dgvBatchGrid.SetFocus();
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
                _Correction.ReadDetailsByStockID(selectedid);
                txtbatchno.Text = _Correction.BatchNo.ToString();
                txtMrp.Text = _Correction.Mrp.ToString("#0.00");
                txtexpiry.Text = _Correction.Expiry.ToString();
                txtSaleRate.Text = _Correction.SaleRate.ToString("#0.00");
                txtPurchaseRate.Text = _Correction.PurchaseRate.ToString("#0.00");
                txtQuantity.Text = _Correction.ClosingStock.ToString();
                txtDistributorRate.Text = _Correction.DistributorRate.ToString("#0.00");

                txtnewbatchno.Text = _Correction.BatchNo.ToString();
                txtNewMrp.Text = _Correction.Mrp.ToString("#0.00");
                txtnewexpiry.Text = _Correction.Expiry.ToString();
                txtNewSaleRate.Text = _Correction.SaleRate.ToString("#0.00");
                txtNewPurchaseRate.Text = _Correction.PurchaseRate.ToString("#0.00");
                txtNewQuantity.Text = _Correction.ClosingStock.ToString();
                txtNewDistributorRate.Text = _Correction.DistributorRate.ToString("#0.00");

                if (_Correction.ClosingStock < 0)
                    txtNewQuantity.Enabled = false;

                txtnewbatchno.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.FillBatchDetails>>" + Ex.Message);
            }
        }
        #endregion

        #region Events
        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvBatchGridDoubleClicked();
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
                pnlNew.Enabled = true;
                FillBatchDetails(selectedID);
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.DgvBatchGrid_DoubleClicked>>" + Ex.Message);
            }
        }
        private void txtnewbatchno_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        txtnewexpiry.Focus();
                        break;
                    }
            }
        }
        private void txtnewexpiry_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        _Correction.NewExpiry = txtnewexpiry.Text.Trim();
                        _Correction.NewExpiry = General.GetValidExpiry(_Correction.NewExpiry);
                        _Correction.NewExpiryDate = General.GetValidExpiryDate(_Correction.NewExpiry);
                        _Correction.NewExpiryDate = General.GetExpiryInyyyymmddForm(_Correction.NewExpiryDate);
                        txtnewexpiry.Text = _Correction.NewExpiry;
                        txtNewMrp.Focus();
                        break;
                    }
                case Keys.Up:
                    {
                        txtnewbatchno.Focus();
                        break;
                    }
            }
        }
        private void txtNewMrp_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        txtNewPurchaseRate.Focus();
                        break;
                    }
                case Keys.Up:
                    {
                        txtnewexpiry.Focus();
                        break;
                    }
            }
        }
        private void txtNewPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        txtNewSaleRate.Focus();
                        break;
                    }
                case Keys.Up:
                    {
                        txtNewMrp.Focus();
                        break;
                    }
            }
        }
        private void txtNewSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (txtNewQuantity.Enabled == true)
                            txtNewQuantity.Focus();
                        else
                            txtNewDistributorRate.Focus();
                        break;
                    }
                case Keys.Up:
                    {
                        txtNewPurchaseRate.Focus();
                        break;
                    }
            }
        }
        private void txtNewQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (General.ShopDetail.ShopDistributorSale == "Y")
                            txtNewDistributorRate.Focus();
                        else
                        {
                            MainToolStrip.Select();
                            tsBtnSave.Select();
                        }
                        break;
                    }
                case Keys.Up:
                    {
                        txtNewSaleRate.Focus();
                        break;
                    }
            }
        }
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

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
                    //this.ActiveControl = dgvBatchGrid;
                    //dgvBatchGrid.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(mcbProduct.SelectedID)) == false)
            {
                this.ActiveControl = dgvBatchGrid;
                dgvBatchGrid.Focus();
            }
            else
            {
                this.ActiveControl = mcbProduct;
                mcbProduct.Focus();
            }
        }
        private void txtNewDistributorRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        #endregion Events      

        #region tooltip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(txtnewbatchno, "Change Batch if required");
            ttToolTip.SetToolTip(txtnewexpiry, "Change Expiry if required");
            ttToolTip.SetToolTip(txtNewMrp, "Change MRP if required");
            ttToolTip.SetToolTip(txtNewPurchaseRate, "Change Purchase Rate if required");
            ttToolTip.SetToolTip(txtNewSaleRate, "Change Sale Rate if required");
            ttToolTip.SetToolTip(txtNewQuantity, "Enter Quantity for New Details");
        }

        #endregion tooltip
    }
}
