using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class InitLog : Entity
    {
        public int RevisionCode { get; set; }
        public string RevisionName { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDeployDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FinishDeployDate { get; set; }

        public string HostName { get; set; }
        public string IPAddress { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
