using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Section : CmsModel
    {
        public int SectionID { get; set; }

        [Required, StringLength(450)]
        public string SectionSysName { get; set; }

        public virtual ICollection<SectionTranslation> SectionTranslations { get; set; }
    }
}
