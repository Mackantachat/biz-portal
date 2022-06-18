using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using BizPortal.Utils.Extensions;
using MongoDB.Bson;

namespace BizPortal.DAL.MongoDB
{
    public abstract class ApplicationRequestBasedEntity : Entity
    {
        /// <summary>
        /// FormConfig
        /// เก็บ revision ของฟอร์ม ณ วันเวลาที่ยื่นคำร้อง
        /// </summary>
        public int FormRevisionCode { get; set; } = 0;
        /// <summary>
        /// FormConfig
        /// เก็บ revision name ของฟอร์ม ณ วันเวลาที่ยื่นคำร้อง
        /// </summary>
        public string FormRevisionName { get; set; } = "Before FormConfig Revision";

        /// <summary>
        /// เวอร์ชันของข้อมูลคำร้อง
        /// 0: เวอร์ชันแรก Data มีข้อมูลทั้งหมดที่เคยกรอกมา แม้ว่าใบอนุญาตที่ขอไม่ได้ต้องการข้อมูลนั้น
        ///    
        /// 1: เวอร์ชัน 1 Data มีเฉพาะข้อมูลที่ใบอนุญาตนั้นต้องการ 
        ///    และมี DataConfig เก็บ config ของ Form ณ เวลาที่ยื่นคำขอ
        /// 3: เวอร์ชัน 3 เก็บ config ของ Form ณ เวลาที่ยื่นคำขอด้วย field: FormRevisionCode, FormRevisionName
        /// </summary>
        public int ApplicationRequestVersion { get; set; } = 0;

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
        [BsonRepresentation(BsonType.String)]
        public UserTypeEnum IdentityType { get; set; }

        /// <summary>
        /// รหัสบริการ เอาจากฐานข้อมูลตาราง Application
        /// </summary>
        public int ApplicationID { get; set; }

        /// <summary>
        /// ชนิดของบริการ เอาจากฐานข้อมูลตาราง Application
        /// </summary>
        public string ApplicationType { get; set; }

        /// <summary>
        /// Batch ID ตอนสร้าง Single Form (เอาไว้ใช้ gen ใบรับคำขอ)
        /// </summary>
        [BsonRepresentation(BsonType.String)]
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
        /// วันครบกำหนดชำระค่าธรรมเนียม
        /// </summary>
        public DateTime? DueDateForPayFee { get; set; }

        /// <summary>
        /// ระยะเวลาดำเนินการ
        /// </summary>
        public int? Duration { get; set; }

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
        /// รหัสหน่วยงานเจ้าของบริการ
        /// </summary>
        public string OrgCode { get; set; }

        public string OrgNameTH { get; set; }

        public string OrgAddress { get; set; }

        public string PermitName { get; set; }

        public string BusinessId { get; set; }

        public string BusinessName { get; set; }

        public string AppSysName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime ExpectSLADate { get; set; }

        public DateTime UpdatedDateByRequestor { get; set; }

        public DateTime UpdatedDateByAgent { get; set; }

        public string UpdatedByAgent { get; set; }

