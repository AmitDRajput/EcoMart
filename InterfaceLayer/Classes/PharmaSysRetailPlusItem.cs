using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.Common;
using System.Windows.Forms;

namespace EcoMart.InterfaceLayer
{
    public class ControlItem
    {
        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
                
        private OperationMode _ItemMode;
        public OperationMode ItemMode
        {
            get { return _ItemMode; }
            set { _ItemMode = value; }
        }

        private UserControl _Control;
        public UserControl Control
        {
            get { return _Control; }
            set { _Control = value; }
        }        
    }
}
