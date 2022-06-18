using BizPortal.ViewModels.V2;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.StandardApi
{
    public class StandardApiRequestModel
    {
        public int ApplicationID { get; set; }
        public Guid? ApplicationRequestID { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public int ApplicationRunningNumber { get; set; }
        /// <summary>
        /// Batch ID ตอนสร้าง Single Form (เอาไว้ใช้ gen ใบรับคำขอ)
        /// </summary>
        public Guid? RequestBatchID { get; set; }
        //[CustomRequired]
        [Display(Name = "APPLICATION_STATUS_REQUEST", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public string StatusOtherText { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// ข้อมูลพิเศษสำหรับการส่งคำร้องแต่ละบริการ
        /// </summary>
        public Dictionary<string, ApplicationRequestDataGroupViewModel> Data { get; set; }
        /// <summary>
        /// ระยะเวลาดำเนินการ
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal? Fee { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี 13 หลัก
        /// </summary>
        [Display(Name = "JURISTIC", ResourceType = typeof(Resources.Application))]
        public string IdentityID { get; set; }

        public string IdentityName { get; set; }

        /// <summary>
        /// ประเภทผู้ขอใช้บริการ
        /// </summary>
        public UserTypeEnum IdentityType { get; set; }
        /// <summary>
        /// IP Address ของเครื่องที่ใช้ส่งคำขอ
        /// </summary>
        public string SourceIPAddress { get; set; }

        /// <summary>
        /// พื้นที่เจ้าของใบอนุญาต
        /// </summary>
        public int? ProvinceID { get; set; }
        public int? AmphurID { get; set; }
        public int? TumbolID { get; set; }
        public string Province { get; set; }
        public string Amphur { get; set; }
        public string Tumbol { get; set; }

        /// <summary>
        /// ไฟล์เอกสารแนบ
        /// </summary>
        [Display(Name = "UPLOAD_FILE", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public FileGroup[] UploadedFiles { get; set; }

        public static StandardApiRequestModel From(ApplicationRequestViewModel model)
        {
            StandardApiRequestModel ret = new StandardApiRequestModel();
            TypeAdapter.Adapt<ApplicationRequestViewModel, StandardApiRequestModel>(model, ret);
            return ret;
        }
        
    }
}
