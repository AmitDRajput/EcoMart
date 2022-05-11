using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PharmaSYSPlus.CommonLibrary
{
    public partial class NumericTextBox : TextBox
    {        
        public NumericTextBox()
        {
            InitializeComponent();
        }

        public override System.Drawing.Font Font
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

        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers != Keys.None || IsValidValue(e.KeyValue) == false)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
               Log.WriteError(ex.ToString());
            }           
        }

        private bool IsValidValue(int Val)
        {
            bool retValue = true;
            if ((Val >= 48 && Val <= 57) || (Val >= 96 && Val <= 105)
              || (Val == 8) || (Val == 46) || (Val == 110) || (Val == 37) || (Val == 39)) // 46=Del 8=Backspace 37=LeftArrow 39=RightArrow
                retValue = true;            
            else
                retValue = false;
            return retValue;
        }        

        private void NumericTextBox_Leave(object sender, EventArgs e)
        {
            int intValue;
            int.TryParse(this.Text, out intValue);
            this.Text = intValue.ToString("0");
            this.BackColor = Color.White;
        }

        private void NumericTextBox_Enter(object sender, EventArgs e)
        {
            int intValue;
            int.TryParse(this.Text, out intValue);
            if (intValue == 0)
                this.Text = "";
            this.BackColor = Color.NavajoWhite;
        }

    }
}
