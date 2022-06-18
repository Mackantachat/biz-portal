using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    /// <summary>
    /// ศูนย์ต้นทุน
    /// </summary>
    public class PaymentHomeCostCenter : ManipulateModel
    {
        public int PaymentHomeCostCenterID { get; set; }

        /// <summary>
        /// ศูนย์ต้นทุน
        /// </summary>
        [Required]
        public string CostCenterCode { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// คำอธิบาย
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// รหัสหน่วยงาน ใช้แค่ 4 หลักซึ่งแตกต่างจากที่ระบบเราใช้ 
        /// </summary>
        [Required]
        public string PaymentOrgCode { get; set; }

        /// <summary>
        /// มีผลจาก
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// ถึง
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// ล็อคต/ทหลักจริง อันนี้ไม่รู้ว่าคืออะไร
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// รหัสจังหวัด ใช้ 4 หลักซึ่งแตกต่างจากที่ระบบเราใช้ เราใช้ 2 หลักแต่ดูแล้วตรงกัน 2หลักแรก 
        /// </summary>
        [Required]
        public string ProvinceCode { get; set; }

        [Required, ForeignKey("PaymentCenter")]
        public int PaymentCenterID { get; set; }

        public virtual PaymentCenter PaymentCenter { get; set; }
    }
}
