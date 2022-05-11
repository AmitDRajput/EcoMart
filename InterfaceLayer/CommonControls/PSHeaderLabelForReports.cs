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
    public partial class PSHeaderLabelForReports : UserControl
    {
        public PSHeaderLabelForReports()
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
    }
}
