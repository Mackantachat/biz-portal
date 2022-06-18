using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public class InvokeResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public bool DisabledSendingSystemEmail { get; set; }

        public string SendToEmail { get; set; }

        public List<string> CcToEmails { get; set; }

        public Exception Exception { get; set; }

        public AppHookInfo Data { get; set; }
    }
}
