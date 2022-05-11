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
    public partial class UclToolDistributorPercentageChange : BaseControl
    {
       //  private int _min = 0;
         private int _max = 0;
        public int progressBar;
         SsStock  prod = new SsStock();

        public UclToolDistributorPercentageChange()
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
            headerLabel1.Text = "Distributor Rate Increased In Percentage";
            txtDistRatePer.Focus();
            return retValue;
        }
        public override bool ClearData()
        {
            ClearControls();
            return base.ClearData();
        }

        private void ClearControls()
        {
            txtDistRatePer.Text = "0";
            txtDistRatePer.Text = "0";
        }
        public override void SetFocus()
        {
            base.SetFocus();
            txtDistRatePer.Focus();
        }
        private void BtnStartClick()
        {
            btnStart.Enabled = false;
            //progressBar1.Minimum = 1;
            if (txtpasswd.Text.ToString().ToUpper() == "CHANGE")
            {
                
                bool retValue = prod.setDistributionPercentage(_max);
                MessageBox.Show("DONE");
            }
        }

        private void txtMinLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDistRatePer.Focus();
        }

        private void txtMaxLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               _max = Convert.ToInt32(txtDistRatePer.Text.ToString());
                if (_max <= 100)
                    txtpasswd.Focus();
                else
                {
                    MessageBox.Show("Please Enter Proper Percentage");
                    txtDistRatePer.Clear();
                    txtDistRatePer.Focus();
                }
                   
            }
           
        }

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text.ToString().ToUpper() == "CHANGE")
                {
                    btnStart.Enabled = true;
                    btnStart.Focus();
                }
                   
                else
                {
                    txtpasswd.Focus();
                    btnStart.Enabled = false;
                }
            }
            else if (e.KeyCode == Keys.Up)
                txtDistRatePer.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                BtnStartClick();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode ==Keys.Enter)
                 {
                   BtnStartClick();
                  }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

       
    }
}
