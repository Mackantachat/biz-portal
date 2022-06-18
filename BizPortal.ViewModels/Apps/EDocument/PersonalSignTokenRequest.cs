using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EDocument
{
    public class PersonalSignTokenRequest
    {
        /// <summary>
        /// นำ DocumentID ที่ได้มาใส่ใน json body
        /// </summary>
        public string DocumentID { get; set; }

        /// <summary>
        /// String รายละเอียด
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// รูปลายเซ็นต์
        /// </summary>
        public PersonalSignature Signature { get; set; }
    }

    public class PersonalSignature
    {
        /// <summary>
        /// File รูปลายเซ็น Base64 (ตามตัวอย่างด้านล่างลองใช้ได้ก่อนครับ)
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// ตำแหน่งแสดงรูปลายเซ็นบน PDF จากซ้าย (px)
        /// </summary>
        public string Left { get; set; }

        /// <summary>
        /// ตำแหน่งแสดงรูปลายเซ็นบน PDF จากล่าง (px)
        /// </summary>
        public string Bottom { get; set; }

        /// <summary>
        /// ตำแหน่งแสดงรูปลายเซ็นบน PDF ความสูงรูป (px)
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// ตำแหน่งแสดงรูปลายเซ็นบน PDF ความกว้างรูป (px)
        /// </summary>
        public string Width { get; set; }
    }
}
