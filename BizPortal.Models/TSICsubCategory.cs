using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class TSICsubCategory
    {
        public int TSICsubCategoryID { get; set; }

        public int TSICcategoryID { get; set; }

        [StringLength(2)]
        [Required]
        public string SubCategoryCode { get; set; }

        [StringLength(450)]
        [Required]
        public string SubCategoryName { get; set; }

        public virtual TSICcategory TSICcategory { get; set; }
    }
}
