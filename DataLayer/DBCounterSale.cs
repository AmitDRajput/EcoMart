using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
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

        //internal DataTable GetHospitalSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType,VoucherSubType, VoucherNumber, VoucherDate, PatientShortName, PatientAddress1, PatientAddress2, AmountNet  from vouchersale WHERE  VoucherSubType = '" + FixAccounts.SubTypeForHospitalSale + "' &&  VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "' order by VoucherNumber";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
        //internal DataTable GetDebtorSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForRegularSale + "' && VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by VoucherDate Desc, VoucherNumber Desc";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
        //internal DataTable GetCreditCardSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    //string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForCreditCardSale + "' && VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by VoucherDate Desc, VoucherNumber Desc";
        //    string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  VoucherSubType = '" + FixAccounts.SubTypeForCreditCardSale + "' && VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by VoucherDate Desc, VoucherNumber Desc";
        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
        internal DataTable GetDistributorSaleOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.id, a.VoucherType, a.VoucherNumber, a.VoucherDate ,b.AccountID,b.AccName, b.AccAddress1, b.accaddress2, a.AmountNet,a.VoucherSubType,a.AccountID  from vouchersale a inner join masteraccount b on a.AccountID = b.AccountID WHERE  a.AccountID != ''  &&  a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.VoucherDate Desc, a.VoucherNumber Desc";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        internal DataTable GetDistributorSaleOverviewData(string subtype)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.id, a.VoucherType, a.VoucherNumber, a.VoucherDate ,b.AccountID,b.AccName, b.AccAddress1, b.accaddress2, a.AmountNet,a.VoucherSubType,a.AccountID  from vouchersale a inner join masteraccount b on a.AccountID = b.AccountID WHERE  a.AccountID != '' &&  a.VoucherSubType = '" + subtype + "' &&  a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.VoucherDate Desc, a.VoucherNumber Desc";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        //public  DataTable GetInstitutionalSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  AccountID != '' && VoucherSubType = '" + FixAccounts.SubTypeForInstitutionalSale + "'  && VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "' order by VoucherNumber";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
        //public DataTable GetSaleWithProductDiscountOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType, VoucherNumber, VoucherDate , PatientName as AccName, PatientAddress1 as AccAddress1, PatientAddress2, AmountNet,VoucherSubType,AccountID  from vouchersale WHERE  VoucherSubType = '" + FixAccounts.SubTypeForSaleWithProductDiscount + "' && VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by VoucherNumber";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
        //internal DataTable GetInstitutionalSaleOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select id, VoucherType,VoucherSubType, VoucherNumber, VoucherDate, PatientShortName, PatientAddress1, PatientAddress2, AmountNet  from vouchersale WHERE  VoucherSubType = '"+FixAccounts.SubTypeForInstitutionalSale +"' order by VoucherNumber";

        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}
    }
}
