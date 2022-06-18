using System;
using System.Collections.Generic;
using BizPortal.Utils.Extensions;

namespace BizPortal.ViewModels.V2
{
    public class FileExplorerViewModel
    {
        public int ApplicationID { get; set; }
        public string ContentType { get; set; }
        public string FileID { get; set; }
        public long FileSize { get; set; }
        public bool IsPublic { get; set; }
        public string FileName { get; set; }
        public string FileTypeCode { get; set; }
        public string FileTypeName { get; set; }
        public int UploadStatus { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Extras { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region [Display Only]
        public string CreatedDateTxt { get { return CreatedDate.ToString("dd/MM/yyyy"); } }

        public string UpdatedDateTxt { get { return UpdatedDate.ToString("dd/MM/yyyy"); } }

        public string ApplicationName { get; set; }
        #endregion
    }
}
