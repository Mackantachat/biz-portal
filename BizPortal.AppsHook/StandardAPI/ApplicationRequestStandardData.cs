using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook.StandardAPI
{
    class ApplicationRequestStandardDataCreate : ApplicationRequestStandardData
    {
        public Dictionary<string, ApplicationRequestDataGroupViewModel> DataFiltered { get; set; }
    }

    class ApplicationRequestStandardData
    {
        public int ApplicationID { get; set; }

        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public string ApplicationRequestNumber { get; set; }

        public int ApplicationRequestRunningNumber { get; set; }

        public string IdentityID { get; set; }

        public UserTypeEnum IdentityType { get; set; }

        public Guid? ApplicationRequestID { get; set; }

        public string TransactionCode { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string Remark { get; set; }

        public bool DisabledSendingSystemEmail { get; set; }

        public string ApplicationRequestNumberAgent { get; set; }
        
        public string IdentityName { get; set; }

        public decimal? Fee { get; set; }

        public decimal? EMSFee { get; set; }

        public DateTime? DueDateForPayFee { get; set; }

        public string PermitDeliveryAddress { get; set; }

        public string PermitDeliveryType { get; set; }

        public string EMSFeePaymentType { get; set; }

        public string PaymentMethod { get; set; }

        public string BillPaymentTypeForPaymentMethod { get; set; }

        public FileGroup[] UploadedFiles { get; set; }

        /*
            "Chats": null,
            "ApplicationRequestVersion": 2,
            "ApplicationID": 146,
            "RequestBatchID": "fd06ca4d-50d6-4b5c-be75-766c4b84c599",
            "Fee": 11000.0,
            "EMSFee": 50.0,
            "DueDateForPayFee": null,
            "Duration": 60,
            "ProvinceID": 11,
            "AmphurID": 1,
            "TumbolID": 1,
            "Province": "สมุทรปราการ",
            "Amphur": "เมืองสมุทรปราการ",
            "Tumbol": "ปากน้ำ",
            "SourceIPAddress": "::1",
            "OrgCode": "19007000",
            "OrgNameTH": "กรมสนับสนุนบริการสุขภาพ",
            "OrgAddress": "ถ.ติวานนท์ อ.เมือง จ.นนทบุรี 11000",
            "PermitName": "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ",
            "AppSysName": "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ",
            "CreatedDate": "2019-03-21T17:15:34.1165881+07:00",
            "UpdatedDate": "2019-03-21T17:15:36.4678669+07:00",
            "ExpectSLADate": "2019-05-20T17:15:33.3545488+07:00",
            "UpdatedDateByRequestor": "2019-03-21T17:15:34.1165881+07:00",
            "UpdatedDateByAgent": "2019-03-21T17:15:34.1165881+07:00",
            "UpdatedByAgent": null,
            "LastRequestorUpdateEmail": null,
            "isViewed": null,
            "Status": 3,
            "StatusOther": "WAITING_AGENT_READ_REQUEST",
            "IsAgentCheckUserCorrection": false,
            "StatusBeforeCancel": null,
            "ActionReply": "EMPTY",
            "PermitDeliveryAddress": null,
            "PermitDeliveryType": null,
            "PaymentMethod": null,
            "PaymentMethodEnabledChoice": null,
            "BillPaymentTypeForPaymentMethod": null,
            "PermitDeliveryTypeEnabledChoice": null,
            "EMSTrackingNumber": null,
            "WaitingApproveDateTime": null,
            "CheckApproveDateTime": null,
            "PendingApproveDateTime": null,
            "PaidFeeApproveDateTime": null,
            "CreateLicenseApproveDateTime": null,
            "RejectDateTime": null,
            "NoDocument": false,
            "TransactionCode": null,
            "TransactionDate": null,
            "Data": "",
            "DataAll": null,
            "DataExcluded": null,
            "DataConfig": null,
            "Remark": null,
            "RequestedFiles": null,
            "GovFiles": [],
            "EPermitFiles": null,
            "BillPaymentFiles": [],
            "PermitCanBeDeliveredOnPayment": false,
            "UserCanGetAppDate": null,
            "ExpectedFinishDate": null,
            "ApplicationName": null,
            "StatusName": null,
            "CreatedDateTxt": "21/03/2562 , 17:15",
            "UpdatedDateTxt": "21/03/2562 , 17:15",
            "CgdPayments": null,
            "Id": "5c936446a6f80449cc793803"
         */
    }
}
