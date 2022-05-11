using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;
using EcoMart.Common;


namespace EcoMart.DataLayer
{
    public class DBBarCode
    {
        public DBBarCode()
        {
        }
        public DataTable GetProductData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from MasterProduct order by ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetBatchData(string iD)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from tblStock where productID = '"+ iD +"' order by Expiry";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow GettblVoucherNumberRow()
        {
            DataRow dr;
            string strSql = "Select * from tblstock where ScanCode is null || Scancode = '' ";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public bool FillBarCodeNumbers(string productID, string productNumber)
        {
            bool retValue = false;
            string strSql = "update masterproduct set  ProductNumberForBarcode = '" + productNumber + "' where productID = '" + productID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }

        public DataRow GetLastProductNumberForBarCode()
        {
            DataRow dr;
            string strSql = "Select ProductNumberForBarcode from masterproduct order by  ProductNumberForBarcode desc";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataTable GetProductsFromMasterProduct()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select productID,ProductNumberForBarcode from masterproduct where ProductNumberForBarcode is null || ProductNumberForBarcode = ''";
            dtable = DBInterface.SelectDataTable(strSql);
            int cc = dtable.Rows.Count;
            return dtable;
        }

        public DataTable GetRowsFromtblStock(string mproductid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select stockID,productID,scancode from tblstock where productID = '" + mproductid + "' order  by scancode desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool FillBarcodeIntblStock(string mstockid, string mbarcode)
        {
            bool retValue = false;
            string strSql = "update tblStock set scancode = '" + mbarcode + "' where stockID = '" + mstockid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }

        public bool DeletedRowsFromtblBarCode()
        {
            bool retValue = false;
            string strSql = "delete from tblBarCode";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }

        internal DataTable GetPurchaseData(string mvoutype, int mvouno)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.productID,a.prodName,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName,a.ProdIfBarCodeRequired,b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.SaleRate,b.Expiry,b.ExpiryDate,(b.Quantity+b.SchemeQuantity) as Quantity,c.StockID,c.ScanCode,d.PurchaseID,d.VoucherType,d.VoucherNumber,b.SerialNumber from voucherpurchase d inner join detailpurchase b on d.PurchaseID = b.PurchaseID  inner join masterproduct a on  b.ProductID = a.ProductID inner join tblstock c on b.stockID = c.StockID  where d.VoucherNumber = " + mvouno + "  && d.VoucherType = '" + mvoutype + "' order by b.SerialNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetStockByProductID(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select StockID,ProductId,BatchNumber,MRP,Expiry,ExpiryDate,TradeRate,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ScanCode,DistributorSaleRate,ScanCode from tblstock where ProductID  = '{0}' and ClosingStock <> 0", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        public DataTable GetGivenProductData(string mstockID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.productID,a.prodName,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName,a.ProdIfBarCodeRequired,b.StockID,'' as PurchaseID,b.BatchNumber,b.MRP,b.SaleRate,b.Expiry,b.ExpiryDate,b.ClosingStock as Quantity,b.ScanCode from tblstock b inner join masterproduct a on b.ProductID = a.ProductID  where b.StockID = '" + mstockID + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetShelfwiseData(string mshelf)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.productID,a.prodName,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName,a.ProdIfBarCodeRequired,b.StockID, '' as PurchaseID,b.BatchNumber,b.MRP,b.SaleRate,b.Expiry,b.ExpiryDate,(b.ClosingStock/a.ProdLoosePack) as Quantity,b.ScanCode from tblstock b inner join masterproduct a on b.ProductID = a.ProductID   where  a.prodshelfID = '" + mshelf + "' && (b.ClosingStock/a.ProdLoosePack) >= 1";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
