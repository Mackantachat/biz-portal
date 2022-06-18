using System;
using System.Collections.Generic;
using BizPortal.Utils.Extensions;

namespace BizPortal.ViewModels.V2
{
    public class ApplicationRequestSearchResultViewModel
    {
        public Guid ApplicationRequestID { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี (นิติบุคคล, บุคคลธรรมดา)
        /// </summary>
        public string IdentityID { get; set; }

        /// <summary>
        /// ชื่อผู้ขอใช้บริการ
        /// </summary>
        public string IdentityName { get; set; }

        /// <summary>
        /// ประเภทผู้ขอใช้บริการ
        /// </summary>
        public UserTypeEnum IdentityType { get; set; }

        public int ApplicationID { get; set; }

        public string ApplicationName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ExpectSLADate { get; set; }
        public bool ExceedSLA {
            get
            {
                return ExpectSLADate.HasValue ? (DateTime.Now.Date > ExpectSLADate.Value) : false;
            }
        }
        public DateTime UpdatedDate { get; set; }
        public DateTime UpdatedDateByRequestor { get; set; }
        public DateTime UpdatedDateByAgent { get; set; }

        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusName { get; set; }

        public string StatusOther { get; set; }
        public string StatusOtherName { get; set; }

        public bool RequestorReplied {
            get {
                return (this.Status == ApplicationStatusV2Enum.CHECK || this.Status == ApplicationStatusV2Enum.PENDING) &&
                                        this.StatusOther == "WAITING_AGENT_WORKING" &&
                                        this.StatusOtherPrevious == "WAITING_USER_WORKING";   
            }
        }
        public string StatusOtherPrevious { get; set; }

        public Nullable<bool> isViewed { get; set; }
        public Nullable<int> Duration { get; set; }
        public int? ProvinceID { get; set; }
        public string Province { get; set; }
        public int? AmphurID { get; set; }
        public string Amphur { get; set; }
        public string BusinessID { get; set; }
        public string BusinessName { get; set; }
        public decimal? Fee { get; set; }

        public string PaymentMethod { get; set; }

        #region Status Date

        #endregion

        #region [Display Only]
        public string CreatedDateTxt { get { return CreatedDate.ToLocalTime().ToStringFormat(); } }

        public string UpdatedDateTxt { get { return UpdatedDate.ToLocalTime().ToStringFormat(); } }
        
        public string ExpectSLADateTxt { get; set; }
        public string OrgCode { get; set; }

        public string OrganizationName { get; set; }

        public string ApplicationRequestNumber { get; set; }
        public string ApplicationRequestNumberAgent { get; set; }

        #endregion
    }
}
