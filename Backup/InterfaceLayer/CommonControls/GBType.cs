using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class GBType : UserControl
    {
       
        public GBType()
        {
            InitializeComponent();
        }

        public GBType(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
