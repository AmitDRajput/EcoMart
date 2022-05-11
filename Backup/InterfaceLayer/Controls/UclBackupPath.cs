using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer
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
    }
}
