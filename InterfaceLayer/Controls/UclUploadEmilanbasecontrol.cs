using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDE2;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;
//using MySql.Data.MySqlClient;
using System.IO;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclUploadEmilan : BaseControl
    {
        public DataTable emilandt;
       
        public UclUploadEmilan()
        {
            InitializeComponent();
        }
        public void FillDetails()
        {
            ConstructPurchaseOrderColumns();
            BindPurchaseOrder(emilandt);
            dgPurchaseOrder.Focus();            
        }
        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            Emilan em = new Emilan();
            DataTable dt =  em.GetPurchaseOrdersForOrderForToday();
            ConstructPurchaseOrderColumns();
            BindPurchaseOrder(dt);
            dgPurchaseOrder.Focus();
        }
         private void ConstructPurchaseOrderColumns()
        {
           dgPurchaseOrder.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
             
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgPurchaseOrder.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";
              //  column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series";
                column.Width = 50;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
             //   column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
            //    column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgPurchaseOrder.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
            //    column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Party";
               // column.DataPropertyName = "Narration";
                column.HeaderText = "Party";
                column.Width = 160;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narration";
                // column.DataPropertyName = "Narration";
                column.HeaderText = "Remark";
                column.Width = 160;
                column.ReadOnly = false;
                dgPurchaseOrder.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
             //   column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPurchaseOrder.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool BindPurchaseOrder(DataTable dt)
        {
            bool retValue = true;
            double amt = 0;
            try
            {

                if (dgPurchaseOrder != null)
                    dgPurchaseOrder.Rows.Clear();
                int _RowIndex = 0;
                int oldordernumber = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    int ordno = Convert.ToInt32(dr["OrderNumber"].ToString());
                    if (ordno != oldordernumber)
                    {
                        oldordernumber = ordno;
                        _RowIndex = dgPurchaseOrder.Rows.Add();
                        string voudt = "";                      
                        DataGridViewRow currentdr = dgPurchaseOrder.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["DSLID"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        //  currentdr.Cells["Col_VoucherSeries"].Value = dr["VoucherSeries"].ToString();
                        //  currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["OrderNumber"].ToString();
                        if (dr["Date"] != DBNull.Value && dr["Date"].ToString() != "")
                        {
                            voudt = Convert.ToDateTime(dr["Date"]).Date.ToString("yyyyMMdd");
                            voudt = General.GetDateInShortDateFormat(voudt);
                        }
                        currentdr.Cells["Col_VoucherDate"].Value = voudt;
                         amt = Convert.ToDouble(dr["Amount"].ToString());
                          currentdr.Cells["Col_AmountNet"].Value = amt.ToString("#0.00");
                        //   currentdr.Cells["Col_Narr"].Value = dr["Remark"].ToString();
                        currentdr.Cells["Col_Party"].Value = dr["AccName"].ToString();
                        currentdr.Cells["Col_Check"].Value = string.Empty;

                        _RowIndex += 1;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }
        private void dgPurchaseOrder_KeyDown(object sender, KeyEventArgs e)
        {
            string ifchecked = string.Empty;
            //if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            //{
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value != null && dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ifchecked = dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString();
                    if (ifchecked != string.Empty)
                        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                    else
                        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                }
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BtnOKClick();
        }

        public void BtnOKClick()
        {
            DataTable newdt = new DataTable();
            DataTable tempdt = new DataTable();
            Emilan em = new Emilan();
            foreach (DataGridViewRow dr in dgPurchaseOrder.Rows)
            {
                if (dr.Cells["Col_Check"].Value != string.Empty)
                {
                    string masterid = "";
                    if (dr.Cells["Col_MasterID"].Value != null && dr.Cells["Col_MasterID"].Value != string.Empty)
                    {
                        masterid = dr.Cells["Col_MasterID"].Value.ToString();
                        tempdt = em.GetOrderDetails(masterid);

                        foreach (DataRow tmpdr in tempdt.Rows)
                        {
                            newdt.Rows.Add(tmpdr);
                        }
                    }
                }
            }
        }
    }
}
