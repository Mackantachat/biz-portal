
using BizPortal.ViewModels.V2;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.StandardApi
{
    public class StandardApiFileInfo
    {
        public string Name;
        public byte[] Content;
        public string FileName;
        public string ContentType;

        public static StandardApiFileInfo From(FileMetadata f)
        {
            
            if (f.Extras != null && f.Extras.ContainsKey("PREDOC") && f.Extras["PREDOC"].ToString() == "true")
            {
                //TODO: Get Predoc content
                return null;
            }
            else
            {
                return new StandardApiFileInfo
                {
                    Name = string.IsNullOrEmpty(f.FileTypeCode) ? f.Extras["REQUEST_FILE_NAME"] : f.FileTypeCode,
                    Content = f.GetBytes(),
                    FileName = f.FileName,
                    ContentType = f.ContentType
                };
            }
        }
    }
}
