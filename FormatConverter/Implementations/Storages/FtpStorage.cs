using FormatConverter.Interface;
using FormatConverter.Models;
using System;
using System.IO;
using System.Net;

namespace FormatConverter.Implementations.Storages
{
    public class FtpStorage : IStorage
    {
        private readonly StorageSettings storageSettings;
        private readonly NetworkCredential networkCredential;

        public FtpStorage(StorageSettings storageSettings)
        {
            this.storageSettings = storageSettings;
            this.networkCredential = new NetworkCredential(storageSettings.Username, storageSettings.Password);
        }
        public string Read()
        {
            using (var request = new WebClient())
            {
                request.Credentials = networkCredential;
                try
                {
                    byte[] file = request.DownloadData(new Uri(storageSettings.Server + storageSettings.Path));
                    string fileString = System.Text.Encoding.UTF8.GetString(file);
                    return fileString;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public void Write(string content)
        {
            var filePath = storageSettings.Server + storageSettings.Path;
            var dirPath = storageSettings.Server + Path.GetDirectoryName(storageSettings.Path);
            var byteContent = System.Text.Encoding.UTF8.GetBytes(content);



            WebClient client = new WebClient();
            client.Credentials = networkCredential;
            if (!FtpDirectoryExists(dirPath))
            {
                FtpCreateDirectory(dirPath);
            }
            client.UploadData(filePath, WebRequestMethods.Ftp.UploadFile, byteContent);
        }
        public bool FtpDirectoryExists(string dirPath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirPath);
                request.Credentials = networkCredential;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }
        public bool FtpCreateDirectory(string dirPath)
        {
            try
            {
                WebRequest request = WebRequest.Create(dirPath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = networkCredential;
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }
    }
}