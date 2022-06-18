using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class TSICcode
    {
        public int TSICcodeID { get; set; }

        public int TSICsubGroupID { get; set; }

        [StringLength(5)]
        [Required]
        public string TSICNumber { get; set; }

        [StringLength(450)]
        [Required]
        public string TSICName { get; set; }

        public decimal TSICMultiple { get; set; }

        public virtual TSICsubGroup TSICsubGroup { get; set; }
    }
}
