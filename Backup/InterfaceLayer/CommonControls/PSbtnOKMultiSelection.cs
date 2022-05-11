using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class PSbtnOKMultiSelection : Button
    {
        public PSbtnOKMultiSelection()
        {
            InitializeComponent();

            //this.BackgroundImage = global::PharmaSYSRetailPlus.Properties.Resources.box_green1;
           // this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Height = 63;
            this.Width = 63;
            this.ForeColor = System.Drawing.Color.White;           
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = "Go";
            }
        }
    }
}
