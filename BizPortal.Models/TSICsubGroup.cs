using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class TSICsubGroup
    {
        public int TSICsubGroupID { get; set; }

        public int TSICgroupID { get; set; }

        [StringLength(4)]
        [Required]
        public string SubGroupCode { get; set; }

        [StringLength(450)]
        [Required]
        public string SubGroupName { get; set; }

        public virtual TSICgroup TSICgroup { get; set; }
    }
}
