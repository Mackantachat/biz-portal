using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BizPortal.DAL.MongoDB
{
    public class FileGroupEntity : Entity
    {
        public FileGroupEntity()
        {
            Files = new List<FileMetadataEntity>();
        }

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

        /// <summary>
        /// สำหรับใช้ใน SingleForm
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public Guid TransactionID { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid FileGroupID { get; set; }
        public string Description { get; set; }
        public List<FileMetadataEntity> Files { get; set; }
        public Dictionary<string, object> Extras { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public void Update()
        {
            var repo = MongoFactory.GetFileGroupCollection();
            repo.Update(this);

            if (Files != null)
            {
                foreach (var file in Files)
                {
                    file.Update();
                }
            }
        }
    }
}
