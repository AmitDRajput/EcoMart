using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using System.Collections;
using System.Reflection;

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    public partial class PSComboBoxNew : UserControl
    {
        #region Declaration
        private UserControl _UserControlToShow = null;
        private DataTable _DataSource;
        private string _SelectedID;      
        private bool _ShowNew;
        public event EventHandler SeletectIndexChanged;
        public event EventHandler EnterKeyPressed;     
        public event EventHandler ItemAddedEdited;
        public event EventHandler UpArrowPressed;
        Form frmOpen;
        #endregion

        #region Constructors
        public PSComboBoxNew()
        {
            InitializeComponent();
            ShowNew = true;
            multiColumComboBox1.GridLines = PSMultiColumComboBox.GridLineView.None;
        }
        #endregion
        
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
                    multiColumComboBox1.SetSelectedItem(_SelectedID);
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
                return multiColumComboBox1.SelectedItem;
            }
        }

        public int DropDownHeight
        {
            get
            {
                return multiColumComboBox1.DropDownHeight;
            }
            set
            {
                multiColumComboBox1.DropDownHeight = value;
            }
        }

        public int DisplayColumnNo
        {
            get
            {
                return multiColumComboBox1.DisplayColumnNo;
            }
            set
            {
                multiColumComboBox1.DisplayColumnNo = value;
            }
        }

        public int ValueColumnNo
        {
            get
            {
                return multiColumComboBox1.ValueColumnNo;
            }
            set
            {
                multiColumComboBox1.ValueColumnNo = value;
            }
        }

        public string[] SourceDataString
        {
            get
            {
                return multiColumComboBox1.SourceDataString;
            }
            set
            {
                multiColumComboBox1.SourceDataString = value;
            }
        }

        public string[] ColumnWidth
        {
            get
            {
                return multiColumComboBox1.ColumnWidth;
            }
            set
            {
                multiColumComboBox1.ColumnWidth = value;
            }
        }

        public PSMultiColumComboBox.ComboStyle Style
        {
            get { return multiColumComboBox1.Style; }
            set { multiColumComboBox1.Style = value; }
        }

        #endregion

        #region Public Methods
        public void FillData(DataTable dtable)
        {
            //Set DataSource
            DataSource = dtable;            
            multiColumComboBox1.DataSource = DataSource;            
        }

        public void SetToolTip(ToolTip tp,string tooltipText)
        {
            tp.SetToolTip(multiColumComboBox1, tooltipText);           
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
            multiColumComboBox1.Width = this.Width - (btnOpen.Width + 3);
            this.Height = multiColumComboBox1.Height;
            btnOpen.Height = multiColumComboBox1.Height + 1;
            multiColumComboBox1.Location = new Point(0, 0);
            btnOpen.Location = new Point(multiColumComboBox1.Width + 3, 0);
        }
       
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_SelectedID))
            {
                frmOpen = new Form();
                frmOpen.FormBorderStyle = FormBorderStyle.FixedSingle;
                frmOpen.ControlBox = false;
                frmOpen.Height = UserControlToShow.Height;
                frmOpen.Width = UserControlToShow.Width;
                frmOpen.StartPosition = FormStartPosition.CenterScreen;
             //   frmOpen.Icon = PharmaSYSRetailPlus.Properties.Resources.Icon;
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
            multiColumComboBox1.Focus();
            if (frmOpen != null)
            {
                frmOpen.Close();
            }
        }
       
        private void multiColumComboBox1_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (multiColumComboBox1.SelectedItem != null)
            {
                _SelectedID = multiColumComboBox1.SelectedItem.Value;                
            }
            else
            {
                _SelectedID = "";
            }
            if (SeletectIndexChanged != null)
                SeletectIndexChanged(sender, e);
        }

        private void multiColumComboBox1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (EnterKeyPressed != null)
                EnterKeyPressed(this, new EventArgs());
        }

        private void multiColumComboBox1_UpArrowPressed(object sender, EventArgs e)
        {
            if (UpArrowPressed != null)
                UpArrowPressed(this, new EventArgs());
        }   
      
        private void PSComboBoxNew_Enter(object sender, EventArgs e)
        {
            this.multiColumComboBox1.SetBackColor(General.ControlFocusColor);
        }

        private void PSComboBoxNew_Leave(object sender, EventArgs e)
        {
            this.multiColumComboBox1.SetBackColor(Color.White);
        }
        #endregion

    }
}
