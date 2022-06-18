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
    public class StandardApiTransactionModel
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

        public string Remark { get; set; }

        /// <summary>
        /// ร้องขอไฟล์เอกสารเพิ่มเติม
        /// </summary>
        public FileGroup AdditionalFiles { get; set; }

        public static StandardApiTransactionModel From(ApplicationRequestViewModel model)
        {
            StandardApiTransactionModel ret = new StandardApiTransactionModel();
            TypeAdapter.Adapt<ApplicationRequestViewModel, StandardApiTransactionModel>(model, ret);
            ret.AdditionalFiles = model.UploadedFiles.Last();
            return ret;
        }
    }
}
