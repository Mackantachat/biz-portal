
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION
{
    public partial class APP_HOSPITAL_PERMISSION_APP_HOSPITAL_PERMISSION_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_INFO_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_01",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OLD_INFO_SECTION_HEADER_TYPE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OLD_INFO_SECTION_HEADER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_02",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	TextboxNumberSettings = SETTING_NUMBER_APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_03",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "GENERAL_HOSPITAL", Text = "โรงพยาบาลทั่วไป" },
                                new Select2Opt() { ID = "DENTAL_HOSPITAL", Text = "โรงพยาบาลทันตกรรม" },
                                new Select2Opt() { ID = "MIDWIFERY_HOSPITAL", Text = "โรงพยาบาลการพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "โรงพยาบาลกายภาพบำบัด" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL", Text = "โรงพยาบาลการแพทย์แผนไทย" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_APPLIED", Text = "โรงพยาบาลการแพทย์แผนไทยประยุกต์" },
                                new Select2Opt() { ID = "SPECIALIZED_EAR", Text = "โรงพยาบาลเฉพาะทาง หู คอ จมูก" },
                                new Select2Opt() { ID = "SPECIALIZED_HEART_DISEASE", Text = "โรงพยาบาลเฉพาะทาง โรคหัวใจ" },
                                new Select2Opt() { ID = "SPECIALIZED_CANCER", Text = "โรงพยาบาลเฉพาะทางโรคมะเร็ง" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วย" },
                                new Select2Opt() { ID = "SPECIALIZED_OTHER", Text = "โรงพยาบาลเฉพาะทางอื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_04",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "CHRONIC_PATIENTS", Text = "โรงพยาบาลผู้ป่วยเรื้อรัง" },
                                new Select2Opt() { ID = "PSYCHIATRIC", Text = "โรงพยาบาลผู้ป่วยจิตเวช" },
                                new Select2Opt() { ID = "ELDERLY", Text = "โรงพยาบาลผู้สูงอายุ" },
                                new Select2Opt() { ID = "MOTHER_CHILD", Text = "โรงพยาบาลแม่และเด็ก" },
                                new Select2Opt() { ID = "DRUG_TREATMENT", Text = "โรงพยาบาลบำบัดยาเสพติด" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วย" },
                                new Select2Opt() { ID = "OTHER_PATIENTS", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วยอื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_05",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_06",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "INTERNAL_MEDICINE", /* อายุรกรรม */
                                "SURGERY", /* ศัลยกรรม */
                                "OBSTETRICS", /* สูตินรีเวชกรรม */
                                "PEDIATRICS", /* กุมารเวชกรรม */
                                "MEDICAL_TECHNOLOGY", /* แผนกเทคนิคการแพทย์ */
                                "ORTHOPEDIC_DEPARTMENT", /* แผนกออร์โธปิดิกส์ */
                                "DERMATOLOGY_DEPARTMENT", /* แผนกโรคผิวหนัง */
                                "ARTIFICIAL_INSEMINATION", /* แผนกการผสมเทียม */
                                "PHYSICAL_THERAPY", /* แผนกกายภาพบำบัด */
                                "THAI_TRADITIONAL_MEDICINE", /* แผนกการแพทย์แผนไทย */
                                "NUTRITION_DEPARTMENT", /* แผนกโภชนาการ */
                                "LAUNDRY_DEPARTMENT", /* แผนกซักฟอก */
                                "INTENSIVE_CARE_UNIT", /* หอผู้ป่วยหนัก */
                                "INTERNAL_EXAMINATION", /* ห้องตรวจภายในและขูดมดลูก */
                                "SMALL_OPERATING_ROOM", /* ห้องผ่าตัดเล็ก */
                                "TREATMENT_ROOM", /* ห้องให้การรักษา */
                                "AFTER_BIRTH", /* ห้องทารกหลังคลอด */
                                "ORGAN_TRANSPLANT", /* การผ่าตัดเปลี่ยนอวัยวะ */
                                "HEMODIALYSIS_ROOM", /* ห้องไตเทียม */
                                "DENTAL_ROOM", /* ห้องทันตกรรม */
                                "DIAGNOSTIC_RADIATION", /* รังสีวินิจฉัยด้วยคอมพิวเตอร์ */
                                "OPEN_HEART_SURGERY", /* การผ่าตัดเปิดหัวใจ */
                                "CARDIAC_CATHETERIZATION", /* การสวนหัวใจ */
                                "RADIATION_THERAPY", /* รังสีบำบัด */
                                "EXAMINATION_MAGNETIC_FIELD", /* การตรวจอวัยวะภายในชนิดสนามแม่เหล็กไฟ้ฟ้า */
                                "BREAKDOWN", /* การสลายนิ่วด้วยเครื่องมือ */
                                "MORGUE", /* ห้องเก็บศพ */
                                "THAI_TRADITIONAL_MEDICINE_APPLIED", /* แผนกการแพทย์แผนไทยประยุกต์ */
                                "MASSAGE_DEPARTMENT", /* แผนกการนวด */
                                "CHINESE_MEDICINE", /* แผนกการแพทย์แผนจีน */
                                "OTHER", /* อื่นๆ */
                            },
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_07",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_08",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER",
                            Type = ControlType.Label,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HEADER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_09",
                            Control = "APP_HOSPITAL_PERMISSION_INFO_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_INFO_SECTION_CONFIRM",
                            Info = "APP_HOSPITAL_PERMISSION_INFO_SECTION_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION",
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
