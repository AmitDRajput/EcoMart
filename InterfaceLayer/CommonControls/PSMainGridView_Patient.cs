using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using EcoMart.Common;
using System.Collections;

namespace EcoMart.InterfaceLayer.CommonControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public class PSMainGridView_Patient : DataGridView
    {
        public event EventHandler OnEnterKeyPressed;
        public event EventHandler OnEnterKeyPressed_Processed;

        public delegate void CellTextChanged(string cellValue);
        public event CellTextChanged  OnCellTextChanged;

        public delegate void CellValueChangeCommited(int colIndex);
        public event CellValueChangeCommited OnCellValueChangeCommited;

        public delegate void ArrowUpDownPressed(int keyValue);
        public event ArrowUpDownPressed OnArrowUpDownPressed;

        public delegate void ArrowLeftRightPressed(int keyValue);
        public event ArrowLeftRightPressed OnArrowLeftRightPressed; 

        public event EventHandler OnRowAdded;
        public event EventHandler OnRowDeleted;
        public event EventHandler OnTABKeyPressed;
        public event EventHandler OnShiftTABKeyPressed;
        public event EventHandler OnEscapeKeyPressed;       

        private bool _IsAllowNewRow = true;
        private bool _IsAllowDelete = true;

        private ArrayList _NumericColumnNames;
        private ArrayList _DoubleColumnNames;
        private ArrayList _DateColumnNames;
        private int _NextRowColumn;

        public PSMainGridView_Patient() : base()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = true;
            this.AllowUserToResizeColumns = true;
            this.AllowUserToOrderColumns = true;
            this.MultiSelect = false;
            this.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.ScrollBars = ScrollBars.Both;
            _NumericColumnNames = new ArrayList();
            _DoubleColumnNames = new ArrayList();
            _DateColumnNames = new ArrayList();
            _NextRowColumn = 0;
            this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(HandleEditingControlShowing);
        }

        private void HandleEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl ctl;
            try
            {               
                ctl = (DataGridViewTextBoxEditingControl)e.Control;
                if (ctl != null)
                {
                    ctl.KeyDown -= ctl_KeyDown;
                    ctl.KeyDown += new KeyEventHandler(ctl_KeyDown);                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ctl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Check for Integer & Doubles
                if (IsValidCellValue(e.KeyValue) == false)
                    e.SuppressKeyPress = true;
                else
                {
                    DataGridViewTextBoxEditingControl ctl = (DataGridViewTextBoxEditingControl)sender;
                    ctl.KeyUp -= ctl_KeyUp;
                    ctl.KeyUp += new KeyEventHandler(ctl_KeyUp);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ctl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string strModifier = e.Modifiers.ToString();
                string keyCode = e.KeyCode.ToString();
                DataGridViewTextBoxEditingControl ctl = (DataGridViewTextBoxEditingControl)sender;
                if (OnCellTextChanged != null)
                {
                    string cellValue = ((DataGridViewTextBoxEditingControl)sender).Text;
                    //Raise Event                   
                    OnCellTextChanged(cellValue);
                }
                ctl.KeyUp -= ctl_KeyUp;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public ArrayList NumericColumnNames
        {
            get { return _NumericColumnNames; }
            set { _NumericColumnNames = value; }
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

        public bool IsAllowNewRow
        {
            get { return _IsAllowNewRow; }
            set { _IsAllowNewRow = value; }
        }

        public bool IsAllowDelete
        {
            get { return _IsAllowDelete; }
            set { _IsAllowDelete = value; }
        }

        public int NextRowColumn
        {
            get { return _NextRowColumn; }
            set { _NextRowColumn = value; }
        }

        public void Initialize()
        {
            try
            {
                if (this.Columns.Count > 0 && this.Rows.Count == 0)
                {
                    int newRowId = this.Rows.Add();
                    this.Rows[newRowId].Selected = true;
                    SetCurrentCell(newRowId, 0);
                }
                this.ClearSelection();//Don't show highlited
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void SetCurrentCell(int rowID, int cellID)
        {
            try
            {
                if (this.Rows.Count > 0 )
                {
                    this.EndEdit();
                  for (int colIndex = cellID; colIndex < this.Columns.Count; colIndex++)
                  {
                      if (this.Rows[rowID].Cells[colIndex].Visible && this.Rows[rowID].Cells[colIndex].ReadOnly == false)
                      {
                          this.EndEdit();
                          this.CurrentCell = this.Rows[rowID].Cells[colIndex];
                          this.BeginEdit(true);
                          break;
                      }
                  }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           bool retValue = false;
            try
            {
                var key = new KeyEventArgs(keyData);
                if (key.KeyCode == Keys.Enter)
                {
                    this.EndEdit();
                    if (this.CurrentCell.Value == null)
                    {
                        this.CurrentCell.Value = "";
                    }                 
                    //Format integer values
                    if (NumericColumnNames != null &&  NumericColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
                    {
                        int intValue;
                        int.TryParse(this.CurrentCell.Value.ToString(), out intValue);
                        this.CurrentCell.Value = intValue.ToString("0");
                    }
                    else if (DoubleColumnNames != null && DoubleColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
                    {
                        double doubleValue;
                        double.TryParse(this.CurrentCell.Value.ToString(), out doubleValue);
                        this.CurrentCell.Value = doubleValue.ToString("0.00");
                    }
                    else if (DateColumnNames != null &&  DateColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
                    {
                       this.CurrentCell.Value = General.FormatDate(this.CurrentCell.Value.ToString());
                    }
                    else
                    {
                        this.CurrentCell.Value = this.CurrentCell.Value.ToString().ToUpper();
                    }
                    if (this.CurrentCell.Value != null && this.CurrentCell.Value.ToString() != "")
                    {
                        if (OnCellValueChangeCommited != null)
                        {
                            OnCellValueChangeCommited(this.CurrentCell.ColumnIndex);
                        }
                        if (IsLastRowAndColumn() && IsAllowNewRow == true)
                        {
                            //Check data in first column
                            if (this.Rows[this.CurrentCell.RowIndex].Cells[0].Value != null && this.Rows[this.CurrentCell.RowIndex].Cells[0].Value.ToString() != "")
                            {
                                int newRowId = this.Rows.Add();
                                if (OnRowAdded != null)
                                    OnRowAdded(newRowId, new EventArgs());
                                SetCurrentCell(newRowId, 0);
                            }
                        }
                            //ss for cashBank 28/6/2012 for cash/bank entries no now row added and no blank row.
                        else if (IsLastColumn() && this.Rows.Count > this.CurrentCell.RowIndex + 1 )
                        {
                            SetCurrentCell(this.CurrentCell.RowIndex + 1, 0);
                        }
                        else
                        {
                            if (OnEnterKeyPressed != null)
                                OnEnterKeyPressed(this.CurrentCell, key);
                            SetCurrentCell(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex + 1);
                            if (OnEnterKeyPressed_Processed != null)
                                OnEnterKeyPressed_Processed(this.CurrentCell, key);
                        }                       
                        retValue = true;
                    }
                }
                else if (key.KeyCode == Keys.Delete && IsAllowDelete == true)
                {
                    int rowIndex = this.CurrentRow.Index;
                    this.Rows.Remove(this.CurrentRow);
                    if (rowIndex == this.Rows.Count)
                    {
                        int newRowId = this.Rows.Add();
                        if (OnRowAdded != null)
                            OnRowAdded(newRowId, new EventArgs());
                        SetCurrentCell(newRowId, 0);
                    }
                    if (OnRowDeleted != null)
                        OnRowDeleted(this, new EventArgs());
                }
                else if (key.KeyValue == 38 || key.KeyValue == 40)
                {
                    if (OnArrowUpDownPressed != null)
                    {
                        OnArrowUpDownPressed(key.KeyValue);
                    }
                }
                else if (key.KeyValue == 37 || key.KeyValue == 39)  //kiran
                {
                    if (OnArrowLeftRightPressed != null)
                    {
                        OnArrowLeftRightPressed(key.KeyValue);
                        retValue = true;
                    }
                }
                else if (key.KeyCode == Keys.Tab && key.Shift == true)
                {
                    if (OnShiftTABKeyPressed != null)
                    {
                        OnShiftTABKeyPressed(this, new EventArgs());
                        retValue = true;
                    }
                }
                else if (key.KeyCode == Keys.Tab)
                {
                    if (OnTABKeyPressed != null)
                    {
                        OnTABKeyPressed(this, new EventArgs());
                        retValue = true;
                    }
                }
                else if (key.KeyCode == Keys.Escape)
                {
                    if (OnEscapeKeyPressed != null)
                    {                       
                        OnEscapeKeyPressed(this, new EventArgs());                      
                        retValue = true;
                    }
                    if (Convert.ToInt32(this.Rows[this.CurrentCell.RowIndex].Cells[11].Value) <= 0) // [ansuman]
                    {
                        this.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private bool IsValidCellValue(int Val)
        {
            bool retValue = true;
            try
            {
                if (NumericColumnNames != null && NumericColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
                {
                    if ((Val >= 48 && Val <= 57) || (Val >= 96 && Val <= 105)
                         || (Val == 8) || (Val == 46) || (Val == 110) || (Val == 37) || (Val == 39)) // 46=Del 8=Backspace 37=LeftArrow 39=RightArrow
                        retValue = true;
                    else
                        retValue = false;
                }
                else if (DoubleColumnNames != null && DoubleColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
                {
                    if ((Val >= 48 && Val <= 57) || (Val >= 96 && Val <= 105)
                   || (Val == 8) || (Val == 46) || (Val == 110) || (Val == 37) || (Val == 39)) // 46=Del 8=Backspace 37=LeftArrow 39=RightArrow
                        retValue = true;
                    else if (Val == 190 && this.CurrentCell.Value.ToString().IndexOf(".") == -1) //190=.
                        retValue = true;
                    else
                        retValue = false;
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }

        public bool IsLastColumn()
        {
            if (NextRowColumn == 0)
                return (this.CurrentCell.ColumnIndex + 1 == this.Columns.Count);
            else
                return (this.CurrentCell.ColumnIndex + 1 == NextRowColumn + 1);
        }

        public bool IsFirstColumn()
        {
            return (this.CurrentCell.ColumnIndex == 1);
        }

        private bool IsLastRow()
        {
            return (this.CurrentRow.Index + 1 == this.Rows.Count);
        }

        private bool IsLastRowAndColumn()
        {
            return (IsLastRow() && IsLastColumn());
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PSMainGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultCellStyle = dataGridViewCellStyle2;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
