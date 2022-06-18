using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BizPortal.DAL.MongoDB
{
    [BsonIgnoreExtraElements]
    public class ApplicationRequestDataGroupEntity
    {
        public ApplicationRequestDataGroupEntity()
        {
            Visible = true;
            Data = new Dictionary<string, string>();
        }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public bool Visible { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
