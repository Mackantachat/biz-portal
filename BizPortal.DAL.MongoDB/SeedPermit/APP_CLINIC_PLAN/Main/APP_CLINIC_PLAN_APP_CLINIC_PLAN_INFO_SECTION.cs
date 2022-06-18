
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_PLAN
{
    public partial class APP_CLINIC_PLAN_APP_CLINIC_PLAN_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_PLAN_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    SectionGroup = "APP_CLINIC_PLAN",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_PLAN_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_01",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_SERVICES",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_SERVICES",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "XRAY_ROOM", /* ห้องเอกซเรย์ */
                                "ARTIFICIAL_ROOM", /* ห้องไตเทียม */
                                "SMALL_ROOM", /* ห้องผ่าตัดเล็ก */
                                "MAJOR_ROOM", /* ห้องผ่าตัดใหญ่ */
                                "ACUPUNCTURE_ROOM", /* ห้องฝังเข็ม */
                                "OTHER", /* อื่นๆ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_02",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_03",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_CLINIC_PLAN_INFO_SECTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "HOSPITAL_BUILDINGS", RadioButtonText = "อาคารสถานพยาบาลโดยเฉพาะ" },
                        			new FormRadioButton() { RadioButtonValue = "RESIDENTIAL_BUILDING", RadioButtonText = "อาคารที่อยู่อาศัย" },
                        			new FormRadioButton() { RadioButtonValue = "HOUSE", RadioButtonText = "ห้องแถว" },
                        			new FormRadioButton() { RadioButtonValue = "TENEMENT_HOUSE", RadioButtonText = "ตึกแถว" },
                        			new FormRadioButton() { RadioButtonValue = "ROW_HOUSE", RadioButtonText = "บ้านแถว" },
                        			new FormRadioButton() { RadioButtonValue = "TWIN_HOUSE", RadioButtonText = "บ้านแฝด" },
                        			new FormRadioButton() { RadioButtonValue = "COMMERCIAL_BUILDING", RadioButtonText = "อาคารพาณิชย์" },
                        			new FormRadioButton() { RadioButtonValue = "OFFICE_BUILDING", RadioButtonText = "อาคารสำนักงาน" },
                        			new FormRadioButton() { RadioButtonValue = "SHOPPING_CENTER", RadioButtonText = "ตั้งอยู่ในศูนย์การค้า" },
                        			new FormRadioButton() { RadioButtonValue = "OTHER", RadioButtonText = "อื่นๆ" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_04",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_05",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_BOOTHS",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_BOOTHS",
                            Info = "APP_CLINIC_PLAN_INFO_SECTION_BOOTHS_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_06",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_FLOORS",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_FLOORS",
                            Info = "APP_CLINIC_PLAN_INFO_SECTION_FLOORS_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_07",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_AREA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_AREA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_08",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_WIDTH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_WIDTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_WIDTH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_09",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_LENGTH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_LENGTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_LENGTH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_10",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_HIGH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_HIGH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_HIGH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_11",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_PROFESSIONALS",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_PROFESSIONALS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_PLAN_INFO_SECTION",
                    RowNumber = 6,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_12",
                            Control = "APP_CLINIC_PLAN_INFO_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_PLAN_INFO_SECTION_CONFIRM",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_PLAN",
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
