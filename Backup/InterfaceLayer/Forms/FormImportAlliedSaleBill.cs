using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using System.IO;
using PharmaSYSRetailPlus.BusinessLayer;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    public partial class FormImportAlliedSaleBill : Form
    {
        private const char TABSEPERATOR = '\t';
        private const char COMMASEPERATOR = ',';
        public ImportBill ImportBillData;

        public event EventHandler OnNewProduct;
        public event EventHandler OnNewParty;
        public event EventHandler OnNewPurchase;
        public event EventHandler OnCancel;

        public FormImportAlliedSaleBill()
        {
            InitializeComponent();
            ImportBillData = new ImportBill();
            rbAllied.Checked = true;
        }

        public void RefreshData()
        {
            try
            {
                if (dgParty.Columns.Count > 0 && pnlPartyMatch.Visible)
                {
                    FillPartyData();
                    dgParty.Focus();
                    SelectPartyInTheGrid();
                }
                if (dgProduct.Columns.Count > 0 && pnlProductMatch.Visible)
                {
                    FillProductData();
                    dgProduct.Focus();
                    ShowProductData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFileName.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            bool retValue = false;
            string wrongFormat = "N";
            if (txtFileName.Text != null && txtFileName.Text.ToString() != string.Empty)
            {
                if (rbAllied.Checked == true)
                {

                    ImportBillData.BillNumberAlreadyEntered = "N";
                    try
                    {
                        ConstructAndFill();

                        string line;
                        if (txtFileName.Text != string.Empty && File.Exists(txtFileName.Text))
                        {
                            StreamReader reader = new StreamReader(txtFileName.Text);
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                if (items.Length >= 10)
                                {

                                    string Header = items[0];
                                    if (Header == "H")
                                    {
                                        //H,1.1,110190,21032014,XXXX,21032014,,1,ChequeNo,21032014,.                        ,XXXX,21032014,Destination, 0,0,     3347.00,MHPU200022,D3089,C.T.DISTRIBUTORS
                                        ImportBillData.BillDate = items[1];
                                        ImportBillData.BillNumber = items[2];
                                        // ImportBillData.BillDate = items[3];
                                        ImportBillData.TransactionType = items[3];
                                        ImportBillData.DistributorCode = items[6];
                                        ImportBillData.DistributorName = items[7];
                                        ImportBillData.mscdaCode = items[12];
                                        ImportBillData.CashDiscountPercent = items[20];
                                        ImportBillData.CashDiscountAmount = items[21];
                                        ImportBillData.TotalAmount = items[15];
                                        ImportBillData.BillNetAmount = items[28];


                                        if (ImportBillData.TransactionType == "1")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                        else if (ImportBillData.TransactionType == "3")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                        else
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                        ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                        ImportBillData.DistributorID = ImportBillData.CheckParty();
                                        ImportBillData.mscdaCode = ImportBillData.DistributorID;
                                        retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.mscdaCode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                        if (retValue)
                                        {
                                            ImportBillData.BillNumberAlreadyEntered = "Y";
                                            txtFileName.Text = "";
                                            break;
                                        }
                                    }
                                    else if (Header == "T")
                                    {

                                        //    1   2                              3      4    5                              6      7                8               9     10 11   12       13          14   15      16  17,18,19,  20,  21   22  , 23,24,    25    
                                        //T, 351,ALLERGAN IND.PVT.LTD.(ALPHA)  ,ALP,   4814,COMBIGAN DROP                 ,5 ML  ,ALL            ,PT0087         ,01112015,1,1,  5.00,    224.24,    224.24,0,    291.24,0,0,0,     1,    0, 0.00,0,0,      0.00
                                        //T,  90,SUN PHARMA (ARIAN DIV)        ,ARI,   2254,AQUAZIDE 25 MG TAB            ,10 TAB,SUN            ,BSM2218        ,01082015,1,1,  5.00,     14.31,     14.31,0,     17.43,0,0,0,     5,    0, 0.00,0,0,      0.00
                                        //T,  90,SUN PHARMA (ARIAN DIV)        ,ARI,   4366,REPALOL H TAB                 ,10 TAB,SUN            ,SKM1663        ,01112015,1,1,  5.00,     66.22,     66.22,0,     86.00,0,0,0,    10,    0, 0.00,0,0,      0.00
                                        //T, 140,SUN PHARMA (AVIOR DIV)        ,AVI,   3067,PIOGLIT MF 7.5 MG TAB         ,10 TAB,SUN            ,SKM1505        ,01042015,1,1,  5.00,     35.42,     35.42,0,     46.00,0,0,0,     3,    0, 0.00,0,0,      0.00
                                        //T, 140,SUN PHARMA (AVIOR DIV)        ,AVI,   8761,VOLIBO M 0.2 MG TAB           ,10 TAB,SUN            ,BSM3782        ,01112015,1,1,  5.00,     59.29,     59.29,0,     77.00,0,0,0,     6,    0, 0.00,0,0,      0.00
                                        //T,  43,SUN PHARMA (AZURA DIV)        ,AZU,   1232,CARDIVAS 12.5 MG TAB          ,10 TAB,SUN            ,BSM3749        ,01112016,1,1,  5.00,     47.74,     47.74,0,     62.00,0,0,0,    10,    0, 0.00,0,0,      0.00
                                        //T, 601,CIPLA (UNKNOWN)               ,CIP,   7660,BAMBUDIL 10 MG TAB            ,10 TAB,CIP            ,PW3066         ,01112016,1,1,  5.00,     36.57,     36.57,0,     48.00,0,0,0,    10,    0, 0.00,0,0,      0.00
                                        //T,  18,SUN PHARMA (MILMET DIV)       ,MIL,   2041,AZELAST DROP                  ,5 ML  ,SUN            ,HKM1169        ,01082015,1,1,  5.00,     59.29,     59.29,0,     77.00,0,0,0,     1,    0, 0.00,0,0,      0.00
                                        //T, 591,SUN PHARMA (SYGNUS)           ,SYN,   9309,LACTIFIBER GRANULES           ,180 GM,SUN            ,BSN0027        ,01122015,1,1,  5.00,    204.03,    204.03,0,    265.00,0,0,0,     3,    0, 0.00,0,0,      0.00
                                        //T,  42,SUN PHARMA (SYNERGY DIV)      ,SYN,   4151,NEXITO FORTE TAB              ,10 TAB,SUN            ,BSM3175        ,01112015,1,1,  5.00,     72.37,     72.37,0,     94.00,0,0,0,     5,    0, 0.00,0,0,      0.00


                                        int rowIndex = dgAlliedProducts.Rows.Add();
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[1];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[2];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[3];
                                        //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[4];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[5];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[6];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[7];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[12];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[8];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[8];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[9];
                                    }
                                    else if (Header == "F")
                                    {
                                        //       1             2          3   4,5,6,     7    , 8,     9,        10,  11,12,13,14,15,16,17,18,19,    20 ,     21,         22,        23     
                                        //F,     3296.32,      0.00,      0.00,0,0,0,    159.38,0,      0.00,      0.00,0,0,0,0,0,0,0,0,      0.00,  3.30,    108.78,      0.08,     3347.00
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
                                        break;
                                    }
                                }
                            } //While
                        }

                        if (ImportBillData.BillNumberAlreadyEntered == "Y")
                            MessageBox.Show("Purchase Already Entered For This Bill Number " + ImportBillData.VoucherType + ":" + ImportBillData.VoucherNumber);
                        else if (wrongFormat != "Y")
                        {

                            lblParty.Visible = true;

                            lblPartyName.Text = ImportBillData.DistributorName;
                            lblPartyName.Visible = true;

                            lblTotalProductsToMatch.Visible = true;
                            lblTotalProducts.Visible = true;
                            lblTotalProducts.Text = dgAlliedProducts.Rows.Count.ToString();

                            lblRemainingProducts.Text = dgAlliedProducts.Rows.Count.ToString();


                            lblPharmaSysParty.Visible = true;
                            lblPharmaSYSPartyName.Visible = true;


                            pnlPartyMatch.Visible = true;
                            pnlPartyMatch.Location = new Point(501, 102);

                            pnlPartyMatch.Focus();
                            dgParty.Focus();
                            SelectPartyInTheGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }
                }
                else if (rbDAVA.Checked == true)
                {

                    ImportBillData.BillNumberAlreadyEntered = "N";
                    try
                    {
                        ConstructAndFill();

                        string line;
                        if (txtFileName.Text != string.Empty && File.Exists(txtFileName.Text))
                        {
                            StreamReader reader = new StreamReader(txtFileName.Text);
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
                                        ImportBillData.DistributorCode = items[17];
                                        ImportBillData.DistributorName = items[19];
                                        ImportBillData.mscdaCode = items[17];
                                        ImportBillData.CashDiscountPercent = items[14];
                                        ImportBillData.CashDiscountAmount = items[15];
                                        ImportBillData.TotalAmount = items[16];
                                        ImportBillData.BillNetAmount = items[16];


                                        if (ImportBillData.TransactionType == "1")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                        else if (ImportBillData.TransactionType == "3")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                        else
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                        ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                        ImportBillData.DistributorID = ImportBillData.CheckParty();
                                        ImportBillData.mscdaCode = ImportBillData.DistributorID;
                                        retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.mscdaCode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                        if (retValue)
                                        {
                                            ImportBillData.BillNumberAlreadyEntered = "Y";
                                            txtFileName.Text = "";
                                            break;
                                        }
                                    }
                                    else if (Header == "T")
                                    {

     //   0,   1  2                            ,  3,      4,    5                         ,   6  ,  7            ,   8           ,   9    ,10,11,  12,        13,        14,15,       16,17,18,19, 20,   21,   22,23,24,    25    
     //   T, 305,CIPLA (RESP TEAM I)           ,RT1,   7190,MUCINAC 600 MG TAB            ,10 TAB,CIP            ,D42567         ,01072016,1,1,  5.00,    136.38,    136.38,0,    179.00,0,0,0,     5,    0, 0.00,0,0,      0.00
     //   T, 324,CIPLA (SPECTRACARE II)        ,SP2,   7347,FORCAN 150 MG TAB             ,1 TAB ,CIP            ,A41566         ,01062016,1,1,  5.00,     12.41,     12.41,0,     16.12,0,0,0,    18,    2, 0.00,0,0,      0.00
     //   T, 324,CIPLA (SPECTRACARE II)        ,SP2,   7690,NOVAMOX CV 625 TAB            ,10 TAB,CIP            ,DT4480         ,01062016,1,1,  5.00,    138.59,    138.59,0,    180.00,0,0,0,     5,    0, 0.00,0,0,     69.30
     //   F,     1598.23,      0.00,      0.00,0,0,0,     72.62,0,      0.00,      0.00,0,0,0,0,0,0,0,0,      0.00,  5.00,     76.45,     -0.10,     1525.00


                                        int rowIndex = dgAlliedProducts.Rows.Add();
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[4];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[5];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[6];
                                        //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[8];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[9];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[20];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[21];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[12];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[13];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[14];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[16];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ItemSCMDiscountAmount"].Value = items[25];

                                    }
                                    else if (Header == "F")
                                    {
                                        //       1             2          3   4,5,6,     7    , 8,     9,        10,  11,12,13,14,15,16,17,18,19,    20 ,     21,         22,        23     
                                        //F,     3296.32,      0.00,      0.00,0,0,0,    159.38,0,      0.00,      0.00,0,0,0,0,0,0,0,0,      0.00,  3.30,    108.78,      0.08,     3347.00
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
                                        break;
                                    }
                                }
                            } //While
                        }

                        if (ImportBillData.BillNumberAlreadyEntered == "Y")
                            MessageBox.Show("Purchase Already Entered For This Bill Number");
                        else if (wrongFormat != "Y")
                        {

                            lblParty.Visible = true;

                            lblPartyName.Text = ImportBillData.DistributorName;
                            lblPartyName.Visible = true;

                            lblTotalProductsToMatch.Visible = true;
                            lblTotalProducts.Visible = true;
                            lblTotalProducts.Text = dgAlliedProducts.Rows.Count.ToString();

                            lblRemainingProducts.Text = dgAlliedProducts.Rows.Count.ToString();


                            lblPharmaSysParty.Visible = true;
                            lblPharmaSYSPartyName.Visible = true;


                            pnlPartyMatch.Visible = true;
                            pnlPartyMatch.Location = new Point(501, 102);

                            pnlPartyMatch.Focus();
                            dgParty.Focus();
                            SelectPartyInTheGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }
                }
                else if (rbAMD.Checked)
                {
                    ImportBillData.BillNumberAlreadyEntered = "N";
                    try
                    {
                        ConstructAndFill();

                        string line;
                      //  string stringdata;
                        if (txtFileName.Text != string.Empty && File.Exists(txtFileName.Text))
                        {
                            StreamReader reader = new StreamReader(txtFileName.Text);
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] items = line.Split(COMMASEPERATOR); //TO CHANGE
                                if (items.Length >= 10)
                                {

                                    string Header = items[0];
                                    if (Header != "CNick")
                                    {
                                        //     0   1     2       3      4                5   6   7              8  9,   10        11    12,  13,14,15,16,   17,   18, 19, 20,21,22,23,24, 25   26   27, 28,29, 30     31  32                           ,33,34,35,36,37,38,  39                ,40,41,42, 43    44  ,45 ,46,47,48,49                                                                                                                                                                                   
                                        //        CRB,846,20140408,D 648,SWAMI MEDICAL STORES,1,3513,ASOMEX-AT TAB,1*10, ,I6A13017,20161101,Nov-16,3,  ,0 ,0 ,61.52,61.52,79.9,0 ,0,  0, 5, 0,9.08,  0,184.56,0, 0,865.42,894,AVALABALE OLMAX CH 20 & 40 GLE,  , 0,  ,  , 0,11,EMCURE PHARMACEUTICA, 0, 0, 0,13.85,13.85,1.6,0 , 0, 0,MHPU200047
                                        //        CRB,846,20140408,D 648,SWAMI MEDICAL STORES,2,8716,ASOMEX OH TAB,1*10,,PDA13003,20151101,Nov-15,2,,0,0,89.9,89.9,118,0,0,0,5,0,8.85,0,179.8,0,0,865.42,894,AVALABALE OLMAX CH 20 & 40 GLE,,0,,,0,11,EMCURE PHARMACEUTICA,0,0,0,13.85,13.85,1.6,0,0,0,MHPU200047
//    0,                         1,     2,                   3,        4,   5,      6,    7 ,        8 ,      9,      10 ,       11,     12,   13,  14,    15,        16,17,18,19,    20,   21,      22,                   23,    24,                 25,    26,    27,    28,     29,        30, 31,  32,            33,    34,     35,    36, 37,   38,       39,     40,    41,   42,   43,    44,    45,     46,    47,  48,  49,       50,     51,      52,      53, 54,55
//CNick,Vendor                    ,CUCode,Customer            ,Area     ,City,PinCode,InvNo ,InvDate   ,OrderNo,OrderDate,Transport,Freight,Paid ,LRNo,LRDate,CreditDays,Ad,Ls,Tx,InvAmt,CNote,MfgrNick,Manufacturer         ,PrCode,ProductDesc        ,PPack ,MyType,MyMode,BatchNo,ExpDate   ,Qty,Free,SchQtyAdjInAmt,Rate  ,GrsAmt ,PTR   ,MRP,WPPer,OctroiPer,SchRate,SchPer,CDPer,TDPer,CSTPer,VATPer,INetAmt,Remark,LOCA,LOCN,KeepWatch,DivNick,MyTypeId,MyItemNo,PTS,Barcode
//     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RN14    ,RANBAXY -CROSLAND 1  ,1944  ,REFZIL-O 250 MG TAB,6 TAB ,SALE  ,      ,2604838,31/03/2016,3  ,0   ,0             ,342.86,1028.58,342.86,450,    0,        0,      0,     0,    3,    0,     0,     5,1047.61,      ,F-17,   0,    FALSE,    RN9,       1,       1,0  ,7328116020
//     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RE1     ,RESILIENT COSMECUTICA,2634  ,CLINSUP V3 ER TAB  ,3 TAB ,SALE  ,      ,D-00114,30/11/2015,2  ,0   ,0             ,51.05 ,102.1  ,51.05 , 67,    0,        0,      0,     0,    3,    0,     0,     5,103.99 ,      ,G-18,   0,    FALSE,    RE1,       1,       2,0  ,7912110200

                                        ImportBillData.BillNumber = items[7];
                                        ImportBillData.BillDate = items[8];
                                        ImportBillData.TransactionType = items[52];
                                        ImportBillData.DistributorCode = "";
                                        ImportBillData.DistributorName = items[1];
                                        ImportBillData.mscdaCode = "";
                                        ImportBillData.CashDiscountPercent = items[42];
                                        ImportBillData.CashDiscountAmount = "0.00";
                                        ImportBillData.TotalAmount = items[20];
                                        ImportBillData.BillNetAmount = items[20];


                                        if (ImportBillData.TransactionType == "1")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                        else if (ImportBillData.TransactionType == "3")
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                        else
                                            ImportBillData.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                                        ImportBillData.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                                        ImportBillData.DistributorID = ImportBillData.CheckParty();
                                        ImportBillData.mscdaCode = ImportBillData.DistributorID;
                                        retValue = ImportBillData.CheckForBillNumberInPurchase(ImportBillData.mscdaCode, ImportBillData.BillNumber, ImportBillData.VoucherType);
                                        if (retValue)
                                        {
                                            ImportBillData.BillNumberAlreadyEntered = "Y";
                                            txtFileName.Text = "";
                                            break;
                                        }
 //   0,                         1,     2,                   3,        4,   5,      6,    7 ,        8 ,      9,      10 ,       11,     12,   13,  14,    15,        16,17,18,19,    20,   21,      22,                   23,    24,                 25,    26,    27,    28,     29,        30, 31,  32,            33,    34,     35,    36, 37,   38,       39,     40,    41,   42,   43,    44,    45,     46,    47,  48,  49,       50,     51,      52,      53, 54,55
//CNick,Vendor                    ,CUCode,Customer            ,Area     ,City,PinCode,InvNo ,InvDate   ,OrderNo,OrderDate,Transport,Freight,Paid ,LRNo,LRDate,CreditDays,Ad,Ls,Tx,InvAmt,CNote,MfgrNick,Manufacturer         ,PrCode,ProductDesc        ,PPack ,MyType,MyMode,BatchNo,ExpDate   ,Qty,Free,SchQtyAdjInAmt,Rate  ,GrsAmt ,PTR   ,MRP,WPPer,OctroiPer,SchRate,SchPer,CDPer,TDPer,CSTPer,VATPer,INetAmt,Remark,LOCA,LOCN,KeepWatch,DivNick,MyTypeId,MyItemNo,PTS,Barcode
//     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RN14    ,RANBAXY -CROSLAND 1  ,1944  ,REFZIL-O 250 MG TAB,6 TAB ,SALE  ,      ,2604838,31/03/2016,3  ,0   ,0             ,342.86,1028.58,342.86,450,    0,        0,      0,     0,    3,    0,     0,     5,1047.61,      ,F-17,   0,    FALSE,    RN9,       1,       1,0  ,7328116020
//     ,ANAND MEDICAL DISTRIBUTORS,57    ,EMKE MEDICAL   (368),JANWAWADI,PUNE,      0,292615,18/10/2014,       ,         ,         ,0      ,FALSE,    ,      ,14        ,0 ,0 ,0 ,1152  ,0    ,RE1     ,RESILIENT COSMECUTICA,2634  ,CLINSUP V3 ER TAB  ,3 TAB ,SALE  ,      ,D-00114,30/11/2015,2  ,0   ,0             ,51.05 ,102.1  ,51.05 , 67,    0,        0,      0,     0,    3,    0,     0,     5,103.99 ,      ,G-18,   0,    FALSE,    RE1,       1,       2,0  ,7912110200

                                        int rowIndex = dgAlliedProducts.Rows.Add();
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Check"].Value = false;
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Index"].Value = rowIndex + 1;
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyID"].Value = items[1];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_CompanyName"].Value = items[2];
                                        //  dgAlliedProducts.Rows[rowIndex].Cells["Col_Company"].Value = items[3];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_DistributorsProductID"].Value = items[24];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ProductName"].Value = items[25];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Pack"].Value = items[26];
                                        //  dgReadData.CurrentRow.Cells[""].Value = items[7];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_BatchNumber"].Value = items[29];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ExpiryDate"].Value = items[30];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Quantity"].Value = items[31];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_Scheme"].Value = items[32];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_VAT"].Value = items[45];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_TradeRate"].Value = items[34];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_PurchaseRate"].Value = items[36];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_MRP"].Value = items[37];
                                        dgAlliedProducts.Rows[rowIndex].Cells["Col_ItemSCMDiscountAmount"].Value = "0.00";
                               
                                        
                                    }
                                    //else
                                    //{
                                    //    MessageBox.Show("Wrong Format");
                                    //    wrongFormat = "Y";
                                    //    break;
                                    //}
                                }
                            } //While
                        }

                        if (ImportBillData.BillNumberAlreadyEntered == "Y")
                            MessageBox.Show("Purchase Already Entered For This Bill Number");
                        else if (wrongFormat != "Y")
                        {

                            lblParty.Visible = true;

                            lblPartyName.Text = ImportBillData.DistributorName;
                            lblPartyName.Visible = true;

                            lblTotalProductsToMatch.Visible = true;
                            lblTotalProducts.Visible = true;
                            lblTotalProducts.Text = dgAlliedProducts.Rows.Count.ToString();

                            lblRemainingProducts.Text = dgAlliedProducts.Rows.Count.ToString();


                            lblPharmaSysParty.Visible = true;
                            lblPharmaSYSPartyName.Visible = true;


                            pnlPartyMatch.Visible = true;
                            pnlPartyMatch.Location = new Point(501, 102);

                            pnlPartyMatch.Focus();
                            dgParty.Focus();
                            SelectPartyInTheGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select File");
                txtFileName.Focus();
            }

        }

        private void SelectPartyInTheGrid()
        {
            try
            {
                //Match Allied Code
                bool matchCode = MatchPartyCode(ImportBillData.DistributorCode, ImportBillData.mscdaCode);
                if (matchCode == false)
                {
                    int length = (ImportBillData.DistributorName.Length >= 10) ? 10 : ImportBillData.DistributorName.Length;
                    //Match from first 10 char
                    for (int index = length; index > 0; index--)
                    {
                        if (MatchPartyName(ImportBillData.DistributorName.Substring(0, index)))
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private bool MatchPartyName(string partyName)
        {
            bool retValue = false;
            try
            {
                for (int index = 0; index < dgParty.Rows.Count; index++)
                {
                    if (dgParty.Rows[index].Cells["Col_Name"].Value != null && dgParty.Rows[index].Cells["Col_Name"].Value.ToString().StartsWith(partyName))
                    {
                        dgParty.Rows[index].Selected = true;
                        dgParty.GridView.CurrentCell = dgParty.Rows[index].Cells[1];
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

        private bool MatchPartyCode(string alliedcode, string mscdacode)
        {
            bool retValue = false;
            try
            {
                for (int index = 0; index < dgParty.Rows.Count; index++)
                {
                    if ((dgParty.Rows[index].Cells["Col_AlliedCode"].Value != null && dgParty.Rows[index].Cells["Col_AlliedCode"].Value.ToString() == alliedcode) || (dgParty.Rows[index].Cells["Col_MscdaCode"].Value != null && dgParty.Rows[index].Cells["Col_MscdaCode"].Value.ToString() == mscdacode))
                    {
                        dgParty.Rows[index].Selected = true;
                        dgParty.GridView.CurrentCell = dgParty.Rows[index].Cells[1];
                        retValue = true;
                        lblPharmaSYSPartyName.Text = ImportBillData.DistributorName;
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

        private void ConstructPartyGridColumns()
        {
            try
            {
                dgParty.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgParty.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgParty.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 175;
                dgParty.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AlliedCode";
                column.DataPropertyName = "AlliedCode";
                column.HeaderText = "AlliedCode";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgParty.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MscdaCode";
                column.DataPropertyName = "MSCDACode";
                column.HeaderText = "MSCDACode";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgParty.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillPartyData()
        {
            try
            {
                DataTable dtable = new DataTable();
                Account _Account = new Account();
                dtable = _Account.GetAccountHoldersListForAlliedImport(FixAccounts.AccCodeForCreditor);
                dgParty.DataSource = dtable;
                dgParty.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructAndFill()
        {
            ConstructReadDataColumns();

            ConstructPartyGridColumns();
            FillPartyData();

            ConstructProductGridColumns();
            FillProductData();
        }

        private void FillProductData()
        {
            DataTable dtable = new DataTable();
            dtable = General.ProductList;
            dgProduct.DataSource = dtable;
            dgProduct.Bind();
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
                column.Width = 270;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 50;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 7;
                dgProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 60;
                dgProduct.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReadDataColumns()
        {
            dgAlliedProducts.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Check";
                columnCheck.ValueType = typeof(bool);
                columnCheck.Width = 50;
                dgAlliedProducts.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Index";
                column.Width = 50;
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyID";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyName";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorsProductID";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountPer";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTPer";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfOctroi";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                dgAlliedProducts.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                dgAlliedProducts.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            ImportBillData.SaleBillData = dgAlliedProducts;
            this.Hide();
            if (OnNewPurchase != null)
                OnNewPurchase(this, new EventArgs());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            if (OnCancel != null)
                OnCancel(this, new EventArgs());
        }

        private void dgParty_DoubleClicked(object sender, EventArgs e)
        {
            ProcessPartyMatch();
        }

        private void ProcessPartyMatch()
        {
            try
            {
                //TODo: Save the allied code in Account Table

                lblPharmaSYSPartyName.Text = dgParty.SelectedRow.Cells[1].Value.ToString();
                ImportBillData.DistributorID = dgParty.SelectedRow.Cells[0].Value.ToString();
                lblProduct.Visible = true;
                lblProductName.Visible = true;
                if (dgAlliedProducts.Rows.Count > 0)
                {
                    dgAlliedProducts.Rows[0].Selected = true;
                    dgAlliedProducts.CurrentCell = dgAlliedProducts.Rows[0].Cells[1];
                }
                lblPartyName.Text = dgParty.SelectedRow.Cells[1].Value.ToString();
                ShowProductData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ShowProductData()
        {
            try
            {
                if (dgAlliedProducts.Rows.Count > 0)
                {
                    string productName = dgAlliedProducts.CurrentRow.Cells["Col_ProductName"].Value.ToString().Trim();
                    if (dgAlliedProducts.Rows[0].Cells["Col_UnitOfMeasure"].Value != null)
                        productName += ", " + dgAlliedProducts.CurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString();
                    if (dgAlliedProducts.Rows[0].Cells["Col_Pack"].Value != null)
                        productName += ", " + dgAlliedProducts.CurrentRow.Cells["Col_Pack"].Value.ToString();
                    if (dgAlliedProducts.Rows[0].Cells["Col_Company"].Value != null)
                        productName += ", " + dgAlliedProducts.CurrentRow.Cells["Col_Company"].Value.ToString();
                    lblProductName.Text = productName;
                }
                lblProductsRemainingToMatch.Visible = true;
                lblRemainingProducts.Visible = true;
                lblRemainingProducts.Text = GetRemainingProductMatchCount();

                pnlProductMatch.Visible = true;
                pnlPartyMatch.Visible = false;

                //    bool matchProductCode = MatchProductCode(ImportBillData. DistributorCode, ImportBillData.mscdaCode);

                dgProduct.Focus();

                string productNameToMatch = dgAlliedProducts.CurrentRow.Cells["Col_ProductName"].Value.ToString().Trim();
                int length = (productNameToMatch.Length >= 15) ? 15 : productNameToMatch.Length;

                //Match from first 15 char
                for (int index = length; index > 0; index--)
                {
                    if (MatchProductName(productNameToMatch.Substring(0, index)))
                        break;
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
                foreach (DataGridViewRow row in dgAlliedProducts.Rows)
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

        private void btnMatchParty_Click(object sender, EventArgs e)
        {
            ProcessPartyMatch();
        }

        private void btnProductMatch_Click(object sender, EventArgs e)
        {
            ProcessProductMatch();
        }

        private void dgProduct_DoubleClicked(object sender, EventArgs e)
        {
            ProcessProductMatch();
        }

        private void ProcessProductMatch()
        {
            try
            {
                dgAlliedProducts.CurrentRow.Cells["Col_Check"].Value = true;
                dgAlliedProducts.CurrentRow.Cells["Col_ProductID"].Value = dgProduct.SelectedRow.Cells["Col_ID"].Value;

                int newIndex = dgAlliedProducts.CurrentRow.Index + 1;
                if (dgAlliedProducts.CurrentRow.Index == dgAlliedProducts.Rows.Count - 1)
                {
                    MessageBox.Show("Match DONE Click Finish To Continue");
                    btnFinish.Enabled = true;

                    lblRemainingProducts.Text = GetRemainingProductMatchCount();
                    pnlProductMatch.Visible = false;

                    btnFinish.Focus();
                }
                else if (newIndex < dgAlliedProducts.Rows.Count)
                {
                    dgAlliedProducts.Rows[newIndex].Selected = true;
                    dgAlliedProducts.CurrentCell = dgAlliedProducts.Rows[newIndex].Cells[1];

                    ShowProductData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            if (OnNewProduct != null)
                OnNewProduct(this, new EventArgs());

        }

        private void btnNewParty_Click(object sender, EventArgs e)
        {
            if (OnNewParty != null)
                OnNewParty(this, new EventArgs());
        }

        private void FormImportAlliedSaleBill_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("ENTER");
        }

        private void rbAllied_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllied.Checked == true)
            {
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
                dgAlliedProducts.CurrentRow.Cells["Col_Check"].Value = false;
                dgAlliedProducts.CurrentRow.Cells["Col_ProductID"].Value = "";

                int newIndex = dgAlliedProducts.CurrentRow.Index + 1;
                if (dgAlliedProducts.CurrentRow.Index == dgAlliedProducts.Rows.Count - 1)
                {
                    MessageBox.Show("Click Finish To Continue");
                    btnFinish.Enabled = true;

                    lblRemainingProducts.Text = GetRemainingProductMatchCount();
                    pnlProductMatch.Visible = false;

                    btnFinish.Focus();
                }
                else if (newIndex < dgAlliedProducts.Rows.Count)
                {
                    dgAlliedProducts.Rows[newIndex].Selected = true;
                    dgAlliedProducts.CurrentCell = dgAlliedProducts.Rows[newIndex].Cells[1];

                    ShowProductData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
