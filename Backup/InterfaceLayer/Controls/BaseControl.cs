using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using System.IO;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class BaseControl : UserControl, IDetailControl
    {
        private double _PrintRowCount;
        private double _PrintPageNumber;
        private int _PrintRowPixel;
        PharmaSYSRetailPlus.BusinessLayer.Favourite favItem = null;

        public event EventHandler ExitClicked;
        public string _SavedID;

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


        public BaseControl()
        {
            InitializeComponent();
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

        public virtual bool Add()
        {
            ShowHideButtons();
            return true;
        }

        public virtual bool Edit()
        {
            ShowHideButtons();
            return true;
        }

        public virtual bool Delete()
        {
            ShowHideButtons();
            return true;
        }

        public virtual bool Fifth()
        {
            ShowHideButtons();
            return true;
        }

        public virtual bool ProcessDelete()
        {
            return true;
        }

        public virtual bool View()
        {
            ShowHideButtons();
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
            //Delete CTRL + D
            if (keyPressed == Keys.D && modifier == Keys.Control)
            {
                tsBtnDelete_Click(tsBtnDelete, new EventArgs());
                retValue = true;
            }
            //Save CTRL + S
            if (keyPressed == Keys.S && modifier == Keys.Control)
            {
                tsBtnSave_Click(tsBtnSave, new EventArgs());
                retValue = true;
            }
            //Cancel CTRL + C
            if (keyPressed == Keys.C && modifier == Keys.Control)
            {
                tsbtnCancel_Click(tsBtnCancel, new EventArgs());
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
            //Search CTRL + H
            if (keyPressed == Keys.H && modifier == Keys.Control)
            {
                tsBtnSearch_Click(tsBtnSearch, new EventArgs());
                retValue = true;
            }
            //Exit CTRL + X
            if (keyPressed == Keys.X && modifier == Keys.Control)
            {
                tsBtnExit_Click(tsBtnExit, new EventArgs());
                retValue = true;
            }
            //ShortCut CTRL + T
            if (keyPressed == Keys.T && modifier == Keys.Control)
            {
                ShowHideShortcutPanel();
                retValue = true;
            }
            //Help F1
            if (keyPressed == Keys.F1)
            {
                ShowHideShortcutPanel();
                retValue = true;
            }

            return retValue;
        }

        public virtual bool Cancel()
        {
            bool retValue = true;
            if (IsDetailChanged())
            {
                DialogResult dResult;
                dResult = MessageBox.Show("Do you want to Cancel?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                dResult = MessageBox.Show("Do you want to Exit?", General.ApplicationTitle, MessageBoxButtons.YesNo,  MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
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
        public bool Search()
        {
            if (SearchControl != null)
            {
                frmSearch = new Form();
                frmSearch.FormBorderStyle = FormBorderStyle.None;
                frmSearch.Height = 500;
                frmSearch.Width = 600;
                frmSearch.StartPosition = FormStartPosition.CenterScreen;
                frmSearch.KeyPreview = true;
                frmSearch.Icon = PharmaSYSRetailPlus.Properties.Resources.Icon;
                frmSearch.Load += new EventHandler(frmSearch_Load);
                SearchControl.ShowOverview();
                SearchControl.OnGridDoubleClicked -= new SearchControl.GridDoubleClicked(SearchControl_OnGridDoubleClicked);
                SearchControl.OnGridDoubleClicked += new SearchControl.GridDoubleClicked(SearchControl_OnGridDoubleClicked);
                SearchControl.ExitClicked -= new EventHandler(SearchControl_ExitClicked);
                SearchControl.ExitClicked += new EventHandler(SearchControl_ExitClicked);
                frmSearch.Controls.Add(SearchControl);
                frmSearch.Size = new Size(SearchControl.GetWidth(), 700);                
                frmSearch.ShowDialog();
            }
            return true;
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

        public void SetButtonFocus(OperationButton button)
        {
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
                PharmaSYSRetailPlus.BusinessLayer.Favourite fav = new PharmaSYSRetailPlus.BusinessLayer.Favourite();
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

        public void SetFavourite(PharmaSYSRetailPlus.BusinessLayer.Favourite item)
        {
            try
            {
                tsBtnfavourite.ToolTipText = "Remove from favourites";               
                this.tsBtnfavourite.Image = Properties.Resources.fav_enable;
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

                    tsBtnSearch.Visible = false;
                    tsSepSearch.Visible = false;

                    tsBtnExit.Visible = true;
                    break;
                case OperationMode.Edit:
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

                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = false;
                    tsSepPrint.Visible = false;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = true;
                    break;
                case OperationMode.View:
                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = false;
                    tsSepSave.Visible = false;

                    tsBtnCancel.Visible = true;
                    tsSepCancel.Visible = true;

                    tsBtnPrint.Visible = true;
                    tsSepPrint.Visible = true;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = true;
                    tsSepSearch.Visible = true;

                    tsBtnExit.Visible = true;
                    break;
                case OperationMode.OpenAsChild:
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
                    tsBtnDelete.Visible = false;
                    tsSepDelete.Visible = false;

                    tsBtnSave.Visible = false;
                    tsSepSave.Visible = false;

                    tsBtnCancel.Visible = false;
                    tsSepCancel.Visible = false;

                    tsBtnPrint.Visible = true;
                    tsSepPrint.Visible = true;

                    tsBtnSavenPrint.Visible = false;
                    tsSepSavenPrint.Visible = false;

                    tsBtnSearch.Visible = false;
                    tsSepSearch.Visible = false;

                    tsBtnExit.Visible = true;
                    break;

                case OperationMode.Fifth:
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
        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            ProcessDelete();
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
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
            }
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            if (Cancel())
            {
                //switch (_Mode)
                //{
                //    case OperationMode.Add:
                //        Add();
                //        break;
                //    case OperationMode.Edit:
                //        Edit();
                //        break;
                //    case OperationMode.Delete:
                //        Delete();
                //        break;
                //    case OperationMode.View:
                //        View();
                //        break;
                //}
            }
        }

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            Print();
            ClearData();
        }

        private void tsBtnSavenPrint_Click(object sender, EventArgs e)
        {
            if (SaveAndPrint())
            {
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
            }
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
                pnlShortcuts.BringToFront();
            else
                pnlShortcuts.SendToBack();
        }

        private void SearchControl_OnGridDoubleClicked(string ID)
        {
            if (_Mode != OperationMode.Add)
            {
                FillSearchData(ID, "");
            }
            if (frmSearch != null)
                frmSearch.Close();
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
                tsBtnfavourite.Image = Properties.Resources.fav_enable;
                AddToFavourite();               
            }
            else
            {
                tsBtnfavourite.ToolTipText = "Add to favourites";
                tsBtnfavourite.Image = Properties.Resources.fav_disable;
                RemoveFromFavourite();                
            }
        }

        #endregion Events

       

    }
}
