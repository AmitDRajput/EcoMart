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
using EcoMart.InterfaceLayer;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolRemoveLinkEDEProduct : BaseControl
    {
        Product prod = new Product();

        public UclToolRemoveLinkEDEProduct()
        {
            InitializeComponent();
        }
        public override void SetFocus()
        {
            base.SetFocus();
            btnStart.Enabled = false;
            mcbCreditor.Focus();
            FillCreditorCombo();
            FillProductCombo();
            tsBtnSave.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            
        }
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
                mcbProduct.ColumnWidth = new string[5] { "0", "250", "50", "50", "50" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillCreditorCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "20", "200", "150", "50", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RemoveLink();
        }

        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            RemoveLink();
        }

        private void RemoveLink()
        {
            btnStart.Enabled = false;
            if (txtpasswd.Text.ToString().ToUpper() == "REMOVE")
            {
                bool retValue = prod.RemoveEDEProductLink(mcbProduct.SelectedID,mcbCreditor.SelectedID);
                MessageBox.Show("DONE");
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbProduct.Focus();
        }

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            txtpasswd.Focus();
        }

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text.ToString().ToUpper() == "REMOVE" && (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty) && (mcbProduct.SelectedID != null && mcbProduct.SelectedID != string.Empty))
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
                mcbProduct.Focus();
        }
    }
}
