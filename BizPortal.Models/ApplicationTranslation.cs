using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class ApplicationTranslation : TranslationModel
    {
        public int ApplicationTranslationID { get; set; }

        public int ApplicationID { get; set; }

        [Required, StringLength(450)]
        public string ApplicationName { get; set; }

        [StringLength(1000)]
        public string ApplicationDetail { get; set; }

        [Column(TypeName = "ntext")]
        public string ApprovedMailMessage { get; set; }

        [Column(TypeName = "ntext")]
        public string RejectedMailMessage { get; set; }

        [Column(TypeName = "ntext")]
        public string SubmitMailMessage { get; set; }

        public virtual Application Application { get; set; }
    }
}
