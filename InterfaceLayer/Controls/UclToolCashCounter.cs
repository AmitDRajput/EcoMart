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
using EcoMart.Printing;

namespace EcoMart.InterfaceLayer
{
     [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolCashCounter : BaseControl
    {
        # region Declaration
         SaleList _SaleList;
        # endregion
        public UclToolCashCounter()
        {
            InitializeComponent();
            _SaleList = new SaleList();           
           // DoCashCounter();
          
        }

        public override bool Exit()
        {
            return base.Exit();
        }
        public override void ReFillData(Control closedControl)
        {
            DoCashCounter();
               dgBillList.Focus();
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (keyPressed == Keys.End)
            {
                DoCashCounter();
                retValue = true;
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        private void cashCounterDate_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                DoCashCounter();
            }
        }

        private void DoCashCounter()
        {
           
            string toDate = cashCounterDate.Value.Date.ToString("yyyyMMdd");
            DataTable dt = new DataTable();
            ConstructdgBillListColumns();
            dt = _SaleList.GetDataForCashCounter(toDate);
            BinddgBillList(dt);
            
        }

        private void ConstructdgBillListColumns()
        {
            dgBillList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";               
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);

                //DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                //columnCheck.Name = "Col_Check";
                //columnCheck.HeaderText = "Check";
                //columnCheck.Width = 50;
                //columnCheck.Visible = true;
                //dgCreditNote.ColumnsMain.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgBillList.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";               
                column.HeaderText = "Series";
                column.Visible = false;
                column.Width = 50;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgBillList.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";               
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientName";
                column.HeaderText = "Name";
                column.Width = 200;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientAddress";
                column.HeaderText = "Address";
                column.Width = 200;
                column.ReadOnly = true;
                dgBillList.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBillList.Columns.Add(column);              
              
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool BinddgBillList(DataTable dt)
        {
            bool retValue = true;
            double amt = 0;
            try
            {

                if (dgBillList != null)
                    dgBillList.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgBillList.Rows.Add();
                    string voudt = "";
                    //double amtclear = 0;
                    DataGridViewRow currentdr = dgBillList.Rows[_RowIndex];
                    currentdr.Cells["Col_CrdbID"].Value = dr["ID"].ToString();
                 //   currentdr.Cells["Col_VoucherSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    amt = Convert.ToDouble(dr["AmountNet"].ToString());
                    currentdr.Cells["Col_AmountNet"].Value = amt.ToString("#0.00");
                    currentdr.Cells["Col_PatientName"].Value = dr["PatientName"].ToString();
                    currentdr.Cells["Col_PatientAddress"].Value = dr["PatientAddress1"].ToString();
                   
                        currentdr.Cells["Col_Check"].Value = string.Empty;
                    //else if (amtclear != 0)
                    //    currentdr.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                    //else
                    //    currentdr.Cells["Col_Check"].Value = string.Empty;

                    _RowIndex += 1;

                }
                dgBillList.Focus();

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }
        private void dgBillList_KeyDown(object sender, KeyEventArgs e)
        {
            string ifchecked = string.Empty;
           
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgBillList.CurrentRow.Cells["Col_Check"].Value != null && dgBillList.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ifchecked = dgBillList.CurrentRow.Cells["Col_Check"].Value.ToString();
                    if (ifchecked != string.Empty)
                        dgBillList.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                    else
                    {
                        if (dgBillList.CurrentRow.Cells["Col_CrdbID"].Value != null && dgBillList.CurrentRow.Cells["Col_CrdbID"].Value.ToString() != string.Empty)
                        {
                            dgBillList.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                            bool retValue = _SaleList.UpdateCashCounter(dgBillList.CurrentRow.Cells["Col_CrdbID"].Value.ToString());
                            DoCashCounter();
                        }


                    }

                  //  CalculateCRDBSelectedAmount();
                }
            
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        // //   timer1.Stop();
        //    try
        //    {
                
        //        DoCashCounter();
        //    //    timer1.Start();
                
        //    }
        //    catch (Exception Ex)
        //    {
                
        //        Log.WriteException(Ex);

        //    }
        //}

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DoCashCounter();
        }

        private void UclToolCashCounter_Load(object sender, EventArgs e)
        {
            DoCashCounter();
            dgBillList.Focus();
        }

        private void UclToolCashCounter_Enter(object sender, EventArgs e)
        {
            DoCashCounter();
            dgBillList.Focus();
        }

        

    }
}
