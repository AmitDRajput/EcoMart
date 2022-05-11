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
    public partial class PSRadioButton : RadioButton
    {

        public PSRadioButton()
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
            // PSRadioButton
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseVisualStyleBackColor = false;
            this.Leave += new System.EventHandler(this.PSRadioButton_Leave);
            this.Enter += new System.EventHandler(this.PSRadioButton_Enter);
            this.ResumeLayout(false);

        }

        private void PSRadioButton_Enter(object sender, EventArgs e)
        {
            this.BackColor = EcoMart.Common.General.ControlFocusColor;
        }

        private void PSRadioButton_Leave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Gainsboro;
        }
    }
}
