using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaSYSPlus.CommonLibrary
{
    public partial class GeneralTextBox : TextBox
    {
        private TextTransformation _textTransform = TextTransformation.Default;
        public GeneralTextBox()
        {
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

        public bool ShouldSerializeFont()
        {
            return false;
        }

        public TextTransformation TextTransform
        {
            get { return _textTransform; }
            set { this._textTransform = value; }
        }
       
        protected override void OnTextChanged(EventArgs e)
        {
            if (this._textTransform == TextTransformation.UpperCase)
            {
                this.Text = this.Text.ToUpperInvariant();
                this.SelectionStart = this.Text.Length;
            }
            else if (this._textTransform == TextTransformation.LowerCase)
            {
                this.Text = this.Text.ToLowerInvariant();
                this.SelectionStart = this.Text.Length;
            }
            base.OnTextChanged(e);
        }       

        public enum TextTransformation
        {
            UpperCase,
            LowerCase,
            Default
        }

        private void GeneralTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void GeneralTextBox_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.NavajoWhite;
        }
    }
}
