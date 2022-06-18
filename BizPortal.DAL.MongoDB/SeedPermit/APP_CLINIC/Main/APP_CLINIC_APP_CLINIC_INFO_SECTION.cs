
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC
{
    public partial class APP_CLINIC_APP_CLINIC_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_INFO_SECTION",
                    SectionGroup = "APP_CLINIC",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC",
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            InitFormSectionRow();
        }

        private static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_01_01",
                            Control = "APP_CLINIC_INFO_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MEDICAL_CLINIC", Text = "คลินิกเวชกรรม" },
                                new Select2Opt() { ID = "DENTAL_CLINIC", Text = "คลินิกทันตกรรม" },
                                new Select2Opt() { ID = "MIDWIFERY_CLINIC", Text = "คลินิกการพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY_CLINIC", Text = "คลินิกกายภาพบำบัด" },
                                new Select2Opt() { ID = "MEDICAL_TECHNOLOGY_CLINIC", Text = "คลินิกเทคนิคการแพทย์" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "คลินิกการแพทย์แผนไทย" },
                                new Select2Opt() { ID = "APPLIED_THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "คลินิกการแพทย์แผนไทยประยุกต์" },
                                new Select2Opt() { ID = "MEDICINE_CLINIC", Text = "คลินิกการประกอบโรคศิลปะ" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICAL_CLINIC", Text = "คลินิกเฉพาะทางเวชกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_DENTAL_CLINIC", Text = "คลินิกเฉพาะทางทันตกรรม" },
                                new Select2Opt() { ID = "SPECIALTY_CLINICS_MIDWIFERY", Text = "คลินิกเฉพาะทางด้านการพยาบาลและผดุงครรภ์" },
                                new Select2Opt() { ID = "UNITED_CLINIC", Text = "สหคลินิก" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC",
                        },
                        new FormControl()
                        {
                            FieldID = "42_01_02",
                            Control = "APP_CLINIC_INFO_SECTION_TYPE_MEDICINE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_INFO_SECTION_TYPE_MEDICINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "THERAPY", Text = "สาขากิจกรรมบำบัด" },
                                new Select2Opt() { ID = "INTERPRETATION_DISORDERS", Text = "สาขาการแก้ไขความผิดปกติของการสื่อความหมาย" },
                                new Select2Opt() { ID = "HEART_AND_CHEST_TECHNOLOGY", Text = "สาขาเทคโนโลยีหัวใจและทรวงอก" },
                                new Select2Opt() { ID = "TECHNICAL_RADIATION", Text = "สาขารังสีเทคนิค" },
                                new Select2Opt() { ID = "CLINICAL_PSYCHOLOGY", Text = "สาขาจิตวิทยาคลินิก" },
                                new Select2Opt() { ID = "ORTHOTICS", Text = "สาขากายอุปกรณ์" },
                                new Select2Opt() { ID = "TRADITIONAL_CHINESE", Text = "สาขาการแพทย์แผนจีน" },
                                new Select2Opt() { ID = "OTHER", Text = "อื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC",
                        },
                        new FormControl()
                        {
                            FieldID = "42_01_03",
                            Control = "APP_CLINIC_INFO_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_INFO_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_INFO_SECTION_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC",
                        },
                        new FormControl()
                        {
                            FieldID = "42_01_04",
                            Control = "APP_CLINIC_INFO_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_INFO_SECTION_CONFIRM",
                            Info = "APP_CLINIC_INFO_SECTION_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM_TRUE_FIRST", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC",
                        },
                    }
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
