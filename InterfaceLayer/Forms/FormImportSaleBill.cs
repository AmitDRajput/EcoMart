using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using System.IO;
using EcoMart.BusinessLayer;

namespace EcoMart.InterfaceLayer
{
    public partial class FormImportSaleBill : Form
    {
        private const char TABSEPERATOR = '\t';
        private const char COMMASEPERATOR = ',';
        // private const char CARESEPERATOR = 
        public ImportBill ImportBillData;
        public MainForm mainformobj;
        //UclProduct _UclProduct;
        //UserControl _ActiveControl;
        //UclProduct _UclProduct;
        //Product _formproduct;
        string tempstr = "";
        Form frmOpen;
        private UserControl UserControlToShow = null;
   //     public event EventHandler OnNewProduct;
        private string _SelectedID;
        public event EventHandler ItemAddedEdited;
        //   public event EventHandler OnNewParty;
        public event EventHandler OnNewPurchase;
        public event EventHandler OnCancel;
        public string tempdelpath = "";
        public bool ifFirst = true;

        public FormImportSaleBill()
        {
            try
            {
                InitializeComponent();
                ImportBillData = new ImportBill();
                cmbFormat.Text = "Allied HTF";
                fillcombofiles();
                FillCreditorCombo();
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.HandleShortcutAction:" + ex.Message);
            }

        }
        public void fillcombofiles()
        {
            try
            {
                psCombofile.SelectedID = null;
                psCombofile.SourceDataString = new string[2] { "FileName", "FilePath" };
                psCombofile.ColumnWidth = new string[2] { "500", "0" };
                psCombofile.DisplayColumnNo = 0;
                psCombofile.ValueColumnNo = 1;
                string dirpath = General.OnlinePurchasePath;
                DirectoryInfo dir = new DirectoryInfo(dirpath);
                DataTable dtfiles = new DataTable();
                dtfiles.Columns.Add("FileName");
                dtfiles.Columns.Add("FilePath");
                foreach (FileInfo files in dir.GetFiles())
                {
                    DataRow dr = dtfiles.NewRow();
                    dr["FileName"] = files.Name;
                    dr["FilePath"] = files.FullName;
                    dtfiles.Rows.Add(dr);
                    psCombofile.FillData(dtfiles);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.HandleShortcutAction:" + ex.Message);
            }
                   
        }
        private bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            //ControlItem _ControlItem = null;
            try
            {
               if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    psCombofile.Focus();
                    retValue = true;
                }
                //if (keyPressed == Keys.F10)
                //{
                //    _ControlItem = new ControlItem();
                //    _ControlItem.ItemName = "Product";
                //    _ControlItem.ItemMode = OperationMode.Add;
                //    _ControlItem.Control = _UclProduct;


                //    OpenItem(_ControlItem);
                //    retValue = true;

                //    FillProductCombo();
                //    //mcbProduct.SelectedID = _SelectedID;
                //    dgProduct.Focus();
                //}
                
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.HandleShortcutAction:" + ex.Message);
            }
            return retValue;
        }
        public void RefreshData()
        {
            try
            {
                FillCreditorCombo();
                FillProductCombo();
                mcbCreditor.Focus();
                psCombofile.Enabled = true;
                pnlProductMatch.Visible = true;

                if (dgProduct.Columns.Count > 0 && pnlProductMatch.Visible)
                {
                    FillProductData();
                    ShowProductData();
                }
                mcbCreditor.Focus();
                //if (txtFolderName.Text != null && txtFolderName.Text.ToString() != string.Empty)
                //    //SetNextFileSelected();  //Amar
                //    MessageBox.Show("hi");
                //else
                //{
                    psCombofile.SelectedID  = "";
                    mcbCreditor.SelectedID = "";
                    if (dgImpotProducts.Rows.Count > 0)
                        dgImpotProducts.Rows.Clear();
                //}
                // this.Show();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            //btnSelectFileClick();
            mcbCreditor.Focus();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e) //Amar com
        {
            //btnSelectFolderClick();
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {

                bool retValue = false;
                string wrongFormat = "N";
                tempstr = "";
                if (psCombofile.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(psCombofile.SelectedID)) == false)
                    tempstr = Convert.ToString(psCombofile.SeletedItem.ItemData[1]);
                tempdelpath = tempstr;
                mcbProduct.SelectedID = string.Empty;
                if (tempstr != null && tempstr != string.Empty)
                {
                    string Format = cmbFormat.Text;
                    switch (Format)
                    {
                        case "Allied HTF":
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            try
                            {
                                ConstructAndFill();
                                string line;
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];
                                            if (Header == "H")
                                            {
                                                ImportBillData.BillDate = items[1];
                                                ImportBillData.BillNumber = items[2];
                                                ImportBillData.TransactionType = items[3];
                                                ImportBillData.DistributorCode = items[6];
                                                ImportBillData.DistributorName = items[7];
                                                ImportBillData.AIOCDACode = items[12];
                                                ImportBillData.CashDiscountPercent = items[20];
                                                ImportBillData.CashDiscountAmount = items[21];
                                                ImportBillData.TotalAmount = items[15];
                                                ImportBillData.PurchaseBillFormat = cmbFormat.Text;
                                                if (ImportBillData.TransactionType == "1")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                                else if (ImportBillData.TransactionType == "3")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                                else
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                                ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                                ImportBillData.CompanyID = mcbCreditor.SelectedID;  //ImportBillData.CheckParty();
                                                ImportBillData.AIOCDACode = ImportBillData.CompanyID;
                                                retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.AIOCDACode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                                if (retValue)
                                                {
                                                    ImportBillData.BillNumberAlreadyEntered = "Y";
                                                    psCombofile.SelectedID = "";
                                                    //cbofillFile.SelectedIndex = -1;
                                                    break;
                                                }
                                            }
                                            else if (Header == "T")
                                            {
                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[22];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[1];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[2];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[3];
                                                //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[4];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[5];

                                                string mexp = items[5];
                                                string mexpl = "";
                                                string mexpr = "";
                                                if (mexp.Length == 10)
                                                {
                                                    mexpl = mexp.Substring(3, 2);
                                                    mexpr = mexp.Substring(8, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                else if (mexp.Length == 8)
                                                {
                                                    mexpl = mexp.Substring(2, 2);
                                                    mexpr = mexp.Substring(6, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = mexp;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[6];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[12];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[8];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[8];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[9];
                                            }
                                            else if (Header == "F")
                                            {
                                                ImportBillData.TotalAmount = items[1];
                                                ImportBillData.Vat5PerAmount = items[7];
                                                ImportBillData.CashDiscountPercent = items[20];
                                                ImportBillData.CashDiscountAmount = items[21];
                                                ImportBillData.RoundOFF = items[22];
                                                ImportBillData.NetAmount = items[23];
                                            }
                                            else
                                            {
                                                MessageBox.Show("Wrong Format");
                                                wrongFormat = "Y";
                                                psCombofile.Focus();
                                                break;
                                            }
                                        }
                                        reader.Close();
                                    } //While

                                    if (ImportBillData.BillNumberAlreadyEntered == "Y")
                                    {
                                        MessageBox.Show("Purchase Already Entered For This Bill Number");
                                        psCombofile.Focus();
                                    }

                                    AfterReadingData(wrongFormat);
                                   
                                }

                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }
                            break;
                        case "DAVA":
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            try
                            {
                                ConstructAndFill();

                                string line;
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];
                                            if (Header == "H")
                                            {
                                                ImportBillData.BillNumber = items[2];
                                                ImportBillData.BillDate = items[3];
                                                ImportBillData.TransactionType = items[7];
                                                ImportBillData.DistributorCode = items[18];
                                                ImportBillData.DistributorName = items[19];
                                                ImportBillData.AIOCDACode = items[17];
                                                ImportBillData.CashDiscountPercent = items[14];
                                                ImportBillData.CashDiscountAmount = items[15];
                                                ImportBillData.TotalAmount = items[16];
                                                ImportBillData.BillNetAmount = items[16];
                                                ImportBillData.PurchaseBillFormat = cmbFormat.Text;


                                                if (ImportBillData.TransactionType == "1")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                                else if (ImportBillData.TransactionType == "3")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                                else
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                                ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                                ImportBillData.CompanyID = mcbCreditor.SelectedID;  //ImportBillData.CheckParty();
                                                ImportBillData.AIOCDACode = ImportBillData.CompanyID;
                                                retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.AIOCDACode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                                if (retValue)
                                                {
                                                    ImportBillData.BillNumberAlreadyEntered = "Y";
                                                    psCombofile.SelectedID = "";

                                                    break;
                                                }
                                            }
                                            else if (Header == "T")
                                            {
                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                //dgImpotProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[4];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[5];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[6];
                                                //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[8];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[9];

                                                string mexp = items[9];
                                                string mexpl = "";
                                                string mexpr = "";
                                                if (mexp.Length == 10)
                                                {
                                                    mexpl = mexp.Substring(3, 2);
                                                    mexpr = mexp.Substring(8, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                else if (mexp.Length == 8)
                                                {
                                                    mexpl = mexp.Substring(2, 2);
                                                    mexpr = mexp.Substring(6, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = mexp;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[20];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[21];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[12];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[13];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[14];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[16];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ItemSCMDiscountAmount"].Value = items[25];

                                            }
                                            else if (Header == "F")
                                            {
                                                ImportBillData.TotalAmount = items[1];
                                                ImportBillData.Vat5PerAmount = items[7];
                                                ImportBillData.CashDiscountPercent = items[20];
                                                ImportBillData.CashDiscountAmount = items[21];
                                                ImportBillData.RoundOFF = items[22];
                                                ImportBillData.NetAmount = items[23];
                                            }
                                            else
                                            {
                                                MessageBox.Show("Wrong Format");
                                                wrongFormat = "Y";
                                                psCombofile.Focus();
                                                break;
                                            }
                                        }
                                    } //While
                                    reader.Close();
                                }
                                AfterReadingData(wrongFormat);

                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }
                            break;
                        case "Medica":
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            try
                            {
                                ConstructAndFill();
                                string line;
                                //tempstr = psCombofile.SeletedItem.ItemData[1].ToString();
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];
                                            if (Header != "CNick")
                                            {
                                                ImportBillData.BillNumber = items[7];
                                                ImportBillData.BillDate = items[8];
                                                ImportBillData.TransactionType = items[52];
                                                ImportBillData.DistributorCode = items[9];
                                                ImportBillData.DistributorName = items[1];
                                                ImportBillData.AIOCDACode = "";
                                                ImportBillData.CashDiscountPercent = items[42];
                                                ImportBillData.CashDiscountAmount = "0.00";
                                                ImportBillData.TotalAmount = items[20];
                                                ImportBillData.BillNetAmount = items[20];
                                                ImportBillData.PurchaseBillFormat = cmbFormat.Text; //rbAMD.Text.ToString();
                                                if (ImportBillData.TransactionType == "1")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                                else if (ImportBillData.TransactionType == "3")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                                else
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                                ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                                ImportBillData.CompanyID = mcbCreditor.SelectedID;  //ImportBillData.CheckParty();
                                                ImportBillData.AIOCDACode = ImportBillData.CompanyID;
                                                retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.AIOCDACode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                                if (retValue)
                                                {
                                                    ImportBillData.BillNumberAlreadyEntered = "Y";
                                                    psCombofile.SelectedID = "";
                                                    break;
                                                }
                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[22];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[24];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[25];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[26];
                                                //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[29];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[30];

                                                string mexp = items[30];
                                                string mexpl = "";
                                                string mexpr = "";
                                                if (mexp.Length == 10)
                                                {
                                                    mexpl = mexp.Substring(3, 2);
                                                    mexpr = mexp.Substring(8, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                else if (mexp.Length == 8)
                                                {
                                                    mexpl = mexp.Substring(2, 2);
                                                    mexpr = mexp.Substring(6, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = mexp;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[31];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[32];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[45];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[34];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[36];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[37];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ItemSCMDiscountAmount"].Value = "0.00";
                                            }
                                            //else
                                            //{
                                            //    MessageBox.Show("Wrong Format");
                                            //    wrongFormat = "Y";
                                            //    break;
                                            //}
                                        }
                                    } //While
                                    reader.Close();
                                }

                                if (ImportBillData.BillNumberAlreadyEntered == "Y")
                                {
                                    MessageBox.Show("Purchase Already Entered For This Bill Number");
                                    psCombofile.Focus();
                                }
                                   
                                else if (wrongFormat != "Y")
                                {
                                    lblTotalProductsToMatch.Visible = true;
                                    lblTotalProducts.Visible = true;
                                    lblTotalProducts.Text = dgImpotProducts.Rows.Count.ToString();

                                    lblRemainingProducts.Text = dgImpotProducts.Rows.Count.ToString();
                                    if (dgImpotProducts.Rows.Count > 0)
                                    {
                                        dgImpotProducts.Rows[0].Selected = true;
                                        dgImpotProducts.CurrentCell = dgImpotProducts.Rows[0].Cells[6];
                                    }
                                    ShowProductData();
                                 
                                }                                
                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }


                            break;

                        case "PharmaSYS":
                            break;
                        case "EcoMart":
                           
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            try
                            {
                                ConstructAndFill();

                                string line;
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];

                                            if (Header == "E")
                                            {

                                                //    0,1,               2,             3, 4,5,6,7,8     ,9    ,10    ,11        ,12    ,13  ,14  ,15  ,16  ,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31   ,32    
                                                //    T,,KETOMAR  EYEDROPS,ALLER  CHARLIE,5ML,,5,0,126.06,89.18,PT0045,31/01/2017,445.90,0.00,0.00,0.00,5.00,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,97.06,0.00
                                                //    T,,PRED - FORTE  EYEDROPS,ALLER  DELTA,5ML,,30,0,36.60,27.67,PT0013,31/03/2017,830.10,0.00,0.00,0.00,5.00,,,,,,,,,,,,,,,29.51,0.00
                                                //    T,,REFRESH (10ML) LIQUIGEL,ALLER  DELTA,10ML,,15,0,212.41,150.20,PT0403,31/03/2017,2253.00,0.00,0.00,0.00,5.00,,,,,,,,,,,,,,,163.50,0.00
                                                //    T,,TEARS PLUS EYE DROPS,ALLERG ALPHA,10 ML,,60,0,61.87,43.75,PT0352,31/03/2017,2625.00,0.00,0.00,0.00,5.00,,,,,,,,,,,,,,,47.62,0.00
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //dgImpotProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                               

                                               
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[4];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[5];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[6];
                                                //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[8];
                                                string mexp = items[8];
                                                string mexpl = "";
                                                string mexpr = "";
                                                if (mexp.Length == 10)
                                                {
                                                    mexpl = mexp.Substring(3, 2);
                                                    mexpr = mexp.Substring(8, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                else if (mexp.Length == 8)
                                                {
                                                    mexpl = mexp.Substring(4, 2);
                                                    mexpr = mexp.Substring(2, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = mexp;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[9];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[10];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[11];                                                
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[12];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[13];
                                                //dgImpotProducts.Rows[rowIndex].Cells["Col_RetailRate"].Value = items[14]; 
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[15];

                                            }
                                        }
                                    } //While
                                    reader.Close();
                                }
                                if (General.EcoMartLicense.ApplicationType == EcoMartLicenseLib.ApplicationTypes.EcoMart)
                                    AfterReadingData(wrongFormat);
                                else
                                {
                                    this.Hide();
                                    
                                }

                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }
                            break;
                        case "Care":
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            string str = "";
                            try
                            {
                                ConstructAndFill();

                                string line;
                                //  string stringdata;
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];
                                            if (Header != "partycode")
                                            {
                                                //     0   1     2       3      4                5   6   7              8  9,   10        11    12,  13,14,15,16,   17,   18, 19, 20,21,22,23,24, 25   26   27, 28,29, 30     31  32                           ,33,34,35,36,37,38,  39                ,40,41,42, 43    44  ,45 ,46,47,48,49                                                                                                                                                                                   
                                                //        CRB,846,20140408,D 648,SWAMI MEDICAL STORES,1,3513,ASOMEX-AT TAB,1*10, ,I6A13017,20161101,Nov-16,3,  ,0 ,0 ,61.52,61.52,79.9,0 ,0,  0, 5, 0,9.08,  0,184.56,0, 0,865.42,894,AVALABALE OLMAX CH 20 & 40 GLE,  , 0,  ,  , 0,11,EMCURE PHARMACEUTICA, 0, 0, 0,13.85,13.85,1.6,0 , 0, 0,MHPU200047
                                                //        CRB,846,20140408,D 648,SWAMI MEDICAL STORES,2,8716,ASOMEX OH TAB,1*10,,PDA13003,20151101,Nov-15,2,,0,0,89.9,89.9,118,0,0,0,5,0,8.85,0,179.8,0,0,865.42,894,AVALABALE OLMAX CH 20 & 40 GLE,,0,,,0,11,EMCURE PHARMACEUTICA,0,0,0,13.85,13.85,1.6,0,0,0,MHPU200047
                                                //    0,                         1,     2,                   3,        4,   5,      6,    7 ,        8 ,      9,      10 ,       11,     12,   13,  14,    15,        16,17,18,19,    20,   21,      22,                   23,    24,                 25,    26,    27,    28,     29,        30, 31,  32,            33,    34,     35,    36, 37,   38,       39,     40,    41,   42,   43,    44,    45,     46,    47,  48,  49,       50,     51,      52,      53, 54,55
                                                //CNick,Vendor                    ,CUCode,Customer            ,Area     ,City,PinCode,InvNo ,InvDate   ,OrderNo,OrderDate,Transport,Freight,Paid ,LRNo,LRDate,CreditDays,Ad,Ls,Tx,InvAmt,CNote,MfgrNick,Manufacturer         ,PrCode,ProductDesc        ,PPack ,MyType,MyMode,BatchNo,ExpDate   ,Qty,Free,SchQtyAdjInAmt,Rate  ,GrsAmt ,PTR   ,MRP,WPPer,OctroiPer,SchRate,SchPer,CDPer,TDPer,CSTPer,VATPer,INetAmt,Remark,LOCA,LOCN,KeepWatch,DivNick,MyTypeId,MyItemNo,PTS,Barcode
                                                //     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RN14    ,RANBAXY -CROSLAND 1  ,1944  ,REFZIL-O 250 MG TAB,6 TAB ,SALE  ,      ,2604838,31/03/2016,3  ,0   ,0             ,342.86,1028.58,342.86,450,    0,        0,      0,     0,    3,    0,     0,     5,1047.61,      ,F-17,   0,    FALSE,    RN9,       1,       1,0  ,7328116020
                                                //     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RE1     ,RESILIENT COSMECUTICA,2634  ,CLINSUP V3 ER TAB  ,3 TAB ,SALE  ,      ,D-00114,30/11/2015,2  ,0   ,0             ,51.05 ,102.1  ,51.05 , 67,    0,        0,      0,     0,    3,    0,     0,     5,103.99 ,      ,G-18,   0,    FALSE,    RE1,       1,       2,0  ,7912110200

                                                ImportBillData.BillNumber = items[3];
                                                ImportBillData.BillNumber = ImportBillData.BillNumber.Replace('"', ' ').Trim();
                                                ImportBillData.BillDate = items[5];
                                                str = items[4];
                                                str = str.Replace('"', ' ').Trim();
                                                ImportBillData.TransactionType = str;
                                                str = items[0];
                                                str = str.Replace('"', ' ').Trim();
                                                ImportBillData.DistributorCode = str;
                                                str = items[1];
                                                str = str.Replace('"', ' ').Trim();
                                                //  str = str.Replace('\"', ' ').Trim();

                                                ImportBillData.DistributorName = str;
                                                ImportBillData.AIOCDACode = "";
                                                ImportBillData.CashDiscountPercent = items[27];
                                                ImportBillData.CashDiscountAmount = items[28];
                                                ImportBillData.TotalAmount = items[33];
                                                ImportBillData.BillNetAmount = items[32];
                                                ImportBillData.PurchaseBillFormat = cmbFormat.Text;//rbCare.Text.ToString();

                                                if (ImportBillData.TransactionType == "CSB")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                                else if (ImportBillData.TransactionType == "CRB")
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                                else
                                                    ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                                ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                                ImportBillData.CompanyID = mcbCreditor.SelectedID;  //ImportBillData.CheckParty();
                                                ImportBillData.AIOCDACode = ImportBillData.CompanyID;
                                                retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.AIOCDACode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                                if (retValue)
                                                {
                                                    ImportBillData.BillNumberAlreadyEntered = "Y";
                                                    psCombofile.SelectedID = "";
                                                    break;
                                                }
                                                //   0,                         1,     2,                   3,        4,   5,      6,    7 ,        8 ,      9,      10 ,       11,     12,   13,  14,    15,        16,17,18,19,    20,   21,      22,                   23,    24,                 25,    26,    27,    28,     29,        30, 31,  32,            33,    34,     35,    36, 37,   38,       39,     40,    41,   42,   43,    44,    45,     46,    47,  48,  49,       50,     51,      52,      53, 54,55
                                                //CNick,Vendor                    ,CUCode,Customer            ,Area     ,City,PinCode,InvNo ,InvDate   ,OrderNo,OrderDate,Transport,Freight,Paid ,LRNo,LRDate,CreditDays,Ad,Ls,Tx,InvAmt,CNote,MfgrNick,Manufacturer         ,PrCode,ProductDesc        ,PPack ,MyType,MyMode,BatchNo,ExpDate   ,Qty,Free,SchQtyAdjInAmt,Rate  ,GrsAmt ,PTR   ,MRP,WPPer,OctroiPer,SchRate,SchPer,CDPer,TDPer,CSTPer,VATPer,INetAmt,Remark,LOCA,LOCN,KeepWatch,DivNick,MyTypeId,MyItemNo,PTS,Barcode
                                                //     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RN14    ,RANBAXY -CROSLAND 1  ,1944  ,REFZIL-O 250 MG TAB,6 TAB ,SALE  ,      ,2604838,31/03/2016,3  ,0   ,0             ,342.86,1028.58,342.86,450,    0,        0,      0,     0,    3,    0,     0,     5,1047.61,      ,F-17,   0,    FALSE,    RN9,       1,       1,0  ,7328116020
                                                //     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RE1     ,RESILIENT COSMECUTICA,2634  ,CLINSUP V3 ER TAB  ,3 TAB ,SALE  ,      ,D-00114,30/11/2015,2  ,0   ,0             ,51.05 ,102.1  ,51.05 , 67,    0,        0,      0,     0,    3,    0,     0,     5,103.99 ,      ,G-18,   0,    FALSE,    RE1,       1,       2,0  ,7912110200

                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                                str = items[7];
                                                str = str.Replace('"', ' ').Trim();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = str;
                                                str = items[8];
                                                str = str.Replace('"', ' ').Trim();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = str;

                                                str = items[10];
                                                str = str.Replace('"', ' ').Trim();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = str;

                                                str = items[16];
                                                str = str.Replace('"', ' ').Trim();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = str;

                                                str = items[17];
                                                str = str.Replace('"', ' ').Trim();
                                                str = General.GetValidExpiry(str);
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = str;
                                                str = General.GetValidExpiryDate(str);
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = str;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[13];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[14];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[22];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[18];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[18];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[19];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ItemSCMDiscountAmount"].Value = "0.00";
                                            }
                                            //else
                                            //{
                                            //    MessageBox.Show("Wrong Format");
                                            //    wrongFormat = "Y";
                                            //    break;
                                            //}
                                        }
                                    } //While
                                    reader.Close();
                                }
                                if (ImportBillData.BillNumberAlreadyEntered == "Y")
                                {
                                    MessageBox.Show("Purchase Already Entered For This Bill Number");
                                    psCombofile.Focus();
                                }

                                AfterReadingData(wrongFormat);

                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }

