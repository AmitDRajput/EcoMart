using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSHeaderLabelForOverView : UserControl
    {
        public event EventHandler ExitClicked;
        public PSHeaderLabelForOverView()
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
                lblHeaderCaptionForOverView.Text = value;
            }
        }       

        private void btnclosed_Click(object sender, EventArgs e)
        {
            if (ExitClicked != null)
                ExitClicked(sender, e);
        }
    }
}
