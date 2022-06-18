using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Business
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public int ApplicationID { get; set; }
        [Required]
        public string AppSysName { get; set; }
        [Required]
        public string BusinessSysName { get; set; }
        public string BusinessNameTH { get; set; }
        public string BusinessNameEN { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime UpdatedDate { get; set; }
    }
}
