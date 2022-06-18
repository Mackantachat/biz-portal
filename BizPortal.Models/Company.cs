using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [Required, StringLength(450)]
        public string CompanySysName { get; set; }

        [Required, StringLength(13), Index(IsUnique = true)]
        public string CompanyTaxID { get; set; }


        public virtual ICollection<CompanyTranslation> CompanyTranslations { get; set; }
    }
}
