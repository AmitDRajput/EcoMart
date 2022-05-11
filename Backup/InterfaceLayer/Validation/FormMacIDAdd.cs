using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    public partial class FormMacIDAdd : Form
    {
        public FormMacIDAdd()
        {
            InitializeComponent();
        }

        public void LoadControl()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public string GetComputerName()
        {
            string compName = string.Empty;
            
            try
            {
                compName = txtComputerName.Text;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return compName;
        }

        //public string GetMacID()
        //{
        //    string macID = string.Empty;

        //    try
        //    {
        //        macID = txtMacID.Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return macID;
        //}

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtComputerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter computer name...!");
            }
            //else if (txtMacID.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("Please enter MAC ID...!");
            //}
            else
                DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
