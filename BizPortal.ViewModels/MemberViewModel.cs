using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class MemberViewModel
    {
        [Display(Name = "USERNAME", ResourceType = typeof(Resources.Member))]
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Display(Name = "ROLES", ResourceType = typeof(Resources.Member))]
        public IEnumerable<string> RoleIds { get; set; }

        public IEnumerable<string> RoleDescriptions { get; set; }

        public string Id { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "FULLNAME", ResourceType = typeof(Resources.Member))]
        public string FullName { get; set; }

        [Display(Name = "USERTYPE", ResourceType = typeof(Resources.Member))]
        public string UserType { get; set; }

        [Display(Name = "ORGANIZATION", ResourceType = typeof(Resources.Global))]
        public string OrgName { get; set; }

        [Display(Name = "ORGANIZATION", ResourceType = typeof(Resources.Global))]
        public string OrgCode { get; set; }

        public IEnumerable<MemberServiceViewModel> Services { get; set; }

        public IEnumerable<int> ManageServices { get; set; }

    }

    public class MemberManageServiceViewModel
    {
        public int ServiceID { get; set; }

    }

    public class MemberServiceViewModel
    {
        public string OrgCode { get; set; }

        public string OrgName { get; set; }

        public int ServiceId { get; set; }

        public string Service { get; set; }

        public IEnumerable<MemberServiceAreaViewModel> Areas { get; set; }

        public bool IsCanManage { get; set; }
    }

    public class MemberServiceAreaViewModel
    {
        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int SectionId { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Section { get; set; }

    }
}
