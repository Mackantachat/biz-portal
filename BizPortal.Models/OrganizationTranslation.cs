using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class OrganizationTranslation : TranslationModel
    {
        public int OrganizationTranslationID { get; set; }

        [Required, ForeignKey("Organization")]
        public string OrgCode { get; set; }

        [Required, StringLength(450)]
        public string OrgName { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(450)]
        public string Abbreviation { get; set; }


        public virtual Organization Organization { get; set; }
    }
}
