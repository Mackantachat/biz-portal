using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class EDocumentEntity : Entity
    {
        public EDocumentEntity()
        {
            var dtNow = DateTime.Now;
            CreatedDate = UpdatedDate = dtNow;
            SigningStatus = EDocumentStatus.NEW;
        }

        /// <summary>
        /// ApplicationID
        /// </summary>
        public int ApplicationID { get; set; }

        /// <summary>
        /// Foreign key ที่เชื่อมกับ ApplicationRequests
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public Guid ApplicationRequestID { get; set; }

        /// <summary>
        /// DocumentID ที่ได้จาก EDocument API
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// DocumentID ที่ได้จาก EDocument API
        /// </summary>
        public string DocumentID { get; set; }

        /// <summary>
        /// DocumentName ที่ได้จาก Config หรือ API หน่วยงาน
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// DocumentUrl ที่ได้จากหน่วยงานใช้ file ตัวเอง 
        /// </summary>
        public string DocumentUrl { get; set; }

        /// <summary>
        /// ประเภทของการ Signing
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public EDocumentType SigningType { get; set; }

        /// <summary>
        /// สถานะการ Signing
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public EDocumentStatus SigningStatus { get; set; }

        /// <summary>
        /// รายชื่อบุคคลที่ต้องลงนาม กรณีลงนามแบบรายบุคคล
        /// </summary>
        public List<EDocumentPersonalSigner> PersonalSigners { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Get an request by ApplicationRequestID sorting by DESC CreatedDate
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static EDocumentEntity Get(Guid ApplicationRequestID)
        {
            var request = MongoFactory.GetEDocumentCollection()
                .Find(o => o.ApplicationRequestID == ApplicationRequestID && o.SigningStatus != EDocumentStatus.DELETED)
                .SortByDescending(o => o.CreatedDate)
                .FirstOrDefault();
            return request;
        }

        /// <summary>
        /// Get an request by DocumentID sorting by DESC CreatedDate
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static EDocumentEntity Get(string DocumentID)
        {
            var request = MongoFactory.GetEDocumentCollection()
                .Find(o => o.DocumentID == DocumentID)
                .SortByDescending(o => o.CreatedDate)
                .FirstOrDefault();
            return request;
        }

        /// <summary>
        /// Get all request by ApplicationRequestID sorting by DESC CreatedDate
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static List<EDocumentEntity> GetAll(Guid ApplicationRequestID)
        {
           return MongoFactory.GetEDocumentCollection()
                              .Find(o => o.ApplicationRequestID == ApplicationRequestID && o.SigningStatus != EDocumentStatus.DELETED)
                              .SortBy(o => o.CreatedDate)
                              .ToList();
        }

        /// <summary>
        /// List all requests of IdentityID
        /// </summary>
        /// <param name="IdentityID"></param>
        /// <returns></returns>
        public static List<EDocumentEntity> List(string IdentityID)
        {
            var requests = MongoFactory.GetEDocumentCollection()
                .Find(o => o.PersonalSigners != null && o.PersonalSigners.Count > 0 && o.PersonalSigners.Any(x => x.IdentityID == IdentityID))
                .ToList();
            return requests;
        }

        public void Create()
        {
            MongoFactory.GetEDocumentCollection().InsertOne(this);
        }

        public void Update()
        {
            MongoFactory.GetEDocumentCollection().Update(this);
        }

        public void Delete()
        {
            MongoFactory.GetEDocumentCollection().Delete(this);
        }
    }

    public class EDocumentPersonalSigner
    {
        public EDocumentPersonalSigner()
        {
            PersonalSigningStatus = PersonalSigningStatus.NEW;
        }

        public int SigningOrder { get; set; }

        public string IdentityID { get; set; }

        public string IdentityName { get; set; }

        public string IdentityFirstName { get; set; }

        public string IdentityLastName { get; set; }

        public string IdentityPosition { get; set; }

        [BsonRepresentation(BsonType.String)]
        public PersonalSigningStatus PersonalSigningStatus { get; set; }

        public string PersonalSigningRemark { get; set; }

        public string SignatureID { get; set; }

        public string SignatureWidth { get; set; }

        public string SignatureHeight { get; set; }

        public string SignatureLeft { get; set; }

        public string SignatureBottom { get; set; }

        public string SignatureBase64 { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? SignedDate { get; set; }

    }
}
