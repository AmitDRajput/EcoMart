using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBCounterSale
    {
        public DBCounterSale()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate, PatientName, PatientAddress1,PatientAddress2, DoctorName, AmountNet from voucherSale WHERE VoucherType = '"+ FixAccounts.VoucherTypeForVoucherSale +"' order by VoucherNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        internal DataTable GetRegularSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate, PatientName, PatientAddress1, PatientAddress2, AmountNet  from vouchersale WHERE VoucherType <> '" + FixAccounts.VoucherTypeForVoucherSale + "' order by VoucherNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        internal DataTable GetHospitalSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType,VoucherSubType, VoucherNumber, VoucherDate, PatientShortName, PatientAddress1, PatientAddress2, AmountNet  from vouchersale WHERE  VoucherSubType = '"+FixAccounts.SubTypeForHospitalSale+"' order by VoucherNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        internal DataTable GetDebtorSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForDebtorSale + "'  order by VoucherDate Desc, VoucherNumber Desc";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        internal DataTable GetDistributorSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForDistributorSale + "'  order by VoucherDate Desc, VoucherNumber Desc";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public  DataTable GetInstitutionalSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForInstitutionalSale + "'  order by VoucherNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        //internal DataTable GetInstitutionalSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType,VoucherSubType, VoucherNumber, VoucherDate, PatientShortName, PatientAddress1, PatientAddress2, AmountNet  from vouchersale WHERE  VoucherSubType = '"+FixAccounts.SubTypeForInstitutionalSale +"' order by VoucherNumber";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
    }
}
