using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DBDStandard
{
    public class BaseFileMetaData
    {
        public string fileName { get; set; }
        public string fileSize { get; set; }
        public string contentType { get; set; }
        public string base64Sting { get; set; }
    }
}
