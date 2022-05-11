
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{

    public class SendSMS
    {

        public bool SendSMSData(string tostr, string msgstr)    
        {
            bool showmsg = true;
            // bool functionReturnValue = false;

            string[] tostrarr = Strings.Split(tostr, ",");
            List<string> valstr = new List<string>();
            string valtostr = null;
            string tempstr = null;
            int sind = 0;
            bool retval = false;

            msgstr = ReplaceChar(msgstr);

            if (tostrarr.Length > 0)
            {
                foreach (string tempstr_loopVariable in tostrarr)
                {
                    tempstr = tempstr_loopVariable;
                    if (tempstr.Trim().Length >= 10)
                    {
                        sind = tempstr.Length - 10;
                        if (sind < 0)
                        {
                            sind = 0;
                        }
                        tempstr = tempstr.Substring(sind, 10);
                        tempstr = "91" + tempstr;
                        valstr.Add(tempstr);
                    }
                }
            }
            else
            {
                if (showmsg)
                {
                    Interaction.MsgBox("Mobile no(s) not valid");
                }

                return retval;
                //return functionReturnValue;
            }

            if (valstr.Count > 0)
            {
                valtostr = Strings.Join(valstr.ToArray(), ",");
            }
            else
            {
                if (showmsg)
                {
                    Interaction.MsgBox("Mobile no(s) not valid");
                }
                 return retval;
                // return functionReturnValue;
            }

            DataTable ds = default(DataTable);// SMSSetting Table

            //SMSSettingDS.SMSSettingsRow dr = default(SMSSettingDS.SMSSettingsRow);
            SMSDB objDB = new SMSDB();

            string url = null;

            ds = objDB.GetSettings();


            if (ds.Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Rows)
                {
                    url = dr["URL"].ToString();

                    if (url.Contains("<to>"))
                    {
                        url = url.Replace("<to>", valtostr);
                    }

                    if (url.Contains("<message>"))
                    {
                        url = url.Replace("<message>", msgstr);
                    }

                    if (IsSMSSent(url))
                    {
                        retval = true;
                        if (showmsg)
                        {
                            Interaction.MsgBox("Message sent successfully");
                        }
                        return retval;
                        // return functionReturnValue;
                    }

                }
                if (showmsg)
                {
                    Interaction.MsgBox("Error in sending sms");
                }
            }
            else
            {
                if (showmsg)
                {
                    Interaction.MsgBox("Please update sms settings");
                }
            }
            return retval;
            //  return functionReturnValue;
        }

        private bool IsSMSSent(string url)
        {

            bool retval = false;
            HttpWebRequest req = default(HttpWebRequest);
            HttpWebResponse res = default(HttpWebResponse);
            Stream s = default(Stream);
            StreamReader rdr = default(StreamReader);
            string result = null;

            try
            {
                //create Web Request object for the url
                req = (HttpWebRequest)WebRequest.Create(url);

                // get the response from the web request
                res = (HttpWebResponse)req.GetResponse();

                // read the response from the WebResponse in to a stram
                s = res.GetResponseStream();
                rdr = new StreamReader(s);
                result = rdr.ReadLine();
                retval = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
            finally
            {
                try
                {
                    s.Flush();
                    s.Close();

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message);
                }

            }
            return retval;
        }

        ////nandini 13th dec 2010
        private string ReplaceChar(string Msg)
        {
            DataTable objDS = new DataTable();
            SpecCharInSMSDB objDb = new SpecCharInSMSDB();
            objDS = objDb.getSpecChar();

            foreach (DataRow dr in objDS.Rows)
            {
                string data = Convert.ToString(dr["Find"]);
                if (Msg.Contains(data))
                {
                    Msg = Msg.Replace(dr["Find"].ToString(), dr["replace"].ToString());
                }
            }
            return Msg;
        }
        public string GetShopDetailsFromData() //Amar
        {
            string Msg = string.Empty;
            try
            {
                Msg = "\n From,\n" + General.ShopDetail.ShopName + "\n Contact No: ";
                bool IsMobileNumber = false;

                if (string.IsNullOrEmpty(General.ShopDetail.ShopMobileNumber) == false)
                {
                    Msg += General.ShopDetail.ShopMobileNumber;
                    IsMobileNumber = true;
                }
                if (string.IsNullOrEmpty(General.ShopDetail.ShopTelephone) == false)
                {
                    if (IsMobileNumber)
                        Msg += "/";
                    Msg += General.ShopDetail.ShopTelephone;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
            return Msg;
        }
            //Public Function SendWay2SMS(ByVal tostr As String, ByVal msgstr As String, Optional ByVal showmsg As Boolean = True) As Boolean

            //    Dim tostrarr() As String = Split(tostr, ",")
            //    Dim valstr As New List(Of String)
            //    'Dim valtostr As String
            //    Dim tempstr As String
            //    Dim sind As Integer
            //    Dim retval As Boolean = False

            //    Dim status As String = String.Empty
            //    Dim cookie As CookieContainer = SMSClientLib.StaticLogin.Connect("9881152653", "4567", status)
            //    Dim siteParameters As String() = SMSClientLib.StaticLogin.GetSiteParameters(cookie)

            //    Try
            //        msgstr = ReplaceChar(msgstr)

            //        If tostrarr.Length > 0 Then
            //            For Each tempstr In tostrarr
            //                If tempstr.Trim.Length >= 10 Then
            //                    sind = tempstr.Length - 10
            //                    If sind < 0 Then
            //                        sind = 0
            //                    End If
            //                    tempstr = tempstr.Substring(sind, 10)
            //                    tempstr = "91" & tempstr
            //                    '           valstr.Add(tempstr)
            //                    Dim messgeSentResult As String = SMSClientLib.SendSMS.Send_Processing(tempstr, msgstr, cookie, siteParameters)
            //                    retval = True
            //                End If
            //            Next
            //        Else
            //            'MsgBox("Mobile no(s) not valid")
            //            Return retval
            //            Exit Function
            //        End If

            //    Catch ex As Exception

            //    End Try

            //    Return retval
            //End Function

        }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
