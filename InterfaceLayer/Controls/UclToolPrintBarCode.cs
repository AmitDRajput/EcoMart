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
using EcoMart.Printing;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net;

namespace EcoMart.InterfaceLayer
{
     [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolPrintBarCode : BaseControl
    {
         # region Declaration
           BarCode _BarCode = new BarCode();
           public string _mStockID = "";
         # endregion

        public UclToolPrintBarCode()
        {
            InitializeComponent();
        }

        public override bool ClearData()
        {
            try
            {               
                ClearControls();                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        private void ClearControls()
        {
            rbtnPurchase.Checked = true;
            txtName.Text = "";
            mcbProduct.SelectedID = "";
            if (dgvBatchGrid.Rows.Count > 0)
                dgvBatchGrid.Rows.Clear();
            if (mpMSVCFill.Rows.Count > 0)
                mpMSVCFill.Rows.Clear();

        }

        public void SetData(string PrintType, string VoucherType, string VoucherNumber, DataTable data) //pass form values
        {
            if (PrintType.ToLower() == "purchase")
            {
                rbtnPurchase.Checked = true;
                cbTransactionType.Items.Clear();
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                //    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                if (VoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                else if (VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                }
                else if (VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                }

                txtVoucherNumber.Text = VoucherNumber;
                btnOKMultiSelection1Clock();
                btnPrintLabels.Enabled = true;
            }
                
        }
        public override bool Add()
        {
          
            bool retValue = base.Add();
            ClearData();
            FillTransactionType();
            FilltxtName();
            FillProductCombo();
            ConstructBatchGrid();
            mpMSVCFill.Visible = false;
            txtName.Text = "";
            dgvBatchGrid.DoubleColumnNames.Add("Col_DistributorRate");
            dgvBatchGrid.DoubleColumnNames.Add("Col_MRP");
            dgvBatchGrid.DoubleColumnNames.Add("Col_PurchaseRate");
            tsBtnAdd.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnDelete.Visible = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            tsBtnSave.Visible = false;
            pnlDebtorProduct.Visible = false;
            btnPrintLabels.Enabled = false;
            txtVoucherNumber.Focus();
            headerLabel1.Text = "Print Bar Code";
            return retValue;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Shelf _Shel = new Shelf();
            DataTable dtable = _Shel.GetOverviewData();
            txtName.FillData(dtable);
        }
        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
            //    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
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
            DataTable dt = new DataTable();
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID.ToString() != string.Empty)
               dt = _BarCode.GetStockByProductID(Convert.ToInt32(mcbProduct.SelectedID.ToString()));
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBatchGrid.DataSource = dt;
                dgvBatchGrid.Bind();
                this.dgvBatchGrid.Visible = true;
                mcbProduct.Enabled = false;
                btnPrintLabels.Enabled = true;
                dgvBatchGrid.SetFocus();
            }
            else
            {
                ClearData();
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
        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvBatchGridDoubleClicked();
        }
        private void dgvBatchGridDoubleClicked()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
               _mStockID = dgvBatchGrid.SelectedRow.Cells[0].Value.ToString();

               DataTable dt = new DataTable();
               dt = _BarCode.GetGivenProductData(_mStockID);
               if (dt != null && dt.Rows.Count > 0)
               {
                   pnlGivenProduct.Visible = false;
                   BindmpMSVCGrid(dt);
               }
               else
               {
                   btnPrintLabels.Enabled = false;
               }

                ButtonPrintLablesClick();
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclCorrectioninRate.DgvBatchGrid_DoubleClicked>>" + Ex.Message);
            }
        }
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            FillBatchGird();
            dgvBatchGrid.Select();
            dgvBatchGrid.Focus();
        }
        private void rbtnPurchase_CheckedChanged(object sender, EventArgs e)
        {
            ChackedChange();
        }
        private void rbtnGivenProduct_CheckedChanged(object sender, EventArgs e)
        {
            ChackedChange();
        }
        private void rbtnShelfWise_CheckedChanged(object sender, EventArgs e)
        {
            ChackedChange();
        }
        private void ChackedChange()
        {
            if (rbtnPurchase.Checked == true)
            {
                pnlPurchase.Visible = true;
                cbTransactionType.Focus();

                pnlGivenProduct.Visible = false;
                pnlShelf.Visible = false;

            }
            else if (rbtnGivenProduct.Checked == true)
            {
                pnlGivenProduct.Visible = true;

                pnlPurchase.Visible = false;
                pnlShelf.Visible = false;
            }
            else if (rbtnShelfWise.Checked == true)
            {
                pnlShelf.Visible = true;
                txtName.Focus();
                pnlPurchase.Visible = false;
                pnlGivenProduct.Visible = false;
            }
        }

        private void btnPrintLabels_Click(object sender, EventArgs e)
        {
            //ButtonPrintLablesClick();
            PrintBarcodeByPRNFile();
        }

        private void PrintBarcodeByPRNFile()
        {
            try
            {
                string filePath = Application.StartupPath + "\\print.prn";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("PRN File is not exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string filedata = File.ReadAllText(filePath);
                SetBarcodeDataToPrint(filePath, filedata);
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SetBarcodeDataToPrint(string filePath, string filedata)
        {
            try
            {              

              //  BarCodeData data;
                string ReplaceFileData = string.Empty;
                foreach (DataGridViewRow dr in mpMSVCFill.Rows)
                {
                    ReplaceFileData = filedata;
                    ReplaceFileData = GetRepaceFileData(dr, ReplaceFileData);

                    string filePath1 = Application.StartupPath + "\\Sample.prn";
                    using (StreamWriter sw = new StreamWriter(filePath1))
                    {
                        sw.Write(ReplaceFileData);
                    }

                    PrinterSettings settings = new PrinterSettings();
                    string printerName = "SNBC";
                    string computerFullName = Dns.GetHostName();// "SWD-LPT-5";

                    Process process1 = new Process();
                    process1.StartInfo.FileName = "cmd.exe";
                    process1.StartInfo.Arguments = "/c copy " + string.Format(@" /B {0} \\{1}\{2}",filePath1, computerFullName, printerName);
                    process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process1.Start();
                    Log.WriteInfo("Command:"+ process1.StartInfo.Arguments.ToString());
                    System.Threading.Thread.Sleep(200);
                    File.Delete(filePath1);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private string GetRepaceFileData(DataGridViewRow dr, string FileData)
        {
            try
            {
                string shop_name = string.Empty;
                string prod_scancode = string.Empty;
                string prod_mrp = string.Empty;
                string prod_selling_price = string.Empty;
                string prod_name = string.Empty;
                string prod_batchno = string.Empty;
                string prod_form = string.Empty;
                string prod_Loosepack = string.Empty;
                string prod_pack = string.Empty;
                string prod_category = string.Empty;
                string prod_expiry_date = string.Empty;
                string account_short_code = string.Empty;
                string voucher_date = string.Empty;
                string bill_rate = string.Empty;
                string shelf_code = string.Empty;
                string bill_no = string.Empty;
                string bill_date = string.Empty;
                string Voucher_no = string.Empty;
                string Voucher_type = string.Empty;
                string Prod_code = string.Empty;
                string Manufacturer_name = string.Empty;
                string Manufacturer_Short_name = string.Empty;

                int Quantity = 0;

                shop_name = General.ShopDetail.ShopName;
                prod_name = SetStringData(dr.Cells["Col_ProductName"].Value);
                prod_scancode = SetStringData(dr.Cells["Col_ScanCode"].Value);
                prod_Loosepack = SetStringData(dr.Cells["Col_UOM"].Value);
                prod_pack = SetStringData(dr.Cells["Col_Pack"].Value);
                prod_batchno = SetStringData(dr.Cells["Col_BatchNumber"].Value);
                prod_mrp = SetStringData(dr.Cells["Col_MRP"].Value);
                prod_expiry_date = SetStringData(dr.Cells["Col_Expiry"].Value);

                Quantity = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());

                FileData = GetReplaceData(FileData,shop_name, "shop_name");
                FileData = GetReplaceData(FileData, prod_name, "prod_name");
                FileData = GetReplaceData(FileData, prod_scancode, "prod_scancode");
                FileData = GetReplaceData(FileData, prod_Loosepack, "prod_Loosepack");
                FileData = GetReplaceData(FileData, prod_pack, "prod_pack");
                FileData = GetReplaceData(FileData, prod_batchno, "prod_batchno");
                FileData = GetReplaceData(FileData, prod_mrp, "prod_mrp");
                FileData = GetReplaceData(FileData, prod_expiry_date, "prod_expiry_date");

                FileData = GetReplaceData(FileData, "PRINT 1,"+Quantity.ToString(), "PRINT 1,1");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return FileData;
        }
        private string GetReplaceData(string FileData, string Replacedata, string ReplceString)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Replacedata)) == false)
                    FileData = FileData.Replace(ReplceString, Replacedata);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return FileData;
        }
        private string SetStringData(object value)
        {
            string data = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)) == false)
                    data = Convert.ToString(value);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return data;
        }

        private void ButtonPrintLablesClick()
        {
            List<BarCodeData> DataList = new List<BarCodeData>();
            BarCodeData data;
            try
            {
                foreach (DataGridViewRow dr in mpMSVCFill.Rows)
                {
                    data = new BarCodeData();
                    data.ShopName = General.ShopDetail.ShopName;
                    data.ProductName = dr.Cells["Col_ProductName"].Value.ToString();
                    data.BarCode = dr.Cells["Col_ScanCode"].Value.ToString();
                    data.ProdLoosePack = Convert.ToInt32(dr.Cells["Col_UOM"].Value.ToString());
                    data.ProdPack = dr.Cells["Col_Pack"].Value.ToString();
                    data.BatchNumber = dr.Cells["Col_BatchNumber"].Value.ToString();
                    data.MRP = "Rs." + dr.Cells["Col_MRP"].Value.ToString();
                    data.Expiry = dr.Cells["Col_Expiry"].Value.ToString();
                    data.Quantity = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                   
                    if (data.Quantity > 0)
                    {
                        data.BarCodeImage = BarCodePrinter.GetImage(data);
                        for (int index = 0; index < data.Quantity; index++)
                        {
                            DataList.Add(data);
                        }                        
                    }
                }
                if (DataList.Count > 0)
                {
                    BarCodePrinter.Print(DataList);
                    btnPrintLabels.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            //if (pnlPurchase.Visible == true)
            //    GetPurchaseData();
            //else if (pnlGivenProduct.Visible == true)
            //    GetGivenProductData();
            ////else if (pnlShelf.Visible == true)
            ////  GetShelfData();
        }      

        private void GetPurchaseData()
        {
            string mvoutype = "";
            int mvouno = 0;
           
            if (cbTransactionType.SelectedItem.ToString() == FixAccounts.TransactionTypeForCredit)
                mvoutype = FixAccounts.VoucherTypeForCreditPurchase;
            else if (cbTransactionType.SelectedItem.ToString() == FixAccounts.TransactionTypeForCash)
                mvoutype = FixAccounts.VoucherTypeForCashPurchase;
            else if (cbTransactionType.SelectedItem.ToString() == FixAccounts.TransactionTypeForCreditStatement)
                mvoutype = FixAccounts.VoucherTypeForCreditStatementPurchase;
            if (txtVoucherNumber.Text != null && txtVoucherNumber.Text.ToString() != string.Empty)
                mvouno = Convert.ToInt32(txtVoucherNumber.Text.ToString());
          
           GetPurchaseDataForGivenVoucher(mvoutype,mvouno);
        }

        private void GetPurchaseDataForGivenVoucher(string mvoutype, int mvouno)
        {
            DataTable dt = new DataTable();
            dt = _BarCode.GetPurchaseData(mvoutype, mvouno);
            if (dt != null && dt.Rows.Count > 0)
            {
                BindmpMSVCGrid(dt);
            }
            else
            {
                btnPrintLabels.Enabled = false;
            }

            ////////List<BarCodeData> DataList = new List<BarCodeData>();
            ////////BarCodeData data;
            ////////foreach (DataRow dr in dt.Rows)
            ////////{
            ////////    data = new BarCodeData();
            ////////    data.ProductName = dr["prodName"].ToString();
            ////////    data.BarCode = dr["ScanCode"].ToString();
            ////////    data.ProdLoosePack = Convert.ToInt32(dr["ProdLoosePack"].ToString());
            ////////    data.ProdPack = dr["ProdPack"].ToString();
            ////////    data.BatchNumber = dr["BatchNumber"].ToString();
            ////////    data.MRP = Convert.ToDouble(dr["MRP"].ToString());
            ////////    data.Expiry = dr["Expiry"].ToString();
            ////////    data.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                
            ////////    DataList.Add(data);
            ////////}
            ////////if (DataList.Count > 0)
            ////////{
            ////////    BarCodePrinter.Print(DataList);
            ////////}
        }

        private void BindmpMSVCGrid(DataTable dt)
        {
            pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
            //  pnlDebtorProduct.Width = 614;
            //  pnlDebtorProduct.Height = 325;
            pnlDebtorProduct.Visible = true;

            mpMSVCFill.Visible = true;
            mpMSVCFill.Dock = DockStyle.Fill;
            ConstructmpMSVCFillColumns();

            mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
            mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
            mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
            mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

            mpMSVCFillBind(dt);          
            btnPrintLabels.Enabled = true;
        }

        private void mpMSVCFillBind(DataTable dt)
        {
            DataTable data = dt;
            int rowindex = 0;
            int mqty = 0;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    mqty = 0;
                    mqty = Convert.ToInt32( Convert.ToDouble(dr["Quantity"].ToString()));
                    rowindex = mpMSVCFill.Rows.Add();
                    mpMSVCFill.Rows[rowindex].Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_Comp"].Value = dr["ProdCompShortName"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_BatchNumber"].Value = dr["BatchNumber"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_MRP"].Value = Convert.ToDouble(dr["MRP"]).ToString("#0.00");
                    mpMSVCFill.Rows[rowindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dr["SaleRate"]).ToString("#0.00");
                    mpMSVCFill.Rows[rowindex].Cells["Col_Expiry"].Value = dr["Expiry"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_Quantity"].Value = mqty.ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_StockID"].Value = dr["StockID"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_PurchaseID"].Value = dr["PurchaseID"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_ExpiryDate"].Value = dr["ExpiryDate"].ToString();
                    mpMSVCFill.Rows[rowindex].Cells["Col_ScanCode"].Value = dr["ScanCode"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtVoucherNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            btnOKMultiSelection1Clock();
        }
        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelection1Clock();
        }

        private void btnOKMultiSelection1Clock()
        {
            GetPurchaseData();
        }

        private void ConstructmpMSVCFillColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVCFill.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 180;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                mpMSVCFill.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 60;
                column.Visible = false;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);

                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSVCFill.ColumnsMain.Add(column);
                //7
              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";            
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseID";
                column.DataPropertyName = "PurchaseID";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.DataPropertyName = "ScanCode";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);

              
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillPurchaseData()
        {
           // Patient dbprod = new Patient();
            DataTable dt = new DataTable();
            int cnt = dt.Rows.Count;
            if (cnt > 0)
                txtNoOfProducts.Text = cnt.ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
              //  pnlDebtorProduct.Width = 614;
              //  pnlDebtorProduct.Height = 325;
                pnlDebtorProduct.Visible = true;

                mpMSVCFill.Visible = true;
                mpMSVCFill.Dock = DockStyle.Fill;
                ConstructmpMSVCFillColumns();

                mpMSVCFill.DataSourceMain = dt;              

                mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                mpMSVCFill.Bind();

                int cntstock = 0;
                foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (dr2.Cells["Col_ClosingStock"].Value != null)
                        int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                    if (dr2.Cells["Col_Quantity"].Value != null)
                        int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                    msalestk = Math.Min(mclstk, mreqstk);
                    if (msalestk > 0)
                        cntstock += 1;
                    if (dr2.Cells["Col_ProductID"].Value != null)
                    {
                        dr2.Cells["Col_SQuantity"].Value = msalestk;
                    }

                }
                txtStockInProducts.Text = cntstock.ToString();
                mpMSVCFill.SetFocus(0, 7);
            }

            else
                lblFooterMessage.Text = "No Fill Data Available for the Patient.";
        }

        private Point GetpnlDebtorProductLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = MMMainPanel.Location.X + 280;
                pt.Y = MMMainPanel.Location.Y + 50;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private void mpMSVCFill_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 6)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim(), out mclstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value.ToString().Trim(), out mreqstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = Math.Min(mclstk, mreqstk);
                        mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value = msalestk;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }    

        private void btnShelfwiseGo_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string _mshelf = "";
            if (txtName.SeletedItem.ItemData[0] != null)
                _mshelf = txtName.SeletedItem.ItemData[0].ToString();
            if (_mshelf != string.Empty)
            {
                dt = _BarCode.GetShelfwiseData(_mshelf);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                BindmpMSVCGrid(dt);
            }
            else
            {
                btnPrintLabels.Enabled = false;
            }
        }

    }
}
