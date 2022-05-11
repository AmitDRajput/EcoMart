using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public class PSCheckBox: CheckBox
    {
        public PSCheckBox()
        {
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

        public bool ShouldSerializeForeColor()
        {
            return false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PSCheckBox
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }
    }
}
