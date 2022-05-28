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
using System.Text.RegularExpressions;
using EcoMart.InterfaceLayer.CommonControls;
using System.Collections;
using EcoMart.DataLayer;
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    public enum SearchMode   // [ansuman]
    {
        ProductWise = 0,
        CompanyWise = 1,
        ContentWise = 2,
    }

    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclBulkProduct : BaseControl
    {
        #region Declaration
        private Product _Product;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        private string Prod_SelectedID = string.Empty;//kiran
        public static DataTable SDtable = null;
        public static DataTable TDtable = null;
        public static DataTable NTDtable = null;

        public SearchMode _SearchMode;   // [ansuman]
        public SearchMode SearchMode
        {
            get { return _SearchMode; }
            set { _SearchMode = value; }
        }
        #endregion

        #region  Constructor
        public UclBulkProduct()
        {
            try
            {
                InitializeComponent();
                _Product = new Product();
                SearchControl = new UclProductSearch();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        #endregion

        # region IDetailControl
        public override void SetFocus()
        {
            //cmbCharacterWise.Focus();
        }
        public override bool ClearData()
        {
            _Product.Initialise();
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            panel2.Enabled = true;
            return true;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();
            return retValue;
        }

        public override bool View()
        {
            htTableList = General.GetTableListByCode("ProductID", "ProdName", "MasterProduct");
            bool retValue = base.View();
            panel2.Enabled = true;
            ClearData();
            Fillmcb();
            //txtName.Focus();
            headerLabel1.Text = "PRODUCT -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Product.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Product.Id, "");

            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            try
            {
                CurrentNumber = htTableList.Count;
                if (htTableList.Contains(CurrentNumber))
                    _Product.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Product.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            CurrentNumber -= 1;
            if (htTableList.Contains(CurrentNumber))
                _Product.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Product.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Product.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Product.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            try
            {
                foreach (DataGridViewRow prodrow in BulkProductGrid.Rows)
                {
                    _Product.Id = prodrow.Cells["Col_ProductID"].Value.ToString();
                    _Product.Name = prodrow.Cells["Col_ProductName"].Value.ToString();
                    _Product.ProdCompID = Convert.ToInt32(prodrow.Cells["Col_ProdCompName"].Value.ToString());
                    _Product.ProdCompShortName = prodrow.Cells["Col_ProdCompShortName"].Value.ToString();
                    _Product.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value);
                    _Product.ProdPack = prodrow.Cells["Col_Pack"].Value.ToString();
                    _Product.ProdPackType = prodrow.Cells["Col_PackType"].Value.ToString();
                    _Product.ProdVATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                    _Product.ProdCST = Convert.ToDouble(prodrow.Cells["Col_CST"].Value);
                    _Product.ProdMinLevel = Convert.ToInt32(prodrow.Cells["Col_MinLevel"].Value);
                    _Product.ProdMaxLevel = Convert.ToInt32(prodrow.Cells["Col_MaxLevel"].Value);
                    _Product.ProdRequireColdStorage = prodrow.Cells["Col_ReqColdStorage"].Value.ToString();
                    _Product.ProdShelfID = Convert.ToInt32(prodrow.Cells["Col_ShelfCode"].Value.ToString());
                    _Product.ProdGenericID = Convert.ToInt32(prodrow.Cells["Col_GenCat"].Value.ToString());
                    _Product.ProdProductCategoryID = Convert.ToInt32(prodrow.Cells["Col_ProdCat"].Value.ToString());
                    _Product.ProdCreditor1ID = Convert.ToInt32(prodrow.Cells["Col_FirstCreditor"].Value.ToString());
                    _Product.ProdCreditor2ID = Convert.ToInt32(prodrow.Cells["Col_SecondCreditor"].Value.ToString());
                    _Product.ProdBoxQty = Convert.ToInt32(prodrow.Cells["Col_BoxQuantity"].Value);
                    _Product.ProdIfShortList = prodrow.Cells["Col_ShowIfShortList"].Value.ToString();
                    _Product.ProdIfScheduledDrug = prodrow.Cells["Col_IfScheduleDrug"].Value.ToString();
                    _Product.ProdScheduleDrugCode = prodrow.Cells["Col_ScheduleDrugCode"].Value.ToString();
                    _Product.ProdIfSaleDisc = prodrow.Cells["Col_IfSaleDisc"].Value.ToString();
                    _Product.ProdGrade = prodrow.Cells["Col_ProdGrade"].Value.ToString();
                    _Product.ProdIfBarCodeRequired = prodrow.Cells["Col_IfBarCodeRequired"].Value.ToString();
                    _Product.ScannedBarcode = prodrow.Cells["Col_ScannedBarCode"].Value.ToString();

                    _Product.ModifiedBy = General.CurrentUser.Id;
                    _Product.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Product.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Product.UpdateDetailsBulk();
                    if (retValue == false)
                        break;
                }
                SDtable = _Product.GetForBulkMaintenance();
                NTDtable = _Product.GetNonTransactionProductsBulk();
                TDtable = _Product.GetTransactionProductsBulk();
                if (retValue)
                {
                    MessageBox.Show("Product Information Updated Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtType.Clear();
                    cbCompany.SelectedID = "";
                    cbGenericCat.SelectedID = "";
                    if (txtType.Visible)
                        txtType.Focus();
                    else if (cbCompany.Visible)
                        cbCompany.Focus();
                    else
                        cbGenericCat.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.Message);
            }
            return retValue;
        }

        #endregion IDetailControl

        #region IDetail Members


        public override bool RefreshProductList()
        {
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
       
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        # endregion IDetail Member

        #region Other Private Methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
       
        private void Fillmcb()
        {
            //FilltxtName();
            FillCompanyCombo();
            FillGenericCategoryCombo();
        }
        private void FillCompanyCombo()
        {
            cbCompany.SelectedID = null;
            cbCompany.SourceDataString = new string[5] { "CompID", "CompName", "CompShortName", "PartyID_1", "PartyID_2" };
            cbCompany.ColumnWidth = new string[5] { "0", "250", "50", "0", "0" };
            cbCompany.ValueColumnNo = 0;
            cbCompany.UserControlToShow = new UclCompany();
            Company _Company = new Company();
            DataTable dtable = _Company.GetOverviewData();
            cbCompany.FillData(dtable);
        }

        private void FillGenericCategoryCombo()
        {
            cbGenericCat.SelectedID = null;
            cbGenericCat.SourceDataString = new string[2] { "GenericCategoryId", "GenericCategoryName" };
            cbGenericCat.ColumnWidth = new string[2] { "0", "600" };   // kiran
            cbGenericCat.ValueColumnNo = 0;
            cbGenericCat.UserControlToShow = new UclGenericCategory();
            GenericCategory _GenericCateory = new GenericCategory();
            DataTable dtable = _GenericCateory.GetOverviewData();
            cbGenericCat.FillData(dtable);
        }

        #endregion

        #region Events

        #endregion

        # region tooltip

        private void AddToolTip()
        {
           
        }
        # endregion

        #region ValidateData

        #endregion ValidateData

        private void cmbCharacterWise_SelectedIndexChanged(object sender, EventArgs e)
        {
            labeltype.Visible = true;
            
            BulkProductGrid.ReadOnly = false;
            BulkProductGrid.Columns[0].Frozen = true;
            BulkProductGrid.Columns[1].Frozen = true;
            //BulkProductGrid.Columns[1].ReadOnly = true;

            if (cmbCharacterWise.SelectedItem.ToString() == "PRODUCT WISE")
            {
                labeltype.Text = "PRODCT WISE";
                txtType.Visible = true;
                txtType.Clear();
                cbCompany.Visible = false;
                cbGenericCat.Visible = false;
                //txtType.Focus();
                _SearchMode = SearchMode.ProductWise;
            }
            else if (cmbCharacterWise.SelectedItem.ToString() == "COMPANY WISE")
            {
                labeltype.Text = "COMPANY WISE";
                txtType.Visible = false;
                cbCompany.Visible = true;
                cbCompany.SelectedID = "";
                cbGenericCat.Visible = false;
                //cbCompany.Focus();
                _SearchMode = SearchMode.CompanyWise;
            }
            else
            {
                labeltype.Text = "CONTENT WISE";
                txtType.Visible = false;
                cbCompany.Visible = false;
                cbGenericCat.Visible = true;
                cbGenericCat.SelectedID = "";
                //cbGenericCat.Focus();
                _SearchMode = SearchMode.ContentWise;
            }
        }
        private void UclBulkProduct_Load(object sender, EventArgs e)
        {
            try
            {
                tsBtnAdd.Visible = tsBtnEdit.Visible = tsBtnFirst.Visible = tsBtnPrevious.Visible = tsBtnNext.Visible = tsBtnLast.Visible = tsBtnView.Visible = tsBtnDelete.Visible = tsBtnFifth.Visible = tsBtnPrint.Visible = tsBtnSavenPrint.Visible = false;
                tsBtnSave.Visible = tsBtnSearch.Visible = tsBtnExit.Visible = true;

                ConstructMainColumns(BulkProductGrid);
                DataTable dt = new DataTable();

                dt = _Product.GetForBulkMaintenance();
                BulkProductGrid.DataSource = dt;
                SDtable = dt;

                NTDtable = _Product.GetNonTransactionProductsBulk();
                TDtable = _Product.GetTransactionProductsBulk();

                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                    {
                        if (dt.Columns[colIndex].ColumnName == "ProdPack")
                        {
                            if (dt.Rows[index][colIndex].ToString() != "" && dt.Rows[index][colIndex].ToString() != " " && dt.Rows[index][colIndex] != null)
                            {
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                            }
                            else
                            {
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                            }
                        }
                        if (dt.Columns[colIndex].ColumnName == "prodpacktype")
                        {
                            if (dt.Rows[index][colIndex] != null && dt.Rows[index][colIndex].ToString() != "")
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                            else
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                        }
                        if (dt.Columns[colIndex].ColumnName == "ProdScheduleDrugCode" || dt.Columns[colIndex].ColumnName == "ProdGrade")
                        {
                            if (dt.Rows[index][colIndex].ToString() != " " && dt.Rows[index][colIndex].ToString() != null)
                            {
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                            }
                            else
                            {
                                BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                            }
                        }
                    }
                }
                RowCount.Text = dt.Rows.Count.ToString();
                radioButton1.Focus();
                BulkProductGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructMainColumns(DataGridView ActiveDataGrid)
        {
            DataGridViewTextBoxColumn column1;
            DataGridViewComboBoxColumn column2;
            ActiveDataGrid.Columns.Clear();
            try
            {
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ProductID";
                column1.DataPropertyName = "ProductID";
                column1.HeaderText = "ProductID";
                column1.Width = 0;
                column1.Visible = false;
                ActiveDataGrid.Columns.Add(column1);
                //1
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ProductName";
                column1.DataPropertyName = "ProdName";
                column1.HeaderText = "ProductName";
                column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column1.Width = 220;
                column1.ReadOnly = true;
                ActiveDataGrid.Columns.Add(column1);
                //2
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_ProdCompName";
                column2.DataPropertyName = "ProdCompID";
                column2.HeaderText = "CompanyName";
                column2.Width = 260;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Company _Company = new Company();
                DataTable dtable = _Company.GetOverviewData();
                column2.DataSource = dtable;
                column2.ValueMember = "CompID";
                column2.DisplayMember = "CompName";
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //3
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ProdCompShortName";
                column1.DataPropertyName = "ProdCompShortName";
                column1.HeaderText = "COM";
                column1.Width = 60;
                //column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //4
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_UOM";
                column1.DataPropertyName = "ProdLoosePack";
                column1.HeaderText = "UOM";
                column1.ReadOnly = false;
                column1.Width = 60;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ValueType = typeof(int);
                ActiveDataGrid.Columns.Add(column1);
                //5
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_Pack";
                column2.DataPropertyName = "ProdPack";
                column2.HeaderText = "Pack";
                column2.Width = 80;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Pack _Pack = new Pack();
                DataTable dtPack = _Pack.GetOverviewData();
                column2.DataSource = dtPack;
                column2.ValueMember = "PackName";
                column2.DisplayMember = "PackName";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //6
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_PackType";
                column2.DataPropertyName = "prodpacktype";
                column2.HeaderText = "PackType";
                column2.Width = 80;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Pack _PackType = new Pack();
                DataTable DtPackType = _PackType.GetOverviewDataForPackType();
                column2.DataSource = DtPackType;
                column2.ValueMember = "PackTypeName";
                column2.DisplayMember = "PackTypeName";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //7
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_VATPer";
                column1.DataPropertyName = "ProdVATPercent";
                column1.HeaderText = "VAT%";
                column1.Width = 50;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //8
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_CST";
                column1.DataPropertyName = "ProdCST";
                column1.HeaderText = "CST";
                column1.Width = 40;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //9
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_MinLevel";
                column1.DataPropertyName = "ProdMinLevel";
                column1.HeaderText = "Min";
                column1.Width = 50;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ReadOnly = false;
                column1.ValueType = typeof(int);
                ActiveDataGrid.Columns.Add(column1);
                //10
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_MaxLevel";
                column1.DataPropertyName = "ProdMaxLevel";
                column1.HeaderText = "Max";
                column1.Width = 50;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ReadOnly = false;
                column1.ValueType = typeof(int);
                ActiveDataGrid.Columns.Add(column1);
                //11
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ReqColdStorage";
                column1.DataPropertyName = "ProdRequireColdStorage";
                column1.HeaderText = "ColdStr";
                column1.Width = 50;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //12
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_ShelfCode";
                column2.DataPropertyName = "ProdShelfID";
                column2.HeaderText = "ShelfCode";
                column2.Width = 100;
                column2.ValueType = typeof(string);
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Shelf _Shelf = new Shelf();
                DataTable dtable1 = _Shelf.GetOverviewData();
                column2.DataSource = dtable1;
                column2.ValueMember = "ShelfID";
                column2.DisplayMember = "ShelfCode";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //13
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_GenCat";
                column2.DataPropertyName = "ProdDrugID";
                column2.HeaderText = "Content";
                column2.Width = 330;
                column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                GenericCategory _GenericCateory = new GenericCategory();
                DataTable dtable2 = _GenericCateory.GetOverviewData();
                column2.DataSource = dtable2;
                column2.ValueMember = "GenericCategoryID";
                column2.DisplayMember = "GenericCategoryName";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //14
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_ProdCat";
                column2.DataPropertyName = "ProdCategoryID";
                column2.HeaderText = "Category";
                column2.Width = 220;
                column2.ReadOnly = false;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                ProductCategory _ProductCategory = new ProductCategory();
                DataTable dtable3 = _ProductCategory.GetOverviewData();
                column2.DataSource = dtable3;
                column2.ValueMember = "ProductCategoryID";
                column2.DisplayMember = "ProductCategoryName";
                ActiveDataGrid.Columns.Add(column2);
                //15
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_FirstCreditor";
                column2.DataPropertyName = "ProdPartyId_1";
                column2.HeaderText = "Creditor1";
                column2.Width = 240;
                column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Account _Account = new Account();
                DataTable dtable4 = _Account.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                column2.DataSource = dtable4;
                column2.ValueMember = "AccountID";
                column2.DisplayMember = "AccName";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //16
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_SecondCreditor";
                column2.DataPropertyName = "ProdPartyId_2";
                column2.HeaderText = "Creditor2";
                column2.Width = 240;
                column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                Account _Account2 = new Account();
                DataTable dtable5 = _Account2.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                column2.DataSource = dtable5;
                column2.ValueMember = "AccountID";
                column2.DisplayMember = "AccName";
                column2.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //17
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_BoxQuantity";
                column1.DataPropertyName = "ProdBoxQuantity";
                column1.HeaderText = "Box";
                column1.Width = 50;
                column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //18
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ShowIfShortList";
                column1.DataPropertyName = "ProdIfShortListed";
                column1.HeaderText = "ShortListed";
                column1.Width = 100;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //19
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_IfScheduleDrug";
                column1.DataPropertyName = "ProdIfSchedule";
                column1.HeaderText = "Sch";
                column1.Width = 50;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //20
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_ScheduleDrugCode";
                column2.DataPropertyName = "ProdScheduleDrugCode";
                column2.HeaderText = "Sch[H/K/N]";
                column2.Width = 60;
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                column2.Items.Add("K");
                column2.Items.Add("H");
                column2.Items.Add("H1");
                column2.Items.Add("N");
                column2.Items.Add("H2");
                column2.Items.Add("H3");
                column2.DisplayMember = "H";
                column2.ValueMember = "";
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //21
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_IfSaleDisc";
                column1.DataPropertyName = "ProdIfSaleDisc";
                column1.HeaderText = "SaleDis";
                column1.Width = 60;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //22
                column2 = new DataGridViewComboBoxColumn();
                column2.Name = "Col_ProdGrade";
                column2.DataPropertyName = "ProdGrade";
                column2.HeaderText = "Grade";
                column2.Width = 60;
                column2.Items.Add("A");
                column2.Items.Add("B");
                column2.Items.Add("C");
                column2.Items.Add("D");
                column2.Items.Add("Z");
                column2.DisplayMember = "B";
                column2.ValueMember = "";
                column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column2);
                //23
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_IfBarCodeRequired";
                column1.DataPropertyName = "ProdIfBarCodeRequired";
                column1.HeaderText = "BarCodeReq.";
                column1.Width = 80;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);
                //24
                column1 = new DataGridViewTextBoxColumn();
                column1.Name = "Col_ScannedBarCode";
                column1.DataPropertyName = "ScannedBarCode";
                column1.HeaderText = "ProdBarcode";
                column1.Width = 180;
                column1.ReadOnly = false;
                ActiveDataGrid.Columns.Add(column1);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        private DataTable ConstructTempColumns(DataTable TempDataTable)
        {
            TempDataTable.Columns.Clear();
            try
            {
                TempDataTable.Columns.Add(new DataColumn("ProductID",typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdName", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdCompID", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdCompShortName", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdLoosePack", typeof(int)));
                TempDataTable.Columns.Add(new DataColumn("ProdPack", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("prodpacktype", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdVATPercent", typeof(double)));
                TempDataTable.Columns.Add(new DataColumn("ProdCST", typeof(double)));
                TempDataTable.Columns.Add(new DataColumn("ProdMinLevel", typeof(int)));
                TempDataTable.Columns.Add(new DataColumn("ProdMaxLevel", typeof(int)));
                TempDataTable.Columns.Add(new DataColumn("ProdRequireColdStorage", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdShelfID", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdDrugID", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdCategoryID", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdPartyId_1", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdPartyId_2", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdBoxQuantity", typeof(int)));
                TempDataTable.Columns.Add(new DataColumn("ProdIfShortListed", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdIfSchedule", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdScheduleDrugCode", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdIfSaleDisc", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdGrade", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ProdIfBarCodeRequired", typeof(string)));
                TempDataTable.Columns.Add(new DataColumn("ScannedBarCode", typeof(string)));

                return TempDataTable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                return null;
            }

        }

        private void BulkProductGrid_Scroll(object sender, ScrollEventArgs e)
        {
            //try
            //{
            //    bool flag = false;
            //}
            //catch(Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }

        private void BulkProductGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                if(BulkProductGrid.Rows.Count == 0 || BulkProductGrid.CurrentRow.Index == 0)
                {
                    if (txtType.Visible)
                        txtType.Focus();
                    else if (cbCompany.Visible)
                        cbCompany.Focus();
                    else
                        cbGenericCat.Focus();
                }
            }
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            txtType.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up)
                    cmbCharacterWise.Focus();
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtType.Text.Length >= 3)
                    {
                        BulkProductGrid.Enabled = true;
                        DataTable NewDT = new DataTable();
                        DataRow[] Rows;
                        if (radioButton1.Checked)
                            Rows = SDtable.Select("ProdName like '" + txtType.Text + "%'", "ProdName ASC");
                        else if (radioButton2.Checked)
                            Rows = TDtable.Select("ProdName like '" + txtType.Text + "%'", "ProdName ASC");
                        else
                            Rows = NTDtable.Select("ProdName like '" + txtType.Text + "%'", "ProdName ASC");
                        if (Rows != null)
                        {
                            NewDT = ConstructTempColumns(NewDT);
                            foreach (DataRow dr in Rows)
                            {
                                NewDT.ImportRow(dr);
                            }
                        }
                        BulkProductGrid.DataSource = NewDT;
                        BindGrid(NewDT);
                    }
                    else
                    {
                        MessageBox.Show("PLEASE ENTER AT LEAST THREE CHARACTERS", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtType.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.Message);
            }
        }

        
        private void cbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (cbCompany.SeletedItem != null)
                {
                    BulkProductGrid.Enabled = true;
                    DataTable NewDT = new DataTable();
                    DataRow[] Rows;
                    if (radioButton1.Checked)
                        Rows = SDtable.Select("ProdCompID = '" + cbCompany.SelectedID + "'", "ProdName ASC");
                    else if (radioButton2.Checked)
                        Rows = TDtable.Select("ProdCompID = '" + cbCompany.SelectedID + "'", "ProdName ASC");
                    else
                        Rows = NTDtable.Select("ProdCompID = '" + cbCompany.SelectedID + "'", "ProdName ASC");
                    if (Rows != null)
                    {
                        NewDT = ConstructTempColumns(NewDT);
                        foreach (DataRow dr in Rows)
                        {
                            NewDT.ImportRow(dr);
                        }
                    }
                    BulkProductGrid.DataSource = NewDT;
                    BindGrid(NewDT);
                }
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.Message);
            }
        }

        private void cbGenericCat_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (cbGenericCat.SeletedItem != null)
                {
                    BulkProductGrid.Enabled = true;
                    DataTable NewDT = new DataTable();
                    DataRow[] Rows;
                    if (radioButton1.Checked)
                        Rows = SDtable.Select("ProdDrugID = '" + cbGenericCat.SeletedItem.ItemData[0].ToString() + "'", "ProdName ASC");
                    else if (radioButton2.Checked)
                        Rows = TDtable.Select("ProdDrugID = '" + cbGenericCat.SeletedItem.ItemData[0].ToString() + "'", "ProdName ASC");
                    else
                        Rows = NTDtable.Select("ProdDrugID = '" + cbGenericCat.SeletedItem.ItemData[0].ToString() + "'", "ProdName ASC");
                    if (Rows != null)
                    {
                        NewDT = ConstructTempColumns(NewDT);
                        foreach (DataRow dr in Rows)
                        {
                            NewDT.ImportRow(dr);
                        }
                    }
                    BulkProductGrid.DataSource = NewDT;
                    BindGrid(NewDT);
                }
            }
            catch(Exception ex)
            {
                Log.WriteInfo(ex.Message);
            }
        }

        private void BulkProductGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (BulkProductGrid.CurrentCell != null)
            {
                if (e.RowIndex == BulkProductGrid.CurrentCell.RowIndex && e.ColumnIndex == BulkProductGrid.CurrentCell.ColumnIndex)
                    e.CellStyle.SelectionBackColor = Color.Red;
                else
                    e.CellStyle.SelectionBackColor = BulkProductGrid.DefaultCellStyle.SelectionBackColor;
            }
        }

        private void BulkProductGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (e.Control.GetType() != typeof(DataGridViewComboBoxEditingControl)) return;
            //if (((ComboBox)e.Control).SelectedIndex == 0)
            //{
            //    //If user selected first combobox value "Custom", make control editable
            //    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
            //}
            //else
            //{
            //    if (((ComboBox)e.Control).DropDownStyle != ComboBoxStyle.DropDown) return;
            //    //If different value and combobox was set to editable, disable editing
            //    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDownList;
            //}
        }

        private void BulkProductGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //DataGridView dataGridView = sender as DataGridView;
            //if (dataGridView == null) return;
            //if (!dataGridView.CurrentCell.IsInEditMode) return;
            //if (dataGridView.CurrentCell.GetType() != typeof(DataGridViewComboBoxCell)) return;
            //DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //if (cell.Items.Contains(e.FormattedValue)) return;
            //cell.Items.Add(e.FormattedValue);
            //cell.Value = e.FormattedValue;
            //if (((DataGridViewComboBoxColumn)dataGridView.Columns[0]).Items.Contains(e.FormattedValue)) return;
            //((DataGridViewComboBoxColumn)dataGridView.Columns[0]).Items.Add(e.FormattedValue);
        }

        private void BulkProductGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //DataGridView dataGridView = sender as DataGridView;
            //if (dataGridView == null || dataGridView.CurrentCell.ColumnIndex != 0) return;
            //var dataGridViewComboBoxCell = dataGridView.CurrentCell as DataGridViewComboBoxCell;
            //if (dataGridViewComboBoxCell != null)
            //{
            //    if (dataGridViewComboBoxCell.EditedFormattedValue.ToString() == "Custom")
            //    {
            //        //Here we move focus to second cell of current row
            //        dataGridView.CurrentCell = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[1];
            //        //Return focus to Combobox cell
            //        dataGridView.CurrentCell = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[0];
            //        //Initiate Edit mode
            //        dataGridView.BeginEdit(true);
            //        return;
            //    }
            //}
            //dataGridView.CurrentCell = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[1];
            //dataGridView.BeginEdit(true);
        }

        private void BulkProductGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (BulkProductGrid.CurrentCell.ColumnIndex == 2)
                {
                    DataRow drow = null;
                    Company cmp = new Company();
                    string ID = BulkProductGrid.CurrentCell.Value.ToString();
                    drow = cmp.ReadDetailsByID(ID);
                    BulkProductGrid.Rows[BulkProductGrid.CurrentCell.RowIndex].Cells[BulkProductGrid.CurrentCell.ColumnIndex + 1].Value = drow["CompShortName"];
                }
            }
            catch (Exception ex)
            { Log.WriteInfo(ex.Message); }
        }

        public void BindGrid(DataTable dt)
        {
            for (int index = 0; index < dt.Rows.Count; index++)
            {
                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    if (dt.Columns[colIndex].ColumnName == "ProdPack")
                    {
                        if (dt.Rows[index][colIndex].ToString() != "" && dt.Rows[index][colIndex].ToString() != " " && dt.Rows[index][colIndex] != null)
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                        else
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                    }
                    if (dt.Columns[colIndex].ColumnName == "prodpacktype")
                    {
                        if (dt.Rows[index][colIndex] != null && dt.Rows[index][colIndex].ToString() != "")
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                        else
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                    }
                    if (dt.Columns[colIndex].ColumnName == "ProdScheduleDrugCode" || dt.Columns[colIndex].ColumnName == "ProdGrade")
                    {
                        if (dt.Rows[index][colIndex].ToString() != "" && dt.Rows[index][colIndex].ToString() != " " && dt.Rows[index][colIndex] != null)
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = dt.Rows[index][colIndex].ToString();
                        else
                            BulkProductGrid.Rows[index].Cells[colIndex].Value = "";
                    }
                    if (dt.Columns[colIndex].ColumnName == "ProductID")
                    {
                        int Pname = Convert.ToInt32(dt.Rows[index][colIndex].ToString());

                        Stock StockObj = new Stock();
                        DataRow drow = null;
                        drow = StockObj.GetProductIDExists(Pname);
                        if (drow != null)
                            BulkProductGrid.Rows[index].DefaultCellStyle.BackColor = Color.Violet;
                    }
                }
            }
            for (int index = 0; index < BulkProductGrid.Rows.Count; index++)
            {
                for (int colindex = 0; colindex < BulkProductGrid.Columns.Count; colindex++)
                {
                    if (BulkProductGrid.Rows[index].DefaultCellStyle.BackColor == Color.Violet)
                        BulkProductGrid.Rows[index].Cells["Col_UOM"].ReadOnly = true;
                }
            }
            BulkProductGrid.Focus();
            RowCount.Text = dt.Rows.Count.ToString();
            BulkProductGrid.Columns["ProductID"].Frozen = true;
            BulkProductGrid.Columns["ProdName"].Frozen = true;
        }

        private void BulkProductGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                radioButton2.Focus();
            else if (e.KeyCode == Keys.Enter)
                cmbCharacterWise.Focus();
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                radioButton3.Focus();
            else if (e.KeyCode == Keys.Left)
                radioButton1.Focus();
            else if (e.KeyCode == Keys.Enter)
                cmbCharacterWise.Focus();
        }

        private void radioButton3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                radioButton2.Focus();
            else if (e.KeyCode == Keys.Right)
                radioButton3.Focus();
            else if (e.KeyCode == Keys.Enter)
                cmbCharacterWise.Focus();
        }

        private void cmbCharacterWise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                radioButton1.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (_SearchMode == SearchMode.ProductWise)
                    txtType.Focus();
                else if (_SearchMode == SearchMode.CompanyWise)
                    cbCompany.Focus();
                else
                    cbGenericCat.Focus();
            }
        }

        private void cbCompany_UpArrowPressed(object sender, EventArgs e)
        {
            cmbCharacterWise.Focus();
        }

        private void cbGenericCat_UpArrowPressed(object sender, EventArgs e)
        {
            cmbCharacterWise.Focus();
        }
    }
}
