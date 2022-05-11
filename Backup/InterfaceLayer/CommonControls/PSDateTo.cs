using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class ToDate : DateTimePicker
    {
        public ToDate()
        {
            InitializeComponent();
            this.CustomFormat = "dd/MM/yyyy";
            this.Format = DateTimePickerFormat.Custom;
            FontFamily FF = new FontFamily("Verdana");
            FontStyle FS = new FontStyle();
            FS = FontStyle.Regular;
            this.Font = new System.Drawing.Font(FF, 11, FS);
            this.Width = 125;
        }

        public ToDate(IContainer container)
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

        }

        private void FromDate_Leave(object sender, EventArgs e)
        {

        }
    }
}
