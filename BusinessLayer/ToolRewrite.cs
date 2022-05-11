using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class ToolRewrite
    {

        public DataTable ReadFixAccounts()
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.ReadFixAccounts();
        }

        public bool CheckmasterAccountForID(string maccid)
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.CheckmasterAccountForID(maccid);
        }



        public void UpdatemasterAccount(string maccid, string macccode, string maccname, string mgroupid)
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            bool retValue = dbtrw.UpdatemasterAccount(maccid,macccode,maccname,mgroupid);
        }

        public DataTable ReadDataFromtblTrnacForBlankTransactionDate()
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.ReadDataFromtblTrnacForBlankTransactionDate();
        }

        public DataRow ReadDateFromvouchersale(string mvoucherID)
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.ReadDateFromvouchersale(mvoucherID);
        }

        public bool UpdateDateIntbltrnac(string mvoucherID, string mdate)
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.UpdateDateIntbltrnac(mvoucherID, mdate);
        }

        public bool DeletePurchaseRecordsFromtblTrnac()
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.DeletePurchaseRecordsFromtblTrnac();
        }

        public DataTable ReadPurchaseVouchers()
        {
            DBToolRewrite dbtrw = new DBToolRewrite();
            return dbtrw.ReadPurchaseVouchers();
        }
    }
}
