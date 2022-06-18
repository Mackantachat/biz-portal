using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DPTStandard
{
    public class DPTFileMetaData
    {
        /// <summary>
        /// FileName ชื่อไฟล์ที่อัพโหลด
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// FileTypeCode
        /// </summary>
        public string FileTypeCode { get; set; }
        /// <summary>
        /// FileID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// FileSize
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// FileTypeCode ที่แปลเป็นภาษาไทย
        /// </summary>
        public string DocName { get; set; }
        /// <summary>
        /// Extras
        /// </summary>
        public Dictionary<string, object> Extras { get; set; }
        /// <summary>
        /// fileUrl ไฟล์แนบเป็น google drive หรือ DrobBox
        /// </summary>
        public string FileUrl { get; set; }

    }

    public class DPTEADocument : DPTEngineerArchitect
    {

        /// <summary>
        /// หนังสือแสดงความยินยอมและรับรองจากวิศวกรรม/สถาปนิก
        /// </summary>
        public DPTFileMetaData[] ConsentForms { get; set; }
        /// <summary>
        /// สำเนาใบอนุญาตเป็นผู้ประกอบวิชาชีพวิศวกรรม/สถาปนิก
        /// </summary>
        public DPTFileMetaData[] LicenseForms { get; set; }
    }
}
