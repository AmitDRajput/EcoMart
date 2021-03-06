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
    public partial class PSLableWithBorderMiddleRight : Label
    {
        public PSLableWithBorderMiddleRight()
        {
            InitializeComponent();
            this.AutoSize = false;
        }

        public PSLableWithBorderMiddleRight(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = false;
            }
        }
      
        public bool ShouldSerializeFont()
        {
            return false;
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

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public bool ShouldSerializeForeColor()
        {
            return false;
        }
    }
}
