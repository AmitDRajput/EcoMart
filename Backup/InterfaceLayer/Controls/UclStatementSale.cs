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
using PrintDataGrid;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStatementSale : BaseControl
    {
        # region Declaration
        DataTable _StatementData;
        DataTable _BillData;
        int month = 0;
        string syear = "";
        string smonth = "";
        Statements _Statements;
        #endregion Declaration

        #region Constructor
        public UclStatementSale()
        {
            try
            {
                InitializeComponent();
                _BillData = new DataTable();
                _StatementData = new DataTable();
                _Statements = new Statements();
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
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "STATEMENT SALE -> NEW";
            string StmtDate = "";
            StmtDate = DateTime.Now.Date.ToString("yyyyMMdd");
            StmtDate = General.GetDateInShortDateFormat(StmtDate);
            txtCreatedDate.Text = StmtDate;
            _Statements.VoucherType = FixAccounts.VoucherTypeForStatementSale;
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
            headerLabel1.Text = "STATEMENT SALE -> DELETE";
            return retValue;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            Statements stmt = new Statements();
            GetFirstAndLastNumbers();
            retValue = stmt.CheckCanbeDeletedSaleStatement(_Statements.LastStatementNumber);
            if (retValue)
            {
                retValue = stmt.DeleteStatementsSale(_Statements.StatementNumber, _Statements.LastStatementNumber, FixAccounts.VoucherTypeForStatementSale, General.ShopDetail.ShopVoucherSeries);
                retValue = stmt.RemoveStatementNumbersFrommasterSale(_Statements.StatementNumber, _Statements.LastStatementNumber, General.ShopDetail.ShopVoucherSeries);
                _Statements.LastStatementNumber = _Statements.StatementNumber - 1;
                retValue = stmt.UpdateLastStatementNumberInTblVoucherNumbersSale();
                lblMessage.Text = "Deleted Successfully...";
                ConstructStatmentColumns(); 
                dgvStatements.Columns["Col_ID"].Visible = false;
            }
            else
            {
                lblMessage.Text = "Can not Delete...";
            }
            return true;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();           
            return retValue;
        }
        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            ConstructBillColumns();
            ConstructStatmentColumns();
            pnlMultiSelection1.Visible = true;
            headerLabel1.Text = "STATEMENT SALE -> VIEW";
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
                        if (dgvbills.Rows[index].Cells["Col_Address2"].Value != null && dgvbills.Rows[index].Cells["Col_Address2"].Value.ToString() != "")
                            preadd2 = dgvbills.Rows[index].Cells["Col_Address2"].Value.ToString();
                        if (dgvbills.Rows[index].Cells["Col_Telephone"].Value != null && dgvbills.Rows[index].Cells["Col_Telephone"].Value.ToString() != "")
                            pretel = dgvbills.Rows[index].Cells["Col_Telephone"].Value.ToString();
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
                                    PrintBill.Print_Bill();
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
                        PrintRowPixel = 418;
                        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                        PrintBill.Rows.Add(row);
                        PrintRowPixel += 17;
                        row = new PrintRow(totalAmount.ToString("#0.00"), PrintRowPixel, 150, fnt);
                        PrintBill.Rows.Add(row);                       
                        PrintRowPixel += 17;
                        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                        PrintBill.Rows.Add(row);

                        PrintBill.Print_Bill();                        
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

                PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(statementNumber.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(mname, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(madd1, 107, 30, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 34;

                row = new PrintRow(madd2, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 34;

                row = new PrintRow("Tel:" + mtel, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_Statements.StatementDate, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);


                PrintRowPixel += 17;
                row = new PrintRow("Vou No  Date       Amount        Vou No  Date       Amount", PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;
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
            //    System.Text.StringBuilder _errorMessage;
            //  _Statements.Validate();

            //  if (_Statements.IsValid)
            //   {
            try
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
                        retValue = _Statements.AddDetailsSale();

                }
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
            try
            {
                foreach (DataGridViewRow dgvr in dgvbills.Rows)
                {
                    _Statements.StatementNumber = 0;
                    _Statements.Id = "";
                    int stmtno = 0;
                    if (dgvr.Cells["Col_ID"].Value != null && dgvr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _Statements.Id = dgvr.Cells["Col_ID"].Value.ToString();
                    }
                    if (dgvr.Cells["Col_StatementNumber"].Value != null)
                        int.TryParse(dgvr.Cells["Col_StatementNumber"].Value.ToString(), out stmtno);
                    _Statements.StatementNumber = stmtno;
                    if (_Statements.Id != "")
                        retValue = _Statements.AddStatementNumberInSaleVoucher();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            retValue = _Statements.UpdateLastStatementNumberInTblVoucherNumbersSale();

       
            if (retValue)
            {
                PSDialogResult result;
                if (printData)
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", "" ,General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                    Print();
                }
                else
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", "", General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                    if (result == PSDialogResult.Print)
                        Print();
                }
            }
            ClearData();
            ConstructBillColumns();
            ConstructStatmentColumns();
            dgvStatements.Columns["Col_ID"].Visible = false;
            //   this.Visible = false;
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
                dgvStatements.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StatementNumber";
                column.HeaderText = "Stmt Number";
                column.DataPropertyName = "VoucherNumber";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.Visible = false;
                column.DataPropertyName = "AccountID";
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.HeaderText = "Party";
                column.DataPropertyName = "AccName";
                column.Width = 250;
                dgvStatements.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address1";
                column.HeaderText = "Address";
                column.DataPropertyName = "AccAddress1";
                column.Width = 250;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.DataPropertyName = "AmountNet";
                column.Width = 150;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NoofBills";
                column.HeaderText = "Bills";
                column.DataPropertyName = "NumberOfBills";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat5Percent";
                column.DataPropertyName = "VAT5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat12point5Percent";
                column.DataPropertyName = "VAT12point5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvStatements.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StatementDate";
                column.DataPropertyName = "VoucherDate";
                column.Visible = false;
                dgvStatements.Columns.Add(column);

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
                dgvbills.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StatementNumber";
                column.HeaderText = "Stmt Number";
                column.DataPropertyName = "statementNumber";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Vou Number";
                column.DataPropertyName = "VoucherNumber";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Vou Date";
                column.DataPropertyName = "VoucherDate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.Visible = false;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 200;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address1";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 150;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address2";
                column.DataPropertyName = "AccAddress2";
                column.HeaderText = "Address";
                column.Width = 150;
                column.Visible = false;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "AccTelephone";
                column.HeaderText = "Address";
                column.Width = 150;
                column.Visible = false;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VouType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Visible = false;
                column.Width = 150;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat5Percent";
                column.DataPropertyName = "VAT5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Vat12point5Percent";
                column.DataPropertyName = "VAT12point5Percent";
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvbills.Columns.Add(column);

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
            try
            {
                if (month > 0)
                {

                    Statements stmt = new Statements();
                    if (_Mode == OperationMode.Add)
                        _BillData = stmt.GetOverviewDataSale(_Statements.FromDate, _Statements.ToDate);
                    else
                    {
                        _BillData = stmt.GetOverviewDataSaleForView(_Statements.FromDate, _Statements.ToDate);
                        _StatementData = stmt.GetOverviewDataSaleStatementForView(_Statements.FromDate, _Statements.ToDate);
                    }
                    ConstructStatmentColumns();
                    dgvStatements.Columns["Col_ID"].Visible = false;

                    ConstructBillColumns();

                    pnlMultiSelection1.Visible = false;
                    dgvbills.DataSource = _BillData;

                    if (_Mode == OperationMode.Add)
                    {
                        dgvStatements.Visible = false;
                        dgvbills.Visible = true;
                        int stmtno = stmt.GetSaleStatementToView(General.ShopDetail.ShopVoucherSeries);
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
                            dgvStatements.Columns[0].ReadOnly = true;
                            dgvStatements.Columns[1].ReadOnly = true;
                            dgvStatements.Columns[2].ReadOnly = true;
                            dgvStatements.Columns[3].ReadOnly = true;
                            dgvStatements.Columns[4].ReadOnly = true;
                            dgvStatements.Columns[5].ReadOnly = true;
                            dgvStatements.Columns[6].ReadOnly = true;
                            dgvStatements.Columns[7].ReadOnly = true;
                            dgvStatements.Columns[8].ReadOnly = true;
                        }
                        else
                        {
                            dgvbills.Visible = false;
                            lblMessage.Text = "No Records for the given period..";
                        }
                    }
                    else
                    {
                        dgvStatements.Visible = true;
                        dgvbills.Visible = false;
                        dgvStatements.DataSource = _StatementData;
                        if (_StatementData != null && _StatementData.Rows.Count > 0)
                            _Statements.StatementDate = dgvStatements.Rows[0].Cells["Col_StatementDate"].Value.ToString();
                    }
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
                btnGo.Enabled = false;
            }
            else
            {
                lblMessage.Text = "";
                btnGo.Enabled = true;
            }
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
            rbtFirstChecked();
        }

        private void rbtFirstChecked()
        {
            if (month > 0)
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
        }
        #endregion Events

        private void rbtFirst_KeyDown(object sender, KeyEventArgs e)
        {
            rbtFirstChecked();
        }



    }
}
