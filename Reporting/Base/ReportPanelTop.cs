using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EcoMart.Reporting.Base
{
    public partial class ReportPanelTop : UserControl
    {
       
        public ReportPanelTop()
        {
            InitializeComponent();
        }

        public string DataField1Caption
        {
            get
            {
                return lblDataField1.Text;
            }
            set
            {
                lblDataField1.Text = value;
            }
        } 


        public string DataField1Value
        {
            get 
            {
                return txtDataField1.Text;
            }
            set 
            {              
                txtDataField1.Text = value;
            }
        }

       
    }
}
