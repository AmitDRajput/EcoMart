using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer.CommonControls
{
    [System.ComponentModel.ToolboxItem(false)]
      
    internal class PSProductMainGridViewCRDB : DataGridView
    {
        public delegate bool EnterKeyPressed(object sender, EventArgs e);
        public event EnterKeyPressed OnEnterKeyPressed;        

        public event EventHandler OnEnterKeyPressed_Processed;

        public delegate void CellTextChanged(string cellValue, int colIndex);
        public event CellTextChanged OnCellTextChanged;

        public delegate void CellValueChangeCommited(int colIndex);
        public event CellValueChangeCommited OnCellValueChangeCommited;

        public delegate void ArrowUpDownPressed(int keyValue);
        public event ArrowUpDownPressed OnArrowUpDownPressed;
       
        public event EventHandler OnRowAdded;
        public event EventHandler OnRowDeleted;
        public event EventHandler OnTABKeyPressed;
        public event EventHandler OnShiftTABKeyPressed;
        public event EventHandler OnEscapeKeyPressed;

        public delegate bool CanRowDeleted(object sender, EventArgs e);
        public event CanRowDeleted OnCanRowDeleted;  

        private string _NewRowColumnName;        
        private bool _IsAllowNewRow = false;
        private bool _IsAllowDelete = true;
        private ArrayList _NumericColumnNames;
        private ArrayList _DoubleColumnNames;
        private int _NextRowColumn;

        public int _CurrentQuantity;

        public PSProductMainGridViewCRDB()
            : base()
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
                   OnCellTextChanged(cellValue, this.CurrentCell.ColumnIndex);
                }
                ctl.KeyUp -= ctl_KeyUp;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public string NewRowColumnName
        {
            get { return _NewRowColumnName; }
            set { _NewRowColumnName = value; }
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

        public int NextRowColumn
        {
            get { return _NextRowColumn; }
            set { _NextRowColumn = value; }
        }
      
        public void Initialize()
        {            
            if (this.Columns.Count > 0 && this.Rows.Count == 0)
            {
                int newRowId = this.Rows.Add();
                this.Rows[newRowId].Selected = true;
                SetCurrentCell(newRowId, 0);
            }
            this.ClearSelection();//Don't show highlited
        }
        // ss 27/08/2013
        public int CurrentQuantity
        {
            get { return _CurrentQuantity; }
            set { _CurrentQuantity = value; }
        }
        // ss 27/08/2013
        public void SetCurrentCell(int rowID, int cellID)
        {
            try
            {                
                    for (int colIndex = cellID; colIndex < this.Columns.Count; colIndex++)
                    {
                        if (this.Rows[rowID] != null)
                        {
                            if (this.Rows[rowID].Cells[colIndex].Visible && this.Rows[rowID].Cells[colIndex].ReadOnly == false)
                            {
                                this.EndEdit();
                                this.CurrentCell = this.Rows[rowID].Cells[colIndex];                                
                                this.BeginEdit(true);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
               
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }

        public void SetCurrentCellForcefully(int rowID, int cellID)
        {
            for (int colIndex = cellID; colIndex < this.Columns.Count; colIndex++)
            {
                if (this.Rows[rowID].Cells[colIndex].Visible)
                {
                    this.EndEdit();
                    this.CurrentCell = this.Rows[rowID].Cells[colIndex];
                    this.BeginEdit(false);
                    break;
                }
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
                    if (NumericColumnNames != null && NumericColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
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
                        if (IsLastRowAndNewRowColumn() && IsAllowNewRow == true)
                        { 
                            int newRowId = this.Rows.Add();
                            if (OnRowAdded != null)
                                OnRowAdded(newRowId, new EventArgs());
                            SetCurrentCell(newRowId, 0);                            
                        }
                        else if (IsNewRowColumn() && IsAllowNewRow == true) // ss 09/08/2013 && IsCurrentCellInEditMode == true
                        {
                          //  if (IsCurrentCellInEditMode == true)
                                SetCurrentCell(this.CurrentCell.RowIndex + 1, 0);
                          //  else
                             //   SetCurrentCell(this.CurrentCell.RowIndex, 0);
                        }
                        else
                        {
                            bool success = true;
                            int CurrentQuantity = 0;
                            int currentrowIndex = this.CurrentCell.RowIndex;
                            if (this.CurrentRow.Cells["Col_Quantity"].Value != null && this.CurrentRow.Cells["Col_Quantity"].Value.ToString() != "")
                                CurrentQuantity = Convert.ToInt32(this.CurrentRow.Cells["Col_Quantity"].Value.ToString());
                            if (OnEnterKeyPressed != null)
                                success = OnEnterKeyPressed(this.CurrentCell, key);
                            if (success)
                            {
                                int columnindex = this.CurrentCell.ColumnIndex;
                                if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y" && this.CurrentRow.Cells[this.CurrentCell.ColumnIndex].Value == null)
                                    columnindex = columnindex - 1;
                                SetCurrentCell(this.CurrentCell.RowIndex, columnindex + 1);
                                if (OnEnterKeyPressed_Processed != null)
                                    OnEnterKeyPressed_Processed(this.CurrentCell, key);
                            }
                        }
                        retValue = true;
                    }
                
                }
                else if (key.KeyCode == Keys.Delete && IsAllowDelete == true)
                {
                    bool canrowdelete = true;
                    if (OnCanRowDeleted != null)
                    {
                        canrowdelete = OnCanRowDeleted(this.CurrentCell, key);
                    }
                    if (canrowdelete)
                    {
                        int rowIndex = this.CurrentRow.Index;
                        DataGridViewRow row = this.Rows[rowIndex];
                        if (OnRowDeleted != null)
                            OnRowDeleted(row, new EventArgs());
                        this.Rows.Remove(this.CurrentRow);
                        if (rowIndex == this.Rows.Count) //If Deleted last row
                        {
                            int newRowId = this.Rows.Add();
                            if (OnRowAdded != null)
                                OnRowAdded(newRowId, new EventArgs());
                            SetCurrentCell(newRowId, 0);
                        }
                    }
                }
                else if (key.KeyValue == 38 || key.KeyValue == 40)
                {
                    if (OnArrowUpDownPressed != null)
                    {
                        OnArrowUpDownPressed(key.KeyValue);
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
            bool retValue=true;
            try
            {
                if (NumericColumnNames != null &&  NumericColumnNames.Contains(this.CurrentCell.OwningColumn.Name))
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
        
        public bool IsNewRowColumn()
        {
            bool retValue = false;
            if (this.CurrentCell.ColumnIndex + 1 != this.Columns.Count)
            {
                if (this.Columns[this.CurrentCell.ColumnIndex].Name.ToLower() == NewRowColumnName.ToLower())
                    retValue = true;
            }

            return retValue;
        } 

        public bool IsFirstColumn()
        {
            return (this.CurrentCell.ColumnIndex == 1);
        }

        private bool IsLastRow()
        {
            return (this.CurrentRow.Index + 1 == this.Rows.Count);
        }

        private bool IsLastRowAndNewRowColumn()
        {
            return (IsLastRow() && IsNewRowColumn());
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PSProductMainGridViewCRDB
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
