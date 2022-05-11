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

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSAutoSuggestTextBox : UserControl
    {
        #region Declaration
        private UserControl _UserControlToShow = null;
        private DataTable _DataSource;
        private string _SelectedID;

        public event EventHandler SeletectIndexChanged;
        public event EventHandler EnterKeyPressed;
        public event EventHandler UpArrowKeyPressed;
        #endregion

        #region Constructors
        public PSAutoSuggestTextBox()
        {
            InitializeComponent();
            multiColumComboBox1.GridLines = PSMultiColumComboBox.GridLineView.None;
            multiColumComboBox1.HideDropFownButton();
            FontFamily FF = new FontFamily("Verdana");
            FontStyle FS = new FontStyle();
            FS = FontStyle.Regular;
            this.Font = new System.Drawing.Font(FF, 9, FS);
        }
        #endregion

        #region Properties
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
        public bool AlphabeticalList
        {
            get { return multiColumComboBox1.AlphabeticalList; }
            set { multiColumComboBox1.AlphabeticalList = value; }
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

        public int TextMaxLenght
        {
            get
            {
                return multiColumComboBox1.TextMaxLenght;
            }
            set
            {
                multiColumComboBox1.TextMaxLenght = value;
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

        public CharacterCasing CharacterCasing
        {
            get
            {
                return multiColumComboBox1.CharacterCasing;
            }
            set
            {
                multiColumComboBox1.CharacterCasing = value;
            }
        }


        public void SetFocus()
        {
            multiColumComboBox1.Focus();
            multiColumComboBox1.BackColor = Color.Beige;
        }

        public override string Text
        {
            get
            {
                return multiColumComboBox1.Text;
            }
            set
            {
                multiColumComboBox1.Text = value;
            }
        }
        public bool ReadOnly
        {
            get
            {
                return multiColumComboBox1.ReadOnly;
            }
            set
            {
                multiColumComboBox1.ReadOnly = value;
            }
        }
        #endregion

        #region Public Methods
        public void FillData(DataTable dtable)
        {
            //Set DataSource
            DataSource = dtable;
            multiColumComboBox1.DataSource = DataSource;
        }

        public void SetToolTip(ToolTip tp, string tooltipText)
        {
            tp.SetToolTip(multiColumComboBox1, tooltipText);
        }


        #endregion

        #region Helping methods    
        public void SetBackColor(Color color)
        {
            multiColumComboBox1.SetBackColor(color);
        }
        #endregion

        #region Events

        private void MComboBoxNew_Resize(object sender, EventArgs e)
        {
            multiColumComboBox1.Width = this.Width + 22;
            this.Height = multiColumComboBox1.Height;
            multiColumComboBox1.Location = new Point(0, 0);
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

        private void multiColumComboBox1_Enter(object sender, EventArgs e)
        {
            SetBackColor(General.ControlFocusColor);
        }

        private void multiColumComboBox1_Leave(object sender, EventArgs e)
        {
            SetBackColor(Color.White);
        }
        #endregion

        private void multiColumComboBox1_UpArrowKeyPressed(object sender, EventArgs e)
        {
            if (UpArrowKeyPressed != null)
                UpArrowKeyPressed(sender, e);
        }
    }
}
