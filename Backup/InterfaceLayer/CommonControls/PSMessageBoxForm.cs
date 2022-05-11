using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.Properties;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class PSMessageBoxForm : Form
    {
        private PSDialogResult dialogResult = PSDialogResult.None;
        private PSMessageBoxButtons buttonToFocus = PSMessageBoxButtons.OK;
        private const int BUTTONSPACE = 6;
        public PSMessageBoxForm()
        {
            InitializeComponent();
        }

        public PSDialogResult PSDialogResult
        {
            get { return dialogResult; }
        }

        public void Show(string message, string title, PSMessageBoxButtons button, PSMessageBoxIcon icon, PSMessageBoxButtons focusButton)
        {
            try
            {
                lblMessageLine2.Visible = false;
                Text = title;
                lblMessageLine1.Text = message;
                buttonToFocus = focusButton;
                ShowButtons(button);
                ShowMsgIcon(icon);
                ShowDialog();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void Show(string messageLine1, string messageLine2, string title, PSMessageBoxButtons button, PSMessageBoxIcon icon, PSMessageBoxButtons focusButton)
        {
            try
            {
                lblMessageLine2.Visible = true;
                Text = title;
                lblMessageLine1.Text = messageLine1;
                lblMessageLine2.Text = messageLine2;
                buttonToFocus = focusButton;
                ShowButtons(button);
                ShowMsgIcon(icon);
                ShowDialog();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }        

        private void ShowButtons(PSMessageBoxButtons button)
        {
            btnOk.Visible = true;
            btnPrint.Visible = false;
            try
            {
                switch (button)
                {
                    case PSMessageBoxButtons.OK:
                        break;
                    case PSMessageBoxButtons.OKPrint:
                        btnPrint.Visible = true;
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SetButtonLocation()
        {
            int buttonCount = 0;
            try
            {
                if (btnOk.Visible)
                    buttonCount++;
                if (btnPrint.Visible)
                    buttonCount++;

                int totalButtonSpace = BUTTONSPACE * (buttonCount - 1);

                int totalButtonWidth = btnOk.Width * buttonCount;
                totalButtonWidth = totalButtonWidth + totalButtonSpace;

                int pnlCenter = PnlButton.Width / 2;
                int buttonCenter = totalButtonWidth / 2;

                int startLocation = pnlCenter - buttonCenter;
                if (btnPrint.Visible)
                {
                    Point pt = new Point(startLocation, btnPrint.Location.Y);
                    btnPrint.Location = pt;
                    startLocation = startLocation + btnPrint.Width + BUTTONSPACE;
                }
                if (btnOk.Visible)
                {
                    Point pt = new Point(startLocation, btnOk.Location.Y);
                    btnOk.Location = pt;
                    startLocation = startLocation + btnOk.Width + BUTTONSPACE;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SetButtonFocus()
        {
            try
            {
                switch (buttonToFocus)
                {
                    case PSMessageBoxButtons.OK:
                        this.ActiveControl = btnOk;
                        btnOk.Focus();
                        break;
                    case PSMessageBoxButtons.Print:
                        this.ActiveControl = btnPrint;
                        btnPrint.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SetMessageLine2Location()
        {
            try
            {
                int pnlCenter = PnlMessage.Width / 2;
                int lineCenter = lblMessageLine2.Width / 2;
                int startLocation = pnlCenter - lineCenter;

                Point pt = new Point(startLocation, lblMessageLine2.Location.Y);
                lblMessageLine2.Location = pt;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ShowMsgIcon(PSMessageBoxIcon icon)
        {
            try
            {
                switch (icon)
                {
                    case PSMessageBoxIcon.Information:
                     //   picIcon.Image = Resources.msg_information;
                        break;
                    case PSMessageBoxIcon.Warning:
                   //     picIcon.Image = Resources.msg_warning;
                        break;
                    case PSMessageBoxIcon.Error:
                    //    picIcon.Image = Resources.msg_error;
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dialogResult = PSDialogResult.Print;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dialogResult = PSDialogResult.OK;
            this.Close();
        }

        private void PSMessageBoxForm_Load(object sender, EventArgs e)
        {
            try
            {
                SetMessageLine2Location();
                SetButtonLocation();
                SetButtonFocus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        
    }
}
