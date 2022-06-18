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
    public class CategoryTranslation : TranslationModel
    {
        public int CategoryTranslationID { get; set; }

        public int CategoryID { get; set; }

        [Required, StringLength(450)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}
