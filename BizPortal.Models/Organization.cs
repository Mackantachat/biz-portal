using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Organization
    {
        [Key, Required, StringLength(450)]
        public string OrgCode { get; set; }

        [Required, StringLength(450)]
        public string OrgSysName { get; set; }

        [StringLength(10)]
        public string MinistryCode { get; set; }

        [StringLength(10)]
        public string DepartmentCode { get; set; }

        [StringLength(10)]
        public string DivisionCode { get; set; }

        [StringLength(1000)]
        public string LogoUrl { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }


        public virtual ICollection<OrganizationTranslation> OrganizationTranslations { get; set; }
    }
}
