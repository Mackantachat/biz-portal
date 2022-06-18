using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class DbdTitle
    {
        public int DbdTitleID { get; set; }

        [Required, StringLength(450)]
        public string TitleName { get; set; }

        [Required, StringLength(450)]
        public string TitleAbbreviation { get; set; }
    }
}
