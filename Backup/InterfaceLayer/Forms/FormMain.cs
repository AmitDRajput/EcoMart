#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Xml.Xsl;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing.Printing;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.BusinessLayer;
using ControlLib;
using PharmaSYSRetailPlus.Reporting.Controls;
using PharmaSYSRetailPlus.Reporting;
#endregion

namespace PharmaSYSRetailPlus
{
    public sealed partial class MainForm : Form
    {

        #region Declaration
        UserControl _ActiveControl;
        public string _ShowYearEnd = "N";
        public OutlookBarItem _BandItem = null;
        private List<UserRights> UserRightsList;
        private bool IsCurrentYear = true;
        List<Favourite> favouriteList = null;

        FormImportAlliedSaleBill _formImportAlliedSaleBill;
        #endregion

        #region Control Declaration

        //Master - I
        UclCompany _UclCompany;
        UclProduct _UclProduct;
        UclAccount _UclAccount;
        UclDoctor _UclDoctor;
        UclGroup _UclGroup;
        UclPatient _UclPatient;
        UclPrescription _UclPrescription;
        UclHospitalPatient _UclHospitalPatient;
        //Master - II
        UclBank _UclBank;
        UclBranch _UclBranch;
        UclArea _UclArea;
        UclCustomer _UclCustomer;
        UclEmailID _UclEmailID;
        UclGenericCategory _UclGenericCategory;
        UclProdCategory _UclProdCategory;
        UclSalesman _UclSalesman;
        UclScheme _UclScheme;
        UclShelf _UclShelf;
        UclWard _UclWard;
        // Sale 
        UclCounterSale _UclCounterSale;
        UclCounterSaleEdit _UclCounterSaleEdit;
        UclPatientSale _UclPatientSale;
        UclDebtorSale _UclDebtorSale;
        UclHospitalSale _UclHospitalSale;
        UclInstitutionalSale _UclInstitutionalSale;
        UclSaleWithoutStock _UclSaleWithoutStock;
        //Purchase
        UclPurchase _UclPurchase;
        UclPurchaseWithoutStock _UclPurchaseWithoutStock;
        //Links
        UclDebtorProduct _UclDebtorProduct;
        UclDrugGrouping _UclDrugGrouping;
        UclPartyCompany _UclPartyCompany;
        UclLinkShelfProduct _UclLinkShelfProduct;
        UclLinkProductScheduleH1 _UclLinkProductScheduleH1;
        //Credit Note
        UclCreditNoteStock _UclCreditNoteStock;
        UclCreditNoteAmount _UclCreditNoteAmount;
        UclStockIn _UclStockIn;
        //Debit Note
        UclDebitNotestock _UclDebitNoteStock;
        UclDebitNoteAmount _UclDebitNoteAmount;
        UclDebitNoteExpiry _UclDebitNoteExpiry;
        UclStockOut _UclStockOut;
        //Other
        UclOPStock _UclOPStock;
        UclCorrectioninRate _UclCorrectioninRate;
        UclMessages _UclMessages;
        UclUserRights _UclUserRights;
        UclUser _UclUser;
        UclSubstitute _UclSubstitute;
        UclOperator _UclOperator;
        UclSpecialSale _UclSpecialSale;
        UclBulkPrintPartywiseSale _UclBulkPrintPartywiseSale;
        UclBarCodePrint _UclBarCodePrint;
        // Distributor
        UclDistributorSale _UclDistributorSale;
        // Tool
        UclStockReProcess _UclStockReProcess;
        //  UclToolCompanyShortName _UclToolCompanyShortName;
        UclBackupPath _UclBackupPath;
        // Year End
        UclYearEnd _UclYearEnd;

        // Settings
        UclSettingsEmail _UclSettingsEmail;

        // Cash
        UclCashReceipt _UclCashReceipt;
        UclCashPayment _UclCashPayment;
        UclCashExpenses _UclCashExpenses;
        // Bank
        UclBankReceipt _UclBankReceipt;
        UclBankPayment _UclBankPayment;
        UclChequeReturn _UclChequeReturn;
        UclBankExpenses _UclBankExpenses;
        UclDoBankReconciliation _UclDoBankReconciliation;

        // Contra
        UclContraEntry _UclContraEntry;
        // Purchase Order
        UclDailyPurchaseOrder _UclDailyPurhcaseOrder;
        UclPurchaseOrder _UclPurchaseOrder;
        UclPurchaseOrderForToday _UclPurchaseOrderForToday;
        //Settings
        UclSettingsSale _UclSettingsSale;
        UclSettingsForPrint _UclSettingsForPrint;
        //Statement
        UclStatementSale _UclStatementSale;
        UclStatementPurchase15Days _UclStatementPurchase15Days;
        UclStatementHospital _UclStatementHospital;
        UclStatementPartywiseSale _UclStatementPartywiseSale;
        UclStatementPartywisePurchase _UclStatementPartywisePurchase;


        //  UclStatementPurchase7Days _UclStatementPurchase7Days;


        #endregion

        #region Report Declaration
        // MASTER LIST
        UclListCompany _UclCompanyList;
        UclListProductAll _UclProductList;
        UclListProductBySelection _UclProductListBySelection;
        UclListAccount _UclAccountList;
        UclListDoctor _UclDoctorList;
        UclListPatient _UclPatientList;
        UclListShelf _UclShelfList;
        UclListBank _UclBankList;
        UclListBranch _UclBranchList;
        UclListArea _UclAreaList;
        UclListGenericCategory _UclGenericCategoryList;
        UclListTodaysCheques _UclListTodaysCheques;
        UclListOperator _UclListOperator;
        UclVouChequeReturnList _UclListChequeReturn;
        // VOUCHER LIST
        UclVouCashReceiptList _UclCashReceiptList;
        UclVouCashPaidList _UclCashPaidList;
        UclVouChequeReceiptList _UclChequeReceiptList;
        UclVouChequePaidList _UclChequePaidList;
        UclVouCashExpensesList _UclCashExpensesList;
        UclVouBankExpensesList _UclVouBankExpensesList;
        UclVouStatementPurchaseList _UclVouStatementPurchaseList;
        UclVouStatementSaleList _UclVouStatementSaleList;
        UclVouChequeReceivedButNotCleared _UclVouChequeReceivedButNotCleared;
        UclVouChequePaidButNotCleared _UclVouChequePaidButNotCleared;
        // CREDIT/DEBIT NOTE LIST
        UclCreditNoteList _UclCreditNoteList;
        UclCreditNoteStockInList _UclStockInList;
        UclDebitNoteList _UclDebitNoteList;
        UclDebitNoteStockOutList _UclStockOutList;
        UclCreditDebitNotePartyList _UclCreditDebitNotePartyList;
        UclCreditDebitNoteProductList _UclCreditDebitNoteProductList;
        UclCreditDebitStockINOUTListProduct _UclStockINOUTListProduct;
        UclDebitNoteListProduct _UclDebitNoteListProduct;
        UclDebitNoteStockOutListProduct _UclStockOutListProduct;
        UclCreditNoteListProduct _UclCreditNoteListProduct;
        UclCreditNoteStockInListProduct _UclStockInListProduct;

        // SCHEME LIST
        UclSchemeListAll _UclSchemeListAll;
        UclSchemeListCompanywise _UclSchemeListCompanywise;
        //UclSchemeListCompanywisePurchase _UclSchemeListCompanywisePurchase;
        UclSchemeListProductPurchase _UclSchemeListProductPurchase;
        // PURCHASE LIST
        UclPurchaseListDaily _UclPurchaseListDaily;
        UclPurchaseListAllPartySummary _UclPurchaseListAllPartySummary;
        UclPurchaseListCategory _UclPurchaseListCategory;
        UclPurchaseListCompany _UclPurchaseListCompany;
        UclPurchaseListDiscount _UclPurchaseListDiscount;
        UclPurchaseListNewProduct _UclPurchaseListNewProduct;
        UclPurchaseListPartywisebills _UclPurchaseListPartywisebills;
        UclPurchaseListProduct _UclPurchaseListProductWise;
        UclPurchaseListProductBatch _UclPurchaseListProductBatch;
        UclPurchaseListPartyProduct _UclPurchaseListPartyProduct;
        // STOCK LIST
        UclStockListCurrentStock _UclStockListCurrentStock;
        UclStockListBatchwise _UclStockListBatchwise;
        UclStockListShelf _UclStockListShelf;
        UclStockListNonMoving _UclStockListNonMoving;
        UclStockListAll _UclStockListAll;
        UclStockListStocknSale _UclStockListStocknSale;
        UclStockListProductLedger _UclStockListProductLedger;
        UclStockListCategorySummary _UclStockListCategorySummary;
        UclStockListCompanywiseSummary _UclStockListCompanywiseSummary;
        UclStockListPatient _UclStockListPatient;
        UclStockListOpeningStock _UclStockListOpeningStock;
        UclStockListOpeningStockProduct _UclStockListOpeningStockProduct;
        // SALE LIST
        UclSaleListProductBatch _UclSaleListProductBatch;
        UclSaleListProduct _UclSaleListProduct;
        UclSaleListPartySaleSummary _UclSaleListPartySaleSummary;
        UclSaleListRegularPartyProduct _UclSaleListRegularPartyProduct;
        UclSaleListSheduledDrug _UclSaleListSheduledDrug;
        UclSaleListDailySale _UclSaleListDailySale;
        UclSaleListPatient _UclSaleListPatient;
        UclSaleListDoctor _UclSaleListDoctor;
        UclSaleListCategory _UclSaleListCategory;
        UclSaleListPartywiseBills _UclSaleListPartywiseBills;
        //     UclSaleListPartyProduct _UclSaleListPartyProduct;
        UclSaleListDoctorCompany _UclSaleListDoctorCompany;
        UclSaleListProductSummary _UclSaleListDaywiseProductSummary;
        UclSaleListAdmitPatient _UclSaleListAdmitPatient;
        UclSaleListOperator _UclSaleListOperator;
        UclSaleListIPD _UclSaleListIPD;
        UclSaleListOPD _UclSaleListOPD;
        UclSaleListCreditCard _UclSaleListCreditCard;
        UclSaleListDailyProducts _UclSaleDailyProducts;
        //     UclSaleListVoucherSale  _UclSaleListVoucherSale;
        // ACCOUNT LIST
        UclAcListSalesRegister _UclAcListSalesRegister;
        UclAcListPurchaseRegister _UclAcListPurchaseRegister;
        UclAcListCashBook _UclAcListCashBook;
        UclAcListBankBook _UclAcListBankBook;
        UclAcListBankBookByClearedDate _UclAcListBankBookByClearedDate;
        UclAcListJournal _UclAcListJournal;
        UclAcListGeneralLedger _UclAcListGeneralLedger;
        UclAcListDebtorLedger _UclAcListDebtorLedger;
        UclAcListCreditorLedger _UclAcListCreditorLedger;
        UclAcListSundryDebtor _UclAcListSundryDebtor;
        UclAcListSundryCreditor _UclAcListSundryCreditor;
        UclAcListAgeing _UclAcListAgeing;
        // FINAL ACCOUNTS
        UclFACListTrialBalance _UclFACListTrialBalance;
        UclFACListEntryofScheduleNumber _UclFACListEntryofScheduleNumber;
        UclFACListProfitandLoss _UclFACListProfitandLoss;
        UclFACListPrintSchedules _UclFACListPrintSchedules;
        UclFACListBalanceSheet _UclFACListBalanceSheet;
        // VAT REPORTS
        UclVATListSalesRegister _UclVATListSalesRegister;
        UclVATListSalesRegisterOtherDetails _UclVATListSalesRegisterOtherDetails;
        UclVATListSalesRegisterDate _UclVATListSalesRegisterDate;
        UclVATListSalesRegisterMonth _UclVATListSalesRegisterMonth;
        UclVATListSalesRegisterParty _UclVATListSalesRegisterTIN;
        UclVATListPurchaseRegister _UclVATListPurchaseRegister;
        UclVATListSalesRegisterDetail _UclVATListSalesRegisterDetail;
        UclVATListPurchaseRegisterOtherDetails _UclVATListPurchaseRegisterOtherDetails;
        UclVATListPurchaseRegisterDate _UclVATListPurchaseRegisterDate;
        UclVATListPurchaseRegisterMonth _UclVATListPurchaseRegisterMonth;
        UclVATListPurchaseRegisterTIN _UclVATListPurchaseRegisterTIN;
        UclVATListPurchaseRegisterDetail _UclVATListPurchaseRegisterDetail;
        UclVATListCreditNote _UclVATListCreditNote;
        UclVATListCombine _UclVATListCombine;
        // MIS REPORTS
        UclMISListBankInterest _UclMISListBankInterest;
        UclMISListCurrentStockStatement _UclMISListCurrentStockStatement;
        UclMISListCurrentStockValue _UclMISListCurrentStockValue;
        UclMISListDailyBankClosing _UclMISListDailyBankClosing;
        UclMISListDailyCashClosing _UclMISListDailyCashClosing;
        UclMISListProfitCompany _UclMISListProfitCompany;
        UclMISListProfitDay _UclMISListProfitDay;
        UclMISListStockStatementBank _UclMISListStockStatementBank;
        UclMISListSummary _UclMISListSummary;
        UclMISListDeletedVouchers _UclMISListDeletedVouchers;
        UclMISListChangedVouchers _UclMISListChangedVouchers;
        //EXPIRY LIST
        UclExpiryListProduct _UclExpiryListProduct;
        // H1
        UclH1SaleList _UclH1SaleList;
        FormSplash splash;
        #endregion

        #region Constructor(s)
        public MainForm(FormSplash splashScreen)
        {
            this.Cursor = Cursors.WaitCursor;
            splash = splashScreen;
            splash.SetProgress("Loading Main Form...", 10);
            InitializeComponent();
            CheckCurrentYear();
            Initialize();
            this.Cursor = Cursors.Default;
        }


        private void CheckCurrentYear()
        {
            try
            {
                int mtoday = Convert.ToInt32(DateTime.Today.Date.ToString("yyyyMMdd"));
                if (mtoday >= Convert.ToInt32(General.ShopDetail.Shopsy) && mtoday <= Convert.ToInt32(General.ShopDetail.Shopey))
                    IsCurrentYear = true;
                else
                    IsCurrentYear = false;
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.CheckCurrentYear:" + ex.Message);
            }
        }

        #endregion

        #region Method(s)
        private void Initialize()
        {
            try
            {
                splash.SetProgress("Loading Rights...", 30);
                LoadRights();
                splash.SetProgress("Loading Outlook bar...", 40);
                InitializeMainOutlookBar();
                splash.SetProgress("Loading Reports...", 60);
                InitializeReports();
                InitializeTools();
                InitializeSettings();               
                InitializeYearEnd();                
                GetIfShowYearEnd();

                splash.SetProgress("Loading Modules...", 70);
                MainOutlookBar.LoadModules();

                splash.SetProgress("Loading Products...", 90);

                General.ConnectClient();

                this.Text = General.ApplicationTitle + " - " + General.ShopDetail.ShopName.Trim() + " " + General.ShopDetail.ShopAddress1.Trim() + " [" + General.ShopDetail.ShopVoucherSeries + "] ";

                DataTable dt = GetFormNameData();
                _UclUserRights.SetFormNameData(dt);
                dt = GetReportNameData();
                _UclUserRights.SetReportNameData(dt);

                lblWelcome.Text = "Welcome " + General.CurrentUser.Name;
                LoadFavourites();
                //LoadMessage();

                CreateImportAlliedSaleBillForm();

                General.FormMainInstance = this;
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.Initialize:" + ex.Message);
                MessageBox.Show("ERROR >> " + ex.Message);
            }
        }

        private void GetIfShowYearEnd()
        {
            IFShowYearEnd _IfShowYearEnd = new IFShowYearEnd();
            _ShowYearEnd = _IfShowYearEnd.GetIfShowYearEnd();
            if (_ShowYearEnd == "Y")

                tsmenuYearEnd.Visible = true;

            else
                tsmenuYearEnd.Visible = true;
        } 

        private void InitializeYearEnd()
        {
             ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                

                _UclYearEnd = new UclYearEnd();
                _UclYearEnd.Dock = DockStyle.Fill;
                _UclYearEnd.Visible = false;
                _UclYearEnd.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Year End";
                _ControlItem.ItemMode = OperationMode.View;
                _ControlItem.Control = _UclYearEnd;

                _SubSubMenuItem = new ToolStripMenuItem("Year End");
                _SubSubMenuItem.Name = "Year End";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsmenuYearEnd.DropDownItems.Add(_SubSubMenuItem);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }           
        }

        private void CreateImportAlliedSaleBillForm()
        {
            _formImportAlliedSaleBill = new FormImportAlliedSaleBill();
            _formImportAlliedSaleBill.OnNewParty += new EventHandler(ImportAlliedSaleBill_OnNewParty);
            _formImportAlliedSaleBill.OnNewProduct += new EventHandler(ImportAlliedSaleBill_OnNewProduct);
            _formImportAlliedSaleBill.OnNewPurchase += new EventHandler(ImportAlliedSaleBill_OnNewPurchase);
            _formImportAlliedSaleBill.OnCancel += new EventHandler(ImportAlliedSaleBill_OnCancel);
        }

        private void ImportAlliedSaleBill_OnCancel(object sender, EventArgs e)
        {
            _formImportAlliedSaleBill.Close();
            _formImportAlliedSaleBill = null;
            _UclPurchase = null;
        }

        private void ImportAlliedSaleBill_OnNewPurchase(object sender, EventArgs e)
        {
            //Open Purchase
            //_UclPurchase = new UclPurchase();
            //_UclPurchase.Dock = DockStyle.Fill;
            //_UclPurchase.Visible = false;
            //_UclPurchase.ExitClicked += new EventHandler(Item_ExitClicked);
            //_item.Control = _UclPurchase;


            ControlItem _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.Add;
            UclPurchase _UclPurchase = new UclPurchase();
            _UclPurchase.ExitClicked += new EventHandler(Item_ExitClicked);
            _ControlItem.Control = _UclPurchase;           
            _UclPurchase.ImportBillData = _formImportAlliedSaleBill.ImportBillData;
            OpenItem(_ControlItem);
            _formImportAlliedSaleBill.Close();
            _formImportAlliedSaleBill = null;
        }

        private void ImportAlliedSaleBill_OnNewProduct(object sender, EventArgs e)
        {
            //Open Product
            ControlItem _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Product";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclProduct;
            OpenItem(_ControlItem);
        }

        private void ImportAlliedSaleBill_OnNewParty(object sender, EventArgs e)
        {
            //Open Account
            ControlItem _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Account";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclAccount;
            OpenItem(_ControlItem);
        }

        private void OpenDefaultForm()
        {
            try
            {
                ControlItem _ControlItem = null;
                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Counter Sale";
                _ControlItem.ItemMode = OperationMode.Add;
                _ControlItem.Control = _UclCounterSale;
                OpenItem(_ControlItem);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.OpenDefaultForm:" + ex.Message);
            }
        }

        private void LoadMessage()
        {
            uclMessageView1.LoadData();
            uclMessageView1.Start();
        }

        private void LoadRights()
        {
            UserRightsList = new List<UserRights>();
            UserRights _UserRights = new UserRights();
            UserRightsList = _UserRights.GetRightList();
        }

        #endregion

        # region initializeOutLookBar

        #region Sale
        private void AddCounterSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCounterSale = new UclCounterSale();
            //_UclCounterSale.Dock = DockStyle.Fill;
            //_UclCounterSale.Visible = false;
            //_UclCounterSale.ExitClicked += new EventHandler(Item_ExitClicked);

            //_UclCounterSaleEdit = new UclCounterSaleEdit();
            //_UclCounterSaleEdit.Dock = DockStyle.Fill;
            //_UclCounterSaleEdit.Visible = false;
            //_UclCounterSaleEdit.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Counter Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCounterSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _BandItem.Visible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _Band.Items.Add(_BandItem);

            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (IsCurrentYear == false)
                isAddVisible = false;

            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Counter Sale Edit";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCounterSaleEdit;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Counter Sale Edit";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCounterSaleEdit;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Counter Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCounterSaleEdit;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPatientSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPatientSale = new UclPatientSale();
            //_UclPatientSale.Dock = DockStyle.Fill;
            //_UclPatientSale.Visible = false;
            //_UclPatientSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPatientSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPatientSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPatientSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPatientSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient Sale";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclPatientSale;

