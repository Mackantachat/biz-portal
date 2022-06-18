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
    public class JuristicApplicationStatusRequestLog : ManipulateModel
    {
        public int JuristicApplicationStatusRequestLogID { get; set; }

        [Required, ForeignKey("JuristicApplicationStatusRequest")]
        public int JuristicApplicationStatusRequestID { get; set; }

        [StringLength(1000)]
        public string Remark { get; set; }
        [Required, ForeignKey("ApplicationStatus")]
        public int? ApplicationStatusID { get; set; }

        [StringLength(450)]
        public string ApplicationStatusOther { get; set; }

        /// <summary>
        /// Transaction Code / Transaction ID (optional) : เลขที่ หรือ รหัสคำขอ (ถ้ามี) 
        /// เผื่ออีกหน่อยต้องผูกธุรกรรม เช่น ระบบของหน่วยงานดึง eForm จากระบบเรา แล้วอัพเดทสถานะผ่าน api
        /// </summary>
        [StringLength(450)]
        public string TransactionCode { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }
        public virtual JuristicApplicationStatusRequest JuristicApplicationStatusRequest { get; set; }
    }
}
