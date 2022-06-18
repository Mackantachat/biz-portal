using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class JuristicRequestView
    {
        [Key, Column(Order = 1)]
        public int JuristicApplicationStatusRequestID { get; set; }

        [Key, Column(Order = 2)]
        public string TwoLetterISOLanguageName { get; set; }
        public int ApplicationID { get; set; }

        public string JuristicID { get; set; }

        public string Remark { get; set; }
        public int ApplicationStatusID { get; set; }
        public string ApplicationStatusOther { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedUserID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string ApplicationSysStatusName { get; set; }
        public string ApplicationStatusName { get; set; }
        public string ApplicationSysName { get; set; }

        public string OrgCode { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDetail { get; set; }

        public string ApplicationUrl { get; set; }

        public string OrgName { get; set; }

        public string Abbreviation { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string CitizenID { get; set; }
        public string PassportID { get; set; }
        public Guid ConsumerKey { get; set; }
    }
}
