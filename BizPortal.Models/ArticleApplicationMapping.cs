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
    public class ArticleApplicationMapping
    {
        [Key, ForeignKey("Application"), Column(Order = 1)]
        public int ApplicationID { get; set; }

        [Key, ForeignKey("Article"), Column(Order = 2)]
        public int ArticleID { get; set; }


        public virtual Application Application { get; set; }

        public virtual Article Article { get; set; }
    }
}
