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
    public partial class UclListCompany : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Company _Company;       
        #endregion

        #region Constructor
        public UclListCompany()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclCompany();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor

        #region IOverview Member
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Company = new Company();
                headerLabel1.Text = "LIST-COMPANY";             
                FillReportGrid();
                PrintReportHead = "Company List";
                PrintReportHead2 = "";
                dgvReportList.Focus();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
            base.SetFocus();
            dgvReportList.Focus();
        }

         public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                if (Exit())
                {                   
                    retValue = true;
                }               
            }

            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        #endregion IOverview Member

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

                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 250;
                        }
                        if (dr.Cells["Col_CompShortName"].Visible == true && dr.Cells["Col_CompShortName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompShortName"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 50;
                        }
                        if (dr.Cells["Col_CompContactPerson"].Visible == true && dr.Cells["Col_CompContactPerson"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompContactPerson"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 200;
                        }
                        if (dr.Cells["Col_Telephone"].Visible == true && dr.Cells["Col_Telephone"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompTelephone"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 150;
                        }

                        if (dr.Cells["Col_Email"].Visible == true && dr.Cells["Col_Email"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompMailId"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                PrintColumnPixel = 1;
                


                row = new PrintRow("Company Name", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 250;

                if (dgvReportList.Columns["Col_CompShortName"].Visible == true)
                {
                    row = new PrintRow("ShortName", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 50;
                }
                if (dgvReportList.Columns["Col_CompContactPerson"].Visible == true)
                {
                    row = new PrintRow("    Contact Person", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 200;
                }

                if (dgvReportList.Columns["Col_Telephone"].Visible == true)
                {
                    row = new PrintRow("Telephone", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 150;
                }
                if (dgvReportList.Columns["Col_Email"].Visible == true)
                {
                    row = new PrintRow("EmailID", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);                   
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
        #endregion Print Report

        #region Other private methods

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CompId";
                column.HeaderText = "ID";
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

               
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company Name";
                column.Width = 400;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "CompShortName";
                column.HeaderText = "Short";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompContactPerson";
                column.DataPropertyName = "CompContactPerson";
                column.HeaderText = "Contact Person";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "CompTelephone";
                column.HeaderText = "Telephone";
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Email";
                column.DataPropertyName = "CompMailId";
                column.HeaderText = "EMailID";
                column.Width = 107;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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
                ConstructReportColumns();
                InitializeReportGrid();
                FormatReportGrid();
                FillReportData();              
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }      
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _Company.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }
        private void FormatReportGrid()
        {
            dgvReportList.OptionalColumnNames.Add("Col_CompContactPerson");
            dgvReportList.OptionalColumnNames.Add("Col_Telephone");
            dgvReportList.OptionalColumnNames.Add("Col_Email");
            dgvReportList.OptionalColumnNames.Add("Col_CompShortName");
        }
        #endregion      

        #region Events
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ShowViewForm(selectedID, ViewMode.Current);
                }
            }
            catch (Exception ex)
            {
              Log.WriteError(ex.ToString());
            }
        }    
        #endregion      
        
    }
}

