
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_LICENSE
{
    public partial class APP_HOSPITAL_LICENSE_APP_HOSPITAL_LICENSE_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_LICENSE_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    SectionGroup = "APP_HOSPITAL_LICENSE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_LICENSE_INFO_SECTION").Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_01_01",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_LICENSE",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_LICENSE_INFO_TOTAL_BED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_TOTAL_BED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A######",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", "{ pattern: /[1-9//]/}"
                                },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        //new FormControl()
                        //{
                        //    FieldID = "F35_01_02",
                        //    Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 4,
                        //    DisplayMaskInput = true,
                        //    MaskInputPattern = "0#",
                        //	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        //},
                        new FormControl()
                        {
                            FieldID = "F35_01_02",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "OPT_A", RadioButtonText = "ขนาดเล็ก (จำนวน 1-30 เตียง)" },
                                    new FormRadioButton() { RadioButtonValue = "OPT_B", RadioButtonText = "ขนาดกลาง (จำนวน 31-90 เตียง)" },
                                    new FormRadioButton() { RadioButtonValue = "OPT_C", RadioButtonText = "ขนาดใหญ่ (จำนวนมากกว่า 90 เตียง)" },
                                }
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,                   
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT_HIDDEN",
                            Type = ControlType.Hidden,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_BED_AMOUNT_HIDDEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_01_03",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_01_04",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_LICENSE_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_01_05",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F35_01_06",
                            Control = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CONFIRM",
                            Info = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_HOSPITAL_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE",
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
