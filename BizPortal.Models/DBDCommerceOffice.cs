using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
   public class DBDCommerceOffice
    {
        [Key,Required,StringLength(5)]
        [Column(Order = 0)]
        public string Code { get; set; }

        public string OfficeNameTh { get; set; }

        public string DisplayOfficeTH { get; set; }

        public string OfficeType_description { get; set; }

        public string OfficeType_descriptionShort { get; set; }

        [StringLength(3)]
        public string OfficeCode { get; set; }

        [StringLength(2)]
        public string AmphurCode { get; set; }



        [Key,Required, StringLength(2)]
        [Column(Order = 1)]
        public string ProvinceCode { get; set; }

        [StringLength(1)]
        public string ActiveFlag { get; set; }
    }
}
