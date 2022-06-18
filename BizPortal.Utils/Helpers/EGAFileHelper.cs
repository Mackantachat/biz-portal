using EGA.EGA_FileService.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class EGAFileHelper
    {
        /// <summary>
        /// ลบไฟล์จาก File.EGA
        /// </summary>
        /// <param name="fileIDs"></param>
        public static void RemoveFiles(string[] fileIDs)
        {
            try
            {
                // try to delete files on file.ega.or.th
                string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceDeleteTokenPath"];
                string servicePath = ConfigurationManager.AppSettings["FileServicePath"];
                string consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
                string secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

                var fileInfoJson = JsonConvert.SerializeObject(fileIDs);
                string token = ServiceHelper.RequestDeleteToken(serviceTokenPath, consumerKey, secret, fileIDs);
                ServiceHelper.DeleteFile(servicePath, token, consumerKey, secret, fileIDs);
            }
            catch { }
        }
    }
}
