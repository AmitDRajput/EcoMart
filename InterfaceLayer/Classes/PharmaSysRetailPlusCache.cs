using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.BusinessLayer;
using EcoMart.Common;



namespace EcoMart.InterfaceLayer
{
    
    //public class EcoMartCache
    //{
        
    //    private static DataTable _productData = null;
    //    static EcoMartCache()
    //    {           
    //        FillProductData();            
    //    }
    //    public static DataTable GetProductData()
    //    {
    //        if (_productData == null)
    //            FillProductData();
    //        _productData.DefaultView.RowFilter = null;
    //        return _productData;           
    //    }

    //    public static void RefreshProductData()
    //    {
    //        ReFillProductData();
    //    }
    //    public static void RefreshProductData(int ProductID)
    //    {
    //        //ReFillProductData(ProductID);
    //    }

    //    //public static void RefreshProductData(int ProductID, OperationMode mode, int quantity)
    //    //{
    //    //    ReFillProductData(ProductID, mode, quantity);
    //    //}
    //    #region Private Methods
    //    private static void FillProductData()
    //    {
            
    //        Product prod = new Product();
    //        _productData = prod.GetOverviewDataForCache();
           
    //    }

    //    private static void ReFillProductData()
    //    {
    //        Product prod = new Product();
    //        _productData = prod.GetOverviewDataForCache();
    //    }

    //    private static void ReFillProductData(int ProductID)
    //    {
    //        try
    //        {
    //            Product prod = new Product();
    //            DataRow dRow = prod.GetOverviewDataForProductIDForCache(ProductID);
               
    //            if (dRow != null)
    //            {
    //                DataRow[] rows = _productData.Select(string.Format("ProductID='{0}'", ProductID));
    //                foreach (DataRow row in rows)
    //                {
    //                    for (int index = 0; index < _productData.Columns.Count; index++)
    //                    {                           
    //                            row[index] = dRow[index];                                
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.WriteError(ex.ToString());
    //        }
    //    }

    //    //private static void ReFillProductData(int ProductID, OperationMode mode, int quantity)
    //    //{
    //    //    try
    //    //    {
    //    //        DataRow[] rows = _productData.Select(string.Format("ProductID='{0}'", ProductID));
    //    //        foreach (DataRow row in rows)
    //    //        {
    //    //            if (mode == OperationMode.Add)
    //    //                row["ProdClosingStock"] = Convert.ToInt32(row["ProdClosingStockDatabase"].ToString()) - quantity;
    //    //            else if (mode == OperationMode.Delete)
    //    //                row["ProdClosingStock"] = Convert.ToInt32(row["ProdClosingStockDatabase"].ToString()) + quantity;

    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Log.WriteError(ex.ToString());
    //    //    }
    //    //}
    //    #endregion
    //}
}
