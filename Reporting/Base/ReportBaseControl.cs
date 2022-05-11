using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.InterfaceLayer;
using EcoMart.BusinessLayer;
using System.IO;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ReportBaseControl : UserControl, IReportControl
    {
        private int _PrintRowCount;
        private int _PrintPageNumber;
        private int _PrintRowPixel;
        private int _PrintColumnPixel;
        private string _PrintReportHead;
        private string _PrintReportHead2;
        private int _PrintTotalPages;
        private Font _PrintFont;
        private double _PrintTotalAmount;
        private double _totpg;
        private string _PrintIfFirstRow;
        private string _StringVMode;

        private string _VouType;
        private string _VouSubType;

        public event EventHandler ExitClicked;
        Form frmView;
        Form frmReport;
        EcoMart.BusinessLayer.Favourite favItem = null;
        public ReportBaseControl()
        {
            InitializeComponent();
            FillEmailIDs();
        }

        #region Properties
        public string VouType
        {
            get { return _VouType; }
            set { _VouType = value; }
        }
        public string VouSubType
        {
            get { return _VouSubType; }
            set { _VouSubType = value; }
        }
        public int PrintRowCount
        {
            get { return _PrintRowCount; }
            set { _PrintRowCount = value; }
        }
        public int PrintPageNumber
        {
            get { return _PrintPageNumber; }
            set { _PrintPageNumber = value; }
        }
        public int PrintRowPixel
        {
            get { return _PrintRowPixel; }
            set { _PrintRowPixel = value; }
        }
        public int PrintColumnPixel
        {
            get { return _PrintColumnPixel; }
            set { _PrintColumnPixel = value; }
        }
        public string PrintReportHead
        {
            get { return _PrintReportHead; }
            set { _PrintReportHead = value; }
        }
        public string PrintReportHead2
        {
            get { return _PrintReportHead2; }
            set { _PrintReportHead2 = value; }
        }
        public int PrintTotalPages
        {
            get { return _PrintTotalPages; }
            set { _PrintTotalPages = value; }
        }
        public Font PrintFont
        {
            get { return _PrintFont; }
            set { _PrintFont = value; }
        }
        public double PrintTotalAmount
        {
            get { return _PrintTotalAmount; }
            set { _PrintTotalAmount = value; }
        }
        public double totpg
        {
            get { return _totpg; }
            set { _totpg = value; }
        }
        public string PrintIfFirstRow
        {
            get { return _PrintIfFirstRow; }
            set { _PrintIfFirstRow = value; }
        }
        private OperationMode _Mode;
        public OperationMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        private BaseControl _ViewControl;
        public BaseControl ViewControl
        {
            get { return _ViewControl; }
            set { _ViewControl = value; }
        }

        private ReportBaseControl _ReportControl;
        public ReportBaseControl ReportControl
        {
            get { return _ReportControl; }
            set { _ReportControl = value; }
        }
        public string StringVMode
        {
            get { return _StringVMode; }
            set { _StringVMode = value; }
        }

        public string _ControlName;
        public string ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        #endregion

        #region Methods


        public void ShowViewForm(string ID, ViewMode vMode)
        {
            if (ViewControl != null)
            {
                frmView = new Form();
                frmView.FormBorderStyle = FormBorderStyle.None;
                frmView.Height = this.Height;
                frmView.Width = this.Width;
                frmView.StartPosition = FormStartPosition.Manual;
                frmView.Location = new Point(this.Location.X + 5, this.Location.Y + 60);
                frmView.KeyPreview = true;
                frmView.KeyDown += new KeyEventHandler(frmView_KeyDown);
                ViewControl.Mode = OperationMode.ReportView;
                ((IDetailControl)ViewControl).View();
                ViewControl.ViewMode = vMode;

                if (vMode == ViewMode.Changed)
                    StringVMode = "C";
                else if (vMode == ViewMode.Current)
                    StringVMode = "";
                else if (vMode == ViewMode.Deleted)
                    StringVMode = "D";


                ViewControl.FillSearchData(ID, StringVMode);
                ViewControl.ExitClicked -= new EventHandler(ViewControl_ExitClicked);
                ViewControl.ExitClicked += new EventHandler(ViewControl_ExitClicked);
                ViewControl.Visible = true;
                ViewControl.Height = this.Height - 6;
                ViewControl.Width = this.Width - 6;
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

        public void ShowReportForm(string ID, string ID2 ,string FromDate, string ToDate)
        {
            try
            {
                if (ReportControl != null)
                {
                    frmReport = new Form();
                    frmReport.FormBorderStyle = FormBorderStyle.None;
                    frmReport.Height = this.Height;
                    frmReport.Width = this.Width;
                    frmReport.StartPosition = FormStartPosition.Manual;
                    frmReport.Location = new Point(this.Location.X + 45, this.Location.Y + 60);
                    frmReport.KeyPreview = true;
                    frmReport.KeyDown += new KeyEventHandler(frmReport_KeyDown);
                    ReportControl.Mode = OperationMode.Report;
                    ReportControl.ShowOverview();
                    ReportControl.ShowReport(ID, ID2, FromDate, ToDate);
                    ReportControl.ExitClicked -= new EventHandler(ReportControl_ExitClicked);
                    ReportControl.ExitClicked += new EventHandler(ReportControl_ExitClicked);
                    ReportControl.Visible = true;
                    ReportControl.Height = this.Height - 6;
                    ReportControl.Width = this.Width - 6;
                    ReportControl.BringToFront();
                    ReportControl.Location = new Point(3, 3);
                    Panel pnl = new Panel();
                    pnl.BackColor = Color.Orange;
                    pnl.Dock = DockStyle.Fill;
                    pnl.Controls.Add(ReportControl);
                    frmReport.Controls.Add(pnl);
                    frmReport.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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


        private void frmReport_KeyDown(object sender, KeyEventArgs e)
        {
            bool IsHandled = false;
            try
            {
                IsHandled = ReportControl.HandleShortcutAction(e.KeyCode, e.Modifiers);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            e.Handled = IsHandled;
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

        private void ReportControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmReport != null)
            {
                frmReport.Close();
            }
        }

        public virtual bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.X && modifier == Keys.Control)
            {
                tsBtnExit_Click(tsBtnExit, new EventArgs());
                retValue = true;
            }
            else if(keyPressed == Keys.P && modifier == Keys.Control) // [07.02.2017]
            {
                tsbtnPrint_Click(tsbtnPrint, new EventArgs());
                retValue = true;
            }
            else if (keyPressed == Keys.Escape)
            {
                tsBtnExit_Click(tsBtnExit, new EventArgs());
                retValue = true;
            }
            return retValue;
        }

        #endregion Methods

        #region Virtual Methods
        public virtual void ShowOverview()
        {

        }

        public virtual void ShowReport(string ID, string FromDate, string ToDate)
        {

        }
        public virtual void ShowReport(string ID1, string ID2, string FromDate, string ToDate)
        {

        }
        public virtual void Print()
        {

        }
        //public virtual bool PrintReport()
        //{
        //    bool retValue = true;
        //    this.Visible = false;
        //    return retValue;
        //}
        public virtual void Export(string ExportFileName)
        {

        }

        public virtual void EMail(string EmailID)
        {

        }
        public virtual bool Exit()
        {
            bool retValue = true;
            pnlFileName.Visible = false;
            txtEmailID.Text = "";
            this.Visible = false;
            if (ExitClicked != null)
                ExitClicked(this, new EventArgs());
            return retValue;
        }

        public virtual void SetFocus()
        {
            this.Focus();
        }

        #endregion Virtual Methods

        #region Events

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            pnlFileName.Visible = false;
            //// string ExportFileName = "";
            // if (pnlFileName.Visible == true)
            //     pnlFileName.Visible = false;
            // else
            // {
            //     pnlFileName.Visible = true;
            //     pnlFileName.BackColor = Color.SlateBlue;
            //     pnlFileName.Height = 53;              
            //     pnlFileName.BringToFront();
            //     txtEmailID.Visible = false;
            //     lblEmailID.Visible = false;
            //     txtFileName.Focus();
            //     if (txtFileName.Text != null && txtFileName.Text.ToString() != string.Empty)
            //     {
            //         txtFileNameKeyDown();
            //     }
            // }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.RestoreDirectory = true;
            dlg.DefaultExt = "pdf";
            dlg.ValidateNames = true;
            dlg.Filter = "PDF File (.pdf)|*.pdf|CSV File (.csv)|*.csv";
            dlg.InitialDirectory = General.ExportPath;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                General.ExportPath = Path.GetDirectoryName(dlg.FileName);
                txtFileName.Text = dlg.FileName;
                ExportReport();
            }
        }

        private void tsBtnEMail_Click(object sender, EventArgs e)
        {

            pnlFileName.BackColor = Color.SkyBlue;
            pnlFileName.Visible = true;
            pnlFileName.BringToFront();
            txtEmailID.Visible = true;
            lblEmailID.Visible = true;
            txtEmailID.Focus();

        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void tsBtnfavourite_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        #endregion Events

        private void txtFileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmailID.Visible == false)
                {
                    txtFileNameKeyDown();
                }
                else
                {
                    txtEmailID.Focus();
                }
            }
        }

        private void txtFileNameKeyDown()
        {
            btnOKClick();
        }

        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmailID.Visible == true)
                {
                    txtEmailIDKeyDown();
                }
            }

        }
        private void txtEmailIDKeyDown()
        {
            btnOKClick();
        }

        private void btnOKClick()
        {
            if (txtEmailID.Visible == true)
            {
                //var addr = new System.Net.Mail.MailAddress(txtEmailID.Text.ToString());
                //if (addr.Address == txtEmailID.Text.ToString())
                SendEmail();
            }
            else
                ExportReport();
        }

        public void SendEmail()
        {
            string ExportFileName = "";
            string EmailID = "";
            if (txtEmailID.Text != null && txtEmailID.Text.ToString() != string.Empty)
            {
                ExportFileName = txtFileName.Text.ToString();
                EmailID = txtEmailID.Text.ToString();
                pnlFileName.Visible = false;
                txtFileName.Text = "";
                txtEmailID.Text = "";
                EMail(EmailID);
            }
        }
        public void ExportReport()
        {
            string ExportFileName = "";
            if (txtFileName.Text != null && txtFileName.Text.ToString() != string.Empty)
            {
                ExportFileName = txtFileName.Text.ToString();
                pnlFileName.Visible = false;
                txtFileName.Text = "";
                Export(ExportFileName);
            }
        }

        private void btnOKMultiSelectionForPanel_Click(object sender, EventArgs e)
        {
            btnOKClick();
        }

        private void FillEmailIDs()
        {
            try
            {
                txtEmailID.SelectedID = null;
                txtEmailID.SourceDataString = new string[3] { "ID", "EmailID", "Details" };
                txtEmailID.ColumnWidth = new string[3] { "0", "250", "200" };
                txtEmailID.ValueColumnNo = 0;
                txtEmailID.UserControlToShow = new UclDoctor();
                EmailID em = new EmailID();
                DataTable dtabled = em.GetOverviewData();
                txtEmailID.FillData(dtabled);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       
    }
}
