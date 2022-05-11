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
    public partial class PSButton : Button
    {
        public PSButton()
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

        private void PSButton_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void PSButton_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.NavajoWhite;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MPButton
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size = new System.Drawing.Size(93, 28);
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }
    }
}
