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


namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclListAccount : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Account _Account;     
        private string _Macccode;
        #endregion

        # region Constructor
        public UclListAccount()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclAccount();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion

        #region IOverview Members
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Account = new Account();
                ClearControls();               
                headerLabel1.Text = "LIST-ACCOUNTS";       
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
            cbCreditor.Focus();
        }
        #endregion IOverview Members

        # region IReport Members
        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);
            GeneralReport.ExportFile(PrintReportHead, PrintReportHead2, dgvReportList, ExportFileName);
        }

        public override void EMail(string EmailID)
        {
            base.EMail(EmailID);
            GeneralReport.SendEmails(PrintReportHead, PrintReportHead2, dgvReportList, EmailID);
        }
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
                PrintBill.Rows.Clear();
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;             
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null || dr.Cells["Col_ID"].Value.ToString() != "")
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
                        PrintColumnPixel = 1;

                        if (dr.Cells["Col_Acccode"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccCode"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 15;
                        }
                     
                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString(), PrintRowPixel,PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 200;
                        }

                        if (dr.Cells["Col_Address"].Value != null && dr.Cells["Col_Address"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_Address"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 200;
                        }
                        if (dr.Cells["Col_Telephone"].Value != null &&   dr.Cells["Col_Telephone"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_Telephone"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 100;
                        }
                        if (dr.Cells["Col_Email"].Value != null &&  dr.Cells["Col_Email"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_Email"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 150;
                        }
                        if (dr.Cells["Col_BirthDay"].Value != null &&  dr.Cells["Col_BirthDay"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_BirthDay"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 50;
                        }
                        if (dr.Cells["Col_BirthMonth"].Value != null &&  dr.Cells["Col_BirthMonth"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_BirthMonth"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 50;
                        }
                        if (dr.Cells["Col_DocName"].Value != null &&  dr.Cells["Col_DocName"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_DocName"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 150;
                        }
                        if (dr.Cells["Col_Remark"].Value != null &&  dr.Cells["Col_Remark"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_Remark"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 200;
                        }
                        if (dr.Cells["Col_VisitDay1"].Value != null &&  dr.Cells["Col_VisitDay1"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_VisitDay1"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 40;
                        }
                        if (dr.Cells["Col_VisitDay2"].Value != null &&  dr.Cells["Col_VisitDay2"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_VisitDay2"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 40;
                        }
                        if ( dr.Cells["Col_VisitDay3"].Value != null &&  dr.Cells["Col_VisitDay3"].Visible == true)
                        {
                            row = new PrintRow(dr.Cells["Col_VisitDay3"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 40;
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                PrintColumnPixel = 1;

                row = new PrintRow("Name", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 215;
                if (dgvReportList.Columns["Col_Address"].Visible == true)
                {
                    row = new PrintRow("Address", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 200;
                }
                if (dgvReportList.Columns["Col_Telephone"].Visible == true)
                {
                    row = new PrintRow("Telephone", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 100;
                }
                if (dgvReportList.Columns["Col_Email"].Visible == true)
                {
                    row = new PrintRow("Email", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 150;
                }
                if (dgvReportList.Columns["Col_BirthDay"].Visible == true)
                {
                    row = new PrintRow("BirthDay", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 50;
                }
                if (dgvReportList.Columns["Col_BirthMonth"].Visible == true)
                {
                    row = new PrintRow("Month", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 50;
                }
                if (dgvReportList.Columns["Col_DocName"].Visible == true)
                {
                    row = new PrintRow("Doctor", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 150;
                }
                if (dgvReportList.Columns["Col_Remark"].Visible == true)
                {
                    row = new PrintRow("Remark", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 200;
                }
                if (dgvReportList.Columns["Col_VisitDay1"].Visible == true)
                {
                    row = new PrintRow("VD1", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 40;
                }
                if (dgvReportList.Columns["Col_VisitDay2"].Visible == true)
                {
                    row = new PrintRow("VD2", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 40;
                }
                if (dgvReportList.Columns["Col_VisitDay3"].Visible == true)
                {
                    row = new PrintRow("VD3", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 40;
                }
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
                lblFooterMessage.Text = "";
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                HidepnlGO();
                ClearCheckBoxes();
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ClearCheckBoxes()
        {
            cbBank.Checked = false;
            cbCreditor.Checked = false;
            cbDebtor.Checked = false;
            cbGeneral.Checked = false;
            cbOtherCreditor.Checked = false;
            cbOtherDebtor.Checked = false;
            cbPurchase.Checked = false;
            cbSale.Checked = false;
        }

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false; 
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccCode";
                column.DataPropertyName = "AccCode";
                column.HeaderText = "CD";
                column.Width = 20;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Account Name";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "AccTelephone";
                column.HeaderText = "Telephone";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Email";
                column.DataPropertyName = "AccEmailID";
                column.HeaderText = "EmailID";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BirthDay";
                column.DataPropertyName = "BirthDay";
                column.HeaderText = "Day";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BirthMonth";
                column.DataPropertyName = "BirthMonth";
                column.HeaderText = "Month";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DocName";
                column.DataPropertyName = "DocName";
                column.HeaderText = "Doctor";
                column.Width = 153;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Remark";
                column.DataPropertyName = "Remark";
                column.HeaderText = "Remarks";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                   column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VisitDay1";
                column.DataPropertyName = "VisitDay1";
                column.HeaderText = "V'Day1";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VisitDay2";
                column.DataPropertyName = "VisitDay2";
                column.HeaderText = "V'Day2";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VisitDay3";
                column.DataPropertyName = "VisitDay3";
                column.HeaderText = "V'Day3";
                column.Width = 73;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();

                column.Name = "Col_Token";
                column.DataPropertyName = "AccTokenNumber";
                column.HeaderText = "Token";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);
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
                
                CheckFilter();
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void CheckFilter()
        {
            _Macccode = string.Empty;
            try
            {
                if (cbCreditor.Checked == true)
                {
                    _Macccode = "'" + FixAccounts.AccCodeForCreditor + "'";
                }
                if (cbDebtor.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode =  string.Concat(_Macccode , ",");
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForDebtor + "'";
                }
                if (cbBank.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode.ToString().Trim() + ",";
                    _Macccode =_Macccode+ "'" + FixAccounts.AccCodeForBank+"'" ;
                }
                if (cbGeneral.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode + ",";
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForGeneral + "'";
                }
                if (cbPurchase.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode + ",";
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForPurchase + "'";
                }
                if (cbSale.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode + ",";
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForSale + "'";
                }
                if (cbOtherCreditor.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode + ",";
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForOtherCreditor + "'";
                }
                if (cbOtherDebtor.Checked == true)
                {
                    if (_Macccode != "")
                        _Macccode = _Macccode + ",";
                    _Macccode = _Macccode + "'" + FixAccounts.AccCodeForOtherDebtor + "'";
                }
                _BindingSource.DefaultView.RowFilter = "";
                if (_Macccode != "")
                    _BindingSource.DefaultView.RowFilter = "AccCode  in ("+ _Macccode + ")";
                else
                {
                    _BindingSource.DefaultView.RowFilter = "";

                }
                              
               
            }
            catch (Exception ex) { Log.WriteException(ex); }

        }
        private void btnOKMultiSelectionClick()
        {
            ShowpnlGO();            
            InitializeReportGrid();
            FormatReportGrid();
            DataTable dt = new DataTable();
            dgvReportList.DataSource = dt;
            dgvReportList.Bind();
            FillReportGrid();
            CheckFilter();
            dgvReportList.DataSource = _BindingSource;              
            dgvReportList.Bind();
            PrintReportHead = "Account List ";
            PrintReportHead2 = "";
            NoofRows();
            dgvReportList.Focus();
        }
        private void FormatReportGrid()
        {
            dgvReportList.OptionalColumnNames.Add("Col_Address");
            dgvReportList.OptionalColumnNames.Add("Col_Telephone");
            dgvReportList.OptionalColumnNames.Add("Col_Email");
            dgvReportList.OptionalColumnNames.Add("Col_Token");
            dgvReportList.OptionalColumnNames.Add("Col_BirthDay");
            dgvReportList.OptionalColumnNames.Add("Col_BirthMonth");
            dgvReportList.OptionalColumnNames.Add("Col_DocName");
            dgvReportList.OptionalColumnNames.Add("Col_Remark");
            dgvReportList.OptionalColumnNames.Add("Col_VisitDay1");
            dgvReportList.OptionalColumnNames.Add("Col_VisitDay2");
            dgvReportList.OptionalColumnNames.Add("Col_VisitDay3");
            

        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }       
        private void FillReportData()
        {
            try
            {
                _BindingSource = new DataTable();
                _BindingSource = _Account.GetOverviewData();
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events
        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        #endregion

        #region Tooltip
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
        #endregion  Tooltip

      
      
    }
}
