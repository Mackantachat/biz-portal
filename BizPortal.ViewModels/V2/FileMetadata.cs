using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BizPortal.ViewModels.V2
{
    public class FileMetadata
    {
        public string FileID { get; set; }
        
        public string FileURL { get; set; }

        public string GovDocumentName { get; set; }
        
        public string GovDocumentDescription { get; set; }
        
        public string EPermitFileName { get; set; }
        
        public string EPermitFileDescription { get; set; }
        
        public string EPermitExpireDate { get; set; }

        public string FileReason { get; set; }
        
        public string FileName { get; set; }

        public string FileTypeCode { get; set; }
        
        public string FileTypeName { get; set; }
        
        public long FileSize { get; set; }
        
        public bool IsPublic { get; set; }
        
        public string ContentType { get; set; }
        
        public int UploadStatus { get; set; }
        
        public Dictionary<string, string> Extras { get; set; }
        
        public string TYPE { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public bool IsPredocError { get; set; }
        
        public bool FileIsEnableUrl { get; set; }

        public string GetDownloadPath()
        {
            // File.EGA
            string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceDownloadTokenPath"];
            string serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
            string consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
            string secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

            string fileInfoJson = JsonConvert.SerializeObject(new { FileId = FileID });
            string token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);
            string downloadPath = string.Format("{0}{1}?accesstoken={2}", ConfigurationManager.AppSettings["FileServicePath"], FileID, token);

            return downloadPath;
        }

        public string GetDownloadToken()
        {
            // File.EGA
            string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceDownloadTokenPath"];
            string serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
            string fileConsumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
            string fileSecret = ConfigurationManager.AppSettings["FileConsumerSecret"];
            string fileInfoJson = JsonConvert.SerializeObject(new { FileId = FileID });

            return FileHelper.RequestAccessToken(serviceTokenPath, fileConsumerKey, fileSecret, fileInfoJson);
        }

        public byte[] GetBytes()
        {
            //var request = new RestRequest();
            //var client = new RestClient(GetDownloadPath());
            //byte[] buffer = client.DownloadData(request);

            byte[] buffer = FileHelper.DownloadFile(GetDownloadPath());

            return buffer;
        }

        public string GetBased64String()
        {
            string base64String = Convert.ToBase64String(GetBytes());
            return base64String;
        }

        public string GetFileDisplayName(Dictionary<string, string> appTranslates = null )
        {
            var fileName = "";
            var fileDesc = "";
            var fileTypeCode = this.FileTypeCode ?? (this.Extras != null ? this.Extras.TryGetString("FILETYPECODE") : null);

            if (this.Extras != null && this.Extras.ContainsKey("DISPLAYNAME"))
            {
                fileName = this.Extras["DISPLAYNAME"].ToString();
            }
            else if (this.Extras != null && this.Extras.ContainsKey("REQUEST_FILE_NAME"))
            {
                fileName = (string)this.Extras["REQUEST_FILE_NAME"];
            }
            else if (!string.IsNullOrEmpty(fileTypeCode))
            {
                if (appTranslates != null && appTranslates.ContainsKey(fileTypeCode))
                {
                    fileName = appTranslates[fileTypeCode];
                }
                else
                {
                    fileName = Utils.Helpers.ResourceHelper.GetResourceWordWithDefault(fileTypeCode.ToUpper(), "FileType", fileTypeCode);
                    fileName = Utils.Helpers.ResourceHelper.GetResourceWordWithDefault(fileTypeCode, "Apps_SingleForm_Filelist", fileTypeCode);
                }
            }
            else
            {
                FileName = this.FileName;
            }

            if (this.Extras.TryGetValue("FILETYPEDESC", out fileDesc))
            {
                fileName = string.IsNullOrEmpty(fileName) ? fileDesc : string.Format("{0} - {1}", fileName, fileDesc);
            }

            return fileName;
        }

    }
}
