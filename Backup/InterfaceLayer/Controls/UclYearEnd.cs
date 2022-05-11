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
using PharmaSYSRetailPlus.DataLayer;
using System.Configuration;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclYearEnd : BaseControl
    {
        #region Declaration

        public string currentdatabase = "pharmasysretailplus" + General.ShopDetail.ShopVoucherSeries;
        public string vsleft = "";
        public string vsright = "";
        public string newvoucherseries = "";
        public string oldvoucherseries = "";
        public string newdatabase = "";
        public string tablename = "";
        public string oldsyear = "";
        public string oldeyear = "";
        public string newsyear = "";
        public string neweyear = "";
        #endregion

        public UclYearEnd()
        {
            InitializeComponent();
          
           
        }

        private void psButton1_Click(object sender, EventArgs e)
        {
            oldvoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
            vsleft = (Convert.ToInt32(General.ShopDetail.ShopVoucherSeries.Substring(0, 2)) + 1).ToString();
            vsright = (Convert.ToInt32(General.ShopDetail.ShopVoucherSeries.Substring(2, 2)) + 1).ToString();
            oldsyear = General.ShopDetail.Shopsy;
            oldeyear = General.ShopDetail.Shopey;
            string lsy = (Convert.ToInt32(General.ShopDetail.Shopsy.Substring(0, 4)) + 1).ToString();
            string ley = (Convert.ToInt32(General.ShopDetail.Shopey.Substring(0, 4)) + 1).ToString();
            newsyear = lsy + General.ShopDetail.Shopsy.Substring(4, 4);
            neweyear = ley + General.ShopDetail.Shopey.Substring(4, 4);
            newvoucherseries = vsleft + vsright;
            newdatabase = "pharmasysretailplus" + newvoucherseries;
            if (CreateNewBase())
            {
                CreateTables();
                if (ConnectData())
                {
                  //  DefinePrimaryKeys();
                    ChangeAccountingYear();
                    //ChangeVoucherNumbers();
                    //ClearPreviousData();
                }
                

            }
            else
                MessageBox.Show("Database Aleady Exists");

        }

        private void DefinePrimaryKeys()
        {
            YearEnd ye = new YearEnd();
            tablename = "changeddetailcashbankexpenses";
            bool retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");           
            tablename = "changeddetailcashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankPaymentID");
            tablename = "changeddetailcashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankReceiptID");
            tablename = "changeddetailcreditdebitnoteamount";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteAmountID");
            tablename = "changeddetailcreditdebitnotestock";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteStockID");
            tablename = "changeddetailpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailPurchaseID");
            tablename = "changeddetailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            tablename = "changedspecialdetailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            tablename = "changedspecialvouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "changedvouchercashbankexpenses";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvouchercashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvouchercashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvouchercreditdebitnote";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvoucherjv";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvoucherpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "changedvouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ChangedID");
            tablename = "deleteddetailcashbankexpenses";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "deleteddetailcashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankPaymentID");
            tablename = "deleteddetailcashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankReceiptID");
            tablename = "deleteddetailcreditdebitnoteamount";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteAmountID");
            tablename = "deleteddetailcreditdebitnotestock";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteStockID");
            tablename = "deleteddetailpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailPurchaseID");
            tablename = "deleteddetailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            tablename = "deletedspecialdetailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            tablename = "deletedspecialvouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "deletedvouchercashbankexpenses";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "deletedvouchercashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "deletedvouchercashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "deletedvouchercreditdebitnote";
            retValue = ye.DefinePrimaryKeys(tablename, "CRDBID");
            tablename = "deletedvoucherjv";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "deletedvoucherpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "purchaseID");
            tablename = "deletedvouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "detailcashbankexpenses";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "detailcashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankPaymentID");
            tablename = "detailcashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankReceiptID");
            tablename = "detailchequereturn";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailChequeReturnID");
            tablename = "detailcreditdebitnoteamount";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteAmountID");
            tablename = "detailcreditdebitnotestock";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailCreditDebitNoteStockID");
            tablename = "detailopstock";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailOpStockID");
            tablename = "detailpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailPurchaseID");
            tablename = "detailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            //tablename = "inity";
            //retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "linkdebtorproduct";
            retValue = ye.DefinePrimaryKeys(tablename, "LinkDebtorProductID");
            tablename = "linkdruggrouping";
            retValue = ye.DefinePrimaryKeys(tablename, "LinkDrugGroupingID");
            tablename = "linkpartycompany";
            retValue = ye.DefinePrimaryKeys(tablename, "LinkPartyCompanyID");
            tablename = "linkpatientproduct";
            retValue = ye.DefinePrimaryKeys(tablename, "LinkPatientProductID");
            tablename = "linkprescription";
            retValue = ye.DefinePrimaryKeys(tablename, "LinkPrescriptionID");
            tablename = "masteraccount";
            retValue = ye.DefinePrimaryKeys(tablename, "AccountID");
            tablename = "masterarea";
            retValue = ye.DefinePrimaryKeys(tablename, "AreaID");
            tablename = "masterbank";
            retValue = ye.DefinePrimaryKeys(tablename, "BankID");
            tablename = "masterbranch";
            retValue = ye.DefinePrimaryKeys(tablename, "BranchID");
            tablename = "mastercompany";
            retValue = ye.DefinePrimaryKeys(tablename, "CompID");
            tablename = "mastercustomer";
            retValue = ye.DefinePrimaryKeys(tablename, "CustomerID");
            tablename = "masterdoctor";
            retValue = ye.DefinePrimaryKeys(tablename, "DocID");
            tablename = "mastergenericcategory";
            retValue = ye.DefinePrimaryKeys(tablename, "GenericCategoryID");
            tablename = "mastergroup";
            retValue = ye.DefinePrimaryKeys(tablename, "GroupID");
            tablename = "masterhospitalpatient";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "masterorder";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "masterpack";
            retValue = ye.DefinePrimaryKeys(tablename, "PackID");
            tablename = "masterpacktype";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "masterpatient";
            retValue = ye.DefinePrimaryKeys(tablename, "PatientID");
            tablename = "masterprescription";
            retValue = ye.DefinePrimaryKeys(tablename, "prescriptionID");
            tablename = "masterproduct";
            retValue = ye.DefinePrimaryKeys(tablename, "ProductID");
            tablename = "masterproductcategory";
            retValue = ye.DefinePrimaryKeys(tablename, "ProductCategoryID");
            tablename = "mastersalesman";
            retValue = ye.DefinePrimaryKeys(tablename, "SalesmanID");
            //tablename = "masterscheduleddrug";
            //retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "masterscheme";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "mastershelf";
            retValue = ye.DefinePrimaryKeys(tablename, "ShelfID");
            //tablename = "mastervatpercent";
            //retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "masterward";
            retValue = ye.DefinePrimaryKeys(tablename, "WardID");
            tablename = "specialdetailsale";
            retValue = ye.DefinePrimaryKeys(tablename, "DetailSaleID");
            tablename = "specialvouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "tblaccountingyear";
            retValue = ye.DefinePrimaryKeys(tablename, "VoucherSeries");
            tablename = "tbldailyshortlist";
            retValue = ye.DefinePrimaryKeys(tablename, "DSLID");
            tablename = "tblfavourite";
            retValue = ye.DefinePrimaryKeys(tablename, "FavouriteId");
            tablename = "tblfixaccounts";
            retValue = ye.DefinePrimaryKeys(tablename, "AccountID");
            tablename = "tblformulae";
            retValue = ye.DefinePrimaryKeys(tablename, "FormulaName");
            //tablename = "tbllocktable";
            //retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "tblmessage";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "tbloperator";
            retValue = ye.DefinePrimaryKeys(tablename, "OperatorID");
            tablename = "tblpharmasysretailpluslic";
            retValue = ye.DefinePrimaryKeys(tablename, "PharmasysRetailPlusID");
            tablename = "tblsaleprescriptions";
            retValue = ye.DefinePrimaryKeys(tablename, "SalePrescriptionID");
            tablename = "tblscanprescriptions";
            retValue = ye.DefinePrimaryKeys(tablename, "ScanPrescriptionID");
            tablename = "tblschedule";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "tblsettings";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");

            tablename = "tblstock";
            retValue = ye.DefineUniqueKeys(tablename, "StockID");
            tablename = "tbltempstock";
            retValue = ye.DefineUniqueKeys(tablename, "TempStockID");

            tablename = "tbltrnac";
            retValue = ye.DefinePrimaryKeys(tablename, "tblTrnacID");
            tablename = "tbluser";
            retValue = ye.DefinePrimaryKeys(tablename, "UserID");
            tablename = "tbluserlevel";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "tbluserrights";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            //tablename = "tblvat";
            //retValue = ye.DefinePrimaryKeys(tablename, "DetailCashBankExpensesID");
            tablename = "tblvouchernumbers";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "tblvouchertypes";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "vouchercashbankexpenses";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "vouchercashbankpayment";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "vouchercashbankreceipt";
            retValue = ye.DefinePrimaryKeys(tablename, "CBID");
            tablename = "voucherchequereturn";
            retValue = ye.DefinePrimaryKeys(tablename, "ChequeReturnID");
            tablename = "vouchercorrectioninrate";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "vouchercreditdebitnote";
            retValue = ye.DefinePrimaryKeys(tablename, "CRDBID");
            tablename = "voucherjv";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "voucheropstock";
            retValue = ye.DefinePrimaryKeys(tablename, "MasterID");
            tablename = "voucherpurchase";
            retValue = ye.DefinePrimaryKeys(tablename, "purchaseID");
            tablename = "vouchersale";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
            tablename = "voucherstatement";
            retValue = ye.DefinePrimaryKeys(tablename, "ID");
        }

        private void CreateTables()
        {
            YearEnd ye = new YearEnd();
            tablename = "changeddetailcashbankexpenses";
            bool retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailcashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailcashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailcreditdebitnoteamount";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailcreditdebitnotestock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changeddetailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedspecialdetailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedspecialvouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvouchercashbankexpenses";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvouchercashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvouchercashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvouchercreditdebitnote";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvoucherjv";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvoucherpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "changedvouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailcashbankexpenses";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailcashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailcashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailcreditdebitnoteamount";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailcreditdebitnotestock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deleteddetailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedspecialdetailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedspecialvouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvouchercashbankexpenses";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvouchercashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvouchercashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvouchercreditdebitnote";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvoucherjv";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvoucherpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "deletedvouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailcashbankexpenses";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailcashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailcashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailchequereturn";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailcreditdebitnoteamount";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailcreditdebitnotestock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailopstock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "detailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "inity";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "linkdebtorproduct";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "linkdruggrouping";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "linkpartycompany";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "linkpatientproduct";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "linkprescription";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masteraccount";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterarea";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterbank";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterbranch";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastercompany";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastercustomer";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterdoctor";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastergenericcategory";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastergroup";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterhospitalpatient";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterorder";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterpack";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterpacktype";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterpatient";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterprescription";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterproduct";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterproductcategory";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastersalesman";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterscheduleddrug";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterscheme";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastershelf";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "mastervatpercent";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "masterward";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "specialdetailsale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "specialvouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblaccountingyear";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbldailyshortlist";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblfavourite";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblfixaccounts";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblformulae";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbllocktable";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblmessage";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbloperator";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblpharmasysretailpluslic";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblsaleprescriptions";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblscanprescriptions";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblschedule";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblsettings";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblstock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbltempstock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbltrnac";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbluser";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbluserlevel";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tbluserrights";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblvat";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblvouchernumbers";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "tblvouchertypes";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchercashbankexpenses";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchercashbankpayment";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchercashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "voucherchequereturn";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchercorrectioninrate";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchercreditdebitnote";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "voucherjv";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "voucheropstock";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "voucherpurchase";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "vouchersale";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
            tablename = "voucherstatement";
            retValue = ye.CreateTable(currentdatabase, newdatabase, tablename);
        }

        private bool CreateNewBase()
        {
            bool retValue = false;
            YearEnd ye = new YearEnd();
            retValue = ye.CreateNewBase(currentdatabase, newdatabase);
            return retValue;
        }

        private bool ConnectData()
        {
            bool retValue = false;
            try
            {
                string conectString = "SERVER={0};DATABASE={1};UID={2};PWD={3}";
                string connStrOld = ConfigurationManager.ConnectionStrings["PharmaSysRetailPlusConnectionString"].ConnectionString;
                connStrOld = LicenseLib.Common.Decrypt(connStrOld);

                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(connStrOld);
                if (builder == null)
                    return false;

                string _server = builder["server"].ToString();
                string _databasename = newdatabase;
                string _username = builder["uid"].ToString();
                string _password = builder["pwd"].ToString();
                string connStr = string.Format(conectString, _server, _databasename, _username, _password);
                DBInterface.ConnectionString = connStr;
                DBInterface.Initialize();
                if (DBInterface.IsDbConnected)
                {
                    SaveConnectionString(connStr);

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void SaveConnectionString(string connStr)
        {
            //Encrypt conenction string
            connStr = LicenseLib.Common.Encrypt(connStr);
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section != null)
            {
                section.ConnectionStrings["PharmaSysRetailPlusConnectionString"].ConnectionString = connStr;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                General.RestartService();
            }           
        }

        private void ChangeAccountingYear()
        {
            bool retValue = false;
            YearEnd ye = new YearEnd();
            retValue =  ye.RemovePreviousYear(oldvoucherseries);
            retValue =  ye.AddRowForCurrentAccountingYear(newvoucherseries,newsyear,neweyear);
        }
    }
}