            _OperationMenuItem = new ToolStripMenuItem("Add/Remove Prescription");
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);




            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }



        private void AddDebtorSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDebtorSale = new UclDebtorSale();
            //_UclDebtorSale.Dock = DockStyle.Fill;
            //_UclDebtorSale.Visible = false;
            //_UclDebtorSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debtor Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDebtorSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debtor Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDebtorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debtor Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclDebtorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debtor Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDebtorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddHospitalSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclHospitalSale = new UclHospitalSale();
            //_UclHospitalSale.Dock = DockStyle.Fill;
            //_UclHospitalSale.Visible = false;
            //_UclHospitalSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclHospitalSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclHospitalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclHospitalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclHospitalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddInstitutionalSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclInstitutionalSale = new UclInstitutionalSale();
            //_UclInstitutionalSale.Dock = DockStyle.Fill;
            //_UclInstitutionalSale.Visible = false;
            //_UclInstitutionalSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Institutional Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclInstitutionalSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Institutional Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclInstitutionalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Institutional Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclInstitutionalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Institutional Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclInstitutionalSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddSaleWithoutStockSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclSaleWithoutStock = new UclSaleWithoutStock();
            //_UclSaleWithoutStock.Dock = DockStyle.Fill;
            //_UclSaleWithoutStock.Visible = false;
            //_UclSaleWithoutStock.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclSaleWithoutStock;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclSaleWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclSaleWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclSaleWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }
        #endregion Sale

        # region Purchase
        private void AddPurchaseItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPurchase = new UclPurchase();
            //_UclPurchase.Dock = DockStyle.Fill;
            //_UclPurchase.Visible = false;
            //_UclPurchase.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPurchase;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPurchase;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPurchase;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPurchase;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclPurchase;

            _OperationMenuItem = new ToolStripMenuItem("Type Change");
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, OperationMode.Edit);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPurchaseWithoutStockItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPurchaseWithoutStock = new UclPurchaseWithoutStock();
            //_UclPurchaseWithoutStock.Dock = DockStyle.Fill;
            //_UclPurchaseWithoutStock.Visible = false;
            //_UclPurchaseWithoutStock.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase II";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPurchaseWithoutStock;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase II";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPurchaseWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase II";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPurchaseWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase II";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPurchaseWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase II";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclPurchaseWithoutStock;

            _OperationMenuItem = new ToolStripMenuItem("Type Change");
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, OperationMode.Edit);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddDailyPurchaseOrderItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            _UclDailyPurhcaseOrder = new UclDailyPurchaseOrder();
            _UclDailyPurhcaseOrder.Dock = DockStyle.Fill;
            _UclDailyPurhcaseOrder.Visible = false;
            _UclDailyPurhcaseOrder.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PO Daily ShortList";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDailyPurhcaseOrder;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
                _BandItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
                _BandItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }

        }

        private void AddPurchaseOrderItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            _UclPurchaseOrder = new UclPurchaseOrder();
            _UclPurchaseOrder.Dock = DockStyle.Fill;
            _UclPurchaseOrder.Visible = false;
            _UclPurchaseOrder.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PurchaseOrder";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPurchaseOrder;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PurchaseOrder";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPurchaseOrder;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PurchaseOrder";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPurchaseOrder;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PurchaseOrder";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPurchaseOrder;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPurchaseOrderForTodayItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            _UclPurchaseOrderForToday = new UclPurchaseOrderForToday();
            _UclPurchaseOrderForToday.Dock = DockStyle.Fill;
            _UclPurchaseOrderForToday.Visible = false;
            _UclPurchaseOrderForToday.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PurchaseOrderForToday";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPurchaseOrderForToday;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.Control.Name, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
                _BandItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
                _BandItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }
        #endregion Purchase

        # region Debit Note

        private void AddDebitNoteStockItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDebitNoteStock = new UclDebitNotestock();
            //_UclDebitNoteStock.Dock = DockStyle.Fill;
            //_UclDebitNoteStock.Visible = false;
            //_UclDebitNoteStock.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Stock";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDebitNoteStock;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Stock";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDebitNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Stock";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclDebitNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Stock";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDebitNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Stock";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclDebitNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = "Split";
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
           
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddDebitNoteAmountItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDebitNoteAmount = new UclDebitNoteAmount();
            //_UclDebitNoteAmount.Dock = DockStyle.Fill;
            //_UclDebitNoteAmount.Visible = false;
            //_UclDebitNoteAmount.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Amount";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDebitNoteAmount;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Amount";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDebitNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Amount";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclDebitNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Amount";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDebitNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddDebitNoteExpiryItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDebitNoteExpiry = new UclDebitNoteExpiry();
            //_UclDebitNoteExpiry.Dock = DockStyle.Fill;
            //_UclDebitNoteExpiry.Visible = false;
            //_UclDebitNoteExpiry.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Debit Note Expiry";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDebitNoteExpiry;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (IsCurrentYear == false)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddStockOutItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStockOut = new UclStockOut();
            //_UclStockOut.Dock = DockStyle.Fill;
            //_UclStockOut.Visible = false;
            //_UclStockOut.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock Out";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStockOut;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock Out";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclStockOut;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock Out";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStockOut;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock Out";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStockOut;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        #endregion Debit Note

        #region Credit Note

        private void AddCreditNoteStockItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCreditNoteStock = new UclCreditNoteStock();
            //_UclCreditNoteStock.Dock = DockStyle.Fill;
            //_UclCreditNoteStock.Visible = false;
            //_UclCreditNoteStock.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Stock";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCreditNoteStock;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Stock";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCreditNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Stock";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCreditNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Stock";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCreditNoteStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCreditNoteAmountItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCreditNoteAmount = new UclCreditNoteAmount();
            //_UclCreditNoteAmount.Dock = DockStyle.Fill;
            //_UclCreditNoteAmount.Visible = false;
            //_UclCreditNoteAmount.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Amount";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCreditNoteAmount;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Amount";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCreditNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Amount";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCreditNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Credit Note Amount";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCreditNoteAmount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddStockINItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStockIn = new UclStockIn();
            //_UclStockIn.Dock = DockStyle.Fill;
            //_UclStockIn.Visible = false;
            //_UclStockIn.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock In";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStockIn;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock In";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclStockIn;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock In";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStockIn;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Stock In";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStockIn;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }
        #endregion Credit Note

        #region Cash

        private void AddCashReceiptItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCashReceipt = new UclCashReceipt();
            //_UclCashReceipt.Dock = DockStyle.Fill;
            //_UclCashReceipt.Visible = false;
            //_UclCashReceipt.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Receipt";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCashReceipt;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Receipt";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCashReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Receipt";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCashReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Receipt";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCashReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCashPaymentItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCashPayment = new UclCashPayment();
            //_UclCashPayment.Dock = DockStyle.Fill;
            //_UclCashPayment.Visible = false;
            //_UclCashPayment.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Payment";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCashPayment;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Payment";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCashPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Payment";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCashPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Payment";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCashPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCashExpensesItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCashExpenses = new UclCashExpenses();
            //_UclCashExpenses.Dock = DockStyle.Fill;
            //_UclCashExpenses.Visible = false;
            //_UclCashExpenses.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Expenses";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCashExpenses;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Expenses";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCashExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Expenses";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCashExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cash Expenses";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCashExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }




        #endregion Cash

        # region Bank

        private void AddBankReceiptItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBankReceipt = new UclBankReceipt();
            //_UclBankReceipt.Dock = DockStyle.Fill;
            //_UclBankReceipt.Visible = false;
            //_UclBankReceipt.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Receipt";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBankReceipt;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Receipt";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclBankReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Receipt";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclBankReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Receipt";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBankReceipt;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Receipt";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclBankReceipt;

            _OperationMenuItem = new ToolStripMenuItem("Voucher Date Change");
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddBankPaymentItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBankPayment = new UclBankPayment();
            //_UclBankPayment.Dock = DockStyle.Fill;
            //_UclBankPayment.Visible = false;
            //_UclBankPayment.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Payment";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBankPayment;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Payment";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclBankPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Payment";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclBankPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Payment";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBankPayment;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Payment";
            _ControlItem.ItemMode = OperationMode.Fifth;
            _ControlItem.Control = _UclBankPayment;

            _OperationMenuItem = new ToolStripMenuItem("Voucher Date Change");
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isFifthVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isFifthVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible || isFifthVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddChequeReturnItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclChequeReturn = new UclChequeReturn();
            //_UclChequeReturn.Dock = DockStyle.Fill;
            //_UclChequeReturn.Visible = false;
            //_UclChequeReturn.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Cheque Return";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclChequeReturn;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            //   if (!IsCurrentYear)
            isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Chequre Return";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclChequeReturn;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddBankExpensesItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBankExpenses = new UclBankExpenses();
            //_UclBankExpenses.Dock = DockStyle.Fill;
            //_UclBankExpenses.Visible = false;
            //_UclBankExpenses.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Expenses";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBankExpenses;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Expenses";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclBankExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Expenses";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclBankExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank Expenses";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBankExpenses;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddDoBankReconciliation(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDoBankReconciliation = new UclDoBankReconciliation();
            //_UclDoBankReconciliation.Dock = DockStyle.Fill;
            //_UclDoBankReconciliation.Visible = false;
            //_UclDoBankReconciliation.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Do Bank Reconciliation";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDoBankReconciliation;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            //   if (!IsCurrentYear)
            isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Do Bank Reconciliation";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDoBankReconciliation;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
           
            if (isAddVisible || isEditVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        #endregion Bank

        #region Contra

        private void AddContraItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclContraEntry = new UclContraEntry();
            //_UclContraEntry.Dock = DockStyle.Fill;
            //_UclContraEntry.Visible = false;
            //_UclContraEntry.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Contra Entry";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclContraEntry;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Contra Entry";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclContraEntry;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Contra Entry";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclContraEntry;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Contra Entry";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclContraEntry;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }



        #endregion Contra

        # region Other

        private void AddOpeningStockItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclOPStock = new UclOPStock();
            //_UclOPStock.Dock = DockStyle.Fill;
            //_UclOPStock.Visible = false;
            //_UclOPStock.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Opening Stock";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclOPStock;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Opening Stock";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclOPStock;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCorrectionInRateItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCorrectioninRate = new UclCorrectioninRate();
            //_UclCorrectioninRate.Dock = DockStyle.Fill;
            //_UclCorrectioninRate.Visible = false;
            //_UclCorrectioninRate.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Correction In Rate";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCorrectioninRate;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);           

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Correction In Rate";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCorrectioninRate;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddUsersItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclUser = new UclUser();
            //_UclUser.Dock = DockStyle.Fill;
            //_UclUser.Visible = false;
            //_UclUser.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Users";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclUser;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            // ss 19/2/2013
            bool isAddVisible = true;
            if (General.CurrentUser.Level <= 1)
                isAddVisible = true;
            else
                isAddVisible = false;
          
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Users";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclUser;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = true;
            if (General.CurrentUser.Level <= 1)
                isEditVisible = true;
            else
                isEditVisible = false;
          
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Users";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclUser;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = true;
            if (General.CurrentUser.Level <= 1)
                isDeleteVisible = true;
            else
                isDeleteVisible = false;
          
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Users";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclUser;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = true;
            if (General.CurrentUser.Level <= 1)
                isViewVisible = true;
            else
                isViewVisible = false;
          
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddUserRightsItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            _UclUserRights = new UclUserRights();
            _UclUserRights.Dock = DockStyle.Fill;
            _UclUserRights.Visible = false;
            _UclUserRights.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "User Rights";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclUserRights;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = true;
            if (General.CurrentUser.Level <= 1)
                isAddVisible = true;
            else
                isAddVisible = false;
         
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);           

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "User Rights";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclUserRights;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = true;
            if (General.CurrentUser.Level <= 1)
                isDeleteVisible = true;
            else
                isDeleteVisible = false;
          
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "User Rights";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclUserRights;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = true;
            if (General.CurrentUser.Level <= 1)
                isViewVisible = true;
            else
                isViewVisible = false;
          
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        //private void AddSettingsSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _OperationMenuItem = null;
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;

            //_UclSettingsSale = new UclSettingsSale();
            //_UclSettingsSale.Dock = DockStyle.Fill;
            //_UclSettingsSale.Visible = false;
            //_UclSettingsSale.ExitClicked += new EventHandler(Item_ExitClicked);

            //_ControlItem = new ControlItem();
            //_ControlItem.ItemName = "Settings";
            //_ControlItem.ItemMode = OperationMode.Add;
            //_ControlItem.Control = _UclSettingsSale;

            //_BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            //_BandItem.Name = _ControlItem.ItemName;
            //_BandItem.Tag = _ControlItem;
            //_Band.Items.Add(_BandItem);
            //_SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            //_SubSubMenuItem.Name = _ControlItem.ItemName;
            //_SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            //_OperationMenuItem = new ToolStripMenuItem("Settings");
            //_OperationMenuItem.Name = "Settings";
            //_OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            //_OperationMenuItem.Tag = _ControlItem;
            //bool isAddVisible = true;
            //if (General.CurrentUser.Level <= 1)
            //    isAddVisible = true;
            //else
            //    isAddVisible = false;
        
            //_OperationMenuItem.Visible = isAddVisible;
            //_SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
           
            //if (isAddVisible)
            //{
            //    _SubSubMenuItem.Visible = true;
            //}
            //else
            //{
            //    _SubSubMenuItem.Visible = false;
            //}
            //if (isAddVisible)
            //{
            //    _BandItem.Visible = true;
            //}
            //else
            //{
            //    _BandItem.Visible = false;
            //}
        //}

        //private void AddSettingsForPrintItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _OperationMenuItem = null;
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;

        //    //_UclSettingsForPrint = new UclSettingsForPrint();
        //    //_UclSettingsForPrint.Dock = DockStyle.Fill;
        //    //_UclSettingsForPrint.Visible = false;
        //    //_UclSettingsForPrint.ExitClicked += new EventHandler(Item_ExitClicked);

        //    _ControlItem = new ControlItem();
        //    _ControlItem.ItemName = "Print Settings";
        //    _ControlItem.ItemMode = OperationMode.Add;
        //    _ControlItem.Control = _UclSettingsForPrint;

        //    _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
        //    _BandItem.Name = _ControlItem.ItemName;
        //    _BandItem.Tag = _ControlItem;
        //    _Band.Items.Add(_BandItem);
        //    _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
        //    _SubSubMenuItem.Name = _ControlItem.ItemName;
        //    _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

        //    _OperationMenuItem = new ToolStripMenuItem("Print Settings");
        //    _OperationMenuItem.Name = "Print Settings";
        //    _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
        //    _OperationMenuItem.Tag = _ControlItem;
        //    bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
        //    _OperationMenuItem.Visible = isAddVisible;
        //    _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
           
        //    if (isAddVisible)
        //    {
        //        _SubSubMenuItem.Visible = true;
        //    }
        //    else
        //    {
        //        _SubSubMenuItem.Visible = false;
        //    }
        //    if (isAddVisible)
        //    {
        //        _BandItem.Visible = true;
        //    }
        //    else
        //    {
        //        _BandItem.Visible = false;
        //    }
        //}


        private void AddSubstituteItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclSubstitute = new UclSubstitute();
          
            //_UclSubstitute.Visible = false;
         

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Similar Products";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclSubstitute;
          
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = true;
            if (General.CurrentUser.Level <= 1)
                isAddVisible = true;
            else
                isAddVisible = false;
            if (!IsCurrentYear)
                isAddVisible = false;
         
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

          
            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }           
        }

        private void AddOperatorItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclOperator = new UclOperator();
            //_UclOperator.Dock = DockStyle.Fill;
            //_UclOperator.Visible = false;
            //_UclOperator.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Operator";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclOperator;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            // ss 08/3/2013
            bool isAddVisible = true;
            if (General.CurrentUser.Level <= 1)
                isAddVisible = true;
            else
                isAddVisible = false;
            if (!IsCurrentYear)
                isAddVisible = false;
          
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Operator";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclOperator;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = true;
            if (General.CurrentUser.Level <= 1)
                isEditVisible = true;
            else
                isEditVisible = false;
          
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Operator";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclOperator;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = true;
            if (General.CurrentUser.Level <= 1)
                isDeleteVisible = true;
            else
                isDeleteVisible = false;
         
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Operator";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclOperator;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = true;
            if (General.CurrentUser.Level <= 1)
                isViewVisible = true;
            else
                isViewVisible = false;
           
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddSpecialSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclSpecialSale = new UclSpecialSale();
            //_UclSpecialSale.Dock = DockStyle.Fill;
            //_UclSpecialSale.Visible = false;
            //_UclSpecialSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bill Reprint";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclSpecialSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bill Reprint";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclSpecialSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bill Reprint";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclSpecialSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bill Reprint";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclSpecialSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddBulkSaleBillPrint(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBulkPrintPartywiseSale = new UclBulkPrintPartywiseSale();
            //_UclBulkPrintPartywiseSale.Dock = DockStyle.Fill;
            //_UclBulkPrintPartywiseSale.Visible = false;
            //_UclBulkPrintPartywiseSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bulk Bill Reprint";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBulkPrintPartywiseSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);


            bool isViewVisible = true;
            if (isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }

        }
        private void AddBarCodePrintItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBarCodePrint = new UclBarCodePrint();
            //_UclBarCodePrint.Dock = DockStyle.Fill;
            //_UclBarCodePrint.Visible = false;
            //_UclBarCodePrint.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "BarCode Print";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBarCodePrint;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem("Print BarCode");
            _OperationMenuItem.Name = "Print BarCode";
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);






            if (isAddVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        #endregion

        #region Statements

        private void AddSaleStatementItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStatementSale = new UclStatementSale();
            //_UclStatementSale.Dock = DockStyle.Fill;
            //_UclStatementSale.Visible = false;
            //_UclStatementSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale ALL";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStatementSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale ALL";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStatementSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale ALL";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStatementSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPurchaseStatement15DaysItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStatementPurchase15Days = new UclStatementPurchase15Days();
            //_UclStatementPurchase15Days.Dock = DockStyle.Fill;
            //_UclStatementPurchase15Days.Visible = false;
            //_UclStatementPurchase15Days.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase (15 Days)";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStatementPurchase15Days;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase (15 Days)";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStatementPurchase15Days;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase (15 Days)";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStatementPurchase15Days;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddStatementHospitalItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStatementHospital = new UclStatementHospital();
            //_UclStatementHospital.Dock = DockStyle.Fill;
            //_UclStatementHospital.Visible = false;
            //_UclStatementHospital.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Statement";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStatementHospital;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Statement";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStatementHospital;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Statement";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStatementHospital;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddSaleStatementPartywiseItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStatementPartywiseSale = new UclStatementPartywiseSale();
            //_UclStatementPartywiseSale.Dock = DockStyle.Fill;
            //_UclStatementPartywiseSale.Visible = false;
            //_UclStatementPartywiseSale.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale (Party)";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStatementPartywiseSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale Party";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStatementPartywiseSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Sale Party";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStatementPartywiseSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddPurchaseStatementPartywiseItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclStatementPartywisePurchase = new UclStatementPartywisePurchase();
            //_UclStatementPartywisePurchase.Dock = DockStyle.Fill;
            //_UclStatementPartywisePurchase.Visible = false;
            //_UclStatementPartywisePurchase.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase (Party)";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclStatementPartywisePurchase;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase Party";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclStatementPartywisePurchase;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Purchase Party";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclStatementPartywisePurchase;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        #endregion Statements

        # region Master - 1
        private void AddAccountItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclAccount = new UclAccount();
            //_UclAccount.Dock = DockStyle.Fill;
            //_UclAccount.Visible = false;
            //_UclAccount.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Account";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclAccount;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Account";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclAccount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Account";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclAccount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Account";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclAccount;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCompanyItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCompany = new UclCompany();
            //_UclCompany.Dock = DockStyle.Fill;
            //_UclCompany.Visible = false;
            //_UclCompany.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Company";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCompany;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Company";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCompany;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Company";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCompany;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Company";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCompany;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddDoctorItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDoctor = new UclDoctor();
            //_UclDoctor.Dock = DockStyle.Fill;
            //_UclDoctor.Visible = false;
            //_UclDoctor.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Doctor";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDoctor;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Doctor";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDoctor;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Doctor";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclDoctor;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Doctor";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDoctor;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddGroupItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclGroup = new UclGroup();
            //_UclGroup.Dock = DockStyle.Fill;
            //_UclGroup.Visible = false;
            //_UclGroup.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "A/c Group";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclGroup;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (IsCurrentYear == false)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "A/c Group";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclGroup;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "A/c Group";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclGroup;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "A/c Group";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclGroup;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPatientItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPatient = new UclPatient();
            //_UclPatient.Dock = DockStyle.Fill;
            //_UclPatient.Visible = false;
            //_UclPatient.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPatient;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Patient";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPrescriptionItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPrescription = new UclPrescription();
            //_UclPrescription.Dock = DockStyle.Fill;
            //_UclPrescription.Visible = false;
            //_UclPrescription.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Prescription";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPrescription;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Prescription";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclPrescription;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Prescription";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclPrescription;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Prescription";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPrescription;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddProductItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclProduct = new UclProduct();
            //_UclProduct.Dock = DockStyle.Fill;
            //_UclProduct.Visible = false;
            //_UclProduct.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Product";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclProduct;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Product";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclProduct;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Product";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclProduct;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Product";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclProduct;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddSchemeItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclScheme = new UclScheme();
            //_UclScheme.Dock = DockStyle.Fill;
            //_UclScheme.Visible = false;
            //_UclScheme.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Scheme";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclScheme;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Scheme";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclScheme;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Scheme";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclScheme;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddShelfItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclShelf = new UclShelf();
            //_UclShelf.Dock = DockStyle.Fill;
            //_UclShelf.Visible = false;
            //_UclShelf.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Shelf";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclShelf;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Shelf";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclShelf;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Shelf";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclShelf;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Shelf";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclShelf;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddHospitalPatientItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclHospitalPatient = new UclHospitalPatient();
            //_UclHospitalPatient.Dock = DockStyle.Fill;
            //_UclHospitalPatient.Visible = false;
            //_UclHospitalPatient.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Patient";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclHospitalPatient;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Patient";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclHospitalPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Patient";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclHospitalPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Hospital Patient";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclHospitalPatient;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }
        #endregion Master - 1

        # region Master - 2
        private void AddAreaItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclArea = new UclArea();
            //_UclArea.Dock = DockStyle.Fill;
            //_UclArea.Visible = false;
            //_UclArea.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Area";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclArea;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Area";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclArea;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Area";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclArea;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Area";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclArea;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddBankItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBank = new UclBank();
            //_UclBank.Dock = DockStyle.Fill;
            //_UclBank.Visible = false;
            //_UclBank.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBank;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclBank;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclBank;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Bank";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBank;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddBranchItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclBranch = new UclBranch();
            //_UclBranch.Dock = DockStyle.Fill;
            //_UclBranch.Visible = false;
            //_UclBranch.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Branch";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclBranch;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Branch";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclBranch;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Branch";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclBranch;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Branch";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclBranch;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddCustomerItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCustomer = new UclCustomer();
            //_UclCustomer.Dock = DockStyle.Fill;
            //_UclCustomer.Visible = false;
            //_UclCustomer.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Customer";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclCustomer;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Customer";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclCustomer;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Customer";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclCustomer;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Customer";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclCustomer;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddEmailIDItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclCustomer = new UclCustomer();
            //_UclCustomer.Dock = DockStyle.Fill;
            //_UclCustomer.Visible = false;
            //_UclCustomer.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "EmailID";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclEmailID;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "EmailID";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclEmailID;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "EmailID";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclEmailID;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "EmailID";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclEmailID;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        private void AddGenericItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclGenericCategory = new UclGenericCategory();
            //_UclGenericCategory.Dock = DockStyle.Fill;
            //_UclGenericCategory.Visible = false;
            //_UclGenericCategory.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Generic Name";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclGenericCategory;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Generic Name";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclGenericCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Generic Name";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclGenericCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Generic Name";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclGenericCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddMessagesItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclMessages = new UclMessages();
            //_UclMessages.Dock = DockStyle.Fill;
            //_UclMessages.Visible = false;
            //_UclMessages.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Messages";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclMessages;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (IsCurrentYear == false)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Messages";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclMessages;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Messages";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclMessages;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Messages";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclMessages;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddProductCategoryItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclProdCategory = new UclProdCategory();
            //_UclProdCategory.Dock = DockStyle.Fill;
            //_UclProdCategory.Visible = false;
            //_UclProdCategory.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductCategory";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclProdCategory;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductCategory";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclProdCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductCategory";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclProdCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductCategory";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclProdCategory;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddSalesmanItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclSalesman = new UclSalesman();
            //_UclSalesman.Dock = DockStyle.Fill;
            //_UclSalesman.Visible = false;
            //_UclSalesman.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Salesman";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclSalesman;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Salesman";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclSalesman;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Salesman";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclSalesman;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Salesman";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclSalesman;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddWardItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclWard = new UclWard();
            //_UclWard.Dock = DockStyle.Fill;
            //_UclWard.Visible = false;
            //_UclWard.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Ward";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclWard;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Ward";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclWard;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Ward";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclWard;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Ward";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclWard;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }


        #endregion Master - 2

        # region Links


        private void AddDebtorProductItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDebtorProduct = new UclDebtorProduct();
            //_UclDebtorProduct.Dock = DockStyle.Fill;
            //_UclDebtorProduct.Visible = false;
            //_UclDebtorProduct.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "DebtorProduct";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDebtorProduct;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "DebtorProduct";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDebtorProduct;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }



        private void AddDrugGroupingItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclDrugGrouping = new UclDrugGrouping();
            //_UclDrugGrouping.Dock = DockStyle.Fill;
            //_UclDrugGrouping.Visible = false;
            //_UclDrugGrouping.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "DrugGrouping (Generic)";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDrugGrouping;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "DrugGrouping (Generic)";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDrugGrouping;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddPartyCompanyItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclPartyCompany = new UclPartyCompany();
            //_UclPartyCompany.Dock = DockStyle.Fill;
            //_UclPartyCompany.Visible = false;
            //_UclPartyCompany.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PartyCompany";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclPartyCompany;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "PartyCompany";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclPartyCompany;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddShelfProductItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclLinkShelfProduct = new UclLinkShelfProduct();
            //_UclLinkShelfProduct.Dock = DockStyle.Fill;
            //_UclLinkShelfProduct.Visible = false;
            //_UclLinkShelfProduct.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ShelfProduct";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclLinkShelfProduct;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ShelfProduct";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclLinkShelfProduct;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        private void AddProductScheduleH1Item(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

            //_UclLinkProductScheduleH1 = new UclLinkProductScheduleH1();
            //_UclLinkProductScheduleH1.Dock = DockStyle.Fill;
            //_UclLinkProductScheduleH1.Visible = false;
            //_UclLinkProductScheduleH1.ExitClicked += new EventHandler(Item_ExitClicked);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductScheduleH1";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclLinkProductScheduleH1;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "ProductScheduleH1";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclLinkProductScheduleH1;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }
        }

        #endregion Links

        # region DataEntryforms
        private void InitializeMainOutlookBar()
        {
            try
            {
                OutlookBarBand _Band = null;
                ToolStripMenuItem _SubMenuItem = null;

                MainOutlookBar.Bands.Clear();
                tsmenuModules.DropDownItems.Clear();

                _Band = new OutlookBarBand("Sale");
                _Band.Name = "Sale";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Sale");
                _SubMenuItem.Name = "Sale";

                AddCounterSaleItem(_Band, _SubMenuItem);
                AddPatientSaleItem(_Band, _SubMenuItem);
                AddDebtorSaleItem(_Band, _SubMenuItem);
                AddHospitalSaleItem(_Band, _SubMenuItem);
                AddInstitutionalSaleItem(_Band, _SubMenuItem);
                AddSaleWithoutStockSaleItem(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Purchase");
                _Band.Name = "Purchase";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Purchase");
                _SubMenuItem.Name = "Purchase";

                AddPurchaseItem(_Band, _SubMenuItem);
                AddPurchaseWithoutStockItem(_Band, _SubMenuItem);

                bool showBand = false;
                foreach (OutlookBarItem item in _Band.Items)
                {
                    if (item.Visible)
                        showBand = true;
                }
                _Band.Visible = showBand;

                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);


                _Band = new OutlookBarBand("Purchase Order");
                _Band.Name = "PurchaseOrder";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("PurchaseOrder");
                _SubMenuItem.Name = "PurchaseOrder";

                AddDailyPurchaseOrderItem(_Band, _SubMenuItem);
                AddPurchaseOrderItem(_Band, _SubMenuItem);
                AddPurchaseOrderForTodayItem(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Debit Note");
                _Band.Name = "Debit Note";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Debit Note");
                _SubMenuItem.Name = "Debit Note";

                AddDebitNoteStockItem(_Band, _SubMenuItem);
                AddDebitNoteAmountItem(_Band, _SubMenuItem);
                AddDebitNoteExpiryItem(_Band, _SubMenuItem);
                AddStockOutItem(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Credit Note");
                _Band.Name = "CreditNote";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Credit Note");
                _SubMenuItem.Name = "CreditNote";

                AddCreditNoteStockItem(_Band, _SubMenuItem);
                AddCreditNoteAmountItem(_Band, _SubMenuItem);
                AddStockINItem(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Cash");
                _Band.Name = "Cash";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Cash");
                _SubMenuItem.Name = "Cash";

                AddCashReceiptItem(_Band, _SubMenuItem);
                AddCashPaymentItem(_Band, _SubMenuItem);
                AddCashExpensesItem(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Bank");
                _Band.Name = "Bank";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Bank");
                _SubMenuItem.Name = "Bank";

                AddBankReceiptItem(_Band, _SubMenuItem);
                AddBankPaymentItem(_Band, _SubMenuItem);
                AddChequeReturnItem(_Band, _SubMenuItem);
                AddBankExpensesItem(_Band, _SubMenuItem);
                AddDoBankReconciliation(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);


                _Band = new OutlookBarBand("Contra");
                _Band.Name = "Contra";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Contra");
                _SubMenuItem.Name = "Contra";


                AddContraItem(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Other");
                _Band.Name = "Other";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Other");
                _SubMenuItem.Name = "Other";

                AddOpeningStockItem(_Band, _SubMenuItem);
                AddCorrectionInRateItem(_Band, _SubMenuItem);
                AddUsersItem(_Band, _SubMenuItem);
                AddUserRightsItem(_Band, _SubMenuItem);
                //AddSettingsSaleItem(_Band, _SubMenuItem);
              //  AddSettingsForPrintItem(_Band, _SubMenuItem);
                AddSubstituteItem(_Band, _SubMenuItem);
                AddOperatorItem(_Band, _SubMenuItem);
                AddSpecialSaleItem(_Band, _SubMenuItem);
                AddBulkSaleBillPrint(_Band, _SubMenuItem);
                AddBarCodePrintItem(_Band, _SubMenuItem);
                //  AddStockReProcessItem(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);


                _Band = new OutlookBarBand("Statements");
                _Band.Name = "Statements";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Statements");
                _SubMenuItem.Name = "Statements";

                AddSaleStatementItem(_Band, _SubMenuItem);
                AddPurchaseStatement15DaysItem(_Band, _SubMenuItem);
                //  AddPurchaseStatement7DaysItem(_Band, _SubMenuItem);
                AddStatementHospitalItem(_Band, _SubMenuItem);
                AddSaleStatementPartywiseItem(_Band, _SubMenuItem);
                AddPurchaseStatementPartywiseItem(_Band, _SubMenuItem);

                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Masters-1");
                _Band.Name = "Masters-1";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Masters-1");
                _SubMenuItem.Name = "Masters-1";

                AddAccountItem(_Band, _SubMenuItem);
                AddCompanyItem(_Band, _SubMenuItem);
                AddDoctorItem(_Band, _SubMenuItem);
                AddGroupItem(_Band, _SubMenuItem);
                AddPatientItem(_Band, _SubMenuItem);
                AddPrescriptionItem(_Band, _SubMenuItem);
                AddProductItem(_Band, _SubMenuItem);
                AddSchemeItem(_Band, _SubMenuItem);
                AddShelfItem(_Band, _SubMenuItem);
                AddHospitalPatientItem(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);


                _Band = new OutlookBarBand("Masters-2");
                _Band.Name = "Masters-2";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Masters-2");
                _SubMenuItem.Name = "Masters-2";

                AddAreaItem(_Band, _SubMenuItem);
                AddBankItem(_Band, _SubMenuItem);
                AddBranchItem(_Band, _SubMenuItem);
                AddCustomerItem(_Band, _SubMenuItem);
                AddEmailIDItem(_Band, _SubMenuItem);
                AddGenericItem(_Band, _SubMenuItem);
                AddMessagesItem(_Band, _SubMenuItem);
                AddProductCategoryItem(_Band, _SubMenuItem);
                AddSalesmanItem(_Band, _SubMenuItem);
                AddWardItem(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);

                _Band = new OutlookBarBand("Links");
                _Band.Name = "Links";
                _Band.SmallImageList = IMLIcons16;
                _Band.LargeImageList = IMLIcons32;
                _Band.Background = Color.White;
                _SubMenuItem = new ToolStripMenuItem("Links");
                _SubMenuItem.Name = "Links";

                AddDebtorProductItem(_Band, _SubMenuItem);
                AddDrugGroupingItem(_Band, _SubMenuItem);
                AddPartyCompanyItem(_Band, _SubMenuItem);
                AddShelfProductItem(_Band, _SubMenuItem);
                AddProductScheduleH1Item(_Band, _SubMenuItem);
                if (_Band.Items.Count == 0)
                {
                    _Band.Visible = false;
                    _SubMenuItem.Visible = false;
                }
                MainOutlookBar.Bands.Add(_Band);
                tsmenuModules.DropDownItems.Add(_SubMenuItem);
//Distributor
                if (General.CurrentSetting.MsetSaleAllowDistributorSale == "Y")
                {

                    _Band = new OutlookBarBand("Distributor");
                    _Band.Name = "Distributor";
                    _Band.SmallImageList = IMLIcons16;
                    _Band.LargeImageList = IMLIcons32;
                    _Band.Background = Color.White;
                    _SubMenuItem = new ToolStripMenuItem("Distributor");
                    _SubMenuItem.Name = "Distributor";

                    AddDistributorSaleItem(_Band, _SubMenuItem);
                    //AddCreditNoteAmountItem(_Band, _SubMenuItem);
                    //AddStockINItem(_Band, _SubMenuItem);
                    if (_Band.Items.Count == 0)
                    {
                        _Band.Visible = false;
                        _SubMenuItem.Visible = false;
                    }
                    MainOutlookBar.Bands.Add(_Band);
                    tsmenuModules.DropDownItems.Add(_SubMenuItem);
                }
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        // Distributor

        private void AddDistributorSaleItem(OutlookBarBand _Band, ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _OperationMenuItem = null;
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;

           

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Distributor Sale";
            _ControlItem.ItemMode = OperationMode.Add;
            _ControlItem.Control = _UclDistributorSale;

            _BandItem = new OutlookBarItem(_ControlItem.ItemName, 9);
            _BandItem.Name = _ControlItem.ItemName;
            _BandItem.Tag = _ControlItem;
            _Band.Items.Add(_BandItem);
            _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
            _SubSubMenuItem.Name = _ControlItem.ItemName;
            _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isAddVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            if (!IsCurrentYear)
                isAddVisible = false;
            _OperationMenuItem.Visible = isAddVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Distributor Sale";
            _ControlItem.ItemMode = OperationMode.Edit;
            _ControlItem.Control = _UclDistributorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isEditVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isEditVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Distributor Sale";
            _ControlItem.ItemMode = OperationMode.Delete;
            _ControlItem.Control = _UclDistributorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isDeleteVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isDeleteVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);

            _ControlItem = new ControlItem();
            _ControlItem.ItemName = "Distributor Sale";
            _ControlItem.ItemMode = OperationMode.View;
            _ControlItem.Control = _UclDistributorSale;

            _OperationMenuItem = new ToolStripMenuItem(_ControlItem.ItemMode.ToString());
            _OperationMenuItem.Name = _ControlItem.ItemMode.ToString();
            _OperationMenuItem.Click += new EventHandler(this.menuItem_Click);
            _OperationMenuItem.Tag = _ControlItem;
            bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
            _OperationMenuItem.Visible = isViewVisible;
            _SubSubMenuItem.DropDownItems.Add(_OperationMenuItem);
            if (isAddVisible || isEditVisible || isDeleteVisible || isViewVisible)
            {
                _SubSubMenuItem.Visible = true;
            }
            else
            {
                _SubSubMenuItem.Visible = false;
            }
            if (isAddVisible)
            {
                _BandItem.Visible = true;
            }
            else
            {
                _BandItem.Visible = false;
            }

        }

        #endregion

        #endregion initializeOutLookBar

        #region initialize Reports

        private void InitializeReports()
        {
            //  OutlookBarBand _Band = null;
            ToolStripMenuItem _SubMenuItem = null;
            try
            {
                tsmenuReports.DropDownItems.Clear();

                #region Reports

                # region List

                _SubMenuItem = new ToolStripMenuItem("List");
                _SubMenuItem.Name = "List";

                AddCompanyListReportItem(_SubMenuItem);
                AddProductListAllReportItem(_SubMenuItem);
                AddProductListBySelectionReportItem(_SubMenuItem);
                AddAccountListReportItem(_SubMenuItem);
                AddDoctorListReportItem(_SubMenuItem);
                AddPatientListReportItem(_SubMenuItem);
                AddShelfListReportItem(_SubMenuItem);
                AddBankListReportItem(_SubMenuItem);
                AddBranchListReportItem(_SubMenuItem);
                AddAreaListReportItem(_SubMenuItem);
                AddGenericCategoryListReportItem(_SubMenuItem);
                AddTodaysChequesListReportItem(_SubMenuItem);
                AddOperatorListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                # endregion List

                # region VoucherList
                _SubMenuItem = new ToolStripMenuItem("Voucher List");
                _SubMenuItem.Name = "Voucher List";

                AddCashReceiptReportItem(_SubMenuItem);
                AddCashPaymentReportItem(_SubMenuItem);
                AddCashExpensesReportItem(_SubMenuItem);
                AddChequeReceiptReportItem(_SubMenuItem);
                AddChequePaidReportItem(_SubMenuItem);
                AddBankExpensesReportItem(_SubMenuItem);
                AddChequeReturnListReportItem(_SubMenuItem);
                AddStatementPurchaseReportItem(_SubMenuItem);
                AddStatementSaleReportItem(_SubMenuItem);
                AddChequeReceivedButNotClearedReportItem(_SubMenuItem);
                AddChequePaidButNotClearedReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                # endregion VoucherList

                # region DebitCreditNote
                _SubMenuItem = new ToolStripMenuItem("DB/CR Note");
                _SubMenuItem.Name = "DBCR NOTE";

                AddDebitNoteListReportItem(_SubMenuItem);
                AddDebitNoteProductListReportItem(_SubMenuItem);
                AddStockOutListReportItem(_SubMenuItem);
                AddStockOutProductListReportItem(_SubMenuItem);
                AddCreditNoteListReportItem(_SubMenuItem);
                AddCreditNoteProductListReportItem(_SubMenuItem);
                AddStockInListReportItem(_SubMenuItem);
                AddStockInProductListReportItem(_SubMenuItem);
                AddCreditDebitNotePartyListReportItem(_SubMenuItem);
                AddCreditDebitNoteProductListReportItem(_SubMenuItem);
                //     AddStockInOutProductListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion DebitCreditNote

                # region Expiry
                _SubMenuItem = new ToolStripMenuItem("Expiry");
                _SubMenuItem.Name = "Expiry";

                AddExpiryListProductReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Expiry

                # region Scheme
                _SubMenuItem = new ToolStripMenuItem("Scheme");
                _SubMenuItem.Name = "Scheme";

                AddSchemeAllListReportItem(_SubMenuItem);
                AddSchemeCompanyListReportItem(_SubMenuItem);
                AddSchemeProductPurchaseListReportItem(_SubMenuItem);
                //    AddSchemeCompanyPurchaseListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Scheme

                # region Purchase
                _SubMenuItem = new ToolStripMenuItem("Purchase");
                _SubMenuItem.Name = "Purchase";

                AddPurchaseDailyListReportItem(_SubMenuItem);
                AddPurchaseProductListReportItem(_SubMenuItem);
                AddPurchaseProductBatchListReportItem(_SubMenuItem);
                AddPurchaseNewProductListReportItem(_SubMenuItem);
                AddPurchasePartyProductListReportItem(_SubMenuItem);
                AddPurchasePartyBillsListReportItem(_SubMenuItem);
                AddPurchaseDiscountListReportItem(_SubMenuItem);
                AddPurchaseAllPartySummaryListReportItem(_SubMenuItem);
                AddPurchaseCategoryListReportItem(_SubMenuItem);
                AddPurchaseCompanyListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Purchase

                # region Sale
                _SubMenuItem = new ToolStripMenuItem("Sale");
                _SubMenuItem.Name = "Sale";

                AddSaleDailyListReportItem(_SubMenuItem);
                AddSaleProductBatchListReportItem(_SubMenuItem);
                AddSaleProductListReportItem(_SubMenuItem);
                AddSalePartySummaryListReportItem(_SubMenuItem);
                AddSaleRegularPartyProductListReportItem(_SubMenuItem);
                AddSaleScheduledDrugListReportItem(_SubMenuItem);
                AddSalePatientListReportItem(_SubMenuItem);
                AddSaleDoctorListReportItem(_SubMenuItem);
                AddSaleCategoryListReportItem(_SubMenuItem);
                //AddSalePartyProductListReportItem(_SubMenuItem);
                AddSaleDoctorCompanyListReportItem(_SubMenuItem);
                AddSaleDaywiseProductSummaryListReportItem(_SubMenuItem);
                AddSaleOperatorListReportItem(_SubMenuItem);
                AddSaleDailyProductsReportItem(_SubMenuItem);
                AddSalePartywiseBillsListReportItem(_SubMenuItem);
                // Hospital report        AddSaleAdmitPatientListReportItem(_SubMenuItem);
                // Hospital report        AddSaleIPDListReportItem(_SubMenuItem);
                // Hospital report        AddSaleOPDListReportItem(_SubMenuItem);
                // Not Required        AddSaleCreditCardListReportItem(_SubMenuItem);
                // Not Required    AddSaleVoucherSaleListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Sale

                # region Stock
                _SubMenuItem = new ToolStripMenuItem("Stock");
                _SubMenuItem.Name = "Stock";

                AddStockCurrentListReportItem(_SubMenuItem);
                AddStockCurrentBatchListReportItem(_SubMenuItem);
                AddStockCurrentShelfListReportItem(_SubMenuItem);
                AddStockNonMovingListReportItem(_SubMenuItem);
                AddStockAllListReportItem(_SubMenuItem);
                AddStockStocknsaleListReportItem(_SubMenuItem);
                AddStockProductLedgerListReportItem(_SubMenuItem);
                AddStockCategorySummaryListReportItem(_SubMenuItem);
                AddStockCompanySummaryListReportItem(_SubMenuItem);
                AddStockPatientListReportItem(_SubMenuItem);
                //  AddStockOpeningStockListReportItem(_SubMenuItem);
                //  AddStockOpeningStockProductListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Stock

                # region Account
                _SubMenuItem = new ToolStripMenuItem("Account");
                _SubMenuItem.Name = "Account";

                AddAcSalesRegisterListReportItem(_SubMenuItem);
                AddAcPurchaseRegisterListReportItem(_SubMenuItem);
                AddAcCashBookListReportItem(_SubMenuItem);
                AddAcBankBookListReportItem(_SubMenuItem);
                AddAcBankBookByClearedDateListReportItem(_SubMenuItem);
                AddAcJournalListReportItem(_SubMenuItem);
                AddAcGeneralLedgerListReportItem(_SubMenuItem);
                AddAcDebtorLedgerListReportItem(_SubMenuItem);
                AddAcCreditorLedgerListReportItem(_SubMenuItem);
                AddAcSundryDebtorListReportItem(_SubMenuItem);
                AddAcSundryCreditorListReportItem(_SubMenuItem);
                AddAcAgeingListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Account

                # region Final Accounts
                _SubMenuItem = new ToolStripMenuItem("Final Accounts");
                _SubMenuItem.Name = "FinalAccounts";

                AddFAcTrialBalanceListReportItem(_SubMenuItem);
                AddFAcEntryOfScheduledNumbersListReportItem(_SubMenuItem);
                AddFAcProfitnLossListReportItem(_SubMenuItem);
                AddFAcPrintSchedulesListReportItem(_SubMenuItem);
                AddFAcBalanceSheetListReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion Final Accounts

                # region VAT
                _SubMenuItem = new ToolStripMenuItem("VAT");
                _SubMenuItem.Name = "VAT";

                AddVATSaleRegisterListReportItem(_SubMenuItem);
                AddVATSaleRegisterOtherDetailsListReportItem(_SubMenuItem);
                AddVATSaleRegisterDateListReportItem(_SubMenuItem);
                AddVATSaleRegisterMonthListReportItem(_SubMenuItem);
                AddVATSaleRegisterTINListReportItem(_SubMenuItem);
                AddVATSaleRegisterDetailListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterOtherDetailsListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterDateListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterMonthListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterTINListReportItem(_SubMenuItem);
                AddVATPurchaseRegisterDetailReportItem(_SubMenuItem);
                AddVATCreditNoteRegisterDetailReportItem(_SubMenuItem);
                AddVATListCombineReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion VAT

                # region MIS
                _SubMenuItem = new ToolStripMenuItem("MIS");
                _SubMenuItem.Name = "MIS";

                AddMISProfitDayListReportItem(_SubMenuItem);
                AddMISProfitCompanyListReportItem(_SubMenuItem);
                AddMISDailyCashClosingListReportItem(_SubMenuItem);
                AddMISDailyBankClosingListReportItem(_SubMenuItem);
                //  AddMISClosingStockValueListReportItem(_SubMenuItem);
                //   AddMISClosingStockStatementListReportItem(_SubMenuItem);
                //   AddMISStockStatementBankListReportItem(_SubMenuItem);
                AddMISBankInterestListReportItem(_SubMenuItem);
                AddMISSummaryListReportItem(_SubMenuItem);
                AddMISDeletedVouchersReportItem(_SubMenuItem);
                AddMISChangedVouchersReportItem(_SubMenuItem);

                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion MIS

                # region H1
                _SubMenuItem = new ToolStripMenuItem("H1");
                _SubMenuItem.Name = "H1";

                AddH1SaleListReportItem(_SubMenuItem);
                tsmenuReports.DropDownItems.Add(_SubMenuItem);
                #endregion H1

                # endregion reports

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #region Report Items

        #region  Report Items For List

        private void AddCompanyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCompanyList = new UclListCompany();
                //_UclCompanyList.Dock = DockStyle.Fill;
                //_UclCompanyList.Visible = false;
                //_UclCompanyList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "CompanyList";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCompanyList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddProductListAllReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclProductList = new UclListProductAll();
                //_UclProductList.Dock = DockStyle.Fill;
                //_UclProductList.Visible = false;
                //_UclProductList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "ProductList (ALL)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclProductList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddProductListBySelectionReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclProductListBySelection = new UclListProductBySelection();
                //_UclProductListBySelection.Dock = DockStyle.Fill;
                //_UclProductListBySelection.Visible = false;
                //_UclProductListBySelection.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Product List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclProductListBySelection;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAccountListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAccountList = new UclListAccount();
                //_UclAccountList.Dock = DockStyle.Fill;
                //_UclAccountList.Visible = false;
                //_UclAccountList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Account List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAccountList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddDoctorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclDoctorList = new UclListDoctor();
                //_UclDoctorList.Dock = DockStyle.Fill;
                //_UclDoctorList.Visible = false;
                //_UclDoctorList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Doctor List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclDoctorList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPatientListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPatientList = new UclListPatient();
                //_UclPatientList.Dock = DockStyle.Fill;
                //_UclPatientList.Visible = false;
                //_UclPatientList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Patient List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPatientList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddShelfListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclShelfList = new UclListShelf();
                //_UclShelfList.Dock = DockStyle.Fill;
                //_UclShelfList.Visible = false;
                //_UclShelfList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Shelf List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclShelfList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddBankListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclBankList = new UclListBank();
                //_UclBankList.Dock = DockStyle.Fill;
                //_UclBankList.Visible = false;
                //_UclBankList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Bank List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclBankList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddBranchListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclBranchList = new UclListBranch();
                //_UclBranchList.Dock = DockStyle.Fill;
                //_UclBranchList.Visible = false;
                //_UclBranchList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Branch List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclBranchList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAreaListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAreaList = new UclListArea();
                //_UclAreaList.Dock = DockStyle.Fill;
                //_UclAreaList.Visible = false;
                //_UclAreaList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Area List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAreaList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddGenericCategoryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclGenericCategoryList = new UclListGenericCategory();
                //_UclGenericCategoryList.Dock = DockStyle.Fill;
                //_UclGenericCategoryList.Visible = false;
                //_UclGenericCategoryList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Generic Category List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclGenericCategoryList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddTodaysChequesListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclListTodaysCheques = new UclListTodaysCheques();
                //_UclListTodaysCheques.Dock = DockStyle.Fill;
                //_UclListTodaysCheques.Visible = false;
                //_UclListTodaysCheques.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Todays Cheques List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclListTodaysCheques;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddOperatorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclListOperator = new UclListOperator();
                //_UclListOperator.Dock = DockStyle.Fill;
                //_UclListOperator.Visible = false;
                //_UclListOperator.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Operator List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclListOperator;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddChequeReturnListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclListChequeReturn = new UclVouChequeReturnList();
                //_UclListChequeReturn.Dock = DockStyle.Fill;
                //_UclListChequeReturn.Visible = false;
                //_UclListChequeReturn.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cheque Return List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclListChequeReturn;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Report Items For List

        # region Report Items For Voucher List

        private void AddCashReceiptReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCashReceiptList = new UclVouCashReceiptList();
                //_UclCashReceiptList.Dock = DockStyle.Fill;
                //_UclCashReceiptList.Visible = false;
                //_UclCashReceiptList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cash Receipt List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCashReceiptList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCashPaymentReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCashPaidList = new UclVouCashPaidList();
                //_UclCashPaidList.Dock = DockStyle.Fill;
                //_UclCashPaidList.Visible = false;
                //_UclCashPaidList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cash Payment List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCashPaidList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCashExpensesReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCashExpensesList = new UclVouCashExpensesList();
                //_UclCashExpensesList.Dock = DockStyle.Fill;
                //_UclCashExpensesList.Visible = false;
                //_UclCashExpensesList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cash Expenses List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCashExpensesList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddChequeReceiptReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclChequeReceiptList = new UclVouChequeReceiptList();
                //_UclChequeReceiptList.Dock = DockStyle.Fill;
                //_UclChequeReceiptList.Visible = false;
                //_UclChequeReceiptList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cheque Receipt";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclChequeReceiptList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void AddChequePaidReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclChequePaidList = new UclVouChequePaidList();
                //_UclChequePaidList.Dock = DockStyle.Fill;
                //_UclChequePaidList.Visible = false;
                //_UclChequePaidList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cheque Paid";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclChequePaidList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void AddBankExpensesReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVouBankExpensesList = new UclVouBankExpensesList();
                //_UclVouBankExpensesList.Dock = DockStyle.Fill;
                //_UclVouBankExpensesList.Visible = false;
                //_UclVouBankExpensesList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Bank Expenses List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVouBankExpensesList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStatementPurchaseReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVouStatementPurchaseList = new UclVouStatementPurchaseList();
                //_UclVouStatementPurchaseList.Dock = DockStyle.Fill;
                //_UclVouStatementPurchaseList.Visible = false;
                //_UclVouStatementPurchaseList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Statement Purchase";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVouStatementPurchaseList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStatementSaleReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                ////_UclVouStatementSaleList = new UclVouStatementSaleList();
                ////_UclVouStatementSaleList.Dock = DockStyle.Fill;
                ////_UclVouStatementSaleList.Visible = false;
                ////_UclVouStatementSaleList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Statement Sale";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVouStatementSaleList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddChequeReceivedButNotClearedReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVouChequeReceivedButNotCleared = new UclVouChequeReceivedButNotCleared();
                //_UclVouChequeReceivedButNotCleared.Dock = DockStyle.Fill;
                //_UclVouChequeReceivedButNotCleared.Visible = false;
                //_UclVouChequeReceivedButNotCleared.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cheque Received But Not Cleared";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVouChequeReceivedButNotCleared;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddChequePaidButNotClearedReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVouChequePaidButNotCleared = new UclVouChequePaidButNotCleared();
                //_UclVouChequePaidButNotCleared.Dock = DockStyle.Fill;
                //_UclVouChequePaidButNotCleared.Visible = false;
                //_UclVouChequePaidButNotCleared.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cheque Paid But Not Cleared";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVouChequePaidButNotCleared;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion  Report Items For Voucher List

        # region Report Items For Debit Credit Note List
        private void AddDebitNoteListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclDebitNoteList = new UclDebitNoteList();
                //_UclDebitNoteList.Dock = DockStyle.Fill;
                //_UclDebitNoteList.Visible = false;
                //_UclDebitNoteList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Debit Note";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclDebitNoteList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddDebitNoteProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclDebitNoteListProduct = new UclDebitNoteListProduct();
                //_UclDebitNoteListProduct.Dock = DockStyle.Fill;
                //_UclDebitNoteListProduct.Visible = false;
                //_UclDebitNoteListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Debit Note (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclDebitNoteListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockOutListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockOutList = new UclDebitNoteStockOutList();
                //_UclStockOutList.Dock = DockStyle.Fill;
                //_UclStockOutList.Visible = false;
                //_UclStockOutList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock Out List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockOutList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockOutProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockOutListProduct = new UclDebitNoteStockOutListProduct();
                //_UclStockOutListProduct.Dock = DockStyle.Fill;
                //_UclStockOutListProduct.Visible = false;
                //_UclStockOutListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock Out (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockOutListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCreditNoteListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCreditNoteList = new UclCreditNoteList();
                //_UclCreditNoteList.Dock = DockStyle.Fill;
                //_UclCreditNoteList.Visible = false;
                //_UclCreditNoteList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Credit Note";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCreditNoteList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCreditNoteProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCreditNoteListProduct = new UclCreditNoteListProduct();
                //_UclCreditNoteListProduct.Dock = DockStyle.Fill;
                //_UclCreditNoteListProduct.Visible = false;
                //_UclCreditNoteListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Credit Note (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCreditNoteListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockInListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockInList = new UclCreditNoteStockInList();
                //_UclStockInList.Dock = DockStyle.Fill;
                //_UclStockInList.Visible = false;
                //_UclStockInList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock IN";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockInList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockInProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockInListProduct = new UclCreditNoteStockInListProduct();
                //_UclStockInListProduct.Dock = DockStyle.Fill;
                //_UclStockInListProduct.Visible = false;
                //_UclStockInListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock IN (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockInListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCreditDebitNotePartyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCreditDebitNotePartyList = new UclCreditDebitNotePartyList();
                //_UclCreditDebitNotePartyList.Dock = DockStyle.Fill;
                //_UclCreditDebitNotePartyList.Visible = false;
                //_UclCreditDebitNotePartyList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "DB/CR Note(Party)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCreditDebitNotePartyList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddCreditDebitNoteProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclCreditDebitNoteProductList = new UclCreditDebitNoteProductList();
                //_UclCreditDebitNoteProductList.Dock = DockStyle.Fill;
                //_UclCreditDebitNoteProductList.Visible = false;
                //_UclCreditDebitNoteProductList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "DB/CR Note(Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclCreditDebitNoteProductList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockInOutProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockINOUTListProduct = new UclCreditDebitStockINOUTListProduct();
                //_UclStockINOUTListProduct.Dock = DockStyle.Fill;
                //_UclStockINOUTListProduct.Visible = false;
                //_UclStockINOUTListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock IN/OUT(Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockINOUTListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Report Items For Debit Credit Note List

        # region Report Items For Expiry
        # endregion Report Items For Expiry

        # region Report Items for Scheme
        private void AddSchemeAllListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSchemeListAll = new UclSchemeListAll();
                //_UclSchemeListAll.Dock = DockStyle.Fill;
                //_UclSchemeListAll.Visible = false;
                //_UclSchemeListAll.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Scheme List (ALL)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSchemeListAll;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void AddSchemeCompanyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSchemeListCompanywise = new UclSchemeListCompanywise();
                //_UclSchemeListCompanywise.Dock = DockStyle.Fill;
                //_UclSchemeListCompanywise.Visible = false;
                //_UclSchemeListCompanywise.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Scheme List (Company)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSchemeListCompanywise;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSchemeProductPurchaseListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSchemeListProductPurchase = new UclSchemeListProductPurchase();
                //_UclSchemeListProductPurchase.Dock = DockStyle.Fill;
                //_UclSchemeListProductPurchase.Visible = false;
                //_UclSchemeListProductPurchase.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Scheme List (Product/Purchase)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSchemeListProductPurchase;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void AddSchemeCompanyPurchaseListReportItem(ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;
        //    try
        //    {
        //        _UclSchemeListCompanywisePurchase = new UclSchemeListCompanywisePurchase();
        //        _UclSchemeListCompanywisePurchase.Dock = DockStyle.Fill;
        //        _UclSchemeListCompanywisePurchase.Visible = false;
        //        _UclSchemeListCompanywisePurchase.ExitClicked += new EventHandler(Item_ExitClicked);

        //        _ControlItem = new ControlItem();
        //        _ControlItem.ItemName = "Scheme List (Purchase)";
        //        _ControlItem.ItemMode = OperationMode.Report;
        //        _ControlItem.Control = _UclSchemeListCompanywisePurchase;

        //        _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
        //        _SubSubMenuItem.Name = _ControlItem.ItemName;
        //        _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
        //        _SubSubMenuItem.Tag = _ControlItem;
        //        _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        # endregion Report Items For Scheme

        # region Report Items For Purchase
        private void AddPurchaseProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListProductWise = new UclPurchaseListProduct();
                //_UclPurchaseListProductWise.Dock = DockStyle.Fill;
                //_UclPurchaseListProductWise.Visible = false;
                //_UclPurchaseListProductWise.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListProductWise;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseProductBatchListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListProductBatch = new UclPurchaseListProductBatch();
                //_UclPurchaseListProductBatch.Dock = DockStyle.Fill;
                //_UclPurchaseListProductBatch.Visible = false;
                //_UclPurchaseListProductBatch.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Batch)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListProductBatch;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseNewProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListNewProduct = new UclPurchaseListNewProduct();
                //_UclPurchaseListNewProduct.Dock = DockStyle.Fill;
                //_UclPurchaseListNewProduct.Visible = false;
                //_UclPurchaseListNewProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Products Created in Current Year";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListNewProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchasePartyProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListPartyProduct = new UclPurchaseListPartyProduct();
                //_UclPurchaseListPartyProduct.Dock = DockStyle.Fill;
                //_UclPurchaseListPartyProduct.Visible = false;
                //_UclPurchaseListPartyProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Party/Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListPartyProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchasePartyBillsListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListPartywisebills = new UclPurchaseListPartywisebills();
                //_UclPurchaseListPartywisebills.Dock = DockStyle.Fill;
                //_UclPurchaseListPartywisebills.Visible = false;
                //_UclPurchaseListPartywisebills.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Partywise Bills)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListPartywisebills;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseDiscountListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListDiscount = new UclPurchaseListDiscount();
                //_UclPurchaseListDiscount.Dock = DockStyle.Fill;
                //_UclPurchaseListDiscount.Visible = false;
                //_UclPurchaseListDiscount.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Discount)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListDiscount;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseAllPartySummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListAllPartySummary = new UclPurchaseListAllPartySummary();
                //_UclPurchaseListAllPartySummary.Dock = DockStyle.Fill;
                //_UclPurchaseListAllPartySummary.Visible = false;
                //_UclPurchaseListAllPartySummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (All Party Summary)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListAllPartySummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseCategoryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListCategory = new UclPurchaseListCategory();
                //_UclPurchaseListCategory.Dock = DockStyle.Fill;
                //_UclPurchaseListCategory.Visible = false;
                //_UclPurchaseListCategory.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Category)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListCategory;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddPurchaseCompanyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListCompany = new UclPurchaseListCompany();
                //_UclPurchaseListCompany.Dock = DockStyle.Fill;
                //_UclPurchaseListCompany.Visible = false;
                //_UclPurchaseListCompany.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Company)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListCompany;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void AddPurchaseDailyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclPurchaseListDaily = new UclPurchaseListDaily();
                //_UclPurchaseListDaily.Dock = DockStyle.Fill;
                //_UclPurchaseListDaily.Visible = false;
                //_UclPurchaseListDaily.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase (Daily)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclPurchaseListDaily;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Report Items For Purchase

        #region Report Items For Sale
        private void AddSaleDailyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListDailySale = new UclSaleListDailySale();
                //_UclSaleListDailySale.Dock = DockStyle.Fill;
                //_UclSaleListDailySale.Visible = false;
                //_UclSaleListDailySale.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Daily)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListDailySale;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleProductBatchListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListProductBatch = new UclSaleListProductBatch();
                //_UclSaleListProductBatch.Dock = DockStyle.Fill;
                //_UclSaleListProductBatch.Visible = false;
                //_UclSaleListProductBatch.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Product/Batch)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListProductBatch;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListProduct = new UclSaleListProduct();
                //_UclSaleListProduct.Dock = DockStyle.Fill;
                //_UclSaleListProduct.Visible = false;
                //_UclSaleListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSalePartySummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListPartySaleSummary = new UclSaleListPartySaleSummary();
                //_UclSaleListPartySaleSummary.Dock = DockStyle.Fill;
                //_UclSaleListPartySaleSummary.Visible = false;
                //_UclSaleListPartySaleSummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Party Summary)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListPartySaleSummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleRegularPartyProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListRegularPartyProduct = new UclSaleListRegularPartyProduct();
                //_UclSaleListRegularPartyProduct.Dock = DockStyle.Fill;
                //_UclSaleListRegularPartyProduct.Visible = false;
                //_UclSaleListRegularPartyProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Regular Party Product)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListRegularPartyProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleScheduledDrugListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListSheduledDrug = new UclSaleListSheduledDrug();
                //_UclSaleListSheduledDrug.Dock = DockStyle.Fill;
                //_UclSaleListSheduledDrug.Visible = false;
                //_UclSaleListSheduledDrug.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Scheduled Drugs)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListSheduledDrug;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSalePatientListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListPatient = new UclSaleListPatient();
                //_UclSaleListPatient.Dock = DockStyle.Fill;
                //_UclSaleListPatient.Visible = false;
                //_UclSaleListPatient.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Patient)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListPatient;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleDoctorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListDoctor = new UclSaleListDoctor();
                //_UclSaleListDoctor.Dock = DockStyle.Fill;
                //_UclSaleListDoctor.Visible = false;
                //_UclSaleListDoctor.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Doctor)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListDoctor;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleCategoryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListCategory = new UclSaleListCategory();
                //_UclSaleListCategory.Dock = DockStyle.Fill;
                //_UclSaleListCategory.Visible = false;
                //_UclSaleListCategory.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Category)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListCategory;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void AddSalePartywiseBillsListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListPartywiseBills = new UclSaleListPartywiseBills();
                //_UclSaleListPartywiseBills.Dock = DockStyle.Fill;
                //_UclSaleListPartywiseBills.Visible = false;
                //_UclSaleListPartywiseBills.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (PartywiseBills)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListPartywiseBills;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //private void AddSalePartyProductListReportItem(ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;
        //    try
        //    {
        //        _UclSaleListPartyProduct = new UclSaleListPartyProduct();
        //        _UclSaleListPartyProduct.Dock = DockStyle.Fill;
        //        _UclSaleListPartyProduct.Visible = false;
        //        _UclSaleListPartyProduct.ExitClicked += new EventHandler(Item_ExitClicked);

        //        _ControlItem = new ControlItem();
        //        _ControlItem.ItemName = "Sale (Party/Product)";
        //        _ControlItem.ItemMode = OperationMode.Report;
        //        _ControlItem.Control = _UclSaleListPartyProduct;

        //        _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
        //        _SubSubMenuItem.Name = _ControlItem.ItemName;
        //        _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
        //        _SubSubMenuItem.Tag = _ControlItem;
        //        _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        private void AddSaleDoctorCompanyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListDoctorCompany = new UclSaleListDoctorCompany();
                //_UclSaleListDoctorCompany.Dock = DockStyle.Fill;
                //_UclSaleListDoctorCompany.Visible = false;
                //_UclSaleListDoctorCompany.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Doctor/Company)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListDoctorCompany;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleDaywiseProductSummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListDaywiseProductSummary = new UclSaleListProductSummary();
                //_UclSaleListDaywiseProductSummary.Dock = DockStyle.Fill;
                //_UclSaleListDaywiseProductSummary.Visible = false;
                //_UclSaleListDaywiseProductSummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Product Summary)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListDaywiseProductSummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleAdmitPatientListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListAdmitPatient = new UclSaleListAdmitPatient();
                //_UclSaleListAdmitPatient.Dock = DockStyle.Fill;
                //_UclSaleListAdmitPatient.Visible = false;
                //_UclSaleListAdmitPatient.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Admit Patients)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListAdmitPatient;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleOperatorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListOperator = new UclSaleListOperator();
                //_UclSaleListOperator.Dock = DockStyle.Fill;
                //_UclSaleListOperator.Visible = false;
                //_UclSaleListOperator.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Operator)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListOperator;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleIPDListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListIPD = new UclSaleListIPD();
                //_UclSaleListIPD.Dock = DockStyle.Fill;
                //_UclSaleListIPD.Visible = false;
                //_UclSaleListIPD.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (IPD)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListIPD;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleOPDListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListOPD = new UclSaleListOPD();
                //_UclSaleListOPD.Dock = DockStyle.Fill;
                //_UclSaleListOPD.Visible = false;
                //_UclSaleListOPD.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (OPD)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListOPD;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleCreditCardListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleListCreditCard = new UclSaleListCreditCard();
                //_UclSaleListCreditCard.Dock = DockStyle.Fill;
                //_UclSaleListCreditCard.Visible = false;
                //_UclSaleListCreditCard.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (CreditCard)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleListCreditCard;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddSaleDailyProductsReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclSaleDailyProducts = new UclSaleListDailyProducts();
                //_UclSaleDailyProducts.Dock = DockStyle.Fill;
                //_UclSaleDailyProducts.Visible = false;
                //_UclSaleDailyProducts.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale (Daily Products)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclSaleDailyProducts;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void AddSaleVoucherSaleListReportItem(ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;
        //    try
        //    {
        //        _UclSaleListVoucherSale = new UclSaleListVoucherSale();
        //        _UclSaleListVoucherSale.Dock = DockStyle.Fill;
        //        _UclSaleListVoucherSale.Visible = false;
        //        _UclSaleListVoucherSale.ExitClicked += new EventHandler(Item_ExitClicked);

        //        _ControlItem = new ControlItem();
        //        _ControlItem.ItemName = "Sale (Voucher)";
        //        _ControlItem.ItemMode = OperationMode.Report;
        //        _ControlItem.Control = _UclSaleListVoucherSale;

        //        _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
        //        _SubSubMenuItem.Name = _ControlItem.ItemName;
        //        _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
        //        _SubSubMenuItem.Tag = _ControlItem;
        //        _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        # endregion Report Items For Sale

        # region Report Items For Stock
        private void AddStockCurrentListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListCurrentStock = new UclStockListCurrentStock();
                //_UclStockListCurrentStock.Dock = DockStyle.Fill;
                //_UclStockListCurrentStock.Visible = false;
                //_UclStockListCurrentStock.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Current Stock";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListCurrentStock;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockCurrentBatchListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListBatchwise = new UclStockListBatchwise();
                //_UclStockListBatchwise.Dock = DockStyle.Fill;
                //_UclStockListBatchwise.Visible = false;
                //_UclStockListBatchwise.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Current Stock (Batch)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListBatchwise;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockCurrentShelfListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListShelf = new UclStockListShelf();
                //_UclStockListShelf.Dock = DockStyle.Fill;
                //_UclStockListShelf.Visible = false;
                //_UclStockListShelf.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Current Stock (Shelf)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListShelf;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockNonMovingListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListNonMoving = new UclStockListNonMoving();
                //_UclStockListNonMoving.Dock = DockStyle.Fill;
                //_UclStockListNonMoving.Visible = false;
                //_UclStockListNonMoving.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Non Moving";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListNonMoving;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockAllListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListAll = new UclStockListAll();
                //_UclStockListAll.Dock = DockStyle.Fill;
                //_UclStockListAll.Visible = false;
                //_UclStockListAll.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "All Products";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListAll;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockStocknsaleListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListStocknSale = new UclStockListStocknSale();
                //_UclStockListStocknSale.Dock = DockStyle.Fill;
                //_UclStockListStocknSale.Visible = false;
                //_UclStockListStocknSale.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock n Sale";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListStocknSale;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockProductLedgerListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListProductLedger = new UclStockListProductLedger();
                //_UclStockListProductLedger.Dock = DockStyle.Fill;
                //_UclStockListProductLedger.Visible = false;
                //_UclStockListProductLedger.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Product Ledger";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListProductLedger;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockCategorySummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListCategorySummary = new UclStockListCategorySummary();
                //_UclStockListCategorySummary.Dock = DockStyle.Fill;
                //_UclStockListCategorySummary.Visible = false;
                //_UclStockListCategorySummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Category Summary";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListCategorySummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void AddStockCompanySummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListCompanywiseSummary = new UclStockListCompanywiseSummary();
                //_UclStockListCompanywiseSummary.Dock = DockStyle.Fill;
                //_UclStockListCompanywiseSummary.Visible = false;
                //_UclStockListCompanywiseSummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Company Summary";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListCompanywiseSummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockPatientListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListPatient = new UclStockListPatient();
                //_UclStockListPatient.Dock = DockStyle.Fill;
                //_UclStockListPatient.Visible = false;
                //_UclStockListPatient.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Visiting Patient";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListPatient;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockOpeningStockListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListOpeningStock = new UclStockListOpeningStock();
                //_UclStockListOpeningStock.Dock = DockStyle.Fill;
                //_UclStockListOpeningStock.Visible = false;
                //_UclStockListOpeningStock.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Opening Stock List";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListOpeningStock;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddStockOpeningStockProductListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclStockListOpeningStockProduct = new UclStockListOpeningStockProduct();
                //_UclStockListOpeningStockProduct.Dock = DockStyle.Fill;
                //_UclStockListOpeningStockProduct.Visible = false;
                //_UclStockListOpeningStockProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Opening Stock - Product";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclStockListOpeningStockProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void AddStockProductbySelectionListReportItem(ToolStripMenuItem _SubMenuItem)
        //{
        //    ToolStripMenuItem _SubSubMenuItem = null;
        //    ControlItem _ControlItem = null;
        //    try
        //    {
        //        _UclProductListBySelectionWithStock = new UclProductListBySelectionwithstock();
        //        _UclProductListBySelectionWithStock.Dock = DockStyle.Fill;
        //        _UclProductListBySelectionWithStock.Visible = false;
        //        _UclProductListBySelectionWithStock.ExitClicked += new EventHandler(Item_ExitClicked);

        //        _ControlItem = new ControlItem();
        //        _ControlItem.ItemName = "Products by Selection";
        //        _ControlItem.ItemMode = OperationMode.Report;
        //        _ControlItem.Control = _UclProductListBySelectionWithStock;

        //        _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
        //        _SubSubMenuItem.Name = _ControlItem.ItemName;
        //        _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
        //        _SubSubMenuItem.Tag = _ControlItem;
        //        _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}


        # endregion Report Items For Stock

        # region Report Items For Accounts
        private void AddAcSalesRegisterListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListSalesRegister = new UclAcListSalesRegister();
                //_UclAcListSalesRegister.Dock = DockStyle.Fill;
                //_UclAcListSalesRegister.Visible = false;
                //_UclAcListSalesRegister.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sales Register";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListSalesRegister;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcPurchaseRegisterListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListPurchaseRegister = new UclAcListPurchaseRegister();
                //_UclAcListPurchaseRegister.Dock = DockStyle.Fill;
                //_UclAcListPurchaseRegister.Visible = false;
                //_UclAcListPurchaseRegister.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListPurchaseRegister;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcCashBookListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListCashBook = new UclAcListCashBook();
                //_UclAcListCashBook.Dock = DockStyle.Fill;
                //_UclAcListCashBook.Visible = false;
                //_UclAcListCashBook.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Cash Book";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListCashBook;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcBankBookListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListBankBook = new UclAcListBankBook();
                //_UclAcListBankBook.Dock = DockStyle.Fill;
                //_UclAcListBankBook.Visible = false;
                //_UclAcListBankBook.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Bank Book";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListBankBook;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcBankBookByClearedDateListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListBankBookByClearedDate = new UclAcListBankBookByClearedDate();
                //_UclAcListBankBookByClearedDate.Dock = DockStyle.Fill;
                //_UclAcListBankBookByClearedDate.Visible = false;
                //_UclAcListBankBookByClearedDate.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Bank Book By Cleared Date";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListBankBookByClearedDate;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }



        private void AddAcJournalListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListJournal = new UclAcListJournal();
                //_UclAcListJournal.Dock = DockStyle.Fill;
                //_UclAcListJournal.Visible = false;
                //_UclAcListJournal.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Journal";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListJournal;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcGeneralLedgerListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListGeneralLedger = new UclAcListGeneralLedger();
                //_UclAcListGeneralLedger.Dock = DockStyle.Fill;
                //_UclAcListGeneralLedger.Visible = false;
                //_UclAcListGeneralLedger.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "General Ledger";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListGeneralLedger;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcDebtorLedgerListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListDebtorLedger = new UclAcListDebtorLedger();
                //_UclAcListDebtorLedger.Dock = DockStyle.Fill;
                //_UclAcListDebtorLedger.Visible = false;
                //_UclAcListDebtorLedger.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Debtor Ledger";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListDebtorLedger;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcCreditorLedgerListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListCreditorLedger = new UclAcListCreditorLedger();
                //_UclAcListCreditorLedger.Dock = DockStyle.Fill;
                //_UclAcListCreditorLedger.Visible = false;
                //_UclAcListCreditorLedger.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Creditor Ledger";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListCreditorLedger;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcSundryDebtorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListSundryDebtor = new UclAcListSundryDebtor();
                //_UclAcListSundryDebtor.Dock = DockStyle.Fill;
                //_UclAcListSundryDebtor.Visible = false;
                //_UclAcListSundryDebtor.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sundry Debtor";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListSundryDebtor;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcSundryCreditorListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListSundryCreditor = new UclAcListSundryCreditor();
                //_UclAcListSundryCreditor.Dock = DockStyle.Fill;
                //_UclAcListSundryCreditor.Visible = false;
                //_UclAcListSundryCreditor.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sundry Creditor";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListSundryCreditor;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddAcAgeingListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclAcListAgeing = new UclAcListAgeing();
                //_UclAcListAgeing.Dock = DockStyle.Fill;
                //_UclAcListAgeing.Visible = false;
                //_UclAcListAgeing.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Ageing";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclAcListAgeing;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion Report Items For Accounts

        # region Report Items For Final Accounts
        private void AddFAcTrialBalanceListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclFACListTrialBalance = new UclFACListTrialBalance();
                //_UclFACListTrialBalance.Dock = DockStyle.Fill;
                //_UclFACListTrialBalance.Visible = false;
                //_UclFACListTrialBalance.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Trial Balance";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclFACListTrialBalance;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddFAcEntryOfScheduledNumbersListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclFACListEntryofScheduleNumber = new UclFACListEntryofScheduleNumber();
                //_UclFACListEntryofScheduleNumber.Dock = DockStyle.Fill;
                //_UclFACListEntryofScheduleNumber.Visible = false;
                //_UclFACListEntryofScheduleNumber.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Entry Of Scheduled Numbers";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclFACListEntryofScheduleNumber;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void AddFAcProfitnLossListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclFACListProfitandLoss = new UclFACListProfitandLoss();
                //_UclFACListProfitandLoss.Dock = DockStyle.Fill;
                //_UclFACListProfitandLoss.Visible = false;
                //_UclFACListProfitandLoss.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Profit & Loss";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclFACListProfitandLoss;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddFAcPrintSchedulesListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclFACListPrintSchedules = new UclFACListPrintSchedules();
                //_UclFACListPrintSchedules.Dock = DockStyle.Fill;
                //_UclFACListPrintSchedules.Visible = false;
                //_UclFACListPrintSchedules.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Print Schedules";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclFACListPrintSchedules;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddFAcBalanceSheetListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclFACListBalanceSheet = new UclFACListBalanceSheet();
                //_UclFACListBalanceSheet.Dock = DockStyle.Fill;
                //_UclFACListBalanceSheet.Visible = false;
                //_UclFACListBalanceSheet.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Balance Sheet";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclFACListBalanceSheet;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Report Items For Final Accounts

        # region Report Items For VAT
        private void AddVATSaleRegisterListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegister = new UclVATListSalesRegister();
                //_UclVATListSalesRegister.Dock = DockStyle.Fill;
                //_UclVATListSalesRegister.Visible = false;
                //_UclVATListSalesRegister.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegister;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATSaleRegisterOtherDetailsListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegisterOtherDetails = new UclVATListSalesRegisterOtherDetails();
                //_UclVATListSalesRegisterOtherDetails.Dock = DockStyle.Fill;
                //_UclVATListSalesRegisterOtherDetails.Visible = false;
                //_UclVATListSalesRegisterOtherDetails.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register (Other Details)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegisterOtherDetails;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATSaleRegisterDateListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegisterDate = new UclVATListSalesRegisterDate();
                //_UclVATListSalesRegisterDate.Dock = DockStyle.Fill;
                //_UclVATListSalesRegisterDate.Visible = false;
                //_UclVATListSalesRegisterDate.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register (Date)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegisterDate;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATSaleRegisterMonthListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegisterMonth = new UclVATListSalesRegisterMonth();
                //_UclVATListSalesRegisterMonth.Dock = DockStyle.Fill;
                //_UclVATListSalesRegisterMonth.Visible = false;
                //_UclVATListSalesRegisterMonth.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register (Month)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegisterMonth;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATSaleRegisterTINListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegisterTIN = new UclVATListSalesRegisterParty();
                //_UclVATListSalesRegisterTIN.Dock = DockStyle.Fill;
                //_UclVATListSalesRegisterTIN.Visible = false;
                //_UclVATListSalesRegisterTIN.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register (Party)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegisterTIN;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATSaleRegisterDetailListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListSalesRegisterDetail = new UclVATListSalesRegisterDetail();
                //_UclVATListSalesRegisterDetail.Dock = DockStyle.Fill;
                //_UclVATListSalesRegisterDetail.Visible = false;
                //_UclVATListSalesRegisterDetail.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sale Register (Detail)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListSalesRegisterDetail;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegister = new UclVATListPurchaseRegister();
                //_UclVATListPurchaseRegister.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegister.Visible = false;
                //_UclVATListPurchaseRegister.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register VAT";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegister;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterOtherDetailsListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegisterOtherDetails = new UclVATListPurchaseRegisterOtherDetails();
                //_UclVATListPurchaseRegisterOtherDetails.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegisterOtherDetails.Visible = false;
                //_UclVATListPurchaseRegisterOtherDetails.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register (OtherDetails)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegisterOtherDetails;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterDateListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegisterDate = new UclVATListPurchaseRegisterDate();
                //_UclVATListPurchaseRegisterDate.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegisterDate.Visible = false;
                //_UclVATListPurchaseRegisterDate.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register (Date)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegisterDate;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterMonthListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegisterMonth = new UclVATListPurchaseRegisterMonth();
                //_UclVATListPurchaseRegisterMonth.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegisterMonth.Visible = false;
                //_UclVATListPurchaseRegisterMonth.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register (Month)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegisterMonth;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterTINListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegisterTIN = new UclVATListPurchaseRegisterTIN();
                //_UclVATListPurchaseRegisterTIN.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegisterTIN.Visible = false;
                //_UclVATListPurchaseRegisterTIN.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register (TIN)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegisterTIN;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATPurchaseRegisterDetailReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListPurchaseRegisterDetail = new UclVATListPurchaseRegisterDetail();
                //_UclVATListPurchaseRegisterDetail.Dock = DockStyle.Fill;
                //_UclVATListPurchaseRegisterDetail.Visible = false;
                //_UclVATListPurchaseRegisterDetail.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Purchase Register (Detail)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListPurchaseRegisterDetail;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATCreditNoteRegisterDetailReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListCreditNote = new UclVATListCreditNote();
                //_UclVATListCreditNote.Dock = DockStyle.Fill;
                //_UclVATListCreditNote.Visible = false;
                //_UclVATListCreditNote.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Sales Return VAT Register (Detail)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListCreditNote;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddVATListCombineReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclVATListCreditNote = new UclVATListCreditNote();
                //_UclVATListCreditNote.Dock = DockStyle.Fill;
                //_UclVATListCreditNote.Visible = false;
                //_UclVATListCreditNote.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "VAT Register Combine(Month)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclVATListCombine;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Report Items For VAT

        # region Report Items For MIS

        private void AddMISProfitDayListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListProfitDay = new UclMISListProfitDay();
                //_UclMISListProfitDay.Dock = DockStyle.Fill;
                //_UclMISListProfitDay.Visible = false;
                //_UclMISListProfitDay.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Profit (Day)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListProfitDay;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISProfitCompanyListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListProfitCompany = new UclMISListProfitCompany();
                //_UclMISListProfitCompany.Dock = DockStyle.Fill;
                //_UclMISListProfitCompany.Visible = false;
                //_UclMISListProfitCompany.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Profit (Company)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListProfitCompany;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISDailyCashClosingListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListDailyCashClosing = new UclMISListDailyCashClosing();
                //_UclMISListDailyCashClosing.Dock = DockStyle.Fill;
                //_UclMISListDailyCashClosing.Visible = false;
                //_UclMISListDailyCashClosing.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Daily Cash Closing";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListDailyCashClosing;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISDailyBankClosingListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListDailyBankClosing = new UclMISListDailyBankClosing();
                //_UclMISListDailyBankClosing.Dock = DockStyle.Fill;
                //_UclMISListDailyBankClosing.Visible = false;
                //_UclMISListDailyBankClosing.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Daily Bank Closing";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListDailyBankClosing;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISClosingStockValueListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListCurrentStockValue = new UclMISListCurrentStockValue();
                //_UclMISListCurrentStockValue.Dock = DockStyle.Fill;
                //_UclMISListCurrentStockValue.Visible = false;
                //_UclMISListCurrentStockValue.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Closing Stock Value";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListCurrentStockValue;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISClosingStockStatementListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListCurrentStockStatement = new UclMISListCurrentStockStatement();
                //_UclMISListCurrentStockStatement.Dock = DockStyle.Fill;
                //_UclMISListCurrentStockStatement.Visible = false;
                //_UclMISListCurrentStockStatement.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock Statement";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListCurrentStockStatement;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISStockStatementBankListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListStockStatementBank = new UclMISListStockStatementBank();
                //_UclMISListStockStatementBank.Dock = DockStyle.Fill;
                //_UclMISListStockStatementBank.Visible = false;
                //_UclMISListStockStatementBank.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock Statement(Bank)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListStockStatementBank;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISBankInterestListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListBankInterest = new UclMISListBankInterest();
                //_UclMISListBankInterest.Dock = DockStyle.Fill;
                //_UclMISListBankInterest.Visible = false;
                //_UclMISListBankInterest.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Bank Interest";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListBankInterest;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISSummaryListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListSummary = new UclMISListSummary();
                //_UclMISListSummary.Dock = DockStyle.Fill;
                //_UclMISListSummary.Visible = false;
                //_UclMISListSummary.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Summary";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListSummary;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISDeletedVouchersReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListDeletedVouchers = new UclMISListDeletedVouchers();
                //_UclMISListDeletedVouchers.Dock = DockStyle.Fill;
                //_UclMISListDeletedVouchers.Visible = false;
                //_UclMISListDeletedVouchers.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Deleted Vouchers";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListDeletedVouchers;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddMISChangedVouchersReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclMISListChangedVouchers = new UclMISListChangedVouchers();
                //_UclMISListChangedVouchers.Dock = DockStyle.Fill;
                //_UclMISListChangedVouchers.Visible = false;
                //_UclMISListChangedVouchers.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Changed Vouchers";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclMISListChangedVouchers;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion Report Items For MIS

        # region Report Items For EXPIRY

        private void AddExpiryListProductReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclExpiryListProduct = new UclExpiryListProduct();
                //_UclExpiryListProduct.Dock = DockStyle.Fill;
                //_UclExpiryListProduct.Visible = false;
                //_UclExpiryListProduct.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Expiry (All Products)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclExpiryListProduct;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Expiry

        #region Report Items For H1
        private void AddH1SaleListReportItem(ToolStripMenuItem _SubMenuItem)
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                //_UclH1SaleList = new UclH1SaleList();
                //_UclH1SaleList.Dock = DockStyle.Fill;
                //_UclH1SaleList.Visible = false;
                //_UclH1SaleList.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "H1 (Sale)";
                _ControlItem.ItemMode = OperationMode.Report;
                _ControlItem.Control = _UclH1SaleList;

                _SubSubMenuItem = new ToolStripMenuItem(_ControlItem.ItemName);
                _SubSubMenuItem.Name = _ControlItem.ItemName;
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                _SubMenuItem.DropDownItems.Add(_SubSubMenuItem);

                bool isViewVisible = IsUserRightAllowed(_ControlItem.ItemName, _ControlItem.ItemMode);
                if (isViewVisible)
                {
                    _SubSubMenuItem.Visible = true;
                }
                else
                {
                    _SubSubMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Report items for h1

        #endregion Report Items

        #endregion Initialize reports


        #region initialize Settings

        private void InitializeSettings()
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                tsMenuSettings.DropDownItems.Clear();

                _UclSettingsEmail = new UclSettingsEmail();
                _UclSettingsEmail.Dock = DockStyle.Fill;
                _UclSettingsEmail.Visible = false;
                _UclSettingsEmail.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Email Setting";
                _ControlItem.ItemMode = OperationMode.Add;
                _ControlItem.Control = _UclSettingsEmail;

                _SubSubMenuItem = new ToolStripMenuItem("Email Setting");
                _SubSubMenuItem.Name = "Email Setting";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsMenuSettings.DropDownItems.Add(_SubSubMenuItem);



                _UclSettingsSale = new UclSettingsSale();
                _UclSettingsSale.Dock = DockStyle.Fill;
                _UclSettingsSale.Visible = false;
                _UclSettingsSale.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Settings";
                _ControlItem.ItemMode = OperationMode.Add;
                _ControlItem.Control = _UclSettingsSale;

                _SubSubMenuItem = new ToolStripMenuItem("Settings");
                _SubSubMenuItem.Name = "Settings";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsMenuSettings.DropDownItems.Add(_SubSubMenuItem);

                _UclSettingsForPrint = new UclSettingsForPrint();
                _UclSettingsForPrint.Dock = DockStyle.Fill;
                _UclSettingsForPrint.Visible = false;
                _UclSettingsForPrint.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Print Settings";
                _ControlItem.ItemMode = OperationMode.Add;
                _ControlItem.Control = _UclSettingsForPrint;

                _SubSubMenuItem = new ToolStripMenuItem("Print Settings");
                _SubSubMenuItem.Name = "Print Settings";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsMenuSettings.DropDownItems.Add(_SubSubMenuItem); 

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Settings

        #region initialize Tools

        private void InitializeTools()
        {
            ToolStripMenuItem _SubSubMenuItem = null;
            ControlItem _ControlItem = null;
            try
            {
                tsMenuTools.DropDownItems.Clear();

                _UclStockReProcess = new UclStockReProcess();
                _UclStockReProcess.Dock = DockStyle.Fill;
                _UclStockReProcess.Visible = false;
                _UclStockReProcess.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "Stock Re-process";
                _ControlItem.ItemMode = OperationMode.View;
                _ControlItem.Control = _UclStockReProcess;

                _SubSubMenuItem = new ToolStripMenuItem("Stock Re-process");
                _SubSubMenuItem.Name = "Stock Re-process";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsMenuTools.DropDownItems.Add(_SubSubMenuItem);








                _UclBackupPath = new UclBackupPath();
                _UclBackupPath.Dock = DockStyle.Fill;
                _UclBackupPath.Visible = false;
                _UclBackupPath.ExitClicked += new EventHandler(Item_ExitClicked);

                _ControlItem = new ControlItem();
                _ControlItem.ItemName = "BackUp Path Setting";
                _ControlItem.ItemMode = OperationMode.Add;
                _ControlItem.Control = _UclBackupPath;

                _SubSubMenuItem = new ToolStripMenuItem("BackUp Path Setting");
                _SubSubMenuItem.Name = "BackUp Path Setting";
                _SubSubMenuItem.Click += new EventHandler(this.menuItem_Click);
                _SubSubMenuItem.Tag = _ControlItem;
                tsMenuTools.DropDownItems.Add(_SubSubMenuItem);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Tools

        #region Helper Methods
        private void InitializeDatabase()
        {
            try
            {
                ConnectionInfo connInfo = new ConnectionInfo();
                connInfo.Initialize();
                if (!connInfo.IsDBConnected)
                {
                    MessageBox.Show("Failed to connect database!", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

        }

        private bool ExitApplication()
        {
            bool retValue = false;
            if (MessageBox.Show("Do you want to exit from PharmaSYS Retail Plus?", General.ApplicationTitle,
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //General.DeleteTempStockByComputerName();
                //General.DisconnectClient();
                CreateDatabaseBackup();
                General.DisposeConnection();
                retValue = true;
            }

            return retValue;
        }

        private void CreateDatabaseBackup()
        {
            try
            {
                string ExeLocation = "C:\\Program Files\\MySQL\\MySQL Server 5.6\\bin\\mysqldump.exe";
                if (System.IO.File.Exists(ExeLocation))
                {
                    //Do Backup
                    string backupPath1 = General.BackupPath.BackupPath1;
                    string backupPath2 = General.BackupPath.BackupPath2;
                    string backupPath3 = General.BackupPath.BackupPath3;

                    if (System.IO.Directory.Exists(backupPath1))
                        DatabaseBackup(ExeLocation, backupPath1);

                    if (System.IO.Directory.Exists(backupPath2))
                        DatabaseBackup(ExeLocation, backupPath2);

                    if (System.IO.Directory.Exists(backupPath3))
                        DatabaseBackup(ExeLocation, backupPath3);

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void DatabaseBackup(string ExeLocation, string backupPath)
        {
            try
            {
                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(PharmaSYSRetailPlus.DataLayer.DBInterface.ConnectionString);
                if (builder == null)
                    return;

                string ServerName = builder["server"].ToString();
                string DBName = builder["database"].ToString();
                string UserID = builder["uid"].ToString();
                string UserPassword = builder["pwd"].ToString();

                backupPath = backupPath + "\\" + General.ConvertCurrentDateToISODateString();
                backupPath = backupPath + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".sql";

                System.IO.StreamWriter file = new System.IO.StreamWriter(backupPath);
                ProcessStartInfo proc = new ProcessStartInfo();
                string cmd = string.Format(@"-u{0} -p{1} -h{2} --database {3}", UserID, UserPassword, ServerName, DBName);
                //string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", "root", "password", "localhost", "dbfile");
                proc.FileName = ExeLocation;
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.Arguments = cmd;
                proc.UseShellExecute = false;
                proc.CreateNoWindow = true;
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process p = Process.Start(proc);
                string res;
                res = p.StandardOutput.ReadToEnd();
                file.WriteLine(res);
                p.WaitForExit();
                file.Close();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region Private Method(s) of IMainForm interface

        private bool IsFormAlreadyOpen(ControlItem _item)
        {
            bool retValue = false;
            foreach (Control ctl in PnlViewDetail.Controls)
            {
                if (ctl.Name.ToLower() == _item.Control.Name.ToLower())
                {
                    retValue = true;
                    break;
                }
            }
            return retValue;
        }

        private void Item_ExitClicked(object sender, EventArgs e)
        {
            try
            {
                Control _item = (Control)sender;
                for (int index = 0; index < PnlViewDetail.Controls.Count; index++)
                {
                    Control ctl = PnlViewDetail.Controls[index];
                    if (ctl.Name.ToLower() == _item.Name.ToLower())
                    {
                        PnlViewDetail.Controls.Remove(ctl);
                        WindowMenuItem_Remove(_item);
                        //Set last active control as checked
                        SetActiveControlInWindowMenu();
                        RefreshImportForm();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.Item_ExitClicked:" + ex.Message);
            }
        }

        private void RefreshImportForm()
        {
            if (_formImportAlliedSaleBill != null)
            {
                _formImportAlliedSaleBill.RefreshData();
            }
        }

        private void SetActiveControlInWindowMenu()
        {
            try
            {
                if (PnlViewDetail.Controls.Count > 0)
                {
                    Control activeControl = PnlViewDetail.Controls[0];
                    foreach (ToolStripMenuItem menuItem in tsmenuWindow.DropDownItems)
                    {
                        ControlItem item = (ControlItem)menuItem.Tag;
                        if (item.Control.Name.ToLower() == activeControl.Name.ToLower())
                        {
                            menuItem.Checked = true;
                            if (item.Control is BaseControl)
                            {
                                ((BaseControl)item.Control).ReFillData();
                            }
                            _ActiveControl = item.Control;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.SetActiveControlInWindowMenu:" + ex.Message);
            }
        }
        #endregion

        #region Event(s)

        #region MenuItem Events

        private void tsmenusubLicAssociation_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation importLic = new FormValidation();
                importLic.Initialize(FormValidation.WizardStates.State_Association);
                importLic.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void tsmenusubImportSaleBillMitra_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void tsmenusubImportSaleBillAllied_Click(object sender, EventArgs e)
        {
            try
            {
                if (_formImportAlliedSaleBill == null)
                    CreateImportAlliedSaleBillForm();
                _formImportAlliedSaleBill.Show();
                _formImportAlliedSaleBill.BringToFront();
                _formImportAlliedSaleBill.Focus();
                _formImportAlliedSaleBill.RefreshData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void tsmenusubExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenItem(ControlItem _item)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //Check & Initialize Control
                if (_item.Control == null)
                {
                    InitializeUserControl(_item);
                }
                //Check for Existing open items
                if (IsFormAlreadyOpen(_item))
                {
                    MessageBox.Show(string.Format("Form '{0}' already open..!", _item.ItemName), General.ApplicationTitle);
                    this.Cursor = Cursors.Default;
                    return;
                }
                ShowProgressIndicator();
                //Check Control
                if (_item.Control != null && _item.Control is BaseControl)
                {

                    switch (_item.ItemMode)
                    {
                        case OperationMode.Add:
                            ((BaseControl)_item.Control).Mode = OperationMode.Add;
                            ((BaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IDetailControl)_item.Control).Add();
                            break;
                        case OperationMode.Edit:
                            ((BaseControl)_item.Control).Mode = OperationMode.Edit;
                            ((BaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IDetailControl)_item.Control).Edit();
                            break;
                        case OperationMode.Delete:
                            ((BaseControl)_item.Control).Mode = OperationMode.Delete;
                            ((BaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IDetailControl)_item.Control).Delete();
                            break;
                        case OperationMode.View:
                            ((BaseControl)_item.Control).Mode = OperationMode.View;
                            ((BaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IDetailControl)_item.Control).View();
                            break;
                        case OperationMode.Fifth:
                            ((BaseControl)_item.Control).Mode = OperationMode.Fifth;
                            ((BaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IDetailControl)_item.Control).Edit();
                            break;
                    }
                }
                else if (_item.Control != null && _item.Control is ReportBaseControl)
                {
                    switch (_item.ItemMode)
                    {
                        case OperationMode.Report:
                            ((ReportBaseControl)_item.Control).Mode = OperationMode.Report;
                            ((ReportBaseControl)_item.Control).ControlName = _item.ItemName;
                            ((IReportControl)_item.Control).ShowOverview();
                            break;
                    }
                }

                //Check done
                PnlViewDetail.Controls.Add(_item.Control);
                _item.Control.Visible = true;
                SetControlSize(_item.Control);
                _item.Control.BringToFront();
                _ActiveControl = _item.Control;
                WindowMenuItem_Add(_item);
                SetFavourite(_item);
                if (_item.Control != null && _item.Control is BaseControl)
                {
                    ((BaseControl)_item.Control).SetFocus();
                }
                else if (_item.Control != null && _item.Control is ReportBaseControl)
                {
                    ((ReportBaseControl)_item.Control).SetFocus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.OpenItem:" + ex.Message);
                Log.WriteError("FormMain.OpenItem:" + ex.StackTrace);
            }
            HideProgressIndicator();
            this.Cursor = Cursors.Default;
        }

        private void SetControlSize(UserControl ucl)
        {
            const int FORMHEIGHT = 630;
            const int FORMWIDTH = 975;
            try
            {
                ucl.Dock = DockStyle.None;
                ucl.Height = FORMHEIGHT;
                ucl.Width = FORMWIDTH;
                int locationXCenter = (PnlViewDetail.Width - FORMWIDTH) / 2;
                int locationYCenter = (PnlViewDetail.Height - FORMHEIGHT) / 2;
                ucl.Location = new Point(locationXCenter, locationYCenter);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.SetControlSize:" + ex.Message);
            }
        }

        private void ShowProgressIndicator()
        {
            try
            {
                toolStripStatusLabel1.Visible = true;
                toolStripProgressBar1.Value = 50;
                toolStripProgressBar1.Visible = true;
                statusStrip1.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.ShowProgressIndicator:" + ex.Message);
            }
        }

        private void HideProgressIndicator()
        {
            try
            {
                toolStripStatusLabel1.Visible = false;
                toolStripProgressBar1.Visible = false;
                statusStrip1.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.HideProgressIndicator:" + ex.Message);
            }
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            ControlItem _item = (ControlItem)((ToolStripMenuItem)sender).Tag;
            OpenItem(_item);
        }

        private void WindowMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ControlItem _item = (ControlItem)((ToolStripMenuItem)sender).Tag;
                WindowMenuItem_ClearAllSelection();
                for (int index = 0; index < PnlViewDetail.Controls.Count; index++)
                {
                    Control ctl = PnlViewDetail.Controls[index];
                    if (ctl.Name.ToLower() == _item.Control.Name.ToLower())
                    {
                        ctl.Visible = true;
                        ctl.BringToFront();
                        break;
                    }
                }
                WindowMenuItem_SetCurrentItemChecked(_item);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.WindowMenuItem_Click:" + ex.Message);
            }
        }

        private void FavouriteMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ControlItem _item = (ControlItem)((ToolStripMenuItem)sender).Tag;
                OpenItem(_item);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.FavouriteMenuItem_Click:" + ex.Message);
            }
        }

        private void WindowMenuItem_SetCurrentItemChecked(ControlItem _item)
        {
            try
            {
                foreach (ToolStripMenuItem menuItem in tsmenuWindow.DropDownItems)
                {
                    if (menuItem.Tag == _item)
                    {
                        menuItem.Checked = true;
                        _ActiveControl = _item.Control;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.ClearAllWindowMenuItemSelection:" + ex.Message);
            }
        }

        private void WindowMenuItem_Add(ControlItem _item)
        {
            try
            {
                WindowMenuItem_ClearAllSelection();
                ToolStripMenuItem _WindowMenuItem = new ToolStripMenuItem(_item.ItemName);
                _WindowMenuItem.Name = _item.ItemName;
                _WindowMenuItem.Click += new EventHandler(this.WindowMenuItem_Click);
                _WindowMenuItem.Tag = _item;
                _WindowMenuItem.Checked = true;
                tsmenuWindow.DropDownItems.Add(_WindowMenuItem);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.AddWindowMenuItem:" + ex.Message);
            }
        }

        private void WindowMenuItem_Remove(Control _item)
        {
            try
            {
                int index = 0;
                for (index = 0; index < tsmenuWindow.DropDownItems.Count; index++)
                {
                    ControlItem current = (ControlItem)tsmenuWindow.DropDownItems[index].Tag;
                    if (current.Control.Name.ToLower() == _item.Name.ToLower())
                    {
                        tsmenuWindow.DropDownItems.Remove(tsmenuWindow.DropDownItems[index]);
                        DeinitializeUserControl(current);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.RemoveWindowMenuItem:" + ex.Message);
            }
        }

        private void WindowMenuItem_ClearAllSelection()
        {
            try
            {
                foreach (ToolStripMenuItem menuItem in tsmenuWindow.DropDownItems)
                {
                    if (menuItem.Checked)
                    {
                        menuItem.Checked = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.WindowMenuItem_ClearAllSelection:" + ex.Message);
            }
        }

        public void WindowMenuItem_RunProductRefresh()
        {
            try
            {
                int index = 0;
                for (index = 0; index < tsmenuWindow.DropDownItems.Count; index++)
                {
                    ControlItem current = (ControlItem)tsmenuWindow.DropDownItems[index].Tag;
                    if (current.Control is UclPurchase)
                    {
                        ((UclPurchase)current.Control).RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.RemoveWindowMenuItem:" + ex.Message);
            }
        }
        #endregion

        #region ToolBar Buttons Events


        #endregion

        #region Form Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenDefaultForm();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            if (_ActiveControl != null && _ActiveControl is BaseControl)
            {
                IsHandled = ((BaseControl)_ActiveControl).HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            else if (_ActiveControl != null && _ActiveControl is ReportBaseControl)
            {
                IsHandled = ((ReportBaseControl)_ActiveControl).HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            if (IsHandled == false)
            {
                IsHandled = HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            e.Handled = IsHandled;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool retValue = ExitApplication();
            e.Cancel = !retValue;
        }
        //Process Global Shortcut keys
        private bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                ControlItem _ControlItem = null;
                //F2 -> Counter Sale Add
                if (keyPressed == Keys.F2)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Counter Sale";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclCounterSale;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F3)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Patient Sale";
                    if (General.CurrentSetting.MsetSaleF3KeyForPatientSaleEdit == "Y")
                        _ControlItem.ItemMode = OperationMode.Edit;
                    else
                        _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclPatientSale;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F4)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Debtor Sale";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclDebtorSale;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F5)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Purchase";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclPurchase;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F6)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Debit Note Stock";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclDebitNoteStock;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F7)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Credit Note Stock";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclCreditNoteStock;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F8)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Cash Receipt";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclCashReceipt;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F9)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Doctor";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclDoctor;
                    OpenItem(_ControlItem);
                    retValue = true;
                }

                if (keyPressed == Keys.F10)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Product";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclProduct;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F11)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Similar Products";
                    _ControlItem.ItemMode = OperationMode.View;
                    _ControlItem.Control = _UclSubstitute;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
                if (keyPressed == Keys.F12)
                {
                    _ControlItem = new ControlItem();
                    _ControlItem.ItemName = "Account";
                    _ControlItem.ItemMode = OperationMode.Add;
                    _ControlItem.Control = _UclAccount;
                    OpenItem(_ControlItem);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.HandleShortcutAction:" + ex.Message);
            }
            return retValue;
        }
        #endregion

        #endregion

        #region OutlookBar Events
        private void MainOutlookBar_ItemClicked(OutlookBarBand band, OutlookBarItem item)
        {
            try
            {
                ControlItem _item = (ControlItem)((OutlookBarItem)item).Tag;
                OpenItem(_item);
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.Item_Clicked:" + ex.Message);
                Log.WriteError("FormMain.Item_Clicked:" + ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Other private methods
        private DataTable GetFormNameData()
        {
            DataTable dt = new DataTable();
            try
            {
                DataColumn dc = new DataColumn("ID");
                dt.Columns.Add(dc);
                dc = new DataColumn("ItemName");
                dt.Columns.Add(dc);
                foreach (OutlookBarBand band in MainOutlookBar.Bands)
                {
                    foreach (OutlookBarItem item in band.Items)
                    {
                        if (item.Tag != null && item.Tag is ControlItem)
                        {
                            DataRow dr = dt.NewRow();
                            ControlItem ctrl = (ControlItem)item.Tag;
                            dr["ID"] = ctrl.ItemName;
                            dr["ItemName"] = band.Name + " -> " + item.Name;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }

        private DataTable GetReportNameData()
        {
            DataTable dt = new DataTable();
            try
            {
                DataColumn dc = new DataColumn("ID");
                dt.Columns.Add(dc);
                dc = new DataColumn("ItemName");
                dt.Columns.Add(dc);

                foreach (ToolStripMenuItem subMenuItem in tsmenuReports.DropDownItems)
                {
                    string reportText = tsmenuReports.Text;
                    reportText = reportText.Replace("&", "");
                    foreach (ToolStripMenuItem innerMenuItem in subMenuItem.DropDownItems)
                    {
                        if (innerMenuItem.Tag != null && innerMenuItem.Tag is ControlItem)
                        {
                            DataRow dr = dt.NewRow();
                            ControlItem ctrl = (ControlItem)innerMenuItem.Tag;
                            dr["ID"] = ctrl.ItemName;
                            dr["ItemName"] = reportText + " -> " + subMenuItem.Text + " -> " + innerMenuItem.Text;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }

        private bool IsUserRightAllowed(string formName, OperationMode mode)
        {
            bool retValue = true;
            try
            {
                foreach (UserRights right in UserRightsList)
                {
                    if (right.FormName.ToLower() == formName.ToLower())
                    {
                        switch (mode)
                        {
                            case OperationMode.Add:
                                if (General.CurrentUser.Level > right.AddLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Edit:
                                if (General.CurrentUser.Level > right.EditLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Delete:
                                if (General.CurrentUser.Level > right.DeleteLevel)
                                    retValue = false;
                                break;
                            case OperationMode.View:
                                if (General.CurrentUser.Level > right.ViewLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Report:
                                if (General.CurrentUser.Level > right.ViewLevel)
                                    retValue = false;
                                break;
                        }
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
        #endregion

        #region Other public methods
        public void LoadFavourites()
        {
            try
            {
                tsmenuFavourites.DropDownItems.Clear();
                favouriteList = new Favourite().GetFavouriteList();
                foreach (Favourite fav in favouriteList)
                {
                    FavouriteItem_Add(fav);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void SetFavourite(ControlItem item)
        {
            try
            {
                if (item.Control != null && item.Control is BaseControl)
                {
                    if (((BaseControl)item.Control).Mode == OperationMode.Add)
                    {
                        ((BaseControl)item.Control).ShowFavourite(true);
                    }
                    else
                        ((BaseControl)item.Control).ShowFavourite(false);
                }

                foreach (Favourite fav in favouriteList)
                {
                    if (item.ItemName.ToLower() == fav.Name.ToLower())
                    {
                        if (item.Control != null && item.Control is BaseControl)
                        {
                            ((BaseControl)item.Control).SetFavourite(fav);
                        }
                        else if (item.Control != null && item.Control is ReportBaseControl)
                        {
                            ((ReportBaseControl)item.Control).SetFavourite(fav);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.SetFavourite:" + ex.Message);
            }
        }

        private void FavouriteItem_Add(Favourite fav)
        {
            try
            {
                ControlItem itm = GetControlItem(fav, tsmenuReports);
                if (itm == null)
                    itm = GetControlItem(fav, tsmenuModules);
                if (itm != null)
                {
                    ToolStripMenuItem _FavMenuItem = new ToolStripMenuItem(fav.Name);
                    _FavMenuItem.Name = fav.Name;
                    _FavMenuItem.Click += new EventHandler(this.FavouriteMenuItem_Click);
                    _FavMenuItem.Tag = itm;
                    tsmenuFavourites.DropDownItems.Add(_FavMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.FavouriteItem_Add:" + ex.Message);
            }
        }

        private ControlItem GetControlItem(Favourite fav, ToolStripMenuItem mainItem)
        {
            ControlItem item = null;
            try
            {
                bool isItemFound = false;
                //Find in menu items
                foreach (ToolStripMenuItem mItem in mainItem.DropDownItems)
                {
                    if (isItemFound)
                        break;
                    foreach (ToolStripMenuItem mItemInner in mItem.DropDownItems)
                    {
                        if (mItemInner.Name.ToLower() == fav.Name.ToLower())
                        {
                            ControlItem itemTemp = null;
                            if (mItemInner.DropDownItems.Count > 0)
                            {
                                foreach (ToolStripMenuItem mItemOperation in mItemInner.DropDownItems)
                                {
                                    itemTemp = (ControlItem)mItemOperation.Tag;
                                    if ((int)itemTemp.ItemMode == fav.OperationMode)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        itemTemp = null;
                                    }
                                }
                            }
                            else
                            {
                                itemTemp = (ControlItem)mItemInner.Tag; //For Reports
                            }
                            if (itemTemp != null && IsUserRightAllowed(itemTemp.ItemName, itemTemp.ItemMode))
                            {
                                item = itemTemp;
                                isItemFound = true;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("FormMain.GetControlItem:" + ex.Message);
            }
            return item;
        }
        #endregion

        private void InitializeUserControl(ControlItem _item)
        {
            try
            {
                switch (_item.ItemName)
                {
                    #region Master-I
                    case "Account":
                        _UclAccount = new UclAccount();
                        _UclAccount.Dock = DockStyle.Fill;
                        _UclAccount.Visible = false;
                        _UclAccount.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAccount;
                        break;
                    case "Company":
                        _UclCompany = new UclCompany();
                        _UclCompany.Dock = DockStyle.Fill;
                        _UclCompany.Visible = false;
                        _UclCompany.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCompany;
                        break;
                    case "Doctor":
                        _UclDoctor = new UclDoctor();
                        _UclDoctor.Dock = DockStyle.Fill;
                        _UclDoctor.Visible = false;
                        _UclDoctor.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDoctor;
                        break;
                    case "A/c Group":
                        _UclGroup = new UclGroup();
                        _UclGroup.Dock = DockStyle.Fill;
                        _UclGroup.Visible = false;
                        _UclGroup.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclGroup;
                        break;
                    case "Patient":
                        _UclPatient = new UclPatient();
                        _UclPatient.Dock = DockStyle.Fill;
                        _UclPatient.Visible = false;
                        _UclPatient.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPatient;
                        break;
                    case "Prescription":
                        _UclPrescription = new UclPrescription();
                        _UclPrescription.Dock = DockStyle.Fill;
                        _UclPrescription.Visible = false;
                        _UclPrescription.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPrescription;
                        break;
                    case "Product":
                        _UclProduct = new UclProduct();
                        _UclProduct.Dock = DockStyle.Fill;
                        _UclProduct.Visible = false;
                        _UclProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclProduct;
                        break;
                    case "Scheme":
                        _UclScheme = new UclScheme();
                        _UclScheme.Dock = DockStyle.Fill;
                        _UclScheme.Visible = false;
                        _UclScheme.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclScheme;
                        break;
                    case "Shelf":
                        _UclShelf = new UclShelf();
                        _UclShelf.Dock = DockStyle.Fill;
                        _UclShelf.Visible = false;
                        _UclShelf.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclShelf;
                        break;
                    case "Hospital Patient":
                        _UclHospitalPatient = new UclHospitalPatient();
                        _UclHospitalPatient.Dock = DockStyle.Fill;
                        _UclHospitalPatient.Visible = false;
                        _UclHospitalPatient.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclHospitalPatient;
                        break;
                    #endregion Master-I

                    #region Master-II
                    case "Area":
                        _UclArea = new UclArea();
                        _UclArea.Dock = DockStyle.Fill;
                        _UclArea.Visible = false;
                        _UclArea.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclArea;
                        break;
                    case "Bank":
                        _UclBank = new UclBank();
                        _UclBank.Dock = DockStyle.Fill;
                        _UclBank.Visible = false;
                        _UclBank.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBank;
                        break;
                    case "Branch":
                        _UclBranch = new UclBranch();
                        _UclBranch.Dock = DockStyle.Fill;
                        _UclBranch.Visible = false;
                        _UclBranch.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBranch;
                        break;
                    case "Customer":
                        _UclCustomer = new UclCustomer();
                        _UclCustomer.Dock = DockStyle.Fill;
                        _UclCustomer.Visible = false;
                        _UclCustomer.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCustomer;
                        break;
                    case "EmailID":
                        _UclEmailID = new UclEmailID();
                        _UclEmailID.Dock = DockStyle.Fill;
                        _UclEmailID.Visible = false;
                        _UclEmailID.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclEmailID;
                        break;
                    case "Generic Name":
                        _UclGenericCategory = new UclGenericCategory();
                        _UclGenericCategory.Dock = DockStyle.Fill;
                        _UclGenericCategory.Visible = false;
                        _UclGenericCategory.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclGenericCategory;
                        break;
                    case "Messages":
                        _UclMessages = new UclMessages();
                        _UclMessages.Dock = DockStyle.Fill;
                        _UclMessages.Visible = false;
                        _UclMessages.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMessages;
                        break;
                    case "ProductCategory":
                        _UclProdCategory = new UclProdCategory();
                        _UclProdCategory.Dock = DockStyle.Fill;
                        _UclProdCategory.Visible = false;
                        _UclProdCategory.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclProdCategory;
                        break;
                    case "Salesman":
                        _UclSalesman = new UclSalesman();
                        _UclSalesman.Dock = DockStyle.Fill;
                        _UclSalesman.Visible = false;
                        _UclSalesman.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSalesman;
                        break;
                    case "Ward":
                        _UclWard = new UclWard();
                        _UclWard.Dock = DockStyle.Fill;
                        _UclWard.Visible = false;
                        _UclWard.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclWard;
                        break;
                    #endregion Master-II

                    # region Links

                    case "DebtorProduct":
                        _UclDebtorProduct = new UclDebtorProduct();
                        _UclDebtorProduct.Dock = DockStyle.Fill;
                        _UclDebtorProduct.Visible = false;
                        _UclDebtorProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebtorProduct;
                        break;
                    case "DrugGrouping (Generic)":
                        _UclDrugGrouping = new UclDrugGrouping();
                        _UclDrugGrouping.Dock = DockStyle.Fill;
                        _UclDrugGrouping.Visible = false;
                        _UclDrugGrouping.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDrugGrouping;
                        break;
                    case "PartyCompany":
                        _UclPartyCompany = new UclPartyCompany();
                        _UclPartyCompany.Dock = DockStyle.Fill;
                        _UclPartyCompany.Visible = false;
                        _UclPartyCompany.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPartyCompany;
                        break;
                    case "ShelfProduct":
                        _UclLinkShelfProduct = new UclLinkShelfProduct();
                        _UclLinkShelfProduct.Dock = DockStyle.Fill;
                        _UclLinkShelfProduct.Visible = false;
                        _UclLinkShelfProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclLinkShelfProduct;
                        break;
                    case "ProductScheduleH1":
                        _UclLinkProductScheduleH1 = new UclLinkProductScheduleH1();
                        _UclLinkProductScheduleH1.Dock = DockStyle.Fill;
                        _UclLinkProductScheduleH1.Visible = false;
                        _UclLinkProductScheduleH1.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclLinkProductScheduleH1;
                        break;
                    #endregion Links
                    # region Distributor

                    case "Distributor Sale":
                        _UclDistributorSale = new UclDistributorSale();
                        _UclDistributorSale.Dock = DockStyle.Fill;
                        _UclDistributorSale.Visible = false;
                        _UclDistributorSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDistributorSale;
                        break;
                    #endregion Distributor
                    #region Sale
                    case "Counter Sale":
                        _UclCounterSale = new UclCounterSale();
                        _UclCounterSale.Dock = DockStyle.Fill;
                        _UclCounterSale.Visible = false;
                        _UclCounterSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCounterSale;
                        break;
                    case "Counter Sale Edit":
                        _UclCounterSaleEdit = new UclCounterSaleEdit();
                        _UclCounterSaleEdit.Dock = DockStyle.Fill;
                        _UclCounterSaleEdit.Visible = false;
                        _UclCounterSaleEdit.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCounterSaleEdit;
                        break;
                    case "Patient Sale":
                        _UclPatientSale = new UclPatientSale();
                        _UclPatientSale.Dock = DockStyle.Fill;
                        _UclPatientSale.Visible = false;
                        _UclPatientSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPatientSale;
                        break;
                    case "Debtor Sale":
                        _UclDebtorSale = new UclDebtorSale();
                        _UclDebtorSale.Dock = DockStyle.Fill;
                        _UclDebtorSale.Visible = false;
                        _UclDebtorSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebtorSale;
                        break;
                    case "Hospital Sale":
                        _UclHospitalSale = new UclHospitalSale();
                        _UclHospitalSale.Dock = DockStyle.Fill;
                        _UclHospitalSale.Visible = false;
                        _UclHospitalSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclHospitalSale;
                        break;
                    case "Institutional Sale":
                        _UclInstitutionalSale = new UclInstitutionalSale();
                        _UclInstitutionalSale.Dock = DockStyle.Fill;
                        _UclInstitutionalSale.Visible = false;
                        _UclInstitutionalSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclInstitutionalSale;
                        break;
                    case "Cash Sale":
                        _UclSaleWithoutStock = new UclSaleWithoutStock();
                        _UclSaleWithoutStock.Dock = DockStyle.Fill;
                        _UclSaleWithoutStock.Visible = false;
                        _UclSaleWithoutStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleWithoutStock;
                        break;

                    #endregion Sale

                    #region Purchase
                    case "Purchase":
                        _UclPurchase = new UclPurchase();
                        _UclPurchase.Dock = DockStyle.Fill;
                        _UclPurchase.Visible = false;
                        _UclPurchase.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchase;
                        break;
                    case "Purchase II":
                        _UclPurchaseWithoutStock = new UclPurchaseWithoutStock();
                        _UclPurchaseWithoutStock.Dock = DockStyle.Fill;
                        _UclPurchaseWithoutStock.Visible = false;
                        _UclPurchaseWithoutStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseWithoutStock;
                        break;
                    #endregion Purchase

                    #region DebitNote
                    case "Debit Note Stock":
                        _UclDebitNoteStock = new UclDebitNotestock();
                        _UclDebitNoteStock.Dock = DockStyle.Fill;
                        _UclDebitNoteStock.Visible = false;
                        _UclDebitNoteStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebitNoteStock;
                        break;
                    case "Debit Note Amount":
                        _UclDebitNoteAmount = new UclDebitNoteAmount();
                        _UclDebitNoteAmount.Dock = DockStyle.Fill;
                        _UclDebitNoteAmount.Visible = false;
                        _UclDebitNoteAmount.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebitNoteAmount;
                        break;
                    case "Debit Note Expiry":
                        _UclDebitNoteExpiry = new UclDebitNoteExpiry();
                        _UclDebitNoteExpiry.Dock = DockStyle.Fill;
                        _UclDebitNoteExpiry.Visible = false;
                        _UclDebitNoteExpiry.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebitNoteExpiry;
                        break;
                    case "Stock Out":
                        _UclStockOut = new UclStockOut();
                        _UclStockOut.Dock = DockStyle.Fill;
                        _UclStockOut.Visible = false;
                        _UclStockOut.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockOut;
                        break;                   
                    #endregion Debit Note

                    #region CreditNote

                    case "Credit Note Stock":
                        _UclCreditNoteStock = new UclCreditNoteStock();
                        _UclCreditNoteStock.Dock = DockStyle.Fill;
                        _UclCreditNoteStock.Visible = false;
                        _UclCreditNoteStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditNoteStock;
                        break;
                    case "Credit Note Amount":
                        _UclCreditNoteAmount = new UclCreditNoteAmount();
                        _UclCreditNoteAmount.Dock = DockStyle.Fill;
                        _UclCreditNoteAmount.Visible = false;
                        _UclCreditNoteAmount.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditNoteAmount;
                        break;
                    case "Stock In":
                        _UclStockIn = new UclStockIn();
                        _UclStockIn.Dock = DockStyle.Fill;
                        _UclStockIn.Visible = false;
                        _UclStockIn.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockIn;
                        break;                   
                    #endregion Credit Note

                    #region Cash
                    case "Cash Receipt":
                        _UclCashReceipt = new UclCashReceipt();
                        _UclCashReceipt.Dock = DockStyle.Fill;
                        _UclCashReceipt.Visible = false;
                        _UclCashReceipt.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashReceipt;
                        break;
                    case "Cash Payment":
                        _UclCashPayment = new UclCashPayment();
                        _UclCashPayment.Dock = DockStyle.Fill;
                        _UclCashPayment.Visible = false;
                        _UclCashPayment.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashPayment;
                        break;
                    case "Cash Expenses":
                        _UclCashExpenses = new UclCashExpenses();
                        _UclCashExpenses.Dock = DockStyle.Fill;
                        _UclCashExpenses.Visible = false;
                        _UclCashExpenses.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashExpenses;
                        break;                   
                    #endregion Cash

                    #region Bank
                    case "Bank Receipt":
                        _UclBankReceipt = new UclBankReceipt();
                        _UclBankReceipt.Dock = DockStyle.Fill;
                        _UclBankReceipt.Visible = false;
                        _UclBankReceipt.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBankReceipt;
                        break;
                    case "Bank Payment":
                        _UclBankPayment = new UclBankPayment();
                        _UclBankPayment.Dock = DockStyle.Fill;
                        _UclBankPayment.Visible = false;
                        _UclBankPayment.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBankPayment;
                        break;
                    case "Cheque Return":
                        _UclChequeReturn = new UclChequeReturn();
                        _UclChequeReturn.Dock = DockStyle.Fill;
                        _UclChequeReturn.Visible = false;
                        _UclChequeReturn.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclChequeReturn;
                        break;
                    case "Bank Expenses":
                        _UclBankExpenses = new UclBankExpenses();
                        _UclBankExpenses.Dock = DockStyle.Fill;
                        _UclBankExpenses.Visible = false;
                        _UclBankExpenses.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBankExpenses;
                        break;
                    case "Do Bank Reconciliation":
                        _UclDoBankReconciliation = new UclDoBankReconciliation();
                        _UclDoBankReconciliation.Dock = DockStyle.Fill;
                        _UclDoBankReconciliation.Visible = false;
                        _UclDoBankReconciliation.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDoBankReconciliation;
                        break;
                    #endregion Bank

                    #region contra
                    case "Contra Entry":
                        _UclContraEntry = new UclContraEntry();
                        _UclContraEntry.Dock = DockStyle.Fill;
                        _UclContraEntry.Visible = false;
                        _UclContraEntry.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclContraEntry;
                        break;
                    #endregion contra

                    #region other
                    case "Opening Stock":
                        _UclOPStock = new UclOPStock();
                        _UclOPStock.Dock = DockStyle.Fill;
                        _UclOPStock.Visible = false;
                        _UclOPStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclOPStock;
                        break;
                    case "Correction In Rate":
                        _UclCorrectioninRate = new UclCorrectioninRate();
                        _UclCorrectioninRate.Dock = DockStyle.Fill;
                        _UclCorrectioninRate.Visible = false;
                        _UclCorrectioninRate.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCorrectioninRate;
                        break;
                    case "Users":
                        _UclUser = new UclUser();
                        _UclUser.Dock = DockStyle.Fill;
                        _UclUser.Visible = false;
                        _UclUser.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclUser;
                        break;
                    //case "User Rights":
                    //    _UclUserRights = new UclUserRights();
                    //    _UclUserRights.Dock = DockStyle.Fill;
                    //    _UclUserRights.Visible = false;
                    //    _UclUserRights.ExitClicked += new EventHandler(Item_ExitClicked);
                    //    _item.Control = _UclUserRights;
                    //    break;
                    //case "Settings":
                    //    _UclSettingsSale = new UclSettingsSale();
                    //    _UclSettingsSale.Dock = DockStyle.Fill;
                    //    _UclSettingsSale.Visible = false;
                    //    _UclSettingsSale.ExitClicked += new EventHandler(Item_ExitClicked);
                    //    _item.Control = _UclSettingsSale;
                    //    break;
                    //case "Print Settings":
                    //    _UclSettingsForPrint = new UclSettingsForPrint();
                    //    _UclSettingsForPrint.Dock = DockStyle.Fill;
                    //    _UclSettingsForPrint.Visible = false;
                    //    _UclSettingsForPrint.ExitClicked += new EventHandler(Item_ExitClicked);
                    //    _item.Control = _UclSettingsForPrint;
                    //    break;
                    case "Similar Products":
                        _UclSubstitute = new UclSubstitute();
                        _UclSubstitute.Dock = DockStyle.Fill;
                        _UclSubstitute.Visible = false;
                        _UclSubstitute.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSubstitute;
                        break;
                    case "Operator":
                        _UclOperator = new UclOperator();
                        _UclOperator.Dock = DockStyle.Fill;
                        _UclOperator.Visible = false;
                        _UclOperator.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclOperator;
                        break;
                    case "Bill Reprint":
                        _UclSpecialSale = new UclSpecialSale();
                        _UclSpecialSale.Dock = DockStyle.Fill;
                        _UclSpecialSale.Visible = false;
                        _UclSpecialSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSpecialSale;
                         break;
                    case "Bulk Bill Reprint":
                         _UclBulkPrintPartywiseSale = new UclBulkPrintPartywiseSale();
                         _UclBulkPrintPartywiseSale.Dock = DockStyle.Fill;
                         _UclBulkPrintPartywiseSale.Visible = false;
                         _UclBulkPrintPartywiseSale.ExitClicked += new EventHandler(Item_ExitClicked);
                         _item.Control = _UclBulkPrintPartywiseSale;                       
                        break;
                    case "BarCode Print":
                        _UclBarCodePrint = new UclBarCodePrint();
                        _UclBarCodePrint.Dock = DockStyle.Fill;
                        _UclBarCodePrint.Visible = false;
                        _UclBarCodePrint.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBarCodePrint;
                        break;
                        
                    #endregion other

                    #region Statement
                    case "Sale ALL":
                        _UclStatementSale = new UclStatementSale();
                        _UclStatementSale.Dock = DockStyle.Fill;
                        _UclStatementSale.Visible = false;
                        _UclStatementSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStatementSale;
                        break;
                    case "Purchase (15 Days)":
                        _UclStatementPurchase15Days = new UclStatementPurchase15Days();
                        _UclStatementPurchase15Days.Dock = DockStyle.Fill;
                        _UclStatementPurchase15Days.Visible = false;
                        _UclStatementPurchase15Days.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStatementPurchase15Days;
                        break;
                    case "Hospital Statement":
                        _UclStatementHospital = new UclStatementHospital();
                        _UclStatementHospital.Dock = DockStyle.Fill;
                        _UclStatementHospital.Visible = false;
                        _UclStatementHospital.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStatementHospital;
                        break;
                    case "Sale (Party)":
                        _UclStatementPartywiseSale = new UclStatementPartywiseSale();
                        _UclStatementPartywiseSale.Dock = DockStyle.Fill;
                        _UclStatementPartywiseSale.Visible = false;
                        _UclStatementPartywiseSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStatementPartywiseSale;
                        break;
                    case "Purchase (Party)":
                        _UclStatementPartywisePurchase = new UclStatementPartywisePurchase();
                        _UclStatementPartywisePurchase.Dock = DockStyle.Fill;
                        _UclStatementPartywisePurchase.Visible = false;
                        _UclStatementPartywisePurchase.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStatementPartywisePurchase;
                        break;
                    #endregion Statement

                    #region Reports
                    #region List
                    case "CompanyList":
                        _UclCompanyList = new UclListCompany();
                        _UclCompanyList.Dock = DockStyle.Fill;
                        _UclCompanyList.Visible = false;
                        _UclCompanyList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCompanyList;
                        break;
                    case "ProductList (ALL)":
                        _UclProductList = new UclListProductAll();
                        _UclProductList.Dock = DockStyle.Fill;
                        _UclProductList.Visible = false;
                        _UclProductList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclProductList;
                        break;
                    case "Product List":
                        _UclProductListBySelection = new UclListProductBySelection();
                        _UclProductListBySelection.Dock = DockStyle.Fill;
                        _UclProductListBySelection.Visible = false;
                        _UclProductListBySelection.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclProductListBySelection;
                        break;
                    case "Account List":
                        _UclAccountList = new UclListAccount();
                        _UclAccountList.Dock = DockStyle.Fill;
                        _UclAccountList.Visible = false;
                        _UclAccountList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAccountList;
                        break;
                    case "Doctor List":
                        _UclDoctorList = new UclListDoctor();
                        _UclDoctorList.Dock = DockStyle.Fill;
                        _UclDoctorList.Visible = false;
                        _UclDoctorList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDoctorList;
                        break;
                    case "Patient List":
                        _UclPatientList = new UclListPatient();
                        _UclPatientList.Dock = DockStyle.Fill;
                        _UclPatientList.Visible = false;
                        _UclPatientList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPatientList;
                        break;
                    case "Shelf List":
                        _UclShelfList = new UclListShelf();
                        _UclShelfList.Dock = DockStyle.Fill;
                        _UclShelfList.Visible = false;
                        _UclShelfList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclShelfList;
                        break;
                    case "Bank List":
                        _UclBankList = new UclListBank();
                        _UclBankList.Dock = DockStyle.Fill;
                        _UclBankList.Visible = false;
                        _UclBankList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBankList;
                        break;
                    case "Branch List":
                        _UclBranchList = new UclListBranch();
                        _UclBranchList.Dock = DockStyle.Fill;
                        _UclBranchList.Visible = false;
                        _UclBranchList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclBranchList;
                        break;
                    case "Area List":
                        _UclAreaList = new UclListArea();
                        _UclAreaList.Dock = DockStyle.Fill;
                        _UclAreaList.Visible = false;
                        _UclAreaList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAreaList;
                        break;
                    case "Generic Category List":
                        _UclGenericCategoryList = new UclListGenericCategory();
                        _UclGenericCategoryList.Dock = DockStyle.Fill;
                        _UclGenericCategoryList.Visible = false;
                        _UclGenericCategoryList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclGenericCategoryList;
                        break;
                    case "Todays Cheques List":
                        _UclListTodaysCheques = new UclListTodaysCheques();
                        _UclListTodaysCheques.Dock = DockStyle.Fill;
                        _UclListTodaysCheques.Visible = false;
                        _UclListTodaysCheques.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclListTodaysCheques;
                        break;
                    case "Operator List":
                        _UclListOperator = new UclListOperator();
                        _UclListOperator.Dock = DockStyle.Fill;
                        _UclListOperator.Visible = false;
                        _UclListOperator.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclListOperator;
                        break;
                    case "Cheque Return List":
                        _UclListChequeReturn = new UclVouChequeReturnList();
                        _UclListChequeReturn.Dock = DockStyle.Fill;
                        _UclListChequeReturn.Visible = false;
                        _UclListChequeReturn.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclListChequeReturn;
                        break;
                    #endregion List

                    #region VoucherList

                    case "Cash Receipt List":
                        _UclCashReceiptList = new UclVouCashReceiptList();
                        _UclCashReceiptList.Dock = DockStyle.Fill;
                        _UclCashReceiptList.Visible = false;
                        _UclCashReceiptList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashReceiptList;
                        break;
                    case "Cash Payment List":
                        _UclCashPaidList = new UclVouCashPaidList();
                        _UclCashPaidList.Dock = DockStyle.Fill;
                        _UclCashPaidList.Visible = false;
                        _UclCashPaidList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashPaidList;
                        break;
                    case "Cash Expenses List":
                        _UclCashExpensesList = new UclVouCashExpensesList();
                        _UclCashExpensesList.Dock = DockStyle.Fill;
                        _UclCashExpensesList.Visible = false;
                        _UclCashExpensesList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCashExpensesList;
                        break;
                    case "Cheque Receipt":
                        _UclChequeReceiptList = new UclVouChequeReceiptList();
                        _UclChequeReceiptList.Dock = DockStyle.Fill;
                        _UclChequeReceiptList.Visible = false;
                        _UclChequeReceiptList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclChequeReceiptList;
                        break;
                    case "Cheque Paid":
                        _UclChequePaidList = new UclVouChequePaidList();
                        _UclChequePaidList.Dock = DockStyle.Fill;
                        _UclChequePaidList.Visible = false;
                        _UclChequePaidList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclChequePaidList;
                        break;
                    case "Bank Expenses List":
                        _UclVouBankExpensesList = new UclVouBankExpensesList();
                        _UclVouBankExpensesList.Dock = DockStyle.Fill;
                        _UclVouBankExpensesList.Visible = false;
                        _UclVouBankExpensesList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVouBankExpensesList;
                        break;
                    case "Statement Purchase":
                        _UclVouStatementPurchaseList = new UclVouStatementPurchaseList();
                        _UclVouStatementPurchaseList.Dock = DockStyle.Fill;
                        _UclVouStatementPurchaseList.Visible = false;
                        _UclVouStatementPurchaseList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVouStatementPurchaseList;
                        break;
                    case "Statement Sale":
                        _UclVouStatementSaleList = new UclVouStatementSaleList();
                        _UclVouStatementSaleList.Dock = DockStyle.Fill;
                        _UclVouStatementSaleList.Visible = false;
                        _UclVouStatementSaleList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVouStatementSaleList;
                        break;
                    case "Cheque Received But Not Cleared":
                        _UclVouChequeReceivedButNotCleared = new UclVouChequeReceivedButNotCleared();
                        _UclVouChequeReceivedButNotCleared.Dock = DockStyle.Fill;
                        _UclVouChequeReceivedButNotCleared.Visible = false;
                        _UclVouChequeReceivedButNotCleared.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVouChequeReceivedButNotCleared;
                        break;
                    case "Cheque Paid But Not Cleared":
                        _UclVouChequePaidButNotCleared = new UclVouChequePaidButNotCleared();
                        _UclVouChequePaidButNotCleared.Dock = DockStyle.Fill;
                        _UclVouChequePaidButNotCleared.Visible = false;
                        _UclVouChequePaidButNotCleared.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVouChequePaidButNotCleared;
                        break;
                    #endregion VoucherList

                    #region DebitCreditNote

                    case "Debit Note":
                        _UclDebitNoteList = new UclDebitNoteList();
                        _UclDebitNoteList.Dock = DockStyle.Fill;
                        _UclDebitNoteList.Visible = false;
                        _UclDebitNoteList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebitNoteList;
                        break;
                    case "Debit Note (Product)":
                        _UclDebitNoteListProduct = new UclDebitNoteListProduct();
                        _UclDebitNoteListProduct.Dock = DockStyle.Fill;
                        _UclDebitNoteListProduct.Visible = false;
                        _UclDebitNoteListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclDebitNoteListProduct;
                        break;
                    case "Stock Out List":
                        _UclStockOutList = new UclDebitNoteStockOutList();
                        _UclStockOutList.Dock = DockStyle.Fill;
                        _UclStockOutList.Visible = false;
                        _UclStockOutList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockOutList;
                        break;
                    case "Stock Out (Product)":
                        _UclStockOutListProduct = new UclDebitNoteStockOutListProduct();
                        _UclStockOutListProduct.Dock = DockStyle.Fill;
                        _UclStockOutListProduct.Visible = false;
                        _UclStockOutListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockOutListProduct;
                        break;
                    case "Credit Note":
                        _UclCreditNoteList = new UclCreditNoteList();
                        _UclCreditNoteList.Dock = DockStyle.Fill;
                        _UclCreditNoteList.Visible = false;
                        _UclCreditNoteList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditNoteList;
                        break;
                    case "Credit Note (Product)":
                        _UclCreditNoteListProduct = new UclCreditNoteListProduct();
                        _UclCreditNoteListProduct.Dock = DockStyle.Fill;
                        _UclCreditNoteListProduct.Visible = false;
                        _UclCreditNoteListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditNoteListProduct;
                        break;
                    case "Stock IN":
                        _UclStockInList = new UclCreditNoteStockInList();
                        _UclStockInList.Dock = DockStyle.Fill;
                        _UclStockInList.Visible = false;
                        _UclStockInList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockInList;
                        break;
                    case "Stock IN (Product)":
                        _UclStockInListProduct = new UclCreditNoteStockInListProduct();
                        _UclStockInListProduct.Dock = DockStyle.Fill;
                        _UclStockInListProduct.Visible = false;
                        _UclStockInListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockInListProduct;
                        break;
                    case "DB/CR Note(Party)":
                        _UclCreditDebitNotePartyList = new UclCreditDebitNotePartyList();
                        _UclCreditDebitNotePartyList.Dock = DockStyle.Fill;
                        _UclCreditDebitNotePartyList.Visible = false;
                        _UclCreditDebitNotePartyList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditDebitNotePartyList;
                        break;
                    case "DB/CR Note(Product)":
                        _UclCreditDebitNoteProductList = new UclCreditDebitNoteProductList();
                        _UclCreditDebitNoteProductList.Dock = DockStyle.Fill;
                        _UclCreditDebitNoteProductList.Visible = false;
                        _UclCreditDebitNoteProductList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclCreditDebitNoteProductList;
                        break;
                    case "Stock IN/OUT(Product)":
                        _UclStockINOUTListProduct = new UclCreditDebitStockINOUTListProduct();
                        _UclStockINOUTListProduct.Dock = DockStyle.Fill;
                        _UclStockINOUTListProduct.Visible = false;
                        _UclStockINOUTListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockINOUTListProduct;
                        break;

                    #endregion DebitCreditNote

                    #region Scheme

                    case "Scheme List (ALL)":
                        _UclSchemeListAll = new UclSchemeListAll();
                        _UclSchemeListAll.Dock = DockStyle.Fill;
                        _UclSchemeListAll.Visible = false;
                        _UclSchemeListAll.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSchemeListAll;
                        break;
                    case "Scheme List (Company)":
                        _UclSchemeListCompanywise = new UclSchemeListCompanywise();
                        _UclSchemeListCompanywise.Dock = DockStyle.Fill;
                        _UclSchemeListCompanywise.Visible = false;
                        _UclSchemeListCompanywise.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSchemeListCompanywise;
                        break;
                    case "Scheme List (Product/Purchase)":
                        _UclSchemeListProductPurchase = new UclSchemeListProductPurchase();
                        _UclSchemeListProductPurchase.Dock = DockStyle.Fill;
                        _UclSchemeListProductPurchase.Visible = false;
                        _UclSchemeListProductPurchase.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSchemeListProductPurchase;
                        break;
                    #endregion Scheme

                    #region Purchase

                    case "Purchase (Product)":
                        _UclPurchaseListProductWise = new UclPurchaseListProduct();
                        _UclPurchaseListProductWise.Dock = DockStyle.Fill;
                        _UclPurchaseListProductWise.Visible = false;
                        _UclPurchaseListProductWise.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListProductWise;
                        break;
                    case "Purchase (Batch)":
                        _UclPurchaseListProductBatch = new UclPurchaseListProductBatch();
                        _UclPurchaseListProductBatch.Dock = DockStyle.Fill;
                        _UclPurchaseListProductBatch.Visible = false;
                        _UclPurchaseListProductBatch.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListProductBatch;
                        break;
                    case "Products Created in Current Year":
                        _UclPurchaseListNewProduct = new UclPurchaseListNewProduct();
                        _UclPurchaseListNewProduct.Dock = DockStyle.Fill;
                        _UclPurchaseListNewProduct.Visible = false;
                        _UclPurchaseListNewProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListNewProduct;
                        break;
                    case "Purchase (Party/Product)":
                        _UclPurchaseListPartyProduct = new UclPurchaseListPartyProduct();
                        _UclPurchaseListPartyProduct.Dock = DockStyle.Fill;
                        _UclPurchaseListPartyProduct.Visible = false;
                        _UclPurchaseListPartyProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListPartyProduct;
                        break;
                    case "Purchase (Partywise Bills)":
                        _UclPurchaseListPartywisebills = new UclPurchaseListPartywisebills();
                        _UclPurchaseListPartywisebills.Dock = DockStyle.Fill;
                        _UclPurchaseListPartywisebills.Visible = false;
                        _UclPurchaseListPartywisebills.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListPartywisebills;
                        break;
                    case "Purchase (Discount)":
                        _UclPurchaseListDiscount = new UclPurchaseListDiscount();
                        _UclPurchaseListDiscount.Dock = DockStyle.Fill;
                        _UclPurchaseListDiscount.Visible = false;
                        _UclPurchaseListDiscount.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListDiscount;
                        break;
                    case "Purchase (All Party Summary)":
                        _UclPurchaseListAllPartySummary = new UclPurchaseListAllPartySummary();
                        _UclPurchaseListAllPartySummary.Dock = DockStyle.Fill;
                        _UclPurchaseListAllPartySummary.Visible = false;
                        _UclPurchaseListAllPartySummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListAllPartySummary;
                        break;
                    case "Purchase (Category)":
                        _UclPurchaseListCategory = new UclPurchaseListCategory();
                        _UclPurchaseListCategory.Dock = DockStyle.Fill;
                        _UclPurchaseListCategory.Visible = false;
                        _UclPurchaseListCategory.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListCategory;
                        break;
                    case "Purchase (Company)":
                        _UclPurchaseListCompany = new UclPurchaseListCompany();
                        _UclPurchaseListCompany.Dock = DockStyle.Fill;
                        _UclPurchaseListCompany.Visible = false;
                        _UclPurchaseListCompany.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListCompany;
                        break;
                    case "Purchase (Daily)":
                        _UclPurchaseListDaily = new UclPurchaseListDaily();
                        _UclPurchaseListDaily.Dock = DockStyle.Fill;
                        _UclPurchaseListDaily.Visible = false;
                        _UclPurchaseListDaily.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclPurchaseListDaily;
                        break;
                    #endregion Purchase

                    #region Sale

                    case "Sale (Daily)":
                        _UclSaleListDailySale = new UclSaleListDailySale();
                        _UclSaleListDailySale.Dock = DockStyle.Fill;
                        _UclSaleListDailySale.Visible = false;
                        _UclSaleListDailySale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListDailySale;
                        break;
                    case "Sale (Product/Batch)":
                        _UclSaleListProductBatch = new UclSaleListProductBatch();
                        _UclSaleListProductBatch.Dock = DockStyle.Fill;
                        _UclSaleListProductBatch.Visible = false;
                        _UclSaleListProductBatch.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListProductBatch;
                        break;
                    case "Sale (Product)":
                        _UclSaleListProduct = new UclSaleListProduct();
                        _UclSaleListProduct.Dock = DockStyle.Fill;
                        _UclSaleListProduct.Visible = false;
                        _UclSaleListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListProduct;
                        break;
                    case "Sale (Party Summary)":
                        _UclSaleListPartySaleSummary = new UclSaleListPartySaleSummary();
                        _UclSaleListPartySaleSummary.Dock = DockStyle.Fill;
                        _UclSaleListPartySaleSummary.Visible = false;
                        _UclSaleListPartySaleSummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListPartySaleSummary;
                        break;
                    case "Sale (Regular Party Product)":
                        _UclSaleListRegularPartyProduct = new UclSaleListRegularPartyProduct();
                        _UclSaleListRegularPartyProduct.Dock = DockStyle.Fill;
                        _UclSaleListRegularPartyProduct.Visible = false;
                        _UclSaleListRegularPartyProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListRegularPartyProduct;
                        break;
                    case "Sale (Scheduled Drugs)":
                        _UclSaleListSheduledDrug = new UclSaleListSheduledDrug();
                        _UclSaleListSheduledDrug.Dock = DockStyle.Fill;
                        _UclSaleListSheduledDrug.Visible = false;
                        _UclSaleListSheduledDrug.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListSheduledDrug;
                        break;
                    case "Sale (Patient)":
                        _UclSaleListPatient = new UclSaleListPatient();
                        _UclSaleListPatient.Dock = DockStyle.Fill;
                        _UclSaleListPatient.Visible = false;
                        _UclSaleListPatient.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListPatient;
                        break;
                    case "Sale (Doctor)":
                        _UclSaleListDoctor = new UclSaleListDoctor();
                        _UclSaleListDoctor.Dock = DockStyle.Fill;
                        _UclSaleListDoctor.Visible = false;
                        _UclSaleListDoctor.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListDoctor;
                        break;
                    case "Sale (Category)":
                        _UclSaleListCategory = new UclSaleListCategory();
                        _UclSaleListCategory.Dock = DockStyle.Fill;
                        _UclSaleListCategory.Visible = false;
                        _UclSaleListCategory.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListCategory;
                        break;
                    case "Sale (PartywiseBills)":
                        _UclSaleListPartywiseBills = new UclSaleListPartywiseBills();
                        _UclSaleListPartywiseBills.Dock = DockStyle.Fill;
                        _UclSaleListPartywiseBills.Visible = false;
                        _UclSaleListPartywiseBills.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListPartywiseBills;
                        break;
                    case "Sale (Doctor/Company)":
                        _UclSaleListDoctorCompany = new UclSaleListDoctorCompany();
                        _UclSaleListDoctorCompany.Dock = DockStyle.Fill;
                        _UclSaleListDoctorCompany.Visible = false;
                        _UclSaleListDoctorCompany.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListDoctorCompany;
                        break;
                    case "Sale (Product Summary)":
                        _UclSaleListDaywiseProductSummary = new UclSaleListProductSummary();
                        _UclSaleListDaywiseProductSummary.Dock = DockStyle.Fill;
                        _UclSaleListDaywiseProductSummary.Visible = false;
                        _UclSaleListDaywiseProductSummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListDaywiseProductSummary;
                        break;
                    case "Sale (Admit Patients)":
                        _UclSaleListAdmitPatient = new UclSaleListAdmitPatient();
                        _UclSaleListAdmitPatient.Dock = DockStyle.Fill;
                        _UclSaleListAdmitPatient.Visible = false;
                        _UclSaleListAdmitPatient.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListAdmitPatient;
                        break;
                    case "Sale (Operator)":
                        _UclSaleListOperator = new UclSaleListOperator();
                        _UclSaleListOperator.Dock = DockStyle.Fill;
                        _UclSaleListOperator.Visible = false;
                        _UclSaleListOperator.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListOperator;
                        break;
                    case "Sale (IPD)":
                        _UclSaleListIPD = new UclSaleListIPD();
                        _UclSaleListIPD.Dock = DockStyle.Fill;
                        _UclSaleListIPD.Visible = false;
                        _UclSaleListIPD.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListIPD;
                        break;
                    case "Sale (OPD)":
                        _UclSaleListOPD = new UclSaleListOPD();
                        _UclSaleListOPD.Dock = DockStyle.Fill;
                        _UclSaleListOPD.Visible = false;
                        _UclSaleListOPD.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListOPD;
                        break;
                    case "Sale (CreditCard)":
                        _UclSaleListCreditCard = new UclSaleListCreditCard();
                        _UclSaleListCreditCard.Dock = DockStyle.Fill;
                        _UclSaleListCreditCard.Visible = false;
                        _UclSaleListCreditCard.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleListCreditCard;
                        break;
                    case "Sale (Daily Products)":
                        _UclSaleDailyProducts = new UclSaleListDailyProducts();
                        _UclSaleDailyProducts.Dock = DockStyle.Fill;
                        _UclSaleDailyProducts.Visible = false;
                        _UclSaleDailyProducts.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclSaleDailyProducts;
                        break;

                    #endregion Sale

                    #region Stock

                    case "Current Stock":
                        _UclStockListCurrentStock = new UclStockListCurrentStock();
                        _UclStockListCurrentStock.Dock = DockStyle.Fill;
                        _UclStockListCurrentStock.Visible = false;
                        _UclStockListCurrentStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListCurrentStock;
                        break;
                    case "Current Stock (Batch)":
                        _UclStockListBatchwise = new UclStockListBatchwise();
                        _UclStockListBatchwise.Dock = DockStyle.Fill;
                        _UclStockListBatchwise.Visible = false;
                        _UclStockListBatchwise.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListBatchwise;
                        break;
                    case "Current Stock (Shelf)":
                        _UclStockListShelf = new UclStockListShelf();
                        _UclStockListShelf.Dock = DockStyle.Fill;
                        _UclStockListShelf.Visible = false;
                        _UclStockListShelf.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListShelf;
                        break;
                    case "Non Moving":
                        _UclStockListNonMoving = new UclStockListNonMoving();
                        _UclStockListNonMoving.Dock = DockStyle.Fill;
                        _UclStockListNonMoving.Visible = false;
                        _UclStockListNonMoving.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListNonMoving;
                        break;
                    case "All Products":
                        _UclStockListAll = new UclStockListAll();
                        _UclStockListAll.Dock = DockStyle.Fill;
                        _UclStockListAll.Visible = false;
                        _UclStockListAll.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListAll;
                        break;
                    case "Stock n Sale":
                        _UclStockListStocknSale = new UclStockListStocknSale();
                        _UclStockListStocknSale.Dock = DockStyle.Fill;
                        _UclStockListStocknSale.Visible = false;
                        _UclStockListStocknSale.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListStocknSale;
                        break;
                    case "Product Ledger":
                        _UclStockListProductLedger = new UclStockListProductLedger();
                        _UclStockListProductLedger.Dock = DockStyle.Fill;
                        _UclStockListProductLedger.Visible = false;
                        _UclStockListProductLedger.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListProductLedger;
                        break;
                    case "Category Summary":
                        _UclStockListCategorySummary = new UclStockListCategorySummary();
                        _UclStockListCategorySummary.Dock = DockStyle.Fill;
                        _UclStockListCategorySummary.Visible = false;
                        _UclStockListCategorySummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListCategorySummary;
                        break;
                    case "Company Summary":
                        _UclStockListCompanywiseSummary = new UclStockListCompanywiseSummary();
                        _UclStockListCompanywiseSummary.Dock = DockStyle.Fill;
                        _UclStockListCompanywiseSummary.Visible = false;
                        _UclStockListCompanywiseSummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListCompanywiseSummary;
                        break;
                    case "Visiting Patient":
                        _UclStockListPatient = new UclStockListPatient();
                        _UclStockListPatient.Dock = DockStyle.Fill;
                        _UclStockListPatient.Visible = false;
                        _UclStockListPatient.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListPatient;
                        break;
                    case "Opening Stock List":
                        _UclStockListOpeningStock = new UclStockListOpeningStock();
                        _UclStockListOpeningStock.Dock = DockStyle.Fill;
                        _UclStockListOpeningStock.Visible = false;
                        _UclStockListOpeningStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListOpeningStock;
                        break;
                    case "Opening Stock - Product":
                        _UclStockListOpeningStockProduct = new UclStockListOpeningStockProduct();
                        _UclStockListOpeningStockProduct.Dock = DockStyle.Fill;
                        _UclStockListOpeningStockProduct.Visible = false;
                        _UclStockListOpeningStockProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListOpeningStockProduct;
                        break;


                    #endregion Stock

                    #region Account

                    case "Sales Register":
                        _UclAcListSalesRegister = new UclAcListSalesRegister();
                        _UclAcListSalesRegister.Dock = DockStyle.Fill;
                        _UclAcListSalesRegister.Visible = false;
                        _UclAcListSalesRegister.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListSalesRegister;
                        break;
                    case "Purchase Register":
                        _UclAcListPurchaseRegister = new UclAcListPurchaseRegister();
                        _UclAcListPurchaseRegister.Dock = DockStyle.Fill;
                        _UclAcListPurchaseRegister.Visible = false;
                        _UclAcListPurchaseRegister.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListPurchaseRegister;
                        break;
                    case "Cash Book":
                        _UclAcListCashBook = new UclAcListCashBook();
                        _UclAcListCashBook.Dock = DockStyle.Fill;
                        _UclAcListCashBook.Visible = false;
                        _UclAcListCashBook.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListCashBook;
                        break;
                    case "Bank Book":
                        _UclAcListBankBook = new UclAcListBankBook();
                        _UclAcListBankBook.Dock = DockStyle.Fill;
                        _UclAcListBankBook.Visible = false;
                        _UclAcListBankBook.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListBankBook;
                        break;
                    case "Bank Book By Cleared Date":
                        _UclAcListBankBookByClearedDate = new UclAcListBankBookByClearedDate();
                        _UclAcListBankBookByClearedDate.Dock = DockStyle.Fill;
                        _UclAcListBankBookByClearedDate.Visible = false;
                        _UclAcListBankBookByClearedDate.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListBankBookByClearedDate;
                        break;
                    case "Journal":
                        _UclAcListJournal = new UclAcListJournal();
                        _UclAcListJournal.Dock = DockStyle.Fill;
                        _UclAcListJournal.Visible = false;
                        _UclAcListJournal.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListJournal;
                        break;
                    case "General Ledger":
                        _UclAcListGeneralLedger = new UclAcListGeneralLedger();
                        _UclAcListGeneralLedger.Dock = DockStyle.Fill;
                        _UclAcListGeneralLedger.Visible = false;
                        _UclAcListGeneralLedger.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListGeneralLedger;
                        break;
                    case "Debtor Ledger":
                        _UclAcListDebtorLedger = new UclAcListDebtorLedger();
                        _UclAcListDebtorLedger.Dock = DockStyle.Fill;
                        _UclAcListDebtorLedger.Visible = false;
                        _UclAcListDebtorLedger.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListDebtorLedger;
                        break;
                    case "Creditor Ledger":
                        _UclAcListCreditorLedger = new UclAcListCreditorLedger();
                        _UclAcListCreditorLedger.Dock = DockStyle.Fill;
                        _UclAcListCreditorLedger.Visible = false;
                        _UclAcListCreditorLedger.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListCreditorLedger;
                        break;
                    case "Sundry Debtor":
                        _UclAcListSundryDebtor = new UclAcListSundryDebtor();
                        _UclAcListSundryDebtor.Dock = DockStyle.Fill;
                        _UclAcListSundryDebtor.Visible = false;
                        _UclAcListSundryDebtor.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListSundryDebtor;
                        break;
                    case "Sundry Creditor":
                        _UclStockListOpeningStock = new UclStockListOpeningStock();
                        _UclStockListOpeningStock.Dock = DockStyle.Fill;
                        _UclStockListOpeningStock.Visible = false;
                        _UclStockListOpeningStock.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclStockListOpeningStock;
                        break;
                    case "Ageing":
                        _UclAcListAgeing = new UclAcListAgeing();
                        _UclAcListAgeing.Dock = DockStyle.Fill;
                        _UclAcListAgeing.Visible = false;
                        _UclAcListAgeing.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclAcListAgeing;
                        break;


                    #endregion Account

                    #region Final Account

                    case "Trial Balance":
                        _UclFACListTrialBalance = new UclFACListTrialBalance();
                        _UclFACListTrialBalance.Dock = DockStyle.Fill;
                        _UclFACListTrialBalance.Visible = false;
                        _UclFACListTrialBalance.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclFACListTrialBalance;
                        break;
                    case "Entry Of Scheduled Numbers":
                        _UclFACListEntryofScheduleNumber = new UclFACListEntryofScheduleNumber();
                        _UclFACListEntryofScheduleNumber.Dock = DockStyle.Fill;
                        _UclFACListEntryofScheduleNumber.Visible = false;
                        _UclFACListEntryofScheduleNumber.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclFACListEntryofScheduleNumber;
                        break;
                    case "Profit & Loss":
                        _UclFACListProfitandLoss = new UclFACListProfitandLoss();
                        _UclFACListProfitandLoss.Dock = DockStyle.Fill;
                        _UclFACListProfitandLoss.Visible = false;
                        _UclFACListProfitandLoss.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclFACListProfitandLoss;
                        break;
                    case "Print Schedules":
                        _UclFACListPrintSchedules = new UclFACListPrintSchedules();
                        _UclFACListPrintSchedules.Dock = DockStyle.Fill;
                        _UclFACListPrintSchedules.Visible = false;
                        _UclFACListPrintSchedules.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclFACListPrintSchedules;
                        break;
                    case "Balance Sheet":
                        _UclFACListBalanceSheet = new UclFACListBalanceSheet();
                        _UclFACListBalanceSheet.Dock = DockStyle.Fill;
                        _UclFACListBalanceSheet.Visible = false;
                        _UclFACListBalanceSheet.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclFACListBalanceSheet;
                        break;
                    #endregion Final Accounts

                    #region VAT

                    case "Sale Register":
                        _UclVATListSalesRegister = new UclVATListSalesRegister();
                        _UclVATListSalesRegister.Dock = DockStyle.Fill;
                        _UclVATListSalesRegister.Visible = false;
                        _UclVATListSalesRegister.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegister;
                        break;
                    case "Sale Register (Other Details)":
                        _UclVATListSalesRegisterOtherDetails = new UclVATListSalesRegisterOtherDetails();
                        _UclVATListSalesRegisterOtherDetails.Dock = DockStyle.Fill;
                        _UclVATListSalesRegisterOtherDetails.Visible = false;
                        _UclVATListSalesRegisterOtherDetails.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegisterOtherDetails;
                        break;
                    case "Sale Register (Date)":
                        _UclVATListSalesRegisterDate = new UclVATListSalesRegisterDate();
                        _UclVATListSalesRegisterDate.Dock = DockStyle.Fill;
                        _UclVATListSalesRegisterDate.Visible = false;
                        _UclVATListSalesRegisterDate.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegisterDate;
                        break;
                    case "Sale Register (Month)":
                        _UclVATListSalesRegisterMonth = new UclVATListSalesRegisterMonth();
                        _UclVATListSalesRegisterMonth.Dock = DockStyle.Fill;
                        _UclVATListSalesRegisterMonth.Visible = false;
                        _UclVATListSalesRegisterMonth.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegisterMonth;
                        break;
                    case "Sale Register (Party)":
                        _UclVATListSalesRegisterTIN = new UclVATListSalesRegisterParty();
                        _UclVATListSalesRegisterTIN.Dock = DockStyle.Fill;
                        _UclVATListSalesRegisterTIN.Visible = false;
                        _UclVATListSalesRegisterTIN.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegisterTIN;
                        break;
                    case "Sale Register (Detail)":
                        _UclVATListSalesRegisterDetail = new UclVATListSalesRegisterDetail();
                        _UclVATListSalesRegisterDetail.Dock = DockStyle.Fill;
                        _UclVATListSalesRegisterDetail.Visible = false;
                        _UclVATListSalesRegisterDetail.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListSalesRegisterDetail;
                        break;
                    case "Purchase Register VAT":
                        _UclVATListPurchaseRegister = new UclVATListPurchaseRegister();
                        _UclVATListPurchaseRegister.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegister.Visible = false;
                        _UclVATListPurchaseRegister.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegister;
                        break;
                    case "Purchase Register (OtherDetails)":
                        _UclVATListPurchaseRegisterOtherDetails = new UclVATListPurchaseRegisterOtherDetails();
                        _UclVATListPurchaseRegisterOtherDetails.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegisterOtherDetails.Visible = false;
                        _UclVATListPurchaseRegisterOtherDetails.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegisterOtherDetails;
                        break;
                    case "Purchase Register (Date)":
                        _UclVATListPurchaseRegisterDate = new UclVATListPurchaseRegisterDate();
                        _UclVATListPurchaseRegisterDate.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegisterDate.Visible = false;
                        _UclVATListPurchaseRegisterDate.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegisterDate;
                        break;
                    case "Purchase Register (Month)":
                        _UclVATListPurchaseRegisterMonth = new UclVATListPurchaseRegisterMonth();
                        _UclVATListPurchaseRegisterMonth.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegisterMonth.Visible = false;
                        _UclVATListPurchaseRegisterMonth.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegisterMonth;
                        break;
                    case "Purchase Register (TIN)":
                        _UclVATListPurchaseRegisterTIN = new UclVATListPurchaseRegisterTIN();
                        _UclVATListPurchaseRegisterTIN.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegisterTIN.Visible = false;
                        _UclVATListPurchaseRegisterTIN.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegisterTIN;
                        break;
                    case "Purchase Register (Detail)":
                        _UclVATListPurchaseRegisterDetail = new UclVATListPurchaseRegisterDetail();
                        _UclVATListPurchaseRegisterDetail.Dock = DockStyle.Fill;
                        _UclVATListPurchaseRegisterDetail.Visible = false;
                        _UclVATListPurchaseRegisterDetail.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListPurchaseRegisterDetail;
                        break;
                    case "Sales Return VAT Register (Detail)":
                        _UclVATListCreditNote = new UclVATListCreditNote();
                        _UclVATListCreditNote.Dock = DockStyle.Fill;
                        _UclVATListCreditNote.Visible = false;
                        _UclVATListCreditNote.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListCreditNote;
                        break;
                    case "VAT Register Combine(Month)":
                        _UclVATListCombine = new UclVATListCombine();
                        _UclVATListCombine.Dock = DockStyle.Fill;
                        _UclVATListCombine.Visible = false;
                        _UclVATListCombine.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclVATListCombine;
                        break;
                    #endregion VAT

                    #region MIS

                    case "Profit (Day)":
                        _UclMISListProfitDay = new UclMISListProfitDay();
                        _UclMISListProfitDay.Dock = DockStyle.Fill;
                        _UclMISListProfitDay.Visible = false;
                        _UclMISListProfitDay.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListProfitDay;
                        break;
                    case "Profit (Company)":
                        _UclMISListProfitCompany = new UclMISListProfitCompany();
                        _UclMISListProfitCompany.Dock = DockStyle.Fill;
                        _UclMISListProfitCompany.Visible = false;
                        _UclMISListProfitCompany.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListProfitCompany;
                        break;
                    case "Daily Cash Closing":
                        _UclMISListDailyCashClosing = new UclMISListDailyCashClosing();
                        _UclMISListDailyCashClosing.Dock = DockStyle.Fill;
                        _UclMISListDailyCashClosing.Visible = false;
                        _UclMISListDailyCashClosing.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListDailyCashClosing;
                        break;
                    case "Daily Bank Closing":
                        _UclMISListDailyBankClosing = new UclMISListDailyBankClosing();
                        _UclMISListDailyBankClosing.Dock = DockStyle.Fill;
                        _UclMISListDailyBankClosing.Visible = false;
                        _UclMISListDailyBankClosing.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListDailyBankClosing;
                        break;
                    case "Closing Stock Value":
                        _UclMISListCurrentStockValue = new UclMISListCurrentStockValue();
                        _UclMISListCurrentStockValue.Dock = DockStyle.Fill;
                        _UclMISListCurrentStockValue.Visible = false;
                        _UclMISListCurrentStockValue.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListCurrentStockValue;
                        break;
                    case "Stock Statement":
                        _UclMISListCurrentStockStatement = new UclMISListCurrentStockStatement();
                        _UclMISListCurrentStockStatement.Dock = DockStyle.Fill;
                        _UclMISListCurrentStockStatement.Visible = false;
                        _UclMISListCurrentStockStatement.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListCurrentStockStatement;
                        break;
                    case "Stock Statement(Bank)":
                        _UclMISListStockStatementBank = new UclMISListStockStatementBank();
                        _UclMISListStockStatementBank.Dock = DockStyle.Fill;
                        _UclMISListStockStatementBank.Visible = false;
                        _UclMISListStockStatementBank.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListStockStatementBank;
                        break;
                    case "Bank Interest":
                        _UclMISListBankInterest = new UclMISListBankInterest();
                        _UclMISListBankInterest.Dock = DockStyle.Fill;
                        _UclMISListBankInterest.Visible = false;
                        _UclMISListBankInterest.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListBankInterest;
                        break;
                    case "Summary":
                        _UclMISListSummary = new UclMISListSummary();
                        _UclMISListSummary.Dock = DockStyle.Fill;
                        _UclMISListSummary.Visible = false;
                        _UclMISListSummary.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListSummary;
                        break;
                    case "Deleted Vouchers":
                        _UclMISListDeletedVouchers = new UclMISListDeletedVouchers();
                        _UclMISListDeletedVouchers.Dock = DockStyle.Fill;
                        _UclMISListDeletedVouchers.Visible = false;
                        _UclMISListDeletedVouchers.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListDeletedVouchers;
                        break;
                    case "Changed Vouchers":
                        _UclMISListChangedVouchers = new UclMISListChangedVouchers();
                        _UclMISListChangedVouchers.Dock = DockStyle.Fill;
                        _UclMISListChangedVouchers.Visible = false;
                        _UclMISListChangedVouchers.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclMISListChangedVouchers;
                        break;
                    #endregion MIS

                    #region Expiry
                    case "Expiry (All Products)":
                        _UclExpiryListProduct = new UclExpiryListProduct();
                        _UclExpiryListProduct.Dock = DockStyle.Fill;
                        _UclExpiryListProduct.Visible = false;
                        _UclExpiryListProduct.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclExpiryListProduct;
                        break;
                    #endregion Expiry

                    #region H1
                    case "H1 (Sale)":
                        _UclH1SaleList = new UclH1SaleList();
                        _UclH1SaleList.Dock = DockStyle.Fill;
                        _UclH1SaleList.Visible = false;
                        _UclH1SaleList.ExitClicked += new EventHandler(Item_ExitClicked);
                        _item.Control = _UclH1SaleList;
                        break;
                    #endregion H1

                    #endregion Reports
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void DeinitializeUserControl(ControlItem _item)
        {
            try
            {
                switch (_item.ItemName)
                {
                    #region Master-I
                    case "Account":
                        _item.Control = null;
                        _UclAccount = null;
                        break;
                    case "Company":
                        _item.Control = null;
                        _UclCompany = null;
                        break;
                    case "Doctor":
                        _item.Control = null;
                        _UclDoctor = null;
                        break;
                    case "A/c Group":
                        _item.Control = null;
                        _UclGroup = null;
                        break;
                    case "Patient":
                        _item.Control = null;
                        _UclPatient = null;
                        break;
                    case "Prescription":
                        _item.Control = null;
                        _UclPrescription = null;
                        break;
                    case "Product":
                        _item.Control = null;
                        _UclProduct = null;
                        break;
                    case "Scheme":
                        _item.Control = null;
                        _UclScheme = null;
                        break;
                    case "Shelf":
                        _item.Control = null;
                        _UclShelf = null;
                        break;
                    case "Hospital Patient":
                        _item.Control = null;
                        _UclHospitalPatient = null;
                        break;
                    #endregion Master-I

                    #region Master-II
                    case "Area":
                        _item.Control = null;
                        _UclArea = null;
                        break;
                    case "Bank":
                        _item.Control = null;
                        _UclBank = null;
                        break;
                    case "Branch":
                        _item.Control = null;
                        _UclBranch = null;
                        break;
                    case "Customer":
                        _item.Control = null;
                        _UclCustomer = null;
                        break;
                    case "EmailID":
                        _item.Control = null;
                        _UclEmailID = null;
                        break;
                    case "Generic Name":
                        _item.Control = null;
                        _UclGenericCategory = null;
                        break;
                    case "Messages":
                        _item.Control = null;
                        _UclMessages = null;
                        break;
                    case "ProductCategory":
                        _item.Control = null;
                        _UclProdCategory = null;
                        break;
                    case "Salesman":
                        _item.Control = null;
                        _UclSalesman = null;
                        break;
                    case "Ward":
                        _item.Control = null;
                        _UclWard = null;
                        break;
                    #endregion Master-II

                    #region Links
                    case "DebtorProduct":
                        _item.Control = null;
                        _UclDebtorProduct = null;
                        break;
                    case "DrugGrouping (Generic)":
                        _item.Control = null;
                        _UclDrugGrouping = null;
                        break;
                    case "PartyCompany":
                        _item.Control = null;
                        _UclPartyCompany = null;
                        break;
                    case "ShelfProduct":
                        _item.Control = null;
                        _UclLinkShelfProduct = null;
                        break;
                    case "ProductScheduleH1":
                        _item.Control = null;
                        _UclLinkProductScheduleH1 = null;
                        break;

                    #endregion Links
                    #region Distributor
                    case "Distributor Sale":
                        _item.Control = null;
                        _UclDistributorSale = null;
                        break;
                    #endregion Distributor
                    #region Sale
                    case "Counter Sale":
                        _item.Control = null;
                        _UclCounterSale = null;
                        break;
                    case "Counter Sale Edit":
                        _item.Control = null;
                        _UclCounterSaleEdit = null;
                        break;
                    case "Patient Sale":
                        _item.Control = null;
                        _UclPatientSale = null;
                        break;
                    case "Debtor Sale":
                        _item.Control = null;
                        _UclDebtorSale = null;
                        break;
                    case "Hospital Sale":
                        _item.Control = null;
                        _UclHospitalSale = null;
                        break;
                    case "Institutional Sale":
                        _item.Control = null;
                        _UclInstitutionalSale = null;
                        break;
                    case " Cash Sale":
                        _item.Control = null;
                        _UclSaleWithoutStock = null;
                        break;

                    #endregion Sale
                    #region Purchase
                    case "Purchase":
                        _item.Control = null;
                        _UclPurchase = null;
                        break;
                    case "Purchase II":
                        _item.Control = null;
                        _UclPurchaseWithoutStock = null;
                        break;
                    #endregion purchase

                    #region Debit Note
                    case "Debit Note Stock":
                        _item.Control = null;
                        _UclDebitNoteStock = null;
                        break;
                    case "Debit Note Amount":
                        _item.Control = null;
                        _UclDebitNoteAmount = null;
                        break;
                    case "Debit Note Expiry":
                        _item.Control = null;
                       _UclDebitNoteExpiry = null;
                        break;
                    case "Stock Out":
                        _item.Control = null;
                       _UclStockOut = null;
                        break;                   
                    #endregion DebitNote

                    #region Credit Note
                    case "Credit Note Stock":
                        _item.Control = null;
                        _UclCreditNoteStock = null;
                        break;
                    case "Credit Note Amount":
                        _item.Control = null;
                        _UclCreditNoteAmount = null;
                        break;
                    case "Stock In":
                        _item.Control = null;
                       _UclStockIn = null;
                        break;
                   
                    #endregion Credit Note

                    #region Cash 
                    case "Cash Receipt":
                        _item.Control = null;
                        _UclCashReceipt = null;
                        break;
                    case "Cash Payment":
                        _item.Control = null;
                        _UclCashPayment = null;
                        break;
                    case "Cash Expenses":
                        _item.Control = null;
                        _UclCashExpenses = null;
                        break;

                    #endregion Cash

                    #region Bank
                    case "Bank Receipt":
                        _item.Control = null;
                       _UclBankReceipt = null;
                        break;
                    case "Bank Payment":
                        _item.Control = null;
                        _UclBankPayment = null;
                        break;
                    case "Cheque Return":
                        _item.Control = null;
                       _UclChequeReturn = null;
                        break;
                    case "Bank Expenses":
                        _item.Control = null;
                        _UclBankExpenses = null;
                        break;
                    case "Contra Entry":
                        _item.Control = null;
                        _UclContraEntry = null;
                        break;

                    #endregion Bank

                    #region contra
                    case "Do Bank Reconciliation":
                        _item.Control = null;
                        _UclDoBankReconciliation = null;
                        break;

                    #endregion contra


                    #region other
                    case "Opening Stock":
                        _item.Control = null;
                        _UclOPStock = null;
                        break;
                    case "Correction In Rate":
                        _item.Control = null;
                        _UclCorrectioninRate = null;
                        break;
                    case "Users":
                        _item.Control = null;
                        _UclUser = null;
                        break;
                    //case "User Rights":
                    //    _item.Control = null;
                    //    _UclUserRights = null;
                    //    break;
                    //case "Settings":
                    //    _item.Control = null;
                    //   _UclSettingsSale = null;
                    //    break;
                    //case "Print Settings":
                    //    _item.Control = null;
                    //    _UclSettingsForPrint = null;
                    //    break;
                    case "Similar Products":
                        _item.Control = null;
                        _UclSubstitute = null;
                        break;
                    case "Operator":
                        _item.Control = null;
                        _UclOperator = null;
                        break;
                    case "Bill Reprint":
                        _item.Control = null;
                       _UclSpecialSale = null;
                        break;
                    case "Bulk Bill Reprint":
                        _item.Control = null;
                       _UclBulkPrintPartywiseSale = null;
                        break;
                    case "BarCode Print":
                        _item.Control = null;
                        _UclBarCodePrint = null;
                        break;
                       
                    #endregion other

                    #region Statement
                    case "Sale ALL":
                        _item.Control = null;
                        _UclStatementSale = null;
                        break;
                    case "Purchase (15 Days)":
                        _item.Control = null;
                        _UclStatementPurchase15Days = null;
                        break;
                    case "Hospital Statement":
                        _item.Control = null;
                       _UclStatementHospital = null;
                        break;
                    case "Sale (Party)":
                        _item.Control = null;
                       _UclStatementPartywiseSale = null;
                        break;
                    case "Purchase (Party)":
                        _item.Control = null;
                        _UclStatementPartywisePurchase = null;
                        break;

                    #endregion Statement

                    #region Reports
                    #region List
                    case "CompanyList":
                        _item.Control = null;
                        _UclCompanyList = null;
                        break;
                    case "ProductList (ALL)":
                        _item.Control = null;
                        _UclProductList = null;
                        break;
                    case "Product List":
                        _item.Control = null;
                        _UclProductListBySelection = null;
                        break;
                    case "Account List":
                        _item.Control = null;
                        _UclAccountList = null;
                        break;
                    case "Doctor List":
                        _item.Control = null;
                        _UclDoctorList = null;
                        break;
                    case "Patient List":
                        _item.Control = null;
                        _UclPatientList = null;
                        break;
                    case "Shelf List":
                        _item.Control = null;
                        _UclShelfList = null;
                        break;
                    case "Bank List":
                        _item.Control = null;
                        _UclBankList = null;
                        break;
                    case "Branch List":
                        _item.Control = null;
                        _UclBranchList = null;
                        break;
                    case "Area List":
                        _item.Control = null;
                        _UclAreaList = null;
                        break;
                    case "Generic Category List":
                        _item.Control = null;
                        _UclGenericCategoryList = null;
                        break;
                    case "Todays Cheques List":
                        _item.Control = null;
                        _UclListTodaysCheques = null;
                        break;
                    case "Operator List":
                        _item.Control = null;
                        _UclListOperator = null;
                        break;
                    case "Cheque Return List":
                        _item.Control = null;
                        _UclListChequeReturn = null;
                        break;
                    #endregion List

                    #region VoucherList

                    case "Cash Receipt List":
                        _item.Control = null;
                        _UclCashReceiptList = null;
                        break;
                    case "Cash Payment List":
                        _item.Control = null;
                        _UclCashPaidList = null;
                        break;
                    case "Cash Expenses List":
                        _item.Control = null;
                        _UclCashExpensesList = null;
                        break;
                    case "Cheque Receipt":
                        _item.Control = null;
                        _UclChequeReceiptList = null;
                        break;
                    case "Cheque Paid":
                        _item.Control = null;
                        _UclChequePaidList = null;
                        break;
                    case "Bank Expenses List":
                        _item.Control = null;
                        _UclVouBankExpensesList = null;
                        break;
                    case "Statement Purchase":
                        _item.Control = null;
                        _UclVouStatementPurchaseList = null;
                        break;
                    case "Statement Sale":
                        _item.Control = null;
                        _UclVouStatementSaleList = null;
                        break;
                    case "Cheque Received But Not Cleared":
                        _item.Control = null;
                        _UclVouChequeReceivedButNotCleared = null;
                        break;
                    case "Cheque Paid But Not Cleared":
                        _item.Control = null;
                        _UclVouChequePaidButNotCleared = null;
                        break;
                    #endregion VoucherList


                    #region DebitCreditNote

                    case "Debit Note":
                        _item.Control = null;
                        _UclDebitNoteList = null;
                        break;
                    case "Debit Note (Product)":
                        _item.Control = null;
                        _UclDebitNoteListProduct = null;
                        break;
                    case "Stock Out List":
                        _item.Control = null;
                        _UclStockOutList = null;
                        break;
                    case "Stock Out (Product)":
                        _item.Control = null;
                        _UclStockOutListProduct = null;
                        break;
                    case "Credit Note":
                        _item.Control = null;
                        _UclCreditNoteList = null;
                        break;
                    case "Credit Note (Product)":
                        _item.Control = null;
                        _UclCreditNoteListProduct = null;
                        break;
                    case "Stock IN":
                        _item.Control = null;
                        _UclStockInList = null;
                        break;
                    case "Stock IN (Product)":
                        _item.Control = null;
                        _UclStockInListProduct = null;
                        break;
                    case "DB/CR Note(Party)":
                        _item.Control = null;
                        _UclCreditDebitNotePartyList = null;
                        break;
                    case "DB/CR Note(Product)":
                        _item.Control = null;
                        _UclCreditDebitNoteProductList = null;
                        break;
                    case "Stock IN/OUT(Product)":
                        _item.Control = null;
                        _UclStockINOUTListProduct = null;
                        break;

                    #endregion DebitCreditNote

                    #region Scheme

                    case "Scheme List (ALL)":
                        _item.Control = null;
                        _UclSchemeListAll = null;
                        break;
                    case "Scheme List (Company)":
                        _item.Control = null;
                        _UclSchemeListCompanywise = null;
                        break;
                    case "Scheme List (Product/Purchase)":
                        _item.Control = null;
                        _UclSchemeListProductPurchase = null;
                        break;
                    #endregion Scheme

                    #region Purchase

                    case "Purchase (Product)":
                        _item.Control = null;
                        _UclPurchaseListProductWise = null;
                        break;
                    case "Purchase (Batch)":
                        _item.Control = null;
                        _UclPurchaseListProductBatch = null;
                        break;
                    case "Products Created in Current Year":
                        _item.Control = null;
                        _UclPurchaseListNewProduct = null;
                        break;
                    case "Purchase (Party/Product)":
                        _item.Control = null;
                        _UclPurchaseListPartyProduct = null;
                        break;
                    case "Purchase (Partywise Bills)":
                        _item.Control = null;
                        _UclPurchaseListPartywisebills = null;
                        break;
                    case "Purchase (Discount)":
                        _item.Control = null;
                        _UclPurchaseListDiscount = null;
                        break;

                    case "Purchase (All Party Summary)":
                        _item.Control = null;
                        _UclPurchaseListAllPartySummary = null;
                        break;
                    case "Purchase (Category)":
                        _item.Control = null;
                        _UclPurchaseListCategory = null;
                        break;
                    case "Purchase (Company)":
                        _item.Control = null;
                        _UclPurchaseListCompany = null;
                        break;
                    case "Purchase (Daily)":
                        _item.Control = null;
                        _UclPurchaseListDaily = null;
                        break;
                    #endregion Purchase

                    #region Sale

                    case "Sale (Daily)":
                        _item.Control = null;
                        _UclSaleListDailySale = null;
                        break;
                    case "Sale (Product/Batch)":
                        _item.Control = null;
                        _UclSaleListProductBatch = null;
                        break;
                    case "Sale (Product)":
                        _item.Control = null;
                        _UclSaleListProduct = null;
                        break;
                    case "Sale (Party Summary)":
                        _item.Control = null;
                        _UclSaleListPartySaleSummary = null;
                        break;
                    case "Sale (Regular Party Product)":
                        _item.Control = null;
                        _UclSaleListRegularPartyProduct = null;
                        break;
                    case "Sale (Scheduled Drugs)":
                        _item.Control = null;
                        _UclSaleListSheduledDrug = null;
                        break;
                    case "Sale (Patient)":
                        _item.Control = null;
                        _UclSaleListPatient = null;
                        break;
                    case "Sale (Doctor)":
                        _item.Control = null;
                        _UclSaleListDoctor = null;
                        break;
                    case "Sale (Category)":
                        _item.Control = null;
                        _UclSaleListCategory = null;
                        break;
                    case "Sale (PartywiseBills)":
                        _item.Control = null;
                        _UclSaleListPartywiseBills = null;
                        break;
                    case "Sale (Doctor/Company)":
                        _item.Control = null;
                        _UclSaleListDoctorCompany = null;
                        break;
                    case "Sale (Product Summary)":
                        _item.Control = null;
                        _UclSaleListDaywiseProductSummary = null;
                        break;
                    case "Sale (Admit Patients)":
                        _item.Control = null;
                        _UclSaleListAdmitPatient = null;
                        break;
                    case "Sale (Operator)":
                        _item.Control = null;
                        _UclSaleListOperator = null;
                        break;
                    case "Sale (IPD)":
                        _item.Control = null;
                        _UclSaleListIPD = null;
                        break;
                    case "Sale (OPD)":
                        _item.Control = null;
                        _UclSaleListOPD = null;
                        break;
                    case "Sale (CreditCard)":
                        _item.Control = null;
                        _UclSaleListCreditCard = null;
                        break;
                    case "Sale (Daily Products)":
                        _item.Control = null;
                        _UclSaleDailyProducts = null;
                        break;

                    #endregion Sale

                    #region Stock

                    case "Current Stock":
                        _item.Control = null;
                        _UclStockListCurrentStock = null;
                        break;
                    case "Current Stock (Batch)":
                        _item.Control = null;
                        _UclStockListBatchwise = null;
                        break;
                    case "Current Stock (Shelf)":
                        _item.Control = null;
                        _UclStockListShelf = null;
                        break;
                    case "Non Moving":
                        _item.Control = null;
                        _UclStockListNonMoving = null;
                        break;
                    case "All Products":
                        _item.Control = null;
                        _UclStockListAll = null;
                        break;
                    case "Stock n Sale":
                        _item.Control = null;
                        _UclStockListStocknSale = null;
                        break;

                    case "Product Ledger":
                        _item.Control = null;
                        _UclStockListProductLedger = null;
                        break;
                    case "Category Summary":
                        _item.Control = null;
                        _UclStockListCategorySummary = null;
                        break;
                    case "Company Summary":
                        _item.Control = null;
                        _UclStockListCompanywiseSummary = null;
                        break;
                    case "Visiting Patient":
                        _item.Control = null;
                        _UclStockListPatient = null;
                        break;
                    case "Opening Stock List":
                        _item.Control = null;
                        _UclStockListOpeningStock = null;
                        break;
                    case "Opening Stock - Product":
                        _item.Control = null;
                        _UclStockListOpeningStockProduct = null;
                        break;

                    #endregion Stock

                    #region Account

                    case "Sales Register":
                        _item.Control = null;
                        _UclAcListSalesRegister = null;
                        break;
                    case "Purchase Register":
                        _item.Control = null;
                        _UclAcListPurchaseRegister = null;
                        break;
                    case "Cash Book":
                        _item.Control = null;
                        _UclAcListCashBook = null;
                        break;
                    case "Bank Book":
                        _item.Control = null;
                        _UclAcListBankBook = null;
                        break;
                    case "Bank Book By Cleared Date":
                        _item.Control = null;
                        _UclAcListBankBookByClearedDate = null;
                        break;
                    case "Journal":
                        _item.Control = null;
                        _UclAcListJournal = null;
                        break;
                    case "General Ledger":
                        _item.Control = null;
                        _UclAcListGeneralLedger = null;
                        break;
                    case "Debtor Ledger":
                        _item.Control = null;
                        _UclAcListDebtorLedger = null;
                        break;
                    case "Creditor Ledger":
                        _item.Control = null;
                        _UclAcListCreditorLedger = null;
                        break;
                    case "Sundry Debtor":
                        _item.Control = null;
                        _UclAcListSundryDebtor = null;
                        break;
                    case "Sundry Creditor":
                        _item.Control = null;
                        _UclAcListSundryCreditor = null;
                        break;
                    case "Ageing":
                        _item.Control = null;
                        _UclAcListAgeing = null;
                        break;

                    #endregion Account

                    #region Final Account

                    case "Trial Balance":
                        _item.Control = null;
                        _UclFACListTrialBalance = null;
                        break;
                    case "Entry Of Scheduled Numbers":
                        _item.Control = null;
                        _UclFACListEntryofScheduleNumber = null;
                        break;
                    case "Profit & Loss":
                        _item.Control = null;
                        _UclFACListProfitandLoss = null;
                        break;
                    case "Print Schedules":
                        _item.Control = null;
                        _UclFACListPrintSchedules = null;
                        break;
                    case "Balance Sheet":
                        _item.Control = null;
                        _UclFACListBalanceSheet = null;
                        break;
                    #endregion Final Accounts

                    #region VAT

                    case "Sale Register":
                        _item.Control = null;
                        _UclVATListSalesRegister = null;
                        break;
                    case "Sale Register (Other Details)":
                        _item.Control = null;
                        _UclVATListSalesRegisterOtherDetails = null;
                        break;
                    case "Sale Register (Date)":
                        _item.Control = null;
                        _UclVATListSalesRegisterDate = null;
                        break;
                    case "Sale Register (Month)":
                        _item.Control = null;
                        _UclVATListSalesRegisterMonth = null;
                        break;
                    case "Sale Register (Party)":
                        _item.Control = null;
                        _UclVATListSalesRegisterTIN = null;
                        break;
                    case "Sale Register (Detail)":
                        _item.Control = null;
                        _UclVATListSalesRegisterDetail = null;
                        break;
                    case "Purchase Register VAT":
                        _item.Control = null;
                        _UclVATListPurchaseRegister = null;
                        break;
                    case "Purchase Register (OtherDetails)":
                        _item.Control = null;
                        _UclVATListPurchaseRegisterOtherDetails = null;
                        break;
                    case "Purchase Register (Date)":
                        _item.Control = null;
                        _UclVATListPurchaseRegisterDate = null;
                        break;
                    case "Purchase Register (Month)":
                        _item.Control = null;
                        _UclVATListPurchaseRegisterMonth = null;
                        break;
                    case "Purchase Register (TIN)":
                        _item.Control = null;
                        _UclVATListPurchaseRegisterTIN = null;
                        break;
                    case "Purchase Register (Detail)":
                        _item.Control = null;
                        _UclVATListPurchaseRegisterDetail = null;
                        break;
                    case "Sales Return VAT Register (Detail)":
                        _item.Control = null;
                        _UclVATListCreditNote = null;
                        break;
                    case "VAT Register Combine(Month)":
                        _item.Control = null;
                        _UclVATListCombine = null;
                        break;
                    #endregion VAT

                    #region MIS

                    case "Profit (Day)":
                        _item.Control = null;
                        _UclMISListProfitDay = null;
                        break;
                    case "Profit (Company)":
                        _item.Control = null;
                        _UclMISListProfitCompany = null;
                        break;
                    case "Daily Cash Closing":
                        _item.Control = null;
                        _UclMISListDailyCashClosing = null;
                        break;
                    case "Daily Bank Closing":
                        _item.Control = null;
                        _UclMISListDailyBankClosing = null;
                        break;
                    case "Closing Stock Value":
                        _item.Control = null;
                        _UclMISListCurrentStockValue = null;
                        break;
                    case "Stock Statement":
                        _item.Control = null;
                        _UclMISListCurrentStockStatement = null;
                        break;
                    case "Stock Statement(Bank)":
                        _item.Control = null;
                        _UclMISListStockStatementBank = null;
                        break;
                    case "Bank Interest":
                        _item.Control = null;
                        _UclMISListBankInterest = null;
                        break;
                    case "Summary":
                        _item.Control = null;
                        _UclMISListSummary = null;
                        break;
                    case "Deleted Vouchers":
                        _item.Control = null;
                        _UclMISListDeletedVouchers = null;
                        break;
                    case "Changed Vouchers":
                        _item.Control = null;
                        _UclMISListChangedVouchers = null;
                        break;
                    #endregion MIS

                    #region Expiry
                    case "Expiry (All Products)":
                        _item.Control = null;
                        _UclExpiryListProduct = null;
                        break;
                    #endregion Expiry

                    #region H1
                    case "H1 (Sale)":
                        _item.Control = null;
                        _UclH1SaleList = null;
                        break;
                    #endregion H1

                    #endregion Reports
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

    }

}



