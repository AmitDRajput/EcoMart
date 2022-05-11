using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSPnlLight : Panel
    {
        public PSPnlLight()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.LightYellow;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public PSPnlLight(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
