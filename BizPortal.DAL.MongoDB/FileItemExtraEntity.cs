using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;

namespace BizPortal.DAL.MongoDB
{
    public class FileMetadataEntity : Entity
    {
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี (นิติบุคคล, บุคคลธรรมดา)
        /// </summary>
        public string IdentityID { get; set; }
        /// <summary>
        /// ชื่อผู้ขอใช้บริการ
        /// </summary>
        public string IdentityName { get; set; }
        /// <summary>
        /// ประเภทผู้ขอใช้บริการ
        /// </summary>
        public UserTypeEnum IdentityType { get; set; }
        public int ApplicationID { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid ApplicationRequestID { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid FileGroupID { get; set; }


        /// <summary>
        /// สำหรับใช้ใน SingleForm
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public Guid TransactionID { get; set; }

        public string FileID { get; set; }
        public string FileURL { get; set; }
        public string GovDocumentName { get; set; }
        public string GovDocumentDescription { get; set; }
        public string FileReason { get; set; }
        public string FileName { get; set; }
        public string FileTypeCode { get; set; }
        public long FileSize { get; set; }
        public bool IsPublic { get; set; }
        public string ContentType { get; set; }
        public string EPermitFileName { get; set; }
        public string EPermitFileDescription { get; set; }
        public DateTime EPermitExpireDate { get; set; }
        //public string BillPaymentFileName { get; set; }
        public UploadStatus UploadStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Dictionary<string, object> Extras { get; set; }
        public bool IsPredocError { get; set; }
        public bool FileIsEnableUrl { get; set; }


        public string GetFileDisplayName(Dictionary<string, string> appTranslates = null)
        {
            var fileName = "";
            var fileDesc = (object)"";
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

                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = Utils.Helpers.ResourceHelper.GetResourceWordWithDefault(fileTypeCode, "Apps_SingleForm_Filelist", fileTypeCode);
                    }
                }
            }
            else
            {
                FileName = this.FileName;
            }

            if (this.Extras.TryGetValue("FILETYPEDESC", out fileDesc))
            {
                fileName = string.IsNullOrEmpty(fileName) ? fileDesc.ToString() : string.Format("{0} - {1}", fileName, fileDesc);
            }

            return fileName;
        }

        public static FileMetadataEntity Get(string id, Guid requestID)
        {
            var repo = MongoFactory.GetFileMetadataCollection().AsQueryable();
            var request = repo.Where(o => o.FileID == id && o.ApplicationRequestID == requestID).FirstOrDefault();
            return request;
        }

        public static FileMetadataEntity GetWithTransaction(string id, Guid transactionID)
        {
            var repo = MongoFactory.GetFileMetadataCollection().AsQueryable();
            var request = repo.Where(o => o.FileID == id && o.TransactionID == transactionID).FirstOrDefault();
            return request;
        }

        public void Update()
        {
            var repo = MongoFactory.GetFileMetadataCollection();
            repo.Update(this);
        }
    }
}
