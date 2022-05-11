using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
//using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolEmilanDownLoadPurchase : BaseControl
    {
        //String DeveloperId = "SGC01", UserId = "RTTT00054", Password = "RTTT00054";
        public UclToolEmilanDownLoadPurchase()
        {
            InitializeComponent();
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            return retValue;
        }
        public override bool ClearData()
        {
            ClearControls();
            return base.ClearData();
        }

        private void ClearControls()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonDownloadClick();
        }

        private void ButtonDownloadClick()
        {
            //try
            //{
            //    Invoices invoices = new Invoices();
            //    DataTable dtInvoices = new DataTable("InvoiceItems");
            //    //invoices.InvoicesFromUserBulk(DeveloperId, UserId, Password, dtInvoices);

            //    //Download fresh Invoices
            //    DataTable invoicesReceived = invoices.InvoicesToUser(DeveloperId, UserId, Password);
               
            //    //Once download Success, you need to Confirm download, Confirmed invoices will be freezed at server

            //    //#region Create .CSV File
            //    //string Name = UserId;
            //    //string Date = DateTime.Now.ToLongDateString();
            //    //string filepath = "E:\\EmilanDownload\\" + Name + "_" + Date + "_Invoice.csv";

            //    ////var parser = new TextFieldParser(filepath);
            //    ////parser.SetDelimiters("|");

            //    ////invoices.InvoicesFromUserCSV(DeveloperId, UserId, Password, filepath);

            //    //bool Save = false;

            //    //if (invoicesReceived != null)
            //    //{
            //    //    StreamWriter sw = new StreamWriter(filepath);
            //    //    //headers  
            //    //    for (int i = 0; i < invoicesReceived.Columns.Count; i++)
            //    //    {
            //    //        switch (Convert.ToString(invoicesReceived.Columns[i]))
            //    //        {
            //    //            case "SellerID": sw.Write("Vendor");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "BuyerID": sw.Write("CUCode");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "BillDate": sw.Write("InvDate");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "BillNumber": sw.Write("InvNo");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Batch": sw.Write("BatchNo");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Remarks": sw.Write("Remarks");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Expiry": sw.Write("ExpDate");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Quantity": sw.Write("Qty");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "SellerProductName": sw.Write("ProductDesc");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "SellerProductPack": sw.Write("PPack");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Rate": sw.Write("Rate");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Amount": sw.Write("InvAmt");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "SellerProductCode": sw.Write("PrCode");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "VATP": sw.Write("VATPer");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "AddlSchemeAmount": sw.Write("SchQtyAdjInAmt");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "FreeQuantity": sw.Write("Free");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "Discount": sw.Write("CDPer");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "AddlScheme": sw.Write("schPer");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "AddlDiscount": sw.Write("TDPer");
            //    //                sw.Write(",");
            //    //                break;
            //    //            case "CST": sw.Write("CSTPer");
            //    //                sw.Write(",");
            //    //                break;

            //    //            default: sw.Write(invoicesReceived.Columns[i]);
            //    //                sw.Write(",");
            //    //                break;
            //    //        }
            //    //    }
            //    //    sw.Write("CNick,	Customer,	Area,	City,	PinCode,	OrderNo,	OrderDate,	Transport,	Freight,	Paid,	LRNo,	LRDate,	CreditDays,	Ad,	Ls,	Tx,	InvAmt,	CNote,	MfgrNick,	Manufacturer,	MyType,	MyMode,	GrsAmt,	PTR	,MRP,	WPPer,	OctroiPer,	INetAmt,	LOCA,	LOCN,	KeepWatch,	DivNick	MyTypeId,	MyItemNo,	PTS	Barcode");
            //    //    sw.Write(sw.NewLine);
            //    //    foreach (DataRow dr in invoicesReceived.Rows)
            //    //    {
            //    //        for (int i = 0; i < invoicesReceived.Columns.Count; i++)
            //    //        {
            //    //            if (!Convert.IsDBNull(dr[i]))
            //    //            {
            //    //                string value = dr[i].ToString();
            //    //                if (value.Contains(','))
            //    //                {
            //    //                    value = String.Format("\"{0}\"", value);
            //    //                    sw.Write(value);
            //    //                }
            //    //                else
            //    //                {
            //    //                    sw.Write(dr[i].ToString());
            //    //                }
            //    //            }
            //    //            if (i < invoicesReceived.Columns.Count - 1)
            //    //            {
            //    //                sw.Write(",");
            //    //            }
            //    //        }
            //    //        sw.Write(sw.NewLine);
            //    //    }
            //    //    sw.Close();
            //    //    Save = true;
            //    //}
            //    //#endregion

            //    #region If Save= True
            //    if (true)
            //    {
            //        //            string strSql  = "INSERT INTO invoice" +
            //        //"(`CNick`, `Vendor`, `CUCode`, `Customer`, `Area`, `City`, `PinCode`, `InvNo`, `InvDate`, `OrderNo`, `OrderDate`, `Transport`, `Freight`, `Paid`, `LRNo`, `LRDate`, `CreditDays`, `Ad`, `Ls`, `Tx`, `InvAmt`, `CNote`, `MfgrNick`, `Manufacturer`, `PrCode`, `ProductDesc`, `PPack`, `MyType`, `MyMode`, `BatchNo`, `ExpDate`, `Qty`, `Free`, `SchQtyAdjInAmt`, `Rate`, `GrsAmt`, `PTR`, `MRP`, `WPPer`, `OctroiPer`, `SchRate`, `SchPer`, `CDPer`, `TDPer`, `CSTPer`, `VATPer`, `INetAmt`, `Remark`, `LOCA`, `LOCN`, `KeepWatch`, `DivNick`, `MyTypeId`, `MyItemNo`, `PTS`, `Barcode`, `BatchID`, `ChallanDate`, `ChallanNumber`, `Reason`, `UPC`, `DiscountAmount`, `AddlDiscountAmount`, `DeductableBeforeDiscount`, `MRPInclusiveTax`, `VATApplication`, `AddlTax`, `SGST`, `CGST`, `IGST`, `BaseSchemeQuantity`, `BaseSchemeFreeQuantity`, `PaymentDueDate`, `ReceivedDate`)" +
            //        //"VALUES (@CNick,@Vendor,@CUCode,@Customer,@Area,@City,@PinCode,@InvNo,@InvDate,@OrderNo,@OrderDate,@Transport,@Freight,@Paid,@LRNo,@LRDate,@CreditDays,@Ad,@Ls,@Tx,@InvAmt,@CNote,@MfgrNick,@Manufacturer,@PrCode,@ProductDesc,@PPack,@MyType,@MyMode,@BatchNo,@ExpDate,@Qty,@Free,@SchQtyAdjInAmt,@Rate,@GrsAmt,@PTR,@MRP,@WPPer,@OctroiPer,@SchRate,@SchPer,@CDPer,@TDPer,@CSTPer,@VATPer,@INetAmt,@Remark,@LOCA,@LOCN,@KeepWatch,@DivNick,@MyTypeId,@MyItemNo,@PTS,@Barcode,@BatchID,@ChallanDate,@ChallanNumber,@Reason,@UPC,@DiscountAmount,@AddlDiscountAmount,@DeductableBeforeDiscount,@MRPInclusiveTax,@VATApplication,@AddlTax,@SGST,@CGST,@IGST,@BaseSchemeQuantity,@BaseSchemeFreeQuantity,@PaymentDueDate,@ReceivedDate)";
            //        //            //MySqlCommand cmd = new MySqlCommand(commandText, con);
            //        //Conn.Open();
            //        Emilan _emilan = new Emilan();
            //        bool retvalue = _emilan.CopyInvoicesReceived(invoicesReceived);
            //        foreach (DataRow dr in invoicesReceived.Rows)
            //        {

            //            //cmd.Parameters.AddWithValue("@CNick", null);
            //            //cmd.Parameters.AddWithValue("@Vendor", Convert.ToString(dr["SellerID"]));
            //            //cmd.Parameters.AddWithValue("@CUCode", Convert.ToString(dr["BuyerID"]));
            //            //cmd.Parameters.AddWithValue("@Customer", null);
            //            //cmd.Parameters.AddWithValue("@Area", null);
            //            //cmd.Parameters.AddWithValue("@City", null);
            //            //cmd.Parameters.AddWithValue("@PinCode", null);
            //            //cmd.Parameters.AddWithValue("@InvNo", Convert.ToString(dr["BillNumber"]));
            //            //cmd.Parameters.AddWithValue("@InvDate", Convert.ToDateTime(dr["BillDate"]));
            //            //cmd.Parameters.AddWithValue("@OrderNo", null);
            //            //cmd.Parameters.AddWithValue("@OrderDate", null);
            //            //cmd.Parameters.AddWithValue("@Transport", null);
            //            //cmd.Parameters.AddWithValue("@Freight", null);
            //            //cmd.Parameters.AddWithValue("@Paid", null);
            //            //cmd.Parameters.AddWithValue("@LRNo", null);
            //            //cmd.Parameters.AddWithValue("@LRDate", null);
            //            //cmd.Parameters.AddWithValue("@CreditDays", null);
            //            //cmd.Parameters.AddWithValue("@Ad", null);
            //            //cmd.Parameters.AddWithValue("@Ls", null);
            //            //cmd.Parameters.AddWithValue("@Tx", null);
            //            //cmd.Parameters.AddWithValue("@InvAmt", Convert.ToString(dr["Amount"]));
            //            //cmd.Parameters.AddWithValue("@CNote", null);
            //            //cmd.Parameters.AddWithValue("@MfgrNick", null);
            //            //cmd.Parameters.AddWithValue("@Manufacturer", null);
            //            //cmd.Parameters.AddWithValue("@PrCode", Convert.ToString(dr["SellerProductCode"]));
            //            //cmd.Parameters.AddWithValue("@ProductDesc", Convert.ToString(dr["SellerProductName"]));
            //            //cmd.Parameters.AddWithValue("@PPack", Convert.ToString(dr["SellerProductPack"]));
            //            //cmd.Parameters.AddWithValue("@MyType", null);
            //            //cmd.Parameters.AddWithValue("@MyMode", null);
            //            //cmd.Parameters.AddWithValue("@BatchNo", Convert.ToString(dr["Batch"]));
            //            //cmd.Parameters.AddWithValue("@ExpDate", Convert.ToDateTime(dr["Expiry"]));
            //            //cmd.Parameters.AddWithValue("@Qty", Convert.ToString(dr["Quantity"]));
            //            //cmd.Parameters.AddWithValue("@Free", Convert.ToString(dr["FreeQuantity"]));
            //            //cmd.Parameters.AddWithValue("@SchQtyAdjInAmt", Convert.ToString(dr["AddlSchemeAmount"]));
            //            //cmd.Parameters.AddWithValue("@Rate", Convert.ToString(dr["Rate"]));
            //            //cmd.Parameters.AddWithValue("@GrsAmt", null);
            //            //cmd.Parameters.AddWithValue("@PTR", null);
            //            //cmd.Parameters.AddWithValue("@MRP", Convert.ToString(dr["MRP"]));
            //            //cmd.Parameters.AddWithValue("@WPPer", null);
            //            //cmd.Parameters.AddWithValue("@OctroiPer", null);
            //            //cmd.Parameters.AddWithValue("@SchRate", null);
            //            //cmd.Parameters.AddWithValue("@SchPer", Convert.ToString(dr["AddlScheme"]));
            //            //cmd.Parameters.AddWithValue("@CDPer", Convert.ToString(dr["Discount"]));
            //            //cmd.Parameters.AddWithValue("@TDPer", Convert.ToString(dr["AddlDiscount"]));
            //            //cmd.Parameters.AddWithValue("@CSTPer", Convert.ToString(dr["CST"]));
            //            //cmd.Parameters.AddWithValue("@VATPer", null);
            //            //cmd.Parameters.AddWithValue("@INetAmt", null);
            //            //cmd.Parameters.AddWithValue("@Remark", Convert.ToString(dr["Remarks"]));
            //            //cmd.Parameters.AddWithValue("@LOCA", null);
            //            //cmd.Parameters.AddWithValue("@LOCN", null);
            //            //cmd.Parameters.AddWithValue("@KeepWatch", null);
            //            //cmd.Parameters.AddWithValue("@DivNick", null);
            //            //cmd.Parameters.AddWithValue("@MyTypeId", null);
            //            //cmd.Parameters.AddWithValue("@MyItemNo", null);
            //            //cmd.Parameters.AddWithValue("@PTS", null);
            //            //cmd.Parameters.AddWithValue("@Barcode", null);
            //            //cmd.Parameters.AddWithValue("@BatchID", Convert.ToString(dr["BatchID"]));
            //            //cmd.Parameters.AddWithValue("@ChallanDate", Convert.ToDateTime(dr["ChallanDate"]));
            //            //cmd.Parameters.AddWithValue("@ChallanNumber", Convert.ToString(dr["ChallanNumber"]));
            //            //cmd.Parameters.AddWithValue("@Reason", Convert.ToString(dr["Reason"]));
            //            //cmd.Parameters.AddWithValue("@UPC", Convert.ToString(dr["UPC"]));
            //            //cmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToString(dr["DiscountAmount"]));
            //            //cmd.Parameters.AddWithValue("@AddlDiscountAmount", Convert.ToString(dr["AddlDiscountAmount"]));
            //            //cmd.Parameters.AddWithValue("@DeductableBeforeDiscount", Convert.ToString(dr["DeductableBeforeDiscount"]));
            //            //cmd.Parameters.AddWithValue("@MRPInclusiveTax", Convert.ToString(dr["MRPInclusiveTax"]));
            //            //cmd.Parameters.AddWithValue("@VATApplication", Convert.ToString(dr["VATApplication"]));
            //            //cmd.Parameters.AddWithValue("@AddlTax", Convert.ToString(dr["AddlTax"]));
            //            //cmd.Parameters.AddWithValue("@SGST", Convert.ToString(dr["SGST"]));
            //            //cmd.Parameters.AddWithValue("@CGST", Convert.ToString(dr["CGST"]));
            //            //cmd.Parameters.AddWithValue("@IGST", Convert.ToString(dr["IGST"]));
            //            //cmd.Parameters.AddWithValue("@BaseSchemeQuantity", Convert.ToString(dr["BaseSchemeQuantity"]));
            //            //cmd.Parameters.AddWithValue("@BaseSchemeFreeQuantity", Convert.ToString(dr["BaseSchemeFreeQuantity"]));
            //            //cmd.Parameters.AddWithValue("@PaymentDueDate", null);
            //            //cmd.Parameters.AddWithValue("@ReceivedDate", Convert.ToDateTime(dr["ReceivedDate"]));
            //            //cmd.ExecuteNonQuery();
            //            //// no need to clear, reuse the same set of parameters
            //            //cmd.Parameters.Clear();
            //        }
            //        //Conn.Close();
            //    }
            //    #endregion

            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }

        private void ConstructdgBillsColumns()
        {
            dgBills.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgBills.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_InvoiceNumber";
                //  column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Invoice Number";
                column.Width = 50;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_InvoiceDate";
                //  column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Date";
                column.Width = 90;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Party";
                // column.DataPropertyName = "Narration";
                column.HeaderText = "Party";
                column.Width = 220;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                //   column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBills.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                //   column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                //    column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 80;
                dgBills.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                //    column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narration";
                // column.DataPropertyName = "Narration";
                column.HeaderText = "Remark";
                column.Width = 200;
                column.ReadOnly = false;
                dgBills.Columns.Add(column);
                //5
               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void UclToolEmilanDownLoadPurchase_Load(object sender, EventArgs e)
        {
            ConstructdgBillsColumns();
            ShowBills();
        }

        private void ShowBills()
        {
            DataTable dt = new DataTable();
            Emilan em = new Emilan();
            dt = em.GetPurchaseBillsForGrid();
            if (dt != null && dt.Rows.Count > 0)
                FillGridWithExistingRows(dt);
        }

        private void FillGridWithExistingRows(DataTable mdt)
        {
            DataTable dt = mdt;
            string minvno = "";
            int currow = 0;
            string mdate = "";
            string accnm = "";
            double mamt = 0;
            string mifbillingdone = "";
            int mvouno = 0;
            foreach (DataRow dr in mdt.Rows)
            {
                mdate = "";
                accnm = "";
                mamt = 0;
                mifbillingdone = "";
                mvouno = 0;
                currow = dgBills.Rows.Add();
                if (dr["InvNo"] != DBNull.Value)
                    minvno = dr["InvNo"].ToString();
                if (dr["InvDate"] != DBNull.Value)
                    mdate = dr["InvDate"].ToString();
                DateTime dd = Convert.ToDateTime(mdate);
                mdate = dd.Date.ToString("yyyyMMdd");
                mdate = General.GetDateInShortDateFormat(mdate);
                if (dr["accname"] != DBNull.Value)
                    accnm = dr["AccName"].ToString();
                if (dr["InvAmt"] != DBNull.Value)
                    mamt = Convert.ToDouble(dr["InvAmt"].ToString());
                if (dr["IfBillingDone"] != DBNull.Value)
                    mifbillingdone = dr["IfBillingDone"].ToString();
                if (dr["VoucherNumber"] != DBNull.Value)
                    mvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());

                dgBills.Rows[currow].Cells["Col_InvoiceNumber"].Value = minvno;
                dgBills.Rows[currow].Cells["Col_InvoiceDate"].Value = mdate;
                dgBills.Rows[currow].Cells["Col_Party"].Value = accnm;
                dgBills.Rows[currow].Cells["Col_AmountNet"].Value = mamt.ToString("#0.00");
                dgBills.Rows[currow].Cells["Col_Narration"].Value = mifbillingdone;
                if (mvouno > 0)
                    dgBills.Rows[currow].ReadOnly = true;
                

            }
        }

        private void dgBills_KeyDown(object sender, KeyEventArgs e)
        {
            //int invno = 0;
            //int challanno = 0;
            //string batchID = "";
            //string vendor = "";
            // Emilan em = new Emilan();
             //if (e.KeyCode == Keys.Enter)
             ////{
             ////    if (dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value != null && dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
             //        ifchecked = dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString();
             //    if (ifchecked != string.Empty)
             //        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = string.Empty;
             //    else
             //        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
             //}        
             //DataTable dt = em.GetDataForSelectedRow(batchID, vendor, invno, challanno);
            //FillPurchaseGrid(dt);

        }
     
        private void FillPurchaseGrid(DataTable dt)
        {
            throw new NotImplementedException();
        }
    }
}
