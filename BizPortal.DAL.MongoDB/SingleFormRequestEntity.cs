using BizPortal.Utils.Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormRequestEntity : SingleFormRequestBasedEntity
    {
        public SingleFormRequestEntity()
        {
            SectionData = new List<SingleFormSectionDataEntity>();
        }

        public static SingleFormRequestEntity Get(string identityID)
        {
            var db = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
            var request = db.Where(o => o.IdentityID == identityID).SingleOrDefault();

            return request;
        }

        // Get for GetDraft
        public static SingleFormRequestEntity Get(string identityID, UserTypeEnum identityType, ApplicationStatusV2Enum[] statusIDs = null)
       {
            var db = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
            var requestQuery = db.Where(o => o.IdentityID == identityID && o.IdentityType == identityType);

            if (statusIDs != null)
            {
                requestQuery = requestQuery.Where(o => statusIDs.Contains(o.Status));
            }

            var request = requestQuery.SingleOrDefault();

            return request;
        }

        public void Create()
        {
            var db = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
            MongoFactory.GetSingleFormRequestCollection().InsertOne(this);
        }

        public void Update()
        {
            var db = MongoFactory.GetSingleFormRequestCollection();
            db.Update(this);
        }

        public void Delete()
        {
            var db = MongoFactory.GetSingleFormRequestCollection();
            db.Delete(this);
        }

        public void DeleteSections(string identityID, string applicationName = null, string[] SectionNames = null)
        {
            var db = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
            var request = db.Where(e => e.IdentityID == identityID).SingleOrDefault();

            if (!string.IsNullOrEmpty(applicationName))
            {
                request.SectionData.RemoveAll(e => e.SectionName.Contains(applicationName));
            }

            if (SectionNames != null)
            {
                request.SectionData.RemoveAll(e => SectionNames.Contains(e.SectionName));
            }

            request.Update();
        }
    }
}
