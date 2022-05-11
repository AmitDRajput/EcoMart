﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    class DBBankReceipt
    {
        public DBBankReceipt()
        {
        }
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "'" + "  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataForChequeReturn(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.IFchequereturn,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where  a.IFchequereturn = 'N' && a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "'" + "  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataForReport(string bankID, string vouType,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + vouType + "' && a.VoucherDate >= '" + fromDate + "'  && a.VoucherDate <= '" + toDate + "' && ChequeDepositedBankID = '"+ bankID +"'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetDataForReportChequesReceivedButNotCleared(string bankID, string vouType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + vouType + "' && a.VoucherDate >= '" + fromDate + "'  && a.VoucherDate <= '" + toDate + "' && ChequeDepositedBankID = '" + bankID + "' && (ClearedDate is null || ClearedDate = '' || ClearedDate <= '" + toDate + "')  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }    

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherSeries,a.VoucherType,a.IfChequereturn,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where CBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }           
            return dRow;
        }
        public DataRow ReadDetailsByIDForChanged(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from changedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where ChangedID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByIDForDeleted(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from deletedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where CBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber, a.ChequeDate,a.CustomerBankID,a.CustomerBranchID,a.ChequeDepositedBankID,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where VoucherNumber= " + vouno + " && VoucherType = '" + FixAccounts.VoucherTypeForBankReceipt + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable GetSaleDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();          
            string strSql = "Select ID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,AmountClear,AmountBalance,PatientShortName,"
                + "AccountID,ID as MasterID,null as MasterSaleID  from vouchersale where  AccountId =  " + "'" + acId + "'" + " &&  VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' && AmountBalance > 0 && VoucherSubType != '" + FixAccounts.SubTypeForHospitalSale + "' order by vouchertype, vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            string strSql = "select a.ID , a.VoucherSeries, a.VoucherType,a.VoucherNumber,a.VoucherDate,'' as VoucherSubType,  a.AmountNet,a.AmountClear,a.AmountBalance," +
                          "a.AccountID,'' as MasterID,'' as MasterSaleID,d.AccName as PatientshortName from voucherstatement a inner join masteraccount d on a.AccountID = d.AccountID where a.AccountID = '" + acId + "'  &&  a.AmountBalance > 0 && (a.VoucherType = '" + FixAccounts.VoucherTypeForStatementSale + "' || a.Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by a.vouchertype ,a.vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSaleDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
            //             + "a.AccountID,a.PatientShortName,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID && b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
                           + "a.AccountID,a.PatientShortName,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID && b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate, '' as VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
                + "a.AccountID,c.AccName as PatientShortName,b.MasterID,b.MasterSaleID  from voucherstatement a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID inner join masteraccount c on a.AccountID = c.AccountId  &&  b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        //public DataTable GetSaleDetailsByIDforModify(string acId, string id)
        //{
        //    DataTable dtable = new DataTable();

        //    string strSql = "Select ID ,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,Amountclear,AmountBalance,"
        //        + "AccountID, null as MasterID, null as MasterSaleID  from vouchersale where  AccountId =  " + "'" + acId + "'" + " &&  VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' && Amountclear = 0 "
        //        + "union Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,AmountBalance,"
        //        + "a.AccountID,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID and b.MasterID = '" + id + "'  order by VoucherNumber";
        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}

        public DataTable GetSaleDetailsByBKRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.MasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID,c.PatientShortName " +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  left outer join vouchersale c on a.MasterSaleID = c.ID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetSaleDetailsByBKRIDForChanged(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ChangedMasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from changeddetailcashbankreceipt a inner join changedvouchercashbankreceipt b  on a.ChangedMasterId = b.ChangedId   where  b.ChangedID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetSaleDetailsByBKRIDForDeleted(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID,a.MasterId, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from deleteddetailcashbankreceipt a inner join deletedvouchercashbankreceipt b  on a.MasterId = b.CBId where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByBKRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.MasterId ,a.FromDate,a.ToDate,b.AccountID,d.AccName as PatientShortName " +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  inner join voucherstatement c on a.MasterSaleID = c.ID  inner join masteraccount d on c.AccountID = d.AccountID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public bool AddDetailsChequeReturn(string ChequeReturnID,string ChequeReturnVouType, int ChequeReturnVouNo,string ChequeReturnVouDate,double ChequeReturnCharges,string Id, string CreditorId, string Narration, string VouType, int VouNo,
         string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChequeReturn(ChequeReturnID,ChequeReturnVouType,ChequeReturnVouNo,ChequeReturnVouDate,ChequeReturnCharges, Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt,string bankaccount,string bank,string branch,string chqno,string chqdate, double onaccountamount ,string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,bankaccount,bank,branch,chqno,chqdate,onaccountamount, createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddChangedDetails(string Id,string changedID, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChanged(Id,changedID, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDeletedDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeleted(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount,  createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticularsChequeReturn(string chequereturnID, string Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
            string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, string DetailID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsChequeReturn( chequereturnID, Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, DetailID, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticulars(string Id,string detailID,  string SaleId, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, string DetailID,int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id,detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount,DetailID,serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticularsChanged(string Id, string changedID, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
             string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, string DetailID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsChanged(Id,changedID, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, DetailID, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticularsDeleted(string Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
           string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, string DetailID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsDeleted(Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, DetailID, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetailsSCCBill(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from vouchersale where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount;
            //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update vouchersale set AmountClear =   " + mamtclear + ",  AmountBalance = " + mamtbalance + " where  ID = '" + saleid + "'";


            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool UpdateSaleStatement(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
             string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from voucherstatement where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount;
            //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update voucherstatement set AmountClear =   " + mamtclear + ",  AmountBalance = " + mamtbalance + " where  ID = '" + saleid + "'";


            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            //bool bRetValue = false;
            //double BBalanceAmt = BBalanceAmount - BClearedAmount;
            //string strSql = "Update voucherstatement set AmountClear =  AmountClear + " + BClearedAmount + ",  AmountBalance = " +
            //   "AmountNet - AmountClear where  ID = '" + saleid + "'";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //    bRetValue = true;
            return bRetValue;
        }
        public bool UpdateOpeningBalance(string accID, double clearedAmount, double PreopeningCleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '" + accID + "'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt + clearedAmount - PreopeningCleared;
            strSql = "update masteraccount  set AccClearedAmount = " + mclearedamt + " Where AccountID = '" + accID + "'";
            bRetValue = DBInterface.ExecuteQuery(strSql) > 0;
            return bRetValue;
        }
        public bool UpdateOpeningBalanceReducePrevious(string accID, double PreopeningCleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '" + accID + "'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt - PreopeningCleared;
            strSql = "update masteraccount  set AccClearedAmount = " + mclearedamt + " Where AccountID = '" + accID + "'";
            bRetValue = DBInterface.ExecuteQuery(strSql) > 0;
            return bRetValue;
        }
        public bool UpdateOpeningBalanceAddNew(string accID, double Cleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '" + accID + "'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt + Cleared;
            strSql = "update masteraccount  set AccClearedAmount = " + mclearedamt + " Where AccountID = '" + accID + "'";
            bRetValue = DBInterface.ExecuteQuery(strSql) > 0;
            return bRetValue;
        }
        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate,double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,bankaccount,bank,branch,chqno,chqdate,onaccountamount, modifiedby,modifieddate,modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsForFifth(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForFifth(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount, modifiedby, modifieddate, modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool RevertPreviousSaleBalance(string nSaleID, double nClearedAmount)
        {
            bool bRetValue = false;
            string strSql = "Update vouchersale set AmountClear =  AmountClear - " + nClearedAmount + ", AmountBalance = " +
                         "AmountNet - AmountClear where ID = " + "'" + nSaleID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool RevertPreviousStatementBalance(string nSaleID, double nClearedAmount)
        {
            bool bRetValue = false;
            string strSql = "Update voucherstatement set AmountClear =  AmountClear - " + nClearedAmount + ", AmountBalance = " +
                         "AmountNet - AmountClear where ID = " + "'" + nSaleID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        internal bool UpdateBankReceiptVoucherForChequeReturn(string Id,string yesorno)
        {
            bool bRetValue = false;
            string strSql = "Update vouchercashbankreceipt set IFChequeReturn = '"+yesorno +"' where CBID = " + "'" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        } 

        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeletePreviosRowsByID(string Id)
        {
            bool bRetValue = false;
            string strSql = "Delete from detailcashbankreceipt where MasterID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
       
        public bool IsNameUnique(string Name, string Id)
        {
            string strSql = GetDataForUnique(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool CheckStock()
        {
            return true;

        }

        #region Query Building Functions
      
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt,string bankaccount,string bank,string branch,string chqno,string chqdate,double onaccountamount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("IfChequeReturn", 'N');
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChequeReturn(string ChequeReturnID, string ChequeReturnVouType, int ChequeReturnVouNo, string ChequeReturnVouDate, double ChequeReturnCharges, string Id,  string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherchequereturn";
            objQuery.AddToQuery("ChequeReturnID", ChequeReturnID);
            objQuery.AddToQuery("ChequeReturnVoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("ChequeReturnVoucherType", ChequeReturnVouType);
            objQuery.AddToQuery("ChequeReturnVoucherNumber", ChequeReturnVouNo);
            objQuery.AddToQuery("ChequeReturnVoucherDate", ChequeReturnVouDate);
            objQuery.AddToQuery("ChequeReturnCharges", ChequeReturnCharges);
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryChanged(string Id,string changedID, string CreditorId, string Narration, string VouType, int VouNo,
         string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "changedvouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("ChangedID", changedID);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForBankReceipt);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDeleted(string Id, string CreditorId, string Narration, string VouType, int VouNo,
        string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "deletedvouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForBankReceipt);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDetails(string Id,string detailID,  string SaleId, string RSeries, string RType, int RNumber, string RDate,
              string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, string DetailID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankreceipt";
            objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);      
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDetailsChequeReturn(string ChequeReturnID, string  Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
            string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, string DetailID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailchequereturn";
            objQuery.AddToQuery("DetailChequeReturnID", detailID);
            objQuery.AddToQuery("MasterChequeReturnID", ChequeReturnID);           
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryDetailsChanged(string Id,string changedID, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
            string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, string DetailID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "changeddetailcashbankreceipt";
            objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ChangedMasterID", changedID);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDetailsDeleted(string Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
           string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, string DetailID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "deleteddetailcashbankreceipt";
            objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }
        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate,double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("IfChequeReturn", 'N');
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }
        private string GetUpdateQueryForFifth(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id, true);
         
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate); 
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);         
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id, true); 
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion private methods
       
    }
}
