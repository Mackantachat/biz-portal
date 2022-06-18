using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class TSICcategory
    {
        public int TSICcategoryID { get; set; }

        [StringLength(1)]
        [Required]
        public string CategoryCode { get; set; }

        [StringLength(450)]
        [Required]
        public string CategoryName { get; set; }
    }
}
