using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class PermitSummaryReport
    {
        [Key, Required]
        public int ID { get; set; }
        [StringLength(30)]
        [Required]
        public string IdentityType { get; set; }
        [Required]
        public int ApplicationID { get; set; }
        [Required]
        public string ApplicationRequestID { get; set; }
        [StringLength(10), Required]
        public string ApplicationRequestNumber { get; set; }
        public string OrgNameTH { get; set; }
        [StringLength(20), Required]
        public string OrgCode { get; set; }
        public string PermitName { get; set; }
        [Required]
        public string AppSysName { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string StatusOther { get; set; }
        public decimal? Fee { get; set; }
        public decimal? EMSFee { get; set; }
        [StringLength(13), Required]
        public string IdentityID { get; set; }
        public string IdentityName { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime UpdatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ExpectedFinishDate { get; set; }
        [Required]
        public int? ProvinceID { get; set; }
        public string Province { get; set; }
        [Required]
        public int? AmphurID { get; set; }
        public string Amphur { get; set; }
        [Required]
        public int? TumbolID { get; set; }
        public string Tumbol { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? JobIncompleteDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobWaitingDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobCheckDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobPendingDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobApprovedWaitingPayFeeDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobPaidFeeCreatingLicenseDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobRejectedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobCompletedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobFailedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? JobCanceledDate { get; set; }

        [DataType(DataType.DateTime), Required]
        public DateTime JobUpdatedDate { get; set; }
    }
}
