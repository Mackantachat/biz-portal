using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{    
    public class FileUploadResponseBeanViewModel
    {
        public int Total { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
        public List<FileUploadResponseViewModel> List { get; set; }
    }
    public class FileUploadResponseViewModel
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileType { get; set; }
        public string ConsumerKey { get; set; }
        public string FileConsumerNameDescription { get; set; }
        public string ApplicationID { get; set; }
        public string Owner { get; set; }
        public string Content { get; set; }
        public string[] ReadPermission { get; set; }
        public string[] WritePermission { get; set; }
        public string[] Tags { get; set; }
        public string Data { get; set; }
        public int FileRevision { get; set; }
        //public DateTime RegisterDate { get; set; }
        //public DateTime LastModified { get; set; }
        //public DateTime LastAccess { get; set; }
        public FileUploadResponseExtraViewModel Extras { get; set; }
    }

    public class FileUploadResponseExtraViewModel
    {
        public int Status { get; set; }
        public int JuristicRequestId { get; set; }
        public string  Type { get; set; }
        public string Name { get; set; }
        public int RequestType { get; set; }

    }
}
