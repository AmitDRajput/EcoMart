
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.IO;
//using SS.HMS.Utility;
//using SS.HMS.Utility.DB;
//using SS.HMS.Utility.Constants;
//Imports Ionic.Zip
using Microsoft.Win32;
using System.IO.Compression;
using Ionic.Zip;
using System.Xml;
using System.Windows.Forms;
using EcoMart.DataLayer;
using Microsoft.VisualBasic;
using EcoMart.Common;

namespace EcoMart
{
    public partial class Download_Updates : Form
    {
        string NoRecFound = "No records found";
        public Download_Updates()
        {
            InitializeComponent();
        }

        private void Download_Updates_Load(object sender, EventArgs e)
        {

        }
        private string md5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public void DeleteFilesFromFolder(string Folder)
        {
            if (Directory.Exists(Folder))
            {
                foreach (string _file in Directory.GetFiles(Folder))
                {
                    File.Delete(_file);
                }

                foreach (string _folder in Directory.GetDirectories(Folder))
                {
                    DeleteFilesFromFolder(_folder);
                }

            }

        }
        public void DownloadFile()
        {
            //Dim version As String = "v1.0.0.4"
            string sqlQuery = "Select VersionNo from Versioning order by AddedDate desc LIMIT 1";
            DataRow dataRow = DBInterface.SelectFirstRow(sqlQuery);
            string version = NoRecFound;
            if (dataRow != null)
                version = dataRow[0].ToString();
            if (version == NoRecFound)
            {
                version = "0";
            }
            string remoteUrl = "http://version.medsonit.com/admin/api/check_version/";
            //string myStringWebResource = null;
            remoteUrl = remoteUrl + version + "/" + md5(version + DateTime.Now.ToString("yyyyMMdd"));
            WebClient myWebClient = new WebClient();
            System.Net.WebClient webClient = new System.Net.WebClient();
            string scriptstatus = "";
            try
            {
                string result = webClient.DownloadString(remoteUrl);
                if (result == "You have already Updated your version")
                {
                    MessageBox.Show("Your version is up to date!");
                }
                else
                {
                    ProgressBar1.Value = ProgressBar1.Value + 10;
                    string[] arr = result.Split('#');
                    string filename = "";
                    string url = "";
                    string Rarfile = "";
                    string Path = Application.StartupPath + "\\Downloads\\" + System.DateTime.Now.ToString("yyyyMMdd");
                    string[] temp = (arr[1].ToUpper()).Split('V');
                    string versionNo1 = temp[1];
                    string versionNo = arr[1].ToString().Trim();
                    if (checkVesrion(versionNo1))
                    {
                        for (int i = 2; i <= arr.Length - 1; i++)
                        {
                            if (arr[i].ToString().Contains("http://"))
                            {
                                url = arr[i].ToString().Trim();
                                filename = arr[i - 2].ToString().Trim();
                                if (filename.Contains(".zip"))
                                {
                                    Rarfile = filename;
                                }
                                if ((!System.IO.Directory.Exists(Path)))
                                {
                                    System.IO.Directory.CreateDirectory(Path);
                                }
                                else
                                {
                                    DeleteFilesFromFolder(Path);
                                    //System.IO.Directory.CreateDirectory(Path)
                                }
                                myWebClient.DownloadFile(url, Path + "\\" + filename);
                                ProgressBar1.Value = ProgressBar1.Value + 10;
                            }
                        }
                        ZipFile zipFile = new ZipFile(Path + "\\" + Rarfile);
                        zipFile.ExtractAll(Path);
                        string[] files = Directory.GetFiles(Path + "\\", "*.XML", SearchOption.AllDirectories);
                        foreach (string s in files)
                        {
                            XmlTextReader reader = new XmlTextReader(s);
                            while (reader.Read())
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        //LblRelease.Text = LblRelease.Text + ("<" + reader.Name & ">")
                                        break; // TODO: might not be correct. Was : Exit Select

                                    case XmlNodeType.Text:
                                        LblRelease.Text = LblRelease.Text + (reader.Value);
                                        break; // TODO: might not be correct. Was : Exit Select

                                    case XmlNodeType.EndElement:
                                        LblRelease.Text = LblRelease.Text + ("");
                                        break; // TODO: might not be correct. Was : Exit Select

                                }
                            }
                        }
                        LblRelease.Visible = true;


                        if (DialogResult.Yes == MessageBox.Show("Download Complete! do you want to install Update ?", "Delete", MessageBoxButtons.YesNoCancel))
                        {
                            string Query = "select isdbupdated from Versioning where VersionNo = '" + version + "'";
                            DataRow dr = DBInterface.SelectFirstRow(Query);
                            scriptstatus = "0";
                            if (dr != null)
                                scriptstatus = dr[0].ToString();
                            if (!(scriptstatus == "True"))
                            {
                                DirectoryInfo Di = new DirectoryInfo(Path);
                                FileInfo[] sFiles = Di.GetFiles();
                                FileInfo File = null;
                                foreach (FileInfo File_loopVariable in sFiles)
                                {
                                    File = File_loopVariable;
                                    if (File.Name.Contains(".sql"))
                                    {
                                        DBInterface.ExecuteScriptFromFile(Path + "\\" + File.Name);
                                    }
                                }

                            }
                            //' Insert into versioning table
                            Query = "select ( Max(id) +1)  from Versioning";
                            dataRow = DBInterface.SelectFirstRow(Query);
                            int ID = 1;
                            if (dataRow != null)
                                ID = Convert.ToInt32(dataRow[0].ToString());
                            string str = "Insert into Versioning Values(" + ID + " ,'" + versionNo + "',1,1,NOW())";
                            DBInterface.ExecuteQuery(str);

                             Query = "select isdbupdated from Versioning where VersionNo = '" + version + "'";
                             dr = DBInterface.SelectFirstRow(Query);
                             scriptstatus = "0";
                             if (dr != null)
                                scriptstatus = dr[0].ToString();
                             if (!(scriptstatus == "True"))
                            {
                                DirectoryInfo Di = new DirectoryInfo(Path);
                                FileInfo[] vFiles = Di.GetFiles();
                                FileInfo File = null;
                                foreach (FileInfo File_loopVariable in vFiles)
                                {
                                    File = File_loopVariable;
                                    if (File.Name.Contains(".sql"))
                                    {
                                        DBInterface.ExecuteScriptFromFile(Path + "\\" + File.Name);
                                    }
                                }

                            }
                            // UnRar(Path, Path + "\" + Rarfile)
                            if ((System.IO.Directory.Exists(Path + "\\Reports")))
                            {
                                DirectoryInfo Di = new DirectoryInfo(Path + "\\Reports");
                                FileInfo[] RFiles = Di.GetFiles();
                                FileInfo File = null;
                                foreach (FileInfo File_loopVariable in RFiles)
                                {
                                    File = File_loopVariable;
                                    if (File.Name.Contains(".rdlc"))
                                    {
                                        FileSystem.FileCopy(Path + "\\Reports", Application.StartupPath + "\\Reports");
                                    }
                                }
                            }

                            Process.Start(Application.StartupPath + "\\ReplaceEXE.exe");
                            //Process p = new Process();
                            //p.StartInfo.FileName = Application.StartupPath + "\\ReplaceEXE.exe";
                            //p.Start();                          
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have already Update your version!");
                    }
                }
            }
            catch (Exception ex)
            {
                // ErrorLog.LogError(ex, string.Empty, this.Name);
                MessageBox.Show("Error Downloading update: "+ ex.Message, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool checkVesrion(string Version)
        {
            bool functionReturnValue = false;
            string Query  = 
            "Select VersionNo from Versioning order by AddedDate desc Limit 1 ";
            DataRow dr = DBInterface.SelectFirstRow(Query);
            string currentVersion = NoRecFound;
            if(dr != null)
                currentVersion = dr[0].ToString();
            if (currentVersion == NoRecFound)
            {
                return true;
            }
            else
            {
                string[] temp = (currentVersion.ToUpper()).Split('V');
                string CV = temp[1].ToString().Trim();
                                
                string[] NewVersion = Strings.Split(Version, ".");
                string[] OldVersion = Strings.Split(CV, ".");

                for (int s = 0; s <= 3; s++)
                {
                    if (Convert.ToInt32(NewVersion[s]) > Convert.ToInt32(OldVersion[s]))
                    {
                        functionReturnValue = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                    else
                    {
                        functionReturnValue = false;
                    }
                }
            }
            return functionReturnValue;
        }
       

        private void UnRar(string WorkingDirectory, string filepath)
        {
            // Microsoft.Win32 and System.Diagnostics namespaces are imported

            RegistryKey objRegKey = null;
            objRegKey = Registry.ClassesRoot.OpenSubKey("WinRAR\\Shell\\Open\\Command");
            // Windows 7 Registry entry for WinRAR Open Command

            object obj = objRegKey.GetValue("");

            string objRarPath = obj.ToString();
            objRarPath = objRarPath.Substring(1, objRarPath.Length - 7);

            objRegKey.Close();

            string objArguments = null;
            // in the following format
            // " X G:\Downloads\samplefile.rar G:\Downloads\sampleextractfolder\"
            objArguments = " X " + " " + filepath + " " + " " + WorkingDirectory;

            ProcessStartInfo objStartInfo = new ProcessStartInfo();
            // Set the UseShellExecute property of StartInfo object to FALSE
            // Otherwise the we can get the following error message
            // The Process object must have the UseShellExecute property set to false in order to use environment variables.
            objStartInfo.UseShellExecute = false;
            objStartInfo.FileName = objRarPath;
            objStartInfo.Arguments = objArguments;
            objStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            objStartInfo.WorkingDirectory = WorkingDirectory + "\\";

            Process objProcess = new Process();
            objProcess.StartInfo = objStartInfo;
            objProcess.Start();

        }      
        private void FtpUploadFile(string filetoupload, string ftpuri, string ftpusername, string ftppassword)
        {
            // Create a web request that will be used to talk with the server and set the request method to upload a file by ftp.
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpuri);

            try
            {
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                // Confirm the Network credentials based on the user name and password passed in.
                ftpRequest.Credentials = new NetworkCredential(ftpusername, ftppassword);

                // Read into a Byte array the contents of the file to be uploaded 
                byte[] bytes = System.IO.File.ReadAllBytes(filetoupload);

                // Transfer the byte array contents into the request stream, write and then close when done.
                ftpRequest.ContentLength = bytes.Length;
                using (Stream UploadStream = ftpRequest.GetRequestStream())
                {
                    UploadStream.Write(bytes, 0, bytes.Length);
                    UploadStream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Process Complete");
        }
        private void FTPDownloadFile(string downloadpath, string ftpuri, string ftpusername, string ftppassword)
        {
            //Create a WebClient.
            WebClient request = new WebClient();

            // Confirm the Network credentials based on the user name and password passed in.
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);

            //Read the file data into a Byte array
            byte[] bytes = request.DownloadData(ftpuri);

            try
            {
                //  Create a FileStream to read the file into
                FileStream DownloadStream = System.IO.File.Create(downloadpath);
                //  Stream this data into the file
                DownloadStream.Write(bytes, 0, bytes.Length);
                //  Close the FileStream
                DownloadStream.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Process Complete");

        }

        private void Btndownload_Click(object sender, EventArgs e)
        {
            //ProgressBar1.Visible = True
            DownloadFile();
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Download_Updates
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 262);
        //    this.Name = "Download_Updates";
        //    this.ResumeLayout(false);

        //}
    }
}
