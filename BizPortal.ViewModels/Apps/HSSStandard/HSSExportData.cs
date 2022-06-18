using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.HSSStandard
{
    public class HSSExportData
    {
        public int FormRevisionCode { get; set; }
        public string FormRevisionName { get; set; }
        public int ApplicationRequestVersion { get; set; }
        public string IdentityID { get; set; }
        public string IdentityName { get; set; }
        public string IdentityType { get; set; }
        public int ApplicationID { get; set; }
        public string RequestBatchID { get; set; }
        public object Fee { get; set; }
        public string EMSFee { get; set; }
        public object DueDateForPayFee { get; set; }
        public int? Duration { get; set; }
        public int? ProvinceID { get; set; }
        public int? AmphurID { get; set; }
        public int? TumbolID { get; set; }
        public string Province { get; set; }
        public string Amphur { get; set; }
        public string Tumbol { get; set; }
        public string SourceIPAddress { get; set; }
        public string OrgCode { get; set; }
        public string OrgNameTH { get; set; }
        public string OrgAddress { get; set; }
        public string PermitName { get; set; }
        public string BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string AppSysName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ExpectSLADate { get; set; }
        public DateTime UpdatedDateByRequestor { get; set; }
        public DateTime UpdatedDateByAgent { get; set; }
        public object UpdatedByAgent { get; set; }
        public object LastRequestorUpdateEmail { get; set; }
        public object isViewed { get; set; }
        public string Status { get; set; }
        public string StatusOther { get; set; }
        public object StatusRemark { get; set; }
        public bool IsAgentCheckUserCorrection { get; set; }
        public object StatusBeforeCancel { get; set; }
        public object ApplicationRequestNumberAgent { get; set; }
        public object ActionReply { get; set; }
        public object PermitDeliveryAddress { get; set; }
        public object PermitDeliveryType { get; set; }
        public object EMSFeePaymentType { get; set; }
        public object PaymentMethod { get; set; }
        public object PaymentMethodEnabledChoice { get; set; }
        public object PaymentMethodOrgDetail { get; set; }
        public object PaymentMethodOrgAddress { get; set; }
        public object PaymentMethodOrgTel { get; set; }
        public object BillPaymentTypeForPaymentMethod { get; set; }
        public object PermitDeliveryTypeEnabledChoice { get; set; }
        public object PermitDeliveryOrgDetail { get; set; }
        public object PermitDeliveryOrgAddress { get; set; }
        public object PermitDeliveryOrgTel { get; set; }
        public object EMSTrackingNumber { get; set; }
        public object WaitingApproveDateTime { get; set; }
        public object CheckApproveDateTime { get; set; }
        public object PendingApproveDateTime { get; set; }
        public object PaidFeeApproveDateTime { get; set; }
        public object CreateLicenseApproveDateTime { get; set; }
        public object RejectDateTime { get; set; }
        public bool NoDocument { get; set; }
        public object TransactionCode { get; set; }
        public object TransactionDate { get; set; }
        public object DataFiltered { get; set; }
        public object DataExcluded { get; set; }
        public object Remark { get; set; }
        public object RequestedFiles { get; set; }
        public List<object> GovFiles { get; set; }
        public object EPermitFiles { get; set; }
        public List<object> BillPaymentFiles { get; set; }
        public bool PermitCanBeDeliveredOnPayment { get; set; }
        public object UserCanGetAppDate { get; set; }
        public object UserCanGetAppDateEnd { get; set; }
        public object ExpectedFinishDate { get; set; }
        public object LastUpdatedFrom { get; set; }
        public bool isDone { get; set; }
        public string ApplicationRequestID { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public int ApplicationRequestRunningNumber { get; set; }
        public object Chats { get; set; }
        public HSSAppData Data { get; set; }
    }
    public class APP_CLINIC_OWNED_OPARETOR_SECTION_DATA
    {
        public int APP_CLINIC_OWNED_OPARETOR_SECTION_TOTAL { get; set; }
        public List<Owned> Owners { get; set; }


    }

    public class Owned
    {
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_FIRSTNAME { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_LASTNAME { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_AGE { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_ID { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_PASSPORT { get; set; }
        public string ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_EMAIL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSE_NUMBER { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_LICENSING_DATE { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_DIPLOMA { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_PLACE_NAME { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_WOKING_LICENSE_NUMBER { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_OTHER { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_OTHER { get; set; }
        public string ADDRESS_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_MOO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_VILLAGE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_SOI_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_ROAD_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_FAX_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_DAY_TIME_WOKING { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_QUIT_WOKING_DATE { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_CONFIRM_WOKING { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_DETAIL_OPTION__RADIO_TEXT { get; set; }
        public string APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION__RADIO_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TITLE_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_NATIONALITY_TEXT { get; set; }
        public string ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_BRANCH_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_OPARETOR_STATUS_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_EMPLOYEE_STATUS_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_DETAIL_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_CLINIC_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_DETAIL_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_OWNED_OPARETOR_SECTION_HOSPITAL_CHOICE_TEXT { get; set; }
        public string ADDRESS_PROVINCE_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_APP_CLINIC_OWNED_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ARR_ITEM_ID { get; set; }
    }

    public class APP_CLINIC_OWNED_OPARETOR_SECTION
    {
        public string GroupName { get; set; }

        public bool Visible { get; set; }
        public APP_CLINIC_OWNED_OPARETOR_SECTION_DATA Data { get; set; }

    }
    public class APP_CLINIC_OWNED_CONFIRM_SECTION_DATA
    {
        public string APP_CLINIC_OWNED_CONFIRM_SECTION_CONFIRM_TRUE_CLINIC_CHECKED_TRUE { get; set; }

    }
    public class APP_CLINIC_OWNED_CONFIRM_SECTION
    {
        public string GroupName { get; set; }

        public bool Visible { get; set; }
        public APP_CLINIC_OWNED_CONFIRM_SECTION_DATA Data { get; set; }

    }
    public class APP_CLINIC_LICENSE_INFO_SECTION_DATA
    {
        public string APP_CLINIC_LICENSE_INFO_SECTION_TYPE { get; set; }

    }
    public class APP_CLINIC_LICENSE_INFO_SECTION
    {
        public string GroupName { get; set; }

        public bool Visible { get; set; }
        public APP_CLINIC_LICENSE_INFO_SECTION_DATA Data { get; set; }

    }
    public class APP_CLINIC_LICENSE_DETAIL_SECTION_DATA
    {
        public int APP_CLINIC_LICENSE_DETAIL_SECTION_TOTAL { get; set; }
        public List<License> Licenses { get; set; }

    }

    public class License
    {
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY { get; set; }
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME { get; set; }
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_DAY_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_TEXT { get; set; }
        public string ARR_ITEM_ID { get; set; }
    }

    public class APP_CLINIC_LICENSE_DETAIL_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public APP_CLINIC_LICENSE_DETAIL_SECTION_DATA Data { get; set; }

    }
    public class APP_CLINIC_LICENSE_INFO_SECTION_2_DATA
    {
        public string APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE_INFO_CHECKED_TRUE { get; set; }

    }
    public class APP_CLINIC_LICENSE_INFO_SECTION_2
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public APP_CLINIC_LICENSE_INFO_SECTION_2_DATA Data { get; set; }

    }
    public class APP_CLINIC_PLAN_INFO_SECTION_DATA
    {
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_XRAY_ROOM { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ARTIFICIAL_ROOM { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_TYPE_OPTION { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_BOOTHS { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_FLOORS { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_AREA { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_WIDTH { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_LENGTH { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_HIGH { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_PROFESSIONALS { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_CONFIRM_TRUE { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_SMALL_ROOM { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_MAJOR_ROOM { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_ACUPUNCTURE_ROOM { get; set; }
        public string APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER { get; set; }

    }
    public class APP_CLINIC_PLAN_INFO_SECTION
    {
        public string GroupName { get; set; }
        //public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public APP_CLINIC_PLAN_INFO_SECTION_DATA Data { get; set; }

    }
    public class APP_CLINIC_INFO_SECTION_DATA
    {
        public string DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE { get; set; }
        public string DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE { get; set; }
        public string APP_CLINIC_INFO_SECTION_OTHER { get; set; }
        public string APP_CLINIC_INFO_SECTION_CONFIRM_CONFIRM_TRUE_FIRST { get; set; }
        public string DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE_TEXT { get; set; }

    }
    public class APP_CLINIC_INFO_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public APP_CLINIC_INFO_SECTION_DATA Data { get; set; }

    }
    public class GENERAL_INFORMATION_DATA
    {
        public string INFORMATION_HEADER__REQUEST_DATE { get; set; }
        public string INFORMATION_HEADER__REQUEST_AT { get; set; }
        public string INFORMATION__REQUEST_AS_OPTION { get; set; }
        public string COMPANY_NAME_TH { get; set; }
        public string COMPANY_NAME_EN { get; set; }
        public string GENERAL_INFORMATION__JURISTIC_TYPE { get; set; }
        public string REGISTER_DATE { get; set; }
        public string CHECKBOX_SHOW_COMMITTEE_INFORMATION { get; set; }

        public string DROPDOWN_CITIZEN_TITLE { get; set; }
        public string CITIZEN_NAME { get; set; }
        public string CITIZEN_LASTNAME { get; set; }
        public string GENERAL_INFORMATION__CITIZEN_AGE { get; set; }
        public string DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY { get; set; }
        public string IDENTITY_ID { get; set; }
        public string GENERAL_EMAIL { get; set; }
        public string DROPDOWN_CITIZEN_TITLE_TEXT { get; set; }
        public string DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT { get; set; }
        public string AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL { get; set; }
        public string AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL { get; set; }
        public string AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT { get; set; }
        public string AJAX_DROPDOWN_GENERAL_INFORMATION__CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT { get; set; }
        public string AJAX_DROPDOWN_CITIZEN_TITLE { get; set; }
        public string AJAX_DROPDOWN_CITIZEN_TITLE_EN { get; set; }
        public string CITIZEN_FIRSTNAME_EN { get; set; }
        public string CITIZEN_LASTNAME_EN { get; set; }
        public string DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE { get; set; }
        public string BIRTH_DATE { get; set; }
        public string BIRTH_DATE_AGE { get; set; }
        public string AJAX_DROPDOWN_CITIZEN_TITLE_TEXT { get; set; }
        public string AJAX_DROPDOWN_CITIZEN_TITLE_EN_TEXT { get; set; }
        public string DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT { get; set; }
        public string DROPDOWN_CITIZEN_TITLE_EN { get; set; }
        public string DROPDOWN_CITIZEN_TITLE_EN_TEXT { get; set; }
    }
    public class GENERAL_INFORMATION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public GENERAL_INFORMATION_DATA Data { get; set; }

    }
    public class HSSAppData
    {

        public GENERAL_INFORMATION GENERAL_INFORMATION { get; set; }
        public CITIZEN_ADDRESS_INFORMATION CITIZEN_ADDRESS_INFORMATION { get; set; }
        public CURRENT_ADDRESS CURRENT_ADDRESS { get; set; }
        public REQUESTOR_INFORMATION__HEADER REQUESTOR_INFORMATION__HEADER { get; set; }
        public INFORMATION_STORE INFORMATION_STORE { get; set; }
        public JURISTIC_ADDRESS_INFORMATION JURISTIC_ADDRESS_INFORMATION { get; set; }
        public COMMITTEE_INFORMATION COMMITTEE_INFORMATION { get; set; }


        //Clinic
        public APP_CLINIC_OWNED_OPARETOR_SECTION APP_CLINIC_OWNED_OPARETOR_SECTION { get; set; }
        public APP_CLINIC_OWNED_CONFIRM_SECTION APP_CLINIC_OWNED_CONFIRM_SECTION { get; set; }
        public APP_CLINIC_LICENSE_INFO_SECTION APP_CLINIC_LICENSE_INFO_SECTION { get; set; }
        public APP_CLINIC_LICENSE_DETAIL_SECTION APP_CLINIC_LICENSE_DETAIL_SECTION { get; set; }
        public APP_CLINIC_LICENSE_INFO_SECTION_2 APP_CLINIC_LICENSE_INFO_SECTION_2 { get; set; }
        public APP_CLINIC_PLAN_INFO_SECTION APP_CLINIC_PLAN_INFO_SECTION { get; set; }
        public APP_CLINIC_INFO_SECTION APP_CLINIC_INFO_SECTION { get; set; }





        //HOSPITAL_PLAN
        public APP_HOSPITAL_PLAN_INFO_SECTION APP_HOSPITAL_PLAN_INFO_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_PERSONNEL_SECTION APP_HOSPITAL_PLAN_PERSONNEL_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION { get; set; }
        public APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE { get; set; }
        public APP_HOSPITAL_LICENSE_INFO_SECTION APP_HOSPITAL_LICENSE_INFO_SECTION { get; set; }

        //HOSPITAL_PERMISSION



        public APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION { get; set; }
        public APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION { get; set; }
        public APP_HOSPITAL_PERMISSION_INFO_SECTION APP_HOSPITAL_PERMISSION_INFO_SECTION { get; set; }
    }

    public class APP_HOSPITAL_PERMISSION_INFO_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PERMISSION_INFO_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PERMISSION_INFO_SECTION_DATA
    {
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER_TYPE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_OTHER { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OBSTETRICS { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ORTHOPEDIC_DEPARTMENT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_PHYSICAL_THERAPY { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_CONFIRM_TRUE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTERNAL_MEDICINE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_SURGERY { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_PEDIATRICS { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MEDICAL_TECHNOLOGY { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DERMATOLOGY_DEPARTMENT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ARTIFICIAL_INSEMINATION { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_THAI_TRADITIONAL_MEDICINE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_NUTRITION_DEPARTMENT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_LAUNDRY_DEPARTMENT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTENSIVE_CARE_UNIT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_INTERNAL_EXAMINATION { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_SMALL_OPERATING_ROOM { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_TREATMENT_ROOM { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_AFTER_BIRTH { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_ORGAN_TRANSPLANT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_HEMODIALYSIS_ROOM { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DENTAL_ROOM { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_DIAGNOSTIC_RADIATION { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OPEN_HEART_SURGERY { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_CARDIAC_CATHETERIZATION { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_RADIATION_THERAPY { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_EXAMINATION_MAGNETIC_FIELD { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_BREAKDOWN { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MORGUE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_THAI_TRADITIONAL_MEDICINE_APPLIED { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_MASSAGE_DEPARTMENT { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_CHINESE_MEDICINE { get; set; }
        public string APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE_TEXT { get; set; }

    }

    public class APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION_DATA
    {
        public string APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION_CONFIRM_TRUE { get; set; }

    }

    public class APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DATA
    {
        public int APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TOTAL { get; set; }
        public List<OWNER> Owners { get; set; }
    }
    public class OWNER
    {
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_AGE { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ID { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_PASSPORT { get; set; }
        public string ADDRESS_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_MOO_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_VILLAGE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_SOI_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_ROAD_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_FAX_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string ADDRESS_EMAIL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TYPE { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSE_NUMBER { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSING_DATE { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DIPLOMA { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING { get; set; }


        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_PLACE_NAME { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_LICENSE_NUMBER { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_OTHER { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_OTHER { get; set; }
        public string ADDRESS_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_MOO_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_VILLAGE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_SOI_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_ROAD_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string ADDRESS_FAX_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_QUIT_WOKING_DATE { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION__RADIO_TEXT { get; set; }
        public string APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION__RADIO_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY_TEXT { get; set; }
        public string ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE_TEXT { get; set; }
        public string ADDRESS_PROVINCE_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS_TEXT { get; set; }
        public string ARR_ITEM_ID { get; set; }

    }
    public class APP_HOSPITAL_LICENSE_INFO_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_LICENSE_INFO_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_LICENSE_INFO_SECTION_DATA
    {
        public string APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE { get; set; }
        public string DROPDOWN_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE { get; set; }
        public string APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TEXT { get; set; }
        public string APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CONFIRM_APP_HOSPITAL_CONFIRM_TRUE { get; set; }
        public string DROPDOWN_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE_TEXT { get; set; }

    }

    public class APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE_DATA
    {
    }

    public class APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_DATA
    {

        public int APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TOTAL { get; set; }
        public List<DOCTOR> Dortors { get; set; }
    }

    public class DOCTOR
    {
        public string DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION{ get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_OTHER { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE{ get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_FIRSTNAME { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_LASTNAME { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_ID_CARD { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_TEXT { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE_TEXT { get; set; }
        public string ARR_ITEM_ID{ get; set; }

    }

    public class APP_HOSPITAL_PLAN_PERSONNEL_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DATA
    {
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DOCTOR_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_NURSE_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DENTIST_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_PHARMACIST_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THERAPIST_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TECHNICAL_AMONT { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL_APPLIED { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_MEDICINE { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_PHARMACY { get; set; }
        public string APP_HOSPITAL_PLAN_PERSONNEL_SECTION_OTHER { get; set; }

    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DATA
    {
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_REASON { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_YEAR { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_MONTH { get; set; }

    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_DATA
    {
        public int APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_TOTAL { get; set; }
        public List<BUSSINESS_SERVICE> BussinessServices { get; set; }
    }

    public class BUSSINESS_SERVICE
    {
        public string DROPDOWN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_AMOUNT { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_TEXT { get; set; }
        public string ARR_ITEM_ID { get; set; }

    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_DATA
    {
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_LOCATION { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_PEOPLE_AMOUNT { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_NURSE_AMOUNT { get; set; }

    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_DATA
    {
        public int APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TOTAL { get; set; }
        public List<BUSSINESS_INVEST> BussinessInvests { get; set; }
    }

    public class BUSSINESS_INVEST
    {
        public string DROPDOWN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE { get; set; }
        public string APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_PERCENT { get; set; }
        public string ARR_IDX { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string DROPDOWN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE_TEXT { get; set; }
        public string ARR_ITEM_ID { get; set; }

    }

    public class APP_HOSPITAL_PLAN_INFO_SECTION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public APP_HOSPITAL_PLAN_INFO_SECTION_DATA Data { get; set; }
    }

    public class APP_HOSPITAL_PLAN_INFO_SECTION_DATA
    {
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_1 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_2 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_4 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_7 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_10 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_11 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_12 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_13 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_14 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_15 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_OTHER { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OPTION { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OTHER { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_INVESTMENT { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_3 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_5 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_6 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_8 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_9 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_16 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_17 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_18 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_19 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_20 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_21 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_22 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_23 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_24 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_25 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_26 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_27 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_28 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_29 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_30 { get; set; }
        public string APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_APP_HOSPITAL_PLAN_MED_TYPE_31 { get; set; }

    }

    public class COMMITTEE_INFORMATION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }

        public COMMITTEE_INFORMATION_DATA Data { get; set; }
    }
    public class COMMITTEE
    {
        public string ARR_IDX { get; set; }
        public string JURISTIC_COMMITTEE_NUMBER { get; set; }
        public string JURISTIC_COMMITTEE_TITLE { get; set; }
        public string JURISTIC_COMMITTEE_NAME { get; set; }
        public string JURISTIC_COMMITTEE_LASTNAME { get; set; }
        public string JURISTIC_COMMITTEE_CITIZEN_ID { get; set; }
        public string COMMITTEE_INFORMATION_CITIZEN_ID { get; set; }
        public string JURISTIC_COMMITTEE_NATIONALITY_OPTION { get; set; }
        public string DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT { get; set; }
        public string JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION { get; set; }
        public string JURISTIC_COMMITTEE_IS_LAWYER_LICENSE_DUEDATE { get; set; }
        public string DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE { get; set; }
        public string JURISTIC_COMMITTEE_ACCOUNTING_LICENSE_ID { get; set; }
        public string JURISTIC_COMMITTEE_ACCOUNTING_DUE_DATE { get; set; }
        public string COMMITTEE_INFORMATION_PASSPORT_NUMBER { get; set; }
        public string IS_EDIT { get; set; }
        public string CUSREQ { get; set; }
        public string JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION__RADIO_TEXT { get; set; }
        public string JURISTIC_COMMITTEE_NATIONALITY_OPTION__RADIO_TEXT { get; set; }
        public string DROPDOWN_JURISTIC_COMMITTEE_ACCOUNTING_TYPE_TEXT { get; set; }
        public string AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL { get; set; }
        public string AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL { get; set; }
        public string AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_PROVINCE_DDL_TEXT { get; set; }
        public string AJAX_DROPDOWN_COMMITTEE_INFORMATION_CITIZEN_IDCARD_ISSUE_AMPHUR_DDL_TEXT { get; set; }

    }
    public class COMMITTEE_INFORMATION_DATA
    {

        public int COMMITTEE_INFORMATION_TOTAL { get; set; }

        public List<COMMITTEE> Commitees { get; set; }
    }

    public class JURISTIC_ADDRESS_INFORMATION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public JURISTIC_ADDRESS_INFORMATION_DATA Data { get; set; }
    }

    public class JURISTIC_ADDRESS_INFORMATION_DATA
    {
        public string ADDRESS_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_MOO_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_SOI_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_ROAD_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT { get; set; }
        public string ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_FAX_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS { get; set; }
        public string ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS { get; set; }

    }

    public class INFORMATION_STORE
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public INFORMATION_STORE_DATA Data { get; set; }
    }

    public class INFORMATION_STORE_DATA
    {
        public string INFORMATION_STORE_NAME_TH { get; set; }
        public string INFORMATION_STORE_NAME_EN { get; set; }
        public string ADDRESS_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_MOO_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_SOI_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_ROAD_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_FAX_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_LAT_INFORMATION_STORE__ADDRESS { get; set; }
        public string ADDRESS_LNG_INFORMATION_STORE__ADDRESS { get; set; }
        public string SEARCH_TEXT_GOOGLE_MAP { get; set; }
        public string CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION { get; set; }
        public string INFORMATION_STORE_HEALTH_OTHER { get; set; }
        public string INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE { get; set; }
        public string ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT { get; set; }
        public string INFORMATION_STORE_TSICID { get; set; }
        public string AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE { get; set; }
        public string CHECKBOX_SHOW_INFORMATION_STORE_NAME { get; set; }
        public string CHECKBOX_SHOW_INFORMATION_STORE_ADDRESS { get; set; }
        public string AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE_TEXT { get; set; }

    }

    public class REQUESTOR_INFORMATION__HEADER
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public string CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION { get; set; }
    }

    public class CURRENT_ADDRESS
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public CURRENT_ADDRESS_DATA Data { get; set; }
    }

    public class CURRENT_ADDRESS_DATA
    {
        public string ADDRESS_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_MOO_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_SOI_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_BUILDING_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_FLOOR_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_ROAD_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_FAX_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_MOBILE_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_EMAIL_CURRENT_INFORMATION__ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_CURRENT_INFORMATION__ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_CURRENT_INFORMATION__ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_CURRENT_INFORMATION__ADDRESS_TEXT { get; set; }
        public string CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS_CURRENT_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE { get; set; }


    }

    public class CITIZEN_ADDRESS_INFORMATION
    {
        public string GroupName { get; set; }
        // public IList<undefined> GroupDescription { get; set; }
        public bool Visible { get; set; }
        public CITIZEN_ADDRESS_INFORMATION_DATA Data { get; set; }
    }

    public class CITIZEN_ADDRESS_INFORMATION_DATA
    {
        public string ADDRESS_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_MOO_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_SOI_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_BUILDING_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_ROOMNO_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_FLOOR_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_ROAD_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_AMPHUR_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_TUMBOL_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_POSTCODE_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_FAX_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT { get; set; }
        public string ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT { get; set; }
        public string ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT { get; set; }
        public string CHECKBOX_SHOW_CITIZEN_ADDRESS_INFORMATION { get; set; }
        public string ADDRESS_MOBILE_CITIZEN_ADDRESS { get; set; }
        public string ADDRESS_EMAIL_CITIZEN_ADDRESS { get; set; }
        public string CITIZEN_EMAIL { get; set; }

    }

    public class HSS_EDOCDetail
    {
        public string ApplicationRequestID { get; set; }
        public string ApplicationID { get; set; }
        public string DocumentID { get; set; }
        public string SigningType { get; set; }
        public string SigningStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
