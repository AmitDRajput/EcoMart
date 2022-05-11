using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EcoMart.Printing;
using PrintDataGrid;
using EcoMart.Common;
using EcoMart.Common.Classes;
using System.Windows.Forms;
using PharmaSYSPlus.CommonLibrary;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace EcoMart.Printing
{
    public static class GeneralReport
    {
        internal static int PrintHeader(int TotalPages, Font fnt, string ReportHead, int PrintRowPixel, int PrintPageNumber, string ReportHead2,string FromDate, string ToDate)
        {
            PrintRow row;
            try
            {

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                row = new PrintRow(ReportHead, PrintRowPixel, 580, mfnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Date   : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), PrintRowPixel, 670, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopAddress2, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Time   : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 670, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopTelephone, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Page Number : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);

                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 670, fnt);
                PrintBill.Rows.Add(row);

                //PrintRowPixel = PrintRowPixel + 17;
                //Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                //row = new PrintRow(ReportHead, PrintRowPixel, 1, mfnt);
                //PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                //string vs = General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")) + " - " + General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")); // [Temporary]
                string vs = FromDate + " - " + ToDate;
                row = new PrintRow("From Date To Date  : " + vs, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Operator : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Operator", PrintRowPixel, 660, fnt);  // [Temporary]
                PrintBill.Rows.Add(row);

                //PrintRowPixel += 400;
                //row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, fnt);  // [Temporary]
                //PrintBill.Rows.Add(row);

                if (ReportHead2 != "")
                {
                    PrintRowPixel = PrintRowPixel + 17;
                    mfnt = new Font("Arial", 10, FontStyle.Bold);
                    row = new PrintRow(ReportHead2, PrintRowPixel, 10, mfnt);
                    PrintBill.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return PrintRowPixel;
        }

        internal static int PrintFooter(int TotalPages, Font fnt, int PrintRowPixel, int PrintPageNumber)
        {
            PrintRow row;
            try
            {

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow((General.ShopDetail.ShopJurisdiction).ToUpper(), PrintRowPixel, 10, fnt);  // [Temp]
                PrintBill.Rows.Add(row);

                row = new PrintRow("FOR SHOP :", PrintRowPixel, 580, fnt);  // [Temp]
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 670, fnt);  // [Temp]
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow("DL Number :", PrintRowPixel, 10, fnt); // [Temp]
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.ShopDetail.ShopDLN, PrintRowPixel, 120, fnt); // [Temp]
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow("VAT Number :", PrintRowPixel, 10, fnt); // [Temp]
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.ShopDetail.ShopVATTINV, PrintRowPixel, 120, fnt); // [Temp]
                PrintBill.Rows.Add(row);

                //Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                row = new PrintRow("PHARMACIST :", PrintRowPixel, 580, fnt);   // [Temp]
                PrintBill.Rows.Add(row);

                Font mfnt = new Font("Arial", 10, FontStyle.Bold);
                row = new PrintRow("SIGNETURE", PrintRowPixel, 690, mfnt);   // [Temp]
                PrintBill.Rows.Add(row);
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

                if (ReportHead2 !=null &&  ReportHead2 != "")
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

        internal static bool  ExportPrintDetails(MReportGridView mrpview, string filePath)
        {
            bool retValue = true;
            try
            {
                CreateCSVFile(mrpview, filePath);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                retValue = false;
            }
            return retValue;
        }
        internal static bool ExportPrintDetails(DataGridView mrpview, string filePath)
        {
            bool retValue = true;
            try
            {
                CreateCSVFile(mrpview, filePath);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                retValue = false;
            }
            return retValue;
        }
        internal static string GetEmailFileName()
        {
            string retValue = string.Empty;
            try
            {
                retValue = AppDomain.CurrentDomain.BaseDirectory.ToString() + GetISODateTime();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private static string GetISODateTime()
        {
            string strRetVal = "";

            string strYear = DateTime.Now.Year.ToString("0000");
            string strMonth = DateTime.Now.Month.ToString("00");
            string strDay = DateTime.Now.Day.ToString("00");
            string strHour = DateTime.Now.Hour.ToString("00");
            string strMinute = DateTime.Now.Minute.ToString("00");
            string strSecond = DateTime.Now.Second.ToString("00");

            strRetVal = strYear + strMonth + strDay + strHour + strMinute + strSecond + ".csv";

            return strRetVal;
        }
        private static void CreateCSVFile(DataGridView mrpview, string filePath)
        {
            string delimiter = ",";
            string headstring = "";
            int noofcolumns = mrpview.Columns.Count;
            for (int i = 0; i < noofcolumns; i++)
            {
                if (mrpview.Columns[i].Visible == true)
                {
                    headstring = headstring + (mrpview.Columns[i].HeaderText.ToString().Replace(delimiter, " ")) + delimiter;
                }
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
                            headstring = headstring + dr.Cells[i].FormattedValue.ToString().Replace(delimiter, " ") + delimiter;
                        }
                        else
                        {
                            headstring = headstring + "" + delimiter;
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
        private static void CreateCSVFile(MReportGridView mrpview, string filePath)
        {
            string delimiter = ",";
            string headstring = "";
            int noofcolumns = mrpview.Columns.Count;
            for (int i = 0; i < noofcolumns; i++)
            {
                if (mrpview.Columns[i].Visible == true)
                {
                    headstring = headstring + (mrpview.Columns[i].HeaderText.ToString().Replace(delimiter, " ")) + delimiter;
                }
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
                            headstring = headstring + dr.Cells[i].FormattedValue.ToString().Replace(delimiter, " ") + delimiter;
                        }
                        else
                        {
                            headstring = headstring + "" + delimiter;
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

        private static bool AttachAndSendMail(string csvfile, SmtpClient client, MailMessage mm)
        {
            bool retValue = true;
            try
            {
                using (Attachment data = new Attachment(csvfile, MediaTypeNames.Application.Octet))
                {
                    // Add time stamp information for the file.             
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(csvfile);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(csvfile);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(csvfile);
                    // Add the file attachment to this e-mail message.
                    mm.Attachments.Add(data);
                    // Add your send code in here too
                    client.Send(mm);
                  //  MessageBox.Show("Email sent SUCCESSFULL ...!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: [AttachAndSendMail]: " + ex.ToString());
                retValue = false;
                //throw ex;
            }
            return retValue;
        }

        internal static bool EmailPrintDetails(MReportGridView mrpview, string filePath,string ReceiverEmailID, string reportHead)
        {
            bool retValue = true;
            string emailFrom, emailFromPassword, emailTo, emailSubject, emailTextBody;
            try
            {
                CreateCSVFile(mrpview, filePath);

                emailFrom = General.CurrentSetting.MsetEmailID.ToString();
                emailFromPassword = General.CurrentSetting.MsetEmailPassword.ToString();
                emailTo = ReceiverEmailID;
                emailSubject = emailTextBody = reportHead;

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 500000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(emailFrom, emailFromPassword);

                MailMessage mm = new MailMessage(emailFrom, emailTo, emailSubject, emailTextBody);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                try
                {

                    if (AttachAndSendMail(filePath, client, mm))
                        retValue = true;
                    else
                        retValue = false;

                    mm.Attachments.Clear();
                    mm.Attachments.Dispose();
                    mm.Dispose();

                }
                catch (Exception ep)
                {
                    MessageBox.Show("ERROR: [SendEmail] failed to send email - " + ep.ToString());
                   // MoveFilesToError(emlfile);
                    retValue = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: [SendEmail]: " + ex.ToString());
                //MoveFilesToError(emlfile);
                retValue = false;
               
            }
            return retValue;
            /*
            string myEmailID = "";
            //myEmailID = "sheelabsharma@gmail.com";
            myEmailID = General.CurrentSetting.MsetEmailID.ToString();
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
           
            smtp.EnableSsl = true;           
            mail.From = new MailAddress(myEmailID);          
           
            mail.To.Add(ReceiverEmailID);
            mail.Subject = "Report";
            

            string user;
            string password;
            user = myEmailID;
          //  password = "s@neritA143";
            password = "sbtsgroup";
            password = General.CurrentSetting.MsetEmailPassword.ToString();
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
            }



            catch (Exception ep)
            {
                MessageBox.Show("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
                        
              */          

        }
        internal static bool EmailPrintDetails(DataGridView mrpview, string filePath, string ReceiverEmailID, string reportHead)
        {
            bool retValue = true;
            string emailFrom, emailFromPassword, emailTo, emailSubject, emailTextBody;
            try
            {
                CreateCSVFile(mrpview, filePath);

                emailFrom = General.CurrentSetting.MsetEmailID.ToString();
                emailFromPassword = General.CurrentSetting.MsetEmailPassword.ToString();
                emailTo = ReceiverEmailID;
                emailSubject = emailTextBody = reportHead;

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 500000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(emailFrom, emailFromPassword);

                MailMessage mm = new MailMessage(emailFrom, emailTo, emailSubject, emailTextBody);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                try
                {

                    if (AttachAndSendMail(filePath, client, mm))
                        retValue = true;
                    else
                        retValue = false;

                    mm.Attachments.Clear();
                    mm.Attachments.Dispose();
                    mm.Dispose();

                }
                catch (Exception ep)
                {
                    MessageBox.Show("ERROR: [SendEmail] failed to send email - " + ep.ToString());
                    // MoveFilesToError(emlfile);
                    retValue = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: [SendEmail]: " + ex.ToString());
                //MoveFilesToError(emlfile);
                retValue = false;

            }
            return retValue;
            /*
            string myEmailID = "";
            //myEmailID = "sheelabsharma@gmail.com";
            myEmailID = General.CurrentSetting.MsetEmailID.ToString();
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
           
            smtp.EnableSsl = true;           
            mail.From = new MailAddress(myEmailID);          
           
            mail.To.Add(ReceiverEmailID);
            mail.Subject = "Report";
            

            string user;
            string password;
            user = myEmailID;
          //  password = "s@neritA143";
            password = "sbtsgroup";
            password = General.CurrentSetting.MsetEmailPassword.ToString();
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
            }



            catch (Exception ep)
            {
                MessageBox.Show("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
                        
              */

        }

        

        public static void SendEmails(string PrintReportHead, string PrintReportHead2, MReportGridView dgvReportList, string EmailID)
        {
            string filePath = GeneralReport.GetEmailFileName();
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                if (GeneralReport.EmailPrintDetails(dgvReportList, filePath, EmailID, PrintReportHead))
                    MessageBox.Show("Email Sent...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
                else
                    MessageBox.Show("Error...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public static void ExportFile(string PrintReportHead, string PrintReportHead2, MReportGridView dgvReportList, string ExportFileName)
        {
            bool retValue = false;
            string filePath = ExportFileName;
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {               
                 if (Path.GetExtension(filePath) == ".pdf")
                {
                    retValue = General.CreatePDF(dgvReportList, filePath);
                    
                }
                 else if (Path.GetExtension(filePath) == ".csv")
                 {
                     ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                     retValue = ExportPrintDetails(dgvReportList, filePath);
                 }
                if (retValue)
                    MessageBox.Show("Export DONE...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
                else
                    MessageBox.Show("Error...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public static void SendEmails(string PrintReportHead, string PrintReportHead2, DataGridView dgvReportList, string EmailID)
        {
            string filePath = GeneralReport.GetEmailFileName();
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                if (GeneralReport.EmailPrintDetails(dgvReportList, filePath, EmailID, PrintReportHead))
                    MessageBox.Show("Email Sent...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
                else
                    MessageBox.Show("Error...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public static void ExportFile(string PrintReportHead, string PrintReportHead2, DataGridView dgvReportList, string ExportFileName)
        {
            bool retValue = false;
            string filePath = ExportFileName;
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {
                if (Path.GetExtension(filePath) == ".pdf")
                {
                    General.CreatePDF(dgvReportList, filePath);
                    
                }
                else if (Path.GetExtension(filePath) == ".csv")
                {
                    ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                    retValue = ExportPrintDetails(dgvReportList, filePath);
                }
                if (retValue)
                    MessageBox.Show("Export DONE...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
                else
                    MessageBox.Show("Error...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
    }
}
