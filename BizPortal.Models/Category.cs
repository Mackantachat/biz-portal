using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Category : CmsModel
    {
        public int CategoryID { get; set; }

        [Required, StringLength(450)]
        public string CategorySysName { get; set; }

        public int SectionID { get; set; }

        public bool Unlisted { get; set; }


        public virtual Section Section { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
