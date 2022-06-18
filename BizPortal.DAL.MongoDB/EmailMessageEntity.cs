using System.Collections.Generic;

namespace BizPortal.DAL.MongoDB
{
    public class EmailMessageEntity
    {
        public string SendFromName { get; set; }

        public string SendFromEmail { get; set; }

        public string ReplyToName { get; set; }

        public string ReplyToEmail { get; set; }

        public string SendToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtmlBody { get; set; }

        public List<FileMetadataEntity> Attachments { get; set; }
    }
}
