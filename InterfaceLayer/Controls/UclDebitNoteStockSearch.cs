using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDebitNoteStockSearch : SearchControl
    {
        # region declaration
        private DataTable _BindingSource;
        private DebitNoteStock _DebitNoteStock;    
        #endregion

        # region constructor
        public UclDebitNoteStockSearch()
        {
            try
            {
                InitializeComponent();
                this.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _DebitNoteStock = new DebitNoteStock();
                headerLabelForOverView1.Text = "DEBIT NOTE STOCK OVERVIEW";
                ConstructSearchColumns();
                FillSearchGrid();
                dgvSearchList.ShowGridFilter = true;
                InitializeDate();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            dgvSearchList.SetFocus();
        }
        public override string SelectedID()
        {
            string retValue = "";
            try
            {
                if (dgvSearchList.Rows.Count > 0)
                {
                    retValue = dgvSearchList.SelectedRow.Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        #endregion ShowOverview

        #region Other private methods

        private void ConstructSearchColumns()  // [Ansuman][16.11.2016]
        {
            dgvSearchList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CRDBID";
                column.HeaderText = "ID";
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNo";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();   // [Ansuman] [16.11.2016]
                column.Name = "Col_CVoucherDate";
                column.DataPropertyName = "ClearedInVoucherDate";
                column.HeaderText = "Bill.Date";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();   // [Ansuman] [16.11.2016]
                column.Name = "Col_CBillNumber";
                column.DataPropertyName = "ClearedInPurchaseBillNumber";
                column.HeaderText = "Bill.NO";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //column.ValueType = typeof(double);
                dgvSearchList.Columns.Add(column);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        //Fill Grid
        public override void ProcessDateFromToClick()
        {
            FillSearchGrid();
        }
        private void FillSearchGrid()
        {
            try
            {
                FillSearchGridData();
                dgvSearchList.DataSource = _BindingSource;
                dgvSearchList.Bind();
                dgvSearchList.DateColumnNames.Add("Col_VoucherDate");
                dgvSearchList.DateColumnNames.Add("Col_CVoucherDate");  // [ansuman] [16.11.2016]
                //dgvSearchList.DateColumnNames.Add("Col_CBillNumber");   // [ansuman] [16.11.2016]
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillSearchGridData()
        {
            try
            {
                DataTable dtable = new DataTable();
                if (General.ifYearEndOverForthePreviousYear == "Y")
                dtable = _DebitNoteStock.GetOverviewDataForAllYears(_DebitNoteStock.CrdbVouType);
                else
                    dtable = _DebitNoteStock.GetOverviewData(_DebitNoteStock.CrdbVouType);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        #endregion

        #region Events

        private void dgvSearchList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvSearchList.SelectedRow != null)
                {
                    GridRowDoubleClicked();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion       
       
    }

 
    }

