using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class TSICgroup
    {
        public int TSICgroupID { get; set; }

        public int TSICsubCategoryID { get; set; }

        [StringLength(3)]
        [Required]
        public string GroupCode { get; set; }

        [StringLength(450)]
        [Required]
        public string GroupName { get; set; }

        public virtual TSICsubCategory TSICsubCategory { get; set; }
    }
}
