using System;
using System.Collections.Generic;

namespace BizPortal.ViewModels.V2
{
    public class DashboardViewModel
    {
        public string IdentityName { get; set; }

        public UserTypeEnum IdentityType { get; set; }

        public string IdentityID { get; set; }
        
        /// <summary>
        /// กรณีที่มีร่างคำร้อง
        /// </summary>
        public string DraftRequest { get; set; }

        public string WaitingApproveRequest { get; set; }

        public string ApproveRequest { get; set; }

        public string LastUpdateTime { get; set; }
        
        /// <summary>
        /// สำหรับ DraftRequest
        /// </summary>
        public List<string> AppList { get; set; }

        public string NumApp { get; set; }

        public string AppStep { get; set; }

        public int FileCnt { get; set; }

        public int FileTotal { get; set; }
        
        /// <summary>
        /// คำร้องที่ Active อยู่
        /// </summary>
        public List<DashboardRequestModel> RequestList { get; set; }
        
        /// <summary>
        /// คำร้องที่ได้รับอนุมัติ/ปฏิเสธ
        /// </summary>
        public List<DashboardRequestModel> RequestApproveList { get; set; }
        
        /// <summary>
        /// คำร้องที่ได้รับอนุมัติ
        /// </summary>
        public List<DashboardRequestModel> RequestOwnerList { get; set; }

        public Guid? DraftRequsetTransID { get; set; }

        public int DraftRequestAppStep { get; set; }

        public int DraftRequestStep { get; set; }
    }

    public class DashboardRequestModel
    {
        public string RequestBatchID { get; set; } // For Grouping Not Use       

        public string CreatedDate { get; set; }

        public string LastUpdateTime { get; set; }

        public List<RequestItem> Requests { get; set; }

        public int SortGroup { get; set; }
    }

    public class RequestItem {

        public string ApplicationRequestID { get; set; }

        public string ApplicationName { get; set; }

        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public List<string> StatusSequence { get; set; }

        public ApplicationStatusV2Enum StatusBeforeRejected { get; set; }

        public string RequestNo { get; set; }

        public string SLDDate { get; set; }

        public string OrgName { get; set; }

        public bool? NoFee { get; set; }

        public bool? NoDoc { get; set; }

        public string CheckApproveDateTime { get; set; }

        public string WaitingApproveDateTime { get; set; }

        public string PendingApproveDateTime { get; set; }

        public string PaidFeeApproveDateTime { get; set; }

        public string CreateLicenseApproveDateTime { get; set; }
        
        public string RejectText { get; set; }

        public string RejectRemarkText { get; set; }

        public DateTime LastUpate { get; set; }

        public DateTime? ExpectedFinishDate { get; set; }

        public DateTime? DueDateForPayFee { get; set; }

        public DateTime? UserCanGetAppDate { get; set; }

        public DateTime? UserCanGetAppDateEnd { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentMethodOrgDetail { get; set; }

        public string PaymentMethodOrgAddress { get; set; }

        public string PaymentMethodOrgTel { get; set; }

        public string PermitDeliveryType { get; set; }

        public string PermitDeliveryOrgDetail { get; set; }

        public string PermitDeliveryOrgAddress { get; set; }

        public string PermitDeliveryOrgTel { get; set; }

        public int ApplicationID { get; set; }

        public string CustomStatusName { get; set; }

        public string NextStepUrl { get; set; }

        public string EMSTrackingNumber { get; set; }

        public FileMetadata BillPaymentFile { get; set; }

        public FileMetadata ReceiptFile { get; set; }

        public string LastUpdatedFrom { get; set; }

    }
}
