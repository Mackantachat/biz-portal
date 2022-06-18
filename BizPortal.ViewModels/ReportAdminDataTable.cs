using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BizPortal.ViewModels
{
    public class ReportAdminDataTable 
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        //public string RoleName { get; set; }
        //public string RoleDescription { get; set; }
        public List<IdentityUserRole> Roles { get; set; }
        public string FullName { get; set; }
        public string OrgName { get; set; }
        
    }
}
