using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PharmaSYSRetailPlus.Printing;
using PrintDataGrid;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.Common.Classes;
using System.Windows.Forms;
using PharmaSYSPlus.CommonLibrary;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


namespace PharmaSYSRetailPlus.Printing
{
    public static class GeneralReport
    {
        internal static int PrintHeader(int TotalPages, Font fnt, string ReportHead, int PrintRowPixel, int PrintPageNumber, string ReportHead2)
        {
            PrintRow row;
            try
            {

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Date   : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Time   : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopTelephone, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Page Number : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);

                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                row = new PrintRow(ReportHead, PrintRowPixel, 1, mfnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("A/c Year        : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                string vs = General.ShopDetail.Shopsy.Substring(0, 4) + "-" + General.ShopDetail.Shopey.Substring(0, 4);
                row = new PrintRow(vs, PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                if (ReportHead2 != "")
                {
                    PrintRowPixel = PrintRowPixel + 17;
                    mfnt = new Font("Arial", 10, FontStyle.Bold);
                    row = new PrintRow(ReportHead2, PrintRowPixel, 1, mfnt);
                    PrintBill.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return PrintRowPixel;
        }

        internal static int PrintHeaderLarge(int TotalPages, Font fnt, string ReportHead, int PrintRowPixel, int PrintPageNumber, string ReportHead2)
        {
            PrintRow row;
            try
            {

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Date   : ", PrintRowPixel, 780, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Time   : ", PrintRowPixel, 780, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 660, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopTelephone, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Page Number : ", PrintRowPixel, 780, fnt);
                PrintBill.Rows.Add(row);

                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 860, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                row = new PrintRow(ReportHead, PrintRowPixel, 1, mfnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("A/c Year        : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                string vs = General.ShopDetail.Shopsy.Substring(0, 4) + "-" + General.ShopDetail.Shopey.Substring(0, 4);
                row = new PrintRow(vs, PrintRowPixel, 860, fnt);
                PrintBill.Rows.Add(row);

                if (ReportHead2 != "")
                {
                    PrintRowPixel = PrintRowPixel + 17;
                    mfnt = new Font("Arial", 10, FontStyle.Bold);
                    row = new PrintRow(ReportHead2, PrintRowPixel, 1, mfnt);
                    PrintBill.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return PrintRowPixel;
        }

        internal static void ExportPrintHeader(string ReportHead, string ReportHead2, string filePath)
        {

            try
            {
                string delimiter = ",";
                string[][] output = new string[][]{
            new string[]{General.ShopDetail.ShopName,"","","","","Report Date   : ",General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd"))}};
                int length = output.GetLength(0);
                StringBuilder sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());

                output = new string[][]{
                  new string[]{General.ShopDetail.ShopAddress1,"","","","","Report Time   : ",DateTime.Now.TimeOfDay.ToString().Substring(0, 5)}};
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());

                output = new string[][]{
                  new string[]{"Tel:",General.ShopDetail.ShopTelephone}};
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());

                string vs = General.ShopDetail.Shopsy.Substring(0, 4) + "-" + General.ShopDetail.Shopey.Substring(0, 4);
                output = new string[][]{
                  new string[]{"A/c Year        : ",vs}};
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());


                output = new string[][]{
                  new string[]{ReportHead}};
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());

                if (ReportHead2 != "")
                {
                    output = new string[][]{
                  new string[]{ReportHead2}};
                    length = output.GetLength(0);
                    sb = new StringBuilder();
                    for (int index = 0; index < length; index++)
                        sb.AppendLine(string.Join(delimiter, output[index]));
                    File.AppendAllText(filePath, sb.ToString());
                }

                output = new string[][]{
                  new string[]{" "}};
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        internal static void ExportPrintDetails(MReportGridView mrpview, string filePath)
        {
            try
            {
                CreateCSVFile(mrpview, filePath);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private static void CreateCSVFile(MReportGridView mrpview, string filePath)
        {
            string delimiter = ",";
            string headstring = "";
            int noofcolumns = mrpview.Columns.Count;
            for (int i = 0; i < noofcolumns; i++)
            {
                if (mrpview.Columns[i].Visible == true)
                    headstring = headstring + (mrpview.Columns[i].HeaderText.ToString()) + ",";
            }
            string[][] output = new string[][]{
                new string[]{headstring}};
            int length = output.GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));
            File.AppendAllText(filePath, sb.ToString());
            foreach (DataGridViewRow dr in mrpview.Rows)
            {
                headstring = "";
                for (int i = 0; i < noofcolumns; i++)
                {
                    if (dr.Cells[i].Visible == true)
                    {
                        if (dr.Cells[i].FormattedValue != null)
                        {
                            headstring = headstring + dr.Cells[i].FormattedValue.ToString() + ",";
                        }
                        else
                        {
                            headstring = headstring + "" + ",";
                        }
                    }
                }


                output = new string[][]{
                            new string[]{headstring}
                            };
                length = output.GetLength(0);
                sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());
            }
        }

        internal static void EmailPrintDetails(MReportGridView mrpview, string filePath,string ReceiverEmailID)
        {

            CreateCSVFile(mrpview, filePath);            
           
            string myEmailID = General.ShopDetail.ShopEmailID.ToString().Trim();
            myEmailID = "shubhamcbhagwat@gmail.com";

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
           
            smtp.EnableSsl = true;           
            mail.From = new MailAddress(myEmailID);          
           
            mail.To.Add(ReceiverEmailID);
            mail.Subject = "Report";
            

            string user;
            string password;
            user = myEmailID;
            password = "s@neritA143";
            password = "shubh@M143";
            NetworkCredential netCre = new NetworkCredential(user, password);
            smtp.Credentials = netCre;
            //smtp.Send(mail);

            try
            {

                using (Attachment atch = new Attachment(filePath))
                {

                    mail.Attachments.Add(new Attachment(filePath));

                    try
                    {
                        smtp.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    atch.Dispose();
                    //  smtp.Dispose();
                    mail.Dispose();
                }

                // oSmtp.SendMail(oServer, oMail);
                //   MessageBox.Show("Mail Send!", "success", MessageBoxButtons.OK);
                //  atch.Dispose();
                //   mail.Attachments.Remove(new Attachment(abc));

                //  mail.Attachments.Clear();

                //oSmtp.Close();
                //oMail.ClearAttachments();
                //oMail.Reset();


            }



            catch (Exception ep)
            {
                MessageBox.Show("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
                        
                        

        }
    }
}
