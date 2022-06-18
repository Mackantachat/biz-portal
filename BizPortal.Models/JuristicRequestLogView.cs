using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class JuristicRequestLogView
    {
        [Key, Column(Order = 1)]
        public int JuristicApplicationStatusRequestLogID { get; set; }

        [Key, Column(Order = 2)]
        public string TwoLetterISOLanguageName { get; set; }

        public int JuristicApplicationStatusRequestID { get; set; }
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
        public string ApplicationStatusName { get; set; }
    }
}
