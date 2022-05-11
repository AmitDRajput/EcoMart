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
    public partial class UclToolShelfTransfer : BaseControl
    {
        # region Declaration
        private Shelf _Shelf;  
        # endregion
        public UclToolShelfTransfer()
        {
            InitializeComponent();
            
        }
        public override bool Add()
        {
            _Shelf = new Shelf();
            bool retValue = base.Add();
            ClearData();
            tsBtnAdd.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnDelete.Visible = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnSavenPrint.Visible = false;
            FilltxtFromShelf();
            FilltxtToShelf();
            headerLabel1.Text = "SHELF Transfer";           
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
         //   System.Text.StringBuilder _errorMessage;
            if (txtFromShelf.SelectedID != null && txtFromShelf.SelectedID.ToString() != string.Empty && txtToShelf.SelectedID != null && txtToShelf.SelectedID.ToString() != string.Empty)
                retValue = _Shelf.ShelfTransfer(txtFromShelf.SelectedID.ToString(), txtToShelf.SelectedID.ToString());
            if (retValue)
                MessageBox.Show("Shelf information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            return retValue;
        }
        private void txtFromShelf_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtFromShelf.SelectedID != null && txtFromShelf.SelectedID.ToString() != string.Empty)
                txtToShelf.Focus();
        }
        public override bool ClearData()
        {
           // _Shelf.Initialise();
            ClearControls();
            txtFromShelf.Focus();          
            return true;
        }
        public void ClearControls()
        {
            txtFromShelf.Text = "";
            txtToShelf.Text = "";
            txtFromShelf.SelectedID = "";
            txtToShelf.SelectedID = "";
        }
        public void FilltxtFromShelf()
        {
            txtFromShelf.SelectedID = null;
            txtFromShelf.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
            txtFromShelf.ColumnWidth = new string[2] { "0", "300" };
            Shelf _Shel = new Shelf();
            DataTable dtable = _Shel.GetOverviewData();
            txtFromShelf.FillData(dtable);
        }
        public void FilltxtToShelf()
        {
            txtToShelf.SelectedID = null;
            txtToShelf.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
            txtToShelf.ColumnWidth = new string[2] { "0", "300" };
            Shelf _Shel = new Shelf();
            DataTable dtable = _Shel.GetOverviewData();
            txtToShelf.FillData(dtable);
        }
    }
}
