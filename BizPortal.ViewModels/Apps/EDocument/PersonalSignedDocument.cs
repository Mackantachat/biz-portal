﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class PersonalSignedDocument
    {
        public string ApplicationName { get; set; }
        public string IdentityName { get; set; }
        public string DocumentID { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? SignedDate { get; set; }
        public Guid ApplicationRequestID { get; set; }
        public string Remark { get; set; }
    }
}
