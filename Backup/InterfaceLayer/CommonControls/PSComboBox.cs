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
    public partial class PSComboBox : ComboBox
    {
        public PSComboBox()
        {
            InitializeComponent();
        }

        public PSComboBox(IContainer container)
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

        private void PSComboBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void PSComboBox_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.NavajoWhite;
        }

        public bool ShouldSerializeForeColor()
        {
            return false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();          
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));        
            this.ResumeLayout(false);

        }
    }
}