        public string LastRequestorUpdateEmail { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        public Nullable<bool> isViewed { get; set; }

        #region [Optional Properties]

        [BsonRepresentation(BsonType.String)]
        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public string StatusRemark { get; set; }

        public bool IsAgentCheckUserCorrection { get; set; }

        /// <summary>
        /// ใช้เก็บ Status ปัจจุบันที่เป็นอยู่ก่อนที่จะยกเลิก
        /// สแตมป์ field นี้ถ้าหาก User ทำการยกเลิกการยื่นใบคำร้องและเจ้าหน้าที่ทำการ Approve การขอยกเลิกนี้แล้ว
        /// </summary>
        public string StatusBeforeCancel { get; set; }

        /// <summary>
        /// Ref Code ของหน่วยงาน
        /// </summary>
        public string ApplicationRequestNumberAgent { get; set; } = "";

        public string ActionReply { get; set; }

        /// <summary>
        /// ที่อยู่สำหรับให้ผู้ประกอบการมารับใบอนุญาต
        /// </summary>
        public string PermitDeliveryAddress { get; set; }

        /// <summary>
        /// ช่องทางการรับใบอนุญาตมี 3 แบบ
        /// </summary>
        public string PermitDeliveryType { get; set; }

        /// <summary>
        /// ผู้ชำระค่าบริการไปรษณีย์มี 2 แบบ
        /// </summary>
        public string EMSFeePaymentType { get; set; }

        /// <summary>
        /// ช่องทางการชำระเงินมี 4 แบบ
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// ตัวเลือกสำหรับหน้าชำระค่าธรรมเนียมเพื่อให้เจ้าหน้าที่เลือกว่าจะเปิดให้ user เลือกชำระเงินจากช่องทางไหนได้บ้าง
        /// เป็นตัวเลือกที่เชื่อมกับหน้า User ในส่วนของ Payment Method
        /// </summary>
        public List<string> PaymentMethodEnabledChoice { get; set; }


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
        /// ประเถทของช่องทางการชำระเงินสำหรับ Payment method = Bill Payment
        /// มีให้เลือกระหว่าง 1.ของหน่วยงาน, 2.กรมบัญชีกลาง
        /// </summary>
        public string BillPaymentTypeForPaymentMethod { get; set; }

        /// <summary>
        /// ตัวเลือกสำหรับหน้าชำระค่าธรรมเนียมเพื่อให้เจ้าหน้าที่เลือกว่าจะเปิดให้ user เลือกช่องทางการรับใบอนุญาตช่องทางไหนได้บ้าง
        /// เป็นตัวเลือกที่เชื่อมกับหน้า User ในส่วนของ PermitDeliveryType
        /// </summary>
        public List<string> PermitDeliveryTypeEnabledChoice { get; set; }

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
        /// <summary>
        /// หมายเลขพัสดุ
        /// แสดงฟิลด์นี้ให้กรอกต่อเมื่ออยู่ตั้นตอนที่ 5 และ PermitDeliveryType = BY_MAIL
        /// </summary>
        public string EMSTrackingNumber { get; set; }

        /// <summary>
        /// วันและเวลาที่ทำการ Approve สถานะยื่นคำขอ 
        /// </summary>
        public DateTime? WaitingApproveDateTime { get; set; }

        /// <summary>
        /// วันและเวลาที่ทำการ Approve สถานะตรวจสอบคำขอ
        /// </summary>
        public DateTime? CheckApproveDateTime { get; set; }

        /// <summary>
        /// วันและเวลาที่ทำการ Approve สถานะพิจรณาอนุมัติ
        /// </summary>
        public DateTime? PendingApproveDateTime { get; set; }

        /// <summary>
        /// วันและเวลาที่ทำการ Approve สถานะชำระค่าธรรมเนียม
        /// </summary>
        public DateTime? PaidFeeApproveDateTime { get; set; }

        /// <summary>
        /// วันและเวลาที่ทำการ Approve สถานะออกใบอนุญาต
        /// </summary>
        public DateTime? CreateLicenseApproveDateTime { get; set; }

        /// <summary>
        /// วันที่ปฏิเสธหรือยกเลิกคำขอ
        /// </summary>
        public DateTime? RejectDateTime { get; set; }

        /// <summary>
        /// ต้องมีการไปรับใบอนุญาตหรือไม่
        /// </summary>
        public bool NoDocument { get; set; }

        /// <summary>
        /// เลขที่/รหัสคำขอ
        /// </summary>
        public string TransactionCode { get; set; }

        /// <summary>
        /// วันที่เกิดรายการ เป็น Unix Timestamp
        /// </summary>
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// ข้อมูลพิเศษสำหรับการส่งคำร้องแต่ละบริการ (ทั้งหมด)
        /// </summary>
        public Dictionary<string, ApplicationRequestDataGroupEntity> Data { get; set; }

        /// <summary>
        /// ข้อมูลพิเศษสำหรับการส่งคำร้องแต่ละบริการ (กรองเฉพาะข้อมูลที่ใบอนุญาตต้องการแล้ว)
        /// มีใน ApplicationRequestVersion 2 ขึ้นไป
        /// </summary>
        public Dictionary<string, ApplicationRequestDataGroupEntity> DataFiltered { get; set; }

        /// <summary>
        /// ข้อมูลพิเศษสำหรับการส่งคำร้องแต่ละบริการ (ส่วนที่ใบอนุญาตไม่ต้องการ)
        /// มีใน ApplicationRequestVersion 2 ขึ้นไป
        /// </summary>
        public Dictionary<string, ApplicationRequestDataGroupEntity> DataExcluded { get; set; }
        
        public class FormConfig
        {
            public FormSectionGroup[] Groups { get; set; }
            public FormSection[] Sections { get; set; }
            public FormSectionRow[] Rows { get; set; }
        }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ร้องขอไฟล์เอกสารเพิ่มเติม
        /// </summary>
        public List<FileMetadataEntity> RequestedFiles { get; set; }

        /// <summary>
        /// ไฟล์ที่ได้รับจากหน่วยงาน
        /// </summary>
        public List<FileMetadataEntity> GovFiles { get; set; }

        /// <summary>
        /// ไฟล์ e-Permit (ใบอนุญาต online ที่ส่งให้ผู้ขอใบอนุญาต)
        /// </summary>
        public List<FileMetadataEntity> EPermitFiles { get; set; }

        /// <summary>
        /// ไฟล์ Billing payment (สำหรับให้เจ้าหน้าที่ upload เมื่อมีการเลือก PaymentMethodEnabledChoice = BillPayment)
        /// ไฟล์ Bill Payment จากหน่วยงาน แยกออกมาจาก GovFiles
        /// </summary>
        public List<FileMetadataEntity> BillPaymentFiles { get; set; }

        /// รับใบอนุญาตทันทีหลังชำระเงินถ้า value = true
        /// </summary>
        public bool PermitCanBeDeliveredOnPayment { get; set; }

        /// <summary>
        /// วันที่ให้ผู้ยื่นคำร้องมารับใบอนุญาต
        /// </summary>
        public DateTime? UserCanGetAppDate { get; set; }

        public DateTime? UserCanGetAppDateEnd { get; set; }

        public DateTime? ExpectedFinishDate { get; set; }

        public string LastUpdatedFrom { get; set; }

        /// <summary>
        /// ใช้ flag ตอนดึง query จาก report เพื่อไม่ให้เรียกซ้ำ
        /// </summary>
        public bool isDone { get; set; } = false;
        #endregion

        #region [Display]

        [BsonIgnore]
        public object ApplicationName { get; set; }

        [BsonIgnore]
        public string StatusName { get; set; }

        [BsonIgnore]
        public string CreatedDateTxt { get { return CreatedDate.ToLocalTime().ToStringFormat(); } }

        [BsonIgnore]
        public string UpdatedDateTxt { get { return UpdatedDate.ToLocalTime().ToStringFormat(); } }

        [BsonIgnore]
        public List<CgdPayment> CgdPayments { get; set; }
        #endregion

        public DateTime? ConsentDateTimeStamp { get; set; }
    }

    public class CgdPayment
    {
        public decimal Amount { get; set; }

        public string CatalogCode { get; set; }

        public string CatalogName { get; set; }

        public string HomeCostcenterCode { get; set; }

        public string HomeCostCenterName { get; set; }
    }
}
