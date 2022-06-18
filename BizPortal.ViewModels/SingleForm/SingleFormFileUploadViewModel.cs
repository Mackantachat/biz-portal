using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleFormFileUploadViewModel
    {
        public string transaction_id { get; set; }
        public List<FileGroup> UploadedFiles { get; set; }

        public int fileCnt { get; set; }
        public int fileTotal { get; set; }
    }
}
