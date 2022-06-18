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
    public class SectionTranslation : TranslationModel
    {
        public int SectionTranslationID { get; set; }

        public int SectionID { get; set; }

        [Required, StringLength(450)]
        public string SectionName { get; set; }

        [Column(TypeName="ntext")]
        public string Description { get; set; }

        public virtual Section Section { get; set; }
    }
}
