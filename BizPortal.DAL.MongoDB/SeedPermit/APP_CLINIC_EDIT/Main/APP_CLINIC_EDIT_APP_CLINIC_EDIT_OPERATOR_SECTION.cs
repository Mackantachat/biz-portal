
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_OPERATOR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_OPERATOR_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_CLINIC_EDIT_OPERATOR_SECTION",
                //               SectionGroup = "APP_CLINIC_EDIT",
                //               Type = SectionType.Form,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_CLINIC_EDIT,
                //               },
                //Ordering = 7,
                //DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION(),
                //DisableCondition = DISABLE_APP_CLINIC_EDIT_OPERATOR_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_OPERATOR_SECTION",
                    SectionGroup = "APP_CLINIC_EDIT_CUS_OPERTION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                    },
                    Ordering = 7,
                    //DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION(),
                    //DisableCondition = DISABLE_APP_CLINIC_EDIT_OPERATOR_SECTION(),
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_OPERATOR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_OPERATOR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_16",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_17",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_18",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_19",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_AGE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_AGE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_20",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_21",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            MaskInputReverse = true,
                        	DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION_ID_CARD(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_22",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_PASSPORT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_PASSPORT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                        	{
                                { "A", "{ pattern: /[^ก-๙]/, optional: true, recursive: true }" },
                        	},
                        	DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION_PASSPORT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_23",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_PROFESSIONAL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_PROFESSIONAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "PRACTICING_DISEASE", Text = "ประกอบวิชาชีพ" },
                                new Select2Opt() { ID = "MEDICAL_PRACTICE", Text = "ประกอบโรคศิลปะ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_24",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_BRANCH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_BRANCH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MEDICINE", Text = "เวชกรรม" },
                                new Select2Opt() { ID = "DENTAL", Text = "ทันตกรรม" },
                                new Select2Opt() { ID = "NURSING_AND_MIDWIFERY", Text = "การพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "MEDICAL_TECHNIQUE", Text = "เทคนิคการแพทย์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "กายภาพบำบัด" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_MEDICINE", Text = "การแพทย์แผนไทย" },
                                new Select2Opt() { ID = "HEALING_ARTS_PRACTICE", Text = "การประกอบโรคศิลปะ" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICINE", Text = "เฉพาะทางเวชกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_DENTISTRY", Text = "เฉพาะทางทันตกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_IN_NURSING_AND_MIDWIFE", Text = "เฉพาะทางด้านการพยาบาลและผดุงครรภ์" },
                                new Select2Opt() { ID = "SPECIALIZED_IN_THE_ENT", Text = "เฉพาะทาง หู คอ จมูก" },
                                new Select2Opt() { ID = "HEART_DISEASE", Text = "เฉพาะทางโรคหัวใจ" },
                                new Select2Opt() { ID = "SPECIFIC_TO_CANCER", Text = "เฉพาะทางโรคมะเร็ง" },
                                new Select2Opt() { ID = "OTHER_SPECIALTY", Text = "เฉพาะทางอื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_25",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_26",
                            Control = "APP_CLINIC_EDIT_OPERATOR_SECTION_LICENSE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_SECTION_LICENSE_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                        	{
                        		EndDate = "0d",
                        	},
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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
