using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class OrgByPersonApproveRequest
    {
        public string[] DocumentIDs { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
