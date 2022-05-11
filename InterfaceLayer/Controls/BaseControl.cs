using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using System.IO;
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class BaseControl : UserControl, IDetailControl
    {
        private double _PrintRowCount;
        private double _PrintPageNumber;
        private int _PrintRowPixel;
        EcoMart.BusinessLayer.Favourite favItem = null;
       

        public event EventHandler ExitClicked;
        public string _SavedID;
        public Int32 _SaveIntID;

        private SearchControl _SearchControl;
        public SearchControl SearchControl
        {
            get { return _SearchControl; }
            set { _SearchControl = value; }
        }

        Form frmSearch;

        public OperationMode _Mode;
        public OperationMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public ViewMode _ViewMode = ViewMode.Current;
        public ViewMode ViewMode
        {
            get { return _ViewMode; }
            set { _ViewMode = value; }
        }

        public string _ControlName;
        public string ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        public bool _IfShortCutOpen;
        public bool IfShortCutOpen
        {
            get { return _IfShortCutOpen; }
            set { _IfShortCutOpen = value; }
        }

        public bool _ISSaleSummaryShow = false;
        public bool ISSaleSummaryShow
        {
            get { return _ISSaleSummaryShow; }
            set { _ISSaleSummaryShow = value; }
        }

        public BaseControl()
        {
            InitializeComponent();
            MainToolStrip.Renderer = new MyRenderer();
        }
      
        #region Properties
        public double PrintRowCount
        {
            get { return _PrintRowCount; }
            set { _PrintRowCount = value; }
        }
        public double PrintPageNumber
        {
            get { return _PrintPageNumber; }
            set { _PrintPageNumber = value; }
        }
        public int PrintRowPixel
        {
            get { return _PrintRowPixel; }
            set { _PrintRowPixel = value; }
        }
        #endregion

        #region IDetailControl Members
        public virtual bool MoveFirst()
        {
            return true;
        }
        public virtual bool MovePrevious()
        {
            return true;
        }
        public virtual bool MoveNext()
        {
            return true;
        }

        public virtual bool MoveLast()
        {
            return true;
        }

        public virtual bool Add()
        {
            ShowHideButtons();
            FillShortcutKeysData();
            return true;
        }

        public virtual bool Edit()
        {
            ShowHideButtons();
            FillShortcutKeysData();
            return true;
        }

        public virtual bool Delete()
        {
            ShowHideButtons();
            FillShortcutKeysData();
            return true;
        }

        public virtual bool Fifth()
        {
            ShowHideButtons();
            FillShortcutKeysData();
            return true;
        }

        public virtual bool ProcessDelete()
        {
            return true;
        }

        public virtual bool View()
        {
            ShowHideButtons();
            FillShortcutKeysData();
            return true;
        }

        public virtual bool Save()
        {
            return true;
        }

        public virtual bool SaveAndPrint()
        {
            return true;
        }

        public virtual bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            //Delete CTRL + A
            if (keyPressed == Keys.A && modifier == Keys.Control)
            {
                bool flag = true;
                if (Mode == OperationMode.Add || Mode == OperationMode.Edit)
                {
                    flag = false;
                }
                else if(flag)
                {
                    tsBtnAdd_Click(tsBtnAdd, new EventArgs());
                    retValue = true;
                }
            }
            //Back CTRL + B
            if (keyPressed == Keys.B && modifier == Keys.Control)
            {
                tsbtnCancel_Click(tsBtnCancel, new EventArgs());
                retValue = true;
            }
            //Close CTRL + C
            if (keyPressed == Keys.C && modifier == Keys.Control)
            {
                tsBtnExit_Click(tsBtnExit, new EventArgs());
                retValue = true;
            }
            //Delete CTRL + D
            if (keyPressed == Keys.D && modifier == Keys.Control)
            {
                tsBtnDelete_Click(tsBtnDelete, new EventArgs());
                retValue = true;
            }
            //Delete CTRL + E
            if (keyPressed == Keys.E && modifier == Keys.Control)
            {
                tsBtnEdit_Click(tsBtnEdit, new EventArgs());
                retValue = true;             
            }
            //Counter Sale Summary CTRL + F
            //if (keyPressed == Keys.F && modifier == Keys.Control && ISSaleSummaryShow == true)
            //{
            //    if (ctrlUclSaleSummaryControl.Visible)
            //    {
            //        ctrlUclSaleSummaryControl.Visible = false;
            //        ctrlUclSaleSummaryControl.SendToBack();
            //    }
            //    else
            //    {
            //        ctrlUclSaleSummaryControl.Visible = true;
            //        ctrlUclSaleSummaryControl.BringToFront();
            //        ctrlUclSaleSummaryControl.btnOKMultiSelectionClick();
            //        ctrlUclSaleSummaryControl.Focus();
            //    }
            //}
            //Search CTRL + H
                if (keyPressed == Keys.H && modifier == Keys.Control)
            {
                tsBtnSearch_Click(tsBtnSearch, new EventArgs());
                retValue = true;
            }
            //Print CTRL + P
            if (keyPressed == Keys.P && modifier == Keys.Control)
            {
                if (tsBtnPrint.Visible == true)
                {
                    tsbtnPrint_Click(tsBtnPrint, new EventArgs());
                    retValue = true;
                }
                else
                    if (tsBtnSavenPrint.Visible == true)
                    {
                        tsBtnSavenPrint_Click(tsBtnSavenPrint, new EventArgs());
                        retValue = true;
                    }
            }

            if (keyPressed == Keys.R && modifier == Keys.Control)
            {
                
            }
            //Save CTRL + S
            if (keyPressed == Keys.S && modifier == Keys.Control)
            {
                if (tsBtnSave.Enabled == true)
                {
                    tsBtnSave_Click(tsBtnSave, new EventArgs());
                }
                retValue = true;
            }
            //Counter Sale Summary CTRL + T//ShortCut CTRL + T
            if (keyPressed == Keys.T && modifier == Keys.Control)
            {
                //ShowHideShortcutPanel();
                //retValue = true;
                if (ISSaleSummaryShow == true && ctrlUclSaleSummaryControl.Visible)
                {
                    ctrlUclSaleSummaryControl.Visible = false;
                    ctrlUclSaleSummaryControl.SendToBack();
                }
                else if(ISSaleSummaryShow)
                {
                    ctrlUclSaleSummaryControl.Visible = true;
                    ctrlUclSaleSummaryControl.BringToFront();
                    ctrlUclSaleSummaryControl.btnOKMultiSelectionClick();
                    ctrlUclSaleSummaryControl.Focus();
                }
            }
            //ShortCut CTRL + Y
            if (keyPressed == Keys.Y && modifier == Keys.Control)
            {
                tsBtnFifth_Click(tsBtnFifth, new EventArgs());
                retValue = true;
            }
            //Help F1
            if (keyPressed == Keys.F1)
            {
                ShowHideShortcutPanel();
                retValue = true;
            }
            if(keyPressed == Keys.Escape)
            {
                if (ctrlUclSaleSummaryControl.Visible)
                {
                    ctrlUclSaleSummaryControl.Visible = false;
                    ctrlUclSaleSummaryControl.SendToBack();
                }
            }
            return retValue;
        }

        public virtual bool Cancel()
        {
            bool retValue = true;
            if (IsDetailChanged())
            {
                DialogResult dResult;
                dResult = MessageBox.Show("Do you want to Go Back?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                {
                    ClearData();
                }
                else
                {
                    retValue = false;
                }
            }
            else
            {
                ClearData();
            }
            return retValue;
        }

        public virtual bool Print()
        {
            return true;
        }

        public virtual bool RefreshProductList()
        {           
            return true;
        }

        //public virtual bool FillSearchData(string ID )
        //{
        //    return true;
        //}
        public virtual bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }
        public virtual bool FillSearchData(string ID)
        {
            return true;
        }
        public virtual bool ClearData()
        {
            return true;
        }

        public virtual void SetFocus()
        {

        }

        public virtual bool Exit()
        {
            bool retValue = true;
            if (IsDetailChanged())
            {
                DialogResult dResult;
                dResult = MessageBox.Show("Do you want to close current Form?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dResult == DialogResult.Yes)
                {
                    //string mysqlpath = "C:\\Program Files\\MySQL\\mysqldump.exe";
                    //if (File.Exists(mysqlpath))
                    //{
                    //    MessageBox.Show("Backup Started");
                    //}
                    this.Visible = false;
                }
                else
                {
                    retValue = false;
                }
            }
            else
            {
                this.Visible = false;
            }
            if (retValue)
            {
                if (ExitClicked != null)
                    ExitClicked(this, new EventArgs());
            }
            return retValue;
           
        }    

        public virtual bool IsDetailChanged()
        {
            return false;
        }

        public virtual void ReFillData()
        {
        }

        public virtual void ReFillData(Control closedControl)
        {

        }

        public virtual string GetShortcutKeys()
        {
            return string.Empty;
        }

        private void FillShortcutKeysData()
        {
            string keyValue = GetShortcutKeys();
            lblShortcutKeys.Text = keyValue;

            //string[] LabelShortKeys = FillAllShortcutkeys();
            //if (LabelShortKeys != null)
            //    SetAllshortkeystoLabel(LabelShortKeys);
        }
        public bool Search()
        {
            try
            {
                if (SearchControl != null)
                {
                    frmSearch = new Form();
                    frmSearch.FormBorderStyle = FormBorderStyle.None;
                    frmSearch.Height = 500;
                    frmSearch.Width = 600;
                    frmSearch.StartPosition = FormStartPosition.CenterScreen;
                    frmSearch.KeyPreview = true;
                    frmSearch.Icon = EcoMart.Properties.Resources.Icon;
                    frmSearch.Load += new EventHandler(frmSearch_Load);
                    SearchControl.ShowOverview();
                    SearchControl.OnGridDoubleClicked -= new SearchControl.GridDoubleClicked(SearchControl_OnGridDoubleClicked);
                    SearchControl.OnGridDoubleClicked += new SearchControl.GridDoubleClicked(SearchControl_OnGridDoubleClicked);
                    SearchControl.ExitClicked -= new EventHandler(SearchControl_ExitClicked);
                    SearchControl.ExitClicked += new EventHandler(SearchControl_ExitClicked);
                    frmSearch.Controls.Add(SearchControl);
                    frmSearch.Size = new Size(SearchControl.GetWidth(), 600);
                    frmSearch.ShowDialog();
                    SetFocus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public virtual void setRightFoooterMessage(DataGridViewRow batchRow)
        {
            try
            {
                string Message = string.Empty;
                Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["BillNo"].Value)) == true) ? string.Empty
                         : "BillNo:" + batchRow.Cells["BillNo"].Value.ToString();
                Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["Col_PurchaseDate"].Value)) == true) ? string.Empty
                        : " BillDate:" + batchRow.Cells["Col_PurchaseDate"].Value.ToString();
                Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["PartyName"].Value)) == true) ? string.Empty
                    : " Party:" + batchRow.Cells["PartyName"].Value.ToString();

                lblRightSideFooterMsg.Text = Message;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            SearchControl.SetFocus();
            
        }
        #endregion
   
        #region Public Methods

        public string SavedID()
        {
            return _SavedID;
        }
        public Int32 SaveIntID()
        {
            return _SaveIntID;
        }

        public void SetButtonFocus(OperationButton button)
        {
            MainToolStrip.Select();
            switch (button)
            {
                case OperationButton.Delete:
                    tsBtnDelete.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.Save:
                    tsBtnSave.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.Cancel:
                    tsBtnCancel.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.Print:
                    tsBtnPrint.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.SavenPrint:
                    tsBtnSavenPrint.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.Exit:
                    tsBtnExit.Select();
                    MainToolStrip.Focus();
                    break;
                case OperationButton.Search:
                    tsBtnSearch.Select();
                    MainToolStrip.Focus();
                    break;
            }
        }

        private void RemoveFromFavourite()
        {
            try
            {
                if (favItem != null)
                {
                    favItem.DeleteDetails();
                    favItem = null;
                    ((MainForm)this.TopLevelControl).LoadFavourites();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void AddToFavourite()
        {
            try
            {
                EcoMart.BusinessLayer.Favourite fav = new EcoMart.BusinessLayer.Favourite();
                fav.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                fav.Name = ControlName;
                fav.ControlName = Name;
                fav.OperationMode = (int)Mode;
                fav.AddDetails();
                favItem = fav;
                ((MainForm)this.TopLevelControl).LoadFavourites();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void SetFavourite(EcoMart.BusinessLayer.Favourite item)
        {
            try
            {
                tsBtnfavourite.ToolTipText = "Remove from favourites";               
                this.tsBtnfavourite.Image = EcoMart.Properties.Resources.fav_enable;
                favItem = item;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void ShowFavourite(bool showFav)
        {
            try
            {
                tsBtnfavourite.Visible = showFav;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Public Methods

        #region Private Methods
        private void ShowHideButtons()
        {
            //TODO: set seperator for all the buttons
            switch (_Mode)
            {
                case OperationMode.Add: //Save, Cancel, Save n Print, Exit
                    tsBtnFirst.Visible = false;
                    tsBtnLast.Visible = false;
                    tsBtnPrevious.Visible = false;
                    tsBtnNext.Visible = false;

                    tsBtnAdd.Visible = false;

                    tsBtnEdit.Visible = false;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = false;
                    tsBtnFifth.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = true;
                    tsSepSave.Visible = true;

                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = true;
                    tsSepSavenPrint.Visible = true;

                    tsBtnSearch.Visible = false;
                    tsSepSearch.Visible = false;

                    tsBtnExit.Visible = true;
                    break;
                case OperationMode.Edit:
                    tsBtnFirst.Visible = false;
                    tsBtnLast.Visible = false;
                    tsBtnPrevious.Visible = false;
                    tsBtnNext.Visible = false;

                    tsBtnAdd.Visible = false;
                    tsBtnFifth.Visible = false;
                    tsBtnEdit.Visible = false;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = true;
                    tsSepSave.Visible = true;

                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = true;
                    tsSepSavenPrint.Visible = true;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = true;
                    break;

                case OperationMode.Delete:

                    tsBtnDelete.Visible = true;
                    tsSepDelete.Visible = true;

                    tsBtnSave.Visible = false;
                    tsSepSave.Visible = false;
                    tsBtnFifth.Visible = false;
                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = false;
                    break;
                case OperationMode.View:
                    tsBtnFirst.Visible = true;
                    tsBtnLast.Visible = true;
                    tsBtnPrevious.Visible = true;
                    tsBtnNext.Visible = true;


                    bool isAddVisible = General.IsUserRightAllowed(ControlName, OperationMode.Add);
                    bool isEditVisible = General.IsUserRightAllowed(ControlName, OperationMode.Edit);
                    bool isDeleteVisible = General.IsUserRightAllowed(ControlName, OperationMode.Delete);
                    
                    tsBtnAdd.Visible = isAddVisible;
                    tsBtnFifth.Visible = isEditVisible;
                    tsBtnEdit.Visible = isEditVisible;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = isDeleteVisible;
                    tsSepDelete.Visible = isDeleteVisible;

                    tsBtnSave.Visible = false;
                    tsSepSave.Visible = false;

                    tsBtnCancel.Visible = false;
                    tsSepCancel.Visible = false;

                    tsBtnPrint.Visible = true;
                    tsSepPrint.Visible = true;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = true;
                    break;              
                case OperationMode.OpenAsChild:
                    tsBtnFirst.Visible = false;
                    tsBtnLast.Visible = false;
                    tsBtnPrevious.Visible = false;
                    tsBtnNext.Visible = false;

                    tsBtnAdd.Visible = false;
                    tsBtnFifth.Visible = false;
                    tsBtnEdit.Visible = false;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = true;
                    tsSepSave.Visible = true;

                    tsBtnCancel.Visible = false;
                    tsSepCancel.Visible = false;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = false;
                    tsSepSearch.Visible = false;

                    tsBtnExit.Visible = true;
                    break;
                case OperationMode.ReportView:
                    tsBtnFirst.Visible = false;
                    tsBtnLast.Visible = false;
                    tsBtnPrevious.Visible = false;
                    tsBtnNext.Visible = false;

                    tsBtnAdd.Visible = false;
                    tsBtnFifth.Visible = false;
                    tsBtnEdit.Visible = false;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = false;
                    tsSepSave.Visible = false;

                    tsBtnCancel.Visible = false;
                    tsSepCancel.Visible = false;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = false;
                    tsSepSearch.Visible = false;

                    tsBtnExit.Visible = true;
                    break;

                case OperationMode.Fifth:
                    tsBtnFirst.Visible = false;
                    tsBtnLast.Visible = false;
                    tsBtnPrevious.Visible = false;
                    tsBtnNext.Visible = false;

                    tsBtnAdd.Visible = false;

                    tsBtnEdit.Visible = false;
                    tsBtnFifth.Visible = false;

                    tsBtnView.Visible = false;

                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = true;
                    tsSepSave.Visible = true;

                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = true;
                    tsSepSavenPrint.Visible = true;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = true;
                    break;
            }
        }
        #endregion Private Methods

        #region Events
        private void tsBtnFirst_Click(object sender, EventArgs e)
        {
            MoveFirst();
        }

        private void tsBtnPrevious_Click(object sender, EventArgs e)
        {
            MovePrevious();
        }

        private void tsBtnNext_Click(object sender, EventArgs e)
        {
            MoveNext();
        }

        private void tsBtnLast_Click(object sender, EventArgs e)
        {
            MoveLast();
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            _Mode = OperationMode.Add;
            Add();
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            _Mode = OperationMode.Edit;
            Edit();
        }

        private void tsBtnView_Click(object sender, EventArgs e)
        {
            _Mode = OperationMode.View;
            View();
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dResult;
            dResult = MessageBox.Show("Are you sure you want to Delete?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dResult == DialogResult.Yes)
            {
                ProcessDelete(); 
                _Mode = OperationMode.Delete;
                Delete();
            }
        }
        private void tsBtnFifth_Click(object sender, EventArgs e)
        {
            _Mode = OperationMode.Fifth;
            Edit();
        } 
        public void tsbtnSaveClik()
        {
            tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = false;
            if (Save())
            {
                tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = true;
                ClearData();
                if (_Mode == OperationMode.OpenAsChild)
                {
                    this.Visible = false;
                    if (ExitClicked != null)
                        ExitClicked(this, new EventArgs());
                }
                else
                {
                    switch (_Mode)
                    {
                        case OperationMode.Add:
                            Add();
                            break;
                        case OperationMode.Edit:
                            Edit();
                            break;
                    }
                }
                //if (string.Equals(_ControlName, "Opening Stock") || string.Equals(_ControlName, "Product"))
                //    CacheObject.Clear("cacheCounterSale");
            }
            else if(tsBtnAdd.Visible)
            {
                tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = true;
                MainToolStrip.Select();
                tsBtnAdd.Select();
            }
            else
                tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = true;
            //  tsBtnSave.Enabled = tsBtnPrint.Enabled = true;
        }
        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            tsbtnSaveClik();
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            if (Cancel())
            {
                _Mode = OperationMode.View;
                View();
            }
        }

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            Print();
           // ClearData();
        }

        private void tsBtnSavenPrint_Click(object sender, EventArgs e)
        {
            tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = false;
            if (SaveAndPrint())
            {
                tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = true;
                ClearData();
                if (_Mode == OperationMode.OpenAsChild)
                {
                    this.Visible = false;
                    if (ExitClicked != null)
                        ExitClicked(this, new EventArgs());
                }
                else
                {
                    switch (_Mode)
                    {
                        case OperationMode.Add:
                            Add();
                            break;
                        case OperationMode.Edit:
                            Edit();
                            break;
                    }
                }
                if (string.Equals(_ControlName, "Opening Stock") || string.Equals(_ControlName, "Product"))
                    CacheObject.Clear("cacheCounterSale");
            }
            else { tsBtnSave.Enabled = tsBtnSavenPrint.Enabled = true; }
        }

        private void tsBtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void tsBtnShortcuts_Click(object sender, EventArgs e)
        {
            ShowHideShortcutPanel();
        }

        private void ShowHideShortcutPanel()
        { 
            pnlShortcuts.Visible = !pnlShortcuts.Visible;
            pnlShortcuts.Location = new Point(this.Width - pnlShortcuts.Width, 0);
            if (pnlShortcuts.Visible)
            {
                pnlShortcuts.BringToFront();
                IfShortCutOpen = true;
            }
            else
            {
                pnlShortcuts.SendToBack();
                IfShortCutOpen = false;

            }
            
        }

        private void SetAllshortkeystoLabel(string[] labelShortKeys)
        {
            FillShortKeyLable(lblShortKey1, pictureBox1, labelShortKeys[0], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShorKey2, pictureBox2, labelShortKeys[1], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey3, pictureBox3, labelShortKeys[2], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey4, pictureBox4, labelShortKeys[3], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey5, pictureBox5, labelShortKeys[4], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey6, pictureBox6, labelShortKeys[5], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey7, pictureBox7, labelShortKeys[6], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey8, pictureBox8, labelShortKeys[7], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey9, pictureBox9, labelShortKeys[8], (int)labelShortKeys.Length);
            FillShortKeyLable(lblShortKey10, pictureBox10, labelShortKeys[9], (int)labelShortKeys.Length);
            FillShortKeyLable(label1, pictureBox11, labelShortKeys[10], (int)labelShortKeys.Length);
            FillShortKeyLable(label2, pictureBox12, labelShortKeys[11], (int)labelShortKeys.Length);
        }

        private void FillShortKeyLable(Label lblShortKey, PictureBox pic, string ShortKeys, int length)
        {
            if (string.IsNullOrEmpty(ShortKeys) == false)
            {
                lblShortKey.Visible = true;
                pic.Visible = true;
                lblShortKey.Text = ShortKeys;
            }
            else
            {
                pic.Visible = false;
                lblShortKey.Visible = false;
            }
        }
        public virtual string[] FillAllShortcutkeys()
        {
            return null;
        }

        private void SearchControl_OnGridDoubleClicked(string ID)
        {
            if (_Mode != OperationMode.Add)
            {
                FillSearchData(ID, "");
            }
            if (frmSearch != null)
            {
                frmSearch.Close();
            }
        }

        private void SearchControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmSearch != null)
                frmSearch.Close();
        }

        private void tsBtnfavourite_Click(object sender, EventArgs e)
        {
            if (favItem == null)
            {
                tsBtnfavourite.ToolTipText = "Remove from favourites";
                tsBtnfavourite.Image = EcoMart.Properties.Resources.fav_enable;
                AddToFavourite();               
            }
            else
            {
                tsBtnfavourite.ToolTipText = "Add to favourites";
                tsBtnfavourite.Image = EcoMart.Properties.Resources.fav_disable;
                RemoveFromFavourite();                
            }
        }

        #endregion Events      

     
    }

    public class MyRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            try
            {
                base.OnRenderItemText(e);
                var btn = e.Item as ToolStripButton;
                if (btn != null )
                {
                    int startMargin = 3;
                    int startWidth = 10;
                    int startHeight = 3;

                    int startX = e.TextRectangle.X + startMargin;
                    int startY = e.TextRectangle.Y + e.TextRectangle.Height - startHeight;

                    string startStr = string.Empty;
                    int loc = GetHotkeyLocation(btn.Text);
                    if (loc > 0)
                    {
                        startStr = btn.Text.Substring(0, loc);
                        if (string.IsNullOrEmpty(startStr) == false)
                            startX += (int)e.Graphics.MeasureString(startStr, btn.Font).Width - startMargin - 2;
                    }                    
                    
                    Rectangle bounds = new Rectangle(startX, startY, startWidth, startHeight);
                    e.Graphics.FillRectangle(Brushes.Orange, bounds);
                }               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private int GetHotkeyLocation(string text)
        {
            int retValue = 0;
            switch (text.ToLower())
            {
                case "save and print":
                    retValue = 9;
                    break;
                case "shortcuts":
                    retValue = 4;
                    break;
                case "search":
                    retValue = 5;
                    break; 
                case "typechange":
                    retValue = 1;
                    break;
            }
            return retValue;
        }
       
    }
}
