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
using EcoMart.InterfaceLayer;
using EcoMart.Printing;
using PrintDataGrid;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStatementPartywisePurchase : BaseControl
    {
        # region Declaration     
        Settings _Settings;
        PrintingVariables  _PrintVariables;
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private Statements _Statements;
        private DataTable _PaymentDetailsBindingSource;
        private double _MCashAmount;
        private double _MCreditAmount;
        private double _MCreditStatementAmount;  
        private BaseControl   ViewControl;
        private Form frmView;
        #endregion Declaration

        #region Constructor
        public UclStatementPartywisePurchase()
        {
            try
            {

                InitializeComponent();                
                _Settings = new Settings();
                _Statements = new Statements();
                _PrintVariables = new PrintingVariables();
                _Purchase = new Purchase();
                ViewControl = new UclPurchase();
                SearchControl = new UclStatementPartywisePurchaseSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add)
                fromDate1.Focus();
            else
                txtStmtNumber.Focus();
        }
        public override bool ClearData()
        {
            ClearControls();

            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            fromDate1.Value = DateTime.Now;
            toDate1.Value = DateTime.Now;
            headerLabel1.Text = "STATEMENT PURCHASE -> NEW";

            tsBtnSave.Enabled = false;
            tsBtnSavenPrint.Enabled = false;
            tsBtnCancel.Visible = false;
            pnlPaymentDetails.Visible = false;
            btnPaymentHistory.Visible = false;
            return retValue;
        }
       
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();         
            fromDate1.Focus();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            ClearData();
            pnlMultiSelection1.Visible = false;
            headerLabel1.Text = "STATEMENT PURCHASE -> DELETE";
            txtStmtNumber.Focus();
            FillPartyCombo();
            return retValue;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_Statements.Id != null && _Statements.Id != "")
                {
                    if (_Statements.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _Statements.DeleteStatementsPurchase(_Statements.Id);
                        if (retValue)
                            retValue = _Statements.RemoveStatementNumbersFrommasterPurchase(_Statements.StatementNumber);
                       
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Log.WriteException(Ex);
            }
            return true; 
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            pnlMultiSelection1.Visible = false;
            headerLabel1.Text = "STATEMENT PURCHASE -> VIEW";
            tsBtnAdd.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnDelete.Enabled = false;
            tsBtnSearch.Enabled = true;
            tsBtnPrint.Enabled = false;
            tsBtnFirst.Visible = false;
            tsBtnLast.Visible = false;
            tsBtnNext.Visible = false;
            tsBtnPrevious.Visible = false;
            txtStmtNumber.Focus(); 
            FillPartyCombo();
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;

            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                _Statements.AccountID = mcbCreditor.SelectedID;           
            _Statements.Vat5Percent = 0;
            _Statements.Vat12point5Percent = 0;
            _Statements.Validate();
            System.Text.StringBuilder _errorMessage;
            if (_Statements.IsValid)
            {
                LockTable.LockTablesForStatementPurchase();

                General.BeginTransaction();


                _Statements.StatementNumber = 0;              
                _Statements.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _Statements.CreatedBy = General.CurrentUser.Id;
                _Statements.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _Statements.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                _Statements.StatementNumber = _Statements.GetAndUpdatePurchaseStatementNumber(General.ShopDetail.ShopVoucherSeries);

               
                _Statements.VoucherType = FixAccounts.VoucherTypeForStatementPurchase;
                if (_Statements.AccountID != "")
                    retValue = _Statements.AddDetailsPurchase();

                //}
                if (retValue)
                {
                    string mvoutype = "";
                    foreach (DataGridViewRow dgvr in dgvReportList.Rows)
                    {

                        if (dgvr.Cells["Col_ID"].Value != null && dgvr.Cells["Col_ID"].Value.ToString() != "")
                        {
                            _Purchase.Id = dgvr.Cells["Col_ID"].Value.ToString();
                            if (dgvr.Cells["Col_VoucherType"].Value != null)
                                mvoutype = dgvr.Cells["Col_VoucherType"].Value.ToString();
                            if (mvoutype != FixAccounts.VoucherTypeForCashPurchase)
                            {
                                if (mvoutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                                {
                                    retValue = _Statements.AddStatementNumberInPurchaseVoucher(_Purchase.Id, _Statements.StatementNumber, _Statements.Id);
                                }
                                //else
                                //{
                                //    if (_Settings.MsetPurchaseIncludeCreditPurchaseInStatements == "Y" && mvoutype == FixAccounts.VoucherTypeForCreditPurchase)
                                //        retValue = _Statements.AddStatementNumberInPurchaseVoucher(_Purchase.Id, _Statements.StatementNumber, _Statements.Id);
                                //}
                            }
                        }
                        if (retValue == false)
                            break;
                    }
                }



                if (retValue)
                    General.CommitTransaction();
                else
                    General.RollbackTransaction();
                LockTable.UnLockTables();
                if (retValue)
                {
                    string msgLine2 = FixAccounts.VoucherTypeForStatementPurchase + "  " + _Statements.StatementNumber.ToString("#0");
                    PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                    if (result == PSDialogResult.Print)
                        Print();                   
                    retValue = true;
                }
                else
                {
                    PSDialogResult result = PSMessageBox.Show("Could not Save...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                    retValue = false;
                }
            }
            else
            {
                LockTable.UnLockTables();
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Statements.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _Statements.Id = ID;
                _Statements.ReadDetailsByID(_Statements.Id); 
                InitializeReportGrid();
                pnlAmounts.Visible = true;
               _Statements.GetDetailsFromMaster(_Statements.StatementNumber, FixAccounts.VoucherTypeForStatementPurchase);
                mcbCreditor.SelectedID = _Statements.AccountID;
                txtStmtNumber.Text = _Statements.StatementNumber.ToString("#0");
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    _Statements.Name = mcbCreditor.SeletedItem.ItemData[2].ToString();
                ShowpnlGO();
                FillReportGrid();
            }

            return true;
        }


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {
            //try
            //{
            //    FillPartyCombo(); 
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Home)
            {
                HidepnlGO();
                retValue = true;
            }
            if (keyPressed == Keys.End)
            {
                btnOKMultiSelectionClick();
                retValue = true;
            }
            if (keyPressed == Keys.Escape)
            {
                if (pnlPaymentDetails.Visible == true)
                    pnlPaymentDetails.Visible = false;
                else
                    retValue = Exit();
                retValue = true;
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        public void GetOverviewData()
        {
            try
            {
                _BindingSource = new DataTable();  
                _Statements.FromDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _Statements.ToDate = DateTime.Now.Date.ToString("yyyyMMdd");
                headerLabel1.Text = "PURCHASE STATEMENTS - PARTYWISE";
                InitializeReportGrid();
                FormatReportGrid();
                FillReportGrid();
                ClearControls();
                FillPartyCombo();
                pnlMultiSelection1.Visible = true;
                AddToolTip();              
                mcbCreditor.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

     
        #endregion IDetail Members

        # region IReport Members

        public override bool Print()
        {
            bool retValue = true;
            PrintData();
            return retValue;
        }

        private void PrintData()
        {
            PrintRow row;
            try
            {
               
                 _PrintVariables.PrintReportHead = "Purchase Statement  From : " + General.GetDateInDateFormat(_Statements.FromDate) + " To : " + General.GetDateInDateFormat(_Statements.ToDate);
                 _PrintVariables.PrintReportHead2 = "[" + txtViewText.Text.ToString() + "]";
                PrintBill.Rows.Clear();
                _PrintVariables.PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                double totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                 _PrintVariables.PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();               
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    double mamt = 0;
                    if (dr.Cells["Col_VoucherType"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow("Continued....", PrintRowPixel, 15,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString().PadRight(30), PrintRowPixel, 1,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadRight(30), PrintRowPixel, 60,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString().PadRight(30), PrintRowPixel, 120,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_BillNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(30), PrintRowPixel, 220,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(320.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix,  _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow("Number of Bills : " + _Statements.NumberofBills.ToString().Trim(), PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                mlen = (_Statements.StatementAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(320.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_Statements.StatementAmount.ToString("#0.00"), PrintRowPixel, colpix, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);

                
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead()
        {
            PrintRow row;
            try
            {

                _PrintVariables.PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));

             

                PrintRowPixel += 17;

                row = new PrintRow(General.ShopDetail.ShopName , PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " "+General.ShopDetail.ShopAddress2, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow("Party : "+ _Statements.Name + " "+General.ShopDetail.ShopAddress2, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow("Statement Number : " + _Statements.StatementNumber.ToString().Trim() + "  From : "+General.GetDateInShortDateFormat( _Statements.FromDate) + "  To :" + General.GetDateInShortDateFormat(_Statements.ToDate) , PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Type", PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 60,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 120,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill Number", PrintRowPixel, 220,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 350,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
             _PrintVariables.PrintRowCount = 0;
            return  _PrintVariables.PrintRowCount;
        }
        #endregion IReport Memebers

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                string startdate = DateTime.Now.Date.ToString("yyyyMMdd");
                _Statements.FromDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _Statements.ToDate = DateTime.Now.Date.ToString("yyyyMMdd");                
                lblMessage.Text = "";
                txtViewText.Text = "";
                ViewFromDate.Text = "";
                ViewToDate.Text = "";
                txtStmtNumber.Text = "";
                txtReportTotalAmount.Text = "0.00";
                txtCashAmount.Text = "0.00";
                txtCreditAmount.Text = "0.00";
                txtCreditStatementAmount.Text = "0.00";
                mcbCreditor.SelectedID = "";
                ConstructReportColumns();
                FillReportGrid();
                dgvReportList.Bind();
                FillPartyCombo();               
                if (_Mode == OperationMode.Add)
                    HidepnlGO();
                else
                {
                    dgvReportList.Visible = false;
                    pnlAmounts.Visible = false;
                    pnlMultiSelection1.Visible = false;
                    txtStmtNumber.Focus();
                    txtCashAmount.Visible = false;
                    txtCreditAmount.Visible = false;
                    txtCreditStatementAmount.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructPaymentDetailsColumns()
        {
            dgPaymentDetails.ColumnsMain.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurID";
                column.DataPropertyName = "MasterPurchaseID";
                column.HeaderText = "PurID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 80;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 80;
                dgPaymentDetails.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.DefaultCellStyle.Format = "d2";
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "ClearAmount";
                column.HeaderText = "Cleared Amount";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPaymentDetails.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CbID";
                column.DataPropertyName = "CBID";
                column.HeaderText = "cID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbpurID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "pID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        
        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.Visible = false;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 60;
                column.ReadOnly = true;
                dgvReportList.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "Bill.Number";
                column.Width = 100;
                column.ReadOnly = true;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = true;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountVAT5Percent";
                column.DataPropertyName = "AmountVAT5Percent";              
                column.Width = 120;
                column.Visible = false;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountVAT12point5Percent";
                column.DataPropertyName = "AmountVAT12point5Percent";               
                column.Width = 120;
                column.Visible = false;
                dgvReportList.ColumnsMain.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportGrid()
        {

            try
            {
                FillReportData();
                dgvReportList.DataSourceMain = _BindingSource;
                dgvReportList.Bind();
                if (_Mode == OperationMode.Add)
                {
                    _Statements.StatementAmount = 0;
                    _MCashAmount = 0;
                    _MCreditAmount = 0;
                    _MCreditStatementAmount = 0;
                    _Statements.NumberofBills = 0;
                    _Statements.Vat5Percent = 0;
                    _Statements.Vat12point5Percent = 0;
                    string voutype = "";
                    double amt = 0;
                    double vamt5 = 0;
                    double vamt12point5 = 0;
                    foreach (DataGridViewRow dr in dgvReportList.Rows)
                    {
                        vamt5 = 0;
                        vamt12point5 = 0;
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "" && dr.Cells["Col_VoucherType"].Value != null)
                        {
                            voutype = dr.Cells["Col_VoucherType"].Value.ToString();
                            amt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            if (dr.Cells["Col_AmountVAT5Percent"].Value != null && dr.Cells["Col_AmountVAT5Percent"].Value.ToString() != "")
                                vamt5 = Convert.ToDouble(dr.Cells["Col_AmountVAT5Percent"].Value.ToString());
                            if (dr.Cells["Col_AmountVAT12point5Percent"].Value != null && dr.Cells["Col_AmountVAT12point5Percent"].Value.ToString() != "")
                                vamt12point5 = Convert.ToDouble(dr.Cells["Col_AmountVAT12point5Percent"].Value.ToString());

                            if (voutype == FixAccounts.VoucherTypeForCashPurchase)
                                _MCashAmount += amt;
                            else if (voutype == FixAccounts.VoucherTypeForCreditPurchase)
                            {
                                _MCreditAmount += amt;
                                //if (_Settings.MsetPurchaseIncludeCreditPurchaseInStatements == "Y")
                                //{
                                //    _Statements.StatementAmount += amt;
                                //    _Statements.Vat5Percent += vamt5;
                                //    _Statements.Vat12point5Percent += vamt12point5;
                                //    _Statements.NumberofBills += 1;
                                //}
                            }
                            else if (voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                            {
                                _MCreditStatementAmount += amt;
                                _Statements.StatementAmount += amt;
                                _Statements.Vat5Percent += vamt5;
                                _Statements.Vat12point5Percent += vamt12point5;
                                _Statements.NumberofBills += 1;
                            }
                        }
                    }
                }
                else
                {
                    tsBtnDelete.Enabled = true;
                    tsBtnPrint.Enabled = true;

                }
                txtReportTotalAmount.Text = _Statements.StatementAmount.ToString("#0.00");
                txtNumberOfBills.Text = _Statements.NumberofBills.ToString("#0");
                txtVat5per.Text = _Statements.Vat5Percent.ToString("#0.00");
                txtVat12point5per.Text = _Statements.Vat12point5Percent.ToString("#0.00");
                txtCashAmount.Text = _MCashAmount.ToString("#0.00");
                txtCreditAmount.Text = _MCreditAmount.ToString("0.00");
                txtCreditStatementAmount.Text = _MCreditStatementAmount.ToString("#0.00");                
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblMessage.Text = strmessage;
        }
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                if (_Mode == OperationMode.Add)
                {
                    _Statements.FromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                    _Statements.ToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                        dtable = _Purchase.GetOverviewDataForPartywiseBillsForStatements(mcbCreditor.SelectedID, _Statements.FromDate, _Statements.ToDate);
                }
                else
                    dtable = _Purchase.GetOverviewDataForPartywiseStatementsView(_Statements.StatementNumber, General.ShopDetail.ShopVoucherSeries);

                _BindingSource = dtable;                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.ColumnsMain["Col_ID"].Visible = false;
            FormatReportGrid(); 
        }

        public void HidepnlGO()
        {               
            pnlAmounts.Visible = false;
            pnlMultiSelection1.Visible = true;          
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {           
            pnlAmounts.Visible = true;
            pnlMultiSelection1.Visible = false;          
            dgvReportList.Visible = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_Statements.FromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_Statements.ToDate);
            txtViewText.Text = _Statements.Name;
            dgvReportList.Focus();
        }
        #endregion

        # region Events
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    _Statements.Name = mcbCreditor.SeletedItem.ItemData[2].ToString();
                _Statements.FromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _Statements.ToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_Statements.FromDate,_Statements.ToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    InitializeReportGrid();
                    tsBtnSave.Enabled = true;
                    tsBtnSavenPrint.Enabled = true;
                    ShowpnlGO();
                    lblMessage.Text = "";                  
                    FillReportGrid();
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();
                }
                else
                    lblMessage.Text = "Check Date";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                toDate1.Focus();              
            }
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCreditor.Focus();
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
                ViewControl.FillSearchData(ID,"C");
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

        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmView != null)
            {
                frmView.Close();
            }
        }

        private void txtStmtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtStmtNumber.Text != null && txtStmtNumber.Text.ToString() != "")
                    _Statements.StatementNumber = Convert.ToInt32(txtStmtNumber.Text.ToString());
                InitializeReportGrid();
                pnlAmounts.Visible = true;
                _Statements.GetDetailsFromMaster(_Statements.StatementNumber, FixAccounts.VoucherTypeForStatementPurchase);  
                mcbCreditor.SelectedID = _Statements.AccountID;
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                _Statements.Name = mcbCreditor.SeletedItem.ItemData[2].ToString();
                ShowpnlGO();
                FillReportGrid();
            }
        }
#endregion events

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip

        private void dgvReportList_OnShowViewForm(DataGridViewRow selectedRow)
        {
            try
            {
                if (selectedRow != null && dgvReportList.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[0].Value.ToString();

                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void UclStatementPartywisePurchase_Load(object sender, EventArgs e)
        {
            tsBtnCancel.Visible = false;
            tsBtnDelete.Enabled = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnPrint.Enabled = false;
            tsBtnSearch.Visible = false;
            tsBtnPrevious.Visible = false;
            tsBtnNext.Visible = false;
            tsBtnFirst.Visible = false;
            tsBtnLast.Visible = false;
            tsBtnSave.Enabled = false;
            tsBtnSavenPrint.Enabled = false;
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPaymentDetails.Visible == false)
                {
                    BindPaymentDetails(mcbCreditor.SelectedID);
                    pnlPaymentDetails.BringToFront();
                    pnlPaymentDetails.Visible = true;
                    dgPaymentDetails.Visible = true; 
                }
                else
                {
                    pnlPaymentDetails.SendToBack();
                    dgPaymentDetails.Visible = false;                  
                    pnlPaymentDetails.Visible = false;  
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnPaymentHistory_Click>>" + Ex.Message);
            }
        }
        private void dgPaymentDetails_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            try
            {
                if (selectedRow != null && dgPaymentDetails.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[6].Value.ToString();
                    voutype = selectedRow.Cells["Col_VoucherType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForBankPayment)
                        ViewControl = new UclBankPayment();
                    else if (voutype == FixAccounts.VoucherTypeForCashPayment)
                        ViewControl = new UclCashPayment();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void BindPaymentDetails(string accID)
        {
            try
            {
                ConstructPaymentDetailsColumns();
                DataTable tmptable = new DataTable();
                _Purchase.Id = _Statements.Id;
                tmptable = _Purchase.ReadPaymentDetailsStatementByID();
                _PaymentDetailsBindingSource = tmptable;
                BindPaymentDetails(_PaymentDetailsBindingSource);
                //dgPaymentDetails.DataSource = _PaymentDetailsBindingSource;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool BindPaymentDetails(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgPaymentDetails != null)
                    dgPaymentDetails.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgPaymentDetails.Rows.Add();
                    string voudt = "";
                    DataGridViewRow currentdr = dgPaymentDetails.Rows[_RowIndex];
                    currentdr.Cells["Col_CrdbID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_PurID"].Value = dr["MasterPurchaseID"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    double clamt = Convert.ToDouble(dr["ClearAmount"].ToString());
                    currentdr.Cells["Col_AmountNet"].Value = clamt.ToString("0.00");
                    currentdr.Cells["Col_CbID"].Value = dr["CBID"].ToString();
                    currentdr.Cells["Col_CrdbpurID"].Value = dr["purchaseID"].ToString();
                    _RowIndex += 1;

                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }
        //here
      
    }
}
