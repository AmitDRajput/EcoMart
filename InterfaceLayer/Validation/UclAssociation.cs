using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer.Validation
{
    public partial class UclAssociation : UserControl, IValidationControl
    {
        public UclAssociation()
        {
            InitializeComponent();
        }

       
        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            if (OnStateOk != null)
            {
            }
            if (OnStateError != null)
            {
            }
            ConstructAssociationGridColumns();
            FillAssociationInfo();
        }       

        public event EventHandler OnStateOk;

        public event EventHandler OnStateError;

        #endregion

        private void ConstructAssociationGridColumns()
        {
            try
            {
                dgvAssociation.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "#";
                column.ValueType = typeof(string);
                column.Visible = true;
                column.ReadOnly = true;
                column.Width = 40;
                dgvAssociation.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MACID";
                column.HeaderText = "MAC-ID";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvAssociation.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Associate";
                column.HeaderText = "Associate";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 75;
                dgvAssociation.Columns.Add(column);          
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillAssociationInfo()
        {
            try
            {
                dgvAssociation.Rows.Clear();
                for (int index = 0; index < General.EcoMartLicense.AssociationList.Count; index++)
                {
                    int selectedrowindex = dgvAssociation.Rows.Add();
                    dgvAssociation.Rows[selectedrowindex].Cells["Col_ID"].Value = index + 1;
                    dgvAssociation.Rows[selectedrowindex].Cells["Col_MACID"].Value = General.EcoMartLicense.AssociationList.Item(index);
                    dgvAssociation.Rows[selectedrowindex].Cells["Col_Associate"].Value = "...";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgvAssociation_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dgvAssociation.SelectedRows.Count > 0)
                {
                    FormMacIDList formMacID = new FormMacIDList();
                    formMacID.LoadControl();
                    if (formMacID.ShowDialog() == DialogResult.OK)
                    {
                        string macID = formMacID.SelectedMACID();
                        if (IsMACISAlreadyAssociated(macID) == false)
                        {
                            DataGridViewRow row = dgvAssociation.SelectedRows[0];
                            int selectedrowindex = row.Index;
                            dgvAssociation.Rows[selectedrowindex].Cells["Col_MACID"].Value = macID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            this.Cursor = Cursors.Default;
        }

        private bool IsMACISAlreadyAssociated(string macId)
        {
            bool retValue = false;
            try
            {
                foreach (DataGridViewRow row in dgvAssociation.Rows)
                {
                    if (row.Cells["Col_MACID"].Value.ToString() == macId)
                    {
                        retValue = true;
                        MessageBox.Show(string.Format("MAC ID: '{0}' already associated...!", macId), General.ApplicationTitle);
                        break;
                    }
                }
                if (!retValue)
                {
                    string macAddressLocal = EcoMart.InterfaceLayer.Classes.NetworkBrowser.GetLocalMacAddress();
                    DataGridViewRow row = dgvAssociation.SelectedRows[0];
                    int selectedrowindex = row.Index;
                    string macAddressGrid=dgvAssociation.Rows[selectedrowindex].Cells["Col_MACID"].Value.ToString();
                    if (macAddressGrid != string.Empty && macAddressGrid == macAddressLocal)
                    {
                        DialogResult result = MessageBox.Show(string.Format("Do you want to change already associated local MAC ID?", macId), General.ApplicationTitle, MessageBoxButtons.YesNo);
                        if (result==DialogResult.Yes)
                            retValue = false;
                        else
                            retValue = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public List<string> GetAssociationList()
        {
            List<string> associationList = new List<string>();
            try
            {
                foreach (DataGridViewRow row in dgvAssociation.Rows)
                {
                    associationList.Add(row.Cells["Col_MACID"].Value.ToString());                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return associationList;
        }
    }
}
