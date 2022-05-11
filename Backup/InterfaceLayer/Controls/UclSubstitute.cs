using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;
using PharmaSYSPlus.CommonLibrary;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSubstitute : BaseControl
    {        

        # region Constructor
        public UclSubstitute()
        {
            try
            {
                InitializeComponent(); 
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {           
            uclSubstituteControl1.SetFocus();
        }

        public override bool Exit()
        {
            bool retValue = false;
            try
            {
            retValue =  base.Exit(); 
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;

        }

        public override bool View()
        {           
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                headerLabel1.Text = "SIMILAR PRODUCT";
                uclSubstituteControl1.Initialize();
                uclSubstituteControl1.SetFocus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        #endregion IDetail Control      
    }
}
