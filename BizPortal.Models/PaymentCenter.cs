using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{

    /// <summary>
    /// หน่วยเบิกจ่าย
    /// </summary>
    public class PaymentCenter : ManipulateModel
    {
        public int PaymentCenterID { get; set; }

        /// <summary>
        /// รหัสหน่วยเบิกจ่าย
        /// </summary>
        [Required]
        public string PaymentCenterCode { get; set; }

        /// <summary>
        /// short description
        /// </summary>
        [Required]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// flag กำหนดการใช้งาน
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// วันที่ปิดตามกฎหมาย
        /// </summary>
        public DateTime? LegalClosingDate { get; set; }

        /// <summary>
        /// วันที่ปิดตามบัญชี
        /// </summary>
        public DateTime? AccountClosingDate { get; set; }
    }
}
