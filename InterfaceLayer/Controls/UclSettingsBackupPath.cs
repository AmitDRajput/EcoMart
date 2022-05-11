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
    public partial class UclBackupPath : BaseControl
    {
        BackupPath _BackupPath = new BackupPath();

        public UclBackupPath()
        {
            InitializeComponent();
            if (General.BackupPath.BackupPath1 != null && General.BackupPath.BackupPath1.ToString() != string.Empty)
                txtBackupPath1.Text = General.BackupPath.BackupPath1.ToString();
            if (General.BackupPath.BackupPath2 != null && General.BackupPath.BackupPath2.ToString() != string.Empty)
                txtBackupPath2.Text = General.BackupPath.BackupPath2.ToString();
            if (General.BackupPath.BackupPath3 != null && General.BackupPath.BackupPath3.ToString() != string.Empty)
                txtBackupPath3.Text = General.BackupPath.BackupPath3.ToString();
     
        }

        private void btnBackupPath1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath1.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnBackupPath2_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath2.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnBackupPath3_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();                
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath3.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "BACKUP PATH SETTINGS";
        //    IfGetOverViewData = GetOverviewData();
            tsBtnCancel.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;         
            tsBtnExit.Enabled = true;
            return retValue;
        }
        public override bool Save()
        {
            General.BackupPath.BackupPath1 = txtBackupPath1.Text.ToString();
            General.BackupPath.BackupPath2 = txtBackupPath2.Text.ToString();
            General.BackupPath.BackupPath3 = txtBackupPath3.Text.ToString();
            bool retValue = _BackupPath.UpdateDetails();
            if (retValue)
            {
                MessageBox.Show("Backup Path saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return retValue;
        }

        private void btnclear1_Click(object sender, EventArgs e)
        {
            try
            {
                txtBackupPath1.Clear();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnclear2_Click(object sender, EventArgs e)
        {
            try
            {
                txtBackupPath2.Clear();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnclear3_Click(object sender, EventArgs e)
        {
            try
            {
                txtBackupPath3.Clear();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnclear1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode ==Keys.Enter)
                {
                    txtBackupPath1.Clear();
                }
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnclear2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtBackupPath1.Clear();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnclear3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtBackupPath1.Clear();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
