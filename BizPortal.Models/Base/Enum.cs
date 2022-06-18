using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace BizPortal
{
    public enum ExtraKeyEnum
    {
        UPLOADER_ID,
        REQUEST_FILE_ID,
        REQUEST_FILE_NAME,
        REQUEST_FILE_REASON,

        LEGACY,
        LEGACY_VERSION,
        LEGACY_TRANSFORMED_DATE,

        JURISTIC_APP_STATUS_REQUEST_ID,

        REQUEST_INFORMATION,
        BIZ_REGISTRATION_ID
    }

    public enum PageNameEnum
    {
        [StringValue("navDashboard")]
        DASHBOARD,
        [StringValue("navAboutInvestment")]
        ABOUT_INVESTMENT,
        [StringValue("navContactUs")]
        CONTACT_US,
        [StringValue("navCompanyProfile")]
        COMPANY_PROFILE,
    };

    public enum PageNameBackendEnum
    {
        [StringValue("navDashboard")]
        DASHBOARD,
        [StringValue("navArticle")]
        ARTICLE,
        [StringValue("navApps")]
        APPS,
        [StringValue("navAppRequest")]
        APP_STATUS,
        [StringValue("navAppManage")]
        APP_MANAGE,
        [StringValue("navMemberManage")]
        MEMBER_MANAGE,

        [StringValue("navAboutInvestment")]
        ABOUT_INVESTMENT,
        [StringValue("navContactUs")]
        CONTACT_US,
        [StringValue("navCompanyProfile")]
        COMPANY_PROFILE,
        [StringValue("navReport")]
        REPORT,
        [StringValue("navReportOverAll")]
        REPORT_OVERALL,
        [StringValue("navReportStatistics")]
        REPORT_STATISTICS,
        [StringValue("navReportOnProcessRequest")]
        REPORT_ON_PROCESS_REQUEST,
        [StringValue("navExportReport")]
        EXPORT_REPORT,
        [StringValue("navReportRole")]
        REPORT_ROLE,
    };

    public enum DropdownlistType
    {
        ALL,
        SELECT,
        NONE
    }

    public enum QuestionEnumType
    {
        [StringValue("QUESTION_WEBSITE")]
        Website = 1,
        [StringValue("QUESTION_CONTACT")]
        Contact = 2,
        [StringValue("QUESTION_CLAIM")]
        Claim = 3,
        [StringValue("QUESTION_OTHER")]
        Other = 4,
    }

    public enum NotifyMsgType
    {
        [StringValue("success")]
        Success,
        [StringValue("error")]
        Error,
        [StringValue("warning")]
        Warning,
        [StringValue("info")]
        Info
    };

    public enum ShowMsgType
    {
        [StringValue("Unauthorized")]
        Unauthorized,
        [StringValue("InvalidAccount")]
        InvalidAccount,
        [StringValue("ProfileUpdated")]
        ProfileUpdated,
        [StringValue("FailedToUpdate")]
        FailedToUpdate,
        [StringValue("ApplicationNotFound")]
        ApplicationNotFound,
        [StringValue("NeedValidate")]
        NeedValidate
    }

    public enum UserTypeEnum
    {
        [StringValue("Citizen")]
        Citizen = 0,
        [StringValue("JuristicPerson")]
        Juristic = 1,
        [StringValue("Foreigner")]
        Foreigner = 2,
        [StringValue("GovernmentAgent")]
        GovernmentAgent = 3,
        [StringValue("Anonymous")]
        Anonymous = 4
    };

    public enum UserProviderEnum
    {
        [StringValue("EGA OpenID Citizen")]
        EGAOpenIDCitizen = 0,
        [StringValue("NDID")]
        NDID = 1,
        [StringValue("EGA OpenID")]
        EGAOpenID = 2
    };

    // application file type
    public enum FileStatus
    {
        [StringValue("Temp")]
        Temp = 0,
        [StringValue("InUse")]
        InUse = 1
    }

    public enum EmployerRegisUserType
    {
        [StringValue("Coperate")]
        Coperate,
        [StringValue("Employer")]
        Employer
    }

    public enum EmployeeRegisFileType
    {
        [GroupValue("Form")]
        [StringValue("หนังสือนำส่งแบบขึ้นทะเบียนผู้ประกันตน สปส.1-03/สปส.1-03/1 (สปส.1-02)")]
        [TypeValue("RegistrationForm")]
        [ValidationValue("required")]
        RegistrationForm102,

        [GroupValue("Form")]
        [StringValue("สปส. 6-07")]
        [TypeValue("RegistrationForm")]
        [ValidationValue("required")]
        RegistrationForm607,

        [GroupValue("Employee,Thai,Foreigner")]
        [StringValue("แบบขึ้นทะเบียนผู้ประกันตน (สปส. 1-03)")]
        [TypeValue("AttachedFile")]
        [ValidationValue("required")]
        RegistrationForm103,

        [GroupValue("Employee,Foreigner")]
        [StringValue("สำเนาหนังสือเดินทาง/สำเนาใบสำคัญประจำตัวคนต่างด้าว")]
        [TypeValue("AttachedFile")]
        [ValidationValue("required")]
        Passport,

        [GroupValue("Employee,Foreigner")]
        [StringValue("สำเนาใบอนุญาติทำงานคนต่างด้าว")]
        [TypeValue("AttachedFile")]
        [ValidationValue("required")]
        WorkPermit,
    }

    public enum EmployerRegisFileType
    {
        // all
        [GroupValue("Coperate,Employer")]
        [TypeValue("EMPLOYER_REGISTER_FORM_1_0_1")]
        [StringValue("แบบขึ้นทะเบียนายจ้าง (สปส.1-01) <br/>  (ในกรณีที่เนื้อที่กรอกข้อมูลในแบบฟอร์มข้อ 2, ข้อ 3.2 และข้อ 6 ไม่เพียงพอ ขอให้ใช้กระดาษอื่นกรอกข้อมูลต่อไปจนครบ จากนั้นบันทึกไฟล์เอกสารและอัปโหลดเพิ่ม)")]
        [Description("แบบขึ้นทะเบียนนายจ้าง (สปส.1-01)")]
        [ValidationValue("required")]
        RegistrationForm,

        // coperate
        [GroupValue("Coperate")]
        [TypeValue("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY")]
        [StringValue("สำเนาหนังสือรับรองการจดทะเบียนนิติบุคคล พร้อมวัตถุประสงค์ หรือสำเนาใบทะเบียนพาณิชย์ หรือหนังสือรับรองตามพระราชบัญญัติประกอบธุรกิจคนต่างด้าว พ.ศ.2542 หรือสำเนาสัญญาร่วมค้า หรือสำเนาหนังสือรับรองแสดงการ จดทะเบียนนิติบุคคลของทุกรายที่ร่วมค้ำ หรือสำเนาหนังสือรับรองหรือหลักฐานการเป็นนิติบุคคลซึ่งแสดงรายการเกี่ยวกับชื่อ ทุน วัตถุประสงค์ รายชื่อกรรมการและผู้มีอำนาจลงนามผูกพันนิติบุคคล และหนังสือแต่งตั้งผู้แทนโดยผู้มีอำนาจลงนามผูกพันนิติบุคคลที่ขอจดทะเบียนนายจ้างเป็นผู้แต่งตั้งให้มีหน้าที่รับผิดชอบดำเนินกิจการในประเทศไทยแทนนิติบุคคล หรือใบอนุญาตให้ประกอบกิจการที่ออกตามกฎหมายอื่น")]
        [Description("สําเนาหรือภาพถ่ายหนังสือรับรองการจดทะเบียนนิติบุคคลพร้อมวัตถุประสงค์")]
        [ValidationValue("required")]
        RegistrationJuristic,

        [GroupValue("Coperate")]
        [TypeValue("TAX_IDENTITY_COPY_OR_FORGEIGN_BUSSINESS_LICENSE")]
        [StringValue("สำเนาบัตรประจำตัวผู้เสียภาษีอากร หรือสำเนาคำขอจดทะเบียนภาษีมูลค่าเพิ่ม (ภ.พ.01) หรือสำเนาภาษีธุรกิจเฉพาะ (ภธ.20) หรือใบอนุญาตประกอบธุรกิจของคนต่างด้าว")]
        [Description("สำเนาหรือภาพถ่าย ภ.พ.01/ ภธ.20/ ใบอนุญาตประกอบธุรกิจของคนต่างด้าว")]
        [ValidationValue("required")]
        RegistrationVat,

        // employer
        [GroupValue("Employer")]
        [TypeValue("IDENTITY_COPY")]
        [StringValue("สําเนาหรือภาพถ่ายบัตรประจําตัวประชาชน (คนต่างด้าวใช้สําเนาหนังสือเดินทางหรือใบสําคัญประจำตัวคนต่างด้าว)")]
        [Description("สําเนาหรือภาพถ่ายบัตรประจําตัวประชาชน (คนต่างด้าวใช้สําเนาหนังสือเดินทางหรือใบสําคัญประจำตัวคนต่างด้าว)")]
        [ValidationValue("required")]
        CitizenCard,

        [GroupValue("Employer")]
        [TypeValue("HOUSEHOLD_REGISTRATION_COPY")]
        [StringValue("สําเนาหรือภาพถ่ายทะเบียนบ้าน")]
        [Description("สําเนาหรือภาพถ่ายทะเบียนบ้าน")]
        [ValidationValue("required")]
        RegistrationBook,

        [GroupValue("Employer")]
        [TypeValue("TAX_IDENTITY_COPY")]
        [StringValue("สําเนาบัตรประจำตัวผู้เสียภาษีอากร หรือสําเนาภาพถ่ายใบอนุญาตประกอบกิจการโรงงาน (ร.ง. 4) หรือสําเนาหรือภาพถ่ายใบทะเบียนภาษีมูลคาเพิ่ม (ภ.พ.20) หรือสําเนาภาพถ่ายภาษีธุรกิจเฉพาะ (ภ.ธ. 20)")]
        [Description("สำเนาหรือภาพถ่าย บัตรประจำตัวผู้เสียภาษีอากร/ร.ง. 4/ภ.พ.20/ภ.ธ. 20")]
        [ValidationValue("required")]
        RegistrationPersonalVat,

        // all
        [GroupValue("Coperate,Employer")]
        [TypeValue("MAP_AND_PHOTO_OF_ORGANIZATION")]
        [StringValue("แผนที่ตั้งและภาพถ่ายของสถานประกอบการ")]
        [Description("แผนที่ตั้งและภาพถ่ายของสถานประกอบการ")]
        [ValidationValue("required")]
        Map,

        [GroupValue("Coperate,Employer")]
        [TypeValue("POWER_OF_ATTORNEY_COPY")]
        [StringValue("หนังสือมอบอำนาจ (เฉพาะกรณีมอบอำนาจให้บุคคลอื่นกระทำการแทนพร้อมติดอากรแสตมป์ตามที่ประมวลรัษฎากรกำหนด) แนบสำเนาบัตรประจำตัวประชำชนของผู้มอบอำนาจและผู้รับมอบอำนาจ")]
        [Description("หนังสือมอบอำนาจ")]
        PowerOfAttorney,

        [GroupValue("Coperate,Employer")]
        [TypeValue("RENTAL_AGREEMENT_COPY")]
        [StringValue("สำเนาหนังสือสัญญาเช่า หรือหนังสือยินยอมให้ใช้สถานที่พร้อมสำเนาบัตรประจำตัวประชาชนและสำเนาทะเบียนบ้านของผู้ให้เช่า หรือสัญญาเช่าแผง หรือหลักฐานแสดงเลขที่แผงและที่ตั้งของตลาด หรือห้างสรรพสินค้า")]
        [Description("สำเนาหนังสือสัญญาเช่า หรือหนังสือยินยอมให้ใช้สถานที่ หรือสัญญาเช่าแผง หรือหลักฐานแสดงเลขที่แผงและที่ตั้งของตลาด หรือห้างสรรพสินค้า")]
        Contract,

        [GroupValue("Coperate,Employer")]
        [TypeValue("EMPLOYER_IDENTITY_COPY")]
        [StringValue("หลักฐานการแสดงตัวของนายจ้าง เช่น สำเนาบัตรประจำตัวประชาชน หรือสำเนาทะเบียนบ้านของนายจ้างหรือกรรมการผู้มีอำนาจลงนาม (กรณีกรรมการบริษัทเป็นชาวต่างชาติ ใช้สำเนาPASSPORT หรือWORK PERMIT หรือVISA)(คนต่างด้าวใช้สำเนาหนังสือเดินทาง หรือสำเนาใบสำคัญประจำตัวคนต่างด้าว) หรือสำเนาใบสำคัญถิ่นที่อยู่ในราชอาณาจักร หรือหลักฐานการได้รับอนุญาตให้เข้ามาในราชอาณาจักรเป็นการชั่วคราวตามกฎหมายว่าด้วยคนเข้ามืองของผู้มีอำนาจผูกพันนิติบุคคล เป็นต้น")]
        [Description("หลักฐานการแสดงตัวของนายจ้าง")]
        [ValidationValue("required")]
        EmployerDoc
    }

    public enum ApplicationStatusEnum
    {
        [StringValue("PENDING")]
        PENDING = 1,
        [StringValue("REJECTED")]
        REJECTED = 2,
        [StringValue("CANCEL")]
        CANCEL = 3,
        [StringValue("COMPLETED")]
        COMPLETED = 4,
        [StringValue("OTHER")]
        OTHER = 5,
        [StringValue("DRAFT")]
        DRAFT = 6
    };

    public enum ApplicationStatusV2Enum
    {
        /// <summary>
        ///  0 แบบร่างคำร้อง
        /// สถานะนี้จะขึ้นเมื่อผู้ประกอบการหรือประชาชน ทำการกดปุ่มบันทึกแบบร่างคำร้อง
        /// *** ปัจจุบันไม่ได้ใช้งานแล้ว ***
        /// </summary>
        DRAFT,
        /// <summary>
        /// 1 คำร้องไม่เสร็จสมบูรณ์ ใช้ในกรณีที่ต้องรอการตอบรับจากระบบปลายทาง หรือผู้ใช้งานจะต้องยืนยันข้อมูลผ่านระบบอื่นหรืออีเมล์
        /// </summary>
        INCOMPLETE,
        /// <summary>
        /// 2 รอการตอบรับจากเจ้าหน้าที่
        /// สถานะนี้จะขึ้นเมื่อผู้ประกอบการหรือประชาชน ทำการกดปุ่มส่งแบบคำร้อง
        /// </summary>
        WAITING,
        /// <summary>
        /// 3 ระหว่างตรวจสอบเอกสาร
        /// สถานะนี้จะขึ้นเมื่อเจ้าหน้าที่หน่วยงานทำการกดปุ่มหรือส่ง webservice เพื่อตรวจสอบเอกสาร หรือเพื่อขอเอกสารเพิ่มเติมโดยสามารถใส่รายละเอียดใน Status Other เพิ่มคู่มาได้ 
        /// เช่น อยู่ระหว่างตรวจสอบความครบถ้วนของเอกสาร, เอกสารไม่ครบถ้วน รอผู้ใช้บริการส่งเอกสารเพิ่มเติม, เอกสารไม่ถูกต้อง รอผู้ใช้บริการส่งเอกสารใหม่, และสามารถระบุชื่อรายการเอกสารที่ต้องการเพิ่มเติมได้
        /// </summary>
        CHECK,
        /// <summary>
        /// 4 พิจารณา
        /// สถานะนี้จะขึ้นเมื่อเจ้าหน้าที่หน่วยงานทำการกดปุ่มหรือส่ง webservice มีการรับเอกสารครบถ้วนสมบูรณ์เพื่อเริ่มดำเนินการ 
        /// หรือหากไม่มีเอกสารถือว่าเป็นการเริ่มดำเนินการโดยสามารถใส่รายละเอียดใน Status Other เพิ่มคู่มาได้ เช่น เอกสารครบถ้วนสมาบูรณ์, แจ้งการชำระเงิน, รับเรื่องเพื่อดำเนินการ เป็นต้น
        /// </summary>
        PENDING,
        /// <summary>
        /// 5 อนุมัติแล้วรอชำระค่าธรรมเนียม
        /// </summary>
        APPROVED_WAITING_PAY_FEE,
        /// <summary>
        /// 6 ออกใบอนุญาต
        /// </summary>
        PAID_FEE_CREATING_LICENSE,
        /// <summary>
        /// 7 ยกเลิกการดำเนินการ
        /// สถานะนี้จะขึ้นเมื่อเจ้าหน้าที่หน่วยงานทำการกดปุ่มหรือส่ง webservice ยกเลิกคำร้อง
        /// โดยสามารถใส่รายละเอียดใน Status Other เพิ่มคู่มาได้ เช่น ไม่ผ่านเกณฑ์, ไม่อนุมัติ เป็นต้น
        /// Note: สิ้นการดำเนินการยื่นคำร้อง ไม่สามารถแก้ไขสถานะได้อีก
        /// </summary>
        REJECTED,
        /// <summary>
        /// 8 ยื่นเรื่องเสร็จสมบูรณ์
        /// สถานะนี้จะขึ้นเมื่อเจ้าหน้าที่หน่วยงานทำการกดปุ่มหรือส่ง webservice ดำเนินการเสร็จสมบูรณ์
        /// Note: สิ้นการดำเนินการยื่นคำร้อง ไม่สามารถแก้ไขสถานะได้อีก
        /// </summary>
        COMPLETED,
        /// <summary>
        /// 9 ยื่นคำร้องไม่สำเร็จ
        /// </summary>
        FAILED,
        /// <summary>
        /// 10 ผู้ประกอบการยกเลิก
        /// </summary>
        CANCELED
    };
 
    public enum FileRequestTypeEnum
    {
        Request = 1,
        Response = 2,

    }

    public enum FileServiceTypeEnum
    {
        Upload = 1,
        Download = 2,

    }

    public enum CGDPersonType
    {
        Citizen = 1,
        Juristic = 2
    }

    public enum CGDPaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }

    public enum CGDInvoiceStatus
    {
        [StringValue("ข้อมูลที่ยังไม่ได้ชำระเงิน (รอชำระ)")]
        Pendding = 0,

        [StringValue("ข้อมูลที่ชำระเงินแล้ว  (ชำระสำเร็จ)")]
        OnlineConfirm = 1,

        [StringValue("ข้อมูลที่ยังไม่ได้ชำระเงิน (ยกเลิกรายการ)")]
        Cancel = 2,

        [StringValue("ข้อมูลที่ยังไม่ได้ชำระเงิน (เกินวันที่ครบกำหนดชำระ)")]
        OverDue = 3,

        [StringValue("ข้อมูลที่ชำระเงินแล้ว และผ่านการยืนยันการชำระเงิน(กระทบยอดสำเร็จ)")]
        Reconcile = 4,

        [StringValue("ข้อมูลที่ชำระเงินแล้ว (ออกใบเสร็จรับเงินแล้ว)")]
        RecreiptCreated = 5,

        [StringValue("บันทึกร่างข้อมูลที่ยังไม่ได้ชำระเงิน (ยังรอการยืนยัน Bill Payment)")]
        Draft = 6,

        [StringValue("เช็ครอเคลียร์ ข้อมูลที่ชำระเงินด้วยเช็คธนาคารแต่ต้องรอผลการชำระด้วยเช็คสำเร็จจากธนาคาร")]
        PendingChequeClearing = 7,
    }

    public enum CGDCheckPaymentStatus
    {
        [StringValue("ทุกสถานะ")]
        All = 99,

        [StringValue("ข้อมูลที่ชำระเงินแล้ว")]
        OnlineConfirm = 3,

        [StringValue("ข้อมูลที่ผ่านการยืนยันการชำระเงิน")]
        Reconcile = 4
    }

    public enum EdocumentXMLStandard 
    {
        UNCEFACT,
        hl7
    }

    public enum EDocumentPermitType
    {
        [StringValue("เอกสารหน่วยงาน")]
        Organization,
        [StringValue("เอกสารจากระบบ")]
        Template
    }

    public enum EDocumentType
    {
        [StringValue("ไม่มีการลงนาม")]
        NotSign = -1,
        [StringValue("ลงนามแบบบุคคล")]
        Personal = 0,
        [StringValue("ลงนามแบบหน่วยงาน")]
        Organization = 1,
        [StringValue("ลงลายมือชื่อแบบหน่วยงานโดยบุคคล")]
        OrgByPerson = 2
    }

    public enum EDocumentStatus
    {
        [StringValue("ใหม่")]
        NEW,
        [StringValue("อยู่ระหว่างดำเนินการ")]
        PENDING,
        [StringValue("สำเร็จ")]
        COMPLETED,
        [StringValue("ปฏิเสธ")]
        REJECTED,
        [StringValue("ลบ")]
        DELETED
    }

    public enum PersonalSigningStatus
    {
        [StringValue("รอลงนาม")]
        NEW,
        [StringValue("ลงนามสำเร็จ")]
        SIGNED,
        [StringValue("ปฏิเสธลงนาม")]
        REJECTED,
        [StringValue("ลงนามไม่สำเร็จ")]
        FAILED
    }

    public enum SigningExtendedDataType 
    {
        Text = 1,
        Number = 2,
        Date = 3
    }

    #region EnumExtension
    public class StringValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
        #endregion
    }

    public class TypeValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string TypeValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>

        public TypeValueAttribute(string value)
        {
            this.TypeValue = value;
        }
        #endregion
    }

    public class GroupValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string GroupValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public GroupValueAttribute(string value)
        {
            this.GroupValue = value;
        }
        #endregion
    }

    public class ValidationValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string ValidationValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public ValidationValueAttribute(string value)
        {
            this.ValidationValue = value;
        }
        #endregion
    }


    public class EnumList
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Validation { get; set; }
    }

    public static class EnumUtils
    {
        public static String GetEnumName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }
        public static String GetEnumDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].Description;
            }

            return value.ToString();
        }
        public static String GetEnumStringValue(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            StringValueAttribute[] enumAttributes = (StringValueAttribute[])fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].StringValue;
            }

            return value.ToString();
        }
        public static String GetEnumTypeValue(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            TypeValueAttribute[] enumAttributes = (TypeValueAttribute[])fieldInfo.GetCustomAttributes(typeof(TypeValueAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].TypeValue;
            }

            return value.ToString();
        }
        public static String GetEnumGroupValue(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            GroupValueAttribute[] enumAttributes = (GroupValueAttribute[])fieldInfo.GetCustomAttributes(typeof(GroupValueAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].GroupValue;
            }

            return value.ToString();
        }
        public static String GetEnumValidationValue(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            ValidationValueAttribute[] enumAttributes = (ValidationValueAttribute[])fieldInfo.GetCustomAttributes(typeof(ValidationValueAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].ValidationValue;
            }

            return value.ToString();
        }

        public static T GetEnum<T>(object value, bool useStringValue = false) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            Array values = Enum.GetValues(typeof(T));
            for (int i = 0; i < values.Length; i++)
            {
                Object obj = values.GetValue(i);
                if (useStringValue)
                {
                    if (((Enum)obj).GetEnumStringValue() == value.ToString())
                        return (T)obj;
                }
                else
                {
                    if (obj.ToString() == value.ToString())
                        return (T)obj;
                }
            }
            throw new InvalidOperationException("Input value does not match the target type.");
        }
        public static List<SelectListItem> ToSelectList<TEnum>(this TEnum value, bool useStringvalue = false, bool userDescriptionToDisplayText = false)
        {
            List<SelectListItem> selectList = Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .OrderBy(e => e)
                    .Select(e => new SelectListItem
                    {
                        Text = userDescriptionToDisplayText ? ((Enum)((object)e)).GetEnumDescription() : ((Enum)((object)e)).GetEnumStringValue(),
                        Value = useStringvalue ? ((Enum)((object)e)).GetEnumStringValue() : ((int)((object)e)).ToString()
                    }).ToList();
            return selectList;
        }
        public static List<EnumList> ToEnumList<TEnum>(this TEnum value)
        {
            List<EnumList> lst = Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .OrderBy(e => e)
                    .Select(e => new EnumList
                    {
                        Name = ((Enum)((object)e)).GetEnumName(),
                        Group = ((Enum)((object)e)).GetEnumGroupValue(),
                        Description = ((Enum)((object)e)).GetEnumDescription(),
                        Text = ((Enum)((object)e)).GetEnumStringValue(),
                        Type = ((Enum)((object)e)).GetEnumTypeValue(),
                        Value = ((int)((object)e)),
                        Validation = ((Enum)((object)e)).GetEnumValidationValue()
                    }).ToList();

            return lst;
        }

        public static ApplicationStatusV2Enum ToEnumValue(this object status)
        {
            ApplicationStatusV2Enum statusEnum;
            if (!Enum.TryParse(status.ToString(), out statusEnum))
            {
                throw new Exception("Error convert Status object to Enum");
            }

            return statusEnum;
        }
    }
    #endregion
}
