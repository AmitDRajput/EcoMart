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
    public partial class MDataGridView : UserControl
    {
        #region Declarations
        private BindingSource _BindingSource;
        private DataTable _DataTable;
        private DataGridViewRowCollection _Rows;
        public DataGridViewColumnCollection Columns;
        private DataGridViewRow _PSGridViewRow;

        private int DATACONTENTTOP;
        private bool _bFilter;
        private string _sFilter = null;
        public ArrayList ColumnStyle;

        public event EventHandler DoubleClicked;
        public event EventHandler SelectedRowChanged;
        public delegate void CellFormattingEventHandler(object sender, DataGridViewCellFormattingEventArgs e);
        public event CellFormattingEventHandler OnCellFormattingEvent;
        private ArrayList _DoubleColumnNames;
        private ArrayList _DateColumnNames;
        #endregion

        #region Constructors
        public MDataGridView()
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
            _PSGridViewRow = new DataGridViewRow();
            _Rows = new DataGridViewRowCollection(DataGridViewContent);
            DataGridViewContent.AutoGenerateColumns = false;
            Columns = DataGridViewHeader.Columns;
            DataGridViewHeader.AutoGenerateColumns = false;
            //DataGridViewHeader.AutoSize = true;
            
            _DoubleColumnNames = new ArrayList();
            _DateColumnNames = new ArrayList();     
                        
            DATACONTENTTOP = 20;
            DataGridViewHeader.ColumnHeadersVisible = false;            
            //DataGridViewHeader.AllowUserToAddRows = false;
            //DataGridViewHeader.AllowUserToDeleteRows = false;
            //DataGridViewHeader.AllowUserToOrderColumns = true;
            //DataGridViewHeader.AllowUserToResizeColumns = true;

            DataGridViewContent.ColumnHeadersVisible = true;
            DataGridViewContent.AllowUserToOrderColumns = true;
            DataGridViewContent.AllowUserToResizeColumns = true;

            DataGridViewDefaultSettings();
            DataGridViewContent.Focus();
        }
        #endregion

        #region Properties        
        //Get Set the filter. If true then search field in grid header will be shown else not
        public bool ShowGridFilter
        {
            get { return _bFilter; }
            set 
            { 
                _bFilter = value;
                DefaultFilterSelect();
                if (!_bFilter && _BindingSource != null)
                    RemoveFilter();
                RepositionGrids();                
                RefreshUI();               
            }
        }
        public string Filter
        {
            get { return _sFilter; }
            set { _sFilter = value;}
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
            get { return _PSGridViewRow; }
        }

        public DataGridViewRowCollection Rows
        {
            get { return _Rows; }
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
        #endregion

        #region Public Functions
        public void Bind()
        {
            if (_DataTable != null)
            {
                Filter = null;
                //bind the content of the datatable in the content grid
                BindGridContent();

                //Hide Columns
                HideColumns();

                //Get all rows
                _Rows = DataGridViewContent.Rows;

                //Reposition content grid
                RepositionGrids();

                //refreshUI when the grid is bound
                RefreshUI();
            }
        }

        public void SetFocus(int rowIndex, int columnIndex)
        {
            if (DataGridViewContent.Rows.Count > rowIndex && DataGridViewContent.Columns.Count > columnIndex)
            {
                DataGridViewContent.CurrentCell = DataGridViewContent.Rows[rowIndex].Cells[columnIndex];
            }
        }

        public void SetFocus()
        {
            DataGridViewHeader.Focus();
            if (DataGridViewHeader.Rows.Count > 0 && DataGridViewHeader.Columns.Count > 2)
            {
                DataGridViewHeader.EndEdit();
                DataGridViewHeader.CurrentCell = DataGridViewHeader.Rows[0].Cells[2];
                DataGridViewHeader.BeginEdit(false);                
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
                    // Initialize the DataGridView.                    
                    DataGridViewContent.Columns.Clear();
                    if (Columns.Count > 0)
                    {
                        for (int iCount = 0; iCount < Columns.Count; iCount++)
                        {
                            DataGridViewColumn column = new DataGridViewTextBoxColumn();
                            column.DataPropertyName = Columns[iCount].DataPropertyName;
                            column.Name = Columns[iCount].Name;
                            column.HeaderText = Columns[iCount].HeaderText;

                            if (Columns[iCount].Visible == false)
                            {
                                column.Visible = false;
                            }
                            column.Width = Columns[iCount].Width;
                            column.DefaultCellStyle = Columns[iCount].DefaultCellStyle;
                            DataGridViewContent.Columns.Add(column);
                        }
                    }
                    _BindingSource.Filter = Filter;
                    _BindingSource.Sort = null;                   
                    _BindingSource.DataSource = DataSource;
                    DataGridViewContent.DataSource = _BindingSource;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        internal void HideColumns()
        {
            for (int iCount = 0; iCount < Columns.Count; iCount++)
            {
                DataGridViewContent.Columns[iCount].Visible = DataGridViewHeader.Columns[iCount].Visible;
            }
        }

        private void DataGridViewDefaultSettings()
        {
            DataGridViewHeader.Top = 0;
            DataGridViewHeader.Left = 0;           
            DataGridViewContent.Top = DATACONTENTTOP;
            DataGridViewContent.Left = 0;
            //gpBox.Top = -6;
            //gpBox.Left = 0;
        }

        private void RefreshUI()
        {
            DataGridViewHeader.Width = this.Width;// -DataGridViewContent.RowHeadersWidth + 1;
            DataGridViewContent.Width = this.Width;
            DataGridViewContent.Height = this.Height - DataGridViewContent.Top;         
        }

        private void RemoveFilter()
        {
            try
            {
                if (DataGridViewHeader.Columns.Count > 0)
                {
                    DataGridViewHeader.EndEdit();
                    for (int iCount = 0; iCount < DataGridViewHeader.Columns.Count; iCount++)
                    {
                        DataGridViewHeader.Rows[0].Cells[iCount].Value = "";
                    }
                    Filter = null;
                    _BindingSource.Filter = Filter;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void RepositionGrids()
        {
            try
            {
                if (_bFilter)
                {
                    DataGridViewContent.Top = DATACONTENTTOP;                
                }
                else
                {
                    DataGridViewContent.Top = 0;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private bool IsNumeric(object ValueToCheck)
        {
            bool Numeric = false;
            int Dummy;        

            try
            {
                string InputValue = Convert.ToString(ValueToCheck);
                Numeric = int.TryParse(InputValue, System.Globalization.NumberStyles.Any, null, out Dummy);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return Numeric;
        }

        private void DefaultFilterSelect()
        {
            if (DataGridViewHeader.CurrentCell != null)
            {
                DataGridViewHeader.BeginEdit(true);
            }
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
        #endregion

        #region Grid Header Events
        private void DataGridViewHeader_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {               
                //When enter button is pressed perform search on that field
                if (e.KeyCode == Keys.Enter)
                {
                    DoFilter();
                    if (DataGridViewContent.Rows.Count > 0)
                        DataGridViewContent.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    DataGridViewHeader.BeginEdit(true);
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void DoFilter()
        {
            try
            {
                Filter = GetFilterString();
                // ss 12/11/2013 check for null
                if (Filter != null)
                    _BindingSource.Filter = Filter;
                else
                    _BindingSource.Filter = "";

                DataGridViewHeader.Rows[0].Cells[DataGridViewHeader.CurrentCellAddress.X].Selected = true;
                DataGridViewHeader.BeginEdit(true);
                RefreshUI();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private string GetFilterString()
        {           
            string strFilterString = "";
            string strFilterColumn = "";            
             try
            {
                // ss 12/11/2013 check for null
                if (DataGridViewHeader != null)
                {
                    for (int ColumnIndex = 0; ColumnIndex < DataGridViewHeader.Rows[0].Cells.Count; ColumnIndex++)
                    {
                        if (DataGridViewHeader.Rows[0].Cells[ColumnIndex].Visible && DataGridViewHeader.Rows[0].Cells[ColumnIndex].Value != null && DataGridViewHeader.Rows[0].Cells[ColumnIndex].Value.ToString() != "")
                        {
                            strFilterColumn = DataGridViewHeader.Columns[ColumnIndex].DataPropertyName;
                            if (strFilterString != "")
                                strFilterString += " AND ";
                            if (DataSource.Columns[strFilterColumn].DataType == typeof(int) || DataSource.Columns[strFilterColumn].DataType == typeof(Int64) || DataSource.Columns[strFilterColumn].DataType == typeof(double) || DoubleColumnNames.Contains(DataGridViewHeader.Columns[ColumnIndex].Name))
                            {
                                strFilterString += strFilterColumn + " = ";
                                strFilterString += DataGridViewHeader.Rows[0].Cells[ColumnIndex].Value.ToString() + " ";
                            }

                            else if (DataSource.Columns[strFilterColumn].DataType == typeof(string))
                            {
                                strFilterString += strFilterColumn;
                                string[] SlashedValues = DataGridViewHeader.Rows[0].Cells[ColumnIndex].Value.ToString().Split('/');
                                if (SlashedValues.Length == 3) //Date Value  
                                {
                                    if (SlashedValues[2].Length == 2)
                                    {
                                        SlashedValues[2] = "20" + SlashedValues[2];
                                    }
                                    strFilterString += " = '" + SlashedValues[2] + SlashedValues[1] + SlashedValues[0] + "' ";
                                }
                                else
                                    strFilterString += " like '" + DataGridViewHeader.Rows[0].Cells[ColumnIndex].Value.ToString() + "%' ";
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strFilterString;
        }

        private void DataGridViewHeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewHeader.BeginEdit(true);
        }
        #endregion        

        #region Control Events
        private void MDataGridView_Resize(object sender, EventArgs e)
        {
            RefreshUI();
        }
        #endregion        

        #region Grid Content Events
        private void DataGridViewContent_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _PSGridViewRow = DataGridViewContent.Rows[e.RowIndex];
                if (_PSGridViewRow != null)
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

        private void DataGridViewContent_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                DataGridViewHeader.Columns[e.Column.Index].Width = e.Column.Width;
                DataGridViewHeader.Width = DataGridViewContent.Width;                
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void DataGridViewContent_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridViewHeader.HorizontalScrollingOffset = DataGridViewContent.HorizontalScrollingOffset;                     

        }

        private void DataGridViewContent_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridViewHeader.Columns[e.Column.Index].DisplayIndex = e.Column.DisplayIndex;
        }

        private void DataGridViewContent_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                RaiseDoubleClick();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
    }
}
