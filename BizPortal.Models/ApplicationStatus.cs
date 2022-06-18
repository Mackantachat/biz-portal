using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class ApplicationStatus
    {
        public int ApplicationStatusID { get; set; }

        [Required, StringLength(450)]
        public string ApplicationSysStatusName { get; set; }


        public virtual ICollection<ApplicationStatusTranslation> ApplicationStatusTranslations { get; set; }
    }
}
