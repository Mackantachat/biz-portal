using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormRequestBasedEntity : Entity
    {
        public string IdentityID { get; set; }

        public string IdentityName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public UserTypeEnum IdentityType { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ApplicationStatusV2Enum Status { get; set; }

        public List<SingleFormSectionDataEntity> SectionData { get; set; }

    }
}
