using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class SerivceCategoryList
    {

        [Key, Required, StringLength(10)]
        public string SerivceCategoryCODE { get; set; }
        [Required]
        public string SerivceCategoryDescription { get; set; }
        [Required]
        public string SerivceCategoryUrl { get; set; }
        public string SerivceCategoryGroup { get; set; }
        [Required]
        public string ConsumerKey { get; set; }
        [Required]
        public string ConsumerSecret { get; set; }
        [Required]
        public string StructureData { get; set; }
        [Required]
        public bool IsActive { get; set; }
       
    }
}
