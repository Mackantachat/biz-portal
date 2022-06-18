using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleFormAppsViewModel
    {
        public int ApplicationID { get; set; }

        public string AppSysName { get; set; }

        public string AppName { get; set; }

        public bool isChecked { get; set; }

        public string OrganizationName { get; set; }

        public bool HideBizPortalSoon { get; set; }
        public bool OnlineRequestAllowed { get; set; }
        public string[] NotAllowedReason { get; set; }
        public string[] Warning { get; set; }
        public int? LogoFileID { get; set; }
        public string ApplicationUrl { get; set; }
        public string HandbookUrl { get; set; }
        public int? OperatingDays { get; set; }
        public decimal? OperatingCost { get; set; }
        public string OperatingCostType { get; set; }
        public decimal? OperatingCost2 { get; set; }
        public string OperatingDaysType { get; set; }
        public int? OperatingDays2 { get; set; }
        public bool ShowRemark { get; set; }

        public string Remark { get; set; }

        public bool CitizenShowRemark { get; set; }

        public string CitizenRemark { get; set; }
        
        public bool TemporaryDisable { get; set; }

        public string TemporaryRemark { get; set; }

        public string AppsHookClassName { get; set; }
    }
}
