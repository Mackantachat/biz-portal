using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.V2
{
    public class ChatViewModel
    {
        public int ApplicationID { get; set; }

        public Guid ApplicationRequestID { get; set; }

        public string ChatUserID { get; set; }

        public string ChatUserFullName { get; set; }

        public string ChatUserType { get; set; }

        public string ChatText { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
