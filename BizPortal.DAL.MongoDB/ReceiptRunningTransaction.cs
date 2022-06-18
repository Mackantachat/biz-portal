using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BizPortal.DAL.MongoDB
{
    public class ReceiptRunningTransaction : Entity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ReceiptID { get; set; }
        public string OrgCode { get; set; }
        public int RunningNumber { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public string Filename { get; set; }
        public string FileId { get; set; }
        public string Memo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool ActiveFlag { get; set; }

        public static void Init()
        {
            MongoFactory.GetDatabase().CreateCollection("ReceiptRunningTransaction");
        }

        public static ReceiptRunningTransaction GetByApplicationRequestNumber(string ApplicationRequestNumber)
        {
            var repo = MongoFactory.GetReceiptRunningTransactionCollection().AsQueryable();
            var request = repo.Where(o => o.ApplicationRequestNumber == ApplicationRequestNumber).FirstOrDefault();
            return request;
        }

        public static ReceiptRunningTransaction GetLastByOrgCode(string orgCode)
        {
            var repo = MongoFactory.GetReceiptRunningTransactionCollection().AsQueryable();
            var request = repo.Where(o => o.OrgCode == orgCode).OrderByDescending(x => x.RunningNumber).FirstOrDefault();
            return request;
        }

        public void Update()
        {
            var repo = MongoFactory.GetReceiptRunningTransactionCollection();
            repo.Update(this);
        }
    }
    
}
