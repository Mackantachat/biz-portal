using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class ApplicationRequestChatEntity : Entity
    {
        public ApplicationRequestChatEntity()
        {
            ChatID = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }

        [BsonRepresentation(BsonType.String)]
        public Guid? ChatID { get; set; }
        public string ChatUserID { get; set; }
        public string ChatUserFullName { get; set; }
        public string ChatUserType { get; set; }
        public string ChatText { get; set; }
        public DateTime CreateDate { get; set; }
        
    }
}
