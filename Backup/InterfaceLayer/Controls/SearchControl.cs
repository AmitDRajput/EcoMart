using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer
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

        #region IOverview Members

        public virtual void ShowOverview()
        {
            
        }

        public virtual int GetWidth()
        {
            return 700;
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

        }  
      
        #endregion       

        private void headerLabelForOverView1_ExitClicked(object sender, EventArgs e)
        {
            if (ExitClicked != null)
                ExitClicked(sender, e);    
        }
    }
}
