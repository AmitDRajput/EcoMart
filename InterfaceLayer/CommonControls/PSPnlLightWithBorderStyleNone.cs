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
    public partial class PSPnlLightWithBorderStyleNone : Panel
    {
        public PSPnlLightWithBorderStyleNone()
        {
            InitializeComponent();
          //  this.BorderStyle = BorderStyle.FixedSingle;
            
        }

        public PSPnlLightWithBorderStyleNone(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
       

        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        public bool ShouldSerializeBackColor()
        {
            return false;
        }

       
       
    }
}
