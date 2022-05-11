using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.InterfaceLayer.CommonControls;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer.Classes
{
    public class PSMessageBox
    {
        public static PSDialogResult Show(string message, string title, PSMessageBoxButtons button, PSMessageBoxIcon icon, PSMessageBoxButtons focusButton)
        {
            PSMessageBoxForm msgForm = new PSMessageBoxForm();
            try
            {

                msgForm.Show(message, title, button, icon, focusButton);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return msgForm.PSDialogResult;
        }

        public static PSDialogResult Show(string messageLine1, string messageLine2, string title, PSMessageBoxButtons button, PSMessageBoxIcon icon, PSMessageBoxButtons focusButton)
        {
            PSMessageBoxForm msgForm = new PSMessageBoxForm();
            try
            {

                msgForm.Show(messageLine1, messageLine2, title, button, icon, focusButton);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return msgForm.PSDialogResult;
        }

        public static PSDialogResult Show(string messageLine1, string messageLine2, string title, PSMessageBoxButtons button, PSMessageBoxIcon icon, PSMessageBoxButtons focusButton, double netAmount)
        {
            PSMessageBoxForm msgForm = new PSMessageBoxForm();
            try
            {
                msgForm.Show(messageLine1, messageLine2, title, button, icon, focusButton, netAmount);


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return msgForm.PSDialogResult;
        }




    }
}
