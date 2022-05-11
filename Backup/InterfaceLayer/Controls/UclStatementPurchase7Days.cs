using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStatementPurchase7Days : BaseControl
    {
        # region Declaration
        DataTable _SourceData;
        Settings _Settings;
        #endregion Declaration

        #region Constructor
        public UclStatementPurchase7Days()
        {
        try
            {
                InitializeComponent();
                _SourceData = new DataTable();
                _Settings = new Settings();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            
        }
        public override bool ClearData()
        {
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "STATEMENT PURCHASE - 7 DAYS -> NEW";
            GetOverviewData();
            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            return retValue;
        }


        public override bool Delete()
        {
              ClearData();
            headerLabel1.Text = "STATEMENT PURCHASE - 7 DAYS -> DELETE";
            return true;
        }

        public override bool ProcessDelete()
        {
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            headerLabel1.Text = "STATEMENT PURCHASE - 7 DAYS -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
           
            retValue = true;
            

            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
        {

        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        public void GetOverviewData()
        {
           
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }



        #endregion IDetail Members
    }
}
