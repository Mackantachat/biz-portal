using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class ApplicationStatusTranslation : TranslationModel
    {
        public int ApplicationStatusTranslationID { get; set; }

        public int ApplicationStatusID { get; set; }

        [Required, StringLength(450)]
        public string ApplicationStatusName { get; set; }


        public virtual ApplicationStatus ApplicationStatus { get; set; }
    }
}
