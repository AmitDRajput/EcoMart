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
using System.Collections;


namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclFACListEntryofScheduleNumber : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private FinalAccounts _FinalAccounts;    
        #endregion

        # region Constructor
        public UclFACListEntryofScheduleNumber()
        {
            InitializeComponent();
        }
        #endregion

        #region IOverview Members
        public override bool Add()
        {
            bool retValue = base.Add();
            _BindingSource = new DataTable();
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            _FinalAccounts = new FinalAccounts();
            headerLabel1.Text = "FINAL ACCOUNTS - SCHEDULE NUMBER ENTRY";
            ConstructReportColumns();
            mpMSVCGroup.Columns["Col_ID"].Visible = false;
            pnlEnterScheduleNumber.Visible = false;
            FillReportGrid();           
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
         //   System.Text.StringBuilder _errorMessage;
            string mgroupid = "";
            int mschedulenumber = 0;
            foreach (DataGridViewRow dr in mpMSVCGroup.Rows)
            {
                mschedulenumber = 0;
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != string.Empty)
                {
                    mgroupid = dr.Cells["Col_ID"].Value.ToString();

                    if (dr.Cells["Col_ScheduleNumber"].Value != null && dr.Cells["Col_ScheduleNumber"].Value.ToString() != string.Empty)
                        mschedulenumber = Convert.ToInt32(dr.Cells["Col_ScheduleNumber"].Value.ToString());
                    _FinalAccounts.SaveScheduleNumber(mgroupid, mschedulenumber);
                }

            }

            return retValue;
        }

        public override void SetFocus()
        {
            mpMSVCGroup.Focus();
        }

        #endregion

        # region IReport Members

      

    
        #endregion

        # region Other Private methods
        private void ConstructReportColumns()
        {
            mpMSVCGroup.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "GroupID";
            column.ReadOnly = true;
            column.Visible = false;
            mpMSVCGroup.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Tag";
            column.ReadOnly = false;
            column.Width = 1;
            mpMSVCGroup.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Name";
            column.DataPropertyName = "GroupName";
            column.HeaderText = "Group";
            column.ReadOnly = true;
            column.Width = 250;
            mpMSVCGroup.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ScheduleNumber";
            column.DataPropertyName = "ScheduleNumber";
            column.HeaderText = "ScheduleNumber";
            column.ReadOnly = true;
            column.Width = 150;
            mpMSVCGroup.Columns.Add(column);
        }

        private void FillReportGrid()
        {
            FillReportData();
            mpMSVCGroup.DataSource = _BindingSource;           
            int noofrecords = mpMSVCGroup.Rows.Count;
            if (noofrecords == 0)
                lblFooterMessage.Text = "NO Records ";
            else if (noofrecords == 1)
                lblFooterMessage.Text = "Record : " + noofrecords.ToString().Trim();
            else
                lblFooterMessage.Text = "Records : " + noofrecords.ToString().Trim();
        }
     
        private void FillReportData()
        {
            DataTable dtable = new DataTable();
            dtable = _FinalAccounts.GetDataForEntryOfScheduledNumbers();
            _BindingSource = dtable;
        }

        public override string GetShortcutKeys()
        {
            string keyCollection = "";
            if (pnlEnterScheduleNumber.Visible == false)
                keyCollection = "Enter Key = Type Shedule Number ";
            else
                keyCollection = "Enter Key = Go Back to Grid ";
            return keyCollection;
        }
        #endregion

        # region Events
    
        #endregion

        private void mpMSVCGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            mpMSVCGroupDoubledClicked();
        }

     
        private void mpMSVCGroupDoubledClicked()
        {
            
            pnlEnterScheduleNumber.Visible = true;
            int number = 0;
            if (mpMSVCGroup.CurrentRow.Cells["Col_ScheduleNumber"].Value != null && mpMSVCGroup.CurrentRow.Cells["Col_ScheduleNumber"].Value.ToString() != string.Empty)
                number = Convert.ToInt32(mpMSVCGroup.CurrentRow.Cells["Col_ScheduleNumber"].Value.ToString());
            txtScheduleNumber.Text = number.ToString();
            txtScheduleNumber.Focus();
            //pnlClearedDate.Visible = true;

            //if (mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value != null && mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value.ToString() != string.Empty)
            //    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value.ToString();
            //else
            //    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ChequeDate"].Value.ToString();



            ////if (mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value != null && mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString() != string.Empty)
            ////    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString();
            ////else
            ////    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ChequeDate"].Value.ToString();
            //_ClearedDate = General.GetExpiryInyyyymmddForm(_ClearedDate);
            //clearedDate.Value = General.ConvertStringToDateyyyyMMdd(_ClearedDate);
            //// clearedDate.Value = Convert.ToDateTime(_ClearedDate);
            ////  clearedDate.Value = new DateTime(Convert.ToInt32(_ClearedDate.Substring(0, 4)), Convert.ToInt32(_ClearedDate.Substring(4, 2)), Convert.ToInt32(_ClearedDate.Substring(6, 2)));
            //clearedDate.Focus();
        }

        private void txtScheduleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtScheduleNumberEnterKeyPressed();
               
            }
        }

        private void txtScheduleNumberEnterKeyPressed()
        {
            mpMSVCGroup.CurrentRow.Cells["Col_ScheduleNumber"].Value = txtScheduleNumber.Text.ToString();
            txtScheduleNumber.Text = "";
            pnlEnterScheduleNumber.Visible = false;
            mpMSVCGroup.Focus();
        }

        private void mpMSVCGroup_DoubleClick(object sender, EventArgs e)
        {
            mpMSVCGroupDoubledClicked();
        }

        private void pnlEnterScheduleNumber_Click(object sender, EventArgs e)
        {
            txtScheduleNumberEnterKeyPressed();
        }
    }
}
