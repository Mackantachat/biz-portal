
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_OWNER
{
    public partial class APP_HOSPITAL_PERMISSION_OWNER_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_OWNER",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION
                    },
					Ordering = 3,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_01",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "OPARETOR", RadioButtonText = "ผู้ดำเนินการ" },
                        			new FormRadioButton() { RadioButtonValue = "EMPLOYEE", RadioButtonText = "ผู้ปฎิบัติงาน" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_02",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_03",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_04",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_05",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_AGE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_AGE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            DisplayMaskInput = true,
                            MaskInputPattern = "00#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_06",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_07",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            MaskInputReverse = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ID(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_08",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_PASSPORT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_PASSPORT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                        	{
                                { "A", "{ pattern: /[^ก-๙]/, optional: true, recursive: true }" },
                        	},
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_PASSPORT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_10",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OWNER_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	AddressForm_ShowEmailControl = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_12",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "PROFESSION", Text = "ประกอบวิชาชีพ" },
                                new Select2Opt() { ID = "MEDICAL", Text = "ประกอบโรคศิลปะ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_13",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
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
                                new Select2Opt() { ID = "MIDWIFERY", Text = "การพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "MEDICAL_TECHNIQUE", Text = "เทคนิคการแพทย์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "กายภาพบำบัด" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL", Text = "การแพทย์แผนไทย" },
                                new Select2Opt() { ID = "MEDICAL_PRACTICE", Text = "การประกอบโรคศิลปะ" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICINE", Text = "เฉพาะทางเวชกรรม" },
                                new Select2Opt() { ID = "ONLY_DENTAL", Text = "เฉพาะทางทันตกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_MIDWIFERY", Text = "เฉพาะทางด้านการพยาบาลและผดุงครรภ์" },
                                new Select2Opt() { ID = "ONLY_EAR_NOSE_THROAT", Text = "เฉพาะทาง หู คอ จมูก" },
                                new Select2Opt() { ID = "ONLY_HEART_DISEASE", Text = "เฉพาะทางโรคหัวใจ" },
                                new Select2Opt() { ID = "ONLY_CANCER", Text = "เฉพาะทางโรคมะเร็ง" },
                                new Select2Opt() { ID = "ONLY_OTHER", Text = "เฉพาะทางอื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 6,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_14",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSE_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_15",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSING_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_LICENSING_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d",
                            },
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 7,
                    Controls = new List<FormControl>()
                    {
                        CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DIPLOMA(),
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 8,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_17",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "TYPE_1", Text = "เป็นผู้ดำเนินการสถานพยาบาล" },
                                new Select2Opt() { ID = "TYPE_2", Text = "เป็นผู้ประกอบวิชาชีพหรือปฏิบัติงานหน้าที่อื่นในสถานพยาบาล หรือในส่วนราชการ หรือหน่วยงานอื่น" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_18",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "TYPE_1", Text = "ไม่ได้รับราชการหรือทำงานอยู่แห่งใด" },
                                new Select2Opt() { ID = "TYPE_2", Text = "ไม่เคยเป็นผู้ประกอบวิชาชีพในสถานพยาบาลเอกชนแห่งใดมาก่อน" },
                                new Select2Opt() { ID = "TYPE_3", Text = "เคยเป็นผู้ประกอบวิชาชีพในสถานพยาบาลเอกชน" },
                                new Select2Opt() { ID = "TYPE_4", Text = "เป็นผู้ดำเนินการสถานพยาบาล" },
                                new Select2Opt() { ID = "TYPE_5", Text = "เป็นผู้ประกอบวิชาชีพหรือปฏิบัติงานหน้าที่อื่นในสถานพยาบาล หรือในส่วนราชการ หรือหน่วยงานอื่น" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 9,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_19",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_PLACE_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_PLACE_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_PLACE_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_20",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_LICENSE_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_LICENSE_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 10,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_21",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "CLINIC", RadioButtonText = "ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน (คลินิก)" },
                        			new FormRadioButton() { RadioButtonValue = "HOSPITAL", RadioButtonText = "ประเภทที่รับผู้ป่วยไว้ค้างคืน (โรงพยาบาล)" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 11,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_22",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_23",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_24",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 12,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_25",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_BED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_BED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL(),
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_26",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_27",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 13,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_28",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 14,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING(),
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 15,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_30",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_QUIT_WOKING_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_QUIT_WOKING_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_QUIT_WOKING_DATE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d",
                            },
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION",
                    RowNumber = 16,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING(),
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
