
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using EcoMart.Common;
using System.Windows.Forms;
using EcoMart.DataLayer;
using Microsoft.VisualBasic;


namespace EcoMart
{
    public partial class AddSMSsettings : Form
    {
        #region Declaration

        //nandini 25th oct 2010
        private string SMSsettings_id;
        //Kiran 07/03/2017
        private string doublequote = "\"";
        private string singlequote = "'";

        #endregion Declaration

        #region UIEvents
        //nandini 25th oct 2010
        private void AddSMSsettings_Load(System.Object sender, System.EventArgs e)
        {
            GetData();
        }

        //nandini 25th oct 2010
        public AddSMSsettings()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            DataTable objDS = new DataTable();
            SMSDB objDB = new SMSDB();

            objDS = objDB.GetSMSsettings();
            DGVSMSsettings.DataSource = objDS;

            DGVSMSsettings.Columns["ID"].Visible = false;
            Btn_AddNew.Visible = false;
            Btn_Update.Visible = false;
            btnSave.Visible = true;
            Btn_Delete.Visible = false;
            ButtonCancel.Visible = true;
            txtURL.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPriority.Text = string.Empty;


            txtURL.Focus();
        }

        //nandini 25th oct 2010
        private void DGVSMSsettings_CellContentClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    SMSsettings_id = Convert.ToString(this.DGVSMSsettings.Rows[e.RowIndex].Cells[this.DGVSMSsettings.Columns[0].Index].Value);
                    GetDataForEdit(SMSsettings_id);
                    Btn_Delete.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
        }
        //nandini 25th oct 2010
        private void GetDataForEdit(string Id)
        {
            LabelError.Text = string.Empty;
            DataTable objRDS = new DataTable();//SMSSettings Datatable
            SMSDB objRDB = new SMSDB();

            objRDS = objRDB.GetSMSsettingsById(Id);


            if (objRDS.Rows.Count > 0)
            {
                DataRow dr = objRDS.Rows[0];
                txtURL.Text = ReadValidString(Convert.ToString(dr["URL"]));
                TxtName.Text = ReadValidString(Convert.ToString(dr["Name"]));
                TxtPriority.Text = (dr["Priority"]).ToString();

            }
            Btn_Delete.Visible = true;
            Btn_AddNew.Visible = true;
            Btn_Update.Visible = true;
            btnSave.Visible = false;
            txtURL.Focus();
        }
        //nandini 25th oct 2010
        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            bool flag = true;

            DataTable objDS = new DataTable(); //SMSSetting DataTable
            SMSDB objDB = new SMSDB();

            if (CheckAvailability(txtURL.Text) == false)
            {
                LabelError.Text = "please enter URL";

                flag = false;
                return;


            }
            if (CheckAvailability(TxtName.Text) == false)
            {
                LabelError.Text = "please enter name";
                flag = false;
                return;

            }
            if (!string.IsNullOrEmpty(TxtPriority.Text))
            {
                if (!Information.IsNumeric(TxtPriority.Text))
                {
                    LabelError.Text = "Please enter valid data for Priority";
                    flag = false;
                    return;
                }
                else
                {
                    LabelError.Text = string.Empty;
                }
            }
            else
            {
                LabelError.Text = "Please enter Priority";
                flag = false;
                return;

            }

            if (flag == false)
            {
                return;

            }
            else
            {
                //string Id = SMSsettings_id;
                string URL = GetValidString(txtURL.Text);
                string Name = GetValidString(TxtName.Text);

                int Priority = int.Parse(TxtPriority.Text);

                bool result = false;
                try
                {
                    result = objDB.InsertSMSSettings(URL, Name, Priority);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message);
                    MessageBox.Show("Error Saving Data", "Error", MessageBoxButtons.OK);
                }


                if (result)
                {
                    MessageBox.Show("Record inserted", "Insert record", MessageBoxButtons.OK);
                    GetData();
                }

            }
        }
        //nandini 25th oct 2010
        private void Btn_Update_Click(System.Object sender, System.EventArgs e)
        {
            bool flag = true;

            DataTable objDS = new DataTable();//SMSSetting Table
            SMSDB objDB = new SMSDB();
            //PopulateList objPopulateList = new PopulateList();

            if (CheckAvailability(txtURL.Text) == false)
            {
                LabelError.Text = "please enter URL";

                flag = false;
                return;
            }
            if (CheckAvailability(TxtName.Text) == false)
            {
                LabelError.Text = "please enter name";
                flag = false;
                return;

            }
            if (!string.IsNullOrEmpty(TxtPriority.Text))
            {
                if (!Information.IsNumeric(TxtPriority.Text))
                {
                    LabelError.Text = "Please enter valid data for Priority";
                    flag = false;
                    return;
                }
                else
                {
                    LabelError.Text = string.Empty;
                }
            }
            else
            {
                LabelError.Text = "Please enter Priority";
                flag = false;
                return;

            }

            if (flag == false)
            {
                return;
            }
            else
            {
                string Id = SMSsettings_id;
                string URL = GetValidString(txtURL.Text);
                string Name = GetValidString(TxtName.Text);

                int Priority = int.Parse(TxtPriority.Text);

                bool result = false;
                try
                {
                    result = objDB.UpdateSMSSettings(Id, URL, Name, Priority);
                    if (result)
                    {
                        MessageBox.Show("Record Updated", "Update record", MessageBoxButtons.OK);
                        GetData();

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message);
                    MessageBox.Show("Error Saving Data", "Error", MessageBoxButtons.OK);
                }



            }
        }
        //nandini 25th oct 2010
        private void Btn_AddNew_Click(System.Object sender, System.EventArgs e)
        {
            txtURL.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPriority.Text = string.Empty;
            ButtonCancel.Visible = true;
            Btn_AddNew.Visible = false;
            Btn_Update.Visible = false;
            btnSave.Visible = true;
            Btn_Delete.Visible = false;

        }
        //nandini 25th oct 2010
        private void ButtonCancel_Click(System.Object sender, System.EventArgs e)
        {
            txtURL.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPriority.Text = string.Empty;
        }
        //nandini 4th oct 2010

        private void Btn_Delete_Click(System.Object sender, System.EventArgs e)
        {

            try
            {

                SMSDB objDB = new SMSDB();
                int res = 0;

                res = objDB.DeleteAlertById(SMSsettings_id);

                if (res > 0)
                {
                    MessageBox.Show("Record Deleted", "Delete record", MessageBoxButtons.OK);
                    GetData();
                    txtURL.Text = string.Empty;
                    TxtName.Text = string.Empty;
                    TxtPriority.Text = string.Empty;
                    Btn_Delete.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Log.WriteError("Btn_Delete_Click" + ex.Message);
                MessageBox.Show("Cannot delete record,data is in use", "Error deleting Data", MessageBoxButtons.OK);

            }

        }

        #endregion UIEvents

        #region Function

        public string ReadValidString(string strval)
        {

            string retstrval = string.Empty;

            if (!string.IsNullOrEmpty(strval))
            {
                retstrval = strval.Replace(doublequote, singlequote);
            }
            strval = null;
            return retstrval;
        }

        public bool CheckAvailability(string data)
        {
            if (data.Trim() == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //29 march 2010 puja
        public string GetValidString(string strval)
        {

            string retstrval = string.Empty;

            if (!string.IsNullOrEmpty(strval))
            {
                retstrval = strval.Replace(singlequote, doublequote);
            }
            strval = null;
            return retstrval;
        }

        #endregion Function       
    }
}



