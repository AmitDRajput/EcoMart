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
    public partial class UclToolChangeVAT5To55 : BaseControl
    {
        private ChangeVAT5to55 _ChangeVAT5to55;

        public UclToolChangeVAT5To55()
        {
            InitializeComponent();
        }
        public override bool Add()
        {
            _ChangeVAT5to55 = new ChangeVAT5to55();
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
            headerLabel1.Text = "Change VAT 5.5 to 6";
            return retValue;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            BtnStartClick();
            
        }     

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.Focus();
            }
        }

        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnStartClick();
            }
        }
        private void BtnStartClick()
        {
            btnStart.Enabled = false;
            if (txtpasswd.Text.ToString().ToUpper() == "CHANGE")
            {
                _ChangeVAT5to55.UpdateProductMaster();
                _ChangeVAT5to55.UpdatetblStock();
                _ChangeVAT5to55.UpdateMasterVATPercent();
                _ChangeVAT5to55.UpdateAccNameInmasterAccount();
                MessageBox.Show("DONE");
            }
        }
    }
}
