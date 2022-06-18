using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class SignedDocumentResponse
    {
        /// <summary>
        /// ID สำหรับอ้างอิง PDF/A-3 ที่ทำการลงลายมือชื่อแล้ว
        /// </summary>
        public string DocumentID { get; set; }

        /// <summary>
        /// ID ยืนยันการลงชื่ออิเล็กทรอนิกส์สำหรับ DocumentID ดังกล่าวแล้ว
        /// </summary>
        public string SignatureID { get; set; }

        /// <summary>
        /// ประกอบด้วย 3 สถานะ คือ SIGNED, REJECTED, FAILED
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Nonce String ที่ระบบหน่วยงานส่งมา ระบบ Signing ทำการ Callback ค่านี้กลับไป
        /// </summary>
        public string Nounce { get; set; }
    }
}
