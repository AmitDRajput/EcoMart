using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using EcoMart.Common;

namespace EcoMart.Printing
{
    public class BarCodePrinter
    {
        private static List<BarCodeData> BarCodeDataList;
        private static int PageHeight = 96;
        private static int PageWidth = 144;

        private static int LabelHeight = 20;
        private static int LabelWidth = 100;
      //private static int LabelGap = 2;

        private static int RowsPerPage;         // Number of Rows per Page
        private static int TotalPages;          // Total Pages
        private static int CurrentPageNo;          // Current Page
        private static int CurrentRowIndex;          // Current Row

        private static SolidBrush blackBrush = new SolidBrush(Color.Black);
        private static Pen whitePen = new Pen(Brushes.White);
        
        private static Font ShopNameFont = new Font("Arial", 7);
        private static Font ProductNameFont = new Font("Arial", 7);
        private static Font ProductPackFont = new Font("Arial", 6);
        private static Font MRPFont = new Font("Arial", 6);
        private static Font ExpiryFont = new Font("Arial", 6);
        private static Font BatchFont = new Font("Arial", 6);
        private static Font ScanCodeFont = new Font("Arial", 6);

        private static BarcodeLib.Barcode b;

        public static void Print(List<BarCodeData> barCodeData)
        {
            try
            {
                BarCodeDataList = new List<BarCodeData>();
                BarCodeDataList = barCodeData;

                System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
                PrintDialog myPrinDialog1 = new PrintDialog();
                myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument2_PrintPage);
                myPrintDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(myPrintDocument1_BeginPrint);


                System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("BarCode", PageWidth, PageHeight);
                ps.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;
                ps.Height = PageHeight;
                ps.Width = PageWidth;
                myPrintDocument1.DefaultPageSettings.PaperSize = ps;
                myPrintDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);


                myPrinDialog1.Document = myPrintDocument1;
                myPrinDialog1.UseEXDialog = true;
                DialogResult dr = myPrinDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    myPrintDocument1.Print();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private static void myPrintDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RowsPerPage = 1;
            CurrentRowIndex = 0;
            CurrentPageNo = 0;
            TotalPages = (int)Math.Ceiling((double)BarCodeDataList.Count / (double)RowsPerPage);

        }
        private static void myPrintDocument2_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            CurrentPageNo++;

            //Draw Data

            StringFormat StrFormat;
            StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;
            StrFormat.LineAlignment = StringAlignment.Center;
            StrFormat.Trimming = StringTrimming.EllipsisCharacter;

            float StartX = 10;
            float StartY = 5;
            // Printing Current Page, Row by Row
            while (CurrentRowIndex < BarCodeDataList.Count)
            {
                if (CurrentRowIndex >= (RowsPerPage * CurrentPageNo))
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    //Row 1
                    StartY = 5;
                    PointF point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].ShopName, ShopNameFont, blackBrush, point);
                    //Row 2
                    StartY = StartY + 10;
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].ProductName, ProductNameFont, blackBrush, point);
                    
                    StartX = StartX + 100;
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].ProdLoosePack + BarCodeDataList[CurrentRowIndex].ProdPack, ProductPackFont, blackBrush, point);
                    //Row 3
                    StartX = 10;
                    StartY = StartY + 10;
                    e.Graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(StartX), Convert.ToInt32(StartY), LabelWidth, LabelHeight));
                    e.Graphics.DrawImage(BarCodeDataList[CurrentRowIndex].BarCodeImage, StartX, StartY);

                    StartX = StartX + LabelWidth;                   
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].Expiry, ExpiryFont, blackBrush, point);
                    //Row 4
                    StartX = 10;
                    StartY = StartY + LabelHeight + 5;
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].BarCode, ScanCodeFont, blackBrush, point);
                    
                    StartX = StartX + 60;
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].BatchNumber, BatchFont, blackBrush, point);

                    StartX = StartX + 40;
                    point = new PointF(StartX, StartY);
                    e.Graphics.DrawString(BarCodeDataList[CurrentRowIndex].MRP.ToString(), MRPFont, blackBrush, point);

                    //StartY = StartY + LabelHeight + 5;
                    //StartX = StartX + LabelWidth + LabelGap;
                    CurrentRowIndex++;
                }
            } //while 

        }


        public static Image GetImage(BarCodeData barCodeData)
        {
            Image img = null;
            try
            {
                b = new BarcodeLib.Barcode();
                int W = LabelWidth;
                int H = LabelHeight;
                b.Alignment = BarcodeLib.AlignmentPositions.CENTER;

                BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
                type = BarcodeLib.TYPE.CODE128;

                b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), "Rotate180FlipXY", true);
                b.IncludeLabel = false;
                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT;
                img = b.Encode(type, barCodeData.BarCode, Color.Black, Color.White, W, H);

            }
            catch (Exception ex)
            {
                EcoMart.Common.Log.WriteException(ex);
            }
            return img;
        }       
    }

    public class BarCodeData
    {
        private string _ShopName;
        private string _ProductName;
        private int _ProdLoosePack;
        private string _ProdPack;
        private string _BatchNumber;
        private string _Expiry;
        private string _MRP;
        private int _Quantity;
        private string _BarCode;
        private Image _BarCodeImage;

        public string ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public int ProdLoosePack
        {
            get { return _ProdLoosePack; }
            set { _ProdLoosePack = value; }
        }
        public string ProdPack
        {
            get { return _ProdPack; }
            set { _ProdPack = value; }
        }
        public string BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }
        public string Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }
        public string MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }

        public Image BarCodeImage
        {
            get { return _BarCodeImage; }
            set { _BarCodeImage = value; }
        }
    }
}
