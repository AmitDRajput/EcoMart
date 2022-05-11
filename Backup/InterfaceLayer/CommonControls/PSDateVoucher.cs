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
    public partial class PSDateVoucher : DateTimePicker
    {
        public PSDateVoucher()
        {
            InitializeComponent();
            this.CustomFormat = "dd/MM/yyyy";
            this.Format = DateTimePickerFormat.Custom;
            FontFamily FF = new FontFamily("Verdana");
            FontStyle FS = new FontStyle();
            FS = FontStyle.Regular;
            this.Font = new System.Drawing.Font(FF, 12, FS);
            this.Width = 125;
        }

        public PSDateVoucher(IContainer container)
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
    }
}
