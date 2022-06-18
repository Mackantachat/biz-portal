using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class PersonalQrCodeResponse
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Landing page สำหรับการ Signing กำหนดให้เป็น URL ดังนี้
        /// https://portal.apps.go.th/edoc/signature/signed
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// URL ของหน้าที่ต้องการให้แสดง หลังจาก User ทำการ Signing เสร็จ
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// X-Signed-Token ใน Response Header ที่ได้จากการเรียก API ลงลายมือชื่ออิเล็กทรอนิกส์แบบบุคคล
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ค่า Random String สำหรับนำไปตรวจสอบเมื่อระบบ Signing ทำการ Callback กลับไป
        /// </summary>
        public string Nonce { get; set; }
    }
}
