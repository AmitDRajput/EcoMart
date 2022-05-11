using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.Printing;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclListPatient : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Patient _Patient;    
        #endregion

        #region Constructor
        public UclListPatient()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor

        #region IOverview Members
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Patient = new Patient();
                headerLabel1.Text = "LIST-PATIENT";
                ConstructReportColumns();
                FillReportGrid();
                PrintReportHead =  "Patient List";
                PrintReportHead2 = "";
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region IReport Members

        public DataTable GetReportData()
        {
            return _BindingSource.Copy();
        }
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
                string reportHeading = General.GetReportHeading();
                reportHeading += "Patient List";
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }            
        #endregion

        #region Other private methods      
        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PatientID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 320;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TelephoneNumber";
                column.HeaderText = "Telephone";
                column.Width = 160;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "Email";
                column.HeaderText = "EmailID";
                column.Width = 160;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TokenNumber";
                column.HeaderText = "Token";
                column.Width = 53;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReportColumns_DBAY()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PatientID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 320;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TelephoneNumber";
                column.HeaderText = "Telephone";
                column.Width = 160;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "BirthDay";
                column.HeaderText = "Day";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "BirthMonth";
                column.HeaderText = "Month";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "BirthYear";
                column.HeaderText = "Year";
                column.Width = 73;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReportColumns_VISIT()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PatientID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 320;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TelephoneNumber";
                column.HeaderText = "Telephone";
                column.Width = 160;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "VisitDay1";
                column.HeaderText = "V'Day";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "VisitDay2";
                column.HeaderText = "V'Day";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "VisitDay2";
                column.HeaderText = "V'Day";
                column.Width = 73;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReportColumns_DOCTOR()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PatientID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 320;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TelephoneNumber";
                column.HeaderText = "Telephone";
                column.Width = 160;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "DocName";
                column.HeaderText = "Doctor";
                column.Width = 213;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructReportColumns_REMARK()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PatientID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 320;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "TelephoneNumber";
                column.HeaderText = "Telephone";
                column.Width = 160;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "Remark";
                column.HeaderText = "Remarks";
                column.Width = 213;
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
                //if (rbtBDay.Checked == true)
                //    ConstructReportColumns_DBAY();
                //else if (rbtVisitDays.Checked == true)
                //    ConstructReportColumns_VISIT();
                //else if (rbtDoctor.Checked == true)
                //    ConstructReportColumns_DOCTOR();
                //else if (rbtRemarks.Checked == true)
                //    ConstructReportColumns_REMARK();
                //else ConstructReportColumns();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
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
            lblFooterMessage.Text = strmessage;
        }       
      
        private void FillReportData()
        {
            try
            {
                
                _BindingSource = new DataTable();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();

                _BindingSource = new DataTable();
                _BindingSource = _Patient.GetOverviewData();                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FillReportGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Events
    }
}
