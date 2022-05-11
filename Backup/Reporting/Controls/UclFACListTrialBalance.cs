using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.Printing;
using PharmaSYSRetailPlus.Reporting;
using PharmaSYSRetailPlus.Reporting.Base;
using PrintDataGrid;


namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclFACListTrialBalance : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _MasterOP;
        private DataTable _TransactionSource;
        private DataTable _TransactionOP;
        private DataTable _BindingSourceLevel2;
        private DataTable _BindingSourceLevel3;
        private DataTable _BindingSourceLevel4;
        private DataTable _BindingSourceGroupwise;
        private Account _Account;
        private FinalAccounts _FinalAccounts;
      //  private MPReports _MPReports;
        private string _MFromDate;
        private string _MToDate;
        //private double _MOpeningDebit = 0;
        //private double _MOpeningCredit = 0;
        private double _MTotOPDebit = 0;
        private double _MTotOPCredit = 0;
        private double _MTotCLDebit = 0;
        private double _MTotCLCredit = 0;
        private double _MTotTRDebit = 0;
        private double _MTotTRCredit = 0;
        private int _MLevel = 0;
        private string PrintAddress = "N";

        private  MReportGridView dgvReport = new MReportGridView();
        #endregion

        # region Constructor
        public UclFACListTrialBalance()
        {
            InitializeComponent();
        }
        #endregion

        #region IOverview Members
        public override void ShowOverview()
        {
            _BindingSource = new DataTable();
            _BindingSourceGroupwise = new DataTable();
            _TransactionSource = new DataTable();
            _TransactionOP = new DataTable();
            _MasterOP = new DataTable();
           // _MPReports = new MPReports();
            _FinalAccounts = new FinalAccounts();
            _Account = new Account();
            headerLabel1.Text = "FINAL ACCOUNTS - TRIAL BALANCE";
            ClearControls();
            AddToolTip();
            HidepnlGO();
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
            if (keyPressed == Keys.F11)
            {
                fromDate1.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.NumPad2 && modifier == Keys.Alt)
            {
                ButtonSecondLevelClicked();
            }
            if (keyPressed == Keys.G && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {
                    btnOKMultiSelectionClick();
                    retValue = true;
                }
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        public override void SetFocus()
        {
            base.SetFocus();
            fromDate1.Focus();
        }

        #endregion

        # region IReport Members



        public override void Print()
        {
            try
            {
                PrintData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintReportHead = "Trial Balance  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                if (dgvReportListLevel1.Visible == true)
                {
                    dgvReport = dgvReportListLevel1;
                    PrintAddress = "N";
                }
                else if (dgvReportListLevel2.Visible == true)
                {
                    dgvReport = dgvReportListLevel2;
                    PrintAddress = "N";
                }
                else if (dgvReportListLevel3.Visible == true)
                {
                    dgvReport = dgvReportListLevel3;
                    PrintAddress = "N";
                }
                else if (dgvReportListLevel4.Visible == true)
                {
                    dgvReport = dgvReportListLevel4;
                    PrintAddress = "N";
                }
                else if (dgvReportList.Visible == true)
                {
                    dgvReport = dgvReportList;
                    PrintAddress = "Y";
                }               

                int totalrows = dgvReport.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                double mamt = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReport.Rows)
                {

                    if (dr.Cells["Col_AccName"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 34;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        mamt = 0;
                        if (dr.Cells["Col_AccName"].Value != null && (dr.Cells["Col_AccName"].Value.ToString() == "Total"))
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }

                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0, 25), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (PrintAddress == "Y")
                        {
                            if (dr.Cells["Col_Address"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(25).Substring(0, 7), PrintRowPixel, 180, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                        }

                        if (dr.Cells["Col_OpeningDB"].Value != null && Convert.ToDouble(dr.Cells["Col_OpeningDB"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_OpeningDB"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(250.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_OpeningCR"].Value != null && Convert.ToDouble(dr.Cells["Col_OpeningCR"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_OpeningCR"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(340.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_TransactionDB"].Value != null && Convert.ToDouble(dr.Cells["Col_TransactionDB"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_TransactionDB"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(430.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_TransactionCR"].Value != null && Convert.ToDouble(dr.Cells["Col_TransactionCR"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_TransactionCR"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(520.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ClosingDB"].Value != null && Convert.ToDouble(dr.Cells["Col_ClosingDB"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_ClosingDB"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(610.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ClosingCR"].Value != null && Convert.ToDouble(dr.Cells["Col_ClosingCR"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ClosingCR"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(700.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }



                }
                PrintRowPixel += 17;
                PrintRowCount += 1;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;


                row = new PrintRow("Account", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                if (PrintAddress == "Y")
                {
                    row = new PrintRow("Address", PrintRowPixel, 180, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow("OP Debit ", PrintRowPixel, 270, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("OP Credit", PrintRowPixel, 360, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("TR Debit ", PrintRowPixel, 450, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("TR Credit", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CL Debit ", PrintRowPixel, 630, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CL Credit ", PrintRowPixel, 720, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }

        #endregion IReportMember


        # region Other Private methods


        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
                dgvReportList.Visible = false;
                dgvReportListLevel1.Visible = false;
                dgvReportListLevel1.Dock = DockStyle.Fill;
                dgvReportListLevel2.Visible = false;
                dgvReportListLevel3.Visible = false;
                dgvDifference.Visible = false;
                btnRemoveZero.Text = "Remove Zero";
                btnFirstLevel.BackColor = Color.Bisque;
                btnSecondLevel.BackColor = Color.White;
                btnThirdLevel.BackColor = Color.White;
                btnFourthLevel.BackColor = Color.White;
                btnAlphabetical.BackColor = Color.White;
                btnDifference.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);

        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();


            ConstructReportColumnsLevel1();
            dgvReportListLevel1.Columns["Col_ID"].Visible = false;
            dgvReportListLevel1.InitializeColumnContextMenu();

            ConstructReportColumnsLevel2();
            dgvReportListLevel2.Columns["Col_ID"].Visible = false;
            dgvReportListLevel2.InitializeColumnContextMenu();

            ConstructReportColumnsLevel3();
            dgvReportListLevel3.Columns["Col_ID"].Visible = false;
            dgvReportListLevel3.InitializeColumnContextMenu();

            ConstructReportColumnsLevel4();
            dgvReportListLevel4.Columns["Col_ID"].Visible = false;
            dgvReportListLevel4.InitializeColumnContextMenu();

            ConstructReportColumnsDifference();
            dgvDifference.Columns["Col_ID"].Visible = false;
            dgvDifference.InitializeColumnContextMenu();

        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            fromDate1.Focus();
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel4.Visible = false;
            dgvReportList.Visible = false;
            btnFirstLevel.Visible = false;
            btnSecondLevel.Visible = false;
            btnThirdLevel.Visible = false;
            btnFourthLevel.Visible = false;
            btnAlphabetical.Visible = false;
            btnRemoveZero.Visible = false;

        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            btnFirstLevel.Visible = true;
            btnSecondLevel.Visible = true;
            btnThirdLevel.Visible = true;
            btnFourthLevel.Visible = true;
            btnAlphabetical.Visible = true;
        }


        private void ConstructReportColumns()
        {
            dgvReportList.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 130;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Address";
            column.HeaderText = "Address";
            column.Width = 100;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningDB";
            column.HeaderText = "OpDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningCR";
            column.HeaderText = "OpCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "TrDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "TrCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingDB";
            column.HeaderText = "ClDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingCR";
            column.HeaderText = "ClCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_GroupID";
            column.HeaderText = "Group";
            column.Visible = false;
            column.Width = 130;
            dgvReportList.Columns.Add(column);


        }
        private void ConstructReportColumnsLevel1()
        {
            dgvReportListLevel1.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountID";
            column.Visible = false;          
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 230;
            dgvReportListLevel1.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningDB";
            column.HeaderText = "OpDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningCR";
            column.HeaderText = "OpCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "TrDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "TrCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingDB";
            column.HeaderText = "ClDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingCR";
            column.HeaderText = "ClCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_GroupID";
            column.HeaderText = "Group";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnderGroupID";
            column.HeaderText = "Parent";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel1.Columns.Add(column);

        }


        private void ConstructReportColumnsLevel2()
        {
            dgvReportListLevel2.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            //    column.Visible = false;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountID";
            column.Visible = false;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 230;
            dgvReportListLevel2.Columns.Add(column);



            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningDB";
            column.HeaderText = "OpDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningCR";
            column.HeaderText = "OpCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "TrDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "TrCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingDB";
            column.HeaderText = "ClDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingCR";
            column.HeaderText = "ClCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_GroupID";
            column.HeaderText = "Group";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel2.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnderGroupID";
            column.HeaderText = "Parent";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel2.Columns.Add(column);
        }

        private void ConstructReportColumnsLevel3()
        {
            dgvReportListLevel3.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountID";
            column.Visible = false;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 230;
            dgvReportListLevel3.Columns.Add(column);



            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningDB";
            column.HeaderText = "OpDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningCR";
            column.HeaderText = "OpCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "TrDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "TrCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingDB";
            column.HeaderText = "ClDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingCR";
            column.HeaderText = "ClCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_GroupID";
            column.HeaderText = "Group";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel3.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnderGroupID";
            column.HeaderText = "Parent";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel3.Columns.Add(column);
        }


        private void ConstructReportColumnsLevel4()
        {
            dgvReportListLevel4.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountID";
            column.Visible = false;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 230;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningDB";
            column.HeaderText = "OpDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OpeningCR";
            column.HeaderText = "OpCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "TrDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "TrCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingDB";
            column.HeaderText = "ClDB";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingCR";
            column.HeaderText = "ClCR";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_GroupID";
            column.HeaderText = "Group";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel4.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnderGroupID";
            column.HeaderText = "Parent";
            column.Visible = false;
            column.Width = 30;
            dgvReportListLevel4.Columns.Add(column);
        }


        private void ConstructReportColumnsDifference()
        {
            dgvDifference.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvDifference.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.HeaderText = "Party";
            column.Width = 230;
            column.Visible = false;
            dgvDifference.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VoucherType";
            column.HeaderText = "VoucherType";
            column.Width = 90;
            dgvDifference.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VoucherNumber";
            column.HeaderText = "VoucherNumber";
            column.Width = 90;
            dgvDifference.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VoucherDate";
            column.HeaderText = "VoucherDate";
            column.Width = 120;
            dgvDifference.Columns.Add(column);          

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionDB";
            column.HeaderText = "Debit";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvDifference.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TransactionCR";
            column.HeaderText = "Credit";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 120;
            dgvDifference.Columns.Add(column);         

        }


        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                FormatReportGrid();
                //   FillLevel4();
                FillLevels();
                ButtonFirstLevelClicked();


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       
        private void FillLevels()
        {
            int mundergroupID = 0;         
            DataGridViewRow currentdr;
            int _RowIndex = 0;
            int GrpID = 0;
            int GrpID2 = 0;
            int GrpID3 = 0;
            int GrpID4 = 0;

            if (dgvReportListLevel1.Rows.Count > 0)
                dgvReportListLevel1.Rows.Clear();
            if (dgvReportListLevel2.Rows.Count > 0)
                dgvReportListLevel2.Rows.Clear();
            if (dgvReportListLevel3.Rows.Count > 0)
                dgvReportListLevel3.Rows.Clear();
            if (dgvReportListLevel4.Rows.Count > 0)
                dgvReportListLevel4.Rows.Clear();
            _MTotCLCredit = 0;
            _MTotCLDebit = 0;
            _MTotOPCredit = 0;
            _MTotOPDebit = 0;
            _MTotTRCredit = 0;
            _MTotTRDebit = 0;

            double amt = 0;

            try
            {
                foreach (DataRow dr in _BindingSourceGroupwise.Rows)
                {
                    if (dr["UnderGroupId"] != DBNull.Value && dr["UnderGroupId"].ToString() != string.Empty)
                        mundergroupID = Convert.ToInt32(dr["UnderGroupId"].ToString());
                    if (mundergroupID >= 1 && mundergroupID <= 20)
                    {
                        GrpID = Convert.ToInt32(dr["GroupID"].ToString().Trim());
                        _RowIndex = dgvReportListLevel1.Rows.Add();
                        currentdr = dgvReportListLevel1.Rows[_RowIndex];
                        currentdr.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        currentdr.Cells["Col_ID"].Value = dr["GroupID"].ToString();
                        currentdr.Cells["Col_AccName"].Value = dr["GroupName"].ToString();
                        currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                        currentdr.Cells["Col_GroupID"].Value = dr["GroupID"].ToString();
                        amt = Convert.ToDouble(dr["OpeningDebit"].ToString());
                        currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["OpeningCredit"].ToString());
                        currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionDebit"].ToString());
                        currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionCredit"].ToString());
                        currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                        currentdr.Cells["Col_ClosingDB"].Value = dr["ClosingDebit"].ToString();
                        currentdr.Cells["Col_ClosingCR"].Value = dr["ClosingCredit"].ToString();
                        _MTotCLCredit += Convert.ToDouble(dr["ClosingCredit"].ToString());
                        _MTotCLDebit += Convert.ToDouble(dr["ClosingDebit"].ToString());
                        _MTotOPCredit += Convert.ToDouble(dr["OpeningCredit"].ToString());
                        _MTotOPDebit += Convert.ToDouble(dr["OpeningDebit"].ToString());
                        _MTotTRCredit += Convert.ToDouble(dr["TransactionCredit"].ToString());
                        _MTotTRDebit += Convert.ToDouble(dr["TransactionDebit"].ToString());

                    }
                }
                _RowIndex = dgvReportListLevel1.Rows.Add();
                currentdr = dgvReportListLevel1.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_AccName"].Value = "Total";
                currentdr.Cells["Col_OpeningDB"].Value = _MTotOPDebit.ToString("#0.00");
                currentdr.Cells["Col_OpeningCR"].Value = _MTotOPCredit.ToString("#0.00");
                currentdr.Cells["Col_TransactionDB"].Value = _MTotTRDebit.ToString("#0.00");
                currentdr.Cells["Col_TransactionCR"].Value = _MTotTRCredit.ToString("#0.00");
                currentdr.Cells["Col_ClosingDB"].Value = _MTotCLDebit.ToString("#0.00");
                currentdr.Cells["Col_ClosingCR"].Value = _MTotCLCredit.ToString("#0.00");

                if (_MTotCLDebit != _MTotCLCredit)
                {
                    GetDifferenceRows();
                    btnDifference.Visible = true;
                }


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            /////////   Level 2

            _MTotCLCredit = 0;
            _MTotCLDebit = 0;
            _MTotOPCredit = 0;
            _MTotOPDebit = 0;
            _MTotTRCredit = 0;
            _MTotTRDebit = 0;

            amt = 0;

            try
            {
                foreach (DataRow dr in _BindingSourceGroupwise.Rows)
                {
                    if (dr["UnderGroupId"] != DBNull.Value && dr["UnderGroupId"].ToString() != string.Empty)
                        mundergroupID = Convert.ToInt32(dr["UnderGroupId"].ToString());
                    if (mundergroupID >= 1 && mundergroupID <= 20)
                    {
                        GrpID = Convert.ToInt32(dr["GroupID"].ToString().Trim());
                        _RowIndex = dgvReportListLevel2.Rows.Add();
                        currentdr = dgvReportListLevel2.Rows[_RowIndex];
                        currentdr.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        currentdr.Cells["Col_ID"].Value = dr["GroupID"].ToString();                       
                        currentdr.Cells["Col_AccName"].Value = dr["GroupName"].ToString();
                        currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                        currentdr.Cells["Col_GroupID"].Value = dr["GroupID"].ToString();
                        amt = Convert.ToDouble(dr["OpeningDebit"].ToString());
                        currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["OpeningCredit"].ToString());
                        currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionDebit"].ToString());
                        currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionCredit"].ToString());
                        currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                        currentdr.Cells["Col_ClosingDB"].Value = dr["ClosingDebit"].ToString();
                        currentdr.Cells["Col_ClosingCR"].Value = dr["ClosingCredit"].ToString();
                        _MTotCLCredit += Convert.ToDouble(dr["ClosingCredit"].ToString());
                        _MTotCLDebit += Convert.ToDouble(dr["ClosingDebit"].ToString());
                        _MTotOPCredit += Convert.ToDouble(dr["OpeningCredit"].ToString());
                        _MTotOPDebit += Convert.ToDouble(dr["OpeningDebit"].ToString());
                        _MTotTRCredit += Convert.ToDouble(dr["TransactionCredit"].ToString());
                        _MTotTRDebit += Convert.ToDouble(dr["TransactionDebit"].ToString());

                        _BindingSourceLevel2 = new DataTable();
                        if (GrpID != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                            _BindingSourceLevel2 = _FinalAccounts.GetGroupsUnderLevel2(GrpID);
                        if (_BindingSourceLevel2 != null && _BindingSourceLevel2.Rows.Count > 0)
                        {

                            foreach (DataRow dr2 in _BindingSourceLevel2.Rows)
                            {
                                if (dr2["GroupID"] != DBNull.Value && dr2["GroupID"].ToString() != string.Empty)
                                {
                                    GrpID2 = Convert.ToInt32(dr2["GroupID"].ToString().Trim());
                                    _RowIndex = dgvReportListLevel2.Rows.Add();
                                    currentdr = dgvReportListLevel2.Rows[_RowIndex];
                                    if (dr2["UnderGroupID"] != DBNull.Value && dr2["UnderGroupID"].ToString() != string.Empty)
                                        currentdr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                                    else
                                        currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                    currentdr.Cells["Col_ID"].Value = dr2["GroupID"].ToString();
                                    if (dr2["AccountID"] != DBNull.Value)
                                        currentdr.Cells["Col_AccountID"].Value = dr2["AccountID"].ToString();
                                    currentdr.Cells["Col_AccName"].Value = "   " + dr2["GroupName"].ToString();
                                    //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                    currentdr.Cells["Col_GroupID"].Value = dr2["GroupID"].ToString();
                                    amt = Convert.ToDouble(dr2["OpeningDebit"].ToString());
                                    currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["OpeningCredit"].ToString());
                                    currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionDebit"].ToString());
                                    currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionCredit"].ToString());
                                    currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                    currentdr.Cells["Col_ClosingDB"].Value = dr2["ClosingDebit"].ToString();
                                    currentdr.Cells["Col_ClosingCR"].Value = dr2["ClosingCredit"].ToString();
                                }


                            }
                        }

                    }
                }
                _RowIndex = dgvReportListLevel2.Rows.Add();
                currentdr = dgvReportListLevel2.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_AccName"].Value = "Total";
                currentdr.Cells["Col_OpeningDB"].Value = _MTotOPDebit.ToString("#0.00");
                currentdr.Cells["Col_OpeningCR"].Value = _MTotOPCredit.ToString("#0.00");
                currentdr.Cells["Col_TransactionDB"].Value = _MTotTRDebit.ToString("#0.00");
                currentdr.Cells["Col_TransactionCR"].Value = _MTotTRCredit.ToString("#0.00");
                currentdr.Cells["Col_ClosingDB"].Value = _MTotCLDebit.ToString("#0.00");
                currentdr.Cells["Col_ClosingCR"].Value = _MTotCLCredit.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }



            /////////////////////////   Level 3

            _MTotCLCredit = 0;
            _MTotCLDebit = 0;
            _MTotOPCredit = 0;
            _MTotOPDebit = 0;
            _MTotTRCredit = 0;
            _MTotTRDebit = 0;

            amt = 0;

            try
            {
                foreach (DataRow dr in _BindingSourceGroupwise.Rows)
                {
                    if (dr["UnderGroupId"] != DBNull.Value && dr["UnderGroupId"].ToString() != string.Empty)
                        mundergroupID = Convert.ToInt32(dr["UnderGroupId"].ToString());
                    if (mundergroupID >= 1 && mundergroupID <= 20)
                    {
                        amt = 0;
                        GrpID = Convert.ToInt32(dr["GroupID"].ToString().Trim());
                        _RowIndex = dgvReportListLevel3.Rows.Add();
                        currentdr = dgvReportListLevel3.Rows[_RowIndex];
                        currentdr.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        currentdr.Cells["Col_ID"].Value = dr["GroupID"].ToString();
                        currentdr.Cells["Col_AccName"].Value = dr["GroupName"].ToString();
                        currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                        currentdr.Cells["Col_GroupID"].Value = dr["GroupID"].ToString();
                        amt = Convert.ToDouble(dr["OpeningDebit"].ToString());
                        currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["OpeningCredit"].ToString());
                        currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionDebit"].ToString());
                        currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionCredit"].ToString());
                        currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                        currentdr.Cells["Col_ClosingDB"].Value = dr["ClosingDebit"].ToString();
                        currentdr.Cells["Col_ClosingCR"].Value = dr["ClosingCredit"].ToString();
                        _MTotCLCredit += Convert.ToDouble(dr["ClosingCredit"].ToString());
                        _MTotCLDebit += Convert.ToDouble(dr["ClosingDebit"].ToString());
                        _MTotOPCredit += Convert.ToDouble(dr["OpeningCredit"].ToString());
                        _MTotOPDebit += Convert.ToDouble(dr["OpeningDebit"].ToString());
                        _MTotTRCredit += Convert.ToDouble(dr["TransactionCredit"].ToString());
                        _MTotTRDebit += Convert.ToDouble(dr["TransactionDebit"].ToString());

                        _BindingSourceLevel2 = new DataTable();
                        if (GrpID != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                            _BindingSourceLevel2 = _FinalAccounts.GetGroupsUnderLevel2(GrpID);

                        if (_BindingSourceLevel2 != null && _BindingSourceLevel2.Rows.Count > 0)
                        {

                            foreach (DataRow dr2 in _BindingSourceLevel2.Rows)
                            {
                                if (dr2["GroupID"] != DBNull.Value && dr2["GroupID"].ToString() != string.Empty)
                                {
                                    amt = 0;
                                    GrpID2 = 0;
                                    if (dr2["UnderGroupID"] != DBNull.Value && dr2["UnderGroupID"].ToString() != string.Empty)
                                        GrpID2 = Convert.ToInt32(dr2["GroupID"].ToString().Trim());
                                    _RowIndex = dgvReportListLevel3.Rows.Add();
                                    currentdr = dgvReportListLevel3.Rows[_RowIndex];
                                    if (dr2["UnderGroupID"] != DBNull.Value && dr2["UnderGroupID"].ToString() != string.Empty)
                                        currentdr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                                    else
                                        currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                    currentdr.Cells["Col_ID"].Value = dr2["GroupID"].ToString();
                                    if (dr2["AccountID"] != DBNull.Value)
                                        currentdr.Cells["Col_AccountID"].Value = dr2["AccountID"].ToString();
                                    currentdr.Cells["Col_AccName"].Value = "   " + dr2["GroupName"].ToString();
                                    //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                    currentdr.Cells["Col_GroupID"].Value = dr2["GroupID"].ToString();
                                    amt = Convert.ToDouble(dr2["OpeningDebit"].ToString());
                                    currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["OpeningCredit"].ToString());
                                    currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionDebit"].ToString());
                                    currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionCredit"].ToString());
                                    currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                    currentdr.Cells["Col_ClosingDB"].Value = dr2["ClosingDebit"].ToString();
                                    currentdr.Cells["Col_ClosingCR"].Value = dr2["ClosingCredit"].ToString();


                                    _BindingSourceLevel3 = new DataTable();
                                    if (GrpID2 != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID2 != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                                        _BindingSourceLevel3 = _FinalAccounts.GetGroupsUnderLevel2(GrpID2);


                                    if (_BindingSourceLevel3 != null && _BindingSourceLevel3.Rows.Count > 0)
                                    {

                                        foreach (DataRow dr3 in _BindingSourceLevel3.Rows)
                                        {
                                            amt = 0;
                                            if (dr3["GroupID"] != DBNull.Value && dr3["GroupID"].ToString() != string.Empty)
                                            {
                                                GrpID3 = Convert.ToInt32(dr3["GroupID"].ToString().Trim());
                                                _RowIndex = dgvReportListLevel3.Rows.Add();
                                                currentdr = dgvReportListLevel3.Rows[_RowIndex];
                                                if (dr3["UnderGroupID"] != DBNull.Value && dr3["UnderGroupID"].ToString() != string.Empty)
                                                    currentdr.DefaultCellStyle.BackColor = Color.LightBlue;
                                                else
                                                    currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                                currentdr.Cells["Col_ID"].Value = dr3["GroupID"].ToString();
                                                if (dr3["AccountID"] != DBNull.Value)
                                                    currentdr.Cells["Col_AccountID"].Value = dr3["AccountID"].ToString();
                                                currentdr.Cells["Col_AccName"].Value = "      " + dr3["GroupName"].ToString();
                                                //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                                currentdr.Cells["Col_GroupID"].Value = dr3["GroupID"].ToString();
                                                amt = Convert.ToDouble(dr3["OpeningDebit"].ToString());
                                                currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["OpeningCredit"].ToString());
                                                currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["TransactionDebit"].ToString());
                                                currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["TransactionCredit"].ToString());
                                                currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                                currentdr.Cells["Col_ClosingDB"].Value = dr3["ClosingDebit"].ToString();
                                                currentdr.Cells["Col_ClosingCR"].Value = dr3["ClosingCredit"].ToString();
                                            }
                                        }
                                    }
                                }


                            }
                        }

                    }
                }
                _RowIndex = dgvReportListLevel3.Rows.Add();
                currentdr = dgvReportListLevel3.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_AccName"].Value = "Total";
                currentdr.Cells["Col_OpeningDB"].Value = _MTotOPDebit.ToString("#0.00");
                currentdr.Cells["Col_OpeningCR"].Value = _MTotOPCredit.ToString("#0.00");
                currentdr.Cells["Col_TransactionDB"].Value = _MTotTRDebit.ToString("#0.00");
                currentdr.Cells["Col_TransactionCR"].Value = _MTotTRCredit.ToString("#0.00");
                currentdr.Cells["Col_ClosingDB"].Value = _MTotCLDebit.ToString("#0.00");
                currentdr.Cells["Col_ClosingCR"].Value = _MTotCLCredit.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }





            /////////////////////////////////   Level 4

            _MTotCLCredit = 0;
            _MTotCLDebit = 0;
            _MTotOPCredit = 0;
            _MTotOPDebit = 0;
            _MTotTRCredit = 0;
            _MTotTRDebit = 0;

            amt = 0;

            try
            {
                foreach (DataRow dr in _BindingSourceGroupwise.Rows)
                {
                    if (dr["UnderGroupId"] != DBNull.Value && dr["UnderGroupId"].ToString() != string.Empty)
                        mundergroupID = Convert.ToInt32(dr["UnderGroupId"].ToString());
                    if (mundergroupID >= 1 && mundergroupID <= 20)
                    {
                        amt = 0;
                        GrpID = Convert.ToInt32(dr["GroupID"].ToString().Trim());
                        _RowIndex = dgvReportListLevel4.Rows.Add();
                        currentdr = dgvReportListLevel4.Rows[_RowIndex];
                        currentdr.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        currentdr.Cells["Col_ID"].Value = dr["GroupID"].ToString();
                        currentdr.Cells["Col_AccName"].Value = dr["GroupName"].ToString();
                        currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                        currentdr.Cells["Col_GroupID"].Value = dr["GroupID"].ToString();
                        amt = Convert.ToDouble(dr["OpeningDebit"].ToString());
                        currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["OpeningCredit"].ToString());
                        currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionDebit"].ToString());
                        currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                        amt = Convert.ToDouble(dr["TransactionCredit"].ToString());
                        currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                        currentdr.Cells["Col_ClosingDB"].Value = dr["ClosingDebit"].ToString();
                        currentdr.Cells["Col_ClosingCR"].Value = dr["ClosingCredit"].ToString();
                        _MTotCLCredit += Convert.ToDouble(dr["ClosingCredit"].ToString());
                        _MTotCLDebit += Convert.ToDouble(dr["ClosingDebit"].ToString());
                        _MTotOPCredit += Convert.ToDouble(dr["OpeningCredit"].ToString());
                        _MTotOPDebit += Convert.ToDouble(dr["OpeningDebit"].ToString());
                        _MTotTRCredit += Convert.ToDouble(dr["TransactionCredit"].ToString());
                        _MTotTRDebit += Convert.ToDouble(dr["TransactionDebit"].ToString());

                        _BindingSourceLevel2 = new DataTable();
                        if (GrpID != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                            _BindingSourceLevel2 = _FinalAccounts.GetGroupsUnderLevel2(GrpID);

                        if (_BindingSourceLevel2 != null && _BindingSourceLevel2.Rows.Count > 0)
                        {

                            foreach (DataRow dr2 in _BindingSourceLevel2.Rows)
                            {
                                if (dr2["GroupID"] != DBNull.Value && dr2["GroupID"].ToString() != string.Empty)
                                {
                                    amt = 0;
                                    GrpID2 = Convert.ToInt32(dr2["GroupID"].ToString().Trim());
                                    _RowIndex = dgvReportListLevel4.Rows.Add();
                                    currentdr = dgvReportListLevel4.Rows[_RowIndex];
                                    if (dr2["UnderGroupID"] != DBNull.Value && dr2["UnderGroupID"].ToString() != string.Empty)
                                        currentdr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                                    else
                                        currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                    currentdr.Cells["Col_ID"].Value = dr2["GroupID"].ToString();
                                    if (dr2["AccountID"] != DBNull.Value)
                                        currentdr.Cells["Col_AccountID"].Value = dr2["AccountID"].ToString();
                                    currentdr.Cells["Col_AccName"].Value = "   " + dr2["GroupName"].ToString();
                                    //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                    currentdr.Cells["Col_GroupID"].Value = dr2["GroupID"].ToString();
                                    amt = Convert.ToDouble(dr2["OpeningDebit"].ToString());
                                    currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["OpeningCredit"].ToString());
                                    currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionDebit"].ToString());
                                    currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                    amt = Convert.ToDouble(dr2["TransactionCredit"].ToString());
                                    currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                    currentdr.Cells["Col_ClosingDB"].Value = dr2["ClosingDebit"].ToString();
                                    currentdr.Cells["Col_ClosingCR"].Value = dr2["ClosingCredit"].ToString();


                                    _BindingSourceLevel3 = new DataTable();
                                    if (GrpID2 != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID2 != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                                        _BindingSourceLevel3 = _FinalAccounts.GetGroupsUnderLevel2(GrpID2);


                                    if (_BindingSourceLevel3 != null && _BindingSourceLevel3.Rows.Count > 0)
                                    {

                                        foreach (DataRow dr3 in _BindingSourceLevel3.Rows)
                                        {
                                            amt = 0;
                                            if (dr3["GroupID"] != DBNull.Value && dr3["GroupID"].ToString() != string.Empty)
                                            {
                                                GrpID3 = 0;
                                                if (dr3["UnderGroupID"] != DBNull.Value && dr3["UnderGroupID"].ToString() != string.Empty)

                                                    GrpID3 = Convert.ToInt32(dr3["GroupID"].ToString().Trim());
                                                _RowIndex = dgvReportListLevel4.Rows.Add();
                                                currentdr = dgvReportListLevel4.Rows[_RowIndex];
                                                if (dr3["UnderGroupID"] != DBNull.Value && dr3["UnderGroupID"].ToString() != string.Empty)
                                                    currentdr.DefaultCellStyle.BackColor = Color.LightBlue;
                                                else
                                                    currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                                currentdr.Cells["Col_ID"].Value = dr3["GroupID"].ToString();
                                                if (dr3["AccountID"] != DBNull.Value)
                                                    currentdr.Cells["Col_AccountID"].Value = dr3["AccountID"].ToString();
                                                currentdr.Cells["Col_AccName"].Value = "      " + dr3["GroupName"].ToString();
                                                //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                                currentdr.Cells["Col_GroupID"].Value = dr3["GroupID"].ToString();
                                                amt = Convert.ToDouble(dr3["OpeningDebit"].ToString());
                                                currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["OpeningCredit"].ToString());
                                                currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["TransactionDebit"].ToString());
                                                currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                                amt = Convert.ToDouble(dr3["TransactionCredit"].ToString());
                                                currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                                currentdr.Cells["Col_ClosingDB"].Value = dr3["ClosingDebit"].ToString();
                                                currentdr.Cells["Col_ClosingCR"].Value = dr3["ClosingCredit"].ToString();





                                                _BindingSourceLevel4 = new DataTable();
                                                if (GrpID3 != Convert.ToInt32(FixAccounts.GroupCodeForDebtor) && GrpID3 != Convert.ToInt32(FixAccounts.GroupCodeForCreditor))
                                                    _BindingSourceLevel4 = _FinalAccounts.GetGroupsUnderLevel2(GrpID3);


                                                if (_BindingSourceLevel4 != null && _BindingSourceLevel4.Rows.Count > 0)
                                                {

                                                    foreach (DataRow dr4 in _BindingSourceLevel4.Rows)
                                                    {
                                                        amt = 0;
                                                        if (dr4["GroupID"] != DBNull.Value && dr4["GroupID"].ToString() != string.Empty)
                                                        {
                                                            GrpID4 = Convert.ToInt32(dr4["GroupID"].ToString().Trim());
                                                            _RowIndex = dgvReportListLevel4.Rows.Add();
                                                            currentdr = dgvReportListLevel4.Rows[_RowIndex];
                                                            if (dr4["UnderGroupID"] != DBNull.Value && dr4["UnderGroupID"].ToString() != string.Empty)
                                                                currentdr.DefaultCellStyle.BackColor = Color.LightBlue;
                                                            else
                                                                currentdr.DefaultCellStyle.BackColor = Color.PaleGreen;
                                                            currentdr.Cells["Col_ID"].Value = dr4["GroupID"].ToString();
                                                            if (dr4["AccountID"] != DBNull.Value)
                                                                currentdr.Cells["Col_AccountID"].Value = dr4["AccountID"].ToString();
                                                            currentdr.Cells["Col_AccName"].Value = "         " + dr4["GroupName"].ToString();
                                                            //  currentdr.Cells["Col_UnderGroupID"].Value = dr["UnderGroupId"].ToString();
                                                            currentdr.Cells["Col_GroupID"].Value = dr4["GroupID"].ToString();
                                                            amt = Convert.ToDouble(dr4["OpeningDebit"].ToString());
                                                            currentdr.Cells["Col_OpeningDB"].Value = amt.ToString("#0.00");
                                                            amt = Convert.ToDouble(dr4["OpeningCredit"].ToString());
                                                            currentdr.Cells["Col_OpeningCR"].Value = amt.ToString("#0.00");
                                                            amt = Convert.ToDouble(dr4["TransactionDebit"].ToString());
                                                            currentdr.Cells["Col_TransactionDB"].Value = amt.ToString("#0.00");
                                                            amt = Convert.ToDouble(dr4["TransactionCredit"].ToString());
                                                            currentdr.Cells["Col_TransactionCR"].Value = amt.ToString("#0.00");
                                                            currentdr.Cells["Col_ClosingDB"].Value = dr4["ClosingDebit"].ToString();
                                                            currentdr.Cells["Col_ClosingCR"].Value = dr4["ClosingCredit"].ToString();
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }


                            }
                        }

                    }
                }
                _RowIndex = dgvReportListLevel4.Rows.Add();
                currentdr = dgvReportListLevel4.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_AccName"].Value = "Total";
                currentdr.Cells["Col_OpeningDB"].Value = _MTotOPDebit.ToString("#0.00");
                currentdr.Cells["Col_OpeningCR"].Value = _MTotOPCredit.ToString("#0.00");
                currentdr.Cells["Col_TransactionDB"].Value = _MTotTRDebit.ToString("#0.00");
                currentdr.Cells["Col_TransactionCR"].Value = _MTotTRCredit.ToString("#0.00");
                currentdr.Cells["Col_ClosingDB"].Value = _MTotCLDebit.ToString("#0.00");
                currentdr.Cells["Col_ClosingCR"].Value = _MTotCLCredit.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }

        private void FillReportData()
        {
            try
            {

                DataTable dtable = new DataTable();

                //dtable = _FinalAccounts.GetTrialBalanceOPDBCRFromMaster();
                //_MasterOP = dtable;
                _FinalAccounts.InitializeDBCRFieldsInMasterAccount();
                dtable = _FinalAccounts.GetTrialBalanceOPDBCRFromTransaction(_MFromDate);
                _TransactionOP = dtable;
                dtable = _FinalAccounts.GetTrialBalanceTRDBCRFromTransaction(_MFromDate, _MToDate);
                _TransactionSource = dtable;

                _FinalAccounts.UpdatebalancesInMasterAccount(_TransactionOP, _TransactionSource);

                _FinalAccounts.CalculateClosingBalanceInmasterAccount();

                _BindingSourceGroupwise = _FinalAccounts.GetGroupWiseTotals();
                //_BindingSource = dtable;
                _BindingSourceLevel2 = _BindingSourceGroupwise;
                _BindingSourceLevel3 = _BindingSourceGroupwise;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.DoubleColumnNames.Add("Col_TransactionDB");
            dgvReportList.DoubleColumnNames.Add("Col_TransactionCR");
            dgvReportList.DoubleColumnNames.Add("Col_ClosingDB");
            dgvReportList.DoubleColumnNames.Add("Col_ClosingCR");

            dgvReportListLevel1.DoubleColumnNames.Add("Col_Debit");
            dgvReportListLevel1.DoubleColumnNames.Add("Col_Credit");
            dgvReportListLevel1.DoubleColumnNames.Add("Col_TransactionDB");
            dgvReportListLevel1.DoubleColumnNames.Add("Col_TransactionCR");
            dgvReportListLevel1.DoubleColumnNames.Add("Col_ClosingDB");
            dgvReportListLevel1.DoubleColumnNames.Add("Col_ClosingCR");

            dgvReportListLevel2.DoubleColumnNames.Add("Col_Debit");
            dgvReportListLevel2.DoubleColumnNames.Add("Col_Credit");
            dgvReportListLevel2.DoubleColumnNames.Add("Col_TransactionDB");
            dgvReportListLevel2.DoubleColumnNames.Add("Col_TransactionCR");
            dgvReportListLevel2.DoubleColumnNames.Add("Col_ClosingDB");
            dgvReportListLevel2.DoubleColumnNames.Add("Col_ClosingCR");

            dgvReportListLevel3.DoubleColumnNames.Add("Col_Debit");
            dgvReportListLevel3.DoubleColumnNames.Add("Col_Credit");
            dgvReportListLevel3.DoubleColumnNames.Add("Col_TransactionDB");
            dgvReportListLevel3.DoubleColumnNames.Add("Col_TransactionCR");
            dgvReportListLevel3.DoubleColumnNames.Add("Col_ClosingDB");
            dgvReportListLevel3.DoubleColumnNames.Add("Col_ClosingCR");

            dgvReportListLevel4.DoubleColumnNames.Add("Col_Debit");
            dgvReportListLevel4.DoubleColumnNames.Add("Col_Credit");
            dgvReportListLevel4.DoubleColumnNames.Add("Col_TransactionDB");
            dgvReportListLevel4.DoubleColumnNames.Add("Col_TransactionCR");
            dgvReportListLevel4.DoubleColumnNames.Add("Col_ClosingDB");
            dgvReportListLevel4.DoubleColumnNames.Add("Col_ClosingCR");

            dgvDifference.DateColumnNames.Add("Col_VoucherDate");


        }

        private void BindReportGrid()
        {
            try
            {
                if (dgvReportList.Rows.Count > 0)
                    dgvReportList.Rows.Clear();

                int _RowIndex;
                string AccID = "";
                DataGridViewRow currentdr;

                double mopeningbalancedb = 0;
                double mopeningbalancecr = 0;

                _MTotCLCredit = 0;
                _MTotCLDebit = 0;
                _MTotOPCredit = 0;
                _MTotOPDebit = 0;
                _MTotTRCredit = 0;
                _MTotTRDebit = 0;

                DataTable dt = new DataTable();
                dt = _FinalAccounts.GetDetailsFromMasterAccount();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["AccountID"].ToString().Trim() != "")
                    {
                        AccID = dr["AccountID"].ToString().Trim();
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["AccountID"].ToString();
                        currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        currentdr.Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                        currentdr.Cells["Col_GroupID"].Value = dr["AccGroupID"].ToString();
                        mopeningbalancedb = Convert.ToDouble(dr["AccOpeningDebit"].ToString()) + Convert.ToDouble(dr["AccTransactionOpeningDb"].ToString());
                        mopeningbalancecr = Convert.ToDouble(dr["AccOpeningCredit"].ToString()) + Convert.ToDouble(dr["AccTransactionOpeningCr"].ToString());
                        currentdr.Cells["Col_OpeningDB"].Value = mopeningbalancedb.ToString("#0.00");
                        currentdr.Cells["Col_OpeningCR"].Value = mopeningbalancecr.ToString("#0.00");
                        currentdr.Cells["Col_TransactionDB"].Value = dr["AccTransactionDebit"].ToString();
                        currentdr.Cells["Col_TransactionCR"].Value = dr["AccTransactionCredit"].ToString();
                        currentdr.Cells["Col_ClosingDB"].Value = dr["AccClosingDebit"].ToString();
                        currentdr.Cells["Col_ClosingCR"].Value = dr["AccClosingCredit"].ToString();
                        _FinalAccounts.MPTrDebit = Convert.ToDouble(dr["AccTransactionDebit"].ToString());
                        _FinalAccounts.MPTrCredit = Convert.ToDouble(dr["AccTransactionCredit"].ToString());


                        _MTotCLCredit += Convert.ToDouble(dr["AccClosingCredit"].ToString());
                        _MTotCLDebit += Convert.ToDouble(dr["AccClosingDebit"].ToString());
                        _MTotOPCredit += mopeningbalancecr;
                        _MTotOPDebit += mopeningbalancedb;
                        _MTotTRCredit += Convert.ToDouble(dr["AccTransactionCredit"].ToString());
                        _MTotTRDebit += Convert.ToDouble(dr["AccTransactionDebit"].ToString());
                        if (btnRemoveZero.Visible == true && btnRemoveZero.Text == "Remove Zero")
                        {
                            if (mopeningbalancedb == 0 && mopeningbalancecr == 0 && _FinalAccounts.MPTrDebit == 0 && _FinalAccounts.MPTrCredit == 0)
                                dgvReportList.Rows.Remove(currentdr);
                        }



                    }

                }



                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportSubTotalColor;
                currentdr.Cells["Col_Address"].Value = "Total";
                currentdr.Cells["Col_OpeningDB"].Value = _MTotOPDebit.ToString("#0.00");
                currentdr.Cells["Col_OpeningCR"].Value = _MTotOPCredit.ToString("#0.00");
                currentdr.Cells["Col_TransactionDB"].Value = _MTotTRDebit.ToString("#0.00");
                currentdr.Cells["Col_TransactionCR"].Value = _MTotTRCredit.ToString("#0.00");
                currentdr.Cells["Col_ClosingDB"].Value = _MTotCLDebit.ToString("#0.00");
                currentdr.Cells["Col_ClosingCR"].Value = _MTotCLCredit.ToString("#0.00");

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    FillReportGrid();
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();
                }
                else
                    lblFooterMessage.Text = "Check Date";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        //private void GetActualOpeningBalance(string accID, string fromDate)
        //{
        //    double totdebit = 0;
        //    double totcredit = 0;
        //    foreach (DataRow dr in _BindingSource.Rows)
        //    {
        //        if (dr["AccountID"].ToString() == accID && Convert.ToInt32(dr["TransactionDate"].ToString()) < Convert.ToInt32(fromDate))
        //        {
        //            if (dr["Debit"] != null && dr["Debit"].ToString() != "")
        //                totdebit += Convert.ToDouble(dr["Debit"].ToString());
        //            if (dr["Credit"] != null && dr["Credit"].ToString() != "")
        //                totcredit += Convert.ToDouble(dr["Credit"].ToString());
        //        }

        //    }
        //    _MOpeningDebit += totdebit;
        //    _MOpeningCredit += totcredit;
        //}

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        private void NoofRows(PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportListn)
        {
            string strmessage = General.NoofRows(dgvReportListn.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        #endregion

        # region Events


        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            //string voutype = "";
            //string vousubtype = "";
            try
            {
                string selectedID = "";
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ReportControl = new UclAcListDebtorLedger();
                    ShowReportForm(selectedID, _MFromDate, _MToDate);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }


        #endregion Events


        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "F10 = Show Report");
                ttToolTip.SetToolTip(pnlMultiSelection1, "F12 = Reopen This Form , F11 = Date ");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        private void txtLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_MLevel >= 1 && _MLevel <= 4)
                    btnOKMultiSelectionClick();
            }
            else if (e.KeyCode == Keys.Up)
                toDate1.Focus();
        }

        private void btnSecondLevel_Click(object sender, EventArgs e)
        {
            ButtonSecondLevelClicked();
        }

        private void ButtonSecondLevelClicked()
        {
            dgvReportList.Visible = false;
            dgvReportList.Dock = DockStyle.None;
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel1.Dock = DockStyle.None;
            dgvReportListLevel2.Visible = true;
            dgvReportListLevel2.Dock = DockStyle.Fill;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel3.Dock = DockStyle.None;
            dgvReportListLevel4.Visible = false;
            dgvReportListLevel4.Dock = DockStyle.None;
            btnRemoveZero.Visible = false;
            dgvDifference.Visible = false;
            dgvDifference.Dock = DockStyle.None;
            btnDifference.BackColor = Color.White;
            btnAlphabetical.BackColor = Color.White;
            btnFirstLevel.BackColor = Color.White;
            btnSecondLevel.BackColor = Color.Bisque;
            btnThirdLevel.BackColor = Color.White;
            btnFourthLevel.BackColor = Color.White;
            NoofRows(dgvReportListLevel2);

        }

        private void btnThirdLevel_Click(object sender, EventArgs e)
        {
            ButtonThirdLevelClicked();
        }
        private void ButtonThirdLevelClicked()
        {
            dgvReportList.Visible = false;
            dgvReportList.Dock = DockStyle.None;
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel1.Dock = DockStyle.None;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel2.Dock = DockStyle.None;
            dgvReportListLevel3.Visible = true;
            dgvReportListLevel3.Dock = DockStyle.Fill;
            dgvReportListLevel4.Visible = false;
            dgvReportListLevel4.Dock = DockStyle.None;
            btnRemoveZero.Visible = false;
            dgvDifference.Visible = false;
            dgvDifference.Dock = DockStyle.None;
            btnDifference.BackColor = Color.White;
            btnAlphabetical.BackColor = Color.White;
            btnFirstLevel.BackColor = Color.White;
            btnSecondLevel.BackColor = Color.White;
            btnThirdLevel.BackColor = Color.Bisque;
            btnFourthLevel.BackColor = Color.White;
        }

        private void btnFourthLevel_Click(object sender, EventArgs e)
        {
            ButtonFourthLevelClicked();
        }
        private void ButtonFourthLevelClicked()
        {
            dgvReportList.Visible = false;
            dgvReportList.Dock = DockStyle.None;
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel1.Dock = DockStyle.None;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel2.Dock = DockStyle.None;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel3.Dock = DockStyle.None;
            dgvReportListLevel4.Visible = true;
            dgvReportListLevel4.Dock = DockStyle.Fill;
            btnRemoveZero.Visible = false;
            dgvDifference.Visible = false;
            dgvDifference.Dock = DockStyle.None;
            btnDifference.BackColor = Color.White;
            btnAlphabetical.BackColor = Color.White;
            btnFirstLevel.BackColor = Color.White;
            btnSecondLevel.BackColor = Color.White;
            btnThirdLevel.BackColor = Color.White;
            btnFourthLevel.BackColor = Color.Bisque;
        }

        private void btnFirstLevel_Click(object sender, EventArgs e)
        {
            ButtonFirstLevelClicked();
        }
        private void ButtonFirstLevelClicked()
        {
            dgvReportList.Visible = false;
            dgvReportList.Dock = DockStyle.None;
            dgvReportListLevel1.Visible = true;
            dgvReportListLevel1.Dock = DockStyle.Fill;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel2.Dock = DockStyle.None;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel3.Dock = DockStyle.None;
            dgvReportListLevel4.Visible = false;
            dgvReportListLevel4.Dock = DockStyle.None;
            btnRemoveZero.Visible = false;
            dgvDifference.Visible = false;
            dgvDifference.Dock = DockStyle.None;
            btnDifference.BackColor = Color.White;
            btnAlphabetical.BackColor = Color.White;
            btnFirstLevel.BackColor = Color.Bisque;
            btnSecondLevel.BackColor = Color.White;
            btnThirdLevel.BackColor = Color.White;
            btnFourthLevel.BackColor = Color.White;
            NoofRows(dgvReportListLevel1);
        }



        private void btnAlphabetical_Click(object sender, EventArgs e)
        {

            ButtonAlphabeticalClicked();
        }
        private void ButtonAlphabeticalClicked()
        {
            dgvReportList.Visible = true;
            dgvReportList.Dock = DockStyle.Fill;
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel1.Dock = DockStyle.None;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel2.Dock = DockStyle.None;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel3.Dock = DockStyle.None;
            dgvReportListLevel4.Visible = false;
            dgvReportListLevel4.Dock = DockStyle.None;
            dgvDifference.Visible = false;
            dgvDifference.Dock = DockStyle.None;
            BindReportGrid();
            btnRemoveZero.Visible = true;
            btnAlphabetical.BackColor = Color.Bisque;
            btnFirstLevel.BackColor = Color.White;
            btnSecondLevel.BackColor = Color.White;
            btnThirdLevel.BackColor = Color.White;
            btnFourthLevel.BackColor = Color.White;
            btnDifference.BackColor = Color.White;
        }

        private void btnRemoveZero_Click(object sender, EventArgs e)
        {
            ButtonRemoveZeroClick();
            if (btnRemoveZero.Text == "Remove Zero")
                btnRemoveZero.Text = "Show Zero";
            else
                btnRemoveZero.Text = "Remove Zero";

        }

        private void ButtonRemoveZeroClick()
        {
            BindReportGrid();
        }

        private void btnDifference_Click(object sender, EventArgs e)
        {
            ButtonDifferenceClicked();
        }

        private void ButtonDifferenceClicked()
        {
            dgvReportList.Visible = false;
            dgvReportList.Dock = DockStyle.None;
            dgvReportListLevel1.Visible = false;
            dgvReportListLevel1.Dock = DockStyle.None;
            dgvReportListLevel2.Visible = false;
            dgvReportListLevel2.Dock = DockStyle.None;
            dgvReportListLevel3.Visible = false;
            dgvReportListLevel3.Dock = DockStyle.None;
            dgvReportListLevel4.Visible = false;
            dgvReportListLevel4.Dock = DockStyle.None;
            dgvDifference.Visible = true;
            dgvDifference.Dock = DockStyle.Fill;
            BindReportGrid();
            btnRemoveZero.Visible = true;
            btnAlphabetical.BackColor = Color.White;
            btnFirstLevel.BackColor = Color.White;
            btnSecondLevel.BackColor = Color.White;
            btnThirdLevel.BackColor = Color.White;
            btnFourthLevel.BackColor = Color.White;
            btnDifference.BackColor = Color.Bisque;
        }

        private void GetDifferenceRows()
        {
            DataTable dt = new DataTable();
            double mdebit = 0;
            double mcredit = 0;
            string mID = "";
            try
            {
                dt = _FinalAccounts.GetTrnacTotalsByVoucherTypeNo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    mdebit = 0;
                    mcredit = 0;
                    mID = "";
                    int _RowIndex;
                    DataGridViewRow currentdr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["VoucherID"] != DBNull.Value)
                            mID = dr["VoucherID"].ToString();
                        if (dr["debit"] != DBNull.Value)
                            mdebit = Convert.ToDouble(dr["debit"].ToString());
                        if (dr["credit"] != DBNull.Value)
                            mcredit = Convert.ToDouble(dr["credit"].ToString());
                        if (mdebit != mcredit)
                        {
                            _RowIndex = dgvDifference.Rows.Add();
                            currentdr = dgvDifference.Rows[_RowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["VoucherID"].ToString();
                            //     currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_VoucherDate"].Value = dr["TransactionDate"].ToString();
                            currentdr.Cells["Col_TransactionDB"].Value = mdebit.ToString("#0.00");
                            currentdr.Cells["Col_TransactionCR"].Value = mcredit.ToString("#0.00");
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgvReportListLevel4_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportListLevel4.SelectedRow != null && dgvReportListLevel4.Rows.Count > 0)
            {
                string parno = string.Empty;
                string acno = string.Empty;
                if (dgvReportListLevel4.SelectedRow.Cells["Col_UnderGroupID"].Value != null && dgvReportListLevel4.SelectedRow.Cells["Col_UnderGroupID"].Value.ToString() != string.Empty)
                    parno = dgvReportListLevel4.SelectedRow.Cells["Col_UnderGroupID"].Value.ToString();
                if (dgvReportListLevel4.SelectedRow.Cells["Col_AccountID"].Value != null && dgvReportListLevel4.SelectedRow.Cells["Col_AccountID"].Value.ToString() != string.Empty)
                    acno = dgvReportListLevel4.SelectedRow.Cells["Col_AccountID"].Value.ToString();
                if (parno == string.Empty)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportListLevel4.SelectedRow.Cells[0].Value.ToString();
                    if (acno == FixAccounts.AccountCash)
                        ReportControl = new UclAcListCashBook();
                    else
                        ReportControl = new UclAcListGeneralLedger();
                    ShowReportForm(selectedID, _MFromDate, _MToDate);
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void dgvReportListLevel3_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                ReportControl = new UclStockListProductLedger();
                ShowReportForm(selectedID, _MFromDate, _MToDate);
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvReportList_DoubleClicked_1(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                ReportControl = new UclStockListProductLedger();
                ShowReportForm(selectedID, _MFromDate, _MToDate);
                this.Cursor = Cursors.Default;
            }
        }
        
    }
}
