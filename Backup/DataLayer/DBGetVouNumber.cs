using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class DBGetVouNumbers
    {
        
        # region Debit Note

        public int GetDebitNote(string voucherseries)
        {
            DataRow drow = null;
            int dnno = 0;          
            try
            {
                string strSql = "Select DebitNote from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);

                if (drow != null)
                {
                    if (drow["DebitNote"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["DebitNote"]);
                }
                UpdateDebitNote(dnno + 1,voucherseries);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dnno+1;
        }

        private void UpdateDebitNote(int dn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set DebitNote = " + dn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
          
        }
        #endregion
        # region Credit Note

        public int GetCreditNote(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            try
            {
                string strSql = "Select CreditNote from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);        

                if (drow != null)
                {
                    if (drow["CreditNote"] != DBNull.Value)
                        cnno = Convert.ToInt32(drow["CreditNote"]);
                }

                UpdateCreditNote(cnno + 1,voucherseries);      
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnno + 1;
        }

        private void UpdateCreditNote(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set CreditNote = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region StockIn

        public int GetStockIn(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StockIn from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StockIn"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StockIn"]);
            }

            UpdateStockIn(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateStockIn(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set StockIn = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region StockOut

        public int GetStockOut(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StockOut from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StockOut"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StockOut"]);
            }

            UpdateStockOut(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateStockOut(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set StockOut = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion        
        # region Cash Receipt

        public int GetCashReceipt(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select CashReceipt from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["CashReceipt"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["CashReceipt"]);
            }

            UpdateCashReceipt(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateCashReceipt(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set CashReceipt = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region Cash Payment

        public int GetCashPayment(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select CashPaid from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["CashPaid"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["CashPaid"]);
            }

            UpdateCashPayment(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateCashPayment(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set CashPaid = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region Bank Receipt

        public int GetBankReceipt(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select BankReceipt from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["BankReceipt"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["BankReceipt"]);
            }

            UpdateBankReceipt(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateBankReceipt(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set BankReceipt = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }

        public int GetChequeReturn(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select ChequeReturn from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["ChequeReturn"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["ChequeReturn"]);
            }

            UpdateChequeReturn(cnno + 1, voucherseries);
            return cnno + 1;
        }

        private void UpdateChequeReturn(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set ChequeReturn = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region Bank Payment

        public int GetBankPayment(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select BankPaid from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["BankPaid"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["BankPaid"]);
            }

            UpdateBankPayment(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateBankPayment(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set BankPaid = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region CCSale

        public int GetCCSale(string voucherseries)
        {
            DataRow drow = null;
            int dnno = 0;
            string strSql = "Select SaleCashCredit from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["SaleCashCredit"] != DBNull.Value)
                    dnno = Convert.ToInt32(drow["SaleCashCredit"]);
            }

            UpdateCCSale(dnno + 1,voucherseries);
            return dnno + 1;
        }

        private void UpdateCCSale(int dn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set SaleCashCredit = " + dn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);

        }

        #endregion
        # region Sale
        public int GetSale(string vtype, string voucherseries)
        {
            DataRow drow = null;
            int dnno = 0;          
            if (vtype == FixAccounts.VoucherTypeForCreditSale)
            {
                string strSql = "Select SaleCashCredit from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["SaleCashCredit"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["SaleCashCredit"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForCashSale)
            {
                string strSql = "Select SaleCash from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["SaleCash"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["SaleCash"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForVoucherSale)
            {
                string strSql = "Select SaleChitNumber from tblvouchernumbers where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["SaleChitNumber"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["SaleChitNumber"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForCreditStatementSale)
            {
                string strSql = "Select SaleCredit from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["SaleCredit"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["SaleCredit"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForDistributorSaleCash)
            {
                string strSql = "Select DistributorSaleCash from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["DistributorSaleCash"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["DistributorSaleCash"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForDistributorSaleCredit)
            {
                string strSql = "Select DistributorSaleCredit from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["DistributorSaleCredit"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["DistributorSaleCredit"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForDistributorSaleCredit)
            {
                string strSql = "Select DistributorSaleCreditStatement from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["DistributorSaleCreditStatement"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["DistributorSaleCreditStatement"]);
                }
            }
            UpdateSale(dnno + 1,vtype,voucherseries);
            return dnno + 1;
        }
        private void UpdateSale(int dn, string vt, string voucherseries)
        {
            string strSql = "";
            if (vt == FixAccounts.VoucherTypeForCreditStatementSale)
                strSql = "Update  tblvouchernumbers set SaleCredit  = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForCashSale)
                strSql = "Update  tblvouchernumbers set SaleCash = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForVoucherSale)
                strSql = "Update tblvouchernumbers set SaleChitNumber = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForCreditSale)
                strSql = "Update  tblvouchernumbers set SaleCashCredit = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForDistributorSaleCash)
                strSql = "Update  tblvouchernumbers set DistributorSaleCash = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForDistributorSaleCredit)
                strSql = "Update  tblvouchernumbers set DistributorSaleCredit = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForDistributorSaleCreditStatement)
                strSql = "Update  tblvouchernumbers set DistributorSaleCreditStatement = " + dn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region Purchase
        public int GetPurchase(string vtype, string voucherseries)
        {
            DataRow drow = null;
            int dnno = 0;           
            if (vtype == FixAccounts.VoucherTypeForCreditPurchase)
            {
                string strSql = "Select PurchaseCashCredit from tblvouchernumbers where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["PurchaseCashCredit"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["PurchaseCashCredit"]);
                }
            }
            else if (vtype == FixAccounts.VoucherTypeForCashPurchase)
            {
                string strSql = "Select PurchaseCash from tblvouchernumbers where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["PurchaseCash"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["PurchaseCash"]);
                }
            }
            else
            { 
               
                string strSql = "Select PurchaseCredit from tblvouchernumbers  where ID = '" + voucherseries + "'";
                drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                {
                    if (drow["PurchaseCredit"] != DBNull.Value)
                        dnno = Convert.ToInt32(drow["PurchaseCredit"]);
                }
            }
            UpdatePurchase(dnno + 1, vtype,voucherseries);
            return dnno + 1;
        }
        private void UpdatePurchase(int dn, string vt, string voucherseries)
        {
            string strSql = "";
            if (vt == FixAccounts.VoucherTypeForCreditPurchase)
                strSql = "Update  tblvouchernumbers set PurchaseCashCredit = " + dn + " where ID = '" + voucherseries + "'";
            else if (vt == FixAccounts.VoucherTypeForCashPurchase)
                strSql = "Update  tblvouchernumbers set PurchaseCash = " + dn + " where ID = '" + voucherseries + "'";
            else
                strSql = "Update  tblvouchernumbers set PurchaseCredit = " + dn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion
        # region Cash Expenses

        public int GetCashExpenses(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select CashExpenses from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["CashExpenses"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["CashExpenses"]);
            }

            UpdateCashExpenses(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateCashExpenses(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set CashExpenses = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion

        # region Bank Expenses

        public int GetBankExpenses(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select BankExpenses from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["BankExpenses"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["BankExpenses"]);
            }

            UpdateBankExpenses(cnno + 1, voucherseries);
            return cnno + 1;
        }

        private void UpdateBankExpenses(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set BankExpenses = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion


        #region PurchseOrder
        public int GetPurchaseOrder(string voucherseries)
        {
            
            DataRow drow = null;
            int pono = 0;
            string strSql = "Select PurchaseOrder from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["PurchaseOrder"] != DBNull.Value)
                    pono = Convert.ToInt32(drow["PurchaseOrder"]);
            }

            UpdatePurchaseOrder(pono + 1,voucherseries);
            return pono + 1;
        }

        private void UpdatePurchaseOrder(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set PurchaseOrder = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion PurchaseOrder
        #region OpeningStock
        public int GetOpeningStock(string voucherseries)
        {

            DataRow drow = null;
            int pono = 0;
            string strSql = "Select OpeningStock from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["OpeningStock"] != DBNull.Value)
                    pono = Convert.ToInt32(drow["OpeningStock"]);
            }

            UpdateOpeningStock(pono + 1,voucherseries);
            return pono + 1;
        }

        private void UpdateOpeningStock(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set OpeningStock = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion Opening Stock
        #region TokenNumber
        //public int GetTokenNumber(string voucherseries)
        //{

        //    DataRow drow = null;
        //    int pono = 0;        
        //    string strSql = "Select TokenNumber from tblvouchernumbers for update";
        //    drow = DBInterface.SelectFirstRow(strSql);
        //    if (drow != null)
        //    {
        //        if (drow["TokenNumber"] != DBNull.Value)
        //            pono = Convert.ToInt32(drow["TokenNumber"]);
        //    }

        //    UpdateTokenNumber(pono + 1);
        //    return pono + 1;
        //}
         public void UpdateTokenNumber(int cn)
        {
            string strSql = "Update  tblvouchernumbers set TokenNumber = " + cn + " ;";
            DBInterface.ExecuteQuery(strSql);
        }
        //public int GetTokenNumberInEditMode()
        //{
        //    DataRow drow = null;
        //    int pono = 0;
        //    string strSql = "Select TokenNumber from tblvouchernumbers for update";
        //    drow = DBInterface.SelectFirstRow(strSql);
        //    if (drow != null)
        //    {
        //        if (drow["TokenNumber"] != DBNull.Value)
        //            pono = Convert.ToInt32(drow["TokenNumber"]);
        //    }          
        //    return pono ;
        //}
        //public void UpdateTokenNumberInEditMode(int cn)
        //{
        //    string strSql = "Update  tblvouchernumbers set TokenNumber = " + cn + " ;";
        //    DBInterface.ExecuteQuery(strSql);
        //}
        #endregion TokenNumber
        #region CorrectioninRate
        public int GetCorrectionInRate(string voucherseries)
        {
            DataRow drow = null;
            int pono = 0;
            string strSql = "Select CorrectionInRate from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["CorrectionInRate"] != DBNull.Value)
                    pono = Convert.ToInt32(drow["CorrectionInRate"]);
            }

            UpdateCorrectionInRate(pono + 1,voucherseries);
            return pono + 1;
        }

        private void UpdateCorrectionInRate(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set CorrectionInRate = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion CorrectionInRate
        # region JV

        public int GetJV(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select JournalVoucher from tblvouchernumbers where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["JournalVoucher"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["JournalVoucher"]);
            }

            UpdateJV(cnno + 1,voucherseries);
            return cnno + 1;
        }

        private void UpdateJV(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set JournalVoucher = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion JV
        # region Purchase Statements

        public int GetPurchaseStatementToView(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementPurchase from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementPurchase"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementPurchase"]);
            }
            return cnno + 1;
        }

       

        public int GetPurchaseStatement(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementPurchase from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementPurchase"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementPurchase"]);
            }

            UpdatePurchaseStatement(cnno + 1,voucherseries);
            return cnno + 1;
        }

        public int GetLastVoucherNumberFortblVoucherNumbersPurchase(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementPurchase from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementPurchase"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementPurchase"]);
            }
            
            return cnno ;
        }

        public int GetLastVoucherNumberFortblVoucherNumbersSale(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementSale from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementSale"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementSale"]);
            }

            return cnno;
        }

        private void UpdatePurchaseStatement(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set StatementPurchase = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        public int GetSaleStatement(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementSale from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementSale"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementSale"]);
            }

            UpdateSaleStatement(cnno + 1, voucherseries);
            return cnno + 1;
        }

        private void UpdateSaleStatement(int cn, string voucherseries)
        {
            string strSql = "Update  tblvouchernumbers set StatementSale = " + cn + " where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
        }
        #endregion PurchaseStatement
        # region Sale Statements
        public int GetSaleStatementToView(string voucherseries)
        {
            DataRow drow = null;
            int cnno = 0;
            string strSql = "Select StatementSale from tblvouchernumbers  where ID = '" + voucherseries + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["StatementSale"] != DBNull.Value)
                    cnno = Convert.ToInt32(drow["StatementSale"]);
            }
            return cnno + 1;
        }
      
        #endregion SaleStatement
    }
}
