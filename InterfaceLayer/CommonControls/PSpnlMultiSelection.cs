using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSpnlMultiSelection : Panel
    {
        public PSpnlMultiSelection()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
           
        }

        public PSpnlMultiSelection(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
       

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
