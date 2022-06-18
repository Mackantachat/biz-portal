using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Tag : ManipulateModel
    {
        public int TagID { get; set; }

        [Required, StringLength(450)]
        public string TagName { get; set; }

        public int Count { get; set; }
    }
}
