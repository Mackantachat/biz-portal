using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class MoiPostalCode
    {
        public int MoiPostalCodeID { get; set; }

        [Required]
        [StringLength(6)]
        [Index("IX_GEOCODE")]
        public string GeoCode { get; set; }

        [Required]
        [StringLength(5)]
        [Index("IX_POSTALCODE")]
        public string PostalCode { get; set; }
    }
}
