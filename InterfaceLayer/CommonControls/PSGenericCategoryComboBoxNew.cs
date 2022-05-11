using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using System.Collections;
using System.Reflection;
using EcoMart.InterfaceLayer;
using EcoMart.InterfaceLayer.CommonControls;

namespace PaperlessPharmaRetail.InterfaceLayer.CommonControls
{
    public partial class PSGenericCategoryComboBoxNew : UserControl
    {
        #region Declaration
        private UserControl _UserControlToShow = null;
        private DataTable _DataSource;
        private string _SelectedID;
        private Int32 _SelectedIntID;
        private bool _ShowNew;
        public event EventHandler SeletectIndexChanged;
        public event EventHandler EnterKeyPressed;
        public event EventHandler ItemAddedEdited;
        public event EventHandler UpArrowPressed;
        Form frmOpen;
        #endregion


        public PSGenericCategoryComboBoxNew()
        {
            InitializeComponent();
            ShowNew = true;
            multiColumComboBoxGeneric.GridLines = PSMultiColumComboBox.GridLineView.None;
        }

        #region Properties
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

        public UserControl UserControlToShow
        {
            get { return _UserControlToShow; }
            set { _UserControlToShow = value; }
        }

        public DataTable DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public string SelectedID
        {
            get { return _SelectedID; }
            set
            {
                _SelectedID = value;
                if (_SelectedID != null)
                    multiColumComboBoxGeneric.SetSelectedItem(_SelectedID);
                else multiColumComboBoxGeneric.SetSelectedItem(null);
            }
        }
        public Int32  SelectedIntID
        {
            get { return _SelectedIntID; }
            set
            {
                _SelectedIntID = value;
                if (_SelectedID != null)
                    multiColumComboBoxGeneric.SetSelectedItem(_SelectedID);
                else multiColumComboBoxGeneric.SetSelectedItem(null);
            }
        }
        public bool ShowNew
        {
            get
            {
                return _ShowNew;
            }
            set
            {
                _ShowNew = value;
                btnOpen.Visible = _ShowNew;
            }
        }

        public ComboBoxItem SeletedItem
        {
            get
            {
                return multiColumComboBoxGeneric.SelectedItem;
            }
        }

        public int DropDownHeight
        {
            get
            {
                return multiColumComboBoxGeneric.DropDownHeight;
            }
            set
            {
                multiColumComboBoxGeneric.DropDownHeight = value;
            }
        }

        public int DisplayColumnNo
        {
            get
            {
                return multiColumComboBoxGeneric.DisplayColumnNo;
            }
            set
            {
                multiColumComboBoxGeneric.DisplayColumnNo = value;
            }
        }

        public int ValueColumnNo
        {
            get
            {
                return multiColumComboBoxGeneric.ValueColumnNo;
            }
            set
            {
                multiColumComboBoxGeneric.ValueColumnNo = value;
            }
        }

        public string[] SourceDataString
        {
            get
            {
                return multiColumComboBoxGeneric.SourceDataString;
            }
            set
            {
                multiColumComboBoxGeneric.SourceDataString = value;
            }
        }

        public string[] ColumnWidth
        {
            get
            {
                return multiColumComboBoxGeneric.ColumnWidth;
            }
            set
            {
                multiColumComboBoxGeneric.ColumnWidth = value;
            }
        }

        public PSMultiColumComboBox.ComboStyle Style
        {
            get { return multiColumComboBoxGeneric.Style; }
            set { multiColumComboBoxGeneric.Style = value; }
        }

        #endregion

        #region Public Methods
        public void FillData(DataTable dtable)
        {
            //Set DataSource
            DataSource = dtable;
            multiColumComboBoxGeneric.DataSource = DataSource;
        }

        public void SetToolTip(ToolTip tp, string tooltipText)
        {
            tp.SetToolTip(multiColumComboBoxGeneric, tooltipText);
        }

