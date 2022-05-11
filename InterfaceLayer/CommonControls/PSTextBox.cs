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

        private bool _IsNumericDataSet = false;
        public bool IsNumericDataSet
        {
            get { return _IsNumericDataSet; }
            set { _IsNumericDataSet = value; }
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
        protected override void OnLeave(EventArgs e)
        {
            this.BackColor = Color.White;
            base.OnLeave(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.BackColor = Color.NavajoWhite;
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
           if(IsNumericDataSet == true)
            {
                if (!char.IsControl(e.KeyChar)
                   && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
