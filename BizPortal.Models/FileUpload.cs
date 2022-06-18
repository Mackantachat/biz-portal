using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class FileUpload : ManipulateModel
    {
        public int FileUploadID { get; set; }

        [StringLength(512)]
        [Required]
        public string FileSysName { get; set; }

        [StringLength(512)]
        [Required]
        public string FileName { get; set; }

        [StringLength(1024)]
        [Required]
        public string AbsolutePath { get; set; }

        [StringLength(512)]
        [Required]
        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        public bool TemporaryStatus { get; set; }
    }
}