                            break;
                        case "MicroPro":
                            ImportBillData.BillNumberAlreadyEntered = "N";
                            try
                            {
                                ConstructAndFill();

                                string line;
                                if (tempstr != string.Empty && File.Exists(tempstr))
                                {
                                    StreamReader reader = new StreamReader(tempstr);
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                        if (items.Length >= 10)
                                        {

                                            string Header = items[0];
                                            if (Header == "H")
                                            {
                                                //H,1.1,110190,21032014,XXXX,21032014,,1,ChequeNo,21032014,.                        ,XXXX,21032014,Destination, 0,0,     3347.00,MHPU200022,D3089,C.T.DISTRIBUTORS
                                                ImportBillData.BillDate = items[3];
                                                ImportBillData.BillNumber = items[2];
                                                // ImportBillData.BillDate = items[3];
                                                ImportBillData.TransactionType = items[7];
                                                ImportBillData.DistributorCode = items[6];
                                                ImportBillData.DistributorName = items[7];
                                                ImportBillData.AIOCDACode = items[12];
                                                //    ImportBillData.CashDiscountPercent = items[20];
                                                //   ImportBillData.CashDiscountAmount = items[21];
                                                //    ImportBillData.TotalAmount = items[15];
                                                ImportBillData.PurchaseBillFormat = cmbFormat.Text;
                                                ImportBillData.TransactionType = "3";
                                                ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                                ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                                ImportBillData.CompanyID = mcbCreditor.SelectedID;  //ImportBillData.CheckParty();
                                                ImportBillData.AIOCDACode = ImportBillData.CompanyID;
                                                retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.AIOCDACode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                                if (retValue)
                                                {
                                                    ImportBillData.BillNumberAlreadyEntered = "Y";
                                                    psCombofile.SelectedID = "";
                                                    break;
                                                }
                                            }
                                            else if (Header == "T")
                                            {
                                                int rowIndex = dgImpotProducts.Rows.Add();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                                //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[3];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[5];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[6];
                                                //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[8];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[9];
                                                string mexp = items[9];
                                                string mexpl = "";
                                                string mexpr = "";
                                                if (mexp.Length == 8)
                                                {
                                                    mexpl = mexp.Substring(2, 2);
                                                    mexpr = mexp.Substring(6, 2);
                                                    mexp = mexpl + "/" + mexpr;
                                                }
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Expiry"].Value = mexp;
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = Convert.ToInt32(Convert.ToDouble(items[20].ToString())).ToString();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = Convert.ToInt32(Convert.ToDouble(items[21].ToString())).ToString();
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[12];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[13];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[14];
                                                dgImpotProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[16];
                                            }
                                            else if (Header == "F")
                                            {
                                                //       1             2          3   4,5,6,     7    , 8,     9,        10,  11,12,13,14,15,16,17,18,19,    20 ,     21,         22,        23     
                                                //F,     3296.32,      0.00,      0.00,0,0,0,    159.38,0,      0.00,      0.00,0,0,0,0,0,0,0,0,      0.00,  3.30,    108.78,      0.08,     3347.00
                                                ImportBillData.TotalAmount = items[1];
                                                ImportBillData.Vat5PerAmount = items[7];
                                                ImportBillData.CashDiscountPercent = items[20];
                                                ImportBillData.CashDiscountAmount = items[2];
                                                //      if (items[14] != string.Empty)
                                                ImportBillData.DebitAmount = Convert.ToDouble(items[14]);
                                                ImportBillData.RoundOFF = items[22];
                                                ImportBillData.NetAmount = items[23];
                                            }
                                            else
                                            {
                                                MessageBox.Show("Wrong Format");
                                                wrongFormat = "Y";
                                                psCombofile.Focus();
                                                break;
                                            }
                                        }
                                    } //While                        
                                    reader.Close();
                                    if (ImportBillData.BillNumberAlreadyEntered == "Y")
                                    {
                                        MessageBox.Show("Purchase Already Entered For This Bill Number");
                                        psCombofile.Focus();
                                    }
                                    AfterReadingData(wrongFormat);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(ex);
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Select File");
                    psCombofile.Focus();
                }
                //btnProductMatch.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void AfterReadingData(string wrongFormat)
        {
            if (ImportBillData.BillNumberAlreadyEntered == "Y")
                MessageBox.Show("Purchase Already Entered For This Bill Number " + ImportBillData.VoucherType + ":" + ImportBillData.VoucherNumber);
            else if (wrongFormat != "Y")
            {
                if (General.EcoMartLicense.ApplicationType == EcoMartLicenseLib.ApplicationTypes.EcoMart)
                {
                    int remainingproducts = 0;
                    string _CompanyProductID = string.Empty;
                    string _retailerProductID = string.Empty;

                    ImportBill ib = new ImportBill();

                    foreach (DataGridViewRow dr in dgImpotProducts.Rows)
                    {
                        _CompanyProductID = string.Empty;
                        _retailerProductID = string.Empty;
                        if (dr.Cells["Col_DistributorsProductID"].Value != null)
                            _CompanyProductID = dr.Cells["Col_DistributorsProductID"].Value.ToString();
                        _retailerProductID = ib.GetRetailerProductIDFromDistributorProductID(ImportBillData.CompanyID, _CompanyProductID);
                        if (_retailerProductID != string.Empty)
                        {
                            dr.Cells["Col_Check"].Value = true;
                            dr.Cells["Col_ProductID"].Value = _retailerProductID;
                        }
                        else
                            remainingproducts += 1;
                    }

                    lblTotalProductsToMatch.Visible = true;
                    lblTotalProducts.Visible = true;
                    lblTotalProducts.Text = dgImpotProducts.Rows.Count.ToString();
                    lblRemainingProducts.Visible = true;
                    lblRemainingProducts.Text = remainingproducts.ToString();
                    //lblPharmaSysParty.Visible = true;
                    //lblPharmaSYSPartyName.Visible = true;
                    //pnlPartyMatch.Visible = true;
                    //pnlPartyMatch.Location = new Point(501, 102);
                    //pnlPartyMatch.Focus();
                    //dgParty.Focus();
                    //SelectPartyInTheGrid();
                    if (remainingproducts == 0)
                    {
                        MessageBox.Show("Click Finish Key");
                        mcbProduct.Visible = false;
                        btnProductMatch.Enabled = false;
                        btnFinish.Visible = true;
                        btnFinish.Enabled = true;
                        dgProduct.Enabled = false;
                    }
                    else
                    {
                        mcbProduct.Enabled = true;
                        btnProductMatch.Enabled = true;
                        btnFinish.Enabled = false;
                        dgProduct.Enabled = true;
                        int newIndex = 0;
                        if (dgImpotProducts.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dr in dgImpotProducts.Rows)
                            {
                                if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == false)
                                {
                                    newIndex = dr.Index;
                                    break;
                                }
                            }
                            dgImpotProducts.Rows[newIndex].Selected = true;
                            dgImpotProducts.CurrentCell = dgImpotProducts.Rows[newIndex].Cells["Col_ProductName"];
                        }
                        ShowProductData();
                    }
                }

            }
        }

        private void FillCreditorCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[7] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccDiscountOffered", "PurchaseBillFormat" };
                mcbCreditor.ColumnWidth = new string[7] { "0", "20", "200", "150", "50", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
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
                mcbProduct.SourceDataString = new string[6] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName", "ProdOpeningStock" };
                mcbProduct.ColumnWidth = new string[6] { "0", "250", "50", "50", "50", "0" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructAndFill()
        {
            ConstructReadDataColumns();
            ConstructProductGridColumns();
            dgProduct.Columns["Col_ID"].Visible = false;
            FillProductData();
            dgProduct.Refresh();
        }

        private void FillProductData()

        {
            try
            {
                 DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();

                dgProduct.DataSource = null;
                dgProduct.DataSource = dtable;
                dgProduct.Bind();
                dgProduct.Refresh();
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructProductGridColumns()
        {
            try
            {
                dgProduct.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 190;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 60;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 70;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 80;
                dgProduct.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReadDataColumns()
        {
            dgImpotProducts.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Check";
                columnCheck.ValueType = typeof(bool);
                columnCheck.Width = 50;
                columnCheck.Visible = false;
                dgImpotProducts.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Index";
                column.Width = 50;
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);
                column.HeaderText = "Index";

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyID";
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);
                column.HeaderText = "CompanyID";

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyName";
                column.HeaderText = "Company Name";
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorsProductID";
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.HeaderText = "Product ID";
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.HeaderText = "ProductName";
                column.Width = 180;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.HeaderText = "UOM";
                column.Width = 40;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.HeaderText = "Pack";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.HeaderText = "Company";
                column.Width = 60;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                column.HeaderText = "Box1";
                column.Width = 40;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.HeaderText = "Prod VAT Per";
                column.Width = 60;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.HeaderText = "Batch Number";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.HeaderText = "QTY";
                column.Width = 40;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.HeaderText = "Trade Rate";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.HeaderText = "MRP";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                column.HeaderText = "VAT";
                column.Width = 40;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.HeaderText = "Scheme";
                column.Width = 50;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                column.HeaderText = "Replacement";
                column.Width = 60;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.HeaderText = "Item Discount Per";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 60;
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.HeaderText = "Item Discount Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountPer";
                column.HeaderText = "Spl Discount Per";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountAmount";
                column.HeaderText = "Spl Discount Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                column.HeaderText = "VAT Amount Purchase";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTAmount";
                column.HeaderText = "CST Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTPer";
                column.HeaderText = "CST Percent";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                column.HeaderText = "Item SCM Discount Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";
                column.HeaderText="ItemSCMDiscountAmountPerUnit";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.HeaderText = "Expiry Date";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfOctroi";
                column.HeaderText = "Octroi";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteAmount";
                column.HeaderText = "Credit Note Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                column.HeaderText = "Cash Discount Amount";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.HeaderText = "Purchase Rate";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.HeaderText = "Sale Rate";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                column.HeaderText = "CompID";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                column.HeaderText = "VAT Amount Sale";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.HeaderText = "Shelf Code";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.HeaderText = "Shelf ID";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.HeaderText = "Margin";
                dgImpotProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.HeaderText = "Stock ID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                dgImpotProducts.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            ImportBillData.SaleBillData = dgImpotProducts;
            this.Hide();
            if (OnNewPurchase != null)
                OnNewPurchase(this, new EventArgs());
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BtnCancelClick();
        }

        private void BtnCancelClick()
        {
            this.Hide();
            if (OnCancel != null)
                OnCancel(this, new EventArgs());
        }

        private void ShowProductData()
        {
            try
            {
                if (dgImpotProducts.Rows.Count > 0)
                {
                    string productName = dgImpotProducts.CurrentRow.Cells["Col_ProductName"].Value.ToString().Trim();
                    if (dgImpotProducts.Rows[0].Cells["Col_UnitOfMeasure"].Value != null)
                        productName += ", " + dgImpotProducts.CurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString();
                    if (dgImpotProducts.Rows[0].Cells["Col_Pack"].Value != null)
                        productName += ", " + dgImpotProducts.CurrentRow.Cells["Col_Pack"].Value.ToString();
                    if (dgImpotProducts.Rows[0].Cells["Col_Company"].Value != null)
                        productName += ", " + dgImpotProducts.CurrentRow.Cells["Col_Company"].Value.ToString();
                    lblProduct.Visible = true;
                    lblProductName.Visible = true;
                    lblProductName.Text = productName;
                }
                lblProductsRemainingToMatch.Visible = true;
                lblRemainingProducts.Visible = true;
                lblRemainingProducts.Text = GetRemainingProductMatchCount();
                pnlProductMatch.Visible = true;
                dgProduct.Focus();

                if (dgImpotProducts.CurrentRow.Cells["Col_ProductName"].Value != null) //!= string.Empty
                {
                    string productNameToMatch = dgImpotProducts.CurrentRow.Cells["Col_ProductName"].Value.ToString().Trim();
                    int length = (productNameToMatch.Length >= 15) ? 15 : productNameToMatch.Length;
                    //Match from first 15 char
                    for (int index = length; index > 0; index--)
                    {
                        if (MatchProductName(productNameToMatch.Substring(0, index)))
                            break;
                    }
                }
                                  
              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private string GetRemainingProductMatchCount()
        {
            string retValue = "";
            try
            {
                int remainingCount = 0;
                foreach (DataGridViewRow row in dgImpotProducts.Rows)
                {
                    if ((bool)row.Cells["Col_Check"].Value == false)
                        remainingCount++;
                }
                retValue = remainingCount.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private bool MatchProductName(string productName)
        {
            bool retValue = false;
            try
            {
                for (int index = 0; index < dgProduct.Rows.Count; index++)
                {
                    if (dgProduct.Rows[index].Cells["Col_Name"].Value != null && dgProduct.Rows[index].Cells["Col_Name"].Value.ToString().StartsWith(productName))
                    {
                        dgProduct.Rows[index].Selected = true;
                        dgProduct.GridView.CurrentCell = dgProduct.Rows[index].Cells[1];
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

        private void btnProductMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgImpotProducts.Rows.Count > 0)
                    ProcessProductMatch();
                mcbProduct.SelectedID = string.Empty;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgProduct_DoubleClicked(object sender, EventArgs e)
        {
            ProcessProductMatch();
        }

        private void ProcessProductMatch()
        {
            try
            {
                bool retvalue = true;
                string _ProductID = string.Empty;
                string _DistProductID = string.Empty;
                _DistProductID = dgImpotProducts.CurrentRow.Cells["Col_DistributorsProductID"].Value.ToString();
                _ProductID = dgProduct.SelectedRow.Cells["Col_ID"].Value.ToString();

                foreach (DataGridViewRow dr in dgImpotProducts.Rows)
                {

                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        string _gridProductID = dr.Cells["Col_ProductID"].Value.ToString();
                        if (_ProductID == _gridProductID && dr.Cells["Col_DistributorsProductID"].Value.ToString() != _DistProductID)
                        {
                            retvalue = false;
                            break;
                        }
                    }
                }
                if (retvalue == true)
                {
                    ImportBillData.CompanyID = mcbCreditor.SelectedID;
                    retvalue = ImportBillData.CheckIFProductIDIsNOTLinked(ImportBillData.CompanyID, _DistProductID, _ProductID);

                }
                //if (retvalue == true)
                //{
                dgImpotProducts.CurrentRow.Cells["Col_Check"].Value = true;
                dgImpotProducts.CurrentRow.Cells["Col_ProductID"].Value = dgProduct.SelectedRow.Cells["Col_ID"].Value;

                int newIndex = dgImpotProducts.CurrentRow.Index + 1;
                foreach (DataGridViewRow dr in dgImpotProducts.Rows)
                {

                    if (dr.Index >= newIndex && Convert.ToBoolean(dr.Cells["Col_Check"].Value) == false)
                    {
                        newIndex = dr.Index;
                        break;
                    }


                }
                lblRemainingProducts.Text = GetRemainingProductMatchCount();
                if ((Convert.ToInt32(lblRemainingProducts.Text.ToString()) == 0) || (dgImpotProducts.CurrentRow.Index == dgImpotProducts.Rows.Count - 1))
                {
                    MessageBox.Show("Match DONE Click  FINISH  To Continue");
                    btnFinish.Enabled = true;

                    lblRemainingProducts.Text = GetRemainingProductMatchCount();
                    pnlProductMatch.Visible = false;

                    btnFinish.Focus();
                }
                else if (newIndex < dgImpotProducts.Rows.Count && (Convert.ToInt32(lblRemainingProducts.Text.ToString()) > 0))
                {
                    dgImpotProducts.Rows[newIndex].Selected = true;
                    dgImpotProducts.CurrentCell = dgImpotProducts.Rows[newIndex].Cells["Col_ProductName"];

                    ShowProductData();
                    //btnProductMatch.Focus();
                }
                //}
                //else
                //{
                //    MessageBox.Show("Product Already Linked..");
                //}
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       
        private void frmOpen_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            if (UserControlToShow is BaseControl)
            {
                IsHandled = ((BaseControl)UserControlToShow).HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            e.Handled = IsHandled;
        }
        private void UserControlToShow_ExitClicked(object sender, EventArgs e)
        {
            _SelectedID = ((BaseControl)UserControlToShow).SavedID();
            if (ItemAddedEdited != null)
                ItemAddedEdited(this, new EventArgs());
            //multiColumComboBox1.Focus();
            //FillProductData();
            mcbProduct.Focus();
            mcbProduct.SelectedID = _SelectedID;

            if (frmOpen != null)
            {
                frmOpen.Close();
            }
        }
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                frmOpen = new Form();
                frmOpen.FormBorderStyle = FormBorderStyle.FixedSingle;
                frmOpen.ControlBox = false;
                UserControlToShow = new UclProduct();
                frmOpen.Height = UserControlToShow.Height;
                frmOpen.Width = UserControlToShow.Width;
                frmOpen.StartPosition = FormStartPosition.CenterScreen;
                ((BaseControl)UserControlToShow).Mode = OperationMode.OpenAsChild;
                //((BaseControl)UserControlToShow).Mode = OperationMode.Add;
                ((BaseControl)UserControlToShow).Visible = true;
                ((BaseControl)UserControlToShow).Add();
                ((BaseControl)UserControlToShow).ExitClicked += new EventHandler(UserControlToShow_ExitClicked);
                frmOpen.Controls.Add(UserControlToShow);
                frmOpen.KeyPreview = true;
                frmOpen.KeyDown -= new KeyEventHandler(frmOpen_KeyDown);
                frmOpen.KeyDown += new KeyEventHandler(frmOpen_KeyDown);
                if (frmOpen.Controls.Count > 0)
                    frmOpen.Controls[0].Focus();
                DialogResult dResult = frmOpen.ShowDialog();
                
                FillProductCombo();

                //mcbProduct.SelectedID = _SelectedID;
                dgProduct.Focus();
                if (string.IsNullOrEmpty(_SelectedID) == false)
                {
                    Product prod = new Product();
                    prod.Id = _SelectedID;
                    prod.ReadDetailsByID();
                    FillProductData();
                    MatchProductName(prod.Name);
                }
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
     
     
        private void btnSkip_Click(object sender, EventArgs e)
        {
            ProcessSkipProduct();
        }

        private void ProcessSkipProduct()
        {
            try
            {
                dgImpotProducts.CurrentRow.Cells["Col_Check"].Value = false;
                dgImpotProducts.CurrentRow.Cells["Col_ProductID"].Value = "";

                int newIndex = dgImpotProducts.CurrentRow.Index + 1;
                if (dgImpotProducts.CurrentRow.Index == dgImpotProducts.Rows.Count - 1)
                {
                    MessageBox.Show("Click Finish To Continue");
                    btnFinish.Enabled = true;

                    lblRemainingProducts.Text = GetRemainingProductMatchCount();
                    pnlProductMatch.Visible = false;

                    btnFinish.Focus();
                }
                else if (newIndex < dgImpotProducts.Rows.Count)
                {
                    dgImpotProducts.Rows[newIndex].Selected = true;
                    dgImpotProducts.CurrentCell = dgImpotProducts.Rows[newIndex].Cells["Col_ProductName"];

                    ShowProductData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            McbCreditorSelectedIndexChanged();
            cmbFormat.Focus();
            //cmbFormat.SelectAll();
        }

        private void McbCreditorSelectedIndexChanged()
        {
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
            {
                //  retValue = CheckIFBillAlreadyEntered();
                psCombofile.Enabled = true;
                if (mcbCreditor.SeletedItem.ItemData[6] != null)
                    ImportBillData.PurchaseBillFormat = mcbCreditor.SeletedItem.ItemData[6].ToString();
                cmbFormat.Text = ImportBillData.PurchaseBillFormat;
                      

            }
        }

        private bool CheckIFBillAlreadyEntered()
        {
            bool retValue = false;
            ImportBillData.CompanyID = mcbCreditor.SelectedID;
            // ImportBillData.AIOCDACode = ImportBillData.GetAIOCDACode(ImportBillData.DistributorID);           
            retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.CompanyID, ImportBillData.BillNumber, ImportBillData.VoucherType);
            if (retValue)
            {
                ImportBillData.BillNumberAlreadyEntered = "Y";
                //cbofillFile.Text = "";
                psCombofile.SelectedID="";

            }
            return retValue;
        }

        private void rbAlliedHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                psCombofile.Focus();
                //btnSelectFileClick(); //amar com
            }
        }

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            string mcbID = string.Empty;
            int currentRow = 0;
            if (mcbProduct.SelectedID != null)
                mcbID = mcbProduct.SelectedID;
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != string.Empty)
                {
                    foreach (DataGridViewRow dr in dgProduct.Rows)
                    {
                        string dgProductID = dr.Cells["Col_ID"].Value.ToString();
                        if (mcbID == dgProductID)
                        {
                            currentRow = dr.Cells["Col_ID"].RowIndex;
                            dgProduct.Rows[currentRow].Selected = true;
                            dgProduct.GridView.CurrentCell = dgProduct.Rows[currentRow].Cells[1];
                            dgProduct.Refresh();
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

        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            McbCreditorSelectedIndexChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SetNextFileSelected();  //Amar com
        }

        //private void SetNextFileSelected()  //Amar Com
        //{
        //    try
        //    {
        //        mcbCreditor.SelectedID = "";
        //        if (dgImpotProducts.Rows.Count > 0)
        //            dgImpotProducts.Rows.Clear();
        //        if (txtFolderName.Text != null && txtFolderName.Text.ToString() != string.Empty && chklstFiles.Items.Count > 0)
        //        {
        //            if (IsLastFile() == false)
        //            {
        //                int selectedindex = chklstFiles.SelectedIndex;
        //                chklstFiles.SetItemChecked(selectedindex, false);
        //                int newSelected = selectedindex + 1;
        //                chklstFiles.SelectedIndex = newSelected;
        //                chklstFiles.SetItemChecked(newSelected, true);

        //                cbofillFile.Text = chklstFiles.CheckedItems[0].ToString();
        //                GetParty();
        //            }
        //        }
        //        else if (ifFirst == false)
        //        {
        //            MessageBox.Show("DONE");
        //            BtnCancelClick();
        //        }
        //        else
        //            ifFirst = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        private void GetParty()
        {
            string templine = "";
            string Header = "";
            ImportBillData.DistributorCode = string.Empty;
            ImportBillData.DistributorName = string.Empty;
            ImportBillData.AIOCDACode = string.Empty;
            StreamReader tempreader = new StreamReader(tempstr);
            while ((templine = tempreader.ReadLine()) != null)
            {
                string[] tempitems = templine.Split(COMMASEPERATOR); //TO CHANGE
                if (tempitems.Length == 38) // care
                {
                    Header = tempitems[0];

                    if (Header == "partycode")
                    {
                        cmbFormat.Text = "Care";
                        // rbCare.Checked = true;

                    }
                    else
                    {
                        ImportBillData.CompanyID = string.Empty;
                        string str = tempitems[0];
                        str = str.Replace('"', ' ').Trim();
                        // str = str.Replace('\',' ').Trim();
                        ImportBillData.DistributorCode = str;
                        if (ImportBillData.DistributorCode != null && ImportBillData.DistributorCode.ToString() != string.Empty)
                            ImportBillData.CompanyID  = ImportBillData.GetDistributorIDFromDistributorCode(ImportBillData.DistributorCode);
                        if (ImportBillData.CompanyID != string.Empty)
                            mcbCreditor.SelectedID = ImportBillData.CompanyID;
                        break;
                    }
                }
                else if (tempitems.Length == 22) // allied HT
                {
                    Header = tempitems[0];
                    if (Header == "H")
                    {
                        cmbFormat.Text = "Allied HT";
                        // rbAlliedHT.Checked = true;
                        ImportBillData.DistributorCode = tempitems[6];
                        ImportBillData.DistributorName = tempitems[5];
                        ImportBillData.AIOCDACode = tempitems[6];
                        if (ImportBillData.AIOCDACode != string.Empty)
                            ImportBillData.CompanyID = ImportBillData.GetDistributorIDFromAIOCDACode(ImportBillData.AIOCDACode);
                        if (ImportBillData.CompanyID == string.Empty)
                            ImportBillData.CompanyID = ImportBillData.GetDistributorIDFromDistributorCode(ImportBillData.DistributorCode);
                        if (ImportBillData.CompanyID != string.Empty)
                            mcbCreditor.SelectedID = ImportBillData.CompanyID;
                        break;
                    }
                }
                else if (tempitems.Length == 56) // Medica AMD
                {
                    Header = tempitems[0];
                    if (Header != "CNick")
                    {
                        ImportBillData.DistributorCode = tempitems[9];
                        cmbFormat.Text = "Medica";
                        if (ImportBillData.DistributorCode != null && ImportBillData.DistributorCode.ToString() != string.Empty)
                            ImportBillData.CompanyID = ImportBillData.GetDistributorIDFromDistributorCode(ImportBillData.DistributorCode);
                        if (ImportBillData.CompanyID != string.Empty)
                            mcbCreditor.SelectedID = ImportBillData.CompanyID;
                        break;
                    }
                }
                else if (tempitems.Length == 20) // dava
                {
                    Header = tempitems[0];
                    if (Header == "H")
                    {
                        cmbFormat.Text = "DAVA";
                        // rbDAVA.Checked = true;
                        ImportBillData.DistributorCode = tempitems[18];
                        ImportBillData.DistributorName = tempitems[19];
                        ImportBillData.AIOCDACode = tempitems[17];
                        // ImportBillData.AIOCDACode = "";
                        if (ImportBillData.AIOCDACode != string.Empty)
                            ImportBillData.CompanyID = ImportBillData.GetDistributorIDFromAIOCDACode(ImportBillData.AIOCDACode);
                        if (ImportBillData.CompanyID == string.Empty)
                            ImportBillData.CompanyID = ImportBillData.GetDistributorIDFromDistributorCode(ImportBillData.DistributorCode);
                        if (ImportBillData.CompanyID != string.Empty)
                            mcbCreditor.SelectedID = ImportBillData.CompanyID;
                        break;
                    }
                }
            }
        }

        //private bool IsLastFile()
        //{
        //    bool retValue = false;
        //    int idx = chklstFiles.SelectedIndex;
        //    if (idx + 1 == chklstFiles.Items.Count)
        //    {
        //        retValue = true;
        //        DialogResult dr = MessageBox.Show("All Files Processed. Do you want to close?", General.ApplicationTitle, MessageBoxButtons.YesNo);
        //        if (dr == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            this.Hide();
        //            if (OnCancel != null)
        //                OnCancel(this, new EventArgs());
        //        }
        //    }

        //    return retValue;
        //}

        private void FormImportSaleBill_Load(object sender, EventArgs e)
        {
            this.ActiveControl = psCombofile;
            psCombofile.Focus();
            psCombofile.AllowDrop = true;
        }

        private void cmbFormat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnReadData.Focus();
            }
        }

        private void cbofillFile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode ==Keys.Enter)
                {
                    mcbCreditor.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbFormat.Focus();
                }
                                   
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void cmbFormat_MouseHover(object sender, EventArgs e)
        {
            //((ComboBox)sender).DroppedDown = true;
        }

        private void cbofillFile_MouseClick(object sender, MouseEventArgs e)
        {
            //fillcombofiles();
        }

        private void cmbFormat_MouseClick(object sender, MouseEventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        private void FormImportSaleBill_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            //if (_ActiveControl != null && _ActiveControl is BaseControl)
            //{
            //    IsHandled = ((BaseControl)_ActiveControl).HandleShortcutAction(e.KeyCode, e.Modifiers);
            //}

            if (IsHandled == false)
            {
                IsHandled = HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            e.Handled = IsHandled;
        }

        private void psCombofile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                   mcbCreditor.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void psCombofile_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                mcbCreditor.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            psCombofile.Focus();
        }
    }
}
