using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleFormAttachmentViewModel
    {
        public string FileName { get; set; }
        public string FileGroup { get; set; }
        public string FileReason { get; set; }
        public string Note { get; set; }
        public string Note_2 { get; set; }
        public string Note_3 { get; set; }
        public string DocFormUrl { get; set; }
        public string AddItemBtnText { get; set; }
        public bool Required { get; set; }

        public bool PreDoc { get; set; }
        public string PreDocFileName { get; set; }
        public string PreDocType { get; set; }
        public string PreDocOrg { get; set; }

        #region [SpecificFileName]
        public bool DisplaySpecificFileName { get; set; }
        public List<string> SpecificFileNames { get; set; }
        #endregion

        //--// Frontis
        /// <summary>
        /// File name for lookup in Resource file (if this field is null, FileName will be used for display)
        /// </summary>
        public string FileNameDisplay { get; set; }

        /// <summary>
        /// Strings to be subtitute in case there is string format in FileNameDisplay
        /// </summary>
        public List<string> FormatValues { get; set; }

        /// <summary>
        /// Show as "Add More Item" button
        /// </summary>
        public bool AddItemButton { get; set; }
        public int AddItemMin { get; set; }
        public int AddItemMax { get; set; }

        string originalFileName = null;
        public string OriginalFileName
        {
            get
            {
                if (originalFileName == null) return FileName;
                else return originalFileName;
            }
            set
            {
                originalFileName = value;
            }
        }

        public bool IsAddedItem { get; set; }
        public bool IsOptional { get; set; }
        public bool FileIsEnableUrl { get; set; }
        public string FileFilter { get; set; }


        public FileSizeConfig FileSize { get; set; }
        public List<string> FileConsumerKey { get; set; }
        public bool PreDocApphook { get; set; }

        public class FileSizeConfig
        {
            public string MaxFileSize { get; set; }
        }

    }
}
