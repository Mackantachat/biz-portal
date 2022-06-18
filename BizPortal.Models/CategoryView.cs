using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class CategoryView
    {
        [Key, Column(Order = 1)]
        public int CategoryID { get; set; }

        [Key, Column(Order = 2)]
        public string TwoLetterISOLanguageName { get; set; }

        public int SectionID { get; set; }

        public bool Published { get; set; }

        public int? Ordering { get; set; }

        public int? ThumbnailID { get; set; }

        public string CreatedUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUserID { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CategorySysName { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string CreatedUserName { get; set; }

        public string UpdatedUserName { get; set; }

        public string SectionSysName { get; set; }

        public string SectionName { get; set; }

        public bool Unlisted { get; set; }
    }
}
