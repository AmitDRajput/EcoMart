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
    public partial class PSTextBox : TextBox
    {
        public PSTextBox()
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
            }
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

        public bool ShouldSerializeFont()
        {
            return false;
        }

        private void PSTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void PSTextBox_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.NavajoWhite;
        }
    }
}
