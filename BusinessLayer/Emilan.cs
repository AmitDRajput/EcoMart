using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.BusinessLayer
{
    class Emilan
    {
        DBEmilan dbe = new DBEmilan();

        internal bool CopyInvoicesReceived(DataTable invoicesReceived)
        {
            bool retValue = false;
            foreach (DataRow dr in invoicesReceived.Rows)
            {
                retValue = dbe.CopyInvoiceReceived(dr);
            }
            return retValue;
        }


        public DataTable GetPurchaseOrdersForOrderForToday(int firstNumber, int lastNumber)
        {
            //DataTable dt = null;
            DBEmilan dbe = new DBEmilan();
            return dbe.GetPurchaseOrdeerForOrderForToday(firstNumber,lastNumber);
        }

        public DataTable GetOrderDetails(string masterid)
        {
            DBEmilan dbe = new DBEmilan();
            return dbe.GetOrderDetails(masterid);
        }

        public DataTable GetPurchaseBillsForGrid()
        {
            DBEmilan dbe = new DBEmilan();
            return dbe.GetPurchaseBillsForGrid();
        }     

        public DataTable GetPurchaseDetails(string vendorID, string batchID, string invno, string challanno)
        {
            DBEmilan dbe = new DBEmilan();
            return dbe.GetPurchaseBillsForGrid(vendorID, batchID, invno, challanno);
        }
    }
}
