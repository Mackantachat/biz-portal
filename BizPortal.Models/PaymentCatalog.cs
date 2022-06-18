using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    /// <summary>
    /// Catalog
    /// </summary>
    public class PaymentCatalog : ManipulateModel
    {
        public int PaymentCatalogID { get; set; }

        /// <summary>
        /// รหัส Catalog
        /// </summary>
        public string PaymentCatalogCode { get; set; }

        /// <summary>
        /// รายการรับชำระ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ประเภทการจัดเก็บ
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// หมวดบริการ
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// สถานะการใช้งาน
        /// </summary>
        public bool UsingStatus { get; set; }

        /// <summary>
        /// สถานะการอนุมัติ
        /// </summary>
        public bool ApproveStatus { get; set; }

        /// <summary>
        /// วันที่เริ่มต้นใช้งาน
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// วันที่สิ้นสุดใช้งาน
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// วันที่ยกเลิกใช้งาน
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// เวอร์ชั่น
        /// </summary>
        public double Version { get; set; }


        /// <summary>
        /// ลำดับที่
        /// </summary>
        public int Order { get; set; }

    }
}
