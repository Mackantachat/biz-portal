using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class CompanyTranslation : TranslationModel
    {
        public int CompanyTranslationID { get; set; }

        public int CompanyID { get; set; }

        [Required, StringLength(450)]
        public string CompanyName { get; set; }


        public virtual Company Company { get; set; }
    }
}
