using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DPTStandard
{
    public class A1LicenseInfo
    {
        public int PurposeType { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseReleasedDate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public string Name { get; set; }
        public DPTAddress Address { get; set; }
    }
}
