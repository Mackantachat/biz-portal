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
    public class ArticleTranslation : TranslationModel
    {
        public int ArticleTranslationID { get; set; }

        public int ArticleID { get; set; }

        [Required, StringLength(450)]
        public string ArticleName { get; set; }

        [Column(TypeName = "ntext")]
        public string ArticleBody { get; set; }

        [StringLength(1000)]
        public string ArticleIntroText { get; set; }


        public virtual Article Article { get; set; }
    }
}
