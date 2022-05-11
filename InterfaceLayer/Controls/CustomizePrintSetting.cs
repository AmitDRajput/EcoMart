using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.Common;
using System.Text.RegularExpressions;
using PharmaSYSDistributorPlus.InterfaceLayer.CommonControls;
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using System.IO;


namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CustomizePrintSetting : BaseControl
    {
        public CustomizePrintSetting()
        {
            InitializeComponent();

            //BindFont();
        }



        private void txtpagewidth_TextChanged(object sender, EventArgs e)
        {

        }


        # region PageContent DgvGrid
        public void Adddynamiccolumns()
        {
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Columns.Add("Col1", "Column");
            dgprintsettingview.Columns["Col1"].ReadOnly = true;
            dgprintsettingview.Rows.Add("Sr.No", "25");
            dgprintsettingview.Rows[0].DefaultCellStyle.BackColor = Color.LightBlue;
            dgprintsettingview.Rows.Add("Qty", "25");

            dgprintsettingview.Rows.Add("Description", "25");

            dgprintsettingview.Rows.Add("Shelf", "25");
            dgprintsettingview.Rows.Add("Comp", "25");
            dgprintsettingview.Rows.Add("Batch", "25");
            dgprintsettingview.Rows.Add("Expiry", "25");

            dgprintsettingview.Rows.Add("M.R.P", "25");
            dgprintsettingview.Rows.Add("SaleRate", "25");
            dgprintsettingview.Rows.Add("PurchaseRate", "25");
            dgprintsettingview.Rows.Add("VAT%", "25");
            dgprintsettingview.Rows.Add("Amount", "25");
            dgprintsettingview.Rows.Add("ScheduleCategory", "25");

        }
        public void Addshowpixel()
        {
            dgprintsettingview.Columns.Add("Column", "Strat Pixel");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Rows[0].Cells[5].Value = "14";
            dgprintsettingview.Rows[1].Cells[5].Value = "70";
            dgprintsettingview.Rows[2].Cells[5].Value = "200";
            dgprintsettingview.Rows[3].Cells[5].Value = "250";
            dgprintsettingview.Rows[4].Cells[5].Value = "300";
            dgprintsettingview.Rows[5].Cells[5].Value = "350";
            dgprintsettingview.Rows[6].Cells[5].Value = "380";
            dgprintsettingview.Rows[7].Cells[5].Value = "410";
            dgprintsettingview.Rows[8].Cells[5].Value = "480";
            dgprintsettingview.Rows[9].Cells[5].Value = "515";
            dgprintsettingview.Rows[10].Cells[5].Value = "545";
            dgprintsettingview.Rows[11].Cells[5].Value = "124";
            dgprintsettingview.Rows[12].Cells[5].Value = "345";
        }
        public void AddColumnDataFieldPageContent()
        {
            dgprintsettingview.Columns.Add("Column", "Data Field");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();
            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Columns[6].Visible = false;
            dgprintsettingview.Rows[0].Cells[6].Value = "Col_Quantity";
            dgprintsettingview.Rows[1].Cells[6].Value = "Col_ProductName";
            dgprintsettingview.Rows[2].Cells[6].Value = "Col_Shelf";
            dgprintsettingview.Rows[3].Cells[6].Value = "Col_ProdCompShortName";
            dgprintsettingview.Rows[4].Cells[6].Value = "Col_BatchNumber";
            dgprintsettingview.Rows[5].Cells[6].Value = "Col_Expiry";
            dgprintsettingview.Rows[6].Cells[6].Value = "Col_RatePerUnit";
            dgprintsettingview.Rows[7].Cells[6].Value = "Col_Amount";
        }
        public void AddColumnDataTypePageContent()
        {
            dgprintsettingview.Columns.Add("Column", "Data Type");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();
            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Columns[7].Visible = false;
            dgprintsettingview.Rows[0].Cells[7].Value = "Integer";
            dgprintsettingview.Rows[1].Cells[7].Value = "Text";
            dgprintsettingview.Rows[2].Cells[7].Value = "Text";
            dgprintsettingview.Rows[3].Cells[7].Value = "Text";
            dgprintsettingview.Rows[4].Cells[7].Value = "Text";
            dgprintsettingview.Rows[5].Cells[7].Value = "Text";
            dgprintsettingview.Rows[6].Cells[7].Value = "Decimal";
            dgprintsettingview.Rows[7].Cells[7].Value = "Decimal";
        }
        public void AddEndpixel()
        {
            dgprintsettingview.Columns.Add("Col6", "End Pixel");
            dgprintsettingview.Rows.Add("");

            dgprintsettingview.Rows.Add("");

            dgprintsettingview.Rows.Add("");
            dgprintsettingview.Rows.Add("");
            dgprintsettingview.Rows.Add("");
            dgprintsettingview.Rows.Add("");

            dgprintsettingview.Rows.Add("");
            dgprintsettingview.Rows.Add("");

        }
        public void dgvFontSize()
        {
            DataGridViewComboBoxColumn combo1 = new DataGridViewComboBoxColumn();
            combo1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            combo1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo1.HeaderText = "Font Size";
            combo1.Items.Add("7");
            combo1.Items.Add("8");
            combo1.Items.Add("9");
            combo1.Items.Add("10");
            combo1.Items.Add("11");
            combo1.Items.Add("12");
            dgprintsettingview.Columns.Add(combo1);
        }
        public void dgvFontName()
        {
            DataGridViewComboBoxColumn combo2 = new DataGridViewComboBoxColumn();
            combo2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            combo2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo2.HeaderText = "Font Name";
            AutoCompleteStringCollection AutoComStr = new AutoCompleteStringCollection();
            foreach (FontFamily fntfamily in FontFamily.Families)
            {
                combo2.Items.Add(fntfamily.Name);
                AutoComStr.Add(fntfamily.Name);
            }

            dgprintsettingview.Columns.Add(combo2);
        }
        public void ShowBindcheckboxindgv()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //set name for the check box column
            colCB.HeaderText = "Show";
            //colCB.Name = "chkcol";          
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }
        public void FontBoldBindcheckboxindgv()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            //set name for the check box column
            colCB.HeaderText = "Font Bold";
            //colCB.Name = "chkcol";        
            //colCB.HeaderText = "";
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgprintsettingview.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }
        # endregion PageContent DgvGrid

        # region pageheader Dgvegrid

        public void AddPageHeader()
        {

            dgvpageheader.Columns.Add("Column", "Header");
            dgvpageheader.Columns["Column"].ReadOnly = true;
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvpageheader.Rows.Add("ShopNameItem", "25");
            dgvpageheader.Rows.Add("ShopAddress1Item", "25");
            dgvpageheader.Rows.Add("ShopAddress2Item", "25");
            dgvpageheader.Rows.Add("ShopTelephoneItem", "25");
            dgvpageheader.Rows.Add("PatientName", "25");
            dgvpageheader.Rows.Add("PatientAddress", "25");
            dgvpageheader.Rows.Add("PatientTelephone", "25");
            dgvpageheader.Rows.Add("DoctorName", "25");
            dgvpageheader.Rows.Add("DoctorAddress", "25");
            dgvpageheader.Rows.Add("TimeItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeSCAItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeSCRItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeSCSItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeSVUItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeDebitNoteItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeCreditNoteItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeStockOUTItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeStockINItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeCashReceiptItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeCashPaymentItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeBankReceiptItem", "25");
            dgvpageheader.Rows.Add("VoucherTypeBankPaymentItem", "25");
            dgvpageheader.Rows.Add("DateItem", "25");
            dgvpageheader.Rows.Add("PageNoItem", "25");

        }

        public void AddPageHeaderText()
        {
            try
            {

                dgvpageheader.Columns.Add("Column", "Text");
                DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

                txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvpageheader.Rows[0].Cells[1].Value = "";
                dgvpageheader.Rows[1].Cells[1].Value = "";
                dgvpageheader.Rows[2].Cells[1].Value = "";
                dgvpageheader.Rows[3].Cells[1].Value = "Tel:";
                dgvpageheader.Rows[4].Cells[1].Value = "Patient:";
                dgvpageheader.Rows[5].Cells[1].Value = "Add :";
                dgvpageheader.Rows[6].Cells[1].Value = "";
                dgvpageheader.Rows[7].Cells[1].Value = "Doc Name Add:";
                dgvpageheader.Rows[8].Cells[1].Value = "Add:";
                dgvpageheader.Rows[9].Cells[1].Value = "Time:";
                dgvpageheader.Rows[10].Cells[1].Value = "Cash Memo No :";
                dgvpageheader.Rows[11].Cells[1].Value = "Credit Sale:";
                dgvpageheader.Rows[12].Cells[1].Value = "Credit Statement:";
                dgvpageheader.Rows[13].Cells[1].Value = "Voucher Sale:";
                dgvpageheader.Rows[14].Cells[1].Value = "Debit Note:";
                dgvpageheader.Rows[15].Cells[1].Value = "Credit Note:";
                dgvpageheader.Rows[16].Cells[1].Value = "Stock OUT:";
                dgvpageheader.Rows[17].Cells[1].Value = "Stock In:";
                dgvpageheader.Rows[18].Cells[1].Value = "Cash Receipt:";
                dgvpageheader.Rows[19].Cells[1].Value = "Cash Payment:";
                dgvpageheader.Rows[20].Cells[1].Value = "Chq Receipt:";
                dgvpageheader.Rows[21].Cells[1].Value = "Chq Payment:";
                dgvpageheader.Rows[22].Cells[1].Value = "Date:";
                dgvpageheader.Rows[23].Cells[1].Value = "Page:";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }


        public void AddPageHeaderRow()
        {

            dgvpageheader.Columns.Add("Column", "Row");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpageheader.Rows[0].Cells[2].Value = "10";
            dgvpageheader.Rows[1].Cells[2].Value = "25";
            dgvpageheader.Rows[2].Cells[2].Value = "40";
            dgvpageheader.Rows[3].Cells[2].Value = "55";
            dgvpageheader.Rows[4].Cells[2].Value = "70";
            dgvpageheader.Rows[5].Cells[2].Value = "70";
            dgvpageheader.Rows[6].Cells[2].Value = "70";
            dgvpageheader.Rows[7].Cells[2].Value = "85";
            dgvpageheader.Rows[8].Cells[2].Value = "85";
            dgvpageheader.Rows[9].Cells[2].Value = "40";
            dgvpageheader.Rows[10].Cells[2].Value = "10";
            dgvpageheader.Rows[11].Cells[2].Value = "10";
            dgvpageheader.Rows[12].Cells[2].Value = "10";
            dgvpageheader.Rows[13].Cells[2].Value = "10";
            dgvpageheader.Rows[14].Cells[2].Value = "54";
            dgvpageheader.Rows[15].Cells[2].Value = "54";
            dgvpageheader.Rows[16].Cells[2].Value = "54";
            dgvpageheader.Rows[17].Cells[2].Value = "54";
            dgvpageheader.Rows[18].Cells[2].Value = "54";
            dgvpageheader.Rows[19].Cells[2].Value = "54";
            dgvpageheader.Rows[20].Cells[2].Value = "54";
            dgvpageheader.Rows[21].Cells[2].Value = "25";
            dgvpageheader.Rows[22].Cells[2].Value = "55";
            dgvpageheader.Rows[23].Cells[2].Value = "55";

        }

        public void AddPageHeaderColumn()
        {

            dgvpageheader.Columns.Add("Column", "Column");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();
            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpageheader.Rows[0].Cells[3].Value = "14";
            dgvpageheader.Rows[1].Cells[3].Value = "14";
            dgvpageheader.Rows[2].Cells[3].Value = "14";
            dgvpageheader.Rows[3].Cells[3].Value = "14";
            dgvpageheader.Rows[4].Cells[3].Value = "250";
            dgvpageheader.Rows[5].Cells[3].Value = "400";
            dgvpageheader.Rows[6].Cells[3].Value = "14";
            dgvpageheader.Rows[7].Cells[3].Value = "250";
            dgvpageheader.Rows[8].Cells[3].Value = "400";
            dgvpageheader.Rows[9].Cells[3].Value = "250";
            dgvpageheader.Rows[10].Cells[3].Value = "360";
            dgvpageheader.Rows[11].Cells[3].Value = "360";
            dgvpageheader.Rows[12].Cells[3].Value = "360";
            dgvpageheader.Rows[13].Cells[3].Value = "360";
            dgvpageheader.Rows[14].Cells[3].Value = "360";
            dgvpageheader.Rows[15].Cells[3].Value = "366";
            dgvpageheader.Rows[16].Cells[3].Value = "366";
            dgvpageheader.Rows[17].Cells[3].Value = "366";
            dgvpageheader.Rows[18].Cells[3].Value = "366";
            dgvpageheader.Rows[19].Cells[3].Value = "600";
            dgvpageheader.Rows[20].Cells[3].Value = "600";
            dgvpageheader.Rows[21].Cells[3].Value = "600";
            dgvpageheader.Rows[22].Cells[3].Value = "600";
            dgvpageheader.Rows[23].Cells[3].Value = "360";

        }


        public void dgvFontNamedgvpageheader()
        {
            DataGridViewComboBoxColumn combo2 = new DataGridViewComboBoxColumn();
            combo2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            combo2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo2.HeaderText = "Font Name";
            AutoCompleteStringCollection AutoComStr = new AutoCompleteStringCollection();
            foreach (FontFamily fntfamily in FontFamily.Families)
            {
                combo2.Items.Add(fntfamily.Name);
                AutoComStr.Add(fntfamily.Name);
            }

            dgvpageheader.Columns.Add(combo2);
        }


        public void dgvFontSizedgvpageheader()
        {
            DataGridViewComboBoxColumn combo1 = new DataGridViewComboBoxColumn();
            combo1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            combo1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo1.HeaderText = "Font Size";
            combo1.Items.Add("7");
            combo1.Items.Add("8");
            combo1.Items.Add("9");
            combo1.Items.Add("10");
            combo1.Items.Add("11");
            combo1.Items.Add("12");
            dgvpageheader.Columns.Add(combo1);
        }

        public void FontBoldBindcheckboxindgvPageHeader()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            //set name for the check box column
            colCB.HeaderText = "Font Bold";
            //colCB.Name = "chkcol";        
            //colCB.HeaderText = "";
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpageheader.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        public void ShowBindcheckboxindgvPageHeader()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //set name for the check box column
            colCB.HeaderText = "Show";
            //colCB.Name = "chkcol";          
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpageheader.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        public void ShowTextBindcheckboxindgvPageHeader()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //set name for the check box column
            colCB.HeaderText = "Show Text";
            //colCB.Name = "chkcol";          
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpageheader.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        #endregion pageheader Dgvegrid

        # region pagefooter DgvGrid
        public void AddPagefooter()
        {

            dgvpagefooter.Columns.Add("Column", "Footer");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();
            dgvpagefooter.Columns["Column"].ReadOnly = true;
            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvpagefooter.Rows.Add("DiscountItem", "25");
            dgvpagefooter.Rows.Add("GrossAmountItem", "25");
            dgvpagefooter.Rows.Add("NarrationItem", "25");
            dgvpagefooter.Rows.Add("CreditNoteItem", "25");
            dgvpagefooter.Rows.Add("DebitNoteItem", "25");
            dgvpagefooter.Rows.Add("BalanceAmountItem", "25");
            dgvpagefooter.Rows.Add("NetAmountItem", "25");
            dgvpagefooter.Rows.Add("SubjectItem", "25");
            dgvpagefooter.Rows.Add("JurisdictionItem", "25");
            dgvpagefooter.Rows.Add("ATOWItem", "25");
            dgvpagefooter.Rows.Add("DLNItem", "25");
            dgvpagefooter.Rows.Add("VATTINItem", "25");
            dgvpagefooter.Rows.Add("DeclarationItem1", "25");
            dgvpagefooter.Rows.Add("DeclarationItem2", "25");
            dgvpagefooter.Rows.Add("DeclarationItem3", "25");
            dgvpagefooter.Rows.Add("ShopNameItem", "25");
            dgvpagefooter.Rows.Add("SignatureItem", "25");
            dgvpagefooter.Rows.Add("ContinueItem", "25");

        }

        public void AddPagefooterText()
        {
            try
            {

                dgvpagefooter.Columns.Add("Column", "Text");
                DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

                txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvpagefooter.Rows[0].Cells[1].Value = "Discount:";
                dgvpagefooter.Rows[1].Cells[1].Value = "Gross Amount:";
                dgvpagefooter.Rows[2].Cells[1].Value = "Narration:";
                dgvpagefooter.Rows[3].Cells[1].Value = "CN:";
                dgvpagefooter.Rows[4].Cells[1].Value = "DN:";
                dgvpagefooter.Rows[5].Cells[1].Value = "Bal:";
                dgvpagefooter.Rows[6].Cells[1].Value = "Net Amount:";
                dgvpagefooter.Rows[7].Cells[1].Value = "E and O E. Subject to:{0} Jurisdiction";
                dgvpagefooter.Rows[8].Cells[1].Value = "";
                dgvpagefooter.Rows[9].Cells[1].Value = "";
                dgvpagefooter.Rows[10].Cells[1].Value = "DLN:";
                dgvpagefooter.Rows[11].Cells[1].Value = "VAT TIN:";
                dgvpagefooter.Rows[12].Cells[1].Value = "E and O E. Subject to:{0} Jurisdiction";
                dgvpagefooter.Rows[13].Cells[1].Value = "";
                dgvpagefooter.Rows[14].Cells[1].Value = "";
                dgvpagefooter.Rows[15].Cells[1].Value = "For";
                dgvpagefooter.Rows[16].Cells[1].Value = "Accidental over charges will be refunded Pharmasists Sign";
                dgvpagefooter.Rows[17].Cells[1].Value = "Continued....";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }


        public void AddPagefooterRow()
        {

            dgvpagefooter.Columns.Add("Column", "Row");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpagefooter.Rows[0].Cells[2].Value = "308";
            dgvpagefooter.Rows[1].Cells[2].Value = "290";
            dgvpagefooter.Rows[2].Cells[2].Value = "325";
            dgvpagefooter.Rows[3].Cells[2].Value = "325";
            dgvpagefooter.Rows[4].Cells[2].Value = "325";
            dgvpagefooter.Rows[5].Cells[2].Value = "308";
            dgvpagefooter.Rows[6].Cells[2].Value = "335";
            dgvpagefooter.Rows[7].Cells[2].Value = "350";
            dgvpagefooter.Rows[8].Cells[2].Value = "350";
            dgvpagefooter.Rows[9].Cells[2].Value = "350";
            dgvpagefooter.Rows[10].Cells[2].Value = "335";
            dgvpagefooter.Rows[11].Cells[2].Value = "335";
            dgvpagefooter.Rows[12].Cells[2].Value = "335";
            dgvpagefooter.Rows[13].Cells[2].Value = "335";
            dgvpagefooter.Rows[14].Cells[2].Value = "375";
            dgvpagefooter.Rows[15].Cells[2].Value = "325";
            dgvpagefooter.Rows[16].Cells[2].Value = "54";
            dgvpagefooter.Rows[17].Cells[2].Value = "54";

        }

        public void AddPagefooterColumn()
        {

            dgvpagefooter.Columns.Add("Column", "Column");
            DataGridViewTextBoxColumn txtcolumndatatype = new DataGridViewTextBoxColumn();

            txtcolumndatatype.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            txtcolumndatatype.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            txtcolumndatatype.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpagefooter.Rows[0].Cells[3].Value = "";
            dgvpagefooter.Rows[1].Cells[3].Value = "";
            dgvpagefooter.Rows[2].Cells[3].Value = "14";
            dgvpagefooter.Rows[3].Cells[3].Value = "200";
            dgvpagefooter.Rows[4].Cells[3].Value = "250";
            dgvpagefooter.Rows[5].Cells[3].Value = "250";
            dgvpagefooter.Rows[6].Cells[3].Value = "370";
            dgvpagefooter.Rows[7].Cells[3].Value = "14";
            dgvpagefooter.Rows[8].Cells[3].Value = "14";
            dgvpagefooter.Rows[9].Cells[3].Value = "14";
            dgvpagefooter.Rows[10].Cells[3].Value = "120";
            dgvpagefooter.Rows[11].Cells[3].Value = "14";
            dgvpagefooter.Rows[12].Cells[3].Value = "14";
            dgvpagefooter.Rows[13].Cells[3].Value = "14";
            dgvpagefooter.Rows[14].Cells[3].Value = "360";
            dgvpagefooter.Rows[15].Cells[3].Value = "366";
            dgvpagefooter.Rows[16].Cells[3].Value = "366";
            dgvpagefooter.Rows[17].Cells[3].Value = "366";

        }


        public void dgvFontNamedgvPagefooter()
        {
            DataGridViewComboBoxColumn combo2 = new DataGridViewComboBoxColumn();
            combo2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            combo2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo2.HeaderText = "Font Name";
            AutoCompleteStringCollection AutoComStr = new AutoCompleteStringCollection();
            foreach (FontFamily fntfamily in FontFamily.Families)
            {
                combo2.Items.Add(fntfamily.Name);
                AutoComStr.Add(fntfamily.Name);
            }

            dgvpagefooter.Columns.Add(combo2);
        }


        public void dgvFontSizedgvPagefooter()
        {
            DataGridViewComboBoxColumn combo1 = new DataGridViewComboBoxColumn();
            combo1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            combo1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            combo1.HeaderText = "Font Size";
            combo1.Items.Add("7");
            combo1.Items.Add("8");
            combo1.Items.Add("9");
            combo1.Items.Add("10");
            combo1.Items.Add("11");
            combo1.Items.Add("12");
            dgvpagefooter.Columns.Add(combo1);
        }

        public void FontBoldBindcheckboxindgvpagefooter()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            //set name for the check box column
            colCB.HeaderText = "Font Bold";
            //colCB.Name = "chkcol";        
            //colCB.HeaderText = "";
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpagefooter.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        public void ShowBindcheckboxindgvpagefooter()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //set name for the check box column
            colCB.HeaderText = "Show";
            //colCB.Name = "chkcol";          
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpagefooter.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        public void ShowTextBindcheckboxindgvpagefooter()
        {
            //Below i create on check box column in the datagrid view

            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //set name for the check box column
            colCB.HeaderText = "Show Text";
            //colCB.Name = "chkcol";          
            //If you use header check box then use it 
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvpagefooter.Columns.Add(colCB);
            //string expression= dgprintsettingview.SelectedCells[0].Value.ToString();
            //richtextprintingsetexpression.Text = expression;

        }

        # endregion


        private void dgprintsettingview_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }
        }

        private void dgprintsettingview_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtaddnewprintersetingvalue.Text != string.Empty)
            {
                cmbprintseting.Items.Add(txtaddnewprintersetingvalue.Text);

            }
        }

        private void btnaddcancel_Click(object sender, EventArgs e)
        {
            cmbprintseting.Items.Remove(cmbprintseting.SelectedItem);
            txtaddnewprintersetingvalue.Clear();
        }

        private void dgprintsettingview_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dc in dgprintsettingview.Columns)
            {

                if (dgprintsettingview.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgprintsettingview.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgprintsettingview.Rows[selectedrowindex];
                    string expression = Convert.ToString(selectedRow.Cells[0].Value);
                    //richtextprintingsetexpression.Text = expression;
                }

            }
        }

        private void btnsavegridvalue_Click(object sender, EventArgs e)
        {
            try
            {
                InsertValuesInXMLFile();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void InsertValuesInXMLFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode printsNode = doc.CreateElement("PrintSettings");

            XmlNode PrintSettings = null;
            if (cmbprintseting.SelectedIndex > -1)
            {

                string cmbvalue = cmbprintseting.SelectedItem.ToString();
                PrintSettings = doc.CreateElement(cmbvalue);




                # region general seting
                //start general setting/
                XmlNode generalSettingNode = doc.CreateElement("GeneralSettings");


                XmlNode pagewidth = doc.CreateElement("PageWidth");
                pagewidth.AppendChild(doc.CreateTextNode(txtpagewidth.Text));
                generalSettingNode.AppendChild(pagewidth);
                PrintSettings.AppendChild(generalSettingNode);

                XmlNode PageHeight = doc.CreateElement("PageHeight");
                PageHeight.AppendChild(doc.CreateTextNode(txtpagewidth.Text));
                generalSettingNode.AppendChild(PageHeight);
                PrintSettings.AppendChild(generalSettingNode);


                XmlNode LineFeed = doc.CreateElement("LineFeed");
                LineFeed.AppendChild(doc.CreateTextNode(txtpagewidth.Text));
                generalSettingNode.AppendChild(LineFeed);
                PrintSettings.AppendChild(generalSettingNode);

                XmlNode ReverseLineFeed = doc.CreateElement("ReverseLineFeed");
                ReverseLineFeed.AppendChild(doc.CreateTextNode(txtreverselinefeed.Text));
                generalSettingNode.AppendChild(ReverseLineFeed);
                PrintSettings.AppendChild(generalSettingNode);

                XmlNode ContentStartRow = doc.CreateElement("ContentStartRow");
                ContentStartRow.AppendChild(doc.CreateTextNode(txtcontentstartrow.Text));
                generalSettingNode.AppendChild(ContentStartRow);
                PrintSettings.AppendChild(generalSettingNode);
                //End Invoke general seting//
                #endregion general seting


                # region page header

                //Start ShopNameItem
                XmlNode PageHeader = doc.CreateElement("PageHeader");

                XmlNode ShopNameItemNode = doc.CreateElement("ShopNameItem");
                PageHeader.AppendChild(ShopNameItemNode);



                XmlNode textnodeshopname = doc.CreateElement("Text");
                //textnodeshopname.AppendChild(doc.CreateElement(""));
                string shopnametext = dgvpageheader.Rows[0].Cells[1].Value.ToString();
                textnodeshopname.AppendChild(doc.CreateTextNode(shopnametext));
                ShopNameItemNode.AppendChild(textnodeshopname);

                XmlNode nameNode = doc.CreateElement("Row");
                string shopnamerow = dgvpageheader.Rows[0].Cells[2].Value.ToString();
                nameNode.AppendChild(doc.CreateTextNode(shopnamerow));
                ShopNameItemNode.AppendChild(nameNode);


                XmlNode priceNode = doc.CreateElement("Column");
                string shopnamecolumn = dgvpageheader.Rows[0].Cells[3].Value.ToString();
                priceNode.AppendChild(doc.CreateTextNode(shopnamecolumn));
                ShopNameItemNode.AppendChild(priceNode);


                XmlNode fontnameshopname = doc.CreateElement("FontName");

                string shopnameFontName = Convert.ToString(dgvpageheader.Rows[0].Cells[4].FormattedValue.ToString());
                if (shopnameFontName == string.Empty)
                {
                    shopnameFontName = "Arial";
                }
                fontnameshopname.AppendChild(doc.CreateTextNode(shopnameFontName));
                ShopNameItemNode.AppendChild(fontnameshopname);



                XmlNode fontsizeshopname = doc.CreateElement("FontSize");
                string shopnameFontsize = Convert.ToString(dgvpageheader.Rows[0].Cells[5].FormattedValue.ToString());
                if (shopnameFontsize == string.Empty)
                {
                    shopnameFontsize = "9";
                }
                fontsizeshopname.AppendChild(doc.CreateTextNode(shopnameFontsize));
                ShopNameItemNode.AppendChild(fontsizeshopname);



                XmlNode FontBoldshopname = doc.CreateElement("FontBold");
                bool shopnameFontBold = Convert.ToBoolean(dgvpageheader.Rows[0].Cells[6].FormattedValue.ToString());
                FontBoldshopname.AppendChild(doc.CreateTextNode(Convert.ToString(shopnameFontBold)));
                ShopNameItemNode.AppendChild(FontBoldshopname);


                XmlNode Showshopname = doc.CreateElement("Show");
                bool shopnameShow = Convert.ToBoolean(dgvpageheader.Rows[0].Cells[7].FormattedValue.ToString());
                Showshopname.AppendChild(doc.CreateTextNode(Convert.ToString(shopnameShow)));
                ShopNameItemNode.AppendChild(Showshopname);



                XmlNode ShowTextshopname = doc.CreateElement("ShowText");
                bool shopnameShowText = Convert.ToBoolean(dgvpageheader.Rows[0].Cells[8].FormattedValue.ToString());
                ShowTextshopname.AppendChild(doc.CreateTextNode(Convert.ToString(shopnameShowText)));
                ShopNameItemNode.AppendChild(ShowTextshopname);

                //End ShopNameItem



                //Start ShopAddress1Item
                XmlNode ShopAddress1ItemNode = doc.CreateElement("ShopAddress1Item");
                PageHeader.AppendChild(ShopAddress1ItemNode);

                XmlNode textnodeShopAddress1 = doc.CreateElement("Text");
                //textnodeShopAddress1.AppendChild(doc.CreateElement(""));
                string ShopAddress1text = dgvpageheader.Rows[1].Cells[1].Value.ToString();
                textnodeShopAddress1.AppendChild(doc.CreateTextNode(ShopAddress1text));
                ShopAddress1ItemNode.AppendChild(textnodeShopAddress1);

                XmlNode ShopAddress1nameNode = doc.CreateElement("Row");
                string ShopAddress1row = dgvpageheader.Rows[1].Cells[2].Value.ToString();
                ShopAddress1nameNode.AppendChild(doc.CreateTextNode(ShopAddress1row));
                ShopAddress1ItemNode.AppendChild(ShopAddress1nameNode);


                XmlNode ShopAddress1priceNode = doc.CreateElement("Column");
                string ShopAddress1column = dgvpageheader.Rows[1].Cells[3].Value.ToString();
                ShopAddress1priceNode.AppendChild(doc.CreateTextNode(ShopAddress1column));
                ShopAddress1ItemNode.AppendChild(ShopAddress1priceNode);


                XmlNode fontnameShopAddress1 = doc.CreateElement("FontName");
                string ShopAddress1FontName = Convert.ToString(dgvpageheader.Rows[1].Cells[4].FormattedValue.ToString());
                if (ShopAddress1FontName == string.Empty)
                {
                    ShopAddress1FontName = "Arial";
                }
                fontnameShopAddress1.AppendChild(doc.CreateTextNode(ShopAddress1FontName));
                ShopAddress1ItemNode.AppendChild(fontnameShopAddress1);



                XmlNode fontsizeShopAddress1 = doc.CreateElement("FontSize");
                string ShopAddress1Fontsize = Convert.ToString(dgvpageheader.Rows[1].Cells[5].FormattedValue.ToString());
                if (ShopAddress1Fontsize == string.Empty)
                {
                    ShopAddress1Fontsize = "9";
                }
                fontsizeShopAddress1.AppendChild(doc.CreateTextNode(ShopAddress1Fontsize));
                ShopAddress1ItemNode.AppendChild(fontsizeShopAddress1);



                XmlNode FontBoldShopAddress1 = doc.CreateElement("FontBold");
                bool ShopAddress1FontBold = Convert.ToBoolean(dgvpageheader.Rows[1].Cells[6].FormattedValue.ToString());
                FontBoldShopAddress1.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress1FontBold)));
                ShopAddress1ItemNode.AppendChild(FontBoldShopAddress1);


                XmlNode ShowShopAddress1 = doc.CreateElement("Show");
                bool ShopAddress1Show = Convert.ToBoolean(dgvpageheader.Rows[1].Cells[7].FormattedValue.ToString());
                ShowShopAddress1.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress1Show)));
                ShopAddress1ItemNode.AppendChild(ShowShopAddress1);



                XmlNode ShowTextShopAddress1 = doc.CreateElement("ShowText");
                bool ShopAddress1ShowText = Convert.ToBoolean(dgvpageheader.Rows[1].Cells[8].FormattedValue.ToString());
                ShowTextShopAddress1.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress1ShowText)));
                ShopAddress1ItemNode.AppendChild(ShowTextShopAddress1);

                //End ShopAddress1Item




                //Start ShopAddress2Item

                XmlNode ShopAddress2ItemNode = doc.CreateElement("ShopAddress2Item");
                PageHeader.AppendChild(ShopAddress2ItemNode);

                XmlNode textnodeShopAddress2 = doc.CreateElement("Text");
                //textnodeShopAddress2.AppendChild(doc.CreateElement(""));
                string ShopAddress2text = dgvpageheader.Rows[2].Cells[1].Value.ToString();
                textnodeShopAddress2.AppendChild(doc.CreateTextNode(ShopAddress2text));
                ShopAddress2ItemNode.AppendChild(textnodeShopAddress2);

                XmlNode ShopAddress2nameNode = doc.CreateElement("Row");
                string ShopAddress2row = dgvpageheader.Rows[2].Cells[2].Value.ToString();
                ShopAddress2nameNode.AppendChild(doc.CreateTextNode(ShopAddress2row));
                ShopAddress2ItemNode.AppendChild(ShopAddress2nameNode);


                XmlNode ShopAddress2priceNode = doc.CreateElement("Column");
                string ShopAddress2column = dgvpageheader.Rows[2].Cells[3].Value.ToString();
                ShopAddress2priceNode.AppendChild(doc.CreateTextNode(ShopAddress2column));
                ShopAddress2ItemNode.AppendChild(ShopAddress2priceNode);


                XmlNode fontnameShopAddress2 = doc.CreateElement("FontName");
                string ShopAddress2FontName = Convert.ToString(dgvpageheader.Rows[2].Cells[4].FormattedValue.ToString());
                if (ShopAddress2FontName == string.Empty)
                {
                    ShopAddress2FontName = "Arial";
                }
                fontnameShopAddress2.AppendChild(doc.CreateTextNode(ShopAddress2FontName));
                ShopAddress2ItemNode.AppendChild(fontnameShopAddress2);



                XmlNode fontsizeShopAddress2 = doc.CreateElement("FontSize");
                string ShopAddress2Fontsize = Convert.ToString(dgvpageheader.Rows[2].Cells[5].FormattedValue.ToString());
                if (ShopAddress2Fontsize == string.Empty)
                {
                    ShopAddress2Fontsize = "9";
                }
                fontsizeShopAddress2.AppendChild(doc.CreateTextNode(ShopAddress2Fontsize));
                ShopAddress2ItemNode.AppendChild(fontsizeShopAddress2);



                XmlNode FontBoldShopAddress2 = doc.CreateElement("FontBold");
                bool ShopAddress2FontBold = Convert.ToBoolean(dgvpageheader.Rows[2].Cells[6].FormattedValue.ToString());
                FontBoldShopAddress2.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress2FontBold)));
                ShopAddress2ItemNode.AppendChild(FontBoldShopAddress2);


                XmlNode ShowShopAddress2 = doc.CreateElement("Show");
                bool ShopAddress2Show = Convert.ToBoolean(dgvpageheader.Rows[2].Cells[7].FormattedValue.ToString());
                ShowShopAddress2.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress2Show)));
                ShopAddress2ItemNode.AppendChild(ShowShopAddress2);



                XmlNode ShowTextShopAddress2 = doc.CreateElement("ShowText");
                bool ShopAddress2ShowText = Convert.ToBoolean(dgvpageheader.Rows[2].Cells[8].FormattedValue.ToString());
                ShowTextShopAddress2.AppendChild(doc.CreateTextNode(Convert.ToString(ShopAddress2ShowText)));
                ShopAddress2ItemNode.AppendChild(ShowTextShopAddress2);


                //End ShopAddress2Item


                //Start ShopTelephoneItem

                XmlNode ShopTelephoneItemNode = doc.CreateElement("ShopTelephoneItem");
                PageHeader.AppendChild(ShopTelephoneItemNode);

                XmlNode textnodeShopTelephone = doc.CreateElement("Text");
                //textnodeShopTelephone.AppendChild(doc.CreateElement(""));
                string ShopTelephonetext = dgvpageheader.Rows[3].Cells[1].Value.ToString();
                textnodeShopTelephone.AppendChild(doc.CreateTextNode(ShopTelephonetext));
                ShopTelephoneItemNode.AppendChild(textnodeShopTelephone);

                XmlNode ShopTelephonenameNode = doc.CreateElement("Row");
                string ShopTelephonerow = dgvpageheader.Rows[3].Cells[2].Value.ToString();
                ShopTelephonenameNode.AppendChild(doc.CreateTextNode(ShopTelephonerow));
                ShopTelephoneItemNode.AppendChild(ShopTelephonenameNode);


                XmlNode ShopTelephonepriceNode = doc.CreateElement("Column");
                string ShopTelephonecolumn = dgvpageheader.Rows[3].Cells[3].Value.ToString();
                ShopTelephonepriceNode.AppendChild(doc.CreateTextNode(ShopTelephonecolumn));
                ShopTelephoneItemNode.AppendChild(ShopTelephonepriceNode);


                XmlNode fontnameShopTelephone = doc.CreateElement("FontName");
                string ShopTelephoneFontName = Convert.ToString(dgvpageheader.Rows[3].Cells[4].FormattedValue.ToString());
                if (ShopTelephoneFontName == string.Empty)
                {
                    ShopTelephoneFontName = "Arial";
                }
                fontnameShopTelephone.AppendChild(doc.CreateTextNode(ShopTelephoneFontName));
                ShopTelephoneItemNode.AppendChild(fontnameShopTelephone);



                XmlNode fontsizeShopTelephone = doc.CreateElement("FontSize");
                string ShopTelephoneFontsize = Convert.ToString(dgvpageheader.Rows[3].Cells[5].FormattedValue.ToString());
                if (ShopTelephoneFontsize == string.Empty)
                {
                    ShopTelephoneFontsize = "9";
                }
                fontsizeShopTelephone.AppendChild(doc.CreateTextNode(ShopTelephoneFontsize));
                ShopTelephoneItemNode.AppendChild(fontsizeShopTelephone);



                XmlNode FontBoldShopTelephone = doc.CreateElement("FontBold");
                bool ShopTelephoneFontBold = Convert.ToBoolean(dgvpageheader.Rows[3].Cells[6].FormattedValue.ToString());
                FontBoldShopTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(ShopTelephoneFontBold)));
                ShopTelephoneItemNode.AppendChild(FontBoldShopTelephone);


                XmlNode ShowShopTelephone = doc.CreateElement("Show");
                bool ShopTelephoneShow = Convert.ToBoolean(dgvpageheader.Rows[3].Cells[7].FormattedValue.ToString());
                ShowShopTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(ShopTelephoneShow)));
                ShopTelephoneItemNode.AppendChild(ShowShopTelephone);



                XmlNode ShowTextShopTelephone = doc.CreateElement("ShowText");
                bool ShopTelephoneShowText = Convert.ToBoolean(dgvpageheader.Rows[3].Cells[8].FormattedValue.ToString());
                ShowTextShopTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(ShopTelephoneShowText)));
                ShopTelephoneItemNode.AppendChild(ShowTextShopTelephone);

                //End ShopTelephoneItem



                //Start PatientName
                XmlNode PatientNameItemNode = doc.CreateElement("PatientName");
                PageHeader.AppendChild(PatientNameItemNode);

                XmlNode textnodePatientName = doc.CreateElement("Text");
                //textnodePatientName.AppendChild(doc.CreateElement(""));
                string PatientNametext = dgvpageheader.Rows[4].Cells[1].Value.ToString();
                textnodePatientName.AppendChild(doc.CreateTextNode(PatientNametext));
                PatientNameItemNode.AppendChild(textnodePatientName);

                XmlNode PatientNamenameNode = doc.CreateElement("Row");
                string PatientNamerow = dgvpageheader.Rows[4].Cells[2].Value.ToString();
                PatientNamenameNode.AppendChild(doc.CreateTextNode(PatientNamerow));
                PatientNameItemNode.AppendChild(PatientNamenameNode);


                XmlNode PatientNamepriceNode = doc.CreateElement("Column");
                string PatientNamecolumn = dgvpageheader.Rows[4].Cells[3].Value.ToString();
                PatientNamepriceNode.AppendChild(doc.CreateTextNode(PatientNamecolumn));
                PatientNameItemNode.AppendChild(PatientNamepriceNode);


                XmlNode fontnamePatientName = doc.CreateElement("FontName");
                string PatientNameFontName = Convert.ToString(dgvpageheader.Rows[4].Cells[4].FormattedValue.ToString());
                if (PatientNameFontName == string.Empty)
                {
                    PatientNameFontName = "Arial";
                }
                fontnamePatientName.AppendChild(doc.CreateTextNode(PatientNameFontName));
                PatientNameItemNode.AppendChild(fontnamePatientName);



                XmlNode fontsizePatientName = doc.CreateElement("FontSize");
                string PatientNameFontsize = Convert.ToString(dgvpageheader.Rows[4].Cells[5].FormattedValue.ToString());
                if (PatientNameFontsize == string.Empty)
                {
                    PatientNameFontsize = "9";
                }
                fontsizePatientName.AppendChild(doc.CreateTextNode(PatientNameFontsize));
                PatientNameItemNode.AppendChild(fontsizePatientName);



                XmlNode FontBoldPatientName = doc.CreateElement("FontBold");
                bool PatientNameFontBold = Convert.ToBoolean(dgvpageheader.Rows[4].Cells[6].FormattedValue.ToString());
                FontBoldPatientName.AppendChild(doc.CreateTextNode(Convert.ToString(PatientNameFontBold)));
                PatientNameItemNode.AppendChild(FontBoldPatientName);


                XmlNode ShowPatientName = doc.CreateElement("Show");
                bool PatientNameShow = Convert.ToBoolean(dgvpageheader.Rows[4].Cells[7].FormattedValue.ToString());
                ShowPatientName.AppendChild(doc.CreateTextNode(Convert.ToString(PatientNameShow)));
                PatientNameItemNode.AppendChild(ShowPatientName);



                XmlNode ShowTextPatientName = doc.CreateElement("ShowText");
                bool PatientNameShowText = Convert.ToBoolean(dgvpageheader.Rows[4].Cells[8].FormattedValue.ToString());
                ShowTextPatientName.AppendChild(doc.CreateTextNode(Convert.ToString(PatientNameShowText)));
                PatientNameItemNode.AppendChild(ShowTextPatientName);
                //End PatientName







                //Start PatientAddress
                XmlNode PatientAddressItemNode = doc.CreateElement("PatientAddress");
                PageHeader.AppendChild(PatientAddressItemNode);

                XmlNode textnodePatientAddress = doc.CreateElement("Text");
                //textnodePatientAddress.AppendChild(doc.CreateElement(""));
                string PatientAddresstext = dgvpageheader.Rows[5].Cells[1].Value.ToString();
                textnodePatientAddress.AppendChild(doc.CreateTextNode(PatientAddresstext));
                PatientAddressItemNode.AppendChild(textnodePatientAddress);

                XmlNode PatientAddressnameNode = doc.CreateElement("Row");
                string PatientAddressrow = dgvpageheader.Rows[5].Cells[2].Value.ToString();
                PatientAddressnameNode.AppendChild(doc.CreateTextNode(PatientAddressrow));
                PatientAddressItemNode.AppendChild(PatientAddressnameNode);


                XmlNode PatientAddresspriceNode = doc.CreateElement("Column");
                string PatientAddresscolumn = dgvpageheader.Rows[5].Cells[3].Value.ToString();
                PatientAddresspriceNode.AppendChild(doc.CreateTextNode(PatientAddresscolumn));
                PatientAddressItemNode.AppendChild(PatientAddresspriceNode);


                XmlNode fontnamePatientAddress = doc.CreateElement("FontName");
                string PatientAddressFontName = Convert.ToString(dgvpageheader.Rows[5].Cells[4].FormattedValue.ToString());
                if (PatientAddressFontName == string.Empty)
                {
                    PatientAddressFontName = "Arial";
                }
                fontnamePatientAddress.AppendChild(doc.CreateTextNode(PatientAddressFontName));
                PatientAddressItemNode.AppendChild(fontnamePatientAddress);



                XmlNode fontsizePatientAddress = doc.CreateElement("FontSize");
                string PatientAddressFontsize = Convert.ToString(dgvpageheader.Rows[5].Cells[5].FormattedValue.ToString());
                if (PatientAddressFontsize == string.Empty)
                {
                    PatientAddressFontsize = "9";
                }
                fontsizePatientAddress.AppendChild(doc.CreateTextNode(PatientAddressFontsize));
                PatientAddressItemNode.AppendChild(fontsizePatientAddress);



                XmlNode FontBoldPatientAddress = doc.CreateElement("FontBold");
                bool PatientAddressFontBold = Convert.ToBoolean(dgvpageheader.Rows[5].Cells[6].FormattedValue.ToString());
                FontBoldPatientAddress.AppendChild(doc.CreateTextNode(Convert.ToString(PatientAddressFontBold)));
                PatientAddressItemNode.AppendChild(FontBoldPatientAddress);


                XmlNode ShowPatientAddress = doc.CreateElement("Show");
                bool PatientAddressShow = Convert.ToBoolean(dgvpageheader.Rows[5].Cells[7].FormattedValue.ToString());
                ShowPatientAddress.AppendChild(doc.CreateTextNode(Convert.ToString(PatientAddressShow)));
                PatientAddressItemNode.AppendChild(ShowPatientAddress);



                XmlNode ShowTextPatientAddress = doc.CreateElement("ShowText");
                bool PatientAddressShowText = Convert.ToBoolean(dgvpageheader.Rows[5].Cells[8].FormattedValue.ToString());
                ShowTextPatientAddress.AppendChild(doc.CreateTextNode(Convert.ToString(PatientAddressShowText)));
                PatientAddressItemNode.AppendChild(ShowTextPatientAddress);


                //End PatientAddress



                //Start PatientTelephone
                XmlNode PatientTelephoneItemNode = doc.CreateElement("PatientTelephone");
                PageHeader.AppendChild(PatientTelephoneItemNode);

                XmlNode textnodePatientTelephone = doc.CreateElement("Text");
                //textnodePatientTelephone.AppendChild(doc.CreateElement(""));
                string PatientTelephonetext = dgvpageheader.Rows[6].Cells[1].Value.ToString();
                textnodePatientTelephone.AppendChild(doc.CreateTextNode(PatientTelephonetext));
                PatientTelephoneItemNode.AppendChild(textnodePatientTelephone);

                XmlNode PatientTelephonenameNode = doc.CreateElement("Row");
                string PatientTelephonerow = dgvpageheader.Rows[6].Cells[2].Value.ToString();
                PatientTelephonenameNode.AppendChild(doc.CreateTextNode(PatientTelephonerow));
                PatientTelephoneItemNode.AppendChild(PatientTelephonenameNode);


                XmlNode PatientTelephonepriceNode = doc.CreateElement("Column");
                string PatientTelephonecolumn = dgvpageheader.Rows[6].Cells[3].Value.ToString();
                PatientTelephonepriceNode.AppendChild(doc.CreateTextNode(PatientTelephonecolumn));
                PatientTelephoneItemNode.AppendChild(PatientTelephonepriceNode);


                XmlNode fontnamePatientTelephone = doc.CreateElement("FontName");
                string PatientTelephoneFontName = Convert.ToString(dgvpageheader.Rows[6].Cells[4].FormattedValue.ToString());
                if (PatientTelephoneFontName == string.Empty)
                {
                    PatientTelephoneFontName = "Arial";
                }
                fontnamePatientTelephone.AppendChild(doc.CreateTextNode(PatientTelephoneFontName));
                PatientTelephoneItemNode.AppendChild(fontnamePatientTelephone);



                XmlNode fontsizePatientTelephone = doc.CreateElement("FontSize");
                string PatientTelephoneFontsize = Convert.ToString(dgvpageheader.Rows[6].Cells[5].FormattedValue.ToString());
                if (PatientTelephoneFontsize == string.Empty)
                {
                    PatientTelephoneFontsize = "9";
                }
                fontsizePatientTelephone.AppendChild(doc.CreateTextNode(PatientTelephoneFontsize));
                PatientTelephoneItemNode.AppendChild(fontsizePatientTelephone);



                XmlNode FontBoldPatientTelephone = doc.CreateElement("FontBold");
                bool PatientTelephoneFontBold = Convert.ToBoolean(dgvpageheader.Rows[6].Cells[6].FormattedValue.ToString());
                FontBoldPatientTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(PatientTelephoneFontBold)));
                PatientTelephoneItemNode.AppendChild(FontBoldPatientTelephone);


                XmlNode ShowPatientTelephone = doc.CreateElement("Show");
                bool PatientTelephoneShow = Convert.ToBoolean(dgvpageheader.Rows[6].Cells[7].FormattedValue.ToString());
                ShowPatientTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(PatientTelephoneShow)));
                PatientTelephoneItemNode.AppendChild(ShowPatientTelephone);



                XmlNode ShowTextPatientTelephone = doc.CreateElement("ShowText");
                bool PatientTelephoneShowText = Convert.ToBoolean(dgvpageheader.Rows[6].Cells[8].FormattedValue.ToString());
                ShowTextPatientTelephone.AppendChild(doc.CreateTextNode(Convert.ToString(PatientTelephoneShowText)));
                PatientTelephoneItemNode.AppendChild(ShowTextPatientTelephone);
                //End PatientTelephone



                //Start Doctorname
                XmlNode DoctorNameItemNode = doc.CreateElement("DoctorName");
                PageHeader.AppendChild(DoctorNameItemNode);

                XmlNode textnodeDoctorName = doc.CreateElement("Text");
                //textnodeDoctorName.AppendChild(doc.CreateElement(""));
                string DoctorNametext = dgvpageheader.Rows[7].Cells[1].Value.ToString();
                textnodeDoctorName.AppendChild(doc.CreateTextNode(DoctorNametext));
                DoctorNameItemNode.AppendChild(textnodeDoctorName);

                XmlNode DoctorNamenameNode = doc.CreateElement("Row");
                string DoctorNamerow = dgvpageheader.Rows[7].Cells[2].Value.ToString();
                DoctorNamenameNode.AppendChild(doc.CreateTextNode(DoctorNamerow));
                DoctorNameItemNode.AppendChild(DoctorNamenameNode);


                XmlNode DoctorNamepriceNode = doc.CreateElement("Column");
                string DoctorNamecolumn = dgvpageheader.Rows[7].Cells[3].Value.ToString();
                DoctorNamepriceNode.AppendChild(doc.CreateTextNode(DoctorNamecolumn));
                DoctorNameItemNode.AppendChild(DoctorNamepriceNode);


                XmlNode fontnameDoctorName = doc.CreateElement("FontName");
                string DoctorNameFontName = Convert.ToString(dgvpageheader.Rows[7].Cells[4].FormattedValue.ToString());
                if (DoctorNameFontName == string.Empty)
                {
                    DoctorNameFontName = "Arial";
                }
                fontnameDoctorName.AppendChild(doc.CreateTextNode(DoctorNameFontName));
                DoctorNameItemNode.AppendChild(fontnameDoctorName);



                XmlNode fontsizeDoctorName = doc.CreateElement("FontSize");
                string DoctorNameFontsize = Convert.ToString(dgvpageheader.Rows[7].Cells[5].FormattedValue.ToString());
                if (DoctorNameFontsize == string.Empty)
                {
                    DoctorNameFontsize = "9";
                }
                fontsizeDoctorName.AppendChild(doc.CreateTextNode(DoctorNameFontsize));
                DoctorNameItemNode.AppendChild(fontsizeDoctorName);



                XmlNode FontBoldDoctorName = doc.CreateElement("FontBold");
                bool DoctorNameFontBold = Convert.ToBoolean(dgvpageheader.Rows[7].Cells[6].FormattedValue.ToString());
                FontBoldDoctorName.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorNameFontBold)));
                DoctorNameItemNode.AppendChild(FontBoldDoctorName);


                XmlNode ShowDoctorName = doc.CreateElement("Show");
                bool DoctorNameShow = Convert.ToBoolean(dgvpageheader.Rows[7].Cells[7].FormattedValue.ToString());
                ShowDoctorName.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorNameShow)));
                DoctorNameItemNode.AppendChild(ShowDoctorName);



                XmlNode ShowTextDoctorName = doc.CreateElement("ShowText");
                bool DoctorNameShowText = Convert.ToBoolean(dgvpageheader.Rows[7].Cells[8].FormattedValue.ToString());
                ShowTextDoctorName.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorNameShowText)));
                DoctorNameItemNode.AppendChild(ShowTextDoctorName);


                // end doctorname




                //Start DoctorAddress
                XmlNode DoctorAddressItemNode = doc.CreateElement("DoctorAddress");
                PageHeader.AppendChild(DoctorAddressItemNode);

                XmlNode textnodeDoctorAddress = doc.CreateElement("Text");
                //textnodeDoctorAddress.AppendChild(doc.CreateElement(""));
                string DoctorAddresstext = dgvpageheader.Rows[8].Cells[1].Value.ToString();
                textnodeDoctorAddress.AppendChild(doc.CreateTextNode(DoctorAddresstext));
                DoctorAddressItemNode.AppendChild(textnodeDoctorAddress);

                XmlNode DoctorAddressnameNode = doc.CreateElement("Row");
                string DoctorAddressrow = dgvpageheader.Rows[8].Cells[2].Value.ToString();
                DoctorAddressnameNode.AppendChild(doc.CreateTextNode(DoctorAddressrow));
                DoctorAddressItemNode.AppendChild(DoctorAddressnameNode);


                XmlNode DoctorAddresspriceNode = doc.CreateElement("Column");
                string DoctorAddresscolumn = dgvpageheader.Rows[8].Cells[3].Value.ToString();
                DoctorAddresspriceNode.AppendChild(doc.CreateTextNode(DoctorAddresscolumn));
                DoctorAddressItemNode.AppendChild(DoctorAddresspriceNode);


                XmlNode fontnameDoctorAddress = doc.CreateElement("FontName");
                string DoctorAddressFontName = Convert.ToString(dgvpageheader.Rows[8].Cells[4].FormattedValue.ToString());
                if (DoctorAddressFontName == string.Empty)
                {
                    DoctorAddressFontName = "Arial";
                }
                fontnameDoctorAddress.AppendChild(doc.CreateTextNode(DoctorAddressFontName));
                DoctorAddressItemNode.AppendChild(fontnameDoctorAddress);



                XmlNode fontsizeDoctorAddress = doc.CreateElement("FontSize");
                string DoctorAddressFontsize = Convert.ToString(dgvpageheader.Rows[8].Cells[5].FormattedValue.ToString());
                if (DoctorAddressFontsize == string.Empty)
                {
                    DoctorAddressFontsize = "9";
                }
                fontsizeDoctorAddress.AppendChild(doc.CreateTextNode(DoctorAddressFontsize));
                DoctorAddressItemNode.AppendChild(fontsizeDoctorAddress);



                XmlNode FontBoldDoctorAddress = doc.CreateElement("FontBold");
                bool DoctorAddressFontBold = Convert.ToBoolean(dgvpageheader.Rows[8].Cells[6].FormattedValue.ToString());
                FontBoldDoctorAddress.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorAddressFontBold)));
                DoctorAddressItemNode.AppendChild(FontBoldDoctorAddress);


                XmlNode ShowDoctorAddress = doc.CreateElement("Show");
                bool DoctorAddressShow = Convert.ToBoolean(dgvpageheader.Rows[8].Cells[7].FormattedValue.ToString());
                ShowDoctorAddress.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorAddressShow)));
                DoctorAddressItemNode.AppendChild(ShowDoctorAddress);



                XmlNode ShowTextDoctorAddress = doc.CreateElement("ShowText");
                bool DoctorAddressShowText = Convert.ToBoolean(dgvpageheader.Rows[8].Cells[8].FormattedValue.ToString());
                ShowTextDoctorAddress.AppendChild(doc.CreateTextNode(Convert.ToString(DoctorAddressShowText)));
                DoctorAddressItemNode.AppendChild(ShowTextDoctorAddress);

                // end doctoraddress//




                //Start TimeItem
                XmlNode TimeItemNode = doc.CreateElement("TimeItem");
                PageHeader.AppendChild(TimeItemNode);

                XmlNode textnodeTime = doc.CreateElement("Text");
                //textnodeTime.AppendChild(doc.CreateElement(""));
                string Timetext = dgvpageheader.Rows[9].Cells[1].Value.ToString();
                textnodeTime.AppendChild(doc.CreateTextNode(Timetext));
                TimeItemNode.AppendChild(textnodeTime);

                XmlNode TimenameNode = doc.CreateElement("Row");
                string Timerow = dgvpageheader.Rows[9].Cells[2].Value.ToString();
                TimenameNode.AppendChild(doc.CreateTextNode(Timerow));
                TimeItemNode.AppendChild(TimenameNode);


                XmlNode TimepriceNode = doc.CreateElement("Column");
                string Timecolumn = dgvpageheader.Rows[9].Cells[3].Value.ToString();
                TimepriceNode.AppendChild(doc.CreateTextNode(Timecolumn));
                TimeItemNode.AppendChild(TimepriceNode);


                XmlNode fontnameTime = doc.CreateElement("FontName");
                string TimeFontName = Convert.ToString(dgvpageheader.Rows[9].Cells[4].FormattedValue.ToString());
                if (TimeFontName == string.Empty)
                {
                    TimeFontName = "Arial";
                }
                fontnameTime.AppendChild(doc.CreateTextNode(TimeFontName));
                TimeItemNode.AppendChild(fontnameTime);



                XmlNode fontsizeTime = doc.CreateElement("FontSize");
                string TimeFontsize = Convert.ToString(dgvpageheader.Rows[9].Cells[5].FormattedValue.ToString());
                if (TimeFontsize == string.Empty)
                {
                    TimeFontsize = "9";
                }
                fontsizeTime.AppendChild(doc.CreateTextNode(TimeFontsize));
                TimeItemNode.AppendChild(fontsizeTime);



                XmlNode FontBoldTime = doc.CreateElement("FontBold");
                bool TimeFontBold = Convert.ToBoolean(dgvpageheader.Rows[9].Cells[6].FormattedValue.ToString());
                FontBoldTime.AppendChild(doc.CreateTextNode(Convert.ToString(TimeFontBold)));
                TimeItemNode.AppendChild(FontBoldTime);


                XmlNode ShowTime = doc.CreateElement("Show");
                bool TimeShow = Convert.ToBoolean(dgvpageheader.Rows[9].Cells[7].FormattedValue.ToString());
                ShowTime.AppendChild(doc.CreateTextNode(Convert.ToString(TimeShow)));
                TimeItemNode.AppendChild(ShowTime);



                XmlNode ShowTextTime = doc.CreateElement("ShowText");
                bool TimeShowText = Convert.ToBoolean(dgvpageheader.Rows[9].Cells[8].FormattedValue.ToString());
                ShowTextTime.AppendChild(doc.CreateTextNode(Convert.ToString(TimeShowText)));
                TimeItemNode.AppendChild(ShowTextTime);

                //end timeitem//


                //Start VoucherTypeSCAItem
                XmlNode VoucherTypeSCAItemItemNode = doc.CreateElement("VoucherTypeSCAItem");
                PageHeader.AppendChild(VoucherTypeSCAItemItemNode);

                XmlNode textnodeVoucherTypeSCAItem = doc.CreateElement("Text");
                //textnodeVoucherTypeSCAItem.AppendChild(doc.CreateElement(""));
                string VoucherTypeSCAItemtext = dgvpageheader.Rows[10].Cells[1].Value.ToString();
                textnodeVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(VoucherTypeSCAItemtext));
                VoucherTypeSCAItemItemNode.AppendChild(textnodeVoucherTypeSCAItem);

                XmlNode VoucherTypeSCAItemnameNode = doc.CreateElement("Row");
                string VoucherTypeSCAItemrow = dgvpageheader.Rows[10].Cells[2].Value.ToString();
                VoucherTypeSCAItemnameNode.AppendChild(doc.CreateTextNode(VoucherTypeSCAItemrow));
                VoucherTypeSCAItemItemNode.AppendChild(VoucherTypeSCAItemnameNode);


                XmlNode VoucherTypeSCAItempriceNode = doc.CreateElement("Column");
                string VoucherTypeSCAItemcolumn = dgvpageheader.Rows[10].Cells[3].Value.ToString();
                VoucherTypeSCAItempriceNode.AppendChild(doc.CreateTextNode(VoucherTypeSCAItemcolumn));
                VoucherTypeSCAItemItemNode.AppendChild(VoucherTypeSCAItempriceNode);


                XmlNode fontnameVoucherTypeSCAItem = doc.CreateElement("FontName");
                string VoucherTypeSCAItemFontName = Convert.ToString(dgvpageheader.Rows[10].Cells[4].FormattedValue.ToString());
                if (VoucherTypeSCAItemFontName == string.Empty)
                {
                    VoucherTypeSCAItemFontName = "Arial";
                }
                fontnameVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(VoucherTypeSCAItemFontName));
                VoucherTypeSCAItemItemNode.AppendChild(fontnameVoucherTypeSCAItem);



                XmlNode fontsizeVoucherTypeSCAItem = doc.CreateElement("FontSize");
                string VoucherTypeSCAItemFontsize = Convert.ToString(dgvpageheader.Rows[10].Cells[5].FormattedValue.ToString());
                if (VoucherTypeSCAItemFontsize == string.Empty)
                {
                    VoucherTypeSCAItemFontsize = "9";
                }
                fontsizeVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(VoucherTypeSCAItemFontsize));
                VoucherTypeSCAItemItemNode.AppendChild(fontsizeVoucherTypeSCAItem);



                XmlNode FontBoldVoucherTypeSCAItem = doc.CreateElement("FontBold");
                bool VoucherTypeSCAItemFontBold = Convert.ToBoolean(dgvpageheader.Rows[10].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCAItemFontBold)));
                VoucherTypeSCAItemItemNode.AppendChild(FontBoldVoucherTypeSCAItem);


                XmlNode ShowVoucherTypeSCAItem = doc.CreateElement("Show");
                bool VoucherTypeSCAItemShow = Convert.ToBoolean(dgvpageheader.Rows[10].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCAItemShow)));
                VoucherTypeSCAItemItemNode.AppendChild(ShowVoucherTypeSCAItem);



                XmlNode ShowTextVoucherTypeSCAItem = doc.CreateElement("ShowText");
                bool VoucherTypeSCAItemShowText = Convert.ToBoolean(dgvpageheader.Rows[10].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeSCAItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCAItemShowText)));
                VoucherTypeSCAItemItemNode.AppendChild(ShowTextVoucherTypeSCAItem);

                //end VoucherTypeSCAItem//


                //Start VoucherTypeSCRItem

                XmlNode VoucherTypeSCRItemItemNode = doc.CreateElement("VoucherTypeSCRItem");
                PageHeader.AppendChild(VoucherTypeSCRItemItemNode);

                XmlNode textnodeVoucherTypeSCRItem = doc.CreateElement("Text");
                //textnodeVoucherTypeSCRItem.AppendChild(doc.CreateElement(""));
                string VoucherTypeSCRItemtext = dgvpageheader.Rows[11].Cells[1].Value.ToString();
                textnodeVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(VoucherTypeSCRItemtext));
                VoucherTypeSCRItemItemNode.AppendChild(textnodeVoucherTypeSCRItem);

                XmlNode VoucherTypeSCRItemnameNode = doc.CreateElement("Row");
                string VoucherTypeSCRItemrow = dgvpageheader.Rows[11].Cells[2].Value.ToString();
                VoucherTypeSCRItemnameNode.AppendChild(doc.CreateTextNode(VoucherTypeSCRItemrow));
                VoucherTypeSCRItemItemNode.AppendChild(VoucherTypeSCRItemnameNode);


                XmlNode VoucherTypeSCRItempriceNode = doc.CreateElement("Column");
                string VoucherTypeSCRItemcolumn = dgvpageheader.Rows[11].Cells[3].Value.ToString();
                VoucherTypeSCRItempriceNode.AppendChild(doc.CreateTextNode(VoucherTypeSCRItemcolumn));
                VoucherTypeSCRItemItemNode.AppendChild(VoucherTypeSCRItempriceNode);


                XmlNode fontnameVoucherTypeSCRItem = doc.CreateElement("FontName");
                string VoucherTypeSCRItemFontName = Convert.ToString(dgvpageheader.Rows[11].Cells[4].FormattedValue.ToString());
                if (VoucherTypeSCRItemFontName == string.Empty)
                {
                    VoucherTypeSCRItemFontName = "Arial";
                }
                fontnameVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(VoucherTypeSCRItemFontName));
                VoucherTypeSCRItemItemNode.AppendChild(fontnameVoucherTypeSCRItem);



                XmlNode fontsizeVoucherTypeSCRItem = doc.CreateElement("FontSize");
                string VoucherTypeSCRItemFontsize = Convert.ToString(dgvpageheader.Rows[11].Cells[5].FormattedValue.ToString());
                if (VoucherTypeSCRItemFontsize == string.Empty)
                {
                    VoucherTypeSCRItemFontsize = "9";
                }
                fontsizeVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(VoucherTypeSCRItemFontsize));
                VoucherTypeSCRItemItemNode.AppendChild(fontsizeVoucherTypeSCRItem);



                XmlNode FontBoldVoucherTypeSCRItem = doc.CreateElement("FontBold");
                bool VoucherTypeSCRItemFontBold = Convert.ToBoolean(dgvpageheader.Rows[11].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCRItemFontBold)));
                VoucherTypeSCRItemItemNode.AppendChild(FontBoldVoucherTypeSCRItem);


                XmlNode ShowVoucherTypeSCRItem = doc.CreateElement("Show");
                bool VoucherTypeSCRItemShow = Convert.ToBoolean(dgvpageheader.Rows[11].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCRItemShow)));
                VoucherTypeSCRItemItemNode.AppendChild(ShowVoucherTypeSCRItem);



                XmlNode ShowTextVoucherTypeSCRItem = doc.CreateElement("ShowText");
                bool VoucherTypeSCRItemShowText = Convert.ToBoolean(dgvpageheader.Rows[11].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeSCRItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCRItemShowText)));
                VoucherTypeSCRItemItemNode.AppendChild(ShowTextVoucherTypeSCRItem);

                //end VoucherTypeSCRItem//


                //Start VoucherTypeSCSItem
                XmlNode VoucherTypeSCSItemItemNode = doc.CreateElement("VoucherTypeSCSItem");
                PageHeader.AppendChild(VoucherTypeSCSItemItemNode);

                XmlNode textnodeVoucherTypeSCSItem = doc.CreateElement("Text");
                //textnodeVoucherTypeSCSItem.AppendChild(doc.CreateElement(""));
                string VoucherTypeSCSItemtext = dgvpageheader.Rows[12].Cells[1].Value.ToString();
                textnodeVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(VoucherTypeSCSItemtext));
                VoucherTypeSCSItemItemNode.AppendChild(textnodeVoucherTypeSCSItem);

                XmlNode VoucherTypeSCSItemnameNode = doc.CreateElement("Row");
                string VoucherTypeSCSItemrow = dgvpageheader.Rows[12].Cells[2].Value.ToString();
                VoucherTypeSCSItemnameNode.AppendChild(doc.CreateTextNode(VoucherTypeSCSItemrow));
                VoucherTypeSCSItemItemNode.AppendChild(VoucherTypeSCSItemnameNode);


                XmlNode VoucherTypeSCSItempriceNode = doc.CreateElement("Column");
                string VoucherTypeSCSItemcolumn = dgvpageheader.Rows[12].Cells[3].Value.ToString();
                VoucherTypeSCSItempriceNode.AppendChild(doc.CreateTextNode(VoucherTypeSCSItemcolumn));
                VoucherTypeSCSItemItemNode.AppendChild(VoucherTypeSCSItempriceNode);


                XmlNode fontnameVoucherTypeSCSItem = doc.CreateElement("FontName");
                string VoucherTypeSCSItemFontName = Convert.ToString(dgvpageheader.Rows[12].Cells[4].FormattedValue.ToString());
                if (VoucherTypeSCSItemFontName == string.Empty)
                {
                    VoucherTypeSCSItemFontName = "Arial";
                }
                fontnameVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(VoucherTypeSCSItemFontName));
                VoucherTypeSCSItemItemNode.AppendChild(fontnameVoucherTypeSCSItem);



                XmlNode fontsizeVoucherTypeSCSItem = doc.CreateElement("FontSize");
                string VoucherTypeSCSItemFontsize = Convert.ToString(dgvpageheader.Rows[12].Cells[5].FormattedValue.ToString());
                if (VoucherTypeSCSItemFontsize == string.Empty)
                {
                    VoucherTypeSCSItemFontsize = "9";
                }
                fontsizeVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(VoucherTypeSCSItemFontsize));
                VoucherTypeSCSItemItemNode.AppendChild(fontsizeVoucherTypeSCSItem);



                XmlNode FontBoldVoucherTypeSCSItem = doc.CreateElement("FontBold");
                bool VoucherTypeSCSItemFontBold = Convert.ToBoolean(dgvpageheader.Rows[12].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCSItemFontBold)));
                VoucherTypeSCSItemItemNode.AppendChild(FontBoldVoucherTypeSCSItem);


                XmlNode ShowVoucherTypeSCSItem = doc.CreateElement("Show");
                bool VoucherTypeSCSItemShow = Convert.ToBoolean(dgvpageheader.Rows[12].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCSItemShow)));
                VoucherTypeSCSItemItemNode.AppendChild(ShowVoucherTypeSCSItem);



                XmlNode ShowTextVoucherTypeSCSItem = doc.CreateElement("ShowText");
                bool VoucherTypeSCSItemShowText = Convert.ToBoolean(dgvpageheader.Rows[12].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeSCSItem.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSCSItemShowText)));
                VoucherTypeSCSItemItemNode.AppendChild(ShowTextVoucherTypeSCSItem);

                //end VoucherTypeSCSItem//


                //Start VoucherTypeSVUItem
                XmlNode VoucherTypeSVUItemNode = doc.CreateElement("VoucherTypeSVUItem");
                PageHeader.AppendChild(VoucherTypeSVUItemNode);

                XmlNode textnodeVoucherTypeSVU = doc.CreateElement("Text");
                //textnodeVoucherTypeSVU.AppendChild(doc.CreateElement(""));
                string VoucherTypeSVUtext = dgvpageheader.Rows[13].Cells[1].Value.ToString();
                textnodeVoucherTypeSVU.AppendChild(doc.CreateTextNode(VoucherTypeSVUtext));
                VoucherTypeSVUItemNode.AppendChild(textnodeVoucherTypeSVU);

                XmlNode VoucherTypeSVUnameNode = doc.CreateElement("Row");
                string VoucherTypeSVUrow = dgvpageheader.Rows[13].Cells[2].Value.ToString();
                VoucherTypeSVUnameNode.AppendChild(doc.CreateTextNode(VoucherTypeSVUrow));
                VoucherTypeSVUItemNode.AppendChild(VoucherTypeSVUnameNode);


                XmlNode VoucherTypeSVUpriceNode = doc.CreateElement("Column");
                string VoucherTypeSVUcolumn = dgvpageheader.Rows[13].Cells[3].Value.ToString();
                VoucherTypeSVUpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeSVUcolumn));
                VoucherTypeSVUItemNode.AppendChild(VoucherTypeSVUpriceNode);


                XmlNode fontnameVoucherTypeSVU = doc.CreateElement("FontName");
                string VoucherTypeSVUFontName = Convert.ToString(dgvpageheader.Rows[13].Cells[4].FormattedValue.ToString());
                if (VoucherTypeSVUFontName == string.Empty)
                {
                    VoucherTypeSVUFontName = "Arial";
                }
                fontnameVoucherTypeSVU.AppendChild(doc.CreateTextNode(VoucherTypeSVUFontName));
                VoucherTypeSVUItemNode.AppendChild(fontnameVoucherTypeSVU);



                XmlNode fontsizeVoucherTypeSVU = doc.CreateElement("FontSize");
                string VoucherTypeSVUFontsize = Convert.ToString(dgvpageheader.Rows[13].Cells[5].FormattedValue.ToString());
                if (VoucherTypeSVUFontsize == string.Empty)
                {
                    VoucherTypeSVUFontsize = "9";
                }
                fontsizeVoucherTypeSVU.AppendChild(doc.CreateTextNode(VoucherTypeSVUFontsize));
                VoucherTypeSVUItemNode.AppendChild(fontsizeVoucherTypeSVU);



                XmlNode FontBoldVoucherTypeSVU = doc.CreateElement("FontBold");
                bool VoucherTypeSVUFontBold = Convert.ToBoolean(dgvpageheader.Rows[13].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeSVU.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSVUFontBold)));
                VoucherTypeSVUItemNode.AppendChild(FontBoldVoucherTypeSVU);


                XmlNode ShowVoucherTypeSVU = doc.CreateElement("Show");
                bool VoucherTypeSVUShow = Convert.ToBoolean(dgvpageheader.Rows[13].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeSVU.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSVUShow)));
                VoucherTypeSVUItemNode.AppendChild(ShowVoucherTypeSVU);



                XmlNode ShowTextVoucherTypeSVU = doc.CreateElement("ShowText");
                bool VoucherTypeSVUShowText = Convert.ToBoolean(dgvpageheader.Rows[13].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeSVU.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeSVUShowText)));
                VoucherTypeSVUItemNode.AppendChild(ShowTextVoucherTypeSVU);

                //end VoucherTypeSVUItem//



                //Start VoucherTypeDebitNoteItem
                XmlNode VoucherTypeDebitNoteItemNode = doc.CreateElement("VoucherTypeDebitNoteItem");
                PageHeader.AppendChild(VoucherTypeDebitNoteItemNode);

                XmlNode textnodeVoucherTypeDebitNote = doc.CreateElement("Text");
                //textnodeVoucherTypeDebitNote.AppendChild(doc.CreateElement(""));
                string VoucherTypeDebitNotetext = dgvpageheader.Rows[14].Cells[1].Value.ToString();
                textnodeVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(VoucherTypeDebitNotetext));
                VoucherTypeDebitNoteItemNode.AppendChild(textnodeVoucherTypeDebitNote);

                XmlNode VoucherTypeDebitNotenameNode = doc.CreateElement("Row");
                string VoucherTypeDebitNoterow = dgvpageheader.Rows[14].Cells[2].Value.ToString();
                VoucherTypeDebitNotenameNode.AppendChild(doc.CreateTextNode(VoucherTypeDebitNoterow));
                VoucherTypeDebitNoteItemNode.AppendChild(VoucherTypeDebitNotenameNode);


                XmlNode VoucherTypeDebitNotepriceNode = doc.CreateElement("Column");
                string VoucherTypeDebitNotecolumn = dgvpageheader.Rows[14].Cells[3].Value.ToString();
                VoucherTypeDebitNotepriceNode.AppendChild(doc.CreateTextNode(VoucherTypeDebitNotecolumn));
                VoucherTypeDebitNoteItemNode.AppendChild(VoucherTypeDebitNotepriceNode);


                XmlNode fontnameVoucherTypeDebitNote = doc.CreateElement("FontName");
                string VoucherTypeDebitNoteFontName = Convert.ToString(dgvpageheader.Rows[14].Cells[4].FormattedValue.ToString());
                if (VoucherTypeDebitNoteFontName == string.Empty)
                {
                    VoucherTypeDebitNoteFontName = "Arial";
                }
                fontnameVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(VoucherTypeDebitNoteFontName));
                VoucherTypeDebitNoteItemNode.AppendChild(fontnameVoucherTypeDebitNote);



                XmlNode fontsizeVoucherTypeDebitNote = doc.CreateElement("FontSize");
                string VoucherTypeDebitNoteFontsize = Convert.ToString(dgvpageheader.Rows[14].Cells[5].FormattedValue.ToString());
                if (VoucherTypeDebitNoteFontsize == string.Empty)
                {
                    VoucherTypeDebitNoteFontsize = "9";
                }
                fontsizeVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(VoucherTypeDebitNoteFontsize));
                VoucherTypeDebitNoteItemNode.AppendChild(fontsizeVoucherTypeDebitNote);



                XmlNode FontBoldVoucherTypeDebitNote = doc.CreateElement("FontBold");
                bool VoucherTypeDebitNoteFontBold = Convert.ToBoolean(dgvpageheader.Rows[14].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeDebitNoteFontBold)));
                VoucherTypeDebitNoteItemNode.AppendChild(FontBoldVoucherTypeDebitNote);


                XmlNode ShowVoucherTypeDebitNote = doc.CreateElement("Show");
                bool VoucherTypeDebitNoteShow = Convert.ToBoolean(dgvpageheader.Rows[14].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeDebitNoteShow)));
                VoucherTypeDebitNoteItemNode.AppendChild(ShowVoucherTypeDebitNote);



                XmlNode ShowTextVoucherTypeDebitNote = doc.CreateElement("ShowText");
                bool VoucherTypeDebitNoteShowText = Convert.ToBoolean(dgvpageheader.Rows[14].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeDebitNoteShowText)));
                VoucherTypeDebitNoteItemNode.AppendChild(ShowTextVoucherTypeDebitNote);
                //end VoucherTypeDebitNoteItem//



                //Start VoucherTypeCreditNoteItem
                XmlNode VoucherTypeCreditNoteItemNode = doc.CreateElement("VoucherTypeCreditNoteItem");
                PageHeader.AppendChild(VoucherTypeCreditNoteItemNode);

                XmlNode textnodeVoucherTypeCreditNote = doc.CreateElement("Text");
                //textnodeVoucherTypeCreditNote.AppendChild(doc.CreateElement(""));
                string VoucherTypeCreditNotetext = dgvpageheader.Rows[15].Cells[1].Value.ToString();
                textnodeVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(VoucherTypeCreditNotetext));
                VoucherTypeCreditNoteItemNode.AppendChild(textnodeVoucherTypeCreditNote);

                XmlNode VoucherTypeCreditNotenameNode = doc.CreateElement("Row");
                string VoucherTypeCreditNoterow = dgvpageheader.Rows[15].Cells[2].Value.ToString();
                VoucherTypeCreditNotenameNode.AppendChild(doc.CreateTextNode(VoucherTypeCreditNoterow));
                VoucherTypeCreditNoteItemNode.AppendChild(VoucherTypeCreditNotenameNode);


                XmlNode VoucherTypeCreditNotepriceNode = doc.CreateElement("Column");
                string VoucherTypeCreditNotecolumn = dgvpageheader.Rows[15].Cells[3].Value.ToString();
                VoucherTypeCreditNotepriceNode.AppendChild(doc.CreateTextNode(VoucherTypeCreditNotecolumn));
                VoucherTypeCreditNoteItemNode.AppendChild(VoucherTypeCreditNotepriceNode);


                XmlNode fontnameVoucherTypeCreditNote = doc.CreateElement("FontName");
                string VoucherTypeCreditNoteFontName = Convert.ToString(dgvpageheader.Rows[15].Cells[4].FormattedValue.ToString());
                if (VoucherTypeCreditNoteFontName == string.Empty)
                {
                    VoucherTypeCreditNoteFontName = "Arial";
                }
                fontnameVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(VoucherTypeCreditNoteFontName));
                VoucherTypeCreditNoteItemNode.AppendChild(fontnameVoucherTypeCreditNote);



                XmlNode fontsizeVoucherTypeCreditNote = doc.CreateElement("FontSize");
                string VoucherTypeCreditNoteFontsize = Convert.ToString(dgvpageheader.Rows[15].Cells[5].FormattedValue.ToString());
                if (VoucherTypeCreditNoteFontsize == string.Empty)
                {
                    VoucherTypeCreditNoteFontsize = "9";
                }
                fontsizeVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(VoucherTypeCreditNoteFontsize));
                VoucherTypeCreditNoteItemNode.AppendChild(fontsizeVoucherTypeCreditNote);



                XmlNode FontBoldVoucherTypeCreditNote = doc.CreateElement("FontBold");
                bool VoucherTypeCreditNoteFontBold = Convert.ToBoolean(dgvpageheader.Rows[15].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCreditNoteFontBold)));
                VoucherTypeCreditNoteItemNode.AppendChild(FontBoldVoucherTypeCreditNote);


                XmlNode ShowVoucherTypeCreditNote = doc.CreateElement("Show");
                bool VoucherTypeCreditNoteShow = Convert.ToBoolean(dgvpageheader.Rows[15].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCreditNoteShow)));
                VoucherTypeCreditNoteItemNode.AppendChild(ShowVoucherTypeCreditNote);



                XmlNode ShowTextVoucherTypeCreditNote = doc.CreateElement("ShowText");
                bool VoucherTypeCreditNoteShowText = Convert.ToBoolean(dgvpageheader.Rows[15].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCreditNoteShowText)));
                VoucherTypeCreditNoteItemNode.AppendChild(ShowTextVoucherTypeCreditNote);
                //end VoucherTypeCreditNoteItem//



                //Start VoucherTypeStockOUTItem
                XmlNode VoucherTypeStockOUTItemNode = doc.CreateElement("VoucherTypeStockOUTItem");
                PageHeader.AppendChild(VoucherTypeStockOUTItemNode);

                XmlNode textnodeVoucherTypeStockOUT = doc.CreateElement("Text");
                //textnodeVoucherTypeStockOUT.AppendChild(doc.CreateElement(""));
                string VoucherTypeStockOUTtext = dgvpageheader.Rows[16].Cells[1].Value.ToString();
                textnodeVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(VoucherTypeStockOUTtext));
                VoucherTypeStockOUTItemNode.AppendChild(textnodeVoucherTypeStockOUT);

                XmlNode VoucherTypeStockOUTnameNode = doc.CreateElement("Row");
                string VoucherTypeStockOUTrow = dgvpageheader.Rows[16].Cells[2].Value.ToString();
                VoucherTypeStockOUTnameNode.AppendChild(doc.CreateTextNode(VoucherTypeStockOUTrow));
                VoucherTypeStockOUTItemNode.AppendChild(VoucherTypeStockOUTnameNode);


                XmlNode VoucherTypeStockOUTpriceNode = doc.CreateElement("Column");
                string VoucherTypeStockOUTcolumn = dgvpageheader.Rows[16].Cells[3].Value.ToString();
                VoucherTypeStockOUTpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeStockOUTcolumn));
                VoucherTypeStockOUTItemNode.AppendChild(VoucherTypeStockOUTpriceNode);


                XmlNode fontnameVoucherTypeStockOUT = doc.CreateElement("FontName");
                string VoucherTypeStockOUTFontName = Convert.ToString(dgvpageheader.Rows[16].Cells[4].FormattedValue.ToString());
                if (VoucherTypeStockOUTFontName == string.Empty)
                {
                    VoucherTypeStockOUTFontName = "Arial";
                }
                fontnameVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(VoucherTypeStockOUTFontName));
                VoucherTypeStockOUTItemNode.AppendChild(fontnameVoucherTypeStockOUT);



                XmlNode fontsizeVoucherTypeStockOUT = doc.CreateElement("FontSize");
                string VoucherTypeStockOUTFontsize = Convert.ToString(dgvpageheader.Rows[16].Cells[5].FormattedValue.ToString());
                if (VoucherTypeStockOUTFontsize == string.Empty)
                {
                    VoucherTypeStockOUTFontsize = "9";
                }
                fontsizeVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(VoucherTypeStockOUTFontsize));
                VoucherTypeStockOUTItemNode.AppendChild(fontsizeVoucherTypeStockOUT);



                XmlNode FontBoldVoucherTypeStockOUT = doc.CreateElement("FontBold");
                bool VoucherTypeStockOUTFontBold = Convert.ToBoolean(dgvpageheader.Rows[16].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockOUTFontBold)));
                VoucherTypeStockOUTItemNode.AppendChild(FontBoldVoucherTypeStockOUT);


                XmlNode ShowVoucherTypeStockOUT = doc.CreateElement("Show");
                bool VoucherTypeStockOUTShow = Convert.ToBoolean(dgvpageheader.Rows[16].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockOUTShow)));
                VoucherTypeStockOUTItemNode.AppendChild(ShowVoucherTypeStockOUT);



                XmlNode ShowTextVoucherTypeStockOUT = doc.CreateElement("ShowText");
                bool VoucherTypeStockOUTShowText = Convert.ToBoolean(dgvpageheader.Rows[16].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeStockOUT.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockOUTShowText)));
                VoucherTypeStockOUTItemNode.AppendChild(ShowTextVoucherTypeStockOUT);

                //end VoucherTypeStockOUTItem//


                //Start VoucherTypeStockINItem//

                XmlNode VoucherTypeStockINItemNode = doc.CreateElement("VoucherTypeStockINItem");
                PageHeader.AppendChild(VoucherTypeStockINItemNode);

                XmlNode textnodeVoucherTypeStockIN = doc.CreateElement("Text");
                //textnodeVoucherTypeStockIN.AppendChild(doc.CreateElement(""));
                string VoucherTypeStockINtext = dgvpageheader.Rows[17].Cells[1].Value.ToString();
                textnodeVoucherTypeStockIN.AppendChild(doc.CreateTextNode(VoucherTypeStockINtext));
                VoucherTypeStockINItemNode.AppendChild(textnodeVoucherTypeStockIN);

                XmlNode VoucherTypeStockINnameNode = doc.CreateElement("Row");
                string VoucherTypeStockINrow = dgvpageheader.Rows[17].Cells[2].Value.ToString();
                VoucherTypeStockINnameNode.AppendChild(doc.CreateTextNode(VoucherTypeStockINrow));
                VoucherTypeStockINItemNode.AppendChild(VoucherTypeStockINnameNode);


                XmlNode VoucherTypeStockINpriceNode = doc.CreateElement("Column");
                string VoucherTypeStockINcolumn = dgvpageheader.Rows[17].Cells[3].Value.ToString();
                VoucherTypeStockINpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeStockINcolumn));
                VoucherTypeStockINItemNode.AppendChild(VoucherTypeStockINpriceNode);


                XmlNode fontnameVoucherTypeStockIN = doc.CreateElement("FontName");
                string VoucherTypeStockINFontName = Convert.ToString(dgvpageheader.Rows[17].Cells[4].FormattedValue.ToString());
                if (VoucherTypeStockINFontName == string.Empty)
                {
                    VoucherTypeStockINFontName = "Arial";
                }
                fontnameVoucherTypeStockIN.AppendChild(doc.CreateTextNode(VoucherTypeStockINFontName));
                VoucherTypeStockINItemNode.AppendChild(fontnameVoucherTypeStockIN);



                XmlNode fontsizeVoucherTypeStockIN = doc.CreateElement("FontSize");
                string VoucherTypeStockINFontsize = Convert.ToString(dgvpageheader.Rows[17].Cells[5].FormattedValue.ToString());
                if (VoucherTypeStockINFontsize == string.Empty)
                {
                    VoucherTypeStockINFontsize = "9";
                }
                fontsizeVoucherTypeStockIN.AppendChild(doc.CreateTextNode(VoucherTypeStockINFontsize));
                VoucherTypeStockINItemNode.AppendChild(fontsizeVoucherTypeStockIN);



                XmlNode FontBoldVoucherTypeStockIN = doc.CreateElement("FontBold");
                bool VoucherTypeStockINFontBold = Convert.ToBoolean(dgvpageheader.Rows[17].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeStockIN.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockINFontBold)));
                VoucherTypeStockINItemNode.AppendChild(FontBoldVoucherTypeStockIN);


                XmlNode ShowVoucherTypeStockIN = doc.CreateElement("Show");
                bool VoucherTypeStockINShow = Convert.ToBoolean(dgvpageheader.Rows[17].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeStockIN.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockINShow)));
                VoucherTypeStockINItemNode.AppendChild(ShowVoucherTypeStockIN);



                XmlNode ShowTextVoucherTypeStockIN = doc.CreateElement("ShowText");
                bool VoucherTypeStockINShowText = Convert.ToBoolean(dgvpageheader.Rows[17].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeStockIN.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeStockINShowText)));
                VoucherTypeStockINItemNode.AppendChild(ShowTextVoucherTypeStockIN);



                //end VoucherTypeStockINItem//




                //Start VoucherTypeCashReceiptItem
                XmlNode VoucherTypeCashReceiptItemNode = doc.CreateElement("VoucherTypeCashReceiptItem");
                PageHeader.AppendChild(VoucherTypeCashReceiptItemNode);

                XmlNode textnodeVoucherTypeCashReceipt = doc.CreateElement("Text");
                //textnodeVoucherTypeCashReceipt.AppendChild(doc.CreateElement(""));
                string VoucherTypeCashReceipttext = dgvpageheader.Rows[18].Cells[1].Value.ToString();
                textnodeVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(VoucherTypeCashReceipttext));
                VoucherTypeCashReceiptItemNode.AppendChild(textnodeVoucherTypeCashReceipt);

                XmlNode VoucherTypeCashReceiptnameNode = doc.CreateElement("Row");
                string VoucherTypeCashReceiptrow = dgvpageheader.Rows[18].Cells[2].Value.ToString();
                VoucherTypeCashReceiptnameNode.AppendChild(doc.CreateTextNode(VoucherTypeCashReceiptrow));
                VoucherTypeCashReceiptItemNode.AppendChild(VoucherTypeCashReceiptnameNode);


                XmlNode VoucherTypeCashReceiptpriceNode = doc.CreateElement("Column");
                string VoucherTypeCashReceiptcolumn = dgvpageheader.Rows[18].Cells[3].Value.ToString();
                VoucherTypeCashReceiptpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeCashReceiptcolumn));
                VoucherTypeCashReceiptItemNode.AppendChild(VoucherTypeCashReceiptpriceNode);


                XmlNode fontnameVoucherTypeCashReceipt = doc.CreateElement("FontName");
                string VoucherTypeCashReceiptFontName = Convert.ToString(dgvpageheader.Rows[18].Cells[4].FormattedValue.ToString());
                if (VoucherTypeCashReceiptFontName == string.Empty)
                {
                    VoucherTypeCashReceiptFontName = "Arial";
                }
                fontnameVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(VoucherTypeCashReceiptFontName));
                VoucherTypeCashReceiptItemNode.AppendChild(fontnameVoucherTypeCashReceipt);



                XmlNode fontsizeVoucherTypeCashReceipt = doc.CreateElement("FontSize");
                string VoucherTypeCashReceiptFontsize = Convert.ToString(dgvpageheader.Rows[18].Cells[5].FormattedValue.ToString());
                if (VoucherTypeCashReceiptFontsize == string.Empty)
                {
                    VoucherTypeCashReceiptFontsize = "Arial";
                }
                fontsizeVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(VoucherTypeCashReceiptFontsize));
                VoucherTypeCashReceiptItemNode.AppendChild(fontsizeVoucherTypeCashReceipt);



                XmlNode FontBoldVoucherTypeCashReceipt = doc.CreateElement("FontBold");
                bool VoucherTypeCashReceiptFontBold = Convert.ToBoolean(dgvpageheader.Rows[18].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashReceiptFontBold)));
                VoucherTypeCashReceiptItemNode.AppendChild(FontBoldVoucherTypeCashReceipt);


                XmlNode ShowVoucherTypeCashReceipt = doc.CreateElement("Show");
                bool VoucherTypeCashReceiptShow = Convert.ToBoolean(dgvpageheader.Rows[18].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashReceiptShow)));
                VoucherTypeCashReceiptItemNode.AppendChild(ShowVoucherTypeCashReceipt);



                XmlNode ShowTextVoucherTypeCashReceipt = doc.CreateElement("ShowText");
                bool VoucherTypeCashReceiptShowText = Convert.ToBoolean(dgvpageheader.Rows[18].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeCashReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashReceiptShowText)));
                VoucherTypeCashReceiptItemNode.AppendChild(ShowTextVoucherTypeCashReceipt);

                //end VoucherTypeCashReceiptItem//


                //Start VoucherTypeCashPaymentItem

                XmlNode VoucherTypeCashPaymentItemNode = doc.CreateElement("VoucherTypeCashPaymentItem");
                PageHeader.AppendChild(VoucherTypeCashPaymentItemNode);

                XmlNode textnodeVoucherTypeCashPayment = doc.CreateElement("Text");
                //textnodeVoucherTypeCashPayment.AppendChild(doc.CreateElement(""));
                string VoucherTypeCashPaymenttext = dgvpageheader.Rows[19].Cells[1].Value.ToString();
                textnodeVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(VoucherTypeCashPaymenttext));
                VoucherTypeCashPaymentItemNode.AppendChild(textnodeVoucherTypeCashPayment);

                XmlNode VoucherTypeCashPaymentnameNode = doc.CreateElement("Row");
                string VoucherTypeCashPaymentrow = dgvpageheader.Rows[19].Cells[2].Value.ToString();
                VoucherTypeCashPaymentnameNode.AppendChild(doc.CreateTextNode(VoucherTypeCashPaymentrow));
                VoucherTypeCashPaymentItemNode.AppendChild(VoucherTypeCashPaymentnameNode);


                XmlNode VoucherTypeCashPaymentpriceNode = doc.CreateElement("Column");
                string VoucherTypeCashPaymentcolumn = dgvpageheader.Rows[19].Cells[3].Value.ToString();
                VoucherTypeCashPaymentpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeCashPaymentcolumn));
                VoucherTypeCashPaymentItemNode.AppendChild(VoucherTypeCashPaymentpriceNode);


                XmlNode fontnameVoucherTypeCashPayment = doc.CreateElement("FontName");
                string VoucherTypeCashPaymentFontName = Convert.ToString(dgvpageheader.Rows[19].Cells[4].FormattedValue.ToString());
                if (VoucherTypeCashPaymentFontName == string.Empty)
                {
                    VoucherTypeCashPaymentFontName = "Arial";
                }
                fontnameVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(VoucherTypeCashPaymentFontName));
                VoucherTypeCashPaymentItemNode.AppendChild(fontnameVoucherTypeCashPayment);



                XmlNode fontsizeVoucherTypeCashPayment = doc.CreateElement("FontSize");
                string VoucherTypeCashPaymentFontsize = Convert.ToString(dgvpageheader.Rows[19].Cells[5].FormattedValue.ToString());
                if (VoucherTypeCashPaymentFontsize == string.Empty)
                {
                    VoucherTypeCashPaymentFontsize = "9";
                }
                fontsizeVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(VoucherTypeCashPaymentFontsize));
                VoucherTypeCashPaymentItemNode.AppendChild(fontsizeVoucherTypeCashPayment);



                XmlNode FontBoldVoucherTypeCashPayment = doc.CreateElement("FontBold");
                bool VoucherTypeCashPaymentFontBold = Convert.ToBoolean(dgvpageheader.Rows[19].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashPaymentFontBold)));
                VoucherTypeCashPaymentItemNode.AppendChild(FontBoldVoucherTypeCashPayment);


                XmlNode ShowVoucherTypeCashPayment = doc.CreateElement("Show");
                bool VoucherTypeCashPaymentShow = Convert.ToBoolean(dgvpageheader.Rows[19].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashPaymentShow)));
                VoucherTypeCashPaymentItemNode.AppendChild(ShowVoucherTypeCashPayment);



                XmlNode ShowTextVoucherTypeCashPayment = doc.CreateElement("ShowText");
                bool VoucherTypeCashPaymentShowText = Convert.ToBoolean(dgvpageheader.Rows[19].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeCashPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeCashPaymentShowText)));
                VoucherTypeCashPaymentItemNode.AppendChild(ShowTextVoucherTypeCashPayment);


                //end VoucherTypeCashPaymentItem//


                //Start VoucherTypeBankReceiptItem
                XmlNode VoucherTypeBankReceiptItemNode = doc.CreateElement("VoucherTypeBankReceiptItem");
                PageHeader.AppendChild(VoucherTypeBankReceiptItemNode);

                XmlNode textnodeVoucherTypeBankReceipt = doc.CreateElement("Text");
                //textnodeVoucherTypeBankReceipt.AppendChild(doc.CreateElement(""));
                string VoucherTypeBankReceipttext = dgvpageheader.Rows[20].Cells[1].Value.ToString();
                textnodeVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(VoucherTypeBankReceipttext));
                VoucherTypeBankReceiptItemNode.AppendChild(textnodeVoucherTypeBankReceipt);

                XmlNode VoucherTypeBankReceiptnameNode = doc.CreateElement("Row");
                string VoucherTypeBankReceiptrow = dgvpageheader.Rows[20].Cells[2].Value.ToString();
                VoucherTypeBankReceiptnameNode.AppendChild(doc.CreateTextNode(VoucherTypeBankReceiptrow));
                VoucherTypeBankReceiptItemNode.AppendChild(VoucherTypeBankReceiptnameNode);


                XmlNode VoucherTypeBankReceiptpriceNode = doc.CreateElement("Column");
                string VoucherTypeBankReceiptcolumn = dgvpageheader.Rows[20].Cells[3].Value.ToString();
                VoucherTypeBankReceiptpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeBankReceiptcolumn));
                VoucherTypeBankReceiptItemNode.AppendChild(VoucherTypeBankReceiptpriceNode);


                XmlNode fontnameVoucherTypeBankReceipt = doc.CreateElement("FontName");
                string VoucherTypeBankReceiptFontName = Convert.ToString(dgvpageheader.Rows[20].Cells[4].FormattedValue.ToString());
                if (VoucherTypeBankReceiptFontName == string.Empty)
                {
                    VoucherTypeBankReceiptFontName = "9";
                }
                fontnameVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(VoucherTypeBankReceiptFontName));
                VoucherTypeBankReceiptItemNode.AppendChild(fontnameVoucherTypeBankReceipt);



                XmlNode fontsizeVoucherTypeBankReceipt = doc.CreateElement("FontSize");
                string VoucherTypeBankReceiptFontsize = Convert.ToString(dgvpageheader.Rows[20].Cells[5].FormattedValue.ToString());
                fontsizeVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(VoucherTypeBankReceiptFontsize));
                VoucherTypeBankReceiptItemNode.AppendChild(fontsizeVoucherTypeBankReceipt);



                XmlNode FontBoldVoucherTypeBankReceipt = doc.CreateElement("FontBold");
                bool VoucherTypeBankReceiptFontBold = Convert.ToBoolean(dgvpageheader.Rows[20].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankReceiptFontBold)));
                VoucherTypeBankReceiptItemNode.AppendChild(FontBoldVoucherTypeBankReceipt);


                XmlNode ShowVoucherTypeBankReceipt = doc.CreateElement("Show");
                bool VoucherTypeBankReceiptShow = Convert.ToBoolean(dgvpageheader.Rows[20].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankReceiptShow)));
                VoucherTypeBankReceiptItemNode.AppendChild(ShowVoucherTypeBankReceipt);



                XmlNode ShowTextVoucherTypeBankReceipt = doc.CreateElement("ShowText");
                bool VoucherTypeBankReceiptShowText = Convert.ToBoolean(dgvpageheader.Rows[20].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeBankReceipt.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankReceiptShowText)));
                VoucherTypeBankReceiptItemNode.AppendChild(ShowTextVoucherTypeBankReceipt);


                //end VoucherTypeBankReceiptItem//



                //Start VoucherTypeBankPaymentItem
                XmlNode VoucherTypeBankPaymentItemNode = doc.CreateElement("VoucherTypeBankPaymentItem");
                PageHeader.AppendChild(VoucherTypeBankPaymentItemNode);

                XmlNode textnodeVoucherTypeBankPayment = doc.CreateElement("Text");
                //textnodeVoucherTypeBankPayment.AppendChild(doc.CreateElement(""));
                string VoucherTypeBankPaymenttext = dgvpageheader.Rows[21].Cells[1].Value.ToString();
                textnodeVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(VoucherTypeBankPaymenttext));
                VoucherTypeBankPaymentItemNode.AppendChild(textnodeVoucherTypeBankPayment);

                XmlNode VoucherTypeBankPaymentnameNode = doc.CreateElement("Row");
                string VoucherTypeBankPaymentrow = dgvpageheader.Rows[21].Cells[2].Value.ToString();
                VoucherTypeBankPaymentnameNode.AppendChild(doc.CreateTextNode(VoucherTypeBankPaymentrow));
                VoucherTypeBankPaymentItemNode.AppendChild(VoucherTypeBankPaymentnameNode);


                XmlNode VoucherTypeBankPaymentpriceNode = doc.CreateElement("Column");
                string VoucherTypeBankPaymentcolumn = dgvpageheader.Rows[21].Cells[3].Value.ToString();
                VoucherTypeBankPaymentpriceNode.AppendChild(doc.CreateTextNode(VoucherTypeBankPaymentcolumn));
                VoucherTypeBankPaymentItemNode.AppendChild(VoucherTypeBankPaymentpriceNode);


                XmlNode fontnameVoucherTypeBankPayment = doc.CreateElement("FontName");
                string VoucherTypeBankPaymentFontName = Convert.ToString(dgvpageheader.Rows[21].Cells[4].FormattedValue.ToString());
                if (VoucherTypeBankPaymentFontName == string.Empty)
                {
                    VoucherTypeBankPaymentFontName = "Arial";
                }
                fontnameVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(VoucherTypeBankPaymentFontName));
                VoucherTypeBankPaymentItemNode.AppendChild(fontnameVoucherTypeBankPayment);



                XmlNode fontsizeVoucherTypeBankPayment = doc.CreateElement("FontSize");
                string VoucherTypeBankPaymentFontsize = Convert.ToString(dgvpageheader.Rows[21].Cells[5].FormattedValue.ToString());
                if (VoucherTypeBankPaymentFontsize == string.Empty)
                {
                    VoucherTypeBankPaymentFontsize = "9";
                }
                fontsizeVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(VoucherTypeBankPaymentFontsize));
                VoucherTypeBankPaymentItemNode.AppendChild(fontsizeVoucherTypeBankPayment);



                XmlNode FontBoldVoucherTypeBankPayment = doc.CreateElement("FontBold");
                bool VoucherTypeBankPaymentFontBold = Convert.ToBoolean(dgvpageheader.Rows[21].Cells[6].FormattedValue.ToString());
                FontBoldVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankPaymentFontBold)));
                VoucherTypeBankPaymentItemNode.AppendChild(FontBoldVoucherTypeBankPayment);


                XmlNode ShowVoucherTypeBankPayment = doc.CreateElement("Show");
                bool VoucherTypeBankPaymentShow = Convert.ToBoolean(dgvpageheader.Rows[21].Cells[7].FormattedValue.ToString());
                ShowVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankPaymentShow)));
                VoucherTypeBankPaymentItemNode.AppendChild(ShowVoucherTypeBankPayment);



                XmlNode ShowTextVoucherTypeBankPayment = doc.CreateElement("ShowText");
                bool VoucherTypeBankPaymentShowText = Convert.ToBoolean(dgvpageheader.Rows[21].Cells[8].FormattedValue.ToString());
                ShowTextVoucherTypeBankPayment.AppendChild(doc.CreateTextNode(Convert.ToString(VoucherTypeBankPaymentShowText)));
                VoucherTypeBankPaymentItemNode.AppendChild(ShowTextVoucherTypeBankPayment);
                //end VoucherTypeBankPaymentItem//


                //Start DateItem
                XmlNode DateItemNode = doc.CreateElement("DateItem");
                PageHeader.AppendChild(DateItemNode);

                XmlNode textnodeDateItem = doc.CreateElement("Text");
                //textnodeDateItem.AppendChild(doc.CreateElement(""));
                string DateItemtext = dgvpageheader.Rows[22].Cells[1].Value.ToString();
                textnodeDateItem.AppendChild(doc.CreateTextNode(DateItemtext));
                DateItemNode.AppendChild(textnodeDateItem);

                XmlNode DateItemnameNode = doc.CreateElement("Row");
                string DateItemrow = dgvpageheader.Rows[22].Cells[2].Value.ToString();
                DateItemnameNode.AppendChild(doc.CreateTextNode(DateItemrow));
                DateItemNode.AppendChild(DateItemnameNode);


                XmlNode DateItempriceNode = doc.CreateElement("Column");
                string DateItemcolumn = dgvpageheader.Rows[22].Cells[3].Value.ToString();
                DateItempriceNode.AppendChild(doc.CreateTextNode(DateItemcolumn));
                DateItemNode.AppendChild(DateItempriceNode);


                XmlNode fontnameDateItem = doc.CreateElement("FontName");
                string DateItemFontName = Convert.ToString(dgvpageheader.Rows[22].Cells[4].FormattedValue.ToString());
                if (DateItemFontName == string.Empty)
                {
                    DateItemFontName = "Arial";
                }
                fontnameDateItem.AppendChild(doc.CreateTextNode(DateItemFontName));
                DateItemNode.AppendChild(fontnameDateItem);



                XmlNode fontsizeDateItem = doc.CreateElement("FontSize");
                string DateItemFontsize = Convert.ToString(dgvpageheader.Rows[22].Cells[5].FormattedValue.ToString());
                if (DateItemFontsize == string.Empty)
                {
                    DateItemFontsize = "9";
                }
                fontsizeDateItem.AppendChild(doc.CreateTextNode(DateItemFontsize));
                DateItemNode.AppendChild(fontsizeDateItem);



                XmlNode FontBoldDateItem = doc.CreateElement("FontBold");
                bool DateItemFontBold = Convert.ToBoolean(dgvpageheader.Rows[22].Cells[6].FormattedValue.ToString());
                FontBoldDateItem.AppendChild(doc.CreateTextNode(Convert.ToString(DateItemFontBold)));
                DateItemNode.AppendChild(FontBoldDateItem);


                XmlNode ShowDateItem = doc.CreateElement("Show");
                bool DateItemShow = Convert.ToBoolean(dgvpageheader.Rows[22].Cells[7].FormattedValue.ToString());
                ShowDateItem.AppendChild(doc.CreateTextNode(Convert.ToString(DateItemShow)));
                DateItemNode.AppendChild(ShowDateItem);



                XmlNode ShowTextDateItem = doc.CreateElement("ShowText");
                bool DateItemShowText = Convert.ToBoolean(dgvpageheader.Rows[22].Cells[8].FormattedValue.ToString());
                ShowTextDateItem.AppendChild(doc.CreateTextNode(Convert.ToString(DateItemShowText)));
                DateItemNode.AppendChild(ShowTextDateItem);
                //end DateItem//


                //Start PageNoItem
                XmlNode PageNoItemNode = doc.CreateElement("PageNoItem");
                PageHeader.AppendChild(PageNoItemNode);

                XmlNode textnodePageNoItem = doc.CreateElement("Text");
                //textnodePageNoItem.AppendChild(doc.CreateElement(""));
                string PageNoItemtext = dgvpageheader.Rows[23].Cells[1].Value.ToString();
                textnodePageNoItem.AppendChild(doc.CreateTextNode(PageNoItemtext));
                PageNoItemNode.AppendChild(textnodePageNoItem);

                XmlNode PageNoItemnameNode = doc.CreateElement("Row");
                string PageNoItemrow = dgvpageheader.Rows[23].Cells[2].Value.ToString();
                PageNoItemnameNode.AppendChild(doc.CreateTextNode(PageNoItemrow));
                PageNoItemNode.AppendChild(PageNoItemnameNode);


                XmlNode PageNoItempriceNode = doc.CreateElement("Column");
                string PageNoItemcolumn = dgvpageheader.Rows[23].Cells[3].Value.ToString();
                PageNoItempriceNode.AppendChild(doc.CreateTextNode(PageNoItemcolumn));
                PageNoItemNode.AppendChild(PageNoItempriceNode);


                XmlNode fontnamePageNoItem = doc.CreateElement("FontName");
                string PageNoItemFontName = Convert.ToString(dgvpageheader.Rows[23].Cells[4].FormattedValue.ToString());
                if (PageNoItemFontName == string.Empty)
                {
                    PageNoItemFontName = "Arial";
                }
                fontnamePageNoItem.AppendChild(doc.CreateTextNode(PageNoItemFontName));
                PageNoItemNode.AppendChild(fontnamePageNoItem);



                XmlNode fontsizePageNoItem = doc.CreateElement("FontSize");
                string PageNoItemFontsize = Convert.ToString(dgvpageheader.Rows[23].Cells[5].FormattedValue.ToString());
                if (PageNoItemFontsize == string.Empty)
                {
                    PageNoItemFontsize = "9";
                }
                fontsizePageNoItem.AppendChild(doc.CreateTextNode(PageNoItemFontsize));
                PageNoItemNode.AppendChild(fontsizePageNoItem);



                XmlNode FontBoldPageNoItem = doc.CreateElement("FontBold");
                bool PageNoItemFontBold = Convert.ToBoolean(dgvpageheader.Rows[23].Cells[6].FormattedValue.ToString());
                FontBoldPageNoItem.AppendChild(doc.CreateTextNode(Convert.ToString(PageNoItemFontBold)));
                PageNoItemNode.AppendChild(FontBoldPageNoItem);


                XmlNode ShowPageNoItem = doc.CreateElement("Show");
                bool PageNoItemShow = Convert.ToBoolean(dgvpageheader.Rows[23].Cells[7].FormattedValue.ToString());
                ShowPageNoItem.AppendChild(doc.CreateTextNode(Convert.ToString(PageNoItemShow)));
                PageNoItemNode.AppendChild(ShowPageNoItem);



                XmlNode ShowTextPageNoItem = doc.CreateElement("ShowText");
                bool PageNoItemShowText = Convert.ToBoolean(dgvpageheader.Rows[23].Cells[8].FormattedValue.ToString());
                ShowTextPageNoItem.AppendChild(doc.CreateTextNode(Convert.ToString(PageNoItemShowText)));
                PageNoItemNode.AppendChild(ShowTextPageNoItem);

                //end PageNoItem//

                #endregion
                PrintSettings.AppendChild(PageHeader);

                # region page content
                // start page content //

                XmlNode PageContent = doc.CreateElement("PageContent");

                // start column1//
                XmlNode Column1 = doc.CreateElement("Column1");
                PageContent.AppendChild(Column1);

                // start serail number//
                XmlNode firstserialnumberColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderserialnumber = dgprintsettingview.Rows[0].Cells[0].Value.ToString();
                firstserialnumberColumnHeader.AppendChild(doc.CreateTextNode(columnheaderserialnumber));
                Column1.AppendChild(firstserialnumberColumnHeader);


                XmlNode firstserialnumberFontName = doc.CreateElement("FontName");
                string SelectedcmbserialnumberfontName = Convert.ToString(dgprintsettingview.Rows[0].Cells[1].FormattedValue.ToString());
                if (SelectedcmbserialnumberfontName == string.Empty)
                {
                    SelectedcmbserialnumberfontName = "Arial";
                }
                firstserialnumberFontName.AppendChild(doc.CreateTextNode(SelectedcmbserialnumberfontName));
                Column1.AppendChild(firstserialnumberFontName);


                XmlNode firstserialnumberFontSize = doc.CreateElement("FontSize");
                string Selectedcmbserialnumberfontsize = Convert.ToString(dgprintsettingview.Rows[0].Cells[2].FormattedValue.ToString());
                if (Selectedcmbserialnumberfontsize == string.Empty)
                {
                    Selectedcmbserialnumberfontsize = "9";
                }
                firstserialnumberFontSize.AppendChild(doc.CreateTextNode(Selectedcmbserialnumberfontsize));
                Column1.AppendChild(firstserialnumberFontSize);


                XmlNode firstserialnumberFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbserialnumberfontbold = Convert.ToBoolean(dgprintsettingview.Rows[0].Cells[3].FormattedValue.ToString());
                firstserialnumberFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbserialnumberfontbold)));
                Column1.AppendChild(firstserialnumberFontBold);


                XmlNode firstserialnumberShow = doc.CreateElement("Show");
                bool Selectedcmbserialnumbershow = Convert.ToBoolean(dgprintsettingview.Rows[0].Cells[4].FormattedValue.ToString());
                firstserialnumberShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbserialnumbershow)));
                Column1.AppendChild(firstserialnumberShow);


                XmlNode firstserialnumberColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbserialnumberColumnDataField = Convert.ToString(dgprintsettingview.Rows[0].Cells[6].FormattedValue.ToString());
                firstserialnumberColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbserialnumberColumnDataField)));
                Column1.AppendChild(firstserialnumberColumnDataField);




                XmlNode firstserialnumberColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbserialnumberColumnDataType = Convert.ToString(dgprintsettingview.Rows[0].Cells[7].FormattedValue.ToString());
                firstserialnumberColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbserialnumberColumnDataType)));
                Column1.AppendChild(firstserialnumberColumnDataType);


                XmlNode firstserialnumberColumn = doc.CreateElement("Column");
                string Selectedcmbserialnumbercolumn = Convert.ToString(dgprintsettingview.Rows[0].Cells[5].FormattedValue.ToString());
                firstserialnumberColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbserialnumbercolumn)));
                Column1.AppendChild(firstserialnumberColumn);

                // end serail number//

                // start column2
                // start quantity //

                XmlNode Column2 = doc.CreateElement("Column2");
                PageContent.AppendChild(Column2);

                XmlNode firstquantityColumnHeader = doc.CreateElement("ColumnHeader");

                string columnheaderquantity = dgprintsettingview.Rows[1].Cells[0].Value.ToString();
                firstquantityColumnHeader.AppendChild(doc.CreateTextNode(columnheaderquantity));
                Column2.AppendChild(firstquantityColumnHeader);


                XmlNode firstquantityFontName = doc.CreateElement("FontName");
                string SelectedcmbquatityfontName = Convert.ToString(dgprintsettingview.Rows[1].Cells[1].FormattedValue.ToString());
                if (SelectedcmbquatityfontName == string.Empty)
                {
                    SelectedcmbquatityfontName = "Arial";
                }
                firstquantityFontName.AppendChild(doc.CreateTextNode(SelectedcmbquatityfontName));
                Column2.AppendChild(firstquantityFontName);


                XmlNode firstquantityFontSize = doc.CreateElement("FontSize");
                string Selectedcmbquatityfontsize = Convert.ToString(dgprintsettingview.Rows[1].Cells[2].FormattedValue.ToString());
                if (Selectedcmbquatityfontsize == string.Empty)
                {
                    Selectedcmbquatityfontsize = "9";
                }
                firstquantityFontSize.AppendChild(doc.CreateTextNode(Selectedcmbquatityfontsize));
                Column2.AppendChild(firstquantityFontSize);


                XmlNode firstquantityFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbquatityfontbold = Convert.ToBoolean(dgprintsettingview.Rows[1].Cells[3].FormattedValue.ToString());
                firstquantityFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbquatityfontbold)));
                Column2.AppendChild(firstquantityFontBold);


                XmlNode firstquantityShow = doc.CreateElement("Show");
                bool Selectedcmbquatityshow = Convert.ToBoolean(dgprintsettingview.Rows[1].Cells[4].FormattedValue.ToString());
                firstquantityShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbquatityshow)));
                Column2.AppendChild(firstquantityShow);


                XmlNode firstquantityColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbquatityColumnDataField = Convert.ToString(dgprintsettingview.Rows[1].Cells[6].FormattedValue.ToString());
                firstquantityColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbquatityColumnDataField)));
                Column2.AppendChild(firstquantityColumnDataField);




                XmlNode firstquantityColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbquatityColumnDataType = Convert.ToString(dgprintsettingview.Rows[1].Cells[7].FormattedValue.ToString());
                firstquantityColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbquatityColumnDataType)));
                Column2.AppendChild(firstquantityColumnDataType);


                XmlNode firstquantityColumn = doc.CreateElement("Column");
                string Selectedcmbquatitycolumn = Convert.ToString(dgprintsettingview.Rows[1].Cells[5].FormattedValue.ToString());
                firstquantityColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbquatitycolumn)));
                Column2.AppendChild(firstquantityColumn);

                // end quantity


                // end column2//

                // start column3//
                XmlNode Column3 = doc.CreateElement("Column3");
                PageContent.AppendChild(Column3);

                XmlNode firstDescriptionColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderDescription = dgprintsettingview.Rows[2].Cells[0].Value.ToString();
                firstDescriptionColumnHeader.AppendChild(doc.CreateTextNode(columnheaderDescription));
                Column3.AppendChild(firstDescriptionColumnHeader);

                XmlNode firstDescriptionFontSize = doc.CreateElement("FontSize");
                string SelectedcmbDescriptionfontsize = Convert.ToString(dgprintsettingview.Rows[2].Cells[2].FormattedValue.ToString());
                if (SelectedcmbDescriptionfontsize == string.Empty)
                {
                    SelectedcmbDescriptionfontsize = "9";
                }
                firstDescriptionFontSize.AppendChild(doc.CreateTextNode(SelectedcmbDescriptionfontsize));
                Column3.AppendChild(firstDescriptionFontSize);


                XmlNode firstDescriptionFontName = doc.CreateElement("FontName");
                string SelectedcmbDescriptionfontName = Convert.ToString(dgprintsettingview.Rows[2].Cells[1].FormattedValue.ToString());
                if (SelectedcmbDescriptionfontName == string.Empty)
                {
                    SelectedcmbDescriptionfontName = "Arial";
                }
                firstDescriptionFontName.AppendChild(doc.CreateTextNode(SelectedcmbDescriptionfontName));
                Column3.AppendChild(firstDescriptionFontName);


                XmlNode firstDescriptionFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbDescriptionfontbold = Convert.ToBoolean(dgprintsettingview.Rows[2].Cells[3].FormattedValue.ToString());
                firstDescriptionFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbDescriptionfontbold)));
                Column3.AppendChild(firstDescriptionFontBold);


                XmlNode firstDescriptionShow = doc.CreateElement("Show");
                bool SelectedcmbDescriptionshow = Convert.ToBoolean(dgprintsettingview.Rows[2].Cells[4].FormattedValue.ToString());
                firstDescriptionShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbDescriptionshow)));
                Column3.AppendChild(firstDescriptionShow);

                XmlNode firstDescriptionColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbDescriptionColumnDataField = Convert.ToString(dgprintsettingview.Rows[2].Cells[6].FormattedValue.ToString());
                firstDescriptionColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbDescriptionColumnDataField)));
                Column3.AppendChild(firstDescriptionColumnDataField);

                XmlNode firstDescriptionColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbDescriptionColumnDataType = Convert.ToString(dgprintsettingview.Rows[2].Cells[7].FormattedValue.ToString());
                firstDescriptionColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbDescriptionColumnDataType)));
                Column3.AppendChild(firstDescriptionColumnDataType);

                XmlNode firstDescriptionColumn = doc.CreateElement("Column");
                string SelectedcmbDescriptioncolumn = Convert.ToString(dgprintsettingview.Rows[2].Cells[5].FormattedValue.ToString());
                firstDescriptionColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbDescriptioncolumn)));
                Column3.AppendChild(firstDescriptionColumn);


                // end column3//


                // start column4//
                XmlNode Column4 = doc.CreateElement("Column4");
                PageContent.AppendChild(Column4);

                XmlNode firstShelfColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderShelf = dgprintsettingview.Rows[3].Cells[0].Value.ToString();
                firstShelfColumnHeader.AppendChild(doc.CreateTextNode(columnheaderShelf));
                Column4.AppendChild(firstShelfColumnHeader);

                XmlNode firstShelfFontSize = doc.CreateElement("FontSize");
                string SelectedcmbShelffontsize = Convert.ToString(dgprintsettingview.Rows[3].Cells[2].FormattedValue.ToString());
                if (SelectedcmbShelffontsize == string.Empty)
                {
                    SelectedcmbShelffontsize = "9";
                }
                firstShelfFontSize.AppendChild(doc.CreateTextNode(SelectedcmbShelffontsize));
                Column4.AppendChild(firstShelfFontSize);


                XmlNode firstShelfFontName = doc.CreateElement("FontName");
                string SelectedcmbShelffontName = Convert.ToString(dgprintsettingview.Rows[3].Cells[1].FormattedValue.ToString());
                if (SelectedcmbShelffontName == string.Empty)
                {
                    SelectedcmbShelffontName = "Arial";
                }
                firstShelfFontName.AppendChild(doc.CreateTextNode(SelectedcmbShelffontName));
                Column4.AppendChild(firstShelfFontName);


                XmlNode firstShelfFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbShelffontbold = Convert.ToBoolean(dgprintsettingview.Rows[3].Cells[3].FormattedValue.ToString());
                firstShelfFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbShelffontbold)));
                Column4.AppendChild(firstShelfFontBold);


                XmlNode firstShelfShow = doc.CreateElement("Show");
                bool SelectedcmbShelfshow = Convert.ToBoolean(dgprintsettingview.Rows[3].Cells[4].FormattedValue.ToString());
                firstShelfShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbShelfshow)));
                Column4.AppendChild(firstShelfShow);

                XmlNode firstShelfColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbShelfColumnDataField = Convert.ToString(dgprintsettingview.Rows[3].Cells[6].FormattedValue.ToString());
                firstShelfColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbShelfColumnDataField)));
                Column4.AppendChild(firstShelfColumnDataField);

                XmlNode firstShelfColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbShelfColumnDataType = Convert.ToString(dgprintsettingview.Rows[3].Cells[7].FormattedValue.ToString());
                firstShelfColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbShelfColumnDataType)));
                Column4.AppendChild(firstShelfColumnDataType);

                XmlNode firstShelfColumn = doc.CreateElement("Column");
                string SelectedcmbShelfcolumn = Convert.ToString(dgprintsettingview.Rows[3].Cells[5].FormattedValue.ToString());
                firstShelfColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbShelfcolumn)));
                Column4.AppendChild(firstShelfColumn);


                // end column4 //



                // start column5//
                XmlNode Column5 = doc.CreateElement("Column4");
                PageContent.AppendChild(Column5);

                XmlNode firstCompColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderComp = dgprintsettingview.Rows[4].Cells[0].Value.ToString();
                firstCompColumnHeader.AppendChild(doc.CreateTextNode(columnheaderComp));
                Column5.AppendChild(firstCompColumnHeader);

                XmlNode firstCompFontSize = doc.CreateElement("FontSize");
                string SelectedcmbCompfontsize = Convert.ToString(dgprintsettingview.Rows[4].Cells[2].FormattedValue.ToString());
                if (SelectedcmbCompfontsize == string.Empty)
                {
                    SelectedcmbCompfontsize = "9";
                }
                firstCompFontSize.AppendChild(doc.CreateTextNode(SelectedcmbCompfontsize));
                Column5.AppendChild(firstCompFontSize);


                XmlNode firstCompFontName = doc.CreateElement("FontName");
                string SelectedcmbCompfontName = Convert.ToString(dgprintsettingview.Rows[4].Cells[1].FormattedValue.ToString());
                if (SelectedcmbCompfontName == string.Empty)
                {
                    SelectedcmbCompfontName = "Arial";
                }
                firstCompFontName.AppendChild(doc.CreateTextNode(SelectedcmbCompfontName));
                Column5.AppendChild(firstCompFontName);


                XmlNode firstCompFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbCompfontbold = Convert.ToBoolean(dgprintsettingview.Rows[4].Cells[3].FormattedValue.ToString());
                firstCompFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbCompfontbold)));
                Column5.AppendChild(firstCompFontBold);


                XmlNode firstCompShow = doc.CreateElement("Show");
                bool SelectedcmbCompshow = Convert.ToBoolean(dgprintsettingview.Rows[4].Cells[4].FormattedValue.ToString());
                firstCompShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbCompshow)));
                Column5.AppendChild(firstCompShow);

                XmlNode firstCompColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbCompColumnDataField = Convert.ToString(dgprintsettingview.Rows[4].Cells[6].FormattedValue.ToString());
                firstCompColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbCompColumnDataField)));
                Column5.AppendChild(firstCompColumnDataField);

                XmlNode firstCompColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbCompColumnDataType = Convert.ToString(dgprintsettingview.Rows[4].Cells[7].FormattedValue.ToString());
                firstCompColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbCompColumnDataType)));
                Column5.AppendChild(firstCompColumnDataType);

                XmlNode firstCompColumn = doc.CreateElement("Column");
                string SelectedcmbCompcolumn = Convert.ToString(dgprintsettingview.Rows[4].Cells[5].FormattedValue.ToString());
                firstCompColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbCompcolumn)));
                Column5.AppendChild(firstCompColumn);


                // end column5 //





                // start column 6//
                XmlNode Column6 = doc.CreateElement("Column6");
                PageContent.AppendChild(Column6);

                XmlNode firstBatchColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderBatch = dgprintsettingview.Rows[5].Cells[0].Value.ToString();
                firstBatchColumnHeader.AppendChild(doc.CreateTextNode(columnheaderBatch));
                Column6.AppendChild(firstBatchColumnHeader);

                XmlNode firstBatchFontSize = doc.CreateElement("FontSize");
                string SelectedcmbBatchfontsize = Convert.ToString(dgprintsettingview.Rows[5].Cells[2].FormattedValue.ToString());
                if (SelectedcmbBatchfontsize == string.Empty)
                {
                    SelectedcmbBatchfontsize = "9";
                }
                firstBatchFontSize.AppendChild(doc.CreateTextNode(SelectedcmbBatchfontsize));
                Column6.AppendChild(firstBatchFontSize);


                XmlNode firstBatchFontName = doc.CreateElement("FontName");
                string SelectedcmbBatchfontName = Convert.ToString(dgprintsettingview.Rows[5].Cells[1].FormattedValue.ToString());
                if (SelectedcmbBatchfontName == string.Empty)
                {
                    SelectedcmbBatchfontName = "Arial";
                }
                firstBatchFontName.AppendChild(doc.CreateTextNode(SelectedcmbBatchfontName));
                Column6.AppendChild(firstBatchFontName);


                XmlNode firstBatchFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbBatchfontbold = Convert.ToBoolean(dgprintsettingview.Rows[5].Cells[3].FormattedValue.ToString());
                firstBatchFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbBatchfontbold)));
                Column6.AppendChild(firstBatchFontBold);


                XmlNode firstBatchShow = doc.CreateElement("Show");
                bool SelectedcmbBatchshow = Convert.ToBoolean(dgprintsettingview.Rows[5].Cells[4].FormattedValue.ToString());
                firstBatchShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbBatchshow)));
                Column6.AppendChild(firstBatchShow);


                XmlNode firstBatchColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbBatchColumnDataField = Convert.ToString(dgprintsettingview.Rows[5].Cells[6].FormattedValue.ToString());
                firstBatchColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbBatchColumnDataField)));
                Column6.AppendChild(firstBatchColumnDataField);

                XmlNode firstBatchColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbBatchColumnDataType = Convert.ToString(dgprintsettingview.Rows[5].Cells[7].FormattedValue.ToString());
                firstBatchColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbBatchColumnDataType)));
                Column6.AppendChild(firstBatchColumnDataType);

                XmlNode firstBatchColumn = doc.CreateElement("Column");
                string SelectedcmbBatchcolumn = Convert.ToString(dgprintsettingview.Rows[5].Cells[5].FormattedValue.ToString());
                firstBatchColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbBatchcolumn)));
                Column6.AppendChild(firstBatchColumn);

                // end column 6 //





                // start column 7//
                XmlNode Column7 = doc.CreateElement("Column7");
                PageContent.AppendChild(Column7);

                XmlNode firstExpiryColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderExpiry = dgprintsettingview.Rows[6].Cells[0].Value.ToString();
                firstExpiryColumnHeader.AppendChild(doc.CreateTextNode(columnheaderExpiry));
                Column7.AppendChild(firstExpiryColumnHeader);

                XmlNode firstExpiryFontSize = doc.CreateElement("FontSize");
                string SelectedcmbExpiryfontsize = Convert.ToString(dgprintsettingview.Rows[6].Cells[2].FormattedValue.ToString());
                if (SelectedcmbExpiryfontsize == string.Empty)
                {
                    SelectedcmbExpiryfontsize = "9";
                }
                firstExpiryFontSize.AppendChild(doc.CreateTextNode(SelectedcmbExpiryfontsize));
                Column7.AppendChild(firstExpiryFontSize);


                XmlNode firstExpiryFontName = doc.CreateElement("FontName");
                string SelectedcmbExpiryfontName = Convert.ToString(dgprintsettingview.Rows[6].Cells[1].FormattedValue.ToString());
                if (SelectedcmbExpiryfontName == string.Empty)
                {
                    SelectedcmbExpiryfontName = "Arial";
                }
                firstExpiryFontName.AppendChild(doc.CreateTextNode(SelectedcmbExpiryfontName));
                Column7.AppendChild(firstExpiryFontName);


                XmlNode firstExpiryFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbExpiryfontbold = Convert.ToBoolean(dgprintsettingview.Rows[6].Cells[3].FormattedValue.ToString());
                firstExpiryFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbExpiryfontbold)));
                Column7.AppendChild(firstExpiryFontBold);


                XmlNode firstExpiryShow = doc.CreateElement("Show");
                bool SelectedcmbExpiryshow = Convert.ToBoolean(dgprintsettingview.Rows[6].Cells[4].FormattedValue.ToString());
                firstExpiryShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbExpiryshow)));
                Column7.AppendChild(firstExpiryShow);

                XmlNode firstExpiryColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbExpiryColumnDataField = Convert.ToString(dgprintsettingview.Rows[6].Cells[6].FormattedValue.ToString());
                firstExpiryColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbExpiryColumnDataField)));
                Column7.AppendChild(firstExpiryColumnDataField);

                XmlNode firstExpiryColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbExpiryColumnDataType = Convert.ToString(dgprintsettingview.Rows[6].Cells[7].FormattedValue.ToString());
                firstExpiryColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbExpiryColumnDataType)));
                Column7.AppendChild(firstExpiryColumnDataType);

                XmlNode firstExpiryColumn = doc.CreateElement("Column");
                string SelectedcmbExpirycolumn = Convert.ToString(dgprintsettingview.Rows[6].Cells[5].FormattedValue.ToString());
                firstExpiryColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbExpirycolumn)));
                Column7.AppendChild(firstExpiryColumn);

                // end column 7 //



                // start column 8//
                XmlNode Column8 = doc.CreateElement("Column8");
                PageContent.AppendChild(Column8);

                XmlNode firstmrpColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheadermrp = dgprintsettingview.Rows[7].Cells[0].Value.ToString();
                firstmrpColumnHeader.AppendChild(doc.CreateTextNode(columnheadermrp));
                Column8.AppendChild(firstmrpColumnHeader);

                XmlNode firstmrpFontSize = doc.CreateElement("FontSize");
                string Selectedcmbmrpfontsize = Convert.ToString(dgprintsettingview.Rows[7].Cells[2].FormattedValue.ToString());
                if (Selectedcmbmrpfontsize == string.Empty)
                {
                    Selectedcmbmrpfontsize = "9";
                }
                firstmrpFontSize.AppendChild(doc.CreateTextNode(Selectedcmbmrpfontsize));
                Column8.AppendChild(firstmrpFontSize);


                XmlNode firstmrpFontName = doc.CreateElement("FontName");
                string SelectedcmbmrpfontName = Convert.ToString(dgprintsettingview.Rows[7].Cells[1].FormattedValue.ToString());
                if (SelectedcmbmrpfontName == string.Empty)
                {
                    SelectedcmbmrpfontName = "Arial";
                }
                firstmrpFontName.AppendChild(doc.CreateTextNode(SelectedcmbmrpfontName));
                Column8.AppendChild(firstmrpFontName);


                XmlNode firstmrpFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbmrpfontbold = Convert.ToBoolean(dgprintsettingview.Rows[7].Cells[3].FormattedValue.ToString());
                firstmrpFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbmrpfontbold)));
                Column8.AppendChild(firstmrpFontBold);


                XmlNode firstmrpShow = doc.CreateElement("Show");
                bool Selectedcmbmrpshow = Convert.ToBoolean(dgprintsettingview.Rows[7].Cells[4].FormattedValue.ToString());
                firstmrpShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbmrpshow)));
                Column8.AppendChild(firstmrpShow);


                XmlNode firstmrpColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbmrpColumnDataField = Convert.ToString(dgprintsettingview.Rows[7].Cells[6].FormattedValue.ToString());
                firstmrpColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbmrpColumnDataField)));
                Column8.AppendChild(firstmrpColumnDataField);

                XmlNode firstmrpColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbmrpColumnDataType = Convert.ToString(dgprintsettingview.Rows[7].Cells[7].FormattedValue.ToString());
                firstmrpColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbmrpColumnDataType)));
                Column8.AppendChild(firstmrpColumnDataType);

                XmlNode firstmrpColumn = doc.CreateElement("Column");
                string Selectedcmbmrpcolumn = Convert.ToString(dgprintsettingview.Rows[7].Cells[5].FormattedValue.ToString());
                firstmrpColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbmrpcolumn)));
                Column8.AppendChild(firstmrpColumn);

                // end column 8 //



                // start column 9//
                XmlNode Column9 = doc.CreateElement("Column9");
                PageContent.AppendChild(Column9);

                XmlNode firstsalerateColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheadersalerate = dgprintsettingview.Rows[8].Cells[0].Value.ToString();
                firstsalerateColumnHeader.AppendChild(doc.CreateTextNode(columnheadersalerate));
                Column9.AppendChild(firstsalerateColumnHeader);

                XmlNode firstsalerateFontSize = doc.CreateElement("FontSize");
                string Selectedcmbsaleratefontsize = Convert.ToString(dgprintsettingview.Rows[8].Cells[2].FormattedValue.ToString());
                if (Selectedcmbsaleratefontsize == string.Empty)
                {
                    Selectedcmbsaleratefontsize = "9";
                }
                firstsalerateFontSize.AppendChild(doc.CreateTextNode(Selectedcmbsaleratefontsize));
                Column9.AppendChild(firstsalerateFontSize);


                XmlNode firstsalerateFontName = doc.CreateElement("FontName");
                string SelectedcmbsaleratefontName = Convert.ToString(dgprintsettingview.Rows[8].Cells[1].FormattedValue.ToString());
                if (SelectedcmbsaleratefontName == string.Empty)
                {
                    SelectedcmbsaleratefontName = "Arial";
                }
                firstsalerateFontName.AppendChild(doc.CreateTextNode(SelectedcmbsaleratefontName));
                Column9.AppendChild(firstsalerateFontName);


                XmlNode firstsalerateFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbsaleratefontbold = Convert.ToBoolean(dgprintsettingview.Rows[8].Cells[3].FormattedValue.ToString());
                firstsalerateFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbsaleratefontbold)));
                Column9.AppendChild(firstsalerateFontBold);


                XmlNode firstsalerateShow = doc.CreateElement("Show");
                bool Selectedcmbsalerateshow = Convert.ToBoolean(dgprintsettingview.Rows[8].Cells[4].FormattedValue.ToString());
                firstsalerateShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbsalerateshow)));
                Column9.AppendChild(firstsalerateShow);


                XmlNode firstsalerateColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbsalerateColumnDataField = Convert.ToString(dgprintsettingview.Rows[8].Cells[6].FormattedValue.ToString());
                firstsalerateColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbsalerateColumnDataField)));
                Column9.AppendChild(firstsalerateColumnDataField);

                XmlNode firstsalerateColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbsalerateColumnDataType = Convert.ToString(dgprintsettingview.Rows[8].Cells[7].FormattedValue.ToString());
                firstsalerateColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbsalerateColumnDataType)));
                Column9.AppendChild(firstsalerateColumnDataType);

                XmlNode firstsalerateColumn = doc.CreateElement("Column");
                string Selectedcmbsaleratecolumn = Convert.ToString(dgprintsettingview.Rows[8].Cells[5].FormattedValue.ToString());
                firstsalerateColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbsaleratecolumn)));
                Column9.AppendChild(firstsalerateColumn);

                // end column 9 //




                // start column 10//
                XmlNode Column10 = doc.CreateElement("Column10");
                PageContent.AppendChild(Column10);

                XmlNode firstpurchaserateColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderpurchaserate = dgprintsettingview.Rows[9].Cells[0].Value.ToString();
                firstpurchaserateColumnHeader.AppendChild(doc.CreateTextNode(columnheaderpurchaserate));
                Column10.AppendChild(firstpurchaserateColumnHeader);

                XmlNode firstpurchaserateFontSize = doc.CreateElement("FontSize");
                string Selectedcmbpurchaseratefontsize = Convert.ToString(dgprintsettingview.Rows[9].Cells[2].FormattedValue.ToString());
                if (Selectedcmbpurchaseratefontsize == string.Empty)
                {
                    Selectedcmbpurchaseratefontsize = "9";
                }
                firstpurchaserateFontSize.AppendChild(doc.CreateTextNode(Selectedcmbpurchaseratefontsize));
                Column10.AppendChild(firstpurchaserateFontSize);


                XmlNode firstpurchaserateFontName = doc.CreateElement("FontName");
                string SelectedcmbpurchaseratefontName = Convert.ToString(dgprintsettingview.Rows[9].Cells[1].FormattedValue.ToString());
                if (SelectedcmbpurchaseratefontName == string.Empty)
                {
                    SelectedcmbpurchaseratefontName = "Arial";
                }
                firstpurchaserateFontName.AppendChild(doc.CreateTextNode(SelectedcmbpurchaseratefontName));
                Column10.AppendChild(firstpurchaserateFontName);


                XmlNode firstpurchaserateFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbpurchaseratefontbold = Convert.ToBoolean(dgprintsettingview.Rows[9].Cells[3].FormattedValue.ToString());
                firstpurchaserateFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbpurchaseratefontbold)));
                Column10.AppendChild(firstpurchaserateFontBold);


                XmlNode firstpurchaserateShow = doc.CreateElement("Show");
                bool Selectedcmbpurchaserateshow = Convert.ToBoolean(dgprintsettingview.Rows[9].Cells[4].FormattedValue.ToString());
                firstpurchaserateShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbpurchaserateshow)));
                Column10.AppendChild(firstpurchaserateShow);


                XmlNode firstpurchaserateColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbpurchaserateColumnDataField = Convert.ToString(dgprintsettingview.Rows[9].Cells[6].FormattedValue.ToString());
                firstpurchaserateColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbpurchaserateColumnDataField)));
                Column10.AppendChild(firstpurchaserateColumnDataField);

                XmlNode firstpurchaserateColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbpurchaserateColumnDataType = Convert.ToString(dgprintsettingview.Rows[9].Cells[7].FormattedValue.ToString());
                firstpurchaserateColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbpurchaserateColumnDataType)));
                Column10.AppendChild(firstpurchaserateColumnDataType);

                XmlNode firstpurchaserateColumn = doc.CreateElement("Column");
                string Selectedcmbpurchaseratecolumn = Convert.ToString(dgprintsettingview.Rows[9].Cells[5].FormattedValue.ToString());
                firstpurchaserateColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbpurchaseratecolumn)));
                Column10.AppendChild(firstpurchaserateColumn);

                // end column 10 //







                // start column 11//
                XmlNode Column11 = doc.CreateElement("Column11");
                PageContent.AppendChild(Column11);

                XmlNode firstvatpercentageColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheadervatpercentage = dgprintsettingview.Rows[10].Cells[0].Value.ToString();
                firstvatpercentageColumnHeader.AppendChild(doc.CreateTextNode(columnheadervatpercentage));
                Column11.AppendChild(firstvatpercentageColumnHeader);

                XmlNode firstvatpercentageFontSize = doc.CreateElement("FontSize");
                string Selectedcmbvatpercentagefontsize = Convert.ToString(dgprintsettingview.Rows[10].Cells[2].FormattedValue.ToString());
                if (Selectedcmbvatpercentagefontsize == string.Empty)
                {
                    Selectedcmbvatpercentagefontsize = "9";
                }
                firstvatpercentageFontSize.AppendChild(doc.CreateTextNode(Selectedcmbvatpercentagefontsize));
                Column11.AppendChild(firstvatpercentageFontSize);


                XmlNode firstvatpercentageFontName = doc.CreateElement("FontName");
                string SelectedcmbvatpercentagefontName = Convert.ToString(dgprintsettingview.Rows[10].Cells[1].FormattedValue.ToString());
                if (SelectedcmbvatpercentagefontName == string.Empty)
                {
                    SelectedcmbvatpercentagefontName = "Arial";
                }
                firstvatpercentageFontName.AppendChild(doc.CreateTextNode(SelectedcmbvatpercentagefontName));
                Column11.AppendChild(firstvatpercentageFontName);


                XmlNode firstvatpercentageFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbvatpercentagefontbold = Convert.ToBoolean(dgprintsettingview.Rows[10].Cells[3].FormattedValue.ToString());
                firstvatpercentageFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbvatpercentagefontbold)));
                Column11.AppendChild(firstvatpercentageFontBold);


                XmlNode firstvatpercentageShow = doc.CreateElement("Show");
                bool Selectedcmbvatpercentageshow = Convert.ToBoolean(dgprintsettingview.Rows[10].Cells[4].FormattedValue.ToString());
                firstvatpercentageShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbvatpercentageshow)));
                Column11.AppendChild(firstvatpercentageShow);


                XmlNode firstvatpercentageColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbvatpercentageColumnDataField = Convert.ToString(dgprintsettingview.Rows[10].Cells[6].FormattedValue.ToString());
                firstvatpercentageColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbvatpercentageColumnDataField)));
                Column11.AppendChild(firstvatpercentageColumnDataField);

                XmlNode firstvatpercentageColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbvatpercentageColumnDataType = Convert.ToString(dgprintsettingview.Rows[10].Cells[7].FormattedValue.ToString());
                firstvatpercentageColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbvatpercentageColumnDataType)));
                Column11.AppendChild(firstvatpercentageColumnDataType);

                XmlNode firstvatpercentageColumn = doc.CreateElement("Column");
                string Selectedcmbvatpercentagecolumn = Convert.ToString(dgprintsettingview.Rows[10].Cells[5].FormattedValue.ToString());
                firstvatpercentageColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbvatpercentagecolumn)));
                Column11.AppendChild(firstvatpercentageColumn);

                // end column 11 //

                // start column 12//
                XmlNode Column12 = doc.CreateElement("Column12");
                PageContent.AppendChild(Column12);

                XmlNode firstAmountColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderAmount = dgprintsettingview.Rows[11].Cells[0].Value.ToString();
                firstAmountColumnHeader.AppendChild(doc.CreateTextNode(columnheaderAmount));
                Column12.AppendChild(firstAmountColumnHeader);

                XmlNode firstAmountFontSize = doc.CreateElement("FontSize");
                string SelectedcmbAmountfontsize = Convert.ToString(dgprintsettingview.Rows[11].Cells[2].FormattedValue.ToString());
                if (SelectedcmbAmountfontsize == string.Empty)
                {
                    SelectedcmbAmountfontsize = "9";
                }
                firstAmountFontSize.AppendChild(doc.CreateTextNode(SelectedcmbAmountfontsize));
                Column12.AppendChild(firstAmountFontSize);


                XmlNode firstAmountFontName = doc.CreateElement("FontName");
                string SelectedcmbAmountfontName = Convert.ToString(dgprintsettingview.Rows[11].Cells[1].FormattedValue.ToString());
                if (SelectedcmbAmountfontName == string.Empty)
                {
                    SelectedcmbAmountfontName = "Arial";
                }
                firstAmountFontName.AppendChild(doc.CreateTextNode(SelectedcmbAmountfontName));
                Column12.AppendChild(firstAmountFontName);


                XmlNode firstAmountFontBold = doc.CreateElement("FontBold");
                bool SelectedcmbAmountfontbold = Convert.ToBoolean(dgprintsettingview.Rows[11].Cells[3].FormattedValue.ToString());
                firstAmountFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbAmountfontbold)));
                Column12.AppendChild(firstAmountFontBold);


                XmlNode firstAmountShow = doc.CreateElement("Show");
                bool SelectedcmbAmountshow = Convert.ToBoolean(dgprintsettingview.Rows[11].Cells[4].FormattedValue.ToString());
                firstAmountShow.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbAmountshow)));
                Column12.AppendChild(firstAmountShow);


                XmlNode firstAmountColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbAmountColumnDataField = Convert.ToString(dgprintsettingview.Rows[11].Cells[6].FormattedValue.ToString());
                firstAmountColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbAmountColumnDataField)));
                Column12.AppendChild(firstAmountColumnDataField);

                XmlNode firstAmountColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbAmountColumnDataType = Convert.ToString(dgprintsettingview.Rows[11].Cells[7].FormattedValue.ToString());
                firstAmountColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbAmountColumnDataType)));
                Column12.AppendChild(firstAmountColumnDataType);

                XmlNode firstAmountColumn = doc.CreateElement("Column");
                string SelectedcmbAmountcolumn = Convert.ToString(dgprintsettingview.Rows[11].Cells[5].FormattedValue.ToString());
                firstAmountColumn.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbAmountcolumn)));
                Column12.AppendChild(firstAmountColumn);
                // end column 12 //

                // start column 13//
                XmlNode Column13 = doc.CreateElement("Column13");
                PageContent.AppendChild(Column13);

                XmlNode firstscedulecategoryColumnHeader = doc.CreateElement("ColumnHeader");
                string columnheaderscedulecategory = dgprintsettingview.Rows[12].Cells[0].Value.ToString();
                firstscedulecategoryColumnHeader.AppendChild(doc.CreateTextNode(columnheaderscedulecategory));
                Column13.AppendChild(firstscedulecategoryColumnHeader);

                XmlNode firstscedulecategoryFontSize = doc.CreateElement("FontSize");
                string Selectedcmbscedulecategoryfontsize = Convert.ToString(dgprintsettingview.Rows[12].Cells[2].FormattedValue.ToString());
                if (Selectedcmbscedulecategoryfontsize == string.Empty)
                {
                    Selectedcmbscedulecategoryfontsize = "9";
                }
                firstscedulecategoryFontSize.AppendChild(doc.CreateTextNode(Selectedcmbscedulecategoryfontsize));
                Column13.AppendChild(firstscedulecategoryFontSize);


                XmlNode firstscedulecategoryFontName = doc.CreateElement("FontName");
                string SelectedcmbscedulecategoryfontName = Convert.ToString(dgprintsettingview.Rows[12].Cells[1].FormattedValue.ToString());
                if (SelectedcmbscedulecategoryfontName == string.Empty)
                {
                    SelectedcmbscedulecategoryfontName = "Arial";
                }
                firstscedulecategoryFontName.AppendChild(doc.CreateTextNode(SelectedcmbscedulecategoryfontName));
                Column13.AppendChild(firstscedulecategoryFontName);


                XmlNode firstscedulecategoryFontBold = doc.CreateElement("FontBold");
                bool Selectedcmbscedulecategoryfontbold = Convert.ToBoolean(dgprintsettingview.Rows[12].Cells[3].FormattedValue.ToString());
                firstscedulecategoryFontBold.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbscedulecategoryfontbold)));
                Column13.AppendChild(firstscedulecategoryFontBold);


                XmlNode firstscedulecategoryShow = doc.CreateElement("Show");
                bool Selectedcmbscedulecategoryshow = Convert.ToBoolean(dgprintsettingview.Rows[12].Cells[4].FormattedValue.ToString());
                firstscedulecategoryShow.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbscedulecategoryshow)));
                Column13.AppendChild(firstscedulecategoryShow);


                XmlNode firstscedulecategoryColumnDataField = doc.CreateElement("ColumnDataField");
                string SelectedcmbscedulecategoryColumnDataField = Convert.ToString(dgprintsettingview.Rows[12].Cells[6].FormattedValue.ToString());
                firstscedulecategoryColumnDataField.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbscedulecategoryColumnDataField)));
                Column13.AppendChild(firstscedulecategoryColumnDataField);

                XmlNode firstscedulecategoryColumnDataType = doc.CreateElement("ColumnDataType");
                string SelectedcmbscedulecategoryColumnDataType = Convert.ToString(dgprintsettingview.Rows[12].Cells[7].FormattedValue.ToString());
                firstscedulecategoryColumnDataType.AppendChild(doc.CreateTextNode(Convert.ToString(SelectedcmbscedulecategoryColumnDataType)));
                Column13.AppendChild(firstscedulecategoryColumnDataType);

                XmlNode firstscedulecategoryColumn = doc.CreateElement("Column");
                string Selectedcmbscedulecategorycolumn = Convert.ToString(dgprintsettingview.Rows[12].Cells[5].FormattedValue.ToString());
                firstscedulecategoryColumn.AppendChild(doc.CreateTextNode(Convert.ToString(Selectedcmbscedulecategorycolumn)));
                Column13.AppendChild(firstscedulecategoryColumn);
                // end column 13 //

                // end page content//

                # endregion

                PrintSettings.AppendChild(PageContent);
                # region page footer


                // start Discount Item//
                XmlNode PageFooter = doc.CreateElement("PageFooter");

                XmlNode DiscounteItemNode = doc.CreateElement("DiscounteItem");
                PageFooter.AppendChild(DiscounteItemNode);



                XmlNode textnodeDiscounte = doc.CreateElement("Text");
                //textnodeDiscounte.AppendChild(doc.CreateElement(""));
                string Discountetext = dgvpagefooter.Rows[0].Cells[1].Value.ToString();
                textnodeDiscounte.AppendChild(doc.CreateTextNode(Discountetext));
                DiscounteItemNode.AppendChild(textnodeDiscounte);

                XmlNode nameNodediscount = doc.CreateElement("Row");
                string Discounterow = dgvpagefooter.Rows[0].Cells[2].Value.ToString();
                nameNodediscount.AppendChild(doc.CreateTextNode(Discounterow));
                DiscounteItemNode.AppendChild(nameNodediscount);


                XmlNode priceNodeDiscount = doc.CreateElement("Column");
                string Discountecolumn = dgvpagefooter.Rows[0].Cells[3].Value.ToString();
                priceNodeDiscount.AppendChild(doc.CreateTextNode(Discountecolumn));
                DiscounteItemNode.AppendChild(priceNodeDiscount);


                XmlNode fontnameDiscounte = doc.CreateElement("FontName");

                string DiscounteFontName = Convert.ToString(dgvpagefooter.Rows[0].Cells[4].FormattedValue.ToString());
                if (DiscounteFontName == string.Empty)
                {
                    DiscounteFontName = "Arial";
                }
                fontnameDiscounte.AppendChild(doc.CreateTextNode(DiscounteFontName));
                DiscounteItemNode.AppendChild(fontnameDiscounte);



                XmlNode fontsizeDiscounte = doc.CreateElement("FontSize");
                string DiscounteFontsize = Convert.ToString(dgvpagefooter.Rows[0].Cells[5].FormattedValue.ToString());
                if (DiscounteFontsize == string.Empty)
                {
                    DiscounteFontsize = "9";
                }
                fontsizeDiscounte.AppendChild(doc.CreateTextNode(DiscounteFontsize));
                DiscounteItemNode.AppendChild(fontsizeDiscounte);



                XmlNode FontBoldDiscounte = doc.CreateElement("FontBold");
                bool DiscounteFontBold = Convert.ToBoolean(dgvpagefooter.Rows[0].Cells[6].FormattedValue.ToString());
                FontBoldDiscounte.AppendChild(doc.CreateTextNode(Convert.ToString(DiscounteFontBold)));
                DiscounteItemNode.AppendChild(FontBoldDiscounte);


                XmlNode ShowDiscounte = doc.CreateElement("Show");
                bool DiscounteShow = Convert.ToBoolean(dgvpagefooter.Rows[0].Cells[7].FormattedValue.ToString());
                ShowDiscounte.AppendChild(doc.CreateTextNode(Convert.ToString(DiscounteShow)));
                DiscounteItemNode.AppendChild(ShowDiscounte);



                XmlNode ShowTextDiscounte = doc.CreateElement("ShowText");
                bool DiscounteShowText = Convert.ToBoolean(dgvpagefooter.Rows[0].Cells[8].FormattedValue.ToString());
                ShowTextDiscounte.AppendChild(doc.CreateTextNode(Convert.ToString(DiscounteShowText)));
                DiscounteItemNode.AppendChild(ShowTextDiscounte);

                //End DiscounteItem          

                // start GrossAmount Item//

                XmlNode GrossAmounteItemNode = doc.CreateElement("GrossAmountItem");
                PageFooter.AppendChild(GrossAmounteItemNode);

                XmlNode textnodeGrossAmounte = doc.CreateElement("Text");
                //textnodeGrossAmounte.AppendChild(doc.CreateElement(""));
                string GrossAmountetext = dgvpagefooter.Rows[1].Cells[1].Value.ToString();
                textnodeGrossAmounte.AppendChild(doc.CreateTextNode(GrossAmountetext));
                GrossAmounteItemNode.AppendChild(textnodeGrossAmounte);

                XmlNode nameNodeGrossAmount = doc.CreateElement("Row");
                string GrossAmounterow = dgvpagefooter.Rows[1].Cells[2].Value.ToString();
                nameNodeGrossAmount.AppendChild(doc.CreateTextNode(GrossAmounterow));
                GrossAmounteItemNode.AppendChild(nameNodeGrossAmount);


                XmlNode priceNodeGrossAmount = doc.CreateElement("Column");
                string GrossAmountecolumn = dgvpagefooter.Rows[1].Cells[3].Value.ToString();
                priceNodeGrossAmount.AppendChild(doc.CreateTextNode(GrossAmountecolumn));
                GrossAmounteItemNode.AppendChild(priceNodeGrossAmount);


                XmlNode fontnameGrossAmounte = doc.CreateElement("FontName");

                string GrossAmounteFontName = Convert.ToString(dgvpagefooter.Rows[1].Cells[4].FormattedValue.ToString());
                fontnameGrossAmounte.AppendChild(doc.CreateTextNode(GrossAmounteFontName));
                GrossAmounteItemNode.AppendChild(fontnameGrossAmounte);



                XmlNode fontsizeGrossAmounte = doc.CreateElement("FontSize");
                string GrossAmounteFontsize = Convert.ToString(dgvpagefooter.Rows[1].Cells[5].FormattedValue.ToString());
                fontsizeGrossAmounte.AppendChild(doc.CreateTextNode(GrossAmounteFontsize));
                GrossAmounteItemNode.AppendChild(fontsizeGrossAmounte);



                XmlNode FontBoldGrossAmounte = doc.CreateElement("FontBold");
                bool GrossAmounteFontBold = Convert.ToBoolean(dgvpagefooter.Rows[1].Cells[6].FormattedValue.ToString());
                FontBoldGrossAmounte.AppendChild(doc.CreateTextNode(Convert.ToString(GrossAmounteFontBold)));
                GrossAmounteItemNode.AppendChild(FontBoldGrossAmounte);


                XmlNode ShowGrossAmounte = doc.CreateElement("Show");
                bool GrossAmounteShow = Convert.ToBoolean(dgvpagefooter.Rows[1].Cells[7].FormattedValue.ToString());
                ShowGrossAmounte.AppendChild(doc.CreateTextNode(Convert.ToString(GrossAmounteShow)));
                GrossAmounteItemNode.AppendChild(ShowGrossAmounte);



                XmlNode ShowTextGrossAmounte = doc.CreateElement("ShowText");
                bool GrossAmounteShowText = Convert.ToBoolean(dgvpagefooter.Rows[1].Cells[8].FormattedValue.ToString());
                ShowTextGrossAmounte.AppendChild(doc.CreateTextNode(Convert.ToString(GrossAmounteShowText)));
                GrossAmounteItemNode.AppendChild(ShowTextGrossAmounte);

                //End GrossAmounteItem


                // start Narration Item//

                XmlNode NarrationItemNode = doc.CreateElement("NarrationItem");
                PageFooter.AppendChild(NarrationItemNode);



                XmlNode textnodeNarration = doc.CreateElement("Text");
                //textnodeNarration.AppendChild(doc.CreateElement(""));
                string Narrationtext = dgvpagefooter.Rows[2].Cells[1].Value.ToString();
                textnodeNarration.AppendChild(doc.CreateTextNode(Narrationtext));
                NarrationItemNode.AppendChild(textnodeNarration);

                XmlNode nameNodeNarration = doc.CreateElement("Row");
                string Narrationrow = dgvpagefooter.Rows[2].Cells[2].Value.ToString();
                nameNodeNarration.AppendChild(doc.CreateTextNode(Narrationrow));
                NarrationItemNode.AppendChild(nameNodeNarration);


                XmlNode priceNodeNarration = doc.CreateElement("Column");
                string Narrationcolumn = dgvpagefooter.Rows[2].Cells[3].Value.ToString();
                priceNodeNarration.AppendChild(doc.CreateTextNode(Narrationcolumn));
                NarrationItemNode.AppendChild(priceNodeNarration);


                XmlNode fontnameNarration = doc.CreateElement("FontName");

                string NarrationFontName = Convert.ToString(dgvpagefooter.Rows[2].Cells[4].FormattedValue.ToString());
                fontnameNarration.AppendChild(doc.CreateTextNode(NarrationFontName));
                NarrationItemNode.AppendChild(fontnameNarration);



                XmlNode fontsizeNarration = doc.CreateElement("FontSize");
                string NarrationFontsize = Convert.ToString(dgvpagefooter.Rows[2].Cells[5].FormattedValue.ToString());
                fontsizeNarration.AppendChild(doc.CreateTextNode(NarrationFontsize));
                NarrationItemNode.AppendChild(fontsizeNarration);



                XmlNode FontBoldNarration = doc.CreateElement("FontBold");
                bool NarrationFontBold = Convert.ToBoolean(dgvpagefooter.Rows[2].Cells[6].FormattedValue.ToString());
                FontBoldNarration.AppendChild(doc.CreateTextNode(Convert.ToString(NarrationFontBold)));
                NarrationItemNode.AppendChild(FontBoldNarration);


                XmlNode ShowNarration = doc.CreateElement("Show");
                bool NarrationShow = Convert.ToBoolean(dgvpagefooter.Rows[2].Cells[7].FormattedValue.ToString());
                ShowNarration.AppendChild(doc.CreateTextNode(Convert.ToString(NarrationShow)));
                NarrationItemNode.AppendChild(ShowNarration);



                XmlNode ShowTextNarration = doc.CreateElement("ShowText");
                bool NarrationShowText = Convert.ToBoolean(dgvpagefooter.Rows[2].Cells[8].FormattedValue.ToString());
                ShowTextNarration.AppendChild(doc.CreateTextNode(Convert.ToString(NarrationShowText)));
                NarrationItemNode.AppendChild(ShowTextNarration);

                //End NarrationItem


                // start CreditNote Item//

                XmlNode CreditNoteItemNode = doc.CreateElement("CreditNoteItem");
                PageFooter.AppendChild(CreditNoteItemNode);



                XmlNode textnodeCreditNote = doc.CreateElement("Text");
                //textnodeCreditNote.AppendChild(doc.CreateElement(""));
                string CreditNotetext = dgvpagefooter.Rows[3].Cells[1].Value.ToString();
                textnodeCreditNote.AppendChild(doc.CreateTextNode(CreditNotetext));
                CreditNoteItemNode.AppendChild(textnodeCreditNote);

                XmlNode nameNodeCreditNote = doc.CreateElement("Row");
                string CreditNoterow = dgvpagefooter.Rows[3].Cells[2].Value.ToString();
                nameNodeCreditNote.AppendChild(doc.CreateTextNode(CreditNoterow));
                CreditNoteItemNode.AppendChild(nameNodeCreditNote);


                XmlNode priceNodeCreditNote = doc.CreateElement("Column");
                string CreditNotecolumn = dgvpagefooter.Rows[3].Cells[3].Value.ToString();
                priceNodeCreditNote.AppendChild(doc.CreateTextNode(CreditNotecolumn));
                CreditNoteItemNode.AppendChild(priceNodeCreditNote);


                XmlNode fontnameCreditNote = doc.CreateElement("FontName");

                string CreditNoteFontName = Convert.ToString(dgvpagefooter.Rows[3].Cells[4].FormattedValue.ToString());
                fontnameCreditNote.AppendChild(doc.CreateTextNode(CreditNoteFontName));
                CreditNoteItemNode.AppendChild(fontnameCreditNote);



                XmlNode fontsizeCreditNote = doc.CreateElement("FontSize");
                string CreditNoteFontsize = Convert.ToString(dgvpagefooter.Rows[3].Cells[5].FormattedValue.ToString());
                fontsizeCreditNote.AppendChild(doc.CreateTextNode(CreditNoteFontsize));
                CreditNoteItemNode.AppendChild(fontsizeCreditNote);



                XmlNode FontBoldCreditNote = doc.CreateElement("FontBold");
                bool CreditNoteFontBold = Convert.ToBoolean(dgvpagefooter.Rows[3].Cells[6].FormattedValue.ToString());
                FontBoldCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(CreditNoteFontBold)));
                CreditNoteItemNode.AppendChild(FontBoldCreditNote);


                XmlNode ShowCreditNote = doc.CreateElement("Show");
                bool CreditNoteShow = Convert.ToBoolean(dgvpagefooter.Rows[3].Cells[7].FormattedValue.ToString());
                ShowCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(CreditNoteShow)));
                CreditNoteItemNode.AppendChild(ShowCreditNote);



                XmlNode ShowTextCreditNote = doc.CreateElement("ShowText");
                bool CreditNoteShowText = Convert.ToBoolean(dgvpagefooter.Rows[3].Cells[8].FormattedValue.ToString());
                ShowTextCreditNote.AppendChild(doc.CreateTextNode(Convert.ToString(CreditNoteShowText)));
                CreditNoteItemNode.AppendChild(ShowTextCreditNote);

                //End CreditNoteItem


                // start DebitNote Item//

                XmlNode DebitNoteItemNode = doc.CreateElement("DebitNoteItem");
                PageFooter.AppendChild(DebitNoteItemNode);



                XmlNode textnodeDebitNote = doc.CreateElement("Text");
                //textnodeDebitNote.AppendChild(doc.CreateElement(""));
                string DebitNotetext = dgvpagefooter.Rows[4].Cells[1].Value.ToString();
                textnodeDebitNote.AppendChild(doc.CreateTextNode(DebitNotetext));
                DebitNoteItemNode.AppendChild(textnodeDebitNote);

                XmlNode nameNodeDebitNote = doc.CreateElement("Row");
                string DebitNoterow = dgvpagefooter.Rows[4].Cells[2].Value.ToString();
                nameNodeDebitNote.AppendChild(doc.CreateTextNode(DebitNoterow));
                DebitNoteItemNode.AppendChild(nameNodeDebitNote);


                XmlNode priceNodeDebitNote = doc.CreateElement("Column");
                string DebitNotecolumn = dgvpagefooter.Rows[4].Cells[3].Value.ToString();
                priceNodeDebitNote.AppendChild(doc.CreateTextNode(DebitNotecolumn));
                DebitNoteItemNode.AppendChild(priceNodeDebitNote);


                XmlNode fontnameDebitNote = doc.CreateElement("FontName");

                string DebitNoteFontName = Convert.ToString(dgvpagefooter.Rows[4].Cells[4].FormattedValue.ToString());
                fontnameDebitNote.AppendChild(doc.CreateTextNode(DebitNoteFontName));
                DebitNoteItemNode.AppendChild(fontnameDebitNote);



                XmlNode fontsizeDebitNote = doc.CreateElement("FontSize");
                string DebitNoteFontsize = Convert.ToString(dgvpagefooter.Rows[4].Cells[5].FormattedValue.ToString());
                fontsizeDebitNote.AppendChild(doc.CreateTextNode(DebitNoteFontsize));
                DebitNoteItemNode.AppendChild(fontsizeDebitNote);



                XmlNode FontBoldDebitNote = doc.CreateElement("FontBold");
                bool DebitNoteFontBold = Convert.ToBoolean(dgvpagefooter.Rows[4].Cells[6].FormattedValue.ToString());
                FontBoldDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(DebitNoteFontBold)));
                DebitNoteItemNode.AppendChild(FontBoldDebitNote);


                XmlNode ShowDebitNote = doc.CreateElement("Show");
                bool DebitNoteShow = Convert.ToBoolean(dgvpagefooter.Rows[4].Cells[7].FormattedValue.ToString());
                ShowDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(DebitNoteShow)));
                DebitNoteItemNode.AppendChild(ShowDebitNote);



                XmlNode ShowTextDebitNote = doc.CreateElement("ShowText");
                bool DebitNoteShowText = Convert.ToBoolean(dgvpagefooter.Rows[4].Cells[8].FormattedValue.ToString());
                ShowTextDebitNote.AppendChild(doc.CreateTextNode(Convert.ToString(DebitNoteShowText)));
                DebitNoteItemNode.AppendChild(ShowTextDebitNote);

                //End DebitNoteItem


                // start BalanceAmount Item//

                XmlNode BalanceAmountItemNode = doc.CreateElement("BalanceAmountItem");
                PageFooter.AppendChild(BalanceAmountItemNode);

                XmlNode textnodeBalanceAmount = doc.CreateElement("Text");
                //textnodeBalanceAmount.AppendChild(doc.CreateElement(""));
                string BalanceAmounttext = dgvpagefooter.Rows[5].Cells[1].Value.ToString();
                textnodeBalanceAmount.AppendChild(doc.CreateTextNode(BalanceAmounttext));
                BalanceAmountItemNode.AppendChild(textnodeBalanceAmount);

                XmlNode nameNodeBalanceAmount = doc.CreateElement("Row");
                string BalanceAmountrow = dgvpagefooter.Rows[5].Cells[2].Value.ToString();
                nameNodeBalanceAmount.AppendChild(doc.CreateTextNode(BalanceAmountrow));
                BalanceAmountItemNode.AppendChild(nameNodeBalanceAmount);


                XmlNode priceNodeBalanceAmount = doc.CreateElement("Column");
                string BalanceAmountcolumn = dgvpagefooter.Rows[5].Cells[3].Value.ToString();
                priceNodeBalanceAmount.AppendChild(doc.CreateTextNode(BalanceAmountcolumn));
                BalanceAmountItemNode.AppendChild(priceNodeBalanceAmount);


                XmlNode fontnameBalanceAmount = doc.CreateElement("FontName");

                string BalanceAmountFontName = Convert.ToString(dgvpagefooter.Rows[5].Cells[4].FormattedValue.ToString());
                fontnameBalanceAmount.AppendChild(doc.CreateTextNode(BalanceAmountFontName));
                BalanceAmountItemNode.AppendChild(fontnameBalanceAmount);



                XmlNode fontsizeBalanceAmount = doc.CreateElement("FontSize");
                string BalanceAmountFontsize = Convert.ToString(dgvpagefooter.Rows[5].Cells[5].FormattedValue.ToString());
                fontsizeBalanceAmount.AppendChild(doc.CreateTextNode(BalanceAmountFontsize));
                BalanceAmountItemNode.AppendChild(fontsizeBalanceAmount);



                XmlNode FontBoldBalanceAmount = doc.CreateElement("FontBold");
                bool BalanceAmountFontBold = Convert.ToBoolean(dgvpagefooter.Rows[5].Cells[6].FormattedValue.ToString());
                FontBoldBalanceAmount.AppendChild(doc.CreateTextNode(Convert.ToString(BalanceAmountFontBold)));
                BalanceAmountItemNode.AppendChild(FontBoldBalanceAmount);


                XmlNode ShowBalanceAmount = doc.CreateElement("Show");
                bool BalanceAmountShow = Convert.ToBoolean(dgvpagefooter.Rows[5].Cells[7].FormattedValue.ToString());
                ShowBalanceAmount.AppendChild(doc.CreateTextNode(Convert.ToString(BalanceAmountShow)));
                BalanceAmountItemNode.AppendChild(ShowBalanceAmount);



                XmlNode ShowTextBalanceAmount = doc.CreateElement("ShowText");
                bool BalanceAmountShowText = Convert.ToBoolean(dgvpagefooter.Rows[5].Cells[8].FormattedValue.ToString());
                ShowTextBalanceAmount.AppendChild(doc.CreateTextNode(Convert.ToString(BalanceAmountShowText)));
                BalanceAmountItemNode.AppendChild(ShowTextBalanceAmount);

                //End BalanceAmountItem


                // start NetAmount Item//

                XmlNode NetAmountItemNode = doc.CreateElement("NetAmountItem");
                PageFooter.AppendChild(NetAmountItemNode);



                XmlNode textnodeNetAmount = doc.CreateElement("Text");
                //textnodeNetAmount.AppendChild(doc.CreateElement(""));
                string NetAmounttext = dgvpagefooter.Rows[6].Cells[1].Value.ToString();
                textnodeNetAmount.AppendChild(doc.CreateTextNode(NetAmounttext));
                NetAmountItemNode.AppendChild(textnodeNetAmount);

                XmlNode nameNodeNetAmount = doc.CreateElement("Row");
                string NetAmountrow = dgvpagefooter.Rows[6].Cells[2].Value.ToString();
                nameNodeNetAmount.AppendChild(doc.CreateTextNode(NetAmountrow));
                NetAmountItemNode.AppendChild(nameNodeNetAmount);


                XmlNode priceNodeNetAmount = doc.CreateElement("Column");
                string NetAmountcolumn = dgvpagefooter.Rows[6].Cells[3].Value.ToString();
                priceNodeNetAmount.AppendChild(doc.CreateTextNode(NetAmountcolumn));
                NetAmountItemNode.AppendChild(priceNodeNetAmount);


                XmlNode fontnameNetAmount = doc.CreateElement("FontName");

                string NetAmountFontName = Convert.ToString(dgvpagefooter.Rows[6].Cells[4].FormattedValue.ToString());
                fontnameNetAmount.AppendChild(doc.CreateTextNode(NetAmountFontName));
                NetAmountItemNode.AppendChild(fontnameNetAmount);



                XmlNode fontsizeNetAmount = doc.CreateElement("FontSize");
                string NetAmountFontsize = Convert.ToString(dgvpagefooter.Rows[6].Cells[5].FormattedValue.ToString());
                fontsizeNetAmount.AppendChild(doc.CreateTextNode(NetAmountFontsize));
                NetAmountItemNode.AppendChild(fontsizeNetAmount);



                XmlNode FontBoldNetAmount = doc.CreateElement("FontBold");
                bool NetAmountFontBold = Convert.ToBoolean(dgvpagefooter.Rows[6].Cells[6].FormattedValue.ToString());
                FontBoldNetAmount.AppendChild(doc.CreateTextNode(Convert.ToString(NetAmountFontBold)));
                NetAmountItemNode.AppendChild(FontBoldNetAmount);


                XmlNode ShowNetAmount = doc.CreateElement("Show");
                bool NetAmountShow = Convert.ToBoolean(dgvpagefooter.Rows[6].Cells[7].FormattedValue.ToString());
                ShowNetAmount.AppendChild(doc.CreateTextNode(Convert.ToString(NetAmountShow)));
                NetAmountItemNode.AppendChild(ShowNetAmount);



                XmlNode ShowTextNetAmount = doc.CreateElement("ShowText");
                bool NetAmountShowText = Convert.ToBoolean(dgvpagefooter.Rows[6].Cells[8].FormattedValue.ToString());
                ShowTextNetAmount.AppendChild(doc.CreateTextNode(Convert.ToString(NetAmountShowText)));
                NetAmountItemNode.AppendChild(ShowTextNetAmount);

                //End NetAmountItem


                // start SubjectItem Item//

                XmlNode SubjectItemItemNode = doc.CreateElement("SubjectItemItem");
                PageFooter.AppendChild(SubjectItemItemNode);



                XmlNode textnodeSubjectItem = doc.CreateElement("Text");
                //textnodeSubjectItem.AppendChild(doc.CreateElement(""));
                string SubjectItemtext = dgvpagefooter.Rows[7].Cells[1].Value.ToString();
                textnodeSubjectItem.AppendChild(doc.CreateTextNode(SubjectItemtext));
                SubjectItemItemNode.AppendChild(textnodeSubjectItem);

                XmlNode nameNodeSubjectItem = doc.CreateElement("Row");
                string SubjectItemrow = dgvpagefooter.Rows[7].Cells[2].Value.ToString();
                nameNodeSubjectItem.AppendChild(doc.CreateTextNode(SubjectItemrow));
                SubjectItemItemNode.AppendChild(nameNodeSubjectItem);


                XmlNode priceNodeSubjectItem = doc.CreateElement("Column");
                string SubjectItemcolumn = dgvpagefooter.Rows[7].Cells[3].Value.ToString();
                priceNodeSubjectItem.AppendChild(doc.CreateTextNode(SubjectItemcolumn));
                SubjectItemItemNode.AppendChild(priceNodeSubjectItem);


                XmlNode fontnameSubjectItem = doc.CreateElement("FontName");

                string SubjectItemFontName = Convert.ToString(dgvpagefooter.Rows[7].Cells[4].FormattedValue.ToString());
                fontnameSubjectItem.AppendChild(doc.CreateTextNode(SubjectItemFontName));
                SubjectItemItemNode.AppendChild(fontnameSubjectItem);



                XmlNode fontsizeSubjectItem = doc.CreateElement("FontSize");
                string SubjectItemFontsize = Convert.ToString(dgvpagefooter.Rows[7].Cells[5].FormattedValue.ToString());
                fontsizeSubjectItem.AppendChild(doc.CreateTextNode(SubjectItemFontsize));
                SubjectItemItemNode.AppendChild(fontsizeSubjectItem);



                XmlNode FontBoldSubjectItem = doc.CreateElement("FontBold");
                bool SubjectItemFontBold = Convert.ToBoolean(dgvpagefooter.Rows[7].Cells[6].FormattedValue.ToString());
                FontBoldSubjectItem.AppendChild(doc.CreateTextNode(Convert.ToString(SubjectItemFontBold)));
                SubjectItemItemNode.AppendChild(FontBoldSubjectItem);


                XmlNode ShowSubjectItem = doc.CreateElement("Show");
                bool SubjectItemShow = Convert.ToBoolean(dgvpagefooter.Rows[7].Cells[7].FormattedValue.ToString());
                ShowSubjectItem.AppendChild(doc.CreateTextNode(Convert.ToString(SubjectItemShow)));
                SubjectItemItemNode.AppendChild(ShowSubjectItem);



                XmlNode ShowTextSubjectItem = doc.CreateElement("ShowText");
                bool SubjectItemShowText = Convert.ToBoolean(dgvpagefooter.Rows[7].Cells[8].FormattedValue.ToString());
                ShowTextSubjectItem.AppendChild(doc.CreateTextNode(Convert.ToString(SubjectItemShowText)));
                SubjectItemItemNode.AppendChild(ShowTextSubjectItem);

                //End SubjectItemItem

                // start Jurisdiction Item//

                XmlNode JurisdictionItemNode = doc.CreateElement("JurisdictionItem");
                PageFooter.AppendChild(JurisdictionItemNode);

                XmlNode textnodeJurisdiction = doc.CreateElement("Text");
                //textnodeJurisdiction.AppendChild(doc.CreateElement(""));
                string Jurisdictiontext = dgvpagefooter.Rows[8].Cells[1].Value.ToString();
                textnodeJurisdiction.AppendChild(doc.CreateTextNode(Jurisdictiontext));
                JurisdictionItemNode.AppendChild(textnodeJurisdiction);

                XmlNode nameNodeJurisdiction = doc.CreateElement("Row");
                string Jurisdictionrow = dgvpagefooter.Rows[8].Cells[2].Value.ToString();
                nameNodeJurisdiction.AppendChild(doc.CreateTextNode(Jurisdictionrow));
                JurisdictionItemNode.AppendChild(nameNodeJurisdiction);


                XmlNode priceNodeJurisdiction = doc.CreateElement("Column");
                string Jurisdictioncolumn = dgvpagefooter.Rows[8].Cells[3].Value.ToString();
                priceNodeJurisdiction.AppendChild(doc.CreateTextNode(Jurisdictioncolumn));
                JurisdictionItemNode.AppendChild(priceNodeJurisdiction);


                XmlNode fontnameJurisdiction = doc.CreateElement("FontName");

                string JurisdictionFontName = Convert.ToString(dgvpagefooter.Rows[8].Cells[4].FormattedValue.ToString());
                fontnameJurisdiction.AppendChild(doc.CreateTextNode(JurisdictionFontName));
                JurisdictionItemNode.AppendChild(fontnameJurisdiction);



                XmlNode fontsizeJurisdiction = doc.CreateElement("FontSize");
                string JurisdictionFontsize = Convert.ToString(dgvpagefooter.Rows[8].Cells[5].FormattedValue.ToString());
                fontsizeJurisdiction.AppendChild(doc.CreateTextNode(JurisdictionFontsize));
                JurisdictionItemNode.AppendChild(fontsizeJurisdiction);



                XmlNode FontBoldJurisdiction = doc.CreateElement("FontBold");
                bool JurisdictionFontBold = Convert.ToBoolean(dgvpagefooter.Rows[8].Cells[6].FormattedValue.ToString());
                FontBoldJurisdiction.AppendChild(doc.CreateTextNode(Convert.ToString(JurisdictionFontBold)));
                JurisdictionItemNode.AppendChild(FontBoldJurisdiction);


                XmlNode ShowJurisdiction = doc.CreateElement("Show");
                bool JurisdictionShow = Convert.ToBoolean(dgvpagefooter.Rows[8].Cells[7].FormattedValue.ToString());
                ShowJurisdiction.AppendChild(doc.CreateTextNode(Convert.ToString(JurisdictionShow)));
                JurisdictionItemNode.AppendChild(ShowJurisdiction);



                XmlNode ShowTextJurisdiction = doc.CreateElement("ShowText");
                bool JurisdictionShowText = Convert.ToBoolean(dgvpagefooter.Rows[8].Cells[8].FormattedValue.ToString());
                ShowTextJurisdiction.AppendChild(doc.CreateTextNode(Convert.ToString(JurisdictionShowText)));
                JurisdictionItemNode.AppendChild(ShowTextJurisdiction);

                //End JurisdictionItem

                // start ATOW Item//

                XmlNode ATOWItemNode = doc.CreateElement("ATOWItem");
                PageFooter.AppendChild(ATOWItemNode);



                XmlNode textnodeATOW = doc.CreateElement("Text");
                //textnodeATOW.AppendChild(doc.CreateElement(""));
                string ATOWtext = dgvpagefooter.Rows[9].Cells[1].Value.ToString();
                textnodeATOW.AppendChild(doc.CreateTextNode(ATOWtext));
                ATOWItemNode.AppendChild(textnodeATOW);

                XmlNode nameNodeATOW = doc.CreateElement("Row");
                string ATOWrow = dgvpagefooter.Rows[9].Cells[2].Value.ToString();
                nameNodeATOW.AppendChild(doc.CreateTextNode(ATOWrow));
                ATOWItemNode.AppendChild(nameNodeATOW);


                XmlNode priceNodeATOW = doc.CreateElement("Column");
                string ATOWcolumn = dgvpagefooter.Rows[9].Cells[3].Value.ToString();
                priceNodeATOW.AppendChild(doc.CreateTextNode(ATOWcolumn));
                ATOWItemNode.AppendChild(priceNodeATOW);


                XmlNode fontnameATOW = doc.CreateElement("FontName");

                string ATOWFontName = Convert.ToString(dgvpagefooter.Rows[9].Cells[4].FormattedValue.ToString());
                fontnameATOW.AppendChild(doc.CreateTextNode(ATOWFontName));
                ATOWItemNode.AppendChild(fontnameATOW);



                XmlNode fontsizeATOW = doc.CreateElement("FontSize");
                string ATOWFontsize = Convert.ToString(dgvpagefooter.Rows[9].Cells[5].FormattedValue.ToString());
                fontsizeATOW.AppendChild(doc.CreateTextNode(ATOWFontsize));
                ATOWItemNode.AppendChild(fontsizeATOW);



                XmlNode FontBoldATOW = doc.CreateElement("FontBold");
                bool ATOWFontBold = Convert.ToBoolean(dgvpagefooter.Rows[9].Cells[6].FormattedValue.ToString());
                FontBoldATOW.AppendChild(doc.CreateTextNode(Convert.ToString(ATOWFontBold)));
                ATOWItemNode.AppendChild(FontBoldATOW);


                XmlNode ShowATOW = doc.CreateElement("Show");
                bool ATOWShow = Convert.ToBoolean(dgvpagefooter.Rows[9].Cells[7].FormattedValue.ToString());
                ShowATOW.AppendChild(doc.CreateTextNode(Convert.ToString(ATOWShow)));
                ATOWItemNode.AppendChild(ShowATOW);



                XmlNode ShowTextATOW = doc.CreateElement("ShowText");
                bool ATOWShowText = Convert.ToBoolean(dgvpagefooter.Rows[9].Cells[8].FormattedValue.ToString());
                ShowTextATOW.AppendChild(doc.CreateTextNode(Convert.ToString(ATOWShowText)));
                ATOWItemNode.AppendChild(ShowTextATOW);

                //End ATOWItem

                // start DLN Item//

                XmlNode DLNItemNode = doc.CreateElement("DLNItem");
                PageFooter.AppendChild(DLNItemNode);



                XmlNode textnodeDLN = doc.CreateElement("Text");
                //textnodeDLN.AppendChild(doc.CreateElement(""));
                string DLNtext = dgvpagefooter.Rows[10].Cells[1].Value.ToString();
                textnodeDLN.AppendChild(doc.CreateTextNode(DLNtext));
                DLNItemNode.AppendChild(textnodeDLN);

                XmlNode nameNodeDLN = doc.CreateElement("Row");
                string DLNrow = dgvpagefooter.Rows[10].Cells[2].Value.ToString();
                nameNodeDLN.AppendChild(doc.CreateTextNode(DLNrow));
                DLNItemNode.AppendChild(nameNodeDLN);


                XmlNode priceNodeDLN = doc.CreateElement("Column");
                string DLNcolumn = dgvpagefooter.Rows[10].Cells[3].Value.ToString();
                priceNodeDLN.AppendChild(doc.CreateTextNode(DLNcolumn));
                DLNItemNode.AppendChild(priceNodeDLN);


                XmlNode fontnameDLN = doc.CreateElement("FontName");

                string DLNFontName = Convert.ToString(dgvpagefooter.Rows[10].Cells[4].FormattedValue.ToString());
                fontnameDLN.AppendChild(doc.CreateTextNode(DLNFontName));
                DLNItemNode.AppendChild(fontnameDLN);



                XmlNode fontsizeDLN = doc.CreateElement("FontSize");
                string DLNFontsize = Convert.ToString(dgvpagefooter.Rows[10].Cells[5].FormattedValue.ToString());
                fontsizeDLN.AppendChild(doc.CreateTextNode(DLNFontsize));
                DLNItemNode.AppendChild(fontsizeDLN);



                XmlNode FontBoldDLN = doc.CreateElement("FontBold");
                bool DLNFontBold = Convert.ToBoolean(dgvpagefooter.Rows[10].Cells[6].FormattedValue.ToString());
                FontBoldDLN.AppendChild(doc.CreateTextNode(Convert.ToString(DLNFontBold)));
                DLNItemNode.AppendChild(FontBoldDLN);


                XmlNode ShowDLN = doc.CreateElement("Show");
                bool DLNShow = Convert.ToBoolean(dgvpagefooter.Rows[10].Cells[7].FormattedValue.ToString());
                ShowDLN.AppendChild(doc.CreateTextNode(Convert.ToString(DLNShow)));
                DLNItemNode.AppendChild(ShowDLN);



                XmlNode ShowTextDLN = doc.CreateElement("ShowText");
                bool DLNShowText = Convert.ToBoolean(dgvpagefooter.Rows[10].Cells[8].FormattedValue.ToString());
                ShowTextDLN.AppendChild(doc.CreateTextNode(Convert.ToString(DLNShowText)));
                DLNItemNode.AppendChild(ShowTextDLN);

                //End DLNItem
                // start VATTIN Item//

                XmlNode VATTINItemNode = doc.CreateElement("VATTINItem");
                PageFooter.AppendChild(VATTINItemNode);



                XmlNode textnodeVATTIN = doc.CreateElement("Text");
                //textnodeVATTIN.AppendChild(doc.CreateElement(""));
                string VATTINtext = dgvpagefooter.Rows[11].Cells[1].Value.ToString();
                textnodeVATTIN.AppendChild(doc.CreateTextNode(VATTINtext));
                VATTINItemNode.AppendChild(textnodeVATTIN);

                XmlNode nameNodeVATTIN = doc.CreateElement("Row");
                string VATTINrow = dgvpagefooter.Rows[11].Cells[2].Value.ToString();
                nameNodeVATTIN.AppendChild(doc.CreateTextNode(VATTINrow));
                VATTINItemNode.AppendChild(nameNodeVATTIN);


                XmlNode priceNodeVATTIN = doc.CreateElement("Column");
                string VATTINcolumn = dgvpagefooter.Rows[11].Cells[3].Value.ToString();
                priceNodeVATTIN.AppendChild(doc.CreateTextNode(VATTINcolumn));
                VATTINItemNode.AppendChild(priceNodeVATTIN);


                XmlNode fontnameVATTIN = doc.CreateElement("FontName");

                string VATTINFontName = Convert.ToString(dgvpagefooter.Rows[11].Cells[4].FormattedValue.ToString());
                fontnameVATTIN.AppendChild(doc.CreateTextNode(VATTINFontName));
                VATTINItemNode.AppendChild(fontnameVATTIN);



                XmlNode fontsizeVATTIN = doc.CreateElement("FontSize");
                string VATTINFontsize = Convert.ToString(dgvpagefooter.Rows[11].Cells[5].FormattedValue.ToString());
                fontsizeVATTIN.AppendChild(doc.CreateTextNode(VATTINFontsize));
                VATTINItemNode.AppendChild(fontsizeVATTIN);



                XmlNode FontBoldVATTIN = doc.CreateElement("FontBold");
                bool VATTINFontBold = Convert.ToBoolean(dgvpagefooter.Rows[11].Cells[6].FormattedValue.ToString());
                FontBoldVATTIN.AppendChild(doc.CreateTextNode(Convert.ToString(VATTINFontBold)));
                VATTINItemNode.AppendChild(FontBoldVATTIN);


                XmlNode ShowVATTIN = doc.CreateElement("Show");
                bool VATTINShow = Convert.ToBoolean(dgvpagefooter.Rows[11].Cells[7].FormattedValue.ToString());
                ShowVATTIN.AppendChild(doc.CreateTextNode(Convert.ToString(VATTINShow)));
                VATTINItemNode.AppendChild(ShowVATTIN);



                XmlNode ShowTextVATTIN = doc.CreateElement("ShowText");
                bool VATTINShowText = Convert.ToBoolean(dgvpagefooter.Rows[11].Cells[8].FormattedValue.ToString());
                ShowTextVATTIN.AppendChild(doc.CreateTextNode(Convert.ToString(VATTINShowText)));
                VATTINItemNode.AppendChild(ShowTextVATTIN);

                //End VATTINItem

                // start DeclarationItem1 Item//

                XmlNode DeclarationItem1ItemNode = doc.CreateElement("DeclarationItem1");
                PageFooter.AppendChild(DeclarationItem1ItemNode);



                XmlNode textnodeDeclarationItem1 = doc.CreateElement("Text");
                //textnodeDeclarationItem1.AppendChild(doc.CreateElement(""));
                string DeclarationItem1text = dgvpagefooter.Rows[12].Cells[1].Value.ToString();
                textnodeDeclarationItem1.AppendChild(doc.CreateTextNode(DeclarationItem1text));
                DeclarationItem1ItemNode.AppendChild(textnodeDeclarationItem1);

                XmlNode nameNodeDeclarationItem1 = doc.CreateElement("Row");
                string DeclarationItem1row = dgvpagefooter.Rows[12].Cells[2].Value.ToString();
                nameNodeDeclarationItem1.AppendChild(doc.CreateTextNode(DeclarationItem1row));
                DeclarationItem1ItemNode.AppendChild(nameNodeDeclarationItem1);


                XmlNode priceNodeDeclarationItem1 = doc.CreateElement("Column");
                string DeclarationItem1column = dgvpagefooter.Rows[12].Cells[3].Value.ToString();
                priceNodeDeclarationItem1.AppendChild(doc.CreateTextNode(DeclarationItem1column));
                DeclarationItem1ItemNode.AppendChild(priceNodeDeclarationItem1);


                XmlNode fontnameDeclarationItem1 = doc.CreateElement("FontName");

                string DeclarationItem1FontName = Convert.ToString(dgvpagefooter.Rows[12].Cells[4].FormattedValue.ToString());
                fontnameDeclarationItem1.AppendChild(doc.CreateTextNode(DeclarationItem1FontName));
                DeclarationItem1ItemNode.AppendChild(fontnameDeclarationItem1);



                XmlNode fontsizeDeclarationItem1 = doc.CreateElement("FontSize");
                string DeclarationItem1Fontsize = Convert.ToString(dgvpagefooter.Rows[12].Cells[5].FormattedValue.ToString());
                fontsizeDeclarationItem1.AppendChild(doc.CreateTextNode(DeclarationItem1Fontsize));
                DeclarationItem1ItemNode.AppendChild(fontsizeDeclarationItem1);



                XmlNode FontBoldDeclarationItem1 = doc.CreateElement("FontBold");
                bool DeclarationItem1FontBold = Convert.ToBoolean(dgvpagefooter.Rows[12].Cells[6].FormattedValue.ToString());
                FontBoldDeclarationItem1.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem1FontBold)));
                DeclarationItem1ItemNode.AppendChild(FontBoldDeclarationItem1);


                XmlNode ShowDeclarationItem1 = doc.CreateElement("Show");
                bool DeclarationItem1Show = Convert.ToBoolean(dgvpagefooter.Rows[12].Cells[7].FormattedValue.ToString());
                ShowDeclarationItem1.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem1Show)));
                DeclarationItem1ItemNode.AppendChild(ShowDeclarationItem1);



                XmlNode ShowTextDeclarationItem1 = doc.CreateElement("ShowText");
                bool DeclarationItem1ShowText = Convert.ToBoolean(dgvpagefooter.Rows[12].Cells[8].FormattedValue.ToString());
                ShowTextDeclarationItem1.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem1ShowText)));
                DeclarationItem1ItemNode.AppendChild(ShowTextDeclarationItem1);

                //End DeclarationItem1Item

                // start DeclarationItem2 Item//

                XmlNode DeclarationItem2ItemNode = doc.CreateElement("DeclarationItem2");
                PageFooter.AppendChild(DeclarationItem2ItemNode);



                XmlNode textnodeDeclarationItem2 = doc.CreateElement("Text");
                //textnodeDeclarationItem2.AppendChild(doc.CreateElement(""));
                string DeclarationItem2text = dgvpagefooter.Rows[13].Cells[1].Value.ToString();
                textnodeDeclarationItem2.AppendChild(doc.CreateTextNode(DeclarationItem2text));
                DeclarationItem2ItemNode.AppendChild(textnodeDeclarationItem2);

                XmlNode nameNodeDeclarationItem2 = doc.CreateElement("Row");
                string DeclarationItem2row = dgvpagefooter.Rows[13].Cells[2].Value.ToString();
                nameNodeDeclarationItem2.AppendChild(doc.CreateTextNode(DeclarationItem2row));
                DeclarationItem2ItemNode.AppendChild(nameNodeDeclarationItem2);


                XmlNode priceNodeDeclarationItem2 = doc.CreateElement("Column");
                string DeclarationItem2column = dgvpagefooter.Rows[13].Cells[3].Value.ToString();
                priceNodeDeclarationItem2.AppendChild(doc.CreateTextNode(DeclarationItem2column));
                DeclarationItem2ItemNode.AppendChild(priceNodeDeclarationItem2);


                XmlNode fontnameDeclarationItem2 = doc.CreateElement("FontName");

                string DeclarationItem2FontName = Convert.ToString(dgvpagefooter.Rows[13].Cells[4].FormattedValue.ToString());
                fontnameDeclarationItem2.AppendChild(doc.CreateTextNode(DeclarationItem2FontName));
                DeclarationItem2ItemNode.AppendChild(fontnameDeclarationItem2);



                XmlNode fontsizeDeclarationItem2 = doc.CreateElement("FontSize");
                string DeclarationItem2Fontsize = Convert.ToString(dgvpagefooter.Rows[13].Cells[5].FormattedValue.ToString());
                fontsizeDeclarationItem2.AppendChild(doc.CreateTextNode(DeclarationItem2Fontsize));
                DeclarationItem2ItemNode.AppendChild(fontsizeDeclarationItem2);



                XmlNode FontBoldDeclarationItem2 = doc.CreateElement("FontBold");
                bool DeclarationItem2FontBold = Convert.ToBoolean(dgvpagefooter.Rows[13].Cells[6].FormattedValue.ToString());
                FontBoldDeclarationItem2.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem2FontBold)));
                DeclarationItem2ItemNode.AppendChild(FontBoldDeclarationItem2);


                XmlNode ShowDeclarationItem2 = doc.CreateElement("Show");
                bool DeclarationItem2Show = Convert.ToBoolean(dgvpagefooter.Rows[13].Cells[7].FormattedValue.ToString());
                ShowDeclarationItem2.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem2Show)));
                DeclarationItem2ItemNode.AppendChild(ShowDeclarationItem2);



                XmlNode ShowTextDeclarationItem2 = doc.CreateElement("ShowText");
                bool DeclarationItem2ShowText = Convert.ToBoolean(dgvpagefooter.Rows[13].Cells[8].FormattedValue.ToString());
                ShowTextDeclarationItem2.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem2ShowText)));
                DeclarationItem2ItemNode.AppendChild(ShowTextDeclarationItem2);

                //End DeclarationItem2Item

                // start DeclarationItem3Item//

                XmlNode DeclarationItem3ItemNode = doc.CreateElement("DeclarationItem3");
                PageFooter.AppendChild(DeclarationItem3ItemNode);



                XmlNode textnodeDeclarationItem3 = doc.CreateElement("Text");
                //textnodeDeclarationItem3.AppendChild(doc.CreateElement(""));
                string DeclarationItem3text = dgvpagefooter.Rows[14].Cells[1].Value.ToString();
                textnodeDeclarationItem3.AppendChild(doc.CreateTextNode(DeclarationItem3text));
                DeclarationItem3ItemNode.AppendChild(textnodeDeclarationItem3);

                XmlNode nameNodeDeclarationItem3 = doc.CreateElement("Row");
                string DeclarationItem3row = dgvpagefooter.Rows[14].Cells[2].Value.ToString();
                nameNodeDeclarationItem3.AppendChild(doc.CreateTextNode(DeclarationItem3row));
                DeclarationItem3ItemNode.AppendChild(nameNodeDeclarationItem3);


                XmlNode priceNodeDeclarationItem3 = doc.CreateElement("Column");
                string DeclarationItem3column = dgvpagefooter.Rows[14].Cells[3].Value.ToString();
                priceNodeDeclarationItem3.AppendChild(doc.CreateTextNode(DeclarationItem3column));
                DeclarationItem3ItemNode.AppendChild(priceNodeDeclarationItem3);


                XmlNode fontnameDeclarationItem3 = doc.CreateElement("FontName");

                string DeclarationItem3FontName = Convert.ToString(dgvpagefooter.Rows[14].Cells[4].FormattedValue.ToString());
                fontnameDeclarationItem3.AppendChild(doc.CreateTextNode(DeclarationItem3FontName));
                DeclarationItem3ItemNode.AppendChild(fontnameDeclarationItem3);



                XmlNode fontsizeDeclarationItem3 = doc.CreateElement("FontSize");
                string DeclarationItem3Fontsize = Convert.ToString(dgvpagefooter.Rows[14].Cells[5].FormattedValue.ToString());
                fontsizeDeclarationItem3.AppendChild(doc.CreateTextNode(DeclarationItem3Fontsize));
                DeclarationItem3ItemNode.AppendChild(fontsizeDeclarationItem3);



                XmlNode FontBoldDeclarationItem3 = doc.CreateElement("FontBold");
                bool DeclarationItem3FontBold = Convert.ToBoolean(dgvpagefooter.Rows[14].Cells[6].FormattedValue.ToString());
                FontBoldDeclarationItem3.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem3FontBold)));
                DeclarationItem3ItemNode.AppendChild(FontBoldDeclarationItem3);


                XmlNode ShowDeclarationItem3 = doc.CreateElement("Show");
                bool DeclarationItem3Show = Convert.ToBoolean(dgvpagefooter.Rows[14].Cells[7].FormattedValue.ToString());
                ShowDeclarationItem3.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem3Show)));
                DeclarationItem3ItemNode.AppendChild(ShowDeclarationItem3);



                XmlNode ShowTextDeclarationItem3 = doc.CreateElement("ShowText");
                bool DeclarationItem3ShowText = Convert.ToBoolean(dgvpagefooter.Rows[14].Cells[8].FormattedValue.ToString());
                ShowTextDeclarationItem3.AppendChild(doc.CreateTextNode(Convert.ToString(DeclarationItem3ShowText)));
                DeclarationItem3ItemNode.AppendChild(ShowTextDeclarationItem3);

                //End DeclarationItem3Item

                // start shopname footer//
                XmlNode ShopNameFooterItemNode = doc.CreateElement("ShopNameItem");
                PageFooter.AppendChild(ShopNameFooterItemNode);

                XmlNode textnodeShopNameFooter = doc.CreateElement("Text");
                //textnodeShopNameFooter.AppendChild(doc.CreateElement(""));
                string ShopNameFootertext = dgvpagefooter.Rows[15].Cells[1].Value.ToString();
                textnodeShopNameFooter.AppendChild(doc.CreateTextNode(ShopNameFootertext));
                ShopNameFooterItemNode.AppendChild(textnodeShopNameFooter);

                XmlNode nameNodeShopNameFooter = doc.CreateElement("Row");
                string ShopNameFooterrow = dgvpagefooter.Rows[15].Cells[2].Value.ToString();
                nameNodeShopNameFooter.AppendChild(doc.CreateTextNode(ShopNameFooterrow));
                ShopNameFooterItemNode.AppendChild(nameNodeShopNameFooter);


                XmlNode priceNodeShopNameFooter = doc.CreateElement("Column");
                string ShopNameFootercolumn = dgvpagefooter.Rows[15].Cells[3].Value.ToString();
                priceNodeShopNameFooter.AppendChild(doc.CreateTextNode(ShopNameFootercolumn));
                ShopNameFooterItemNode.AppendChild(priceNodeShopNameFooter);


                XmlNode fontnameShopNameFooter = doc.CreateElement("FontName");

                string ShopNameFooterFontName = Convert.ToString(dgvpagefooter.Rows[15].Cells[4].FormattedValue.ToString());
                fontnameShopNameFooter.AppendChild(doc.CreateTextNode(ShopNameFooterFontName));
                ShopNameFooterItemNode.AppendChild(fontnameShopNameFooter);



                XmlNode fontsizeShopNameFooter = doc.CreateElement("FontSize");
                string ShopNameFooterFontsize = Convert.ToString(dgvpagefooter.Rows[15].Cells[5].FormattedValue.ToString());
                fontsizeShopNameFooter.AppendChild(doc.CreateTextNode(ShopNameFooterFontsize));
                ShopNameFooterItemNode.AppendChild(fontsizeShopNameFooter);



                XmlNode FontBoldShopNameFooter = doc.CreateElement("FontBold");
                bool ShopNameFooterFontBold = Convert.ToBoolean(dgvpagefooter.Rows[15].Cells[6].FormattedValue.ToString());
                FontBoldShopNameFooter.AppendChild(doc.CreateTextNode(Convert.ToString(ShopNameFooterFontBold)));
                ShopNameFooterItemNode.AppendChild(FontBoldShopNameFooter);


                XmlNode ShowShopNameFooter = doc.CreateElement("Show");
                bool ShopNameFooterShow = Convert.ToBoolean(dgvpagefooter.Rows[15].Cells[7].FormattedValue.ToString());
                ShowShopNameFooter.AppendChild(doc.CreateTextNode(Convert.ToString(ShopNameFooterShow)));
                ShopNameFooterItemNode.AppendChild(ShowShopNameFooter);



                XmlNode ShowTextShopNameFooter = doc.CreateElement("ShowText");
                bool ShopNameFooterShowText = Convert.ToBoolean(dgvpagefooter.Rows[15].Cells[8].FormattedValue.ToString());
                ShowTextShopNameFooter.AppendChild(doc.CreateTextNode(Convert.ToString(ShopNameFooterShowText)));
                ShopNameFooterItemNode.AppendChild(ShowTextShopNameFooter);

                // end shop name footer


                // start SignatureItem//

                XmlNode SignatureItemNode = doc.CreateElement("SignatureItem");
                PageFooter.AppendChild(SignatureItemNode);



                XmlNode textnodeSignature = doc.CreateElement("Text");
                //textnodeSignature.AppendChild(doc.CreateElement(""));
                string Signaturetext = dgvpagefooter.Rows[16].Cells[1].Value.ToString();
                textnodeSignature.AppendChild(doc.CreateTextNode(Signaturetext));
                SignatureItemNode.AppendChild(textnodeSignature);

                XmlNode nameNodeSignature = doc.CreateElement("Row");
                string Signaturerow = dgvpagefooter.Rows[16].Cells[2].Value.ToString();
                nameNodeSignature.AppendChild(doc.CreateTextNode(Signaturerow));
                SignatureItemNode.AppendChild(nameNodeSignature);


                XmlNode priceNodeSignature = doc.CreateElement("Column");
                string Signaturecolumn = dgvpagefooter.Rows[16].Cells[3].Value.ToString();
                priceNodeSignature.AppendChild(doc.CreateTextNode(Signaturecolumn));
                SignatureItemNode.AppendChild(priceNodeSignature);


                XmlNode fontnameSignature = doc.CreateElement("FontName");

                string SignatureFontName = Convert.ToString(dgvpagefooter.Rows[16].Cells[4].FormattedValue.ToString());
                fontnameSignature.AppendChild(doc.CreateTextNode(SignatureFontName));
                SignatureItemNode.AppendChild(fontnameSignature);



                XmlNode fontsizeSignature = doc.CreateElement("FontSize");
                string SignatureFontsize = Convert.ToString(dgvpagefooter.Rows[16].Cells[5].FormattedValue.ToString());
                fontsizeSignature.AppendChild(doc.CreateTextNode(SignatureFontsize));
                SignatureItemNode.AppendChild(fontsizeSignature);



                XmlNode FontBoldSignature = doc.CreateElement("FontBold");
                bool SignatureFontBold = Convert.ToBoolean(dgvpagefooter.Rows[16].Cells[6].FormattedValue.ToString());
                FontBoldSignature.AppendChild(doc.CreateTextNode(Convert.ToString(SignatureFontBold)));
                SignatureItemNode.AppendChild(FontBoldSignature);


                XmlNode ShowSignature = doc.CreateElement("Show");
                bool SignatureShow = Convert.ToBoolean(dgvpagefooter.Rows[16].Cells[7].FormattedValue.ToString());
                ShowSignature.AppendChild(doc.CreateTextNode(Convert.ToString(SignatureShow)));
                SignatureItemNode.AppendChild(ShowSignature);



                XmlNode ShowTextSignature = doc.CreateElement("ShowText");
                bool SignatureShowText = Convert.ToBoolean(dgvpagefooter.Rows[16].Cells[8].FormattedValue.ToString());
                ShowTextSignature.AppendChild(doc.CreateTextNode(Convert.ToString(SignatureShowText)));
                SignatureItemNode.AppendChild(ShowTextSignature);

                //End SignatureItem


                // start ContinueItem//

                XmlNode ContinueItemNode = doc.CreateElement("ContinueItem");
                PageFooter.AppendChild(ContinueItemNode);



                XmlNode textnodeContinue = doc.CreateElement("Text");
                //textnodeContinue.AppendChild(doc.CreateElement(""));
                string Continuetext = dgvpagefooter.Rows[17].Cells[1].Value.ToString();
                textnodeContinue.AppendChild(doc.CreateTextNode(Continuetext));
                ContinueItemNode.AppendChild(textnodeContinue);

                XmlNode nameNodeContinue = doc.CreateElement("Row");
                string Continuerow = dgvpagefooter.Rows[17].Cells[2].Value.ToString();
                nameNodeContinue.AppendChild(doc.CreateTextNode(Continuerow));
                ContinueItemNode.AppendChild(nameNodeContinue);


                XmlNode priceNodeContinue = doc.CreateElement("Column");
                string Continuecolumn = dgvpagefooter.Rows[17].Cells[3].Value.ToString();
                priceNodeContinue.AppendChild(doc.CreateTextNode(Continuecolumn));
                ContinueItemNode.AppendChild(priceNodeContinue);


                XmlNode fontnameContinue = doc.CreateElement("FontName");

                string ContinueFontName = Convert.ToString(dgvpagefooter.Rows[17].Cells[4].FormattedValue.ToString());
                fontnameContinue.AppendChild(doc.CreateTextNode(ContinueFontName));
                ContinueItemNode.AppendChild(fontnameContinue);



                XmlNode fontsizeContinue = doc.CreateElement("FontSize");
                string ContinueFontsize = Convert.ToString(dgvpagefooter.Rows[17].Cells[5].FormattedValue.ToString());
                fontsizeContinue.AppendChild(doc.CreateTextNode(ContinueFontsize));
                ContinueItemNode.AppendChild(fontsizeContinue);



                XmlNode FontBoldContinue = doc.CreateElement("FontBold");
                bool ContinueFontBold = Convert.ToBoolean(dgvpagefooter.Rows[17].Cells[6].FormattedValue.ToString());
                FontBoldContinue.AppendChild(doc.CreateTextNode(Convert.ToString(ContinueFontBold)));
                ContinueItemNode.AppendChild(FontBoldContinue);


                XmlNode ShowContinue = doc.CreateElement("Show");
                bool ContinueShow = Convert.ToBoolean(dgvpagefooter.Rows[17].Cells[7].FormattedValue.ToString());
                ShowContinue.AppendChild(doc.CreateTextNode(Convert.ToString(ContinueShow)));
                ContinueItemNode.AppendChild(ShowContinue);



                XmlNode ShowTextContinue = doc.CreateElement("ShowText");
                bool ContinueShowText = Convert.ToBoolean(dgvpagefooter.Rows[17].Cells[8].FormattedValue.ToString());
                ShowTextContinue.AppendChild(doc.CreateTextNode(Convert.ToString(ContinueShowText)));
                ContinueItemNode.AppendChild(ShowTextContinue);

                //End ContinueItem


                # endregion page footer


                //PrintSettings
                PrintSettings.AppendChild(PageFooter);

                printsNode.AppendChild(PrintSettings);
                doc.AppendChild(printsNode);              




                string PATH = @"D:\demoproject\PharmaBackup_28sep\PharmaSYSDistributorPlus\PrintSettings.xml";            
                FileInfo file = new FileInfo(PATH);
                string destDir = @"D:\demoproject\PharmaBackup_28sep\PharmaSYSDistributorPlus\PrintSettings.xml";
                FileInfo destFile = new FileInfo(Path.Combine(destDir, file.Name));
                if (destFile.Exists)
                {
                    if (file.LastWriteTime > destFile.LastWriteTime)
                    {
                        // now you can safely overwrite it
                        file.CopyTo(destFile.FullName, true);
                    }
                }


                doc.Save(destDir);
                MessageBox.Show("Generate XML File sucessfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Please Select Print Setting", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnview_Click(object sender, EventArgs e)
        {
            try
            {

                GetXMLData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void GetXMLData()
        {
            try
            {
                XmlNode PrintSettings = null;

                # region Page Header View Data
                DataTable dt = new DataTable();
                dt.Columns.Add("PageWidth", typeof(string));
                dt.Columns.Add("PageHeight", typeof(string));
                dt.Columns.Add("LineFeed", typeof(string));
                dt.Columns.Add("ReverseLineFeed", typeof(string));
                dt.Columns.Add("ContentStartRow", typeof(string));
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(@"D:\demoproject\PharmaBackup_28sep\PharmaSYSDistributorPlus\PrintSettings.xml");
                //XmlNodeList nodeListcmb = xmldoc.SelectNodes("PrintSettings");

                XmlNode oMainNode = xmldoc.SelectSingleNode("PrintSettings");

                XmlNode oEventNode = oMainNode.ChildNodes[0];
                string sEventNodeName = oEventNode.Name;
                cmbprintseting.Text = sEventNodeName;
                PrintSettings = xmldoc.CreateElement(sEventNodeName);




                XmlNodeList nodeList = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/GeneralSettings");
                foreach (XmlNode node in nodeList)
                {
                    DataRow dtrow = dt.NewRow();
                    dtrow["PageWidth"] = node["PageWidth"].InnerText;
                    dtrow["PageHeight"] = node["PageHeight"].InnerText;
                    dtrow["LineFeed"] = node["LineFeed"].InnerText;
                    dtrow["ReverseLineFeed"] = node["ReverseLineFeed"].InnerText;
                    dtrow["ContentStartRow"] = node["ContentStartRow"].InnerText;

                    dt.Rows.Add(dtrow);
                }
                if (dt.Rows.Count > 0)
                {
                    txtpagewidth.Text = dt.Rows[0]["PageWidth"].ToString();
                    txtpageheight.Text = dt.Rows[0]["PageHeight"].ToString();
                    txtlinefeed.Text = dt.Rows[0]["LineFeed"].ToString();
                    txtreverselinefeed.Text = dt.Rows[0]["ReverseLineFeed"].ToString();
                    txtcontentstartrow.Text = dt.Rows[0]["ContentStartRow"].ToString();
                }
                // shopnameItem//
                XmlNodeList xnList = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/ShopNameItem");
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        string text = xn["Text"].InnerText;
                        string row = xn["Row"].InnerText;
                        string Column = xn["Column"].InnerText;
                        string FontSize = xn["FontSize"].InnerText;
                        string FontName = xn["FontName"].InnerText;
                        bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                        bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                        bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);



                        dgvpageheader.Rows[0].Cells[1].Value = text;
                        dgvpageheader.Rows[0].Cells[2].Value = row;
                        dgvpageheader.Rows[0].Cells[3].Value = Column;

                        DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[0].Cells[4]);
                        if (cityCell != null)
                        {
                            cityCell.Value = FontName;
                        }
                        else
                        {
                            cityCell.Value = "Arial";
                        }

                        DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[0].Cells[5]);
                        if (cityCellsize != null)
                        {
                            cityCellsize.Value = FontSize;
                        }
                        else
                        {
                            cityCellsize.Value = "9";
                        }

                        dgvpageheader.Rows[0].Cells[6].Value = FontBold;
                        dgvpageheader.Rows[0].Cells[7].Value = Show;
                        dgvpageheader.Rows[0].Cells[8].Value = ShowText;
                    }
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }
                }
                // end shopname

                // start shopaddressitem1
                XmlNodeList xnListshopaddressitem1 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/ShopAddress1Item");
                foreach (XmlNode xn in xnListshopaddressitem1)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[1].Cells[1].Value = text;
                    dgvpageheader.Rows[1].Cells[2].Value = row;
                    dgvpageheader.Rows[1].Cells[3].Value = Column;

                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[1].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[1].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }

                    dgvpageheader.Rows[1].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[1].Cells[7].Value = Show;
                    dgvpageheader.Rows[1].Cells[8].Value = ShowText;
                }

                // end shopaddress1


                // start shopaddressitem2
                XmlNodeList xnListshopaddressitem2 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/ShopAddress2Item");
                foreach (XmlNode xn in xnListshopaddressitem2)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[2].Cells[1].Value = text;
                    dgvpageheader.Rows[2].Cells[2].Value = row;
                    dgvpageheader.Rows[2].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[2].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[2].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[2].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[2].Cells[7].Value = Show;
                    dgvpageheader.Rows[2].Cells[8].Value = ShowText;
                }

                // end shopaddress2

                // start ShopTelephoneItem
                XmlNodeList xnListShopTelephoneItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/ShopTelephoneItem");
                foreach (XmlNode xn in xnListShopTelephoneItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[3].Cells[1].Value = text;
                    dgvpageheader.Rows[3].Cells[2].Value = row;
                    dgvpageheader.Rows[3].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[3].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[3].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }

                    dgvpageheader.Rows[3].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[3].Cells[7].Value = Show;
                    dgvpageheader.Rows[3].Cells[8].Value = ShowText;
                }

                // end ShopTelephoneItem

                // start PatientName
                XmlNodeList xnListPatientName = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/PatientName");
                foreach (XmlNode xn in xnListPatientName)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[4].Cells[1].Value = text;
                    dgvpageheader.Rows[4].Cells[2].Value = row;
                    dgvpageheader.Rows[4].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[4].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[4].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[4].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[4].Cells[7].Value = Show;
                    dgvpageheader.Rows[4].Cells[8].Value = ShowText;
                }

                // end PatientName
                // start PatientAddress
                XmlNodeList xnListPatientAddress = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/PatientAddress");
                foreach (XmlNode xn in xnListPatientAddress)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[5].Cells[1].Value = text;
                    dgvpageheader.Rows[5].Cells[2].Value = row;
                    dgvpageheader.Rows[5].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[5].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[5].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[5].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[5].Cells[7].Value = Show;
                    dgvpageheader.Rows[5].Cells[8].Value = ShowText;
                }

                // end PatientAddress
                // start PatientTelephone
                XmlNodeList xnListPatientTelephone = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/PatientTelephone");
                foreach (XmlNode xn in xnListPatientTelephone)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[6].Cells[1].Value = text;
                    dgvpageheader.Rows[6].Cells[2].Value = row;
                    dgvpageheader.Rows[6].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[6].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[6].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }

                    dgvpageheader.Rows[6].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[6].Cells[7].Value = Show;
                    dgvpageheader.Rows[6].Cells[8].Value = ShowText;
                }

                // end PatientTelephone
                // start DoctorName
                XmlNodeList xnListDoctorName = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/DoctorName");
                foreach (XmlNode xn in xnListDoctorName)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[7].Cells[1].Value = text;
                    dgvpageheader.Rows[7].Cells[2].Value = row;
                    dgvpageheader.Rows[7].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[7].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[7].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[7].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[7].Cells[7].Value = Show;
                    dgvpageheader.Rows[7].Cells[8].Value = ShowText;
                }

                // end DoctorName
                // start DoctorAddress
                XmlNodeList xnListDoctorAddress = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/DoctorAddress");
                foreach (XmlNode xn in xnListDoctorAddress)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[8].Cells[1].Value = text;
                    dgvpageheader.Rows[8].Cells[2].Value = row;
                    dgvpageheader.Rows[8].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[8].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[8].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCell.Value = "9";
                    }
                    dgvpageheader.Rows[8].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[8].Cells[7].Value = Show;
                    dgvpageheader.Rows[8].Cells[8].Value = ShowText;
                }

                // end DoctorAddress

                // start TimeItem
                XmlNodeList xnListTimeItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/TimeItem");
                foreach (XmlNode xn in xnListTimeItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[9].Cells[1].Value = text;
                    dgvpageheader.Rows[9].Cells[2].Value = row;
                    dgvpageheader.Rows[9].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[9].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[9].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[9].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[9].Cells[7].Value = Show;
                    dgvpageheader.Rows[9].Cells[8].Value = ShowText;
                }

                // end TimeItem


                // start VoucherTypeSCAItem
                XmlNodeList xnListVoucherTypeSCAItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeSCAItem");
                foreach (XmlNode xn in xnListVoucherTypeSCAItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[10].Cells[1].Value = text;
                    dgvpageheader.Rows[10].Cells[2].Value = row;
                    dgvpageheader.Rows[10].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[10].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[10].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[10].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[10].Cells[7].Value = Show;
                    dgvpageheader.Rows[10].Cells[8].Value = ShowText;
                }

                // end VoucherTypeSCAItem

                // start VoucherTypeSCRItem
                XmlNodeList xnListVoucherTypeSCRItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeSCRItem");
                foreach (XmlNode xn in xnListVoucherTypeSCRItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[11].Cells[1].Value = text;
                    dgvpageheader.Rows[11].Cells[2].Value = row;
                    dgvpageheader.Rows[11].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[11].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[11].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[11].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[11].Cells[7].Value = Show;
                    dgvpageheader.Rows[11].Cells[8].Value = ShowText;
                }

                // end VoucherTypeSCRItem
                // start VoucherTypeSCSItem
                XmlNodeList xnListVoucherTypeSCSItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeSCSItem");
                foreach (XmlNode xn in xnListVoucherTypeSCSItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[12].Cells[1].Value = text;
                    dgvpageheader.Rows[12].Cells[2].Value = row;
                    dgvpageheader.Rows[12].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[12].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[12].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[12].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[12].Cells[7].Value = Show;
                    dgvpageheader.Rows[12].Cells[8].Value = ShowText;
                }

                // end VoucherTypeSCSItem
                // start VoucherTypeSVUItem
                XmlNodeList xnListVoucherTypeSVUItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeSVUItem");
                foreach (XmlNode xn in xnListVoucherTypeSVUItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[13].Cells[1].Value = text;
                    dgvpageheader.Rows[13].Cells[2].Value = row;
                    dgvpageheader.Rows[13].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[13].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[13].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[13].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[13].Cells[7].Value = Show;
                    dgvpageheader.Rows[13].Cells[8].Value = ShowText;
                }

                // end VoucherTypeSVUItem
                // start VoucherTypeDebitNoteItem
                XmlNodeList xnListVoucherTypeDebitNoteItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeDebitNoteItem");
                foreach (XmlNode xn in xnListVoucherTypeDebitNoteItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[14].Cells[1].Value = text;
                    dgvpageheader.Rows[14].Cells[2].Value = row;
                    dgvpageheader.Rows[14].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[14].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[14].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[14].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[14].Cells[7].Value = Show;
                    dgvpageheader.Rows[14].Cells[8].Value = ShowText;
                }

                // end VoucherTypeDebitNoteItem

                // start VoucherTypeCreditNoteItem
                XmlNodeList xnListVoucherTypeCreditNoteItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeCreditNoteItem");
                foreach (XmlNode xn in xnListVoucherTypeCreditNoteItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[15].Cells[1].Value = text;
                    dgvpageheader.Rows[15].Cells[2].Value = row;
                    dgvpageheader.Rows[15].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[15].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[15].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[15].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[15].Cells[7].Value = Show;
                    dgvpageheader.Rows[15].Cells[8].Value = ShowText;
                }

                // end VoucherTypeCreditNoteItem
                // start VoucherTypeStockOUTItem
                XmlNodeList xnListVoucherTypeStockOUTItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeStockOUTItem");
                foreach (XmlNode xn in xnListVoucherTypeStockOUTItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[16].Cells[1].Value = text;
                    dgvpageheader.Rows[16].Cells[2].Value = row;
                    dgvpageheader.Rows[16].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[16].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[16].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[16].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[16].Cells[7].Value = Show;
                    dgvpageheader.Rows[16].Cells[8].Value = ShowText;
                }

                // start VoucherTypeStockINItem
                XmlNodeList xnListVoucherTypeStockINItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeStockINItem");
                foreach (XmlNode xn in xnListVoucherTypeStockINItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[17].Cells[1].Value = text;
                    dgvpageheader.Rows[17].Cells[2].Value = row;
                    dgvpageheader.Rows[17].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[17].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[17].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[17].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[17].Cells[7].Value = Show;
                    dgvpageheader.Rows[17].Cells[8].Value = ShowText;
                }

                // end VoucherTypeStockINItem
                // start VoucherTypeCashReceiptItem
                XmlNodeList xnListVoucherTypeCashReceiptItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeCashReceiptItem");
                foreach (XmlNode xn in xnListVoucherTypeCashReceiptItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[18].Cells[1].Value = text;
                    dgvpageheader.Rows[18].Cells[2].Value = row;
                    dgvpageheader.Rows[18].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[18].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[18].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[18].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[18].Cells[7].Value = Show;
                    dgvpageheader.Rows[18].Cells[8].Value = ShowText;
                }

                // end VoucherTypeCashReceiptItem
                // start VoucherTypeCashPaymentItem
                XmlNodeList xnListVoucherTypeCashPaymentItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeCashPaymentItem");
                foreach (XmlNode xn in xnListVoucherTypeCashPaymentItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[19].Cells[1].Value = text;
                    dgvpageheader.Rows[19].Cells[2].Value = row;
                    dgvpageheader.Rows[19].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[19].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[19].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[19].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[19].Cells[7].Value = Show;
                    dgvpageheader.Rows[19].Cells[8].Value = ShowText;
                }

                // end VoucherTypeCashPaymentItem
                // start VoucherTypeBankReceiptItem
                XmlNodeList xnListVoucherTypeBankReceiptItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeBankReceiptItem");
                foreach (XmlNode xn in xnListVoucherTypeBankReceiptItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[20].Cells[1].Value = text;
                    dgvpageheader.Rows[20].Cells[2].Value = row;
                    dgvpageheader.Rows[20].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[20].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[20].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[20].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[20].Cells[7].Value = Show;
                    dgvpageheader.Rows[20].Cells[8].Value = ShowText;
                }

                // end VoucherTypeBankReceiptItem

                // start VoucherTypeBankPaymentItem
                XmlNodeList xnListVoucherTypeBankPaymentItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/VoucherTypeBankPaymentItem");
                foreach (XmlNode xn in xnListVoucherTypeBankPaymentItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[21].Cells[1].Value = text;
                    dgvpageheader.Rows[21].Cells[2].Value = row;
                    dgvpageheader.Rows[21].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[21].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[21].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[21].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[21].Cells[7].Value = Show;
                    dgvpageheader.Rows[21].Cells[8].Value = ShowText;
                }

                // end VoucherTypeBankPaymentItem

                // start DateItem
                XmlNodeList xnListDateItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/DateItem");
                foreach (XmlNode xn in xnListDateItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[22].Cells[1].Value = text;
                    dgvpageheader.Rows[22].Cells[2].Value = row;
                    dgvpageheader.Rows[22].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[22].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[22].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[22].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[22].Cells[7].Value = Show;
                    dgvpageheader.Rows[22].Cells[8].Value = ShowText;
                }

                // end DateItem
                // start PageNoItem
                XmlNodeList xnListPageNoItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageHeader/PageNoItem");
                foreach (XmlNode xn in xnListPageNoItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpageheader.Rows[23].Cells[1].Value = text;
                    dgvpageheader.Rows[23].Cells[2].Value = row;
                    dgvpageheader.Rows[23].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[23].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[23].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpageheader.Rows[23].Cells[6].Value = FontBold;
                    dgvpageheader.Rows[23].Cells[7].Value = Show;
                    dgvpageheader.Rows[23].Cells[8].Value = ShowText;
                }

                // end PageNoItem

                # endregion Page Header View Data

                # region page content view data
                // Column1// serail number
                XmlNodeList xnListcolumn1 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column0");
                foreach (XmlNode xn in xnListcolumn1)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    if (ColumnHeader == string.Empty)
                    {
                        dgprintsettingview.Rows[0].Cells[0].Value = "Sr.No";
                    }
                    else
                    {
                        dgprintsettingview.Rows[0].Cells[0].Value = ColumnHeader;
                    }
                    // dgprintsettingview.Rows[0].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[0].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {

                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[0].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {

                        cityCellsize.Value = "10";
                    }
                    dgprintsettingview.Rows[0].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[0].Cells[4].Value = Show;
                    dgprintsettingview.Rows[0].Cells[5].Value = Column;

                }
                // end Coulmn1
                // start column2//
                XmlNodeList xnListcolumn2 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column2");
                foreach (XmlNode xn in xnListcolumn2)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;


                    dgprintsettingview.Rows[1].Cells[0].Value = ColumnHeader;

                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[1].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[1].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[1].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[1].Cells[4].Value = Show;
                    dgprintsettingview.Rows[1].Cells[5].Value = Column;

                }
                // end Coulmn2
                // start column3//
                XmlNodeList xnListcolumn3 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column3");
                foreach (XmlNode xn in xnListcolumn3)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[2].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[2].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[2].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[2].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[2].Cells[4].Value = Show;
                    dgprintsettingview.Rows[2].Cells[5].Value = Column;

                }
                // end Coulmn3



                // start column4//
                XmlNodeList xnListcolumn4 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column4");
                foreach (XmlNode xn in xnListcolumn4)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;
                    dgprintsettingview.Rows[3].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[3].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[3].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[3].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[3].Cells[4].Value = Show;
                    dgprintsettingview.Rows[3].Cells[5].Value = Column;



                }
                // end Coulmn4


                // start column5//
                XmlNodeList xnListcolumn5 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column5");
                foreach (XmlNode xn in xnListcolumn5)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[4].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[4].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[4].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[4].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[4].Cells[4].Value = Show;
                    dgprintsettingview.Rows[4].Cells[5].Value = Column;
                }
                // end Coulmn5

                // start column6//
                XmlNodeList xnListcolumn6 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column6");
                foreach (XmlNode xn in xnListcolumn6)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[5].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[5].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[5].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[5].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[5].Cells[4].Value = Show;
                    dgprintsettingview.Rows[5].Cells[5].Value = Column;

                }
                // end Coulmn6

                // start column7//
                XmlNodeList xnListcolumn7 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column7");
                foreach (XmlNode xn in xnListcolumn7)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[6].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[6].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[6].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[6].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[6].Cells[4].Value = Show;
                    dgprintsettingview.Rows[6].Cells[5].Value = Column;

                }
                // end Coulmn7
                // start column8//
                XmlNodeList xnListcolumn8 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column8");
                foreach (XmlNode xn in xnListcolumn8)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[7].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[7].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[7].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                   
                    dgprintsettingview.Rows[7].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[7].Cells[4].Value = Show;
                    dgprintsettingview.Rows[7].Cells[5].Value = Column;


                }
                // end Coulmn8

                // start column9// sales rate
                XmlNodeList xnListcolumn9 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column9");
                foreach (XmlNode xn in xnListcolumn9)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    if (ColumnHeader == string.Empty)
                    {
                        dgprintsettingview.Rows[8].Cells[0].Value = "SaleRate";
                    }
                    else
                    {
                        dgprintsettingview.Rows[8].Cells[0].Value = ColumnHeader;
                    }


                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[8].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[8].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgprintsettingview.Rows[8].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[8].Cells[4].Value = Show;
                    dgprintsettingview.Rows[8].Cells[5].Value = Column;

                }
                // end Coulmn9

                // start column10//
                XmlNodeList xnListcolumn10 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column10");
                foreach (XmlNode xn in xnListcolumn10)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;


                    if (ColumnHeader == string.Empty)
                    {
                        dgprintsettingview.Rows[9].Cells[0].Value = "PurchaseRate";
                    }
                    else
                    {
                         dgprintsettingview.Rows[9].Cells[0].Value = ColumnHeader;
                    }


                   
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[9].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[9].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgprintsettingview.Rows[9].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[9].Cells[4].Value = Show;
                    dgprintsettingview.Rows[9].Cells[5].Value = Column;


                }
                // end Coulmn10

                // start column11// vat%
                XmlNodeList xnListcolumn11 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column11");
                foreach (XmlNode xn in xnListcolumn11)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    
                    if(ColumnHeader == string.Empty)
                    {
                        dgprintsettingview.Rows[10].Cells[0].Value = "VAT%";
                    }
                    else{
                        dgprintsettingview.Rows[10].Cells[0].Value = ColumnHeader;
                    }

                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[10].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";

                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[10].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";

                    }
                    dgprintsettingview.Rows[10].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[10].Cells[4].Value = Show;
                    dgprintsettingview.Rows[10].Cells[5].Value = Column;


                }
                // end Coulmn11

                // start column12//
                XmlNodeList xnListcolumn12 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column12");
                foreach (XmlNode xn in xnListcolumn12)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;

                    dgprintsettingview.Rows[11].Cells[0].Value = ColumnHeader;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[11].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[11].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    dgprintsettingview.Rows[11].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[11].Cells[4].Value = Show;
                    dgprintsettingview.Rows[11].Cells[5].Value = Column;


                }
                // end Coulmn12

                // start column13// schedule category//
                XmlNodeList xnListcolumn13 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageContent/Column13");
                foreach (XmlNode xn in xnListcolumn13)
                {

                    string ColumnHeader = xn["ColumnHeader"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    string Column = xn["Column"].InnerText;



                    if (ColumnHeader == string.Empty)
                    {
                         dgprintsettingview.Rows[12].Cells[0].Value = "ScheduleCategory";
                    }
                    else{

                         dgprintsettingview.Rows[12].Cells[0].Value = ColumnHeader;
                    }


                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[12].Cells[1]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[12].Cells[2]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCell.Value = "9";
                    }
                    dgprintsettingview.Rows[12].Cells[3].Value = FontBold;
                    dgprintsettingview.Rows[12].Cells[4].Value = Show;
                    dgprintsettingview.Rows[12].Cells[5].Value = Column;


                }
                // end Coulmn12


                # endregion page content view data
                # region Page footer View Data


                // DiscountItem//
                XmlNodeList xnListDiscountItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DiscountItem");
                foreach (XmlNode xn in xnListDiscountItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[0].Cells[1].Value = text;
                    dgvpagefooter.Rows[0].Cells[2].Value = row;
                    dgvpagefooter.Rows[0].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[0].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[0].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[0].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[0].Cells[7].Value = Show;
                    dgvpagefooter.Rows[0].Cells[8].Value = ShowText;
                }
                // end shopname

                // start GrossAmountItem
                XmlNodeList xnListGrossAmountItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/GrossAmountItem");
                foreach (XmlNode xn in xnListGrossAmountItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[1].Cells[1].Value = text;
                    dgvpagefooter.Rows[1].Cells[2].Value = row;
                    dgvpagefooter.Rows[1].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[1].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[1].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[1].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[1].Cells[7].Value = Show;
                    dgvpagefooter.Rows[1].Cells[8].Value = ShowText;
                }

                // end shopaddress1


                // start NarrationItem
                XmlNodeList xnListNarrationItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/NarrationItem");
                foreach (XmlNode xn in xnListNarrationItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[2].Cells[1].Value = text;
                    dgvpagefooter.Rows[2].Cells[2].Value = row;
                    dgvpagefooter.Rows[2].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[2].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[2].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[2].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[2].Cells[7].Value = Show;
                    dgvpagefooter.Rows[2].Cells[8].Value = ShowText;
                }

                // end shopaddress2

                // start CreditNoteItem
                XmlNodeList xnListCreditNoteItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/CreditNoteItem");
                foreach (XmlNode xn in xnListCreditNoteItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[3].Cells[1].Value = text;
                    dgvpagefooter.Rows[3].Cells[2].Value = row;
                    dgvpagefooter.Rows[3].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[3].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[3].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[3].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[3].Cells[7].Value = Show;
                    dgvpagefooter.Rows[3].Cells[8].Value = ShowText;
                }

                // end CreditNoteItem

                // start DebitNoteItem
                XmlNodeList xnListDebitNoteItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DebitNoteItem");
                foreach (XmlNode xn in xnListDebitNoteItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[4].Cells[1].Value = text;
                    dgvpagefooter.Rows[4].Cells[2].Value = row;
                    dgvpagefooter.Rows[4].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[4].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[4].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[4].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[4].Cells[7].Value = Show;
                    dgvpagefooter.Rows[4].Cells[8].Value = ShowText;
                }

                // end DebitNoteItem

                // start BalanceAmountItem
                XmlNodeList xnListBalanceAmountItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/BalanceAmountItem");
                foreach (XmlNode xn in xnListBalanceAmountItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[5].Cells[1].Value = text;
                    dgvpagefooter.Rows[5].Cells[2].Value = row;
                    dgvpagefooter.Rows[5].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[5].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[5].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[5].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[5].Cells[7].Value = Show;
                    dgvpagefooter.Rows[5].Cells[8].Value = ShowText;
                }

                // end BalanceAmountItem
                // start NetAmountItem
                XmlNodeList xnListNetAmountItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/NetAmountItem");
                foreach (XmlNode xn in xnListNetAmountItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[6].Cells[1].Value = text;
                    dgvpagefooter.Rows[6].Cells[2].Value = row;
                    dgvpagefooter.Rows[6].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[6].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[6].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[6].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[6].Cells[7].Value = Show;
                    dgvpagefooter.Rows[6].Cells[8].Value = ShowText;
                }

                // end NetAmountItem
                // start SubjectItem
                XmlNodeList xnListSubjectItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/SubjectItem");
                foreach (XmlNode xn in xnListSubjectItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[7].Cells[1].Value = text;
                    dgvpagefooter.Rows[7].Cells[2].Value = row;
                    dgvpagefooter.Rows[7].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[7].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[7].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[7].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[7].Cells[7].Value = Show;
                    dgvpagefooter.Rows[7].Cells[8].Value = ShowText;
                }

                // end SubjectItem
                // start JurisdictionItem
                XmlNodeList xnListJurisdictionItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/JurisdictionItem");
                foreach (XmlNode xn in xnListJurisdictionItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[8].Cells[1].Value = text;
                    dgvpagefooter.Rows[8].Cells[2].Value = row;
                    dgvpagefooter.Rows[8].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[8].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[8].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[8].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[8].Cells[7].Value = Show;
                    dgvpagefooter.Rows[8].Cells[8].Value = ShowText;
                }

                // end JurisdictionItem

                // start ATOWItem
                XmlNodeList xnListATOWItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/ATOWItem");
                foreach (XmlNode xn in xnListATOWItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[9].Cells[1].Value = text;
                    dgvpagefooter.Rows[9].Cells[2].Value = row;
                    dgvpagefooter.Rows[9].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[9].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[9].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[9].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[9].Cells[7].Value = Show;
                    dgvpagefooter.Rows[9].Cells[8].Value = ShowText;
                }

                // end ATOWItem


                // start DLNItem
                XmlNodeList xnListDLNItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DLNItem");
                foreach (XmlNode xn in xnListDLNItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[10].Cells[1].Value = text;
                    dgvpagefooter.Rows[10].Cells[2].Value = row;
                    dgvpagefooter.Rows[10].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[10].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[10].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[10].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[10].Cells[7].Value = Show;
                    dgvpagefooter.Rows[10].Cells[8].Value = ShowText;
                }

                // end DLNItem
               // string s = "";
                // start VATTINItem
                XmlNodeList xnListVATTINItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/VATTINItem");
                foreach (XmlNode xn in xnListVATTINItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[11].Cells[1].Value = text;
                    dgvpagefooter.Rows[11].Cells[2].Value = row;
                    dgvpagefooter.Rows[11].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[11].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[11].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[11].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[11].Cells[7].Value = Show;
                    dgvpagefooter.Rows[11].Cells[8].Value = ShowText;
                }

                // end VATTINItem
                // start DeclarationItem1
                XmlNodeList xnListDeclarationItem1 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DeclarationItem1");
                foreach (XmlNode xn in xnListDeclarationItem1)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[12].Cells[1].Value = text;
                    dgvpagefooter.Rows[12].Cells[2].Value = row;
                    dgvpagefooter.Rows[12].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[12].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[12].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[12].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[12].Cells[7].Value = Show;
                    dgvpagefooter.Rows[12].Cells[8].Value = ShowText;
                }

                // end DeclarationItem1
                // start DeclarationItem2
                XmlNodeList xnListDeclarationItem2 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DeclarationItem2");
                foreach (XmlNode xn in xnListDeclarationItem2)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[13].Cells[1].Value = text;
                    dgvpagefooter.Rows[13].Cells[2].Value = row;
                    dgvpagefooter.Rows[13].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[13].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[13].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }

                    dgvpagefooter.Rows[13].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[13].Cells[7].Value = Show;
                    dgvpagefooter.Rows[13].Cells[8].Value = ShowText;
                }

                // end DeclarationItem2
                // start DeclarationItem3
                XmlNodeList xnListDeclarationItem3 = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/DeclarationItem3");
                foreach (XmlNode xn in xnListDeclarationItem3)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[14].Cells[1].Value = text;
                    dgvpagefooter.Rows[14].Cells[2].Value = row;
                    dgvpagefooter.Rows[14].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[14].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[14].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[14].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[14].Cells[7].Value = Show;
                    dgvpagefooter.Rows[14].Cells[8].Value = ShowText;
                }

                // end DeclarationItem3

                // start ShopNameItempf
                XmlNodeList xnListShopNameItempf = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/ShopNameItem");
                foreach (XmlNode xn in xnListShopNameItempf)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[15].Cells[1].Value = text;
                    dgvpagefooter.Rows[15].Cells[2].Value = row;
                    dgvpagefooter.Rows[15].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[15].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[15].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[15].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[15].Cells[7].Value = Show;
                    dgvpagefooter.Rows[15].Cells[8].Value = ShowText;
                }

                // end ShopNameItempf
                // start SignatureItem
                XmlNodeList xnListSignatureItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/SignatureItem");
                foreach (XmlNode xn in xnListSignatureItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[16].Cells[1].Value = text;
                    dgvpagefooter.Rows[16].Cells[2].Value = row;
                    dgvpagefooter.Rows[16].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[16].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }


                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[16].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[16].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[16].Cells[7].Value = Show;
                    dgvpagefooter.Rows[16].Cells[8].Value = ShowText;
                }

                // start ContinueItem
                XmlNodeList xnListContinueItem = xmldoc.SelectNodes("PrintSettings/" + sEventNodeName + "/PageFooter/ContinueItem");
                foreach (XmlNode xn in xnListContinueItem)
                {

                    string text = xn["Text"].InnerText;
                    string row = xn["Row"].InnerText;
                    string Column = xn["Column"].InnerText;
                    string FontSize = xn["FontSize"].InnerText;
                    string FontName = xn["FontName"].InnerText;
                    bool FontBold = Convert.ToBoolean(xn["FontBold"].InnerText);
                    bool Show = Convert.ToBoolean(xn["Show"].InnerText);
                    bool ShowText = Convert.ToBoolean(xn["ShowText"].InnerText);

                    dgvpagefooter.Rows[17].Cells[1].Value = text;
                    dgvpagefooter.Rows[17].Cells[2].Value = row;
                    dgvpagefooter.Rows[17].Cells[3].Value = Column;
                    DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[17].Cells[4]);
                    if (cityCell != null)
                    {
                        cityCell.Value = FontName;
                    }
                    else
                    {
                        cityCell.Value = "Arial";
                    }

                    DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[17].Cells[5]);
                    if (cityCellsize != null)
                    {
                        cityCellsize.Value = FontSize;
                    }
                    else
                    {
                        cityCellsize.Value = "9";
                    }
                    dgvpagefooter.Rows[17].Cells[6].Value = FontBold;
                    dgvpagefooter.Rows[17].Cells[7].Value = Show;
                    dgvpagefooter.Rows[17].Cells[8].Value = ShowText;
                }

                // end ContinueItem




                # endregion Page footer View Data
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }



        }

        private void dgvpageheader_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvpagefooter_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgprintsettingview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        public void cleardata()
        {
            DataGridViewComboBoxCell cityCell = (DataGridViewComboBoxCell)(dgvpageheader.Rows[0].Cells[4]);
            cityCell.Value = "";
            DataGridViewComboBoxCell cityCellsize = (DataGridViewComboBoxCell)(dgvpageheader.Rows[0].Cells[5]);
            cityCellsize.Value = "";
            dgvpageheader.Rows[0].Cells[6].Value = " ";
            dgvpageheader.Rows[0].Cells[7].Value = " ";
            dgvpageheader.Rows[0].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell1 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[1].Cells[4]);
            cityCell1.Value = "";
            DataGridViewComboBoxCell cityCellsize1 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[1].Cells[5]);
            cityCellsize1.Value = "";
            dgvpageheader.Rows[1].Cells[6].Value = " ";
            dgvpageheader.Rows[1].Cells[7].Value = " ";
            dgvpageheader.Rows[1].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell2 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[2].Cells[4]);
            cityCell2.Value = "";
            DataGridViewComboBoxCell cityCellsize2 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[2].Cells[5]);
            cityCellsize2.Value = "";
            dgvpageheader.Rows[2].Cells[6].Value = " ";
            dgvpageheader.Rows[2].Cells[7].Value = " ";
            dgvpageheader.Rows[2].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell3 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[3].Cells[4]);
            cityCell3.Value = "";
            DataGridViewComboBoxCell cityCellsize3 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[3].Cells[5]);
            cityCellsize3.Value = "";
            dgvpageheader.Rows[3].Cells[6].Value = " ";
            dgvpageheader.Rows[3].Cells[7].Value = " ";
            dgvpageheader.Rows[3].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell4 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[4].Cells[4]);
            cityCell4.Value = "";
            DataGridViewComboBoxCell cityCellsize4 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[4].Cells[5]);
            cityCellsize4.Value = "";
            dgvpageheader.Rows[4].Cells[6].Value = " ";
            dgvpageheader.Rows[4].Cells[7].Value = " ";
            dgvpageheader.Rows[4].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell5 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[5].Cells[4]);
            cityCell5.Value = "";
            DataGridViewComboBoxCell cityCellsize5 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[5].Cells[5]);
            cityCellsize5.Value = "";
            dgvpageheader.Rows[5].Cells[6].Value = " ";
            dgvpageheader.Rows[5].Cells[7].Value = " ";
            dgvpageheader.Rows[5].Cells[8].Value = " ";


            DataGridViewComboBoxCell cityCell6 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[6].Cells[4]);
            cityCell6.Value = "";
            DataGridViewComboBoxCell cityCellsize6 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[6].Cells[5]);
            cityCellsize6.Value = "";
            dgvpageheader.Rows[6].Cells[6].Value = " ";
            dgvpageheader.Rows[6].Cells[7].Value = " ";
            dgvpageheader.Rows[6].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell7 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[7].Cells[4]);
            cityCell7.Value = "";
            DataGridViewComboBoxCell cityCellsize7 = (DataGridViewComboBoxCell)(dgvpageheader.Rows[7].Cells[5]);
            cityCellsize7.Value = "";
            dgvpageheader.Rows[7].Cells[6].Value = " ";
            dgvpageheader.Rows[7].Cells[7].Value = " ";
            dgvpageheader.Rows[7].Cells[8].Value = " ";


            DataGridViewComboBoxCell cityCellf = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[0].Cells[4]);
            cityCellf.Value = "";
            DataGridViewComboBoxCell cityCellsizedg = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[0].Cells[5]);
            cityCellsizedg.Value = "";
            dgvpagefooter.Rows[0].Cells[6].Value = " ";
            dgvpagefooter.Rows[0].Cells[7].Value = " ";
            dgvpagefooter.Rows[0].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell12 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[1].Cells[4]);
            cityCell12.Value = "";
            DataGridViewComboBoxCell cityCellsize14 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[1].Cells[5]);
            cityCellsize14.Value = "";
            dgvpagefooter.Rows[1].Cells[6].Value = " ";
            dgvpagefooter.Rows[1].Cells[7].Value = " ";
            dgvpagefooter.Rows[1].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell21 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[2].Cells[4]);
            cityCell21.Value = "";
            DataGridViewComboBoxCell cityCellsize23 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[2].Cells[5]);
            cityCellsize23.Value = "";
            dgvpagefooter.Rows[2].Cells[6].Value = " ";
            dgvpagefooter.Rows[2].Cells[7].Value = " ";
            dgvpagefooter.Rows[2].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell34 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[3].Cells[4]);
            cityCell34.Value = "";
            DataGridViewComboBoxCell cityCellsize35 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[3].Cells[5]);
            cityCellsize35.Value = "";
            dgvpagefooter.Rows[3].Cells[6].Value = " ";
            dgvpagefooter.Rows[3].Cells[7].Value = " ";
            dgvpagefooter.Rows[3].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell554 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[4].Cells[4]);
            cityCell554.Value = "";
            DataGridViewComboBoxCell cityCellsize44 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[4].Cells[5]);
            cityCellsize44.Value = "";
            dgvpagefooter.Rows[4].Cells[6].Value = " ";
            dgvpagefooter.Rows[4].Cells[7].Value = " ";
            dgvpagefooter.Rows[4].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell59 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[5].Cells[4]);
            cityCell59.Value = "";
            DataGridViewComboBoxCell cityCellsize55 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[5].Cells[5]);
            cityCellsize55.Value = "";
            dgvpagefooter.Rows[5].Cells[6].Value = " ";
            dgvpagefooter.Rows[5].Cells[7].Value = " ";
            dgvpagefooter.Rows[5].Cells[8].Value = " ";


            DataGridViewComboBoxCell cityCell556 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[6].Cells[4]);
            cityCell556.Value = "";
            DataGridViewComboBoxCell cityCellsizer6 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[6].Cells[5]);
            cityCellsizer6.Value = "";
            dgvpagefooter.Rows[6].Cells[6].Value = " ";
            dgvpagefooter.Rows[6].Cells[7].Value = " ";
            dgvpagefooter.Rows[6].Cells[8].Value = " ";

            DataGridViewComboBoxCell cityCell337 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[7].Cells[4]);
            cityCell337.Value = "";
            DataGridViewComboBoxCell cityCellsize3437 = (DataGridViewComboBoxCell)(dgvpagefooter.Rows[7].Cells[5]);
            cityCellsize3437.Value = "";
            dgvpagefooter.Rows[7].Cells[6].Value = " ";
            dgvpagefooter.Rows[7].Cells[7].Value = " ";
            dgvpagefooter.Rows[7].Cells[8].Value = " ";


            DataGridViewComboBoxCell cityCell3373 = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[0].Cells[1]);
            cityCell3373.Value = "";
            DataGridViewComboBoxCell cityCellsize34378 = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[0].Cells[2]);
            cityCellsize34378.Value = "";


            dgprintsettingview.Rows[0].Cells[3].Value = " ";
            dgprintsettingview.Rows[0].Cells[4].Value = " ";



            DataGridViewComboBoxCell cityCell3371 = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[1].Cells[1]);
            cityCell3371.Value = "";
            DataGridViewComboBoxCell cityCellsize34373 = (DataGridViewComboBoxCell)(dgprintsettingview.Rows[1].Cells[2]);
            cityCellsize34373.Value = "";


            dgprintsettingview.Rows[1].Cells[3].Value = " ";
            dgprintsettingview.Rows[1].Cells[4].Value = " ";



        }

        private void dgvpageheader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int clum = Convert.ToInt32(dgvpageheader.CurrentRow.Cells[3].Value);          
                //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

        }

        private void dgvpageheader_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvpagefooter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int clum = Convert.ToInt32(dgvpageheader.CurrentRow.Cells[3].Value);
            //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void dgvpageheader_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int clum = Convert.ToInt32(dgvpageheader.CurrentRow.Cells[3].Value);
            //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void dgvpageheader_SelectionChanged(object sender, EventArgs e)
        {
         
        }

        private void dgvpagefooter_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int clum = Convert.ToInt32(dgvpagefooter.CurrentRow.Cells[3].Value);
            //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void dgprintsettingview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int clum = Convert.ToInt32(dgprintsettingview.CurrentRow.Cells[5].Value);
            //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void dgprintsettingview_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int clum = Convert.ToInt32(dgprintsettingview.CurrentRow.Cells[5].Value);
            //check id value for current row
            if (txtpagewidth.Text != string.Empty)
            {
                if (clum > 600)
                {

                    MessageBox.Show("Please Enter Column Value less than Page Width", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }




    }
}
