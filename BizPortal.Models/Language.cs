using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class Language
    {
        public int LanguageID { get; set; }

        [Required, StringLength(450)]
        public string LanguageName { get; set; }

        [Required, StringLength(2), Index(IsUnique = true)]
        public string TwoLetterISOLanguageName { get; set; }
    }
}
