using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaSYSPlus.CommonLibrary
{
    public partial class DecimalTextBox : TextBox
    {
        public DecimalTextBox()
        {
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Right;
        }      

        private bool IsValidValue(int Val)
        {
            bool retValue = true;
            if ((Val >= 48 && Val <= 57) || (Val >= 96 && Val <= 105)
              || (Val == 8) || (Val == 46) || (Val == 110) || (Val == 37) || (Val == 39)) // 46=Del 8=Backspace 37=LeftArrow 39=RightArrow
                retValue = true;
            else if (Val == 190 && this.Text.ToString().IndexOf(".") == -1) //190=.
                retValue = true;
            else
                retValue = false;
            return retValue;
        }

        private void DecimalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.None || IsValidValue(e.KeyValue) == false)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void DecimalTextBox_Enter(object sender, EventArgs e)
        {
            //double dblValue;
            //double.TryParse(this.Text, out dblValue);
            //if (dblValue == 0)
            //    this.Text = "";
            if (this.ReadOnly == false)
                this.BackColor = Color.NavajoWhite;
        }

        private void DecimalTextBox_Leave(object sender, EventArgs e)
        {
            double doubleValue;
            double.TryParse(this.Text, out doubleValue);
            this.Text = doubleValue.ToString("0.00");
            this.BackColor = Color.White;
        }
       
    }
}
