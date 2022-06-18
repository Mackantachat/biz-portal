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
    public class Article : CmsModel
    {
        public int ArticleID { get; set; }

        [Required, StringLength(450)]
        public string ArticleSysName { get; set; }

        /// <summary>
        /// ไม่ถูกแสดงในลิสต์
        /// </summary>
        public bool Unlisted { get; set; }

        public int? SectionID { get; set; }

        public int? CategoryID { get; set; }

        public DateTime? StartPublishing { get; set; }

        public DateTime? FinishPublishing { get; set; }

        public int Reads { get; set; }

        [ForeignKey("PreviousArticle")]
        public int? PreviousArticleID { get; set; }

        [ForeignKey("NextArticle")]
        public int? NextArticleID { get; set; }


        public virtual Article PreviousArticle { get; set; }

        public virtual Article NextArticle { get; set; }

        public virtual Section Section { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ArticleTranslation> ArticleTranslations { get; set; }

        [InverseProperty("PreviousArticle")]
        public virtual ICollection<Article> PreviousArticles { get; set; }

        [InverseProperty("NextArticle")]
        public virtual ICollection<Article> NextArticles { get; set; }
    }
}
