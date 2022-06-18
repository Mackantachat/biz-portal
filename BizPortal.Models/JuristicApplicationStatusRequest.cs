using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class JuristicApplicationStatusRequest : ManipulateModel
    {
        public int JuristicApplicationStatusRequestID { get; set; }

        public int ApplicationID { get; set; }

        public string JuristicID { get; set; }

        [StringLength(1000)]
        public string Remark { get; set; }

        public int? ApplicationStatusID { get; set; }

        [StringLength(450)]
        public string ApplicationStatusOther { get; set; }

        public bool NotLinkedToJuristicUser { get; set; }

        /// <summary>
        /// Transaction Code / Transaction ID (optional) : เลขที่ หรือ รหัสคำขอ (ถ้ามี) 
        /// เผื่ออีกหน่อยต้องผูกธุรกรรม เช่น ระบบของหน่วยงานดึง eForm จากระบบเรา แล้วอัพเดทสถานะผ่าน api
        /// </summary>
        [StringLength(450)]
        public string TransactionCode { get; set; }

        public bool MigratedToMongoDB { get; set; }

        public DateTime? MigratedDate { get; set; }

        public Guid? MigratedRequestID { get; set; }


        public virtual Application Application { get; set; }


        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual ICollection<JuristicApplicationStatusRequestLog> JuristicApplicationStatusRequestLogs { get; set; }
    }
}
