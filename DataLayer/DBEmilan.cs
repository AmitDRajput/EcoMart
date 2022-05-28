using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBEmilan
    {
        public bool CopyInvoiceReceived(DataRow dr)
        {
            bool retValue = false;             
            string strSql = GetInsertQuery("",Convert.ToString(dr["SellerID"]),Convert.ToString(dr["BuyerID"]),"","","","",Convert.ToString(dr["BillNumber"]),
               Convert.ToString(dr["BillDate"]),"","","","","","","","","","","",Convert.ToString(dr["Amount"]),"","","",Convert.ToString(dr["SellerProductCode"]),
                Convert.ToString(dr["SellerProductName"]),Convert.ToString(dr["SellerProductPack"]),"","",Convert.ToString(dr["Batch"]),Convert.ToDateTime(dr["Expiry"]),
                Convert.ToString(dr["Quantity"]),Convert.ToString(dr["FreeQuantity"]),Convert.ToString(dr["AddlSchemeAmount"]),Convert.ToString(dr["Rate"]),
                "","",Convert.ToString(dr["MRP"]),"","","",Convert.ToString(dr["AddlScheme"]),Convert.ToString(dr["Discount"]),Convert.ToString(dr["AddlDiscount"]),
                Convert.ToString(dr["CST"]),"","",Convert.ToString(dr["Remarks"]),"","","","","","","","",Convert.ToString(dr["BatchID"]),Convert.ToDateTime(dr["ChallanDate"]),
                Convert.ToString(dr["ChallanNumber"]),Convert.ToString(dr["Reason"]),Convert.ToString(dr["UPC"]),Convert.ToString(dr["DiscountAmount"]),
                Convert.ToString(dr["AddlDiscountAmount"]),Convert.ToString(dr["DeductableBeforeDiscount"]),Convert.ToString(dr["MRPInclusiveTax"]),
                Convert.ToString(dr["VATApplication"]),Convert.ToString(dr["AddlTax"]),Convert.ToString(dr["SGST"]),Convert.ToString(dr["CGST"]),
                Convert.ToString(dr["IGST"]),Convert.ToString(dr["BaseSchemeQuantity"]),Convert.ToString(dr["BaseSchemeFreeQuantity"]),"",Convert.ToDateTime(dr["ReceivedDate"]));

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
           
            return retValue;
        }

        private string GetInsertQuery(string CNick, string Vendor, string CUCode, string Customer, string Area, string City, string PinCode, string InvNo,
                           string InvDate, string OrderNo, string OrderDate, string Transport, string Freight, string Paid,
                           string LRNo, string LRDate, string CreditDays, string Ad, string Ls, string Tx, string InvAmt,
                           string CNote, string MfgrNick, string Manufacturer, string PrCode, string ProductDesc, string PPack,
                           string MyType, string MyMode, string BatchNo, DateTime ExpDate, string Qty, string Free,
                           string SchQtyAdjInAmt, string Rate, string GrsAmt, string PTR, string MRP, string WPPer,
                           string OctroiPer, string SchRate, string SchPer, string CDPer, string TDPer, string CSTPer,
                           string VATPer, string INetAmt, string Remark, string LOCA, string LOCN, string KeepWatch,
                           string DivNick, string MyTypeId, string MyItemNo, string PTS, string Barcode, string BatchID,
                           DateTime ChallanDate, string ChallanNumber, string Reason, string UPC, string DiscountAmount,
                           string AddlDiscountAmount, string DeductableBeforeDiscount, string MRPInclusiveTax, string VATApplication,
                           string AddlTax, string SGST, string CGST, string IGST, string BaseSchemeQuantity,
                           string BaseSchemeFreeQuantity, string PaymentDueDate, DateTime ReceivedDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "EmilanInvoice";
            objQuery.AddToQuery("CNick", CNick);
            objQuery.AddToQuery("Vendor", Vendor);
            objQuery.AddToQuery("CUCode", CUCode);
            objQuery.AddToQuery("Customer", Customer);
            objQuery.AddToQuery("Area", Area);
            objQuery.AddToQuery("City", City);
            objQuery.AddToQuery("PinCode", PinCode);
            objQuery.AddToQuery("InvNo", InvNo);
            objQuery.AddToQuery("InvDate", InvDate);
            objQuery.AddToQuery("OrderNo", OrderNo);
         //   objQuery.AddToQuery("OrderDate", OrderDate);
            objQuery.AddToQuery("Transport", Transport);
            objQuery.AddToQuery("Freight", Freight);
            objQuery.AddToQuery("Paid", Paid);
            objQuery.AddToQuery("LRNo", LRNo);
          //  objQuery.AddToQuery("LRDate", LRDate);
            objQuery.AddToQuery("CreditDays", CreditDays);
            objQuery.AddToQuery("Ad", Ad);
            objQuery.AddToQuery("Ls", Ls);
            objQuery.AddToQuery("Tx", Tx);
            objQuery.AddToQuery("InvAmt", InvAmt);
            objQuery.AddToQuery("CNote", CNote);
            objQuery.AddToQuery("MfgrNick", MfgrNick);
            objQuery.AddToQuery("Manufacturer", Manufacturer);
            objQuery.AddToQuery("PrCode", PrCode);
            objQuery.AddToQuery("ProductDesc", ProductDesc);
            objQuery.AddToQuery("PPack", PPack);
            objQuery.AddToQuery("MyType", MyType);
            objQuery.AddToQuery("MyMode", MyMode);
            objQuery.AddToQuery("BatchNo", BatchNo);
           // objQuery.AddToQuery("ExpDate", ExpDate);
            objQuery.AddToQuery("Qty", Qty);
            objQuery.AddToQuery("Free", Free);
            objQuery.AddToQuery("SchQtyAdjInAmt", SchQtyAdjInAmt);
            objQuery.AddToQuery("Rate", Rate);
            objQuery.AddToQuery("GrsAmt", GrsAmt);
            objQuery.AddToQuery("PTR", PTR);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("WPPer", WPPer);
            objQuery.AddToQuery("OctroiPer", OctroiPer);
            objQuery.AddToQuery("SchRate", SchRate);
            objQuery.AddToQuery("SchPer", SchPer);
            objQuery.AddToQuery("CDPer", CDPer);
            objQuery.AddToQuery("TDPer", TDPer);
            objQuery.AddToQuery("CSTPer", CSTPer);
            objQuery.AddToQuery("VATPer", VATPer);
            objQuery.AddToQuery("INetAmt", INetAmt);
            objQuery.AddToQuery("Remark", Remark);
            objQuery.AddToQuery("LOCA", LOCA);
            objQuery.AddToQuery("LOCN", LOCN);
            objQuery.AddToQuery("KeepWatch", KeepWatch);
            objQuery.AddToQuery("DivNick", DivNick);
            objQuery.AddToQuery("MyTypeId", MyTypeId);
            objQuery.AddToQuery("MyItemNo", MyItemNo);
            objQuery.AddToQuery("PTS", PTS);
            objQuery.AddToQuery("Barcode", Barcode);
            objQuery.AddToQuery("BatchID", BatchID);
          //  objQuery.AddToQuery("ChallanDate", ChallanDate);
            objQuery.AddToQuery("ChallanNumber", ChallanNumber);
            objQuery.AddToQuery("Reason", Reason);
            objQuery.AddToQuery("UPC", UPC);
            objQuery.AddToQuery("DiscountAmount", DiscountAmount);
            objQuery.AddToQuery("AddlDiscountAmount", AddlDiscountAmount);
            objQuery.AddToQuery("DeductableBeforeDiscount", DeductableBeforeDiscount);
            objQuery.AddToQuery("MRPInclusiveTax", MRPInclusiveTax);
            objQuery.AddToQuery("VATApplication", VATApplication);
            objQuery.AddToQuery("AddlTax", AddlTax);
            objQuery.AddToQuery("SGST", SGST);
            objQuery.AddToQuery("CGST", CGST);
            objQuery.AddToQuery("IGST", IGST);
            objQuery.AddToQuery("BaseSchemeQuantity", BaseSchemeQuantity);
            objQuery.AddToQuery("BaseSchemeFreeQuantity", BaseSchemeFreeQuantity);
           // objQuery.AddToQuery("PaymentDueDate", PaymentDueDate);

          //  objQuery.AddToQuery("ReceivedDate", ReceivedDate);

            //objQuery.AddToQuery("CreatedUserID", createdby);
            //objQuery.AddToQuery("CreatedDate", createddate);
            //objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        public DataTable GetPurchaseOrdeerForOrderForToday(int firstNumber, int lastNumber)
        {
            DataTable dt = null;
            string strSql = "select convert(a.ordernumber , char) as ordernumber,STR_TO_DATE( date_format(orderdate, '%d/%m/%y'),'%d/%m/%Y') as date,'RTTT00055' as supplierID,b.AIOCDACode as SupplierProductCode," +
                " '' as UPC,b.ProductID as BuyerProductCode,b.ProdName as BuyerProductName,b.ProdPack as BuyerProductPack,"+
                " a.OrderQuantity as Quantity, a.SchemeQuantity as FreeQuantity, a.Narration as Remarks,a.AccountID,"+
                " a.DSLID,a.MasterID,c.AccName,d.Amount  from tbldailyshortlist a inner join masterproduct b on a.ProductID = b.ProductID inner join masteraccount c on a.AccountID = c.AccountID inner join masterorder d on a.MasterID = d.ID where ordernumber >= "+ firstNumber +" && ordernumber <= "+ lastNumber +"  order by a.ordernumber"; //&& AIOCDACode is not null && AIOCDACode != '' order by ordernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOrderDetails(string masterid)
        {
            DataTable dt = null;
            string strSql = "select convert(a.ordernumber , char) as ordernumber,STR_TO_DATE( date_format(orderdate, '%d/%m/%y'),'%d/%m/%Y') as date,'RTTT00055' as supplierID,'' as SupplierProductCode," +
                " b.AIOCDACode as UPC,'' as BuyerProductCode,b.ProdName as BuyerProductName,b.ProdPack as BuyerProductPack," +
                " a.OrderQuantity as Quantity, a.SchemeQuantity as FreeQuantity, a.Narration as Remarks  from tbldailyshortlist a inner join masterproduct b on a.ProductID = b.ProductID inner join masteraccount c on a.AccountID = c.AccountID  where masterID = '"+ masterid+"'"; //&& AIOCDACode is not null && AIOCDACode != '' order by ordernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetPurchaseBillsForGrid()
        {
            DataTable dt = null;
            string strSql = "select a.InvNo,a.InvDate,a.Vendor,a.ChallanNumber,a.InvAmt,a.IfBillingDone,a.VoucherNumber,a.VoucherDate,a.BatchID,b.accname from emilaninvoice a inner join masteraccount b on a.vendor = b.AIOCDACode where ( vouchernumber is null || vouchernumber = 0) group by invNo, challanNumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetPurchaseBillsForGrid(string vendorID, string batchID, string invno, string challanno)
        {
            DataTable dt = null;
            string strSql = "select a.CNick,a.Vendor,a.CUCode,a.Customer,a.Area,a.City,a.PinCode,a.InvNo,a.InvDate,a.OrderNo,a.OrderDate,a.Transport,"+
                             "a.Freight,a.Paid,a.LRNo,a.LRDate,a.CreditDays,a.Ad,a.Ls,a.Tx,a.InvAmt,a.CNote,a.MfgrNick,a.Manufacturer,a.PrCode,a.ProductDesc,"+
                             "a.PPack,a.MyType,a.MyMode,a.BatchNo,a.ExpDate,a.Qty,a.Free,a.SchQtyAdjInAmt,a.Rate,a.GrsAmt,a.PTR,a.MRP,a.WPPer,a.OctroiPer,a.SchRate,"+
                             "a.SchPer,a.CDPer,a.TDPer,a.CSTPer,a.VATPer,a.INetAmt,a.Remark,a.LOCA,a.LOCN,a.KeepWatch,a.DivNick,a.MyTypeId,a.MyItemNo,a.PTS,"+
                             "a.Barcode,a.BatchID,a.ChallanDate,a.ChallanNumber,a.Reason,a.UPC,a.DiscountAmount,a.AddlDiscountAmount,a.DeductableBeforeDiscount,a.MRPInclusiveTax," +
                             "a.VATApplication,a.AddlTax,a.SGST,a.CGST,a.IGST,a.BaseSchemeQuantity,a.BaseSchemeFreeQuantity,a.PaymentDueDate,a.ReceivedDate " +
                             "from emilaninvoice a inner join masteraccount b on a.vendor = b.AIOCDACode where Vendor = '" + vendorID + "' && BatchID = '"+ batchID +"' && Invno = '"+ invno +"' && ChallanNumber = '"+ challanno +"'";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
    }
}
