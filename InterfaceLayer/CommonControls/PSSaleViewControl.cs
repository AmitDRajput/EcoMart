using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using System.Collections;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSSaleViewControl : UserControl
    {

        private BindingSource _BindingSource;
        private DataTable _DataTable;
        private DataTable _DataTableMain;

        private DataGridViewColumnCollection _ColumnsMain;
        private DataGridViewColumnCollection _ColumnsSub;

        private string _sFilter = null;

        public delegate void CellTextChanged(string cellValue);
        public event CellTextChanged OnCellTextChanged;

        public delegate void CellValueChangeCommited(int colIndex);
        public event CellValueChangeCommited OnCellValueChangeCommited;

        public delegate void DetailsFilled(DataGridViewRow selectedRow);
        public event DetailsFilled OnDetailsFilled;

        public event DataGridViewCellEventHandler OnCellEnter;
        public event DataGridViewCellEventHandler OnCellLeave;
        public event DataGridViewCellEventHandler OnCellContentClick;

        public event EventHandler OnRowAdded;
        public event EventHandler OnRowDeleted;
        public event EventHandler OnTABKeyPressed;
        public event EventHandler OnShiftTABKeyPressed;
        public event EventHandler OnEscapeKeyPressed;

        public delegate void ShowViewForm(DataGridViewRow selectedRow);
        public event ShowViewForm OnShowViewForm;

        private DataGridViewRow _DataGridViewSelectedRow;
        private int _SubGridWidth;
        private Form frmView;
        private Color CellDefaultColor = Color.White;        

        public PSSaleViewControl()
        {
            InitializeComponent();
            dgMainGrid.AutoGenerateColumns = false;
            dgSubGrid.AutoGenerateColumns = false;

            _DataTable = null;
            _BindingSource = new BindingSource();

            _ColumnsMain = new DataGridViewColumnCollection(dgMainGrid);
            _ColumnsSub = new DataGridViewColumnCollection(dgSubGrid);

            _DataGridViewSelectedRow = new DataGridViewRow();
            _SubGridWidth = 380;
        }

        public DataGridViewColumnCollection ColumnsMain
        {
            get { return dgMainGrid.Columns; }
        }

        public DataGridViewColumnCollection ColumnsSub
        {
            get { return dgSubGrid.Columns; }
        }

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

        public DataTable DataSourceMain
        {
            get
            {
                return _DataTableMain;
            }
            set
            {
                _DataTableMain = value;
            }
        }

        public DataGridViewRowCollection Rows
        {
            get { return dgMainGrid.Rows; }
        }

        public DataGridViewRow SelectedRow
        {
            get { return _DataGridViewSelectedRow; }
        }

        public string Filter
        {
            get { return _sFilter; }
            set { _sFilter = value; }
        }

        public int SubGridWidth
        {
            get { return _SubGridWidth; }
            set { _SubGridWidth = value; }
        }

        public bool IsAllowNewRow
        {
            get { return dgMainGrid.IsAllowNewRow; }

            set { dgMainGrid.IsAllowNewRow = value; }
        }

        public bool IsAllowDelete
        {
            get { return dgMainGrid.IsAllowDelete; }

            set { dgMainGrid.IsAllowDelete = value; }
        }

        public ArrayList NumericColumnNames
        {
            get { return dgMainGrid.NumericColumnNames; }
            set { dgMainGrid.NumericColumnNames = value; }
        }

        public ArrayList DoubleColumnNames
        {
            get { return dgMainGrid.DoubleColumnNames; }
            set { dgMainGrid.DoubleColumnNames = value; }
        }

        public ArrayList DateColumnNames
        {
            get { return dgMainGrid.DateColumnNames; }
            set { dgMainGrid.DateColumnNames = value; }
        }

        public int NextRowColumn
        {
            get { return dgMainGrid.NextRowColumn; }
            set { dgMainGrid.NextRowColumn = value; }
        }

        private BaseControl _ViewControl;
        public BaseControl ViewControl
        {
            get { return _ViewControl; }
            set { _ViewControl = value; }
        }

        public void ProcessViewForm(string ID, Size size, Point location)
        {
            try
            {
                if (ViewControl != null)
                {
                    frmView = new Form();
                    frmView.FormBorderStyle = FormBorderStyle.None;
                    frmView.Height = size.Height;
                    frmView.Width = size.Width;
                    frmView.StartPosition = FormStartPosition.Manual;
                    frmView.Location = new Point(location.X + 45, location.Y + 60);
                    frmView.KeyPreview = true;
                    frmView.KeyDown += new KeyEventHandler(frmView_KeyDown);
                    ViewControl.Mode = OperationMode.ReportView;
                    ((IDetailControl)ViewControl).View();
                    ViewControl.FillSearchData(ID," ");
                    ViewControl.ExitClicked -= new EventHandler(ViewControl_ExitClicked);
                    ViewControl.ExitClicked += new EventHandler(ViewControl_ExitClicked);
                    ViewControl.Visible = true;
                    ViewControl.Height = size.Height - 6;
                    ViewControl.Width = size.Width - 6;
                    ViewControl.BringToFront();
                    ViewControl.Location = new Point(3, 3);
                    Panel pnl = new Panel();
                    pnl.BackColor = Color.Orange;
                    pnl.Dock = DockStyle.Fill;
                    pnl.Controls.Add(ViewControl);
                    frmView.Controls.Add(pnl);
                    frmView.ShowDialog();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void frmView_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            try
            {
                IsHandled = ViewControl.HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            e.Handled = IsHandled;
        }

        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmView != null)
            {
                frmView.Close();
            }
        }

        public void Bind()
        {
            try
            {
                BindGridMain();
                BindGridSub();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void ReBindSubGrid()
        {
            BindGridSub();
        }

        public void ClosePopupGrid()
        {
            dgSubGrid.Visible = false;
        }

        private void BindGridMain()
        {
            try
            {
                try
                {
                    //Disable Sorting                    
                    foreach (DataGridViewColumn column in dgMainGrid.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    if (_DataTableMain != null)
                    {
                        dgMainGrid.Rows.Clear();
                        for (int index = 0; index < _DataTableMain.Rows.Count; index++)
                        {
                            int rowIndex = dgMainGrid.Rows.Add();
                            DataGridViewRow dr = dgMainGrid.Rows[rowIndex];
                            for (int colIndex = 0; colIndex < dgMainGrid.Columns.Count; colIndex++)
                            {
                                DataGridViewColumn col = dgMainGrid.Columns[colIndex];
                                if (col.DataPropertyName != null && col.DataPropertyName != "")
                                {
                                    if (DoubleColumnNames != null && DoubleColumnNames.Contains(col.Name))
                                    {
                                        dr.Cells[col.Name].Value = Convert.ToDouble(_DataTableMain.Rows[index][col.DataPropertyName]).ToString("#0.00");
                                    }
                                    else
                                    {
                                        dr.Cells[col.Name].Value = _DataTableMain.Rows[index][col.DataPropertyName].ToString();
                                    }
                                }
                            }
                        }
                    }
                    if (IsAllowNewRow)
                        dgMainGrid.Initialize();
                }
                catch (Exception ex) { Log.WriteError(ex.ToString()); }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void BindGridSub()
        {
            try
            {
                if (DataSource != null)
                {
                    dgSubGrid.DataSource = null;
                    _BindingSource.Filter = Filter;
                    _BindingSource.DataSource = DataSource;
                    dgSubGrid.DataSource = _BindingSource;
                   
                    //ss 10/08/2012 giving error in patient sale 
                //    dgSubGrid.Columns[0].Visible = false;
                    dgSubGrid.Visible = false;
                }
                dgSubGrid.Width = _SubGridWidth;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public DataTable GetGridData()
        {
            DataTable dt = new DataTable(this.Name);
            try
            {
                foreach (DataGridViewColumn col in dgMainGrid.Columns)
                {
                    dt.Columns.Add(col.DataPropertyName);
                }

                foreach (DataGridViewRow row in dgMainGrid.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    // ss 2/7/2013
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value == null)
                                dRow[cell.ColumnIndex] = "";
                            else
                                dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dt;
        }

        private void dgMainGrid_OnCellTextChanged(string cellValue)
        {
            try
            {
                if (DataSource != null)
                {
                    if (cellValue == "")
                    {
                        dgSubGrid.Visible = false;
                    }
                    else if (dgMainGrid.IsFirstColumn())
                    {
                        DoFilter(cellValue);
                        dgSubGrid.Location = GetSubGridLocation();
                        dgSubGrid.Visible = true;
                    }
                }
                if (OnCellTextChanged != null)
                    OnCellTextChanged(cellValue);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private Point GetSubGridLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = dgMainGrid.Location.X + dgMainGrid.RowHeadersWidth + dgMainGrid.Columns[1].Width;
                pt.Y = dgMainGrid.Location.Y + dgMainGrid.ColumnHeadersHeight;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        public void DoFilter(string filterValue)
        {
            try
            {
                Filter = GetFilterString(filterValue);
                _BindingSource.Filter = Filter;
                if (dgSubGrid.Rows.Count > 0)
                {
                    dgSubGrid.Rows[0].Selected = true;
                    dgSubGrid.CurrentCell = dgSubGrid.Rows[0].Cells[1];
                }
                //Set ID column invisible
                dgSubGrid.Columns[0].Visible = false;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private string GetFilterString(string filterValue)
        {
            string strFilterString = "";
            string strFilterColumn = "";
            try
            {

                strFilterColumn = dgMainGrid.Columns[1].DataPropertyName;
                if (strFilterString != "")
                    strFilterString += " AND ";
                if (DataSource.Columns[dgMainGrid.Columns[1].DataPropertyName].DataType == typeof(int))
                {
                    strFilterString += strFilterColumn + " = ";
                    strFilterString += filterValue + " ";
                }
                else if (DataSource.Columns[dgMainGrid.Columns[1].DataPropertyName].DataType == typeof(string))
                {
                    strFilterString += strFilterColumn;
                    if (General.CurrentSetting.MsetGeneralAlphabetical == "Y")
                    { strFilterString += " like '" + filterValue + "%' "; }
                    else
                    { strFilterString += " like '%" + filterValue + "%' "; }
                 
                }
            }
            catch (Exception ex) 
            { 
                Log.WriteError(ex.ToString()); 
            }
            return strFilterString;
        }

        public DataGridViewRow MainDataGridCurrentRow
        {
            get { return dgMainGrid.Rows[dgMainGrid.CurrentRow.Index]; }
        }

        public void SetFocus(int columnIndex)
        {
            try
            {
                //ss 26/6/2012
                if (dgMainGrid.Rows.Count > 0)
                    dgMainGrid.SetCurrentCell(dgMainGrid.CurrentRow.Index, columnIndex);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void SetFocus(int rowIndex, int columnIndex)
        {
            try
            {
                dgMainGrid.SetCurrentCell(rowIndex, columnIndex);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void Sort(DataGridViewColumn columnToSort, ListSortDirection direction)
        {
            dgMainGrid.Sort(columnToSort, direction);
        }

        public void ClearSelection()
        {
            dgMainGrid.EndEdit();
            dgMainGrid.ClearSelection();
        }

        private void FillDetails()
        {
            try
            {
                dgSubGrid.Visible = false;
                if (DataSource != null)
                {
                    DataGridViewRow rowToUpdate = dgMainGrid.Rows[dgMainGrid.CurrentRow.Index];
                    if (_DataGridViewSelectedRow != null)
                    {
                        for (int index = 0; index < dgSubGrid.Columns.Count; index++)
                        {
                            rowToUpdate.Cells[index].Value = _DataGridViewSelectedRow.Cells[index].Value;
                        }
                    }
                    if (OnDetailsFilled != null)
                        OnDetailsFilled(_DataGridViewSelectedRow);
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
                    if (DoubleColumnNames != null && DoubleColumnNames.Contains(this.dgMainGrid.Columns[columnIndex].Name))
                    {
                        double doubleValue;
                        double.TryParse(columnValue.ToString(), out doubleValue);
                        columnValue = doubleValue.ToString("0.00");
                    }
                    //Format Date
                    if (DateColumnNames != null && DateColumnNames.Contains(this.dgMainGrid.Columns[columnIndex].Name))
                    {
                        columnValue = General.FormatDate(columnValue.ToString());
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return columnValue;
        }

        private void dgSubGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _DataGridViewSelectedRow = dgSubGrid.Rows[e.RowIndex];
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgSubGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
             
                FillDetails();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                dgMainGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
               
                if (dgMainGrid.IsFirstColumn())
                {
                    if (dgMainGrid.CurrentRow.Cells[0].Value != null && dgMainGrid.CurrentRow.Cells[0].Value.ToString() != "")
                    {
                        DoFilter(dgMainGrid.CurrentRow.Cells[1].Value.ToString());
                    }
                    if (dgSubGrid.Rows.Count > 0)
                        FillDetails();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (OnCellValueChangeCommited != null && colIndex == 19)
                {
                    OnCellValueChangeCommited(colIndex);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        internal void CommitEdit(DataGridViewDataErrorContexts dataGridViewDataErrorContexts)
        {

        }
        private void dgMainGrid_OnArrowUpDownPressed(int keyValue)
        {
            try
            {
                if (dgSubGrid.Visible && dgSubGrid.Rows.Count > 0)
                {
                    dgSubGrid.Focus();
                    if (dgSubGrid.Rows.Count > 1)
                    {
                        dgSubGrid.Rows[0].Selected = false;
                        dgSubGrid.Rows[1].Selected = true;
                        dgSubGrid.CurrentCell = dgSubGrid.Rows[1].Cells[1];
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgSubGrid_KeyDown(object sender, KeyEventArgs e)
        {
            // if here
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgMainGrid.IsFirstColumn())
                        FillDetails();
                    dgSubGrid.Visible = false;
                  //  dgMainGrid.Focus();
                 //   SetFocus(2);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgSubGrid.Visible = false;
                    dgMainGrid.Focus();
                    dgMainGrid.CurrentCell.Value = "";
                    dgMainGrid.EndEdit();
                    dgMainGrid.BeginEdit(false);
                    e.Handled = true;
                }                
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnRowDeleted(object sender, EventArgs e)
        {
            if (dgSubGrid.Visible)
                dgSubGrid.Visible = false;
            if (OnRowDeleted != null)
                OnRowDeleted(sender, e);
        }

        private void dgMainGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {            
            if (OnCellEnter != null)
                OnCellEnter(sender, e);           
        }

        private void dgMainGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (OnCellLeave != null)
            {
                dgMainGrid.EndEdit();
                OnCellLeave(sender, e);
            }            
        }

        private void dgMainGrid_OnTABKeyPressed(object sender, EventArgs e)
        {
            if (OnTABKeyPressed != null)
                OnTABKeyPressed(sender, e);
        }

        private void dgMainGrid_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            if (OnShiftTABKeyPressed != null)
                OnShiftTABKeyPressed(sender, e);
        }

        private void dgMainGrid_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            if (dgSubGrid.Visible)
            {
                dgSubGrid.Visible = false;
                dgMainGrid.Focus();
                dgMainGrid.EndEdit();
                if (dgMainGrid.CurrentRow.Cells[0].Value == null)
                {
                    dgMainGrid.CurrentCell.Value = "";
                }
                else if (dgMainGrid.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    dgMainGrid.CurrentCell.Value = "";
                }
                dgMainGrid.BeginEdit(false);
            }
            else
            {
                if (OnEscapeKeyPressed != null)
                    OnEscapeKeyPressed(sender, e);
            }
        }

        private void dgMainGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                e.Value = FormatCells(e.ColumnIndex, e.Value);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (OnShowViewForm != null && e.RowIndex >= 0 && dgMainGrid.Rows.Count > 0)
                {
                    OnShowViewForm(dgMainGrid.Rows[e.RowIndex]);
                }               
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.V && e.Shift == true)
                {
                    if (OnShowViewForm != null && dgMainGrid.CurrentRow != null && dgMainGrid.Rows.Count > 0)
                    {
                        OnShowViewForm(dgMainGrid.CurrentRow);
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellDefaultColor = dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
            dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = General.ControlFocusColor;
        }

        private void dgMainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = CellDefaultColor;
        }

        private void dgMainGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OnCellContentClick != null)
            {
                dgMainGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                OnCellContentClick(sender, e);
            }
        }
      

        private void dgMainGrid_OnRowAdded(object sender, EventArgs e)
        {
            try
            {
                if (OnRowAdded != null)
                {
                    OnRowAdded(sender, e);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
    }
}
