using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MitraPlus.DataLayer
{
    class DBSale
    {
        //public bool Save(string SaleId, string VoucherSeries, string VoucherType, long VoucherNumber, long CounterSaleNumber,
        //    string VoucherDate, string VoucherSubType, string AccountId, double AmountNet, double AmountClear, double AmountGross,
        //    double CashDiscountPercent, double AmountSpecialDiscount, double AmountCashDiscount, double AddOnFreight,
        //    double OctroiPercentage, double AmountOctroi, string Narration, int StatementNumber, string DoctorId, string DoctorName,
        //    string PatientId, string PatientName, string PatientAddress1, string PatientAddress2, Int16 IPDOPDCode)
        //{
        //    string sql = GetInsertQuery(SaleId, VoucherSeries, VoucherType, VoucherNumber, CounterSaleNumber, VoucherDate,
        //        VoucherSubType, AccountId, AmountNet, AmountClear, AmountGross, CashDiscountPercent, AmountSpecialDiscount, AmountCashDiscount, AddOnFreight, OctroiPercentage,
        //        AmountOctroi, Narration, StatementNumber, DoctorId, DoctorName, PatientId, PatientName, PatientAddress1,PatientAddress2, IPDOPDCode);

        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(sql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        
        //#region "Query Builder"
        //private string GetInsertQuery(string SaleId, string VoucherSeries, string VoucherType, long VoucherNumber, long CounterSaleNumber,
        //    string VoucherDate, string VoucherSubType, string AccountId, double AmountNet, double AmountClear, double AmountGross,
        //    double CashDiscountPercent, double AmountSpecialDiscount, double AmountCashDiscount, double AddOnFreight,
        //    double OctroiPercentage, double AmountOctroi, string Narration, int StatementNumber, string DoctorId, string DoctorName,
        //    string PatientId, string PatientName, string PatientAddress1, string PatientAddress2, Int16 IPDOPDCode)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "vouchersale";
        //    objQuery.AddToQuery("Id", SaleId);
        //    objQuery.AddToQuery("VoucherSeries", VoucherSeries);
        //    objQuery.AddToQuery("VoucherType", VoucherType);
        //    objQuery.AddToQuery("VoucherNumber", VoucherNumber);
        //    objQuery.AddToQuery("CounterSaleNumber", CounterSaleNumber);
        //    objQuery.AddToQuery("VoucherDate", VoucherDate);
        //    objQuery.AddToQuery("VoucherSubType", VoucherSubType);
        //    objQuery.AddToQuery("AccountId", AccountId);
        //    objQuery.AddToQuery("AmountNet", AmountNet);
        //    objQuery.AddToQuery("AmountClear", AmountClear);
        //    objQuery.AddToQuery("AmountGross", AmountGross);
        //    objQuery.AddToQuery("CashDiscountPercent", CashDiscountPercent);
        //    objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
        //    objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
        //    objQuery.AddToQuery("AddOnFreight", AddOnFreight);
        //    objQuery.AddToQuery("AmountOctroi", AmountOctroi);
        //    objQuery.AddToQuery("Narration", Narration);
        //    objQuery.AddToQuery("StatementNumber", StatementNumber);
        //    objQuery.AddToQuery("DoctorId", DoctorId);
        //    objQuery.AddToQuery("DoctorName", DoctorName);
        //    objQuery.AddToQuery("PatientId", PatientId);
        //    objQuery.AddToQuery("PatientName", PatientName);
        //    objQuery.AddToQuery("PatientAddress1", PatientAddress1);
        //    objQuery.AddToQuery("PatientAddress2", PatientAddress2);
        //    objQuery.AddToQuery("IPDOPDCode", IPDOPDCode);
        //    return objQuery.InsertQuery();
        //}

        //#endregion



        //public DataRow ReadDetailById(string Id)
        //{
        //    string strSql = string.Format("SELECT Id, VoucherSeries, VoucherType, VoucherNumber, CounterSaleNumber, str_to_date(VoucherDate,'%y%c%d') AS 'VoucherDate'," +
        //        "VoucherSubType, AccountId, AmountNet, AmountClear, AmountGross, CashDiscountPercent, AmountSpecialDiscount, AmountCashDiscount, AddOnFreight, OctroiPercentage," +
        //        "AmountOctroi, Narration, StatementNumber, DoctorId, DoctorName, PatientId, PatientName, PatientAddress1, PatientAddress2, IPDOPDCode FROM vouchersale WHERE ID = '{0}'", Id);
        //    return DBInterface.SelectFirstRow(strSql);
        //}

        //public bool Update(string SaleId, string VoucherSeries, string VoucherType, long VoucherNumber, long CounterSaleNumber, string VoucherDate, string VoucherSubType, string AccountId, double AmountNet, double AmountClear, double AmountGross, double CashDiscountPercent, double AmountSpecialDiscount, double AmountCashDiscount, double AddOnFreight, double OctroiPercentage, double AmountOctroi, string Narration, int StatementNumber, string DoctorId, string DoctorName, string PatientId, string PatientName, string PatientAddress1, string PatientAddress2, short IPDOPDCode)
        //{
        //    string sql = GetUpdateQuery(SaleId, VoucherSeries, VoucherType, VoucherNumber, CounterSaleNumber, VoucherDate,
        //        VoucherSubType, AccountId, AmountNet, AmountClear, AmountGross, CashDiscountPercent, AmountSpecialDiscount, AmountCashDiscount, AddOnFreight, OctroiPercentage,
        //        AmountOctroi, Narration, StatementNumber, DoctorId, DoctorName, PatientId, PatientName, PatientAddress1, PatientAddress2, IPDOPDCode);
        //    sql += " ID='" + SaleId + "'";
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(sql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        //// while delete area in master-2 

        //public DataTable GetOverviewDataSelect(string field,string fieldId)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("Select *  from vouchersale where "+field+" =  '{0}'", fieldId);

        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}

        //private string GetUpdateQuery(string SaleId, string VoucherSeries, string VoucherType, long VoucherNumber, long CounterSaleNumber, string VoucherDate, string VoucherSubType, string AccountId, double AmountNet, double AmountClear, double AmountGross, double CashDiscountPercent, double AmountSpecialDiscount, double AmountCashDiscount, double AddOnFreight, double OctroiPercentage, double AmountOctroi, string Narration, int StatementNumber, string DoctorId, string DoctorName, string PatientId, string PatientName, string PatientAddress1, string PatientAddress2, short IPDOPDCode)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "vouchersale";
        //    objQuery.AddToQuery("Id", SaleId);
        //    objQuery.AddToQuery("VoucherSeries", VoucherSeries);
        //    objQuery.AddToQuery("VoucherType", VoucherType);
        //    objQuery.AddToQuery("VoucherNumber", VoucherNumber);
        //    objQuery.AddToQuery("CounterSaleNumber", CounterSaleNumber);
        //    objQuery.AddToQuery("VoucherDate", VoucherDate);
        //    objQuery.AddToQuery("VoucherSubType", VoucherSubType);
        //    objQuery.AddToQuery("AccountId", AccountId);
        //    objQuery.AddToQuery("AmountNet", AmountNet);
        //    objQuery.AddToQuery("AmountClear", AmountClear);
        //    objQuery.AddToQuery("AmountGross", AmountGross);
        //    objQuery.AddToQuery("CashDiscountPercent", CashDiscountPercent);
        // //   objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
        //    objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
        //    objQuery.AddToQuery("AddOnFreight", AddOnFreight);
        //    objQuery.AddToQuery("AmountOctroi", AmountOctroi);
        //    objQuery.AddToQuery("Narration", Narration);
        //    objQuery.AddToQuery("StatementNumber", StatementNumber);
        //    objQuery.AddToQuery("DoctorId", DoctorId);
        //    objQuery.AddToQuery("DoctorName", DoctorName);
        //    objQuery.AddToQuery("PatientId", PatientId);
        //    objQuery.AddToQuery("PatientName", PatientName);
        //    objQuery.AddToQuery("PatientAddress1", PatientAddress1);
        //    objQuery.AddToQuery("PatientAddress2", PatientAddress2);
        //    objQuery.AddToQuery("IPDOPDCode", IPDOPDCode);
        //    return objQuery.UpdateQuery();
        //}

        



       

        
    }
}
