using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using EcoMart.InterfaceLayer;
using EcoMart.Printing;
using PrintDataGrid;


namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclEcoMartProductWiseSaleStockist : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        # endregion
        public UclEcoMartProductWiseSaleStockist()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
