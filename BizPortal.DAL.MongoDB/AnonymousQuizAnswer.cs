using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System.Collections.Specialized;

namespace BizPortal.DAL.MongoDB
{
    /// <summary>
    /// For storing Quiz answer of anonymous user
    /// </summary>
    public class AnonymousQuizAnswer : Entity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid QaID { get; set; }

        public Dictionary<string, string> Collection { get; set; }

        public DateTime CreatedTime { get; set; }
    }

}
