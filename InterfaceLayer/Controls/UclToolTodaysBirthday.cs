using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolTodaysBirthday : BaseControl    
    {
        public UclToolTodaysBirthday()
        {
            InitializeComponent();
        }
        public override bool Add()
        {
           // _ChangeVAT5to55 = new ChangeVAT5to55();
            bool retValue = base.Add();
            ClearData();
            tsBtnAdd.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnDelete.Visible = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnSave.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            headerLabel1.Text = "Todays Birthday";
            LoadPatientData();
            txtMsg.Text = "Wish You Many Many Happy Returns Of The Day, Happy Birthday.";
            return retValue;
        }

        private void UclToolTodaysBirthday_Load(object sender, EventArgs e)
        {
        }

        private void LoadPatientData()
        {
          //  Patient _Patient = new Patient();
            DateTime TodaysDate = DateTime.Now.Date;
           // DataTable _PatientData = _Patient.GetTodaysBirthdayPatient(TodaysDate);
            dgvTodaysbirthday.Rows.Clear();
            //foreach (DataRow dr in _PatientData.Rows)
            //{
            //    int RowIndex = dgvTodaysbirthday.Rows.Add();
            //    DataGridViewRow row = dgvTodaysbirthday.Rows[RowIndex];

            //    row.Cells["Col_PatientID"].Value = SetData(dr["PatientID"]);
            //    row.Cells["Col_PatientName"].Value = SetData(dr["PatientName"]);
            //    row.Cells["Col_MobileNumberForSMS"].Value = SetData(dr["MobileNumberForSMS"]);
            //}
        }

        private string SetData(object Data)
        {
            string StrData = Convert.ToString(Data);
            if (string.IsNullOrEmpty(StrData) == false)
                return StrData;
            else return string.Empty;
                
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (dgvTodaysbirthday.Rows.Count > 0 && dgvTodaysbirthday.SelectedRows.Count > 0)
            {
                string Msg = "Dear Customer, \n" + txtMsg.Text + "\n From,\n" + General.ShopDetail.ShopName + "\n";
                bool IsMobileNumber = false;
                if (string.IsNullOrEmpty(General.ShopDetail.ShopMobileNumber) == false)
                {
                    Msg += General.ShopDetail.ShopMobileNumber; IsMobileNumber = true;
                }
                if (string.IsNullOrEmpty(General.ShopDetail.ShopTelephone) == false)
                {
                    if (IsMobileNumber)
                        Msg += "/";
                    Msg += General.ShopDetail.ShopTelephone;
                }
                SendSMS mSMS = new SendSMS();
                DataGridViewRow row = dgvTodaysbirthday.SelectedRows[0];
                if (string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_MobileNumberForSMS"].Value)) == false)
                {
                    mSMS.SendSMSData(row.Cells["Col_MobileNumberForSMS"].Value.ToString(), Msg);
                }
                else
                    MessageBox.Show("Please Update Mobile Number", "EcoMart", MessageBoxButtons.OK);
            }
        }
    }
}
