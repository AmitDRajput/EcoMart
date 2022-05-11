using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer.CommonControls
{
    #region Enum

    #endregion

    [System.ComponentModel.ToolboxItem(false)]
    public partial class PSMultiColumComboBox : UserControl
    {
        #region Declaraion(s)
        PopupEditor popup;
        DataGridView dgMultiColumn;
        private DataGridViewRow _SelectedRow;
        private BindingSource _BindingSource;
        private UserControl dgMultiColumnContainer;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler EnterKeyPressed;
        public event EventHandler UpArrowKeyPressed;
        public delegate void TextValueChanged(string Text);
        public event TextValueChanged OnTextValueChanged;
        //public event EventHandler TextValueChanged;
    //    public event EventHandler TabKeyPressed;
        private bool raiseEnterKeyEvent = false;
        private bool _AlphabeticalList = false;
        private bool IsselectAllText = true;
        #endregion

        #region Enum
        public enum GridLineView
        {
            None,
            Vertical,
            Horizontal,
            Both
        }

        public enum ComboStyle
        {
            DropDownList,
            DropDown
        }
        #endregion

        #region Constructor
        public PSMultiColumComboBox()
        {
            InitializeComponent();
            _BindingSource = new BindingSource();
            InitializeMultiColumnGridView();
            InitializePopup();
            ResizeCombo();
        }
        #endregion        

        #region Properties
        private DataTable _DataSource;
        public DataTable DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                BindData();
            }
        }

        private string[] _SourceDataString;
        public string[] SourceDataString
        {
            get
            {
                return _SourceDataString;
            }
            set
            {
                _SourceDataString = value;
            }
        }

        private string[] _SourceDataHeader;
        public string[] SourceDataHeader
        {
            get
            {
                return _SourceDataHeader;
            }
            set
            {
                _SourceDataHeader = value;
            }
        }

        private string[] _ColumnWidth;
        public string[] ColumnWidth
        {
            get { return _ColumnWidth; }
            set { _ColumnWidth = value; }
        }

        private int _DisplayColumnNo = 1;
        public int DisplayColumnNo
        {
            get { return _DisplayColumnNo; }
            set { _DisplayColumnNo = value; }
        }

        private int _ValueColumnNo = 0;
        public int ValueColumnNo
        {
            get { return _ValueColumnNo; }
            set { _ValueColumnNo = value; }
        }

        private bool _ShowHeader = false;
        public bool ShowHeader
        {
            get { return _ShowHeader; }
            set { _ShowHeader = value; }
        }

        private PSMultiColumComboBox.GridLineView _GridLines = PSMultiColumComboBox.GridLineView.None;
        public PSMultiColumComboBox.GridLineView GridLines
        {
            get { return _GridLines; }
            set { _GridLines = value; }
        }

        private int _DropDownHeight = 200;
        public int DropDownHeight
        {
            get { return _DropDownHeight; }
            set { _DropDownHeight = value; }
        }

        private int _DropDownWidth = 200;

        private int _TextMaxLenght = 32767;
        public int TextMaxLenght
        {
            get { return _TextMaxLenght; }
            set { txtbox.MaxLength = value;
                _TextMaxLenght = value; }
        }

        private ComboBoxItem _SelectedItem = null;
        public ComboBoxItem SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; }
        }

        private ComboStyle _Style = ComboStyle.DropDownList;
        public ComboStyle Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        public CharacterCasing CharacterCasing
        {
            get
            {
                return txtbox.CharacterCasing;
            }
            set
            {
                txtbox.CharacterCasing = value;
            }
        }
        public override string Text
        {
            get
            {
                return txtbox.Text;
            }
            set
            {
                txtbox.Text = value;
            }
        }
        public bool ReadOnly
        {
            get
            {
                return txtbox.ReadOnly;
            }
            set
            {
                txtbox.ReadOnly = value;
            }
        }

        public bool AlphabeticalList
        {
            get
            {
                return _AlphabeticalList;
            }
            set
            {
                _AlphabeticalList = value;
            }
        }

        #endregion

        #region Private Methods
        private void InitializePopup()
        {
            popup = new PopupEditor(dgMultiColumnContainer);
            popup.AutoClose = false;
        }

        private void BindData()
        {
            if (DataSource != null)
            {
                dgMultiColumn.DataSource = null;
                ConstructDataColumns();
                CalculateDropDownWidth();
                dgMultiColumn.ColumnHeadersVisible = ShowHeader;
                switch (GridLines)
                {
                    case GridLineView.None:
                        dgMultiColumn.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        break;
                    case GridLineView.Vertical:
                        dgMultiColumn.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
                        break;
                    case GridLineView.Horizontal:
                        dgMultiColumn.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        break;
                    case GridLineView.Both:
                        dgMultiColumn.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                        break;
                }
            }
            _BindingSource.Sort = null;
            _BindingSource.DataSource = _DataSource;
            dgMultiColumn.DataSource = _BindingSource;
            if (dgMultiColumn.Rows.Count > 0)
            {
                dgMultiColumn.Rows[0].Selected = true;
                _SelectedRow = dgMultiColumn.Rows[0];
            }

        }

        private void ConstructDataColumns()
        {
            if (_SourceDataString != null)
            {
                dgMultiColumn.AutoGenerateColumns = false;
                dgMultiColumn.Columns.Clear();
                DataGridViewTextBoxColumn column;
                for (int index = 0; index < _SourceDataString.Length; index++)
                {
                    column = new DataGridViewTextBoxColumn();
                    column.Name = "Col_" + _SourceDataString[index];
                    column.DataPropertyName = _SourceDataString[index];
                    if (_SourceDataHeader != null && _SourceDataHeader.Length > index)
                        column.HeaderText = _SourceDataHeader[index];
                    else
                        column.HeaderText = _SourceDataString[index];
                    if (_ColumnWidth != null && _ColumnWidth.Length > index)
                    {
                        int colWidth;
                        int.TryParse(_ColumnWidth[index], out colWidth);
                        if (colWidth == 0)
                        {
                            column.Width = colWidth;
                            column.Visible = false;
                        }
                        else
                        {
                            column.Width = colWidth;
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Width = 100; //Default
                        column.Visible = true;
                    }
                    dgMultiColumn.Columns.Add(column);
                }
            }
            else
            {
                dgMultiColumn.AutoGenerateColumns = true;
                dgMultiColumn.Columns.Clear();
            }
        }

        private void CalculateDropDownWidth()
        {
            _DropDownWidth = 0;
            if (_ColumnWidth != null)
            {
                for (int index = 0; index < _ColumnWidth.Length; index++)
                {
                    _DropDownWidth += Convert.ToInt32(_ColumnWidth[index]);
                }
            }
        }

        private void ShowPopup()
        {
            popup.Height = DropDownHeight;
            popup.Width = _DropDownWidth + 20;
            dgMultiColumn.Size = popup.Size;
            dgMultiColumnContainer.Size = popup.Size;
            if (dgMultiColumn.Rows.Count > 0)
                popup.Show(this);
        }

        private void FilterData()
        {
            try
            {
                SetFilter(txtbox.Text);
                if (dgMultiColumn.Rows.Count > 0)
                {
                    dgMultiColumn.Rows[0].Selected = true;
                    _SelectedRow = dgMultiColumn.Rows[0];
                }
                ShowHideColumn();
                if (dgMultiColumn.Rows.Count == 0)
                    popup.Close();
            }
            catch
            {
            }
        }

        private void ShowHideColumn()
        {
            if (_ColumnWidth != null)
            {
                for (int index = 0; index < _ColumnWidth.Length; index++)
                {
                    int colWidth;
                    int.TryParse(_ColumnWidth[index], out colWidth);
                    if (dgMultiColumn.Columns.Count > index)
                    {
                        if (colWidth == 0)
                        {
                            dgMultiColumn.Columns[index].Width = colWidth;
                            dgMultiColumn.Columns[index].Visible = false;
                        }
                        else
                        {
                            dgMultiColumn.Columns[index].Width = colWidth;
                            dgMultiColumn.Columns[index].Visible = true;
                        }
                    }
                }
            }
        }

        private void SetFilter(string filterText)
        {
            string strFilterColumn;
            try
            {
                if (filterText.Trim().Length > 0 && dgMultiColumn.Columns.Count > 0)
                {
                    strFilterColumn = dgMultiColumn.Columns[_DisplayColumnNo].DataPropertyName;
                    if (General.CurrentSetting.MsetGeneralAlphabetical == "Y" || AlphabeticalList == true)
                    {
                        _BindingSource.Filter = strFilterColumn + string.Format(" LIKE '{0}%'", filterText.ToString().Trim());
                    }
                    else
                    {
                        _BindingSource.Filter = strFilterColumn + string.Format(" LIKE '%{0}%'", filterText.ToString().Trim());
                    }
                }
                else
                {
                    _BindingSource.Filter = null;
                }
            }
            catch
            {
            }
        }

        private void ResizeCombo()
        {
            this.Height = this.txtbox.Height;
            this.txtbox.Location = new Point(0, 0);
            this.buttonDropDown.Height = this.txtbox.Height + 1;
            this.buttonDropDown.Width = this.buttonDropDown.Height + 1;
            this.txtbox.Width = this.Width - this.buttonDropDown.Width;
            this.Refresh();
        }
        #endregion

        #region Public Methods 
        public void Clear()
        {
            _SelectedItem = null;
            txtbox.Text = "";
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new EventArgs());
        }

        public void SetSelectedItem(string itemValue)
        {
            string strFilterColumn;
            try
            {
                if (itemValue != null && itemValue.Trim().Length > 0 && dgMultiColumn.Columns.Count > 0)
                {
                    strFilterColumn = dgMultiColumn.Columns[_ValueColumnNo].DataPropertyName;
                    if (dgMultiColumn.Columns[_ValueColumnNo].ValueType == typeof(Int32))
                        _BindingSource.Filter = strFilterColumn + string.Format(" = {0}", itemValue);
                    else
                        _BindingSource.Filter = strFilterColumn + string.Format(" LIKE '%{0}%'", itemValue);

                    if (dgMultiColumn.Rows.Count > 0)
                    {
                        dgMultiColumn.Rows[0].Selected = true;
                        _SelectedRow = dgMultiColumn.Rows[0];
                    }
                    dgMultiColumn_ItemSeleted(_SelectedRow);
                }
                else
                {
                    _BindingSource.Filter = null;
                    dgMultiColumn_ItemSeleted(null);
                }
                ShowHideColumn();
            }
            catch
            {

            }
        }

        public void SetBackColor(Color color)
        {
            txtbox.BackColor = color;
        }

        public void HideDropFownButton()
        {
            buttonDropDown.Visible = false;
        }
        #endregion

        #region Events

        private void MultiColumComboBox_Resize(object sender, EventArgs e)
        {
            ResizeCombo();
        }

        private void buttonDropDown_Click(object sender, EventArgs e)
        {
            if (popup.Visible)
            {
                popup.Close();
            }
            else
            {
                FilterData();
                ShowPopup();
            }
        }

        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                raiseEnterKeyEvent = true;
            if (e.KeyCode == Keys.Menu || e.KeyCode == Keys.LWin)
                popup.Close();
        }

        private void txtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (popup.Visible)
                    dgMultiColumn_ItemSeleted(_SelectedRow);
                //else
                //{
                //    if (_SelectedItem != null)
                //        txtbox.Text = _SelectedItem.Text;
                //    //else
                //    //    txtbox.Text = string.Empty;
                //}
                txtbox.SelectionStart = txtbox.Text.Length;
                txtbox.Focus();
                if (raiseEnterKeyEvent)
                {
                    if (EnterKeyPressed != null)
                        EnterKeyPressed(this, new EventArgs());
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgMultiColumn_ItemSeleted(null);
                popup.Close();
                txtbox.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                //Filter Data
                popup.Focus();
                dgMultiColumnContainer.Focus();
                dgMultiColumn.Focus();
                FilterData();
                if (popup.Visible == false)
                {
                    ShowPopup();
                }
                else
                {
                    if (dgMultiColumn.Rows.Count > 1)
                    {
                        dgMultiColumn.Rows[0].Selected = false;
                        dgMultiColumn.Rows[1].Selected = true;
                        //Set Current Cell
                        for (int index = 0; index < dgMultiColumn.Rows[1].Cells.Count; index++)
                        {
                            if (dgMultiColumn.Rows[1].Cells[index].Visible)
                            {
                                dgMultiColumn.CurrentCell = dgMultiColumn.Rows[1].Cells[index];
                                break;
                            }
                        }

                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (popup.Visible)
                {
                    popup.Close();
                    txtbox.Focus();
                }
                else
                {
                    if (UpArrowKeyPressed != null)
                        UpArrowKeyPressed(sender, e);
                }
            }
            else if (txtbox.Text == "")
            {
                popup.Close();
            }
            else
            {
                //Filter data
                FilterData();
                if (popup.Visible == false)
                {
                    ShowPopup();
                }
                txtbox.Focus();
            }
            raiseEnterKeyEvent = false;
        }

        private void txtbox_Leave(object sender, EventArgs e)
        {
            popup.Close();
            if (txtbox.Text.Trim().Length == 0)
            {
                Clear();
            }
        }

        private void txtbox_FontChanged(object sender, EventArgs e)
        {
            ResizeCombo();
        }
        #endregion

        #region DataGridView Methods
        private void InitializeMultiColumnGridView()
        {
            this.dgMultiColumn = new DataGridView();
            this.dgMultiColumn.AllowUserToAddRows = false;
            this.dgMultiColumn.AllowUserToDeleteRows = false;
            this.dgMultiColumn.AllowUserToResizeColumns = false;
            this.dgMultiColumn.AllowUserToResizeRows = false;
            this.dgMultiColumn.AutoGenerateColumns = false;
            this.dgMultiColumn.BackgroundColor = System.Drawing.Color.White;
            this.dgMultiColumn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiColumn.Location = new System.Drawing.Point(0, 0);
            this.dgMultiColumn.MultiSelect = false;
            this.dgMultiColumn.Name = "dgMultiColumn";
            this.dgMultiColumn.ReadOnly = true;
            this.dgMultiColumn.RowHeadersVisible = false;
            this.dgMultiColumn.RowHeadersWidth = 25;
            this.dgMultiColumn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMultiColumn.Size = new System.Drawing.Size(_DropDownWidth, _DropDownHeight);
            this.dgMultiColumn.TabIndex = 0;
            this.dgMultiColumn.DefaultCellStyle.Font = txtbox.Font;
            this.dgMultiColumn.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiColumn_RowEnter);
            this.dgMultiColumn.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiColumn_CellDoubleClick);
            this.dgMultiColumn.KeyDown += new KeyEventHandler(dgMultiColumn_KeyDown);

            dgMultiColumnContainer = new UserControl();
            dgMultiColumnContainer.Name = "GridContainer";
            dgMultiColumnContainer.Size = this.dgMultiColumn.Size;
            dgMultiColumnContainer.Controls.Add(dgMultiColumn);
            txtbox.GotFocus += OnFocus;
        }
        private void OnFocus(object sender, EventArgs e)
        {
            if (IsselectAllText)
                txtbox.SelectAll();
        }

        private void dgMultiColumn_ItemSeleted(DataGridViewRow item)
        {
            try
            {
                ComboBoxItem selItem = new ComboBoxItem();
                if (item != null && item.Cells.Count > _DisplayColumnNo && item.Cells.Count > _ValueColumnNo && item.Cells[_DisplayColumnNo].Value != null)
                {
                    selItem.Text = item.Cells[_DisplayColumnNo].Value.ToString();
                    selItem.Value = item.Cells[_ValueColumnNo].Value.ToString();
                    for (int index = 0; index < item.Cells.Count; index++)
                    {
                        // ss 09/09/2012 added if 
                        if (item.Cells[index].Value != null)
                            selItem.ItemData.Add(item.Cells[index].Value.ToString());
                    }

                }
                else
                {
                    selItem = null;
                }
                _SelectedItem = selItem;
                if (_SelectedItem == null)
                {
                    if (Style == ComboStyle.DropDownList)
                        txtbox.Text = "";
                }
                else
                {
                    txtbox.Text = _SelectedItem.Text;
                }
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, new EventArgs());
                popup.Close();
            }
            catch (Exception Ex)
            { Log.WriteException(Ex); }
        }

        #endregion

        #region DataGridView Events
        private void dgMultiColumn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgMultiColumn.SelectedRows.Count > 0)
                dgMultiColumn_ItemSeleted(dgMultiColumn.SelectedRows[0]);
        }

        private void dgMultiColumn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgMultiColumn.SelectedRows.Count > 0)
                    {
                        _SelectedRow = dgMultiColumn.SelectedRows[0];
                        dgMultiColumn_ItemSeleted(_SelectedRow);
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    _SelectedRow = null;
                    dgMultiColumn_ItemSeleted(_SelectedRow);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Back)
                {
                    IsselectAllText = false;
                    txtbox.Focus();
                    popup.Close();
                    IsselectAllText = true;
                    e.Handled = true;
                }
            }
            catch (Exception Ex)
            { Log.WriteException(Ex); }

        }

        private void dgMultiColumn_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _SelectedRow = dgMultiColumn.Rows[e.RowIndex];
        }

        private void MultiColumComboBox_Leave(object sender, EventArgs e)
        {
            if (popup != null && popup.Visible)
            {
                popup.Close();
            }
        }

        #endregion

        private void txtbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbox.Text) == true)
            {
                _SelectedItem = null;
            }
            if (OnTextValueChanged != null)
            {
                OnTextValueChanged(txtbox.Text);
                //General.ComboBoxText = txtbox.Text;
            }
        }

        private void txtbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // try
            //{
            //    if (e.KeyCode == Keys.Tab && TabKeyPressed != null)
            //    {
            //        TabKeyPressed(this, e);
            //    }
            //}
            //catch (Exception Ex)
            //{ Log.WriteException(Ex); }
        }
    }

    [ToolboxItem(false)]
    public class PopupEditor : ToolStripDropDown
    {
        #region Declaration(s)
        private ToolStripControlHost _host;
        #endregion

        #region Constructor
        public PopupEditor(Control content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            Content = content;
            AutoSize = false;
            DoubleBuffered = true;
            ResizeRedraw = true;
            _host = new ToolStripControlHost(content);
            Padding = Margin = _host.Padding = _host.Margin = Padding.Empty;
            content.Margin = Padding.Empty;
            MinimumSize = content.MinimumSize;
            content.MinimumSize = content.Size;
            MaximumSize = content.MaximumSize;
            Size = content.Size;
            _host.Size = content.Size;
            TabStop = content.TabStop = true;
            content.Location = Point.Empty;
            Items.Add(_host);
            content.Disposed += (sender, e) =>
            {
                content = null;
                Dispose(true);
            };
            content.RegionChanged += (sender, e) => UpdateRegion();
            UpdateRegion();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (((keyData & Keys.Alt) == Keys.Alt) || keyData == Keys.LWin)
            {
                //if ((keyData & Keys.F4) != Keys.F4)
                //{
                //    return false;
                //}
                //else
                //{
                Close();
                //}
            }
            bool processed = base.ProcessDialogKey(keyData);
            if (!processed && (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift)))
            {
                bool backward = (keyData & Keys.Shift) == Keys.Shift;
                Content.SelectNextControl(null, !backward, true, true, true);
            }
            return processed;
        }
        #endregion

        #region Properties
        public Control Content { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the pop-up region.
        /// </summary>
        private void UpdateRegion()
        {
            if (Region != null)
            {
                Region.Dispose();
                Region = null;
            }
            if (Content.Region != null)
            {
                Region = Content.Region.Clone();
            }
        }

        private void Show(Control control, Rectangle area)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                location.Y -= Size.Height + area.Height;
            }
            location = control.PointToClient(location);
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        private void Show(Rectangle area)
        {
            Point location = new Point(area.Left, area.Top + area.Height);
            Rectangle screen = Screen.FromControl(this).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                location.Y -= Size.Height + area.Height;
            }
            Show(location, ToolStripDropDownDirection.BelowRight);
        }
        #endregion

        #region Public Methods
        public void Show(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            Show(control, control.ClientRectangle);
        }
        #endregion
    }

    public class ComboBoxItem
    {
        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private List<string> _ItemData;

        public List<string> ItemData
        {
            get { return _ItemData; }
            set { _ItemData = value; }
        }

        public ComboBoxItem()
        {
            _Text = "";
            _Value = "";
            _ItemData = new List<string>();
        }
    }   
    
}
