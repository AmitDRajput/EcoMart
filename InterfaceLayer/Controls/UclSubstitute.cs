using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using PharmaSYSPlus.CommonLibrary;


namespace EcoMart.InterfaceLayer
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

        #region IDetail Control

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
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                retValue = uclSubstituteControl1.HandleShortcutAction(keyPressed, modifier);

                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
                return retValue;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        #endregion IDetail Control      
    }
}
