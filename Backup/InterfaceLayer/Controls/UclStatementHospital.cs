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
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.Printing;
using PrintDataGrid;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStatementHospital : BaseControl
    {
        # region Declaration
        Settings _Settings;
        PrintingVariables _PrintVariables;
        private DataTable _BindingSource;
        private SSSale _SSSale;
        private Statements _Statements;
        private double _MCashAmount;
        private double _MCreditAmount;      
        private BaseControl ViewControl;
        private Form frmView;
        #endregion Declaration

        #region Constructor
        public UclStatementHospital()
        {
            try
            {
                InitializeComponent();
                _Settings = new Settings();
                _Statements = new Statements();
                _PrintVariables = new PrintingVariables();
                _SSSale = new SSSale(); 
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
                mcbCreditor.Focus();
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
            _Settings.FillSettings();
            headerLabel1.Text = "STATEMENT HOSPITAL SALE -> NEW";
            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            mcbCreditor.Focus();
            return retValue;
        }


        public override bool Delete()
        {
            bool retValue = base.Delete();
            ClearData();
            headerLabel1.Text = "STATEMENT HOSPITAL SALE -> DELETE";
            txtStmtNumber.Focus();
            FillHospitalPatientCombo();
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
                        retValue = _Statements.DeleteStatementsSale(_Statements.Id);
                        if (retValue)
                            retValue = _Statements.RemoveStatementNumbersFrommasterSale(_Statements.StatementNumber);

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
            headerLabel1.Text = "STATEMENT HOSPITAL SALE -> VIEW";
            txtStmtNumber.Focus();
            FillHospitalPatientCombo();
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
                LockTable.LockTablesForStatementSale();

                General.BeginTransaction();


                _Statements.StatementNumber = 0;
                _Statements.AccountID = "";
                _Statements.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _Statements.CreatedBy = General.CurrentUser.Id;
                _Statements.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _Statements.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                _Statements.StatementNumber = _Statements.GetAndUpdateSaleStatementNumber(General.ShopDetail.ShopVoucherSeries);


                _Statements.VoucherType = FixAccounts.VoucherTypeForStatementHospital;
                _Statements.AccountID = mcbCreditor.SelectedID;
                if (_Statements.AccountID != "")
                    retValue = _Statements.AddDetailsSale();
                //}
                if (retValue)
                {
                    string mvoutype = "";
                    foreach (DataGridViewRow dgvr in dgvReportList.Rows)
                    {

                        if (dgvr.Cells["Col_ID"].Value != null && dgvr.Cells["Col_ID"].Value.ToString() != "")
                        {
                            _SSSale.Id = dgvr.Cells["Col_ID"].Value.ToString();
                            if (dgvr.Cells["Col_VoucherType"].Value != null)
                                mvoutype = dgvr.Cells["Col_VoucherType"].Value.ToString();
                            if (mvoutype != FixAccounts.VoucherTypeForCashSale)
                            {
                                if (mvoutype == FixAccounts.VoucherTypeForCreditSale)
                                {
                                    retValue = _Statements.AddStatementNumberInSaleVoucher(_SSSale.Id, _Statements.StatementNumber, _Statements.Id);
                                }                              
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
                    string msgLine2 = FixAccounts.VoucherTypeForStatementHospital + "  " + _Statements.StatementNumber.ToString("#0");
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
            return true;
        }


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
        {
            try
            {
                FillHospitalPatientCombo();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                retValue = Exit();
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
                headerLabel1.Text = "SALE STATEMENTS - PARTYWISE";
                InitializeReportGrid();
                FormatReportGrid();
                FillReportGrid();
                ClearControls();
                FillHospitalPatientCombo();
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

                _PrintVariables.PrintReportHead = "SALE Statement  From : " + General.GetDateInDateFormat(_Statements.FromDate) + " To : " + General.GetDateInDateFormat(_Statements.ToDate);
                _PrintVariables.PrintReportHead2 = "[" + txtViewText.Text.ToString() + "]";
                PrintBill.Rows.Clear();
                _PrintVariables.PrintFont = new Font("Arial", 10, FontStyle.Regular);
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
                            row = new PrintRow("Continued....", PrintRowPixel, 15, _PrintVariables.PrintFont);
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
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString().PadRight(30), PrintRowPixel, 1, _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadRight(30), PrintRowPixel, 60, _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString().PadRight(30), PrintRowPixel, 120, _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_BillNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(30), PrintRowPixel, 220, _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(320.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, _PrintVariables.PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow("Number of Bills : " + _Statements.NumberofBills.ToString().Trim(), PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                mlen = (_Statements.StatementAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(320.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_Statements.StatementAmount.ToString("#0.00"), PrintRowPixel, colpix, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, _PrintVariables.PrintFont);
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
                //string reverseFeed = ((char)27).ToString() + ((char)106).ToString() + ((char)216).ToString();
                //PrintFactory.sendTextToLPT1(reverseFeed);
                //reverseFeed = ((char)27).ToString() + ((char)106).ToString() + ((char)216).ToString();
                //PrintFactory.sendTextToLPT1(reverseFeed);
                //reverseFeed = ((char)27).ToString() + ((char)106).ToString() + ((char)216).ToString();
                //PrintFactory.sendTextToLPT1(reverseFeed);
                // PrintBill.Rows.Add(((char)27).ToString() + "j" + ((char)216).ToString());

                //  string reverseFeed =  ((char)27).ToString() + "j" + ((char)216).ToString();
                _PrintVariables.PrintFont = new Font("Arial", 10, FontStyle.Regular);

                //   row = new PrintRow(reverseFeed, PrintRowPixel, 1,  _PrintVariables.PrintFont);
                //   PrintBill.Rows.Add(row);

                //   PrintBill.Print_Bill();

                PrintRowPixel += 17;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " " + General.ShopDetail.ShopAddress2, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow("Party : " + _Statements.Name + " " + General.ShopDetail.ShopAddress2, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow("Statement Number : " + _Statements.StatementNumber.ToString().Trim() + "  From : " + General.GetDateInShortDateFormat(_Statements.FromDate) + "  To :" + General.GetDateInShortDateFormat(_Statements.ToDate), PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Type", PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 60, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 120, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill Number", PrintRowPixel, 220, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 350, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, _PrintVariables.PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            _PrintVariables.PrintRowCount = 0;
            return _PrintVariables.PrintRowCount;
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

                FillReportGrid();
                dgvReportList.Bind();
                FillHospitalPatientCombo();
                mcbCreditor.SelectedID = "";
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
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 60;
                dgvReportList.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "No";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub Type";
                column.Width = 60;
                dgvReportList.ColumnsMain.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                dgvReportList.ColumnsMain.Add(column);
                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
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

                            if (voutype == FixAccounts.VoucherTypeForCashSale)
                                _MCashAmount += amt;
                            else if (voutype == FixAccounts.VoucherTypeForCreditSale)
                            {
                                _MCreditAmount += amt;
                             
                                    _Statements.StatementAmount += amt;
                                    _Statements.Vat5Percent += vamt5;
                                    _Statements.Vat12point5Percent += vamt12point5;
                                    _Statements.NumberofBills += 1;
                                
                            }                           
                        }
                    }
                }

                txtReportTotalAmount.Text = _Statements.StatementAmount.ToString("#0.00");
                txtNumberOfBills.Text = _Statements.NumberofBills.ToString("#0");
                txtVat5per.Text = _Statements.Vat5Percent.ToString("#0.00");
                txtVat12point5per.Text = _Statements.Vat12point5Percent.ToString("#0.00");
                txtCashAmount.Text = _MCashAmount.ToString("#0.00");
                txtCreditAmount.Text = _MCreditAmount.ToString("0.00");               
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
                    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                        dtable = _SSSale.GetOverviewDataForHospitalStatement(mcbCreditor.SelectedID);
                }
                else
                    dtable = _SSSale.GetOverviewDataForPartywiseStatementsView(_Statements.StatementNumber, General.ShopDetail.ShopVoucherSeries);

                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillHospitalPatientCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[6] { "ID", "InwardNumber", "PatientName", "Address1", "Address2", "RoomNumber" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "100", "200", "150", "150","50" };               
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclHospitalPatient();
                HospitalPatient _Party = new HospitalPatient();
                DataTable dtable = _Party.GetOverviewDataForStatement();
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
            //    dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlAmounts.Visible = false;
            pnlMultiSelection1.Visible = true;
            mcbCreditor.Focus();
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
                retValue = General.CheckDates(_Statements.FromDate, _Statements.ToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    InitializeReportGrid();
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

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    string vousubtype = dgvReportList.SelectedRow.Cells["Col_SubType"].Value.ToString();                   
                    ViewControl = new UclHospitalSale();
                   
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
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
                //  frmView.Icon = PharmaSYSRetailPlus.Properties.Resources.Icon;
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
                _Statements.GetDetailsFromMaster(_Statements.StatementNumber, FixAccounts.VoucherTypeForStatementHospital);
                mcbCreditor.SelectedID = _Statements.AccountID;
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
    }
}
