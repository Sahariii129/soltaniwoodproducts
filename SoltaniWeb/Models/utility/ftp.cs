using SoltaniWeb.Models.repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace FtpClass
{

    public static class Ftp
    {
        //public bool Upload(UploadParametr u)
        //{
        //    //var q = Rep_Ftp.GetFtp(u.ServiceName);

        //    //if (q == null)
        //    //{
        //    //    Log.Add("", "", "Upload==>FTP", "حساب Ftp برای" + u.ServiceName + "موجود نمی باشد", "خطای اپلود", "Systemic");
        //    //    throw new Exception("خطایی هنگام اپلود رخ داد");
        //    //}


        //    // FileInfo fileInf = new FileInfo(filename);
        //    string uri = q.Server + q.Path + "/" + u.FileName;
        //    FtpWebRequest reqFTP;
        //    // Create FtpWebRequest object from the Uri provided
        //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(q.Server + q.Path + "/" + u.FileName));
        //    // Provide the WebPermission Credintials
        //    reqFTP.Credentials = new NetworkCredential(q.UserName, q.Password);
        //    // By default KeepAlive is true, where the control connection is not closed
        //    // after a command is executed.
        //    reqFTP.KeepAlive = false;
        //    // Specify the command to be executed.
        //    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
        //    // Specify the data transfer type.
        //    reqFTP.UseBinary = true;
        //    // Notify the server about the size of the uploaded file
        //    reqFTP.Length = u.File.Length;
        //    // The buffer size is set to 2kb
        //    int buffLength = 2048;
        //    byte[] buff = new byte[buffLength];
        //    int contentLen;
        //    // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
        //    Stream fs = u.File;
        //    try
        //    {
        //        Thread.Sleep(1000);
        //        // Stream to which the file to be upload is written
        //        Stream stream = reqFTP.GetRequestStream();
        //        // Read from the file stream 2kb at a time
        //        contentLen = fs.Read(buff, 0, buffLength);
        //        // Until Stream content ends
        //        while (contentLen != 0)
        //        {
        //            // Write Content from the file stream to the FTP Upload Stream
        //            stream.Write(buff, 0, contentLen);
        //            contentLen = fs.Read(buff, 0, buffLength);
        //        }
        //        // Close the file stream and the Request Stream
        //        stream.Close();
        //        fs.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Add("", "", "FtpUploadFile", ex.Message, "خطا هنگام اپلود تصویر مشتری", "FtpSystem");
        //        return false;
        //    }
        //}

        //public bool Delete(DeleteParametr d)
        //{
        //    try
        //    {
        //        var q = Rep_Ftp.GetFtp(d.ServiceName);
        //        if (q == null)
        //        {
        //            Log.Add("", "", "Delete==>FTP", "حساب Ftp برای" + d.ServiceName + "موجود نمی باشد", "خطای حذف فایل", "Systemic");
        //            throw new Exception("خطایی هنگام حذف Ftp رخ داد");
        //        }
        //        // Get the object used to communicate with the server.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(q.Server + q.Path + "/" + d.FileName));
        //        request.Credentials = new NetworkCredential(q.UserName, q.Password);
        //        request.Method = WebRequestMethods.Ftp.DeleteFile;

        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //        //Console.WriteLine("Delete status: {0}", response.StatusDescription);
        //        response.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Add("", "", "FtDeleteFile", ex.Message, "خطا هنگام حذف و جایگزینی تصویر مشتری", "FtpSystem");
        //        return false;
        //    }
        //}
        public static bool DeleteWithCkeck(DeleteParametr d, bool Check = true)
        {
            try
            {
                ftprepository Rep_FTP = new ftprepository();

                FileExistParametr f = new FileExistParametr();
                f.FileName = d.FileName;
                f.FTPID = d.FtpID;
                if (Check == true)
                {
                    if (!FileExist(f))
                        return false;
                }
                else
                    if (!FileExist(f))
                        return true;

                var q = Rep_FTP.getftp(d.FtpID);
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(q.serverlink + q.path1 + f.FileName);
                FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(q.serverlink + q.path2 + f.FileName);
                request.Credentials = new NetworkCredential(q.ftpusername, q.ftppassword);
                request2.Credentials = new NetworkCredential(q.ftpusername, q.ftppassword);

                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request2.Method = WebRequestMethods.Ftp.DeleteFile;
                try
                {
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    FtpWebResponse response2 = (FtpWebResponse)request2.GetResponse();
                    //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                    response.Close();
                    response2.Close();
                    return true;
                }
                catch (Exception)
                {

                    return true;
                }
               
                
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool FileExist(FileExistParametr f)
        {

            ftprepository Rep_FTP = new ftprepository();
            var q = Rep_FTP.getftp(f.FTPID);
            var request = (FtpWebRequest)WebRequest.Create(q.serverlink + q.path1 + f.FileName);
            var request2 = (FtpWebRequest)WebRequest.Create(q.serverlink + q.path2 + f.FileName);
            request.Credentials = new NetworkCredential(q.ftpusername, q.ftppassword);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request2.Credentials = new NetworkCredential(q.ftpusername, q.ftppassword);
            request2.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                Thread.Sleep(1000);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                FtpWebResponse response2 = (FtpWebResponse)request2.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

    }



    public class DeleteParametr
    {
        public int FtpID { get; set; }
        public string FileName { get; set; }

    }

    public class UploadParametr
    {
        public string ServiceName { get; set; }
        public Stream File { get; set; }
        public string FileName { get; set; }
        public int FtpID { get; set; }

    }
    public class FileExistParametr
    {
        public int FTPID { get; set; }
        public string FileName { get; set; }


    }
}