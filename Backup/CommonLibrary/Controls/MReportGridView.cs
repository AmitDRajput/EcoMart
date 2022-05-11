using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace PharmaSYSPlus.CommonLibrary
{
    public partial class MReportGridView : UserControl
    {
        #region Declarations
        private BindingSource _BindingSource;
        private DataTable _DataTable;
        //private DataGridViewRowCollection _Rows;
        public DataGridViewColumnCollection Columns;
        private DataGridViewRow _MDataGridViewRow;     
        public ArrayList ColumnStyle;
        public event EventHandler DoubleClicked;
        public event EventHandler SelectedRowChanged;
        public delegate void CellFormattingEventHandler(object sender, DataGridViewCellFormattingEventArgs e);
        public event CellFormattingEventHandler OnCellFormattingEvent;
        private ArrayList _DoubleColumnNames;
        private ArrayList _DateColumnNames;
        private ArrayList _ConvertDatetoMonth;
        private ArrayList _OptionalColumnNames;
        private bool _ApplyAlternateRowStyle;
        #endregion

        #region Constructors
        public MReportGridView()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Initialize Default Values
        private void Initialize()
        {
            _DataTable = new DataTable();
            _BindingSource = new BindingSource();
            ColumnStyle = new ArrayList();
            _MDataGridViewRow = new DataGridViewRow();
            //_Rows = new DataGridViewRowCollection(DataGridViewContent);
            DataGridViewContent.AutoGenerateColumns = false;
            Columns = DataGridViewContent.Columns;

            DataGridViewContent.ColumnHeadersVisible = true;
            DataGridViewContent.AllowUserToOrderColumns = true;
            DataGridViewContent.AllowUserToResizeColumns = true;            
            _DoubleColumnNames = new ArrayList();
            _DateColumnNames = new ArrayList();
            _ConvertDatetoMonth = new ArrayList();
            _OptionalColumnNames = new ArrayList();
            ApplyAlternateRowStyle = false;
            DataGridViewContent.Focus();
            
        }
        #endregion

        #region Properties  
      
        public DataGridView GridView
        {
            get
            {
                return DataGridViewContent;
            }
        }
        
        //Get Set the datatable to bind to the grids
        public DataTable DataSource
        {
            get
            {
                return _DataTable;
            }
            set
            {
                _DataTable = value;
            }
        } 

        public DataGridViewRow SelectedRow
        {
            get { return _MDataGridViewRow; }
        }

        public DataGridViewRowCollection Rows
        {
            get 
            {
                return DataGridViewContent.Rows;
            }
        }

        public ArrayList DoubleColumnNames
        {
            get { return _DoubleColumnNames; }
            set { _DoubleColumnNames = value; }
        }

        public ArrayList DateColumnNames
        {
            get { return _DateColumnNames; }
            set { _DateColumnNames = value; }
        }

        public ArrayList ConvertDatetoMonth
        {
            get { return _ConvertDatetoMonth; }
            set { _ConvertDatetoMonth = value; }
        }
        public ArrayList OptionalColumnNames
        {
            get { return _OptionalColumnNames; }
            set { _OptionalColumnNames = value; }
        }


        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
        {
            get { return DataGridViewContent.AutoSizeColumnsMode; }
            set { DataGridViewContent.AutoSizeColumnsMode = value; }
        }

        public bool ApplyAlternateRowStyle
        {
            get { return _ApplyAlternateRowStyle; }
            set
            {
                _ApplyAlternateRowStyle = value;
                if (_ApplyAlternateRowStyle)
                    DataGridViewContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
                else
                    DataGridViewContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            }
        }
        #endregion

        #region Public Functions
        public void Bind()
        {
            if (_DataTable != null)
            {   
                //bind the content of the datatable in the content grid
                BindGridContent();
                //fill Column Context Menu
                InitializeColumnContextMenu();
                //Get all rows
                //_Rows = DataGridViewContent.Rows;
            }
        }       

        public void InitializeColumnContextMenu()
        {
            menuColumns.Items.Clear();
            try
            {
                foreach (DataGridViewColumn column in Columns)
                {
                    if (column.Visible)
                    {
                        System.Windows.Forms.ToolStripMenuItem menuItem = new ToolStripMenuItem();
                        menuItem.Checked = true;
                        menuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                        menuItem.Name = column.Name;
                        menuItem.Text = column.HeaderText;
                        menuItem.CheckOnClick = true;
                        menuItem.Tag = column;
                        menuItem.Click += new EventHandler(menuItem_Click);
                        menuItem.Enabled = IsColumnOptional(column.Name);
                        menuColumns.Items.Add(menuItem);
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private bool IsColumnOptional(string columnName)
        {
            bool retValue = false;
            try
            {
                if (OptionalColumnNames.Contains(columnName))
                {
                    retValue = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return retValue;
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.ToolStripMenuItem)sender).Tag != null)
                {
                    System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
                    DataGridViewColumn column = (DataGridViewColumn)item.Tag;
                    column.Visible = (item.CheckState == CheckState.Checked);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }       
       

        public void SetFocus(int rowIndex, int columnIndex)
        {
            if (DataGridViewContent.Rows.Count > rowIndex && DataGridViewContent.Columns.Count > columnIndex)
            {
                DataGridViewContent.CurrentCell = this.Rows[rowIndex].Cells[columnIndex];
            }
        }

        public void SetFocus()
        {
            if (DataGridViewContent.Rows.Count > 0 && DataGridViewContent.Columns.Count > 0)
            {
                DataGridViewContent.CurrentCell = this.Rows[0].Cells[0];
            }
        }
        #endregion

        #region Private Functions       

        private void BindGridContent()
        {
            try
            {
                if (Columns.Count > 0)
                {                    
                    _BindingSource.Sort = null;                   
                    _BindingSource.DataSource = DataSource;
                    DataGridViewContent.DataSource = _BindingSource;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private object FormatCells(int columnIndex, object columnValue)
        {
            try
            {
                if (columnValue != null)
                {
                    //Format Double
                    if (DoubleColumnNames.Contains(this.DataGridViewContent.Columns[columnIndex].Name))
                    {
                        double doubleValue;
                        double.TryParse(columnValue.ToString(), out doubleValue);
                        columnValue = doubleValue.ToString("0.00");
                    }
                    //Format Date
                    if (DateColumnNames.Contains(this.DataGridViewContent.Columns[columnIndex].Name))
                    {
                        columnValue = FormatDate(columnValue.ToString());
                    }
                    // FormatDate to Month
                    if (ConvertDatetoMonth.Contains(this.DataGridViewContent.Columns[columnIndex].Name))
                    {
                        columnValue = FormatDatetoMonth(columnValue.ToString());
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return columnValue;
        }

        private string FormatDate(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            try
            {
                if (getStringDate.Length == 8)
                {
                    strYear = getStringDate.Substring(2, 2);
                    strMonth = getStringDate.Substring(4, 2);
                    strDay = getStringDate.Substring(6, 2);

                    strFormattedDate = strDay + "/" + strMonth + "/" + strYear;
                }        
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strFormattedDate;
        }

        private string FormatDatetoMonth(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strDateMonth = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            try
            {
                if (getStringDate.Length == 8)
                {
                    strYear = getStringDate.Substring(2, 2);
                    strMonth = getStringDate.Substring(4, 2);
                    strDay = getStringDate.Substring(6, 2);

                    strFormattedDate = strDay + "/" + strMonth + "/" + strYear;
                    DateTime mydate = Convert.ToDateTime(strFormattedDate);
                    strDateMonth = mydate.Month.ToString();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strDateMonth;
        }

        #endregion

        #region Grid Header Events
        
             

    
        #endregion        

        #region Control Events        
        #endregion        

        #region Grid Content Events
        private void DataGridViewContent_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _MDataGridViewRow = DataGridViewContent.Rows[e.RowIndex];
                if (_MDataGridViewRow != null)
                    RaiseSelectedRowChanged();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        #endregion

        #region Event Handler
        public void RaiseDoubleClick()
        {
            // Method to raise the event.
            if (DoubleClicked != null)
            {
                DoubleClicked(this, EventArgs.Empty);
            }
        }

        public void RaiseSelectedRowChanged()
        {
            // Method to raise the event.
            if (SelectedRowChanged != null)
            {
                SelectedRowChanged(this, EventArgs.Empty);
            }
        }

        private void DataGridViewContent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.Value = FormatCells(e.ColumnIndex, e.Value);
            if (OnCellFormattingEvent !=null)
            {
                OnCellFormattingEvent(sender, e);
            }
        }

      

        private void DataGridViewContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DoubleClicked != null)
                {
                    DoubleClicked(this, EventArgs.Empty);
                    e.Handled = true;
                }
            }
        }
        #endregion       

        private void DataGridViewContent_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                RaiseDoubleClick();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void DataGridViewContent_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    menuColumns.Show(System.Windows.Forms.Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
