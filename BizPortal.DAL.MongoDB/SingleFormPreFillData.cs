using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormPreFillData : Entity
    {
        public string IdentityID { get; set; }

        public string ReferenceID { get; set; }

        public object Data { get; set; }

        public DateTime CreatedDate { get; set; }

        public static SingleFormPreFillData Get(string IdentityID, string ReferenceID)
        {
            var db = MongoFactory.GetSingleFormPreFillDataCollection().AsQueryable();
            var request = db.Where(o => o.IdentityID == IdentityID && o.ReferenceID == ReferenceID)
                            .OrderByDescending(o=> o.CreatedDate)
                            .FirstOrDefault();

            return request;
        }

        public void Create()
        {
            try
            {
                CreatedDate = DateTime.Now;
                var repo = MongoFactory.GetSingleFormPreFillDataCollection().AsQueryable();
                MongoFactory.GetSingleFormPreFillDataCollection().InsertOne(this);
            }
            catch (Exception)
            {

            }
        }

        public void Delete()
        {
            var repo = MongoFactory.GetSingleFormPreFillDataCollection();
            repo.Delete(this);
        }

        public void Delete(string identityID)
        {
            var repo = MongoFactory.GetSingleFormPreFillDataCollection();
            var data = repo.Find(e => e.IdentityID == identityID).ToList();

            if (data != null)
            {
                foreach (var d in data)
                {
                    repo.Delete(d);

                }
            }
        }

        public void Delete(string identityID, string referenceID)
        {
            var repo = MongoFactory.GetSingleFormPreFillDataCollection();
            repo.Delete(Get(IdentityID, ReferenceID));
        }

    }
}
