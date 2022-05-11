using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class PSHeaderLabel : UserControl
    {       
        public PSHeaderLabel()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                lblHeaderCaption.Text = value;
            }
        }       
   
    }
}
