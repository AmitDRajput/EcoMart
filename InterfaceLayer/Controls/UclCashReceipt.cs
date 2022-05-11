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
using PharmaSYSPlus.CommonLibrary;
using EcoMart.InterfaceLayer.CommonControls;
using PrintDataGrid;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]

    public partial class UclCashReceipt : BaseControl
    {
        #region Declaration
        private CashReceipt _CashReceipt;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        DataTable _JVtable = new DataTable();
        private Form frmView;
        private DateTime _preDate = DateTime.Now;
        private BaseControl ViewControl;
        bool IfOpeningAdded = false;
        #endregion

        # region Constructor
        public UclCashReceipt()
        {
            try
            {
                InitializeComponent();
                _CashReceipt = new CashReceipt();
                SearchControl = new UclCashReceiptSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            try
            {
                if (_Mode == OperationMode.Add)
                    mcbCreditor.Focus();
                else
                    txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool ClearData()
        {
            try
            {
                _CashReceipt.Initialise();
                ClearControls();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            try
            {
                ClearData();
                datePickerBillDate.Value = _preDate;
                headerLabel1.Text = "CASH RECEIPT -> NEW";
                FillPartyCombo();
                EnableDisableAdd();
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void EnableDisableAdd()
        {
            mcbCreditor.ShowNew = true;
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            pnlNameAddress.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.Enabled = false;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                headerLabel1.Text = "CASH RECEIPT -> EDIT";
                FillmpMSVCGrid("");
                datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                FillOpeningBalanceGrid("");
                FillPartyCombo();
                mcbCreditor.ShowNew = false;
                datePickerBillDate.Value = DateTime.Now;
                EnableDisableForEdit();
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void EnableDisableForEdit()
        {

            btnModify.Visible = true;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            pnlNameAddress.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
        }

        private void EnableDisable()
        {
            pnlVou.Enabled = true;
            pnlNameAddress.Enabled = true;
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            mcbCreditor.Focus();
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "CASH RECEIPT -> DELETE";
                ClearData();
                datePickerBillDate.Value = DateTime.Now;
                FillmpMSVCGrid("");
                FillPartyCombo();
                EnableDisableDelete();
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        private void EnableDisableDelete()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            pnlNameAddress.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_CashReceipt.Id != null && _CashReceipt.Id != "")
                {
                    if (_CashReceipt.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _CashReceipt.DeleteDetails();
                        if (retValue)
                        {
                            DeletePreviousEntry();
                            RevertPreviousSalesBalance();
                            _CashReceipt.DeleteJV();
                        }
                        retValue = _CashReceipt.DeleteAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _CashReceipt.ModifiedBy = General.CurrentUser.Id;
                            _CashReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CashReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _CashReceipt.AddDeletedDetails();
                            AddPreviousRowsInDeletedDetail();
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Delete...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LockTable.UnLockTables();
                ClearData();
            }
            catch (Exception Ex)
            {
                LockTable.UnLockTables();
                Log.WriteException(Ex);
            }
            return true;
        }
        public override bool View()
        {
            bool retValue = base.View();
            try
            {

                headerLabel1.Text = "CASH RECEIPT -> VIEW";
                FillmpMSVCGrid("");
                FillPartyCombo();
                EnableDisableView();
                if (General.IfYearEndOverGlobal == "Y")
                {
                    if (General.CurrentUser.Level <= 1)
                    {
                        tsBtnAdd.Visible = true;
                        tsBtnDelete.Visible = true;
                        tsBtnFifth.Visible = true;
                        tsBtnEdit.Visible = true;
                    }
                    else
                    {
                        tsBtnAdd.Visible = false;
                        tsBtnDelete.Visible = false;
                        tsBtnFifth.Visible = false;
                        tsBtnEdit.Visible = false;
                    }
                }
                txtVouchernumber.Focus();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void EnableDisableView()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            pnlNameAddress.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _CashReceipt.CBVouType = FixAccounts.VoucherTypeForCashReceipt;
                }
                _CashReceipt.GetLastRecord();
                FillSearchData(_CashReceipt.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool Print()
        {
            bool retValue = true;
            if (txtAmountReceived.Text != null && Convert.ToDouble(txtAmountReceived.Text.ToString()) > 0)
            {
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
                {

                    ConstructPrintGridColumns();
                    PrintGrid.Rows.Clear();
                    FillPrintGrid();
                    if (General.CurrentSetting.MsetPrintCashBankVoucher == "Y")
                        PrintCashBankVoucherPrePrintedPaper();
                    else
                        PrintCashBankVoucherPlainPaper();
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
            }
            ClearData();
            return retValue;
        }
        private void PrintCashBankVoucherPrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            string atow = General.AmountToWord(_CashReceipt.CBAmount);
            printer.Print(_CashReceipt.CBVouType, _CashReceipt.CBVouNo.ToString(), _CashReceipt.CBVouDate, _CashReceipt.CBName, _CashReceipt.CBAddress1, _CashReceipt.CBAddress2, "", "", PrintGrid.Rows, _CashReceipt.CBNarration, _CashReceipt.CBAmount, "", _CashReceipt.CBTotalDiscount, 0, 0, 0, 0, atow);

        }

        private void PrintCashBankVoucherPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            string atow = General.AmountToWord(_CashReceipt.CBAmount);
            printer.Print(_CashReceipt.CBVouType, _CashReceipt.CBVouNo.ToString(), _CashReceipt.CBVouDate, _CashReceipt.CBName, _CashReceipt.CBAddress1, _CashReceipt.CBAddress2, "", "", PrintGrid.Rows, _CashReceipt.CBNarration, _CashReceipt.CBAmount, "", _CashReceipt.CBTotalDiscount, 0, 0, 0, 0, atow);

        }
        private void FillPrintGrid()
        {
            int colcount = mpMSCSale.ColumnsMain.Count;
            PSCashBankcontrol GridForPrint = new PSCashBankcontrol();
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                GridForPrint = mpMSCSale;
            else
                GridForPrint = mpMSVC;

            try
            {

                foreach (DataGridViewRow dr in GridForPrint.Rows)
                {
                    if (dr.Cells[0].Value != null && ((dr.Cells["Col_GetClearedAmount"].Value != null && dr.Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty && Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString()) > 0) || dr.Cells["Col_ClearedAmount"].Value != null))
                    {
                        int printgridindex = PrintGrid.Rows.Add();
                        if (dr.Cells["Col_BillSeries"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Quantity"].Value = dr.Cells["Col_BillSeries"].Value.ToString();
                        if (dr.Cells["Col_BillType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_BillType"].Value.ToString();
                        if (dr.Cells["Col_BillNumber"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Shelf"].Value = dr.Cells["Col_BillNumber"].Value.ToString();
                        if (dr.Cells["Col_BillSubType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProdCompShortName"].Value = dr.Cells["Col_BillSubType"].Value.ToString();
                        if (dr.Cells["Col_BillFromDate"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_BatchNumber"].Value = General.GetDateInShortDateFormat(dr.Cells["Col_BillFromDate"].Value.ToString());
                        if (dr.Cells["Col_BillAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_RatePerUnit"].Value = dr.Cells["Col_BillAmount"].Value.ToString();
                        double amt = 0;
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                            amt = Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString());
                        else
                            amt = Convert.ToDouble(dr.Cells["Col_ClearedAmount"].Value.ToString());
                        if (dr.Cells["Col_ClearedAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Amount"].Value = amt.ToString();


                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //public override bool Print()
        //{
        //    bool retValue = true;
        //    if (txtAmountReceived.Text != null && txtAmountReceived.Text != string.Empty && Convert.ToDouble(txtAmountReceived.Text.ToString()) != 0)
        //        PrintData();
        //    ClearData();
        //    return retValue;
        //}

        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = mpMSCSale.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {

                    if (dr.Cells["Col_ClearedAmount"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            //////////_CashReceipt.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _CashReceipt.PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        if (dr.Cells["Col_BillSeries"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_BillSeries"].Value.ToString(), PrintRowPixel, 85, fnt);
                            PrintBill.Rows.Add(row);
                        }
                        row = new PrintRow(dr.Cells["Col_BillType"].Value.ToString(), PrintRowPixel, 125, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString(), PrintRowPixel, 175, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BillFromDate"].Value.ToString(), PrintRowPixel, 250, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BalanceAmount"].Value.ToString(), PrintRowPixel, 350, fnt);
                        PrintBill.Rows.Add(row);
                    }
                }
                PrintRowPixel = 325;
                row = new PrintRow(_CashReceipt.CBNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_CashReceipt.CBAmount.ToString("#0.00"), PrintRowPixel, 700, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = 418;
                row = new PrintRow("---", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);


                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHeader(int TotalPages, int Rowcount, Font fnt)
        {
            PrintRow row;
            try
            {
                string billtype = "Cash Receipt";

                PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_CashReceipt.CBVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;
                row = new PrintRow(_CashReceipt.CBName, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 34;
                string myadd = _CashReceipt.CBAddress1.Trim() + " " + _CashReceipt.CBAddress2.Trim();
                row = new PrintRow(myadd, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_CashReceipt.CBVouDate, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            return Rowcount;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _CashReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _CashReceipt.GetFirstRecord();
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_CashReceipt.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _CashReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CashReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _CashReceipt.CBVouNo = i;
                dr = _CashReceipt.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_CashReceipt.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _CashReceipt.GetLastVoucherNumber(FixAccounts.VoucherTypeForCashReceipt, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CashReceipt.CBVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _CashReceipt.CBVouNo = i;
                dr = _CashReceipt.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_CashReceipt.Id, "");
            }
            return retValue;
        }
        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmView != null)
            {
                frmView.Close();
            }
        }
        public void ShowViewForm(string ID)
        {
            if (ViewControl != null)
            {
                frmView = new Form();
                frmView.FormBorderStyle = FormBorderStyle.None;
                frmView.Height = this.Height;
                frmView.Width = this.Width;
                frmView.StartPosition = FormStartPosition.Manual;
                frmView.Location = new Point(this.Location.X + 45, this.Location.Y + 60);
                //  frmView.Icon = EcoMart.Properties.Resources.Icon;
                ViewControl.Mode = OperationMode.ReportView;
                ((IDetailControl)ViewControl).View();
                ViewControl.FillSearchData(ID, "");
                ViewControl.ExitClicked -= new EventHandler(ViewControl_ExitClicked);
                ViewControl.ExitClicked += new EventHandler(ViewControl_ExitClicked);
                ViewControl.Visible = true;
                ViewControl.Height = this.Height - 6;
                ViewControl.Width = this.Width - 6;
                ViewControl.BringToFront();
                ViewControl.Location = new Point(3, 3);
                Panel pnl = new Panel();
                pnl.BackColor = Color.Orange;
                pnl.Dock = DockStyle.Fill;
                pnl.Controls.Add(ViewControl);
                frmView.Controls.Add(pnl);
                frmView.ShowDialog();
            }
        }
        private void mpMSCSale_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                mpMSCSale.ViewControl = new UclDistributorSale("R");               
                string selectedID = selectedRow.Cells[0].Value.ToString();
                voutype = selectedRow.Cells["Col_BillType"].Value.ToString();
                vousubtype = selectedRow.Cells["Col_BillSubType"].Value.ToString();
                if (vousubtype == FixAccounts.SubTypeForRegularSale)
                    ViewControl = new UclDistributorSale("R");                
                ShowViewForm(selectedID);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }

        private bool SaveData(bool printData)
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    _CashReceipt.CBTotalDiscount = GetTotalDiscount();
                    LockTable.LockTablesForCashBankReceipts();
                    _CashReceipt.CBName = mcbCreditor.SeletedItem.ItemData[2];
                    _CashReceipt.CBAddress1 = mcbCreditor.SeletedItem.ItemData[3];
                    _CashReceipt.CBAddress2 = mcbCreditor.SeletedItem.ItemData[7];
                    _CashReceipt.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[8];


                    _CashReceipt.CBAccountID = mcbCreditor.SelectedID;
                    _CashReceipt.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _CashReceipt.CBNarration = txtNarration.Text.ToString().Trim();
                    if (txtAmtNotAdjusted.Text != null)
                    {
                        _CashReceipt.CBOnAccountAmount = Convert.ToDouble(txtAmtNotAdjusted.Text.ToString());
                    }
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _CashReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _CashReceipt.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Add)
                        _preDate = datePickerBillDate.Value;
                    if (_Mode == OperationMode.Edit)
                        _CashReceipt.IFEdit = "Y";

                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _CashReceipt.CBVouNo = _CashReceipt.GetAndUpdateCSRNumber(General.ShopDetail.ShopVoucherSeries);
                    }
                    _CashReceipt.Validate();

                    if (_CashReceipt.IsValid)
                    {
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            //_CashReceipt.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashReceipt.CreatedBy = General.CurrentUser.Id;
                            _CashReceipt.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CashReceipt.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            // _CashReceipt.CBVouNo = _CashReceipt.GetAndUpdateCSRNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _CashReceipt.CBVouNo.ToString();
                            //if (_CashReceipt.CBTotalDiscount > 0)
                            //{
                            //    _CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            //    _CashReceipt.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            //}
                            if (_CashReceipt.CBTotalDiscount > 0)
                            {
                                _CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            }
                            else
                            {
                                _CashReceipt.CBJVNo = 0;
                                _CashReceipt.CBJVIDpay = 0;
                            }
                            _CashReceipt.IntID = 0;
                            _CashReceipt.IntID = _CashReceipt.AddDetails();
                            if (_CashReceipt.IntID > 0)
                                retValue = true;
                            else
                                retValue = false;
                            _SavedID = _CashReceipt.Id;

                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                            {
                                _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashReceipt.AddAccountDetailsIntbltrnacDebit();
                            }
                            if (retValue)
                            {
                                _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashReceipt.AddAccountDetailsIntbltrnacCredit();
                            }
                            if (retValue && _CashReceipt.CBTotalDiscount > 0)
                            {
                                retValue = _CashReceipt.AddToMasterJV();
                                retValue = _CashReceipt.UpdateJVIDInVoucherCashBankReceipt();// update jvid with update jv number
                                _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashReceipt.AddJVTotblTrnacDebit();
                                _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashReceipt.AddJVTotblTrnacCredit();

                            }

                            if (retValue)
                            {
                                SaveOldDetails();
                                General.CommitTransaction();
                            }
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                string msgLine2 = _CashReceipt.CBVouType + "  " + _CashReceipt.CBVouNo.ToString("#0");
                                PSDialogResult result;
                                if (printData)
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetCashReceiptSale =="Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _CashReceipt.CBVouNo + " Of Amount :" + _CashReceipt.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_CashReceipt.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_CashReceipt.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                    if (result == PSDialogResult.Print)
                                        Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetCashReceiptSale == "Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _CashReceipt.CBVouNo + " Of Amount :" + _CashReceipt.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_CashReceipt.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_CashReceipt.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                                _SavedID = _CashReceipt.Id;

                                retValue = true;
                            }
                            else
                            {
                                PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                retValue = false;
                            }
                        }
                        else if (_Mode == OperationMode.Edit)
                        {
                            if (_CashReceipt.ModifyEdit == "Y")
                            {
                                General.BeginTransaction();
                                _CashReceipt.CBAccountID = mcbCreditor.SelectedID;
                                _CashReceipt.ModifiedBy = General.CurrentUser.Id;
                                _CashReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _CashReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                if (_CashReceipt.CBTotalDiscount > 0)
                                {
                                    _CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                }
                                else
                                {
                                    _CashReceipt.CBJVNo = 0;
                                    _CashReceipt.CBJVIDpay = 0;
                                }
                                retValue = _CashReceipt.UpdateDetails();
                                if (retValue)
                                {
                                    retValue = DeletePreviousEntry();
                                    retValue = RevertPreviousSalesBalance();
                                    retValue = saveDetails();
                                }
                                if (retValue)
                                {
                                    retValue = _CashReceipt.DeleteJV();
                                    retValue = _CashReceipt.DeleteAccountDetails();
                                }
                                if (retValue)
                                {
                                    _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashReceipt.AddAccountDetailsIntbltrnacDebit();
                                }
                                if (retValue)
                                {
                                    _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashReceipt.AddAccountDetailsIntbltrnacCredit();
                                }
                                if (retValue && _CashReceipt.CBTotalDiscount > 0)
                                {
                                    //_CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashReceipt.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashReceipt.AddToMasterJV();
                                    _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashReceipt.AddJVTotblTrnacDebit();
                                    _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashReceipt.AddJVTotblTrnacCredit();
                                }

                                if (retValue)
                                {
                                    SaveOldDetails();
                                    General.CommitTransaction();
                                }
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    _CashReceipt.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _CashReceipt.AddChangedDetails();
                                    AddPreviousRowsInChangedDetail();
                                    string msgLine2 = _CashReceipt.CBVouType + "  " + _CashReceipt.CBVouNo.ToString("#0");
                                    PSDialogResult result;
                                    if (printData)
                                    {
                                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                        Print();
                                    }
                                    else
                                    {
                                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                        if (result == PSDialogResult.Print)
                                            Print();
                                    }
                                    _SavedID = _CashReceipt.Id;
                                    retValue = true;
                                }

                                else
                                {
                                    MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    retValue = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _CashReceipt.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            LockTable.UnLockTables();
            return retValue;
        }


        private double GetTotalDiscount()
        {
            double totdisc = 0;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != string.Empty)
                    totdisc += Convert.ToDouble(dr.Cells["Col_DiscountAmount"].Value.ToString());

            }
            return totdisc;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _CashReceipt.Id = ID;
                    if (Vmode == "C")
                        _CashReceipt.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _CashReceipt.ReadDetailsByIDForDeleted();
                    else
                        _CashReceipt.ReadDetailsByID();
                    mcbCreditor.SelectedID = _CashReceipt.CBAccountID;
                    _CashReceipt.ActualAccountID = _CashReceipt.CBAccountID;
                    mpMSCSale.Visible = false;
                    mpMSVC.Visible = true;

                    FillmpMSVCGrid(Vmode);

                    FillmpPVCTempGrid();
                    DateTime mydate = new DateTime(Convert.ToInt32(_CashReceipt.CBVouDate.Substring(0, 4)), Convert.ToInt32(_CashReceipt.CBVouDate.Substring(4, 2)), Convert.ToInt32(_CashReceipt.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtAddress1.Text = _CashReceipt.CBAddress1;
                    txtAddress2.Text = _CashReceipt.CBAddress2;
                    txtNarration.Text = _CashReceipt.CBNarration;
                    txtVouchernumber.Text = _CashReceipt.CBVouNo.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForCashReceipt;
                    txtAmountReceived.Text = _CashReceipt.CBAmount.ToString("#0.00");
                    _CashReceipt.CBOnAccountAmount = _CashReceipt.CBAmount;
                    txtAmtNotAdjusted.Text = _CashReceipt.CBOnAccountAmount.ToString("#0.00");

                    if (_Mode == OperationMode.Add)
                        pnlVou.Enabled = false;
                    else
                    {
                        mcbCreditor.Enabled = false;
                        txtAmountReceived.Enabled = false;
                        pnlNameAddress.Enabled = true;
                        pnlVou.Enabled = true;
                        txtVouchernumber.ReadOnly = false;
                        txtVouchernumber.Enabled = true;
                    }
                    if (_Mode == OperationMode.Edit)
                    {
                        mpMSVC.ClearSelection();
                        mcbCreditor.BackColor = Color.White;
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.BackColor = General.ControlFocusColor;
                        btnModify.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclAccount)
            {
                string preselectedID = "";
                if (mcbCreditor.SelectedID != null)
                    preselectedID = mcbCreditor.SelectedID;
                FillPartyCombo();
                mcbCreditor.SelectedID = preselectedID;
            }
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                //mpMSCSale.ClearSelection();
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {

                    txtAmountReceived.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {

                    btnClearOpeningBalanceClick();
                    retValue = true;
                }

                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {

                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {

                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {

                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Escape)
                {
                    retValue = Exit();
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public void ModifyEdit()
        {
            bool retValue = false;
            try
            {
                tsBtnSearch.Visible = false;
                btnModify.BackColor = Color.White;
                _Mode = OperationMode.Edit;
                _CashReceipt.ModifyEdit = "Y";

                retValue = FillmpPVC1GridSaleforModify();
                FillOpeningBalanceGrid("");

                //  retValue = RevertPreviousEntry();

                //FillDiscountFromTempGrid();
                mpMSCSale.Refresh();
                headerLabel1.Text = "CASH RECEIPT -> MODIFY";
                txtAmtNotAdjusted.Text = _CashReceipt.CBAmount.ToString("#0.00");
                txtAmountReceived.Enabled = true;

                EnableDisable();
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public bool RevertPreviousEntry()
        {
            bool returnVal = true;
            string mpmsvcID = "";
            double pvcClearedAmount = 0;
            double pvcdiscountamt = 0;
            double pvcbalaceamount = 0;
            double mbalaceamount = 0;
            try
            {
                foreach (DataGridViewRow pvcdr in mpMSVC.Rows)
                {
                    mpmsvcID = "";
                    pvcClearedAmount = 0;
                    pvcdiscountamt = 0;
                    pvcbalaceamount = 0;
                    if (pvcdr.Cells["Col_ID"].Value != null && pvcdr.Cells["Col_ID"].Value.ToString() != string.Empty)
                    {
                        mpmsvcID = pvcdr.Cells["Col_ID"].Value.ToString();
                        if (pvcdr.Cells["Col_BalanceAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_BalanceAmount"].Value.ToString(), out pvcbalaceamount);
                        if (pvcdr.Cells["Col_ClearedAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_ClearedAmount"].Value.ToString(), out pvcClearedAmount);
                        if (pvcdr.Cells["Col_DiscountAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_DiscountAmount"].Value.ToString(), out pvcdiscountamt);
                    }
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {



                        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BillType"].Value.ToString() != "OPB")
                        {
                            string drowMSCSaleID = drowMSCSale.Cells["Col_ID"].Value.ToString();
                            if (mpmsvcID == drowMSCSaleID)
                            {
                                mbalaceamount = 0;
                                pvcClearedAmount = 0;  // [ansuman][1/4/2017]
                                if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                    double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
                                //  if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
                                //        double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                                //   if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null)
                                //       double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);

                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + pvcClearedAmount + pvcdiscountamt;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
            //////bool returnVal = true;
            //////try
            //////{
            //////    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
            //////    {

            //////        double mClearedAmount = 0;



            //////        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BillType"].Value.ToString() != "OPB")
            //////        {
            //////            double mbalaceamount = 0;
            //////            double discountamount = 0;

            //////            if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
            //////            if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
            //////            if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out discountamount);


            //////            drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount + discountamount;
            //////            drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";


            //////        }


            //////    }
            //////}
            //////catch (Exception ex)
            //////{
            //////    Log.WriteError(ex.ToString());
            //////    returnVal = false;
            //////}
            //////return returnVal;

        }

        public bool RevertPreviousSalesBalance()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow drowPVCTemp in mpPVCTemp.Rows)
                {
                    string mSaleID = "";
                    string mcbid = "";
                    string mvoutype = "";
                    double mClearedAmount = 0;
                    double mdiscamount = 0;
                    if (drowPVCTemp.Cells["Col_MasterSaleID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_MasterSaleID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_DiscountAmount"].Value != null && drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscamount);
                    if (drowPVCTemp.Cells["Col_BillType"].Value != null)
                        mvoutype = drowPVCTemp.Cells["Col_BillType"].Value.ToString();
                    if (mSaleID != null && mClearedAmount != 0 && mvoutype != "")
                    {

                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            retValue = _CashReceipt.UpdateOpeningBalanceReducePrevious(_CashReceipt.preAccountID, _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditSale )
                            retValue = _CashReceipt.RevertPreviousSalesBalance(mSaleID, mClearedAmount + mdiscamount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementSale )
                            retValue = _CashReceipt.RevertPreviousStatementBalance(mSaleID, mClearedAmount + mdiscamount);
                    }

                    if (retValue == false)
                        break;
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;

        }

        #endregion

        #region Other Private Methods

        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _CashReceipt.DeletePreviousRecords();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return returnVal;
        }

        private bool saveDetails()
        {
            bool returnVal = true;
            _CashReceipt.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow drow in mpMSCSale.Rows)
                {
                    if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                       Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                    {
                        _CashReceipt.SerialNumber += 1;
                        _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (drow.Cells["Col_MasterID"].Value != null)
                            _CashReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                        _CashReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        if (_CashReceipt.DSaleId == "OPB")
                        {
                            _CashReceipt.DSaleId = "0";
                        }
                        else
                        {
                            _CashReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        }
                        _CashReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                        _CashReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                        if (drow.Cells["Col_BillNumber"].Value.ToString() != string.Empty)
                            _CashReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                        if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                            _CashReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                        if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                            _CashReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                        _CashReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                        _CashReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                        _CashReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                        _CashReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                        returnVal = _CashReceipt.AddParticularsDetails();
                        if (returnVal)
                        {
                            if (_CashReceipt.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                returnVal = _CashReceipt.UpdateOpeningBalanceAddNew();
                            else if (_CashReceipt.DVoucherType == FixAccounts.VoucherTypeForStatementSale )
                                returnVal = _CashReceipt.UpdateSaleStatement();
                            else if (_CashReceipt.DVoucherType == FixAccounts.VoucherTypeForCreditSale )
                                returnVal = _CashReceipt.UpdateSCCBill();
                            if (returnVal == false)
                                break;
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;

        }
        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = true;
                _CashReceipt.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_BalanceAmount"].Value != null && drow.Cells["Col_BalanceAmount"].Value.ToString() != "" &&
                           Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value) > 0)
                        {
                            _CashReceipt.SerialNumber += 1;
                            _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _CashReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _CashReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            _CashReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            _CashReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            _CashReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            _CashReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _CashReceipt.AddParticularsDetailsChanged();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInDeletedDetail()
        {
            {
                bool returnVal = true;
                _CashReceipt.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_BalanceAmount"].Value != null && drow.Cells["Col_BalanceAmount"].Value.ToString() != "" &&
                           Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value) > 0)
                        {
                            _CashReceipt.SerialNumber += 1;
                            _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _CashReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _CashReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            _CashReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            _CashReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            _CashReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            _CashReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _CashReceipt.AddParticularsDetailsDeleted();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
                return returnVal;
            }
        }
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                dgClearOpeningBalance.Visible = false;
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                mpMSCSale.IsAllowNewRow = false;
                txtAddress1.Clear();
                txtAddress2.Clear();
                lblFooterMessage.Text = "";
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVoucherSeries.Text = _CashReceipt.CBVouSeries;
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForCashReceipt;
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";
                mpMSVC.ColumnsMain.Clear();
                mpMSCSale.ColumnsMain.Clear();
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                mcbCreditor.SelectedID = "";
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    this.mcbCreditor.Focus();
                    txtVouchernumber.Enabled = false;
                    datePickerFromDate.Visible = true;
                    datePickerToDate.Visible = true;
                    lblFromDate.Visible = true;
                    lblToDate.Visible = true;
                }
                else
                {
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbCreditor.Enabled = false;
                    datePickerFromDate.Visible = false;
                    datePickerToDate.Visible = false;
                    lblFromDate.Visible = false;
                    lblToDate.Visible = false;
                }
                this.mcbCreditor.Focus();
                _saledtable = null;
                _statementdtable = null;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void NoofRows()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void NoofRowsFormpMSVCGrid()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillPartyCombo()//Amar
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[9] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningDebit", "AccClearedAmount", "AccTelephone", "MobileNumberForSMS" };
                mcbCreditor.ColumnWidth = new string[9] { "0", "20", "200", "200", "150", "0", "0", "0","0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorCreditorListForCashBankReceipt();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                {
                    if (mcbCreditor.SeletedItem.ItemData[3] != null)
                        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    if (mcbCreditor.SeletedItem.ItemData[4] != null)
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem.ItemData[5] != null)
                        _CashReceipt.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    if (mcbCreditor.SeletedItem.ItemData[6] != null)
                        _CashReceipt.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _CashReceipt.CBAccountID = mcbCreditor.SelectedID;
                    //  FillmpMSVCGrid("");                       
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                {

                    GetData();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GetData()  //Amar
        {
            try
            {
                _CashReceipt.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
                _CashReceipt.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
                if (mcbCreditor.SeletedItem != null)
                {
                    if (mcbCreditor.SeletedItem.ItemData[3] != null)
                        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    if (mcbCreditor.SeletedItem != null && mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem != null && mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
                        _CashReceipt.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    if (mcbCreditor.SeletedItem != null && mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6] != string.Empty)
                        _CashReceipt.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _CashReceipt.CBAccountID = mcbCreditor.SelectedID;

                    if (_Mode != OperationMode.ReportView)
                        FillmpMSVCGrid("");
                    FillOpeningBalanceGrid("");
                    mpMSCSale.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        //private void GetData()
        //{
        //    _CashReceipt.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
        //    _CashReceipt.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
        //    if (mcbCreditor.SeletedItem != null)
        //    {
        //        if (mcbCreditor.SeletedItem.ItemData[3] != null)
        //            txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
        //        if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
        //            txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
        //        if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
        //            _CashReceipt.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
        //        if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6] != string.Empty)
        //            _CashReceipt.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
        //        _CashReceipt.CBAccountID = mcbCreditor.SelectedID;
        //        //if (_Mode != OperationMode.ReportView)
        //        //{
        //        FillmpMSVCGrid("");
        //        FillOpeningBalanceGrid("");
        //        //}
        //        // if (_Mode != OperationMode.ReportView)

        //        mpMSCSale.ClearSelection();
        //    }
        //}
        private bool FillmpPVC1GridSaleforModify()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();

                IfOpeningAdded = false;
                dtable = _CashReceipt.ReadBillDetailsByIDforModify();
                _statementdtable = _CashReceipt.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();
                BindmpMSCSaleGrid(dtable, _statementdtable);
                retValue = RevertPreviousEntry();
                IfOpeningAdded = true;
                _saledtable = _CashReceipt.ReadBillDetailsByID();
                _statementdtable = _CashReceipt.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
                FillOpeningBalanceGrid("");
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        //private void FillDiscountFromTempGrid()
        //{
        //    string mtempvouid = "";
        //    string msalevouid = "";
        //    double mtempdiscamt = 0;
        //    double balamount = 0;
        //    foreach (DataGridViewRow tempdr in mpPVCTemp.Rows)
        //    {
        //        if (tempdr.Cells["Col_MasterSaleID"].Value != null)
        //            mtempvouid = tempdr.Cells["Col_MasterSaleID"].Value.ToString();
        //        if (tempdr.Cells["Col_DiscountAmount"].Value != null)
        //            mtempdiscamt = Convert.ToDouble(tempdr.Cells["Col_DiscountAmount"].Value.ToString());
        //        if (mtempvouid != FixAccounts.VoucherTypeForOpeningBalance && mtempdiscamt > 0)
        //        {
        //            foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //            {
        //                if (dr.Cells["Col_ID"].Value != null)
        //                    msalevouid = dr.Cells["Col_ID"].Value.ToString();
        //                if (mtempvouid == msalevouid)
        //                {
        //                    if (dr.Cells["Col_BalanceAmount"].Value != null)
        //                        balamount = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
        //                    balamount += mtempdiscamt;
        //                    dr.Cells["Col_BalanceAmount"].Value = balamount.ToString("#0.00");
        //                }
        //            }
        //        }
        //    }
        //}

        private void FormatSaleGrid()
        {
            mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
        }
        private bool FillmpMSVCGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructMainColumns();

                mpMSVC.DoubleColumnNames.Add("Col_BillAmount");
                mpMSVC.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_DiscountAmount");
                mpMSVC.DateColumnNames.Add("Col_VoucherDate");
                mpMSVC.DateColumnNames.Add("Col_BillFromDate");



                ConstructSaleColumns();

                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_DiscountAmount");
                mpMSCSale.DateColumnNames.Add("Col_VoucherDate");
                mpMSCSale.DateColumnNames.Add("Col_BillFromDate");

                DataTable dtable = new DataTable();
                if (_CashReceipt.CBAccountID != null && _CashReceipt.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_CashReceipt.ActualAccountID != _CashReceipt.CBAccountID && _CashReceipt.ModifyEdit == "Y"))
                    {
                        _saledtable = _CashReceipt.ReadBillDetailsByID();
                        _statementdtable = _CashReceipt.ReadStatementDetailsByID();
                        mpMSCSale.Rows.Clear();
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        IfOpeningAdded = false;
                        BindmpMSCSaleGrid(_saledtable, _statementdtable);
                        NoofRows();
                    }
                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            _saledtable = _CashReceipt.ReadBillDetailsByCSRIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _CashReceipt.ReadBillDetailsByCSRIDForDeleted();
                        else
                        {
                            _saledtable = _CashReceipt.ReadBillDetailsByCSRID();
                            //  _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();
                        }
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in _saledtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                _CashReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                                _CashReceipt.DiscountInOpeningBalance = Convert.ToDouble(dr["DiscountAmount"].ToString());
                                break;
                            }
                        }
                        retValue = true;
                        txtVouchernumber.BackColor = Color.White;
                        btnModify.BackColor = General.ControlFocusColor;
                    }

                }

                retValue = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }



        private void BindmpMSVCGrid(DataTable saletable, DataTable statementtable)
        {
            // view
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            try
            {
                if (_Mode == OperationMode.Add)
                {
                    if ((_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared) > 0)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared).ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {
            //modify
            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_CashReceipt.preAccountID != null && _CashReceipt.preAccountID != "") && _CashReceipt.CBAccountID != _CashReceipt.preAccountID) || _CashReceipt.ModifyEdit == "Y"))
                {
                    if ((_CashReceipt.OpeningClearedInVoucher > 0 || (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance) > 0) && (_CashReceipt.preAccountID == null || _CashReceipt.CBAccountID == _CashReceipt.preAccountID))
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        if ((_CashReceipt.preAccountID == null || _CashReceipt.preAccountID == "") || ((_CashReceipt.preAccountID != null || _CashReceipt.preAccountID != "") && _CashReceipt.CBAccountID == _CashReceipt.preAccountID))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _CashReceipt.OpeningBalance;
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashReceipt.OpeningClearedInVoucher >= 0)
                    {
                        if (_CashReceipt.OpeningBalance > 0 && (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_BillType"].Value = "OPB";
                            currentdr.Cells["Col_BillNumber"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private bool SearchforIDInSaleGrid(string ID)
        {
            bool retValue = false;
            string _GridID = "";
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    _GridID = dr.Cells["Col_ID"].Value.ToString();
                if (_GridID == ID)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }
        private bool FillmpPVCTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructTempColumns();
                FormatTempGrid();
                DataTable dtable = new DataTable();
                dtable = _CashReceipt.ReadBillDetailsByCSRID();
                _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();
                //  mpPVCTemp.DataSource .DataSourceMain = dtable;
                BindTempGrid(dtable, _statementdtable);
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }




        private void FormatTempGrid()
        {
            //mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
            //mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
            //mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
            mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");
        }
        private void BindTempGrid(DataTable saletable, DataTable statementtable)
        {

            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

            }

        }


        #endregion

        #region Construct columns
        private void ConstructPrintGridColumns()
        {
            DataGridViewTextBoxColumn column;
            PrintGrid.Columns.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                PrintGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                PrintGrid.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                ////// added new columns 29/3/2015

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                // column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFMultipleMRP";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);



            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series.";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type.";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                //7 -- 9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);



                //9 -- 11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSVC.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.Visible = true;
                column.ReadOnly = false;
                mpMSVC.ColumnsMain.Add(column);


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSaleColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSCSale.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.HeaderText = "Series";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.HeaderText = "Type,";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.Width = 30;
                if (_Mode == OperationMode.Add)
                {
                    column.Visible = true;
                    //column.ReadOnly = false;
                }
                else
                {
                    column.Visible = false;
                    //column.ReadOnly = true;
                }
                mpMSCSale.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);



                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);


                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount.";
                column.Width = 110;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSCSale.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = false;
                mpMSCSale.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVCTemp.Columns.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region EVENTS



        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_Mode == OperationMode.Add)
                {
                    if (_CashReceipt.PaymentSubType == 2)
                        mpMSCSale.SetFocus(0, 6); // old 11
                    else if (_CashReceipt.PaymentSubType == 3 || (_CashReceipt.PaymentSubType == 0))
                        mpMSCSale.SetFocus(0, 13);
                    else
                        txtNarration.Focus();
                }
                else
                    mpMSCSale.SetFocus(0, 13);
            }
            else if (e.KeyCode == Keys.Up)
                txtAmountReceived.Focus();
        }

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 13 && (_CashReceipt.PaymentSubType == 3 || _CashReceipt.PaymentSubType == 0))// old 11
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _CashReceipt.CellOldValueAmount = getclearedamt;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (getclearedamt == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

                    }
                    //Sale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        {
            //  txtAmountReceived.Enabled = false;
            double totalreceived = 0;
            double getclearedamt = 0;
            double mbalanceAmount = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double mdiscountamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);

            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            try
            {
                if (colIndex == 6)
                {
                    ColCheckChecked();
                }
                if (colIndex == 13) // old 8
                {
                    if (_CashReceipt.PaymentSubType == 3 || _CashReceipt.PaymentSubType == 0)
                    {
                        if (getclearedamt == 0)
                        {
                            _CashReceipt.CellOldValueAmount = 0;
                        }

                        if (mbalanceAmount < getclearedamt)
                        {
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                            _CashReceipt.CellOldValueAmount = clearedamt;
                        }
                        else
                        {
                            if (mamtnotadj == 0)
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashReceipt.CellOldValueAmount;


                            //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                            //{

                            _CashReceipt.CellOldValueAmount = getclearedamt;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                double mdiscount = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                    mcleared = getclearedamt;
                                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                                //totalreceived += mcleared + mdiscount;
                            }
                            mamtnotadj = (mbillamt - totalreceived);

                            if (mamtnotadj < 0)
                            {
                                mamtnotadj = 0;
                                _CashReceipt.CellOldValueAmount = 0;
                                _CashReceipt.CellOldValueDiscount = 0;
                                getclearedamt = 0;
                            }
                            totalreceived = 0;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                double mdiscount = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                    mcleared = getclearedamt;
                                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                                //totalreceived += mcleared + mdiscount;
                            }

                            mamtnotadj = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                            if (mamtnotadj >= 0 && getclearedamt > 0)
                            {

                                mdiscountamt = mbalanceAmount - getclearedamt;
                                if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                                {
                                    if (mdiscountamt > 10)
                                        mdiscountamt = 0;
                                }
                            }
                            else
                                mdiscountamt = 0;
                            if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                            {
                                if (mdiscountamt > 10)
                                    mdiscountamt = 0;
                            }

                            if (getclearedamt == 0)
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                            else
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
                            //  mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                            if (mdiscountamt == 0)
                            {
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                                mpMSCSale.NextRowColumn = 13;
                            }
                            else
                            {
                                mpMSCSale.NextRowColumn = 14;
                                mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;

                            }
                        }
                        //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                        //if (mpMSCSale.Rows.Count > rowindex + 1)
                        //    mpMSCSale.SetFocus(rowindex + 1, 9);
                        //}

                    }
                    //else
                    //{
                    //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = mbalanceAmount.ToString("#0.00");
                    //}
                }
                else if (colIndex == 14)
                {
                    double mdiscgiven = 0;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    if (mdiscgiven != 0)
                    {
                        if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    }
                    totalreceived = 0;
                    foreach (DataGridViewRow dr in mpMSCSale.Rows)
                    {
                        double mcleared = 0;
                        double mdiscount = 0;
                        if (dr.Cells["Col_GetClearedAmount"].Value != null)
                            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);

                        if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                            double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);

                        //totalreceived += (mcleared + mdiscount);
                        totalreceived += (mcleared);
                    }
                    if (mbillamt - totalreceived > 0)
                        mamtnotadj = (mbillamt - totalreceived);
                    else
                        mamtnotadj = 0;
                    txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                    //double mdiscgiven = 0;
                    //if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                    //    double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    //mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    //if (mdiscgiven != 0)
                    //{
                    //    if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                    //        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    //}
                    ////if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

                    ////    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ColCheckChecked()
        {
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value == null || mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value.ToString().Trim() == string.Empty)
            {
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
            }
            else
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = " ";
            double totamount = 0;
            double amt = 0;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_Check"].Value.ToString().Trim() != string.Empty && dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                {
                    amt = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    dr.Cells["Col_GetClearedAmount"].Value = amt.ToString("#0.00");
                    totamount += amt;
                }
                else
                    dr.Cells["Col_GetClearedAmount"].Value = "0.00";
            }
            // mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
            txtAmountReceived.Text = totamount.ToString("#0.00");
            //  mpMSCSale.Refresh();     
            mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 6);
        }
        //private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        //{
        //    txtAmountReceived.Enabled = false;
        //    double totalreceived = 0;
        //    double getclearedamt = 0;
        //    double mbalanceAmount = 0;
        //    double mamtnotadj = 0;
        //    double clearedamt = 0;
        //    double mbillamt = 0;
        //    double mdiscountamt = 0;
        //    double.TryParse(txtAmountReceived.Text, out mbillamt);
        //    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);

        //    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
        //        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
        //    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
        //    double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

        //    clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
        //    try
        //    {

        //        if (colIndex == 12) // old 8
        //        {

        //            if (getclearedamt == 0)
        //            {
        //                _CashReceipt.CellOldValueAmount = 0;
        //            }

        //            if (mbalanceAmount < getclearedamt)
        //            {
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
        //                _CashReceipt.CellOldValueAmount = clearedamt;
        //            }
        //            else
        //            {
        //                if (mamtnotadj == 0)
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashReceipt.CellOldValueAmount;


        //                //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
        //                //{

        //                _CashReceipt.CellOldValueAmount = getclearedamt;
        //                foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //                {
        //                    double mcleared = 0;
        //                    double mdiscount = 0;
        //                    if (dr.Cells["Col_GetClearedAmount"].Value != null)
        //                        double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
        //                    if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
        //                        mcleared = getclearedamt;
        //                    if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //                        double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
        //                    if (mcleared > 0)
        //                        totalreceived += mcleared;
        //                }
        //                mamtnotadj = (mbillamt - totalreceived);

        //                if (mamtnotadj < 0)
        //                {
        //                    mamtnotadj = 0;
        //                    _CashReceipt.CellOldValueAmount = 0;
        //                    _CashReceipt.CellOldValueDiscount = 0;
        //                    getclearedamt = 0;
        //                }
        //                txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
        //                if (mamtnotadj >= 0 && getclearedamt > 0)
        //                {
        //                    mdiscountamt = mbalanceAmount - getclearedamt;
        //                    if (mdiscountamt > 10)
        //                        mdiscountamt = 0;
        //                }
        //                else
        //                    mdiscountamt = 0;


        //                if (getclearedamt == 0)
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //                else
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
        //                //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
        //                //if (mpMSCSale.Rows.Count > rowindex + 1)
        //                //    mpMSCSale.SetFocus(rowindex + 1, 9);
        //                //}
        //            }
        //        }
        //        else if (colIndex == 13)
        //        {
        //            double mdiscgiven = 0;
        //            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
        //            mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
        //            if (mdiscgiven != 0)
        //            {
        //                if (mdiscgiven != mdiscountamt || getclearedamt == 0)
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //            }
        //            //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

        //            //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        private void mpMSCSale_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 13 && (txtAmountReceived.Text != txtTotalBalance.Text && mpMSCSale.ColumnsMain["Col_Check"].ReadOnly != false))// old 12
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashReceipt.CellOldValueAmount.ToString("#0.00");
                //else if (e.ColumnIndex == 13 && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BillType"].Value.ToString() == "OPB")
                //{
                //    btnClearOpeningBalanceClick();
                //}

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                ModifyEdit();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (datePickerFromDate.Visible == true)
                    datePickerFromDate.Focus();
                else
                    txtAmountReceived.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _CashReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _CashReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();

                        _CashReceipt.ReadDetailsByVouNumber();
                        if (mpMSVC.Rows.Count > 1)
                        {
                            mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                            System.Threading.Thread.Sleep(10);
                        }
                        FillSearchData(_CashReceipt.Id, "");
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }


        #endregion

        #region UIEvents

        private void UclCashReceipt_Load(object sender, EventArgs e)
        {
            dgClearOpeningBalance.Visible = false;
            datePickerBillDate.Value = General.TodayDateTime;
            dgClearOpeningBalance.Visible = false;
            datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
            datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
        }

        private void mpMSCSale_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            ClearGrid();

            mcbCreditor.Focus();
        }

        private void btnClearOpeningBalance_Click(object sender, EventArgs e)
        {
            btnClearOpeningBalanceClick();
        }

        private void btnClearOpeningBalanceClick()
        {
            double opamt = 0;
            try
            {
                if (mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty)
                    opamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
                txtOpeningBalanceAmount.Text = opamt.ToString("#0.00");
                if (opamt > 0 && dgClearOpeningBalance.Rows.Count > 0)
                {
                    dgClearOpeningBalance.Visible = true;
                    dgClearOpeningBalance.Enabled = true;
                    dgClearOpeningBalance.SetFocus(0, 12);
                }
                else if (opamt > 0)

                    lblFooterMessage.Text = "No Previous Records...";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgClearOpeningBalance_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 12) // old 8
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    // txtOpeningBalanceAmount.Text = mpMSCSale.MainDataGridCurrentRow.Cells[12].ToString();
                    double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _CashReceipt.CellOpeningBalanceOldValueAmount = getclearedamt;
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (getclearedamt == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

                    }
                    dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 12) // old 8
                    dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashReceipt.CellOpeningBalanceOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellValueChangeCommited(int colIndex)
        {
            txtAmountReceived.Enabled = false;
            txtOpeningBalanceAmount.Enabled = false;
            double totalreceived = 0;
            double getclearedamt = 0;
            double mbalanceAmount = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double mdiscountamt = 0;
            mbillamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
            double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);

            if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            try
            {

                if (colIndex == 12) // old 8
                {

                    if (getclearedamt == 0)
                    {
                        _CashReceipt.CellOpeningBalanceOldValueAmount = 0;
                    }

                    if (mbalanceAmount < getclearedamt)
                    {
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _CashReceipt.CellOpeningBalanceOldValueAmount = clearedamt;
                    }
                    else
                    {
                        if (mamtnotadj == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashReceipt.CellOpeningBalanceOldValueAmount;


                        //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                        //{

                        _CashReceipt.CellOpeningBalanceOldValueAmount = getclearedamt;
                        foreach (DataGridViewRow dr in dgClearOpeningBalance.Rows)
                        {
                            double mcleared = 0;
                            double mdiscount = 0;
                            if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                            if (dr.Index == dgClearOpeningBalance.MainDataGridCurrentRow.Index)
                                mcleared = getclearedamt;
                            if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                            if (mcleared > 0)
                                totalreceived += mcleared;
                        }

                        mamtnotadj = (mbillamt - totalreceived);

                        if (mamtnotadj < 0)
                        {
                            mamtnotadj = 0;
                            _CashReceipt.CellOpeningBalanceOldValueAmount = 0;
                            _CashReceipt.CellOpeningBalanceOldValueDiscount = 0;
                            getclearedamt = 0;
                        }
                        txtOpeningBalanceAmount.Text = mamtnotadj.ToString("#0.00");
                        if (mamtnotadj >= 0 && getclearedamt > 0)
                        {
                            mdiscountamt = mbalanceAmount - getclearedamt;
                            if (mdiscountamt > 10)
                                mdiscountamt = 0;
                        }
                        else
                            mdiscountamt = 0;


                        if (getclearedamt == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                        else
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                        //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                        //if (mpMSCSale.Rows.Count > rowindex + 1)
                        //    mpMSCSale.SetFocus(rowindex + 1, 9);
                        //}
                    }
                }
                else if (colIndex == 13)
                {
                    double mdiscgiven = 0;
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    if (mdiscgiven != 0)
                    {
                        if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    }
                    //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

                    //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgClearOpeningBalance_OnEscapeKeyPressed(object sender, EventArgs e)
        {



        }

        private bool FillOpeningBalanceGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();

                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BillAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_GetClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_DiscountAmount");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_VoucherDate");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_BillFromDate");



                ConstructdgClearOpeningBalanceColumns();

                dgClearOpeningBalance.DoubleColumnNames.Add("Col_GetClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BillAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BalanceAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_ClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_DiscountAmount");
                dgClearOpeningBalance.DateColumnNames.Add("Col_VoucherDate");
                dgClearOpeningBalance.DateColumnNames.Add("Col_BillFromDate");

                DataTable dtable = new DataTable();
                if (_CashReceipt.CBAccountID != null && _CashReceipt.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_CashReceipt.ActualAccountID != _CashReceipt.CBAccountID && _CashReceipt.ModifyEdit == "Y"))
                    {
                        _saledtable = _CashReceipt.ReadOldBillDetailsByID();
                        _statementdtable = _CashReceipt.ReadOldStatementDetailsByID();
                        dgClearOpeningBalance.Rows.Clear();
                        BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        //IfOpeningAdded = false;
                        //BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        NoofRows();
                    }
                    else
                    {
                        dgClearOpeningBalanceTemp.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            _saledtable = _CashReceipt.ReadOldBillDetailsByCSRIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _CashReceipt.ReadOldBillDetailsByCSRIDForDeleted();
                        else
                        {
                            /////////  _saledtable = _CashReceipt.ReadOldBillDetailsByCSRID();
                            //  _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();

                            _saledtable = _CashReceipt.ReadOldBillDetailsByID();
                            _statementdtable = _CashReceipt.ReadOldStatementDetailsByID();
                            dgClearOpeningBalance.Rows.Clear();
                            BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);

                        }
                        BinddgClearOpeningBalanceTempGrid(_saledtable, _statementdtable);
                        // NoofRowsFormpMSVCGrid();
                        //foreach (DataRow dr in _saledtable.Rows)
                        //{
                        //    if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                        //    {
                        //        _CashReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                        //        _CashReceipt.DiscountInOpeningBalance = Convert.ToDouble(dr["DiscountAmount"].ToString());
                        //        break;
                        //    }
                        //}
                        retValue = true;
                        //txtVouchernumber.BackColor = Color.White;
                        //btnModify.BackColor = General.ControlFocusColor;
                    }

                }

                retValue = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void BinddgClearOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            // view
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            if (_Mode == OperationMode.Add)
            {
                if ((_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared) > 0)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared).ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                    IfOpeningAdded = true;
                }
            }
            if (saletable != null && saletable.Rows.Count > 0)
            {
                foreach (DataRow dr in saletable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
        }

        private void BinddgClearOpeningBalanceGrid(DataTable saletable, DataTable statementtable)
        {
            //modify
            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            IfOpeningAdded = true;
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_CashReceipt.preAccountID != null && _CashReceipt.preAccountID != "") && _CashReceipt.CBAccountID != _CashReceipt.preAccountID) || _CashReceipt.ModifyEdit == "Y"))
                {
                    if ((_CashReceipt.OpeningClearedInVoucher > 0 || (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance) > 0) && (_CashReceipt.preAccountID == null || _CashReceipt.CBAccountID == _CashReceipt.preAccountID))
                    {
                        _rowIndex = dgClearOpeningBalance.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        if ((_CashReceipt.preAccountID == null || _CashReceipt.preAccountID == "") || ((_CashReceipt.preAccountID != null || _CashReceipt.preAccountID != "") && _CashReceipt.CBAccountID == _CashReceipt.preAccountID))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _CashReceipt.OpeningBalance;
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashReceipt.OpeningClearedInVoucher >= 0)
                    {
                        if (_CashReceipt.OpeningBalance > 0 && (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_BillType"].Value = "OPB";
                            currentdr.Cells["Col_BillNumber"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _CashReceipt.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashReceipt.OpeningBalance - _CashReceipt.OpeningCleared + _CashReceipt.OpeningClearedInVoucher + _CashReceipt.DiscountInOpeningBalance).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void ConstructdgClearOpeningBalanceColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalance.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.HeaderText = "Series";
                column.Width = 70;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.HeaderText = "Type,";
                column.ReadOnly = true;
                column.Width = 55;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);



                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);


                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount.";
                column.Width = 110;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructdgClearOpeningBalanceTempMainColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalanceTemp.Columns.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool SaveOldDetails()
        {
            bool returnVal = true;
            _CashReceipt.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow drow in dgClearOpeningBalance.Rows)
                {
                    if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                       Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                    {
                        _CashReceipt.SerialNumber += 1;
                        //   _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (drow.Cells["Col_MasterID"].Value != null)
                            _CashReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                        _CashReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        _CashReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                        _CashReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                        if (drow.Cells["Col_BillNumber"].Value.ToString() != string.Empty)
                            _CashReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                        if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                            _CashReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                        if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                            _CashReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                        _CashReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                        _CashReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                        _CashReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                        _CashReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                        //  returnVal = _CashReceipt.AddParticularsDetailsOldSale();                        

                        if (_CashReceipt.DVoucherType == FixAccounts.VoucherTypeForStatementSale )
                            returnVal = _CashReceipt.UpdateSaleStatementOld();
                        else if (_CashReceipt.DVoucherType == FixAccounts.VoucherTypeForCreditSale )
                            returnVal = _CashReceipt.UpdateSCCBillOld();


                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }

            return returnVal;
        }

        private bool FillOpeningBalanceTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();
                //  FormatOpeningBalanceTempGrid();
                DataTable dtable = new DataTable();
                dtable = _CashReceipt.ReadBillDetailsByCSRIDFromtblOld();
                _statementdtable = _CashReceipt.ReadStatementDetailsByCSRIDFromtblOld();
                //  mpPVCTemp.DataSource .DataSourceMain = dtable;
                BindOpeningBalanceTempGrid(dtable, _statementdtable);
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void BindOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

            }
        }
        private void datePickerFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime dd = datePickerFromDate.Value.AddMonths(1).AddDays(-1);
                datePickerToDate.Value = dd;
                datePickerToDate.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }

        private void datePickerToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData();
                txtAmountReceived.Text = txtTotalBalance.Text;
                txtAmountReceived.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                datePickerFromDate.Focus();
        }
        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            if (mpMSCSale.ColumnsMain["Col_Check"].ReadOnly == true)
                ClearGrid();
        }

        public void ClearGrid()
        {
            try
            {
                txtAmtNotAdjusted.Text = txtAmountReceived.Text;
                _CashReceipt.CellOldValueAmount = 0;
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    dr.Cells["Col_GetClearedAmount"].Value = 0;
                    dr.Cells["Col_DiscountAmount"].Value = 0;
                    dr.Cells["Col_Check"].Value = " ";
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAmountReceived_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearGrid();
                txtNarration.Focus();
                mpMSCSale.ClearSelection();
                _CashReceipt.PaymentSubType = 0;

                if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0 && _Mode == OperationMode.Add)
                {
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                }
                else
                {

                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                }
                if (_Mode == OperationMode.Add)
                {

                    if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
                    {
                        _CashReceipt.PaymentSubType = 1;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                        FillGridForDates();
                    }
                    else if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0)
                    {
                        _CashReceipt.PaymentSubType = 2;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;

                    }
                    else
                    {
                        _CashReceipt.PaymentSubType = 3;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                    }
                }

                // txtChequeNumber.Focus();
                txtNarration.Focus();


            }
        }
        private void FillGridForDates()
        {
            //double clearedAmount = 0;
            //double balanceAmount = 0;
            if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
            {
                txtAmtNotAdjusted.Text = "0.00";
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                    {
                        dr.Cells["Col_GetClearedAmount"].Value = dr.Cells["Col_BalanceAmount"].Value;
                    }
                }
            }
        }

        #endregion UIEvents

        #region tooltip

        #endregion
    }
}
