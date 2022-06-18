using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class DBDCommerceObjective
    {
        [Key, Required, StringLength(5)]
        public string Code { get; set; }

        public string DescriptionTh { get; set; }

        public string DisplayObjectiveTH { get; set; }

        [StringLength(1)]
        public string ActiveFlag { get; set; }

        [StringLength(1)]
        public string CommerceFlag { get; set; }

    }
}
