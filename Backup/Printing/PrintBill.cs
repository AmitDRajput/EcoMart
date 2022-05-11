using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.ObjectModel;


namespace PrintDataGrid
{
    class PrintBill
    {
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static StringFormat StrFormat;  // Holds content of a TextBox Cell to write by DrawString

        

        public static int CurrentRowPosition = 0;
        public static int CurrentColumnPosition = 0;

        public static float RowHeight = 0;
        public static int CharWidth = 0;

        public static PrintRowCollection Rows = new PrintRowCollection();

       
        private static Font _Font = new Font("Arial", 10, FontStyle.Regular);

        static PrintBill()
        {
            Rows.OnPrintRowAdded -= new PrintRowCollection.PrintRowAdded(Rows_OnPrintRowAdded);
            Rows.OnPrintRowAdded += new PrintRowCollection.PrintRowAdded(Rows_OnPrintRowAdded);
        }

        static void Rows_OnPrintRowAdded(PrintRow row)
        {
            //Set Current Column Position
            //CurrentColumnPosition = 
        }

      

        public static void Print_Report()
        {
            /////ss
           // PrintPreviewDialog ppvw1;
            CoolPrintPreview.CoolPrintPreviewDialog ppvw;
            /////ss
            try
            {
                printDoc = new System.Drawing.Printing.PrintDocument();
                printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom size", 1152, 1248);  // 416 is near to perfection for 4 inch

                RowHeight = _Font.GetHeight();

                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                /////////// ss  upper two lines also
               
                ppvw = new CoolPrintPreview.CoolPrintPreviewDialog();
                ppvw.Document = printDoc;
                ppvw.StartPosition = FormStartPosition.CenterScreen;
                ppvw.WindowState = FormWindowState.Maximized;

                // Showing the Print Preview Page
                if (ppvw.ShowDialog() != DialogResult.OK)
                {
                    printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                    printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                    return;
                }

                ////////ss

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        public static void Print_Bill()
        {
          //  PrintPreviewDialog ppvw;
            ////////CoolPrintPreview.CoolPrintPreviewDialog ppvw;
            try
            {
                printDoc = new System.Drawing.Printing.PrintDocument();

                RowHeight = _Font.GetHeight();

                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);


                //Set Paper Size 
                //printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CustomSize", 800, 411);
                // Printing the Documnet

                //  ss

                ////////ppvw = new CoolPrintPreview.CoolPrintPreviewDialog();
                ////////ppvw.Document = printDoc;
                ////////ppvw.StartPosition = FormStartPosition.CenterScreen;
                ////////ppvw.WindowState = FormWindowState.Maximized;

                ////////// Showing the Print Preview Page
                ////////if (ppvw.ShowDialog() != DialogResult.OK)
                ////////{
                ////////    printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                ////////    printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                ////////    return;
                ////////}
               // ss

                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }               

        public static void Print_Bill(int PageWidth, int PageHeight)
        {
          // PrintPreviewDialog ppvw;
           //  ss
            ////////CoolPrintPreview.CoolPrintPreviewDialog ppvw;
          //  ss
            try
            {
                printDoc = new System.Drawing.Printing.PrintDocument();

                RowHeight = _Font.GetHeight();

                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);


                //Set Paper Size 
                printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CustomSize", PageWidth, PageHeight);
                // Printing the Documnet

                //ss
                ////////ppvw = new CoolPrintPreview.CoolPrintPreviewDialog();
                ////////ppvw.Document = printDoc;
                ////////ppvw.StartPosition = FormStartPosition.CenterScreen;
                ////////ppvw.WindowState = FormWindowState.Maximized;

                ////////// Showing the Print Preview Page
                ////////if (ppvw.ShowDialog() != DialogResult.OK)
                ////////{
                ////////    printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                ////////    printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                ////////    return;
                ////////}
                //ss
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }               
      
        private static void PrintDoc_BeginPrint(object sender,
                    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Formatting the Content of Text Cell to print
                StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;
                StrFormat.LineAlignment = StringAlignment.Center;
                StrFormat.Trimming = StringTrimming.EllipsisCharacter; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender,
                    System.Drawing.Printing.PrintPageEventArgs e)
        {          
            try
            {
                foreach (PrintRow row in Rows)
                {
                    e.Graphics.DrawString(row.PrintValue, row.PrintFont,
                                        Brushes.Black,
                                        new PointF(row.Column, row.Row), StrFormat);                    
                    
                }
                e.HasMorePages = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private static float GetRowHeight(int rowID)
        //{
        //    float totalHeight = 0;
        //    for (int i = 1; i <= rowID; i++)
        //    {
        //        totalHeight += GetRowHeightFromCollection(i);
        //    }
        //    return totalHeight;
        //}

        //private static float GetRowHeightFromCollection(int rowID)
        //{
        //    float rowHeight = 0;
        //    bool isRowFound = false;
        //    foreach (PrintRow row in Rows)
        //    {
        //        if (row.Row == rowID)
        //        {
        //            float height = row.PrintFont.GetHeight();
        //            if (height > rowHeight)
        //                rowHeight = height;
        //            isRowFound = true;
        //        }
        //    }
        //    if (isRowFound == false)
        //    {
        //        rowHeight = _Font.GetHeight();
        //    }
        //    return rowHeight;
        //}
    }

    

    public class PrintRow
    {
        private int rowID;
        private int columnID;
        private string printValue;
        private Font printFont;

        public PrintRow(string value, int row, int column, Font font)
        {
            printValue = value;
            rowID = row;
            columnID = column;
            printFont = font;
        }

       

        public string PrintValue
        {
            get { return printValue; }
        }

        public int Row
        {
            get { return rowID; }
        }

        public int Column
        {
            get { return columnID; }
        }

        public Font PrintFont
        {
            get { return printFont; }
        }
    }

    public class PrintRowCollection : Collection<PrintRow>
    {
        public delegate void PrintRowAdded(PrintRow row);
        public event PrintRowAdded OnPrintRowAdded;       
        public PrintRowCollection()
        {
          
        }

        protected override void InsertItem(int index, PrintRow item)
        {
            base.InsertItem(index, item);
            if (OnPrintRowAdded != null)
                OnPrintRowAdded(item);
        }

    }
}
