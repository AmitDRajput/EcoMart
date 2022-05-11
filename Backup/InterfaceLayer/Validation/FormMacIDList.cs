using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using System.Collections;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    public partial class FormMacIDList : Form
    {
        public FormMacIDList()
        {
            InitializeComponent();
        }

        public string SelectedMACID()
        {
            string selectedId = string.Empty;
            try
            {
                if (dgvMacIDList.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgvMacIDList.SelectedRows[0];
                    selectedId = row.Cells["Col_MACID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return selectedId;
        }

        public void LoadControl()
        {
            try
            {
                ConstructMACIDGridColumns();
                FillMACIDInfo();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructMACIDGridColumns()
        {
            try
            {
                dgvMacIDList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "#";
                column.ValueType = typeof(string);
                column.Visible = true;
                column.ReadOnly = true;
                column.Width = 40;
                dgvMacIDList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PCName";
                column.HeaderText = "PC Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 100;
                dgvMacIDList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MACID";
                column.HeaderText = "MAC-ID";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMacIDList.Columns.Add(column);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillMACIDInfo()
        {
            try
            {
                dgvMacIDList.Rows.Clear();
                ArrayList pcList = new ArrayList();
                pcList = NetworkBrowser.GetNetworkComputers();

                string macAddress = NetworkBrowser.GetLocalMacAddress();

                int index = 0;

                int selectedrowindex = dgvMacIDList.Rows.Add();
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_ID"].Value = index + 1;
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_PCName"].Value = "LOCAL PC";
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_MACID"].Value = macAddress;
                dgvMacIDList.Rows[selectedrowindex].DefaultCellStyle.BackColor = Color.Green;
                index++;

                foreach (string pcName in pcList)
                {
                    macAddress = NetworkBrowser.GetMacAddress(pcName);
                   
                    selectedrowindex = dgvMacIDList.Rows.Add();
                    dgvMacIDList.Rows[selectedrowindex].Cells["Col_ID"].Value = index + 1;
                    dgvMacIDList.Rows[selectedrowindex].Cells["Col_PCName"].Value = pcName;
                    dgvMacIDList.Rows[selectedrowindex].Cells["Col_MACID"].Value = macAddress;
                    index++;
                }                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }       

        private void dgvMacIDList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvMacIDList.SelectedRows.Count > 0)
                    DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormMacIDAdd formMacIDAdd = new FormMacIDAdd();
            formMacIDAdd.LoadControl();
            if (formMacIDAdd.ShowDialog() == DialogResult.OK)
            {                
                int selectedrowindex = dgvMacIDList.Rows.Add();
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_ID"].Value = dgvMacIDList.Rows.Count;
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_PCName"].Value = formMacIDAdd.GetComputerName();
                dgvMacIDList.Rows[selectedrowindex].Cells["Col_MACID"].Value = formMacIDAdd.GetComputerName();
                dgvMacIDList.Rows[selectedrowindex].Selected = true;
            }
        }
    }
}
