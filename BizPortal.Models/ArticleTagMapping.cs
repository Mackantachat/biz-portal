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
    public class ArticleTagMapping
    {
        [Key, ForeignKey("Tag"), Column(Order = 1)]
        public int TagID { get; set; }

        [Key, ForeignKey("Article"), Column(Order = 2)]
        public int ArticleID { get; set; }

        public int LanguageID { get; set; }

        public virtual Language Language { get; set; }

        public virtual Tag Tag { get; set; }

        public virtual Article Article { get; set; }
    }
}
