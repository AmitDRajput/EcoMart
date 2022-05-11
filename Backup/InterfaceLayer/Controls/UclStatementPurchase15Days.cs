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
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;
using PrintDataGrid;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStatementPurchase15Days : BaseControl
    {
        # region Declaration
        DataTable _StatementData;
        DataTable _BillData;
        int month = 0;
        string syear = "";
        string smonth = "";
        string _vouType = FixAccounts.VoucherTypeForStatementPurchase;
        Statements _Statements;
        private BaseControl ViewControl;     
        #endregion Declaration

        #region Constructor
        public UclStatementPurchase15Days()
        {
            try
            {
                InitializeComponent();
                _BillData = new DataTable();
                _StatementData = new DataTable();
                _Statements = new Statements();
                ViewControl = new UclPurchase();
                
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
            txtMonth.Focus();
        }
        public override bool ClearData()
        {
            txtMonth.Text = "";
            txtYear.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtCreatedDate.Text = "";
            dgvbills.Visible = false;
            dgvStatements.Visible = false;
            pnlMultiSelection1.Visible = true;
            _BillData = null;
            _StatementData = null;
            _Statements.FromDate = "";
            _Statements.ToDate = "";
            return true;


        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "STATEMENT PURCHASE 15 DAYS -> NEW";
            string StmtDate = "";
            StmtDate = DateTime.Now.Date.ToString("yyyyMMdd");
            StmtDate = General.GetDateInShortDateFormat(StmtDate);
            pnlMultiSelection1.Visible = true;
            txtCreatedDate.Text = StmtDate;
            _Statements.VoucherType = FixAccounts.VoucherTypeForStatementPurchase;
            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            return retValue;
        }


        public override bool Delete()
        {
            bool retValue = base.Delete();
            ClearData();
            headerLabel1.Text = "STATEMENT PURCHASE 15 DAYS -> DELETE";
            return retValue;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            Statements stmt = new Statements();
            GetFirstAndLastNumbers();
            GetLastVoucherNumberFortblVoucherNumbers();
            if (_Statements.LastStatementNumberinTable == _Statements.LastStatementNumber)
            {
                retValue = stmt.CheckCanbeDeletedPurchaseStatement(_Statements.LastStatementNumber);
                if (retValue)
                {
                    retValue = stmt.DeleteStatementsPurchase(_Statements.StatementNumber, _Statements.LastStatementNumber, FixAccounts.VoucherTypeForStatementPurchase, General.ShopDetail.ShopVoucherSeries);
                    retValue = stmt.RemoveStatementNumbersFrommasterPurchase(_Statements.StatementNumber, _Statements.LastStatementNumber, General.ShopDetail.ShopVoucherSeries);
                    _Statements.LastStatementNumber = _Statements.StatementNumber - 1;
                    retValue = stmt.UpdateLastStatementNumberInTblVoucherNumbersPurchase();
                    lblMessage.Text = "Deleted Successfully...";
                    ConstructStatmentColumns();
                    ClearData();
                }
                else
                    lblMessage.Text = "Can not Delete...";
               
            }
            else
                lblMessage.Text = "Not Last Statement Can not Delete...";
            return true;
        }

        private void GetLastVoucherNumberFortblVoucherNumbers()
        {
            _Statements.GetLastVoucherNumberFortblVoucherNumbersPurchase();
        }

        public override bool Exit()
        {      
            bool retValue =  base.Exit();
             //ClearData();
             //ConstructBillColumns();
             //ConstructStatmentColumns();
             //pnlMultiSelection1.Visible = true;
             return retValue;

        }
        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            pnlMultiSelection1.Visible = true;
            headerLabel1.Text = "STATEMENT PURCHASE - 15 DAYS -> VIEW";
            return retValue;
        }
        public override bool Print()
        {
            bool retValue = true;
            PrintStatements();
            ClearData();
            return retValue;
        }

        public void PrintStatements()
        {
            PrintRow row;
            PharmaSYSRetailPlus.Printing.GeneralSettings generalPrintSettings;
            generalPrintSettings = General.PrintSettings.SaleBillPrintSettingsPlainPaper.GeneralSettings;
           
            try
            {
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvbills.Rows.Count;
                int headrows = dgvStatements.Rows.Count * 6;
                totalrows += headrows;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                int prestatementnumber = 0;
                int currentstatementnumber = 0;
                string prename = " ";
                string preadd1 = " ";
                string preadd2 = " ";
                string pretel = " ";
                int jindex = 0;
                string ifleft = "Y";
                double totalAmount = 0;

                for (int index = 0; index < dgvbills.Rows.Count; index++)
                {
                    if (dgvbills.Rows[index].Cells["Col_StatementNumber"].Value != null)
                    {
                        prestatementnumber = Convert.ToInt32(dgvbills.Rows[index].Cells["Col_StatementNumber"].Value.ToString());
                        if (dgvbills.Rows[index].Cells["Col_AccountName"].Value != null && dgvbills.Rows[index].Cells["Col_AccountName"].Value.ToString() != "")
                            prename = dgvbills.Rows[index].Cells["Col_AccountName"].Value.ToString();

                        if (dgvbills.Rows[index].Cells["Col_Address1"].Value != null && dgvbills.Rows[index].Cells["Col_Address1"].Value.ToString() != "")
                            preadd1 = dgvbills.Rows[index].Cells["Col_Address1"].Value.ToString();
                       // if (dgvbills.Rows[index].Cells["Col_Address2"].Value != null && dgvbills.Rows[index].Cells["Col_Address2"].Value.ToString() != "")
                          //  preadd2 = dgvbills.Rows[index].Cells["Col_Address2"].Value.ToString();
                       // if (dgvbills.Rows[index].Cells["Col_Telephone"].Value != null && dgvbills.Rows[index].Cells["Col_Telephone"].Value.ToString() != "")
                          //  pretel = dgvbills.Rows[index].Cells["Col_Telephone"].Value.ToString();
                        PrintRowPixel = 0;
                        totalAmount = 0;
                        PrintHeader(prestatementnumber, prename, preadd1, preadd2, pretel, fnt);
                        for (jindex = index; jindex < dgvbills.Rows.Count; jindex++)
                        {
                            if (dgvbills.Rows[jindex].Cells["Col_StatementNumber"].Value != null)
                                currentstatementnumber = Convert.ToInt32(dgvbills.Rows[jindex].Cells["Col_StatementNumber"].Value.ToString());
                            else
                                currentstatementnumber = 0;
                            if (prestatementnumber != currentstatementnumber)
                                break;
                            else
                            {
                                if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                                {
                                    PrintRowPixel = 325;
                                    row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                                    PrintBill.Rows.Add(row);
                                    PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                                    PrintBill.Rows.Clear();
                                    PrintRowPixel = 0;
                                    PrintHeader(prestatementnumber, prename, preadd1, preadd2, pretel, fnt);

                                    rowcount = 0;
                                }

                                if (ifleft == "Y")
                                {
                                    PrintRowPixel += 17;
                                    rowcount += 1;
                                    row = new PrintRow(dgvbills.Rows[jindex].Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 1, fnt);
                                    PrintBill.Rows.Add(row);
                                    row = new PrintRow(dgvbills.Rows[jindex].Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 50, fnt);
                                    PrintBill.Rows.Add(row);
                                    double mamt = Convert.ToDouble(dgvbills.Rows[jindex].Cells["Col_AmountNet"].Value.ToString());
                                    totalAmount += mamt;
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 150, fnt);
                                    PrintBill.Rows.Add(row);
                                    ifleft = "N";
                                }
                                else
                                {
                                    row = new PrintRow(dgvbills.Rows[jindex].Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 300, fnt);
                                    PrintBill.Rows.Add(row);
                                    row = new PrintRow(dgvbills.Rows[jindex].Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 350, fnt);
                                    PrintBill.Rows.Add(row);
                                    double mamt = Convert.ToDouble(dgvbills.Rows[jindex].Cells["Col_AmountNet"].Value.ToString());
                                    totalAmount += mamt;
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 450, fnt);
                                    PrintBill.Rows.Add(row);
                                    ifleft = "Y";
                                }

                            }

                        }
                        PrintRowPixel = 325;
                        //PrintRowPixel = 418;
                        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                        PrintBill.Rows.Add(row);
                        PrintRowPixel += 17;
                        row = new PrintRow(totalAmount.ToString("#0.00"), PrintRowPixel, 150, fnt);
                        PrintBill.Rows.Add(row);
                        PrintRowPixel += 17;
                        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                        PrintBill.Rows.Add(row);

                        PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                        ifleft = "Y";
                        if (jindex < dgvbills.Rows.Count)
                            index = jindex - 1;
                        else
                            index = jindex;
                    }
                }




                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void PrintHeader(int statementNumber, string mname, string madd1, string madd2, string mtel, Font fnt)
        {
            PrintRow row;
            try
            {
                string billtype = "";
                billtype = "Sale Statement:";

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(statementNumber.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(mname, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(madd1, 107, 30, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(madd2, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow("Tel:" + mtel, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_Statements.StatementDate, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow("Vou No     Date           Amount                                              Vou No  Date       Amount", PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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

            _Statements.StatementAmount = 1;
            _Statements.AccountID = dgvStatements.Rows[0].Cells["Col_AccountID"].Value.ToString();
            _Statements.Validate();

            if (_Statements.IsValid)
            {
                foreach (DataGridViewRow dgvr in dgvStatements.Rows)
                {
                    _Statements.StatementNumber = 0;
                    _Statements.AccountID = "";
                    _Statements.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Statements.CreatedBy = General.CurrentUser.Id;
                    _Statements.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Statements.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    if (dgvr.Cells["Col_StatementNumber"].Value != null && dgvr.Cells["Col_StatementNumber"].Value.ToString() != "")
                    {
                        _Statements.StatementNumber = Convert.ToInt32(dgvr.Cells["Col_StatementNumber"].Value.ToString());
                        _Statements.LastStatementNumber = _Statements.StatementNumber;
                    }

                    if (dgvr.Cells["Col_AccountID"].Value != null && dgvr.Cells["Col_AccountID"].Value.ToString() != "")
                        _Statements.AccountID = dgvr.Cells["Col_AccountID"].Value.ToString();
                    if (dgvr.Cells["Col_Amount"].Value != null && dgvr.Cells["Col_Amount"].Value.ToString() != "")
                        _Statements.StatementAmount = Convert.ToDouble(dgvr.Cells["Col_Amount"].Value.ToString());
                    if (dgvr.Cells["Col_Vat5Percent"].Value != null && dgvr.Cells["Col_Vat5Percent"].Value.ToString() != "")
                        _Statements.Vat5Percent = Convert.ToDouble(dgvr.Cells["Col_Vat5Percent"].Value.ToString());
                    if (dgvr.Cells["Col_Vat12point5Percent"].Value != null && dgvr.Cells["Col_Vat12point5Percent"].Value.ToString() != "")
                        _Statements.Vat12point5Percent = Convert.ToDouble(dgvr.Cells["Col_Vat12point5Percent"].Value.ToString());
                    if (dgvr.Cells["Col_NoofBills"].Value != null && dgvr.Cells["Col_NoofBills"].Value.ToString() != string.Empty)
                        _Statements.NumberofBills = Convert.ToInt32(dgvr.Cells["Col_NoofBills"].Value.ToString());
                    if (_Statements.AccountID != "")
                    retValue = _Statements.AddDetailsPurchase();

                }
                foreach (DataGridViewRow dgvr in dgvbills.Rows)
                {
                    _Statements.StatementNumber = 0;
                   string  PurchaseId = "";
                    int stmtno = 0;
                    if (dgvr.Cells["Col_ID"].Value != null && dgvr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        PurchaseId = dgvr.Cells["Col_ID"].Value.ToString();
                    }
                    if (dgvr.Cells["Col_StatementNumber"].Value != null)
                        int.TryParse(dgvr.Cells["Col_StatementNumber"].Value.ToString(), out stmtno);
                    _Statements.StatementNumber = stmtno;
                    if (_Statements.Id != "")
                    retValue = _Statements.AddStatementNumberInPurchaseVoucher(PurchaseId,_Statements.StatementNumber,_Statements.Id);
                }
                retValue = _Statements.UpdateLastStatementNumberInTblVoucherNumbersPurchase();
                if (retValue)
                {
                    PSDialogResult result;
                    if (printData)
                    {
                        result = PSMessageBox.Show("Information has been saved successfully.", "", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                        Print();
                    }
                    else
                    {
                        result = PSMessageBox.Show("Information has been saved successfully.", "", General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                        if (result == PSDialogResult.Print)
                            Print();
                    }
                }

            }
            ClearData();
            ConstructBillColumns();
            ConstructStatmentColumns();          
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }

        public void GetFirstAndLastNumbers()
        {
            int stmtno = 0;
            if (_StatementData.Rows.Count > 0)
            {
                if (dgvStatements.Rows[0].Cells["Col_StatementNumber"].Value != null)
                {
                    int.TryParse(dgvStatements.Rows[0].Cells["Col_StatementNumber"].Value.ToString(), out stmtno);
                    _Statements.StatementNumber = stmtno;
                }
                foreach (DataGridViewRow dgvr in dgvStatements.Rows)
                {
                    stmtno = 0;
                    if (dgvr.Cells["Col_StatementNumber"].Value != null)
                    {
                        int.TryParse(dgvr.Cells["Col_StatementNumber"].Value.ToString(), out stmtno);
                        _Statements.LastStatementNumber = stmtno;
                    }
                }
            }
        }

        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
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
        private void ConstructStatmentColumns()
        {
            try
            {
                dgvStatements.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StatementNumber";
                column.HeaderText = "Stmt Number";
                column.DataPropertyName = "VoucherNumber";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.Visible = false;
                column.DataPropertyName = "AccountID";
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.HeaderText = "Party";
                column.DataPropertyName = "AccName";
                column.Width = 250;
                dgvStatements.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address1";
                column.HeaderText = "Address";
                column.DataPropertyName = "AccAddress1";
                column.Width = 250;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.DataPropertyName = "AmountNet";
                column.Width = 150;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NoofBills";
                column.HeaderText = "Bills";
                column.DataPropertyName = "NumberOfBills";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat5Percent";
                column.DataPropertyName = "VAT5Per";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    column.Width = 150;
                dgvStatements.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat12point5Percent";
                column.DataPropertyName = "VAT12point5Per";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              //  column.Width = 150;
                dgvStatements.ColumnsMain.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructBillColumns()
        {
            try
            {
                dgvbills.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.Visible = false;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StatementNumber";
                column.HeaderText = "Stmt Number";
                column.DataPropertyName = "statementNumber";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Vou Number";
                column.DataPropertyName = "VoucherNumber";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "Bill Number";
                column.DataPropertyName = "PurchaseBillNumber";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Vou Date";
                column.DataPropertyName = "VoucherDate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.Visible = false;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 200;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address1";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 150;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VouType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Visible = false;
                column.Width = 150;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat5Percent";
                column.DataPropertyName = "AmountVAT5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat12point5Percent";
                column.DataPropertyName = "AmountVAT12point5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.ColumnsMain.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public void GetOverviewData()
        {



        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }



        #endregion IDetail Members
        #region events

        private void btnGo_Click(object sender, EventArgs e)
        {
            Statements stmt = new Statements();
            try
            {
               
                if (_Mode == OperationMode.Add)
                    _BillData = stmt.GetOverviewDataPurchase15Days(_Statements.FromDate, _Statements.ToDate);
                else
                {
                    _BillData = stmt.GetOverviewDataPurchase15DaysForView(_Statements.FromDate, _Statements.ToDate);
                    _StatementData = stmt.GetOverviewDataBothStatementForView(_vouType, _Statements.FromDate, _Statements.ToDate);
                }
                ConstructStatmentColumns();
                ConstructBillColumns();

                pnlMultiSelection1.Visible = false;
                dgvbills.DataSourceMain = _BillData;
                dgvbills.Bind();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }


            try
            {
                if (_Mode == OperationMode.Add)
                {
                    dgvStatements.Visible = false;
                    dgvbills.Visible = true;
                    int stmtno = stmt.GetPurchaseStatementToView(General.ShopDetail.ShopVoucherSeries);
                    int startstmtno = stmtno;
                    string accountID = "";
                    string curraccountID = "";
                    string accountName = "";
                    string accountAddress1 = "";
                    double amount = 0;
                    double totalAmount = 0;
                    int noofbills = 0;
                    int _RowIndex = 0;
                    int mcount = dgvbills.Rows.Count;
                    if (_BillData != null && _BillData.Rows.Count > 0)
                    {
                        if (dgvbills.Rows[0].Cells["Col_AccountID"].Value != null)
                        {
                            curraccountID = dgvbills.Rows[0].Cells["Col_AccountID"].Value.ToString();
                            dgvbills.Rows[0].Cells["Col_StatementNumber"].Value = stmtno.ToString();
                        }
                        foreach (DataGridViewRow dgvr in dgvbills.Rows)
                        {
                            if (dgvr.Cells["Col_AccountID"].Value != null)
                            {
                                accountID = dgvr.Cells["Col_AccountID"].Value.ToString();
                                if (curraccountID == accountID)
                                {
                                    dgvr.Cells["Col_StatementNumber"].Value = stmtno.ToString();


                                    amount = amount + Convert.ToDouble(dgvr.Cells["Col_AmountNet"].Value.ToString());
                                    accountName = dgvr.Cells["Col_AccountName"].Value.ToString();
                                    accountAddress1 = dgvr.Cells["Col_Address1"].Value.ToString();
                                    noofbills += 1;
                                }
                                else
                                {


                                    totalAmount += amount;
                                    _RowIndex = dgvStatements.Rows.Add();
                                    dgvStatements.Rows[_RowIndex].Cells["Col_AccountID"].Value = curraccountID;
                                    dgvStatements.Rows[_RowIndex].Cells["Col_StatementNumber"].Value = stmtno.ToString();
                                    dgvStatements.Rows[_RowIndex].Cells["Col_AccountName"].Value = accountName;
                                    dgvStatements.Rows[_RowIndex].Cells["Col_Address1"].Value = accountAddress1;
                                    dgvStatements.Rows[_RowIndex].Cells["Col_Amount"].Value = amount.ToString("#0.00");
                                    dgvStatements.Rows[_RowIndex].Cells["Col_NoofBills"].Value = noofbills.ToString("#0");
                                    curraccountID = dgvr.Cells["Col_AccountID"].Value.ToString();

                                    stmtno += 1;
                                    dgvr.Cells["Col_StatementNumber"].Value = stmtno.ToString();
                                    curraccountID = dgvr.Cells["Col_AccountID"].Value.ToString();

                                    //  stmtno = Convert.ToInt32(dgvr.Cells["Col_StatementNumber"].Value.ToString());
                                    amount = Convert.ToDouble(dgvr.Cells["Col_AmountNet"].Value.ToString());
                                    accountName = dgvr.Cells["Col_AccountName"].Value.ToString();
                                    accountAddress1 = dgvr.Cells["Col_Address1"].Value.ToString();
                                    noofbills = 1;

                                }
                            }

                        }

                        totalAmount += amount;
                        _RowIndex = dgvStatements.Rows.Add();
                        dgvStatements.Rows[_RowIndex].Cells["Col_AccountID"].Value = curraccountID;
                        dgvStatements.Rows[_RowIndex].Cells["Col_StatementNumber"].Value = stmtno.ToString();
                        dgvStatements.Rows[_RowIndex].Cells["Col_AccountName"].Value = accountName;
                        dgvStatements.Rows[_RowIndex].Cells["Col_Address1"].Value = accountAddress1;
                        dgvStatements.Rows[_RowIndex].Cells["Col_Amount"].Value = amount.ToString("#0.00");
                        dgvStatements.Rows[_RowIndex].Cells["Col_NoofBills"].Value = noofbills.ToString("#0");
                        dgvStatements.Visible = true;
                        dgvbills.Visible = false;
                        lblMessage.Text = "";
                        dgvStatements.ColumnsMain[0].ReadOnly = true;
                        dgvStatements.ColumnsMain[1].ReadOnly = true;
                        dgvStatements.ColumnsMain[2].ReadOnly = true;
                        dgvStatements.ColumnsMain[3].ReadOnly = true;
                        dgvStatements.ColumnsMain[4].ReadOnly = true;
                        dgvStatements.ColumnsMain[5].ReadOnly = true;
                        dgvStatements.ColumnsMain[6].ReadOnly = true;
                        dgvStatements.ColumnsMain[7].ReadOnly = true;
                        dgvStatements.ColumnsMain[8].ReadOnly = true;
                    }
                    else
                        lblMessage.Text = "No Records for the given period..";
                }
                else
                {
                    dgvStatements.Visible = true;
                    dgvbills.Visible = false;
                    dgvStatements.DataSourceMain = _StatementData;
                    dgvStatements.Bind();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            int month = 0;
            if (txtMonth.Text != null && txtMonth.Text != "")
                month = Convert.ToInt32(txtMonth.Text.ToString());
            if (month < 1 || month > 12)
            {
                lblMessage.Text = "1 to 12";
                txtMonth.Focus();
            }
            else
                lblMessage.Text = "";
        }

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMonth.Text != null && txtMonth.Text != "")
                {
                    month = Convert.ToInt32(txtMonth.Text.ToString());
                    if (month <= 3)
                        syear = General.ShopDetail.Shopey.Substring(0, 4);
                    else
                        syear = General.ShopDetail.Shopsy.Substring(0, 4);
                    txtYear.Text = syear;
                    rbtFirst.Focus();
                }
                else
                    txtMonth.Focus();
            }

        }

        private void rbtFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtFirst.Checked)
            {
                if (month < 10)
                    smonth = "0" + month.ToString("0");
                else
                    smonth = month.ToString("00");
                _Statements.FromDate = syear + smonth + "01";
                _Statements.ToDate = syear + smonth + "15";
            }
            else
            {
                if (month < 10)
                    smonth = "0" + month.ToString("0");
                else
                    smonth = month.ToString("00");
                _Statements.FromDate = syear + smonth + "16";
                string dfmdate = syear + smonth + "01";
                DateTime fmdate = General.ConvertStringToDateyyyyMMdd(dfmdate);
                DateTime ttDate = fmdate.AddMonths(1);
                TimeSpan tt = new TimeSpan(1, 0, 0, 0);
                ttDate = ttDate.Subtract(tt);
                _Statements.ToDate = ttDate.ToString("yyyyMMdd");
            }
            txtFromDate.Text = General.GetDateInShortDateFormat(_Statements.FromDate);
            txtToDate.Text = General.GetDateInShortDateFormat(_Statements.ToDate);
        }
        #endregion Events

        private void dgvStatements_OnShowViewForm(DataGridViewRow selectedRow)
        {
            dgvStatements.SendToBack();
            dgvbills.BringToFront();
            dgvbills.Visible = true;
        }

    }
}
