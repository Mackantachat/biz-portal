using BizPortal.Models;
using BizPortal.Utils;
using EGA.EGA_Development.Util.MailV2.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BizPortal.ViewModels.V2
{
    public class ApplicationRequestViewModel
    {
        #region[Request Information]

        /// <summary>
        /// ชื่อบริการจาก SQLDB
        /// </summary>
        public string AppSysName { get; set; }

        public string Business { get; set; }

        /// <summary>
        /// เลขรับเรื่อง หรือ Request ID (RequestID)
        /// TYYMMDDRRR(10หลัก)
        /// T คือ ประเภทผู้ใช้บริการ คือ นิติบุคคล = J , บุคคลธรรมดา = C
        /// YY คือ ปี พ.ศ. 2 หลักท้าย เช่น 2559 YY = 59
        /// MM คือ เดือนเป็นตัวเลข เช่น สิงหาคม MM = 08
        /// DD คือ วันที่ เช่น วันที่ 29 DD = 29
        /// RRR คือ Running Number การส่งข้อมูลบริการในแต่ละวัน นับแยกตามประเภทผู้ใช้บริการ(เริ่มนับ 001 ใหม่ทุกวัน)
        /// เช่น C590829001, J590829001
        /// </summary>
        public string ApplicationRequestNumber { get; set; }

        public int ApplicationRequestRunningNumber { get; set; }
        /// <summary>
        /// Ref Code ของหน่วยงาน
        /// </summary>
        public bool IsRequestNumberAgent { get; set; }

        public string ApplicationRequestNumberAgent { get; set; }

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

        public Guid? ApplicationRequestID { get; set; }

        /// <summary>
        /// Batch ID ตอนสร้าง Single Form (เอาไว้ใช้ gen ใบรับคำขอ)
        /// </summary>
        public Guid? RequestBatchID { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal? Fee { get; set; }

        /// <summary>
        /// ค่าส่งใบอนุญาตทางไปรษณีย์
        /// </summary>
        public decimal? EMSFee { get; set; }

   

        /// <summary>
        /// ระยะเวลาดำเนินการ
        /// </summary>
        public int? Duration { get; set; }

        public bool PermitCanBeDeliveredOnPayment { get; set; }


        public string ExpectedFinishDate { get; set; }

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
        /// IP Address ของเครื่องที่ใช้ส่งคำขอ
        /// </summary>
        public string SourceIPAddress { get; set; }

        /// <summary>
        /// เป็นรายการตอบกลับจากหน่วยงาน
        /// </summary>
        public bool ReplyFromOrg { get; set; }

        public bool ReplyFromApiUpdate { get; set; }

        /// <summary>
        /// เลขที่/รหัสคำขอ
        /// </summary>
        [StringLength(450)]
        public string TransactionCode { get; set; }

        /// <summary>
        /// วันที่เกิดรายการ
        /// </summary>
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// ข้อมูลพิเศษสำหรับการส่งคำร้องแต่ละบริการ
        /// </summary>
        public Dictionary<string, ApplicationRequestDataGroupViewModel> Data { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Display(Name = "REMARK", ResourceType = typeof(Resources.Global))]
        public string Remark { get; set; }

        public string OrgNameTH { get; set; }

        public string OrgAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime UpdatedDateByRequestor { get; set; }

        public DateTime UpdatedDateByAgent { get; set; }

        public string UpdatedByAgent { get; set; }

        public string LastRequestorUpdateEmail { get; set; }

        public bool? IsPassStepWaiting { get; set; }

        public DateTime? ConsentDateTimeStamp { get; set; }

        #endregion

        #region[Status]

        /// <summary>
        /// รหัสบริการ เอาจากฐานข้อมูลตาราง Application
        /// </summary>
        [CustomRequired]
        [Display(Name = "APPLICATION", ResourceType = typeof(Resources.Application))]
        public int ApplicationID { get; set; }

        [Display(Name = "APPLICATION_STATUS_REQUEST", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public string StatusOtherText { get; set; }

        public string StatusRemark { get; set; }

        public string StatusBeforeCancel { get; set; }

        public bool? DisableUpdateStatus { get; set; }

        #endregion

        #region[Request Files]

        /// <summary>
        /// ร้องขอไฟล์เอกสารเพิ่มเติม
        /// </summary>
        [Display(Name = "REQUEST_FILE", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public FileMetadata[] RequestedFiles { get; set; }

        #endregion

        #region[Gov Files]

        /// <summary>
        /// ชื่อของไฟล์แนบเอกสาร
        /// </summary>
        [Display(Name = "UPLOAD_FILE_DOC_NAME", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string GovDocumentName { get; set; }

        /// <summary>
        /// รายละเอียดของไฟล์แนบเอกสาร
        /// </summary>
        [Display(Name = "UPLOAD_FILE_DOC_DESC", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string GovDocumentDescription { get; set; }

        /// <summary>
        /// ไฟล์เอกสารแนบ
        /// </summary>
        [Display(Name = "UPLOAD_FILE", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public FileMetadata[] GovFiles { get; set; }

        #endregion

        #region[User Files]

        /// <summary>
        /// ไฟล์เอกสารแนบที่ผู้ใช้งานทำการอัปโหลดเอกสารเพิ่มเติมเข้ามา
        /// </summary>
        [Display(Name = "UPLOAD_FILE", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public FileGroup[] UploadedFiles { get; set; }

        public Base64Attachment[] Attachments { get; set; }

        #endregion

        #region[Payment]

        public bool IsFee { get; set; }

        /// <summary>
        /// วันครบกำหนดชำระค่าธรรมเนียม
        /// </summary>
        public string DueDateForPayFee { get; set; }

        /// <summary>
        /// ตัวเลือกสำหรับหน้าชำระค่าธรรมเนียมเพื่อให้เจ้าหน้าที่เลือกว่าจะเปิดให้ user เลือกชำระเงินจากช่องทางไหนได้บ้าง
        /// เป็นตัวเลือกที่เชื่อมกับหน้า User ในส่วนของ Payment Method
        /// </summary>
        public string[] PaymentMethodEnabledChoice { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นชื่อสำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PaymentMethodOrgDetail { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นที่อยู่สำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PaymentMethodOrgAddress { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นเบอร์สำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PaymentMethodOrgTel { get; set; }

        /// <summary>
        /// For API only
        /// สำหรับให้เจ้าหน้าที่หน่วยงานส่ง Billpayment file
        /// </summary>
        public Base64Attachment[] AttachBillPayment { get; set; }

        /// <summary>
        /// ช่องทางการชำระเงินที่ user เลือกมาจาก PaymentMethodEnabledChoice (หน้า Track/Detail)
        /// </summary>
        [Display(Name = "PAYMENT_MEDTHOD", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// For Backend Only
        /// ประเถทของช่องทางการชำระเงินสำหรับ Payment method = Bill Payment
        /// มีให้เลือกระหว่าง 1.ของหน่วยงาน, 2.กรมบัญชีกลาง
        /// </summary>
        public string BillPaymentTypeForPaymentMethod { get; set; }

        /// <summary>
        /// For Backend Only
        /// ไฟล์ Billing payment (สำหรับให้เจ้าหน้าที่ upload เมื่อมีการเลือก PaymentMethodEnabledChoice = BillPayment)
        /// </summary>
        public FileMetadata[] BillPaymentFiles { get; set; }

        public int MyProperty { get; set; }

        public BillpaymentViewModel CGDPayment { get; set; }

        [JsonIgnore]
        public bool IsEnableCGDPayment { get; set; }

        [JsonIgnore]
        public bool? IsCheckPaymentStatus { get; set; }

        #endregion

        #region[Permit]

        /// <summary>
        /// ตัวเลือกสำหรับหน้าชำระค่าธรรมเนียมเพื่อให้เจ้าหน้าที่เลือกว่าจะเปิดให้ user เลือกช่องทางการรับใบอนุญาตช่องทางไหนได้บ้าง
        /// เป็นตัวเลือกที่เชื่อมกับหน้า User ในส่วนของ PermitDeliveryType
        /// </summary>
        public string[] PermitDeliveryTypeEnabledChoice { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นชื่อสำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PermitDeliveryOrgDetail { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นที่อยู่สำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PermitDeliveryOrgAddress { get; set; }

        /// <summary>
        /// กรณีเลือกเป็นรับที่สำนักงานจะใช้ตรงนี้ไปแสดงเป็นเบอร์สำนักงาน แต่ถ้าไม่มี default จะเป็นสำนักงานใหญ่
        /// </summary>
        public string PermitDeliveryOrgTel { get; set; }

        /// <summary>
        /// ช่องทางการรับใบอนุญาตมี 3 แบบ
        /// </summary>
        [Display(Name = "PERMIT_DELIVERY_TYPE", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string PermitDeliveryType { get; set; }

        /// <summary>
        /// ที่อยู่สำหรับให้ผู้ประกอบการมารับใบอนุญาต (ใช้กับขั้นตอนที่ 4 ชำระค่าธรรมเนียม)
        /// </summary>
        //[Display(Name = "PERMIT_DELIVERY_ADDRESS", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string PermitDeliveryAddress { get; set; }

        public bool PermitDeliverOnPayment_OK
        {
            get
            {
                return PermitCanBeDeliveredOnPayment && PaymentMethod == "AT_OWNER_ORG" && PermitDeliveryType == "AT_OWNER_ORG";
            }
        }

        /// <summary>
        /// ผู้ชำระค่าบริการไปรษณีย์มี 2 แบบ
        /// </summary>
        public string EMSFeePaymentType { get; set; }

        /// <summary>
        /// หมายเลขพัสดุ
        /// แสดงฟิลด์นี้ให้กรอกต่อเมื่ออยู่ตั้นตอนที่ 5 และ PermitDeliveryType = BY_MAIL 
        /// </summary>
        //[Display(Name = "EMS_TRACKING_NUMBER", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public string EMSTrackingNumber { get; set; }


        /// <summary>
        /// ไฟล์ e-Permit (ใบอนุญาต online ที่ส่งให้ผู้ขอใบอนุญาต)
        /// </summary>
        public FileMetadata[] EPermitFiles { get; set; }

        /// <summary>
        /// ไฟล์ e-Permit (ใบอนุญาต online ที่ส่งให้ผู้ขอใบอนุญาต)
        /// </summary>
        public FileMetadata[] EPermitFilesForDisplay { get; set; }

        /// <summary>
        /// มีใบอนุณาตหรือไม่
        /// </summary>
        public bool NoDocument { get; set; }

        public string UserCanGetAppDate { get; set; }

        public string UserCanGetAppDateEnd { get; set; }

        #endregion

        #region[E-License]

        public bool IsPendingSigning { get; set; }

        [JsonIgnore]
        public bool IsEnableELicense { get; set; }

        public List<ELicenseViewModel> ELicenses { get; set; }

        #endregion

        #region[Notification]

        /// <summary>
        /// ไม่ส่งอีเมลแจ้งสถานะของระบบ
        /// </summary>
        public bool DisabledSendingSystemEmail { get; set; }

        /// <summary>
        /// ส่งข้อมูลมากรณีต้องการส่งอีเมล์ (ไม่ใช้ Default Email)
        /// </summary>
        public ApplicationStatusEmailMessage EmailMessage { get; set; }

        /// <summary>
        /// ส่งข้อมูลกรณีต้องการส่ง sms
        /// </summary>
        public ApplicationStatusSmsMessage SmsMessage { get; set; }
        #endregion

        #region[Chat]

        public ChatViewModel[] Chats { get; set; }
        #endregion

        #region[Validation]
        public Dictionary<string, string> ValidateErrors()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (SmsMessage != null)
            {
                if (SmsMessage.MobileNumbers == null)
                {
                    errors.Add("MobileNumbers", "MobileNumbers is required.");
                }
                else
                {
                    MobileNumberInfo[] invalidNumbers = SmsMessage.GetInvalidNumbers();
                    if (invalidNumbers.Length > 0)
                    {
                        string numbers = string.Join(",", invalidNumbers.Select(o => o.MobileNumber).ToArray());
                        errors.Add("MobileNumbers", "Invalid mobile numbers. (" + numbers + ")");
                    }
                }
            }

            return errors;
        }
        #endregion

        #region [Optional Properties]

        /// <summary>
        /// ข้อมูลเพิ่มเติมที่รับเพิ่มจากหน่วยงาน
        /// </summary>
        public Dictionary<string, object> Extras { get; set; }

        [JsonIgnore]
        public string LastUpdatedFrom { get; set; }

        #endregion
    }

    [Serializable]
    public class ApplicationRequestViewModelSerializable
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
    }
}
