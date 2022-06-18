using BizPortal.ViewModels.SingleForm;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormSectionDataEntity : ISectionData
    {
        public SingleFormSectionDataEntity()
        {
            FormData = new Dictionary<string, object>();
            ArrayData = new List<Dictionary<string, object>>();
        }

        public string SectionName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SectionType Type { get; set; }

        public Dictionary<string, object> FormData { get; set; }

        public List<Dictionary<string, object>> ArrayData { get; set; }
    }
}
