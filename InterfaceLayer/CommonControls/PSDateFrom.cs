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
    public partial class FromDate : DateTimePicker
    {
        //protected override void OnPaint(PaintEventArgs e)
        //{

        //    base.OnPaint(e);

        //    e.Graphics.FillRectangle(new SolidBrush(Color.Red), this.ClientRectangle);

        //    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(Color.Blue), 0, 0);

        //    e.Graphics.DrawImage(Properties.Resources.dropdowntriangle , new Point(this.ClientRectangle.X + this.ClientRectangle.Width - 16, this.ClientRectangle.Y));

        //}
        public FromDate()
        {
          //  this.SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();           
            FontFamily FF = new FontFamily("Verdana");
            FontStyle FS = new FontStyle();
            FS = FontStyle.Regular;
            this.Font = new System.Drawing.Font(FF, 10, FS);
            this.Width = 125;
            this.CustomFormat = "dd/MM/yyyy";

        }

        public FromDate(IContainer container)
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

        public bool ShouldSerializeFont()
        {
            return false;
        }     

        private void FromDate_Enter(object sender, EventArgs e)
        {
            this.BackColor = EcoMart.Common.General.ControlFocusColor;
        }

        private void FromDate_Leave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
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
