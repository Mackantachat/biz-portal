using System;

namespace BizPortal.ViewModels.Report.Page3
{
    public class ApplicationModel
    {
        public DateTime? CreatedDate { get; set; }

        public string ApplicationRequestNumber { get; set; }

        public string PermitName { get; set; }

        public string IdentityName { get; set; }

        public object ExpectedFinishDate { get; set; }

        public string Status { get; set; }

        public string StatusOperation { get; set; }

        public string OrgName { get; set; }

        public string IdentityType { get; set; }

        public int? ProvinceID { get; set; }
        public int? AmphurID { get; set; }
        public int? ApplicationID { get; set; }
        public string OrganizationID { get; set; }

        public int? Year { get; set; }
    }

    public class AppStatusNbr
    {
        public string StrStatus { get; set; }

        public int StatusNbr { get; set; }
    }

    public class AppSepByOrg
    {
        public string OrgName { get; set; }
        public int AppSepOrg { get; set; }

    }

    public class AppSepByApp
    {
        public string AppName { get; set; }

        public int AppNbr { get; set; }

    }

    public class AppStatusCheck
    {
        public int CHECK { get; set; }

        public int PENDING { get; set; }

        public int APPROVED_WAITING_PAY_FEE { get; set; }

        public int PAID_FEE_CREATING_LICENSE { get; set; }
    }
}
