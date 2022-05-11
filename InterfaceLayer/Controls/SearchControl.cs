using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SearchControl : UserControl
    {
        public event EventHandler ExitClicked;

        public delegate void GridDoubleClicked(string ID);
        public event GridDoubleClicked OnGridDoubleClicked;
        // ss
        public delegate void EndKeyPressed(string ekey);
        public event EndKeyPressed OnEndKeyPressed;
        //ss      

        public SearchControl()
        {
            InitializeComponent();           
        }

        internal void InitializeDate()
        {
            try
            {
                dtFrom.Value = DateTime.Now;
                dtFrom.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                dtFrom.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);

                dtTo.Value = DateTime.Now;
                dtTo.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                dtTo.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);

                if (General.CurrentSetting.MsetGeneralAskDatesInSearch == "Y")
                {
                    dtFrom.Visible = true;
                    dtTo.Visible = true;
                    btnDateFromToGo.Visible = true;
                    mPlbl2.Visible = true;
                    mPlbl4.Visible = true;
                }
                else
                {
                    mPlbl4.Visible = false;
                    mPlbl2.Visible = false;
                    dtFrom.Visible = false;
                    dtTo.Visible = false;
                    btnDateFromToGo.Visible = false;
                    dtFrom.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                    dtTo.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey); 
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #region IOverview Members

        public virtual void ShowOverview()
        {
            
        }

        public virtual int GetWidth()
        {
            return 1000;
        }

        public virtual string SelectedID()
        {
            return "";
        }
        public virtual string ekey()
        {
            return "";
        }
        public virtual void GridRowDoubleClicked()
        {
            if (OnGridDoubleClicked != null)
                OnGridDoubleClicked(SelectedID());
        }
        public virtual void EndKeyPressed1()
        {
            if (OnEndKeyPressed != null)
                OnEndKeyPressed(ekey());
        }
        public virtual void SetFocus()
        {
            if (MMDatePanel.Visible)
                dtFrom.Focus(); 
        }

        public virtual void ProcessDateFromToClick()
        {
        }
      
        #endregion       

        private void headerLabelForOverView1_ExitClicked(object sender, EventArgs e)
        {
            if (ExitClicked != null)
                ExitClicked(sender, e);    
        }

        private void dtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtTo.Focus();
            }
        }

        private void dtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessDateFromToClick();
            }
        }

        private void btnDateFromToGo_Click(object sender, EventArgs e)
        {
            ProcessDateFromToClick();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                KeyEventArgs e = new KeyEventArgs(keyData);
                if (ExitClicked != null)
                    ExitClicked(this, e);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}