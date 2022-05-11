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
    public partial class UclToolSetMinMaxLevel : BaseControl
    {
         private int _min = 0;
         private int _max = 0;
         Product prod = new Product();

        public UclToolSetMinMaxLevel()
        {
            InitializeComponent();
        }

        public override bool Add()
        {           
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
            btnStart.Enabled = false;
            headerLabel1.Text = "Change Minimum and Maximum Level";
            txtMaxLevel.Focus();
            return retValue;
        }
        public override bool ClearData()
        {
            ClearControls();
            return base.ClearData();
        }

        private void ClearControls()
        {
            txtMaxLevel.Text = "0";
            txtMaxLevel.Text = "0";
        }
        public override void SetFocus()
        {
            base.SetFocus();
            txtMinLevel.Focus();
        }
        private void BtnStartClick()
        {
            btnStart.Enabled = false;
            if (txtpasswd.Text.ToString().ToUpper() == "CHANGE")
            {
                bool retValue = prod.SetMinMax(_min, _max);
                MessageBox.Show("DONE");
            }
        }

        private void txtMinLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMaxLevel.Focus();
        }

        private void txtMaxLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _min = Convert.ToInt32(txtMinLevel.Text.ToString());
                _max = Convert.ToInt32(txtMaxLevel.Text.ToString());
                if (_min <= _max)
                    txtpasswd.Focus();
                else
                    txtMinLevel.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtMinLevel.Focus();

        }

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text.ToString().ToUpper() == "CHANGE")
                    btnStart.Enabled = true;
                else
                {
                    txtpasswd.Focus();
                    btnStart.Enabled = false;
                }
            }
            else if (e.KeyCode == Keys.Up)
                txtMaxLevel.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            BtnStartClick();
        }   
    }
}
