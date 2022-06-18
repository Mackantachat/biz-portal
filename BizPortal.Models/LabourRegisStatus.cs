using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class LabourRegisStatus
    {
        public int LabourRegisStatusID { get; set; }

        [Display(Name = "เลขนิติบุคคล 13 หลัก")]
        [Required, StringLength(13), RegularExpression(@"\d{13}")]
        public string JuristicID { get; set; }

        [Display(Name = "หมายเลขอ้างอิง")]
        [Required]
        public string RefCode { get; set; }

        [Required]
        [EmailAddress()]
        public string Email { get; set; }

        public long TransactionTimestamp { get; set; }

        public bool Status { get; set; }

        public int ApplicationID { get; set; }


        public virtual Application Application { get; set; }
    }
}