        public void SetToolTipNewButton(ToolTip tp, string tooltipText)
        {
            tp.SetToolTip(btnOpen, tooltipText);
        }
        #endregion

        #region Helping methods      
        #endregion

        #region Events

        private void MComboBoxNew_Resize(object sender, EventArgs e)
        {
            btnOpen.Width = 35;
            multiColumComboBoxGeneric.Width = this.Width - (btnOpen.Width + 3);
            this.Height = multiColumComboBoxGeneric.Height;
            btnOpen.Height = multiColumComboBoxGeneric.Height + 1;
            multiColumComboBoxGeneric.Location = new Point(0, 0);
            btnOpen.Location = new Point(multiColumComboBoxGeneric.Width + 3, 0);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            bool contains = "Product Barcode".Split(',').Contains(((System.Windows.Forms.Control.ControlAccessibleObject)((System.Windows.Forms.Control)sender).AccessibilityObject).Parent.Parent.Name);
            if (contains)
                _SelectedID = "";

            if (string.IsNullOrEmpty(_SelectedID))
            {
                frmOpen = new Form();
                frmOpen.FormBorderStyle = FormBorderStyle.FixedSingle;
                frmOpen.ControlBox = false;
                frmOpen.Height = UserControlToShow.Height;
                frmOpen.Width = UserControlToShow.Width;
                frmOpen.StartPosition = FormStartPosition.CenterScreen;
                //   frmOpen.Icon = EcoMart.Properties.Resources.Icon;
                ((BaseControl)UserControlToShow).Mode = OperationMode.OpenAsChild;
                ((BaseControl)UserControlToShow).Visible = true;
                ((BaseControl)UserControlToShow).Add();
                ((BaseControl)UserControlToShow).ExitClicked += new EventHandler(UserControlToShow_ExitClicked);
                frmOpen.Controls.Add(UserControlToShow);
                frmOpen.KeyPreview = true;
                frmOpen.KeyDown -= new KeyEventHandler(frmOpen_KeyDown);
                frmOpen.KeyDown += new KeyEventHandler(frmOpen_KeyDown);
                if (frmOpen.Controls.Count > 0)
                    frmOpen.Controls[0].Focus();
                frmOpen.ShowDialog();

                multiColumComboBoxGeneric.Focus();
            }
        }

        private void frmOpen_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            if (UserControlToShow is BaseControl)
            {
                IsHandled = ((BaseControl)UserControlToShow).HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            e.Handled = IsHandled;
        }

        private void UserControlToShow_ExitClicked(object sender, EventArgs e)
        {
            _SelectedID = ((BaseControl)UserControlToShow).SavedID();
            if (ItemAddedEdited != null)
                ItemAddedEdited(this, new EventArgs());
            multiColumComboBoxGeneric.Focus();
            if (frmOpen != null)
            {
                frmOpen.Close();
            }
        }

        private void multiColumComboBoxGeneric_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (multiColumComboBoxGeneric.SelectedItem != null)
            {
                _SelectedID = multiColumComboBoxGeneric.SelectedItem.Value;
            }
            else
            {
                _SelectedID = "";
            }
            if (SeletectIndexChanged != null)
                SeletectIndexChanged(sender, e);
        }

        private void multiColumComboBoxGeneric_EnterKeyPressed(object sender, EventArgs e)
        {
            if (EnterKeyPressed != null)
                EnterKeyPressed(this, new EventArgs());
        }

        private void multiColumComboBoxGeneric_UpArrowPressed(object sender, EventArgs e)
        {
            if (UpArrowPressed != null)
                UpArrowPressed(this, new EventArgs());
        }

        private void PSComboBoxNew_Enter(object sender, EventArgs e)
        {
            this.multiColumComboBoxGeneric.SetBackColor(General.ControlFocusColor);
        }

        private void PSComboBoxNew_Leave(object sender, EventArgs e)
        {
            this.multiColumComboBoxGeneric.SetBackColor(Color.White);
        }
        #endregion
    }
}
