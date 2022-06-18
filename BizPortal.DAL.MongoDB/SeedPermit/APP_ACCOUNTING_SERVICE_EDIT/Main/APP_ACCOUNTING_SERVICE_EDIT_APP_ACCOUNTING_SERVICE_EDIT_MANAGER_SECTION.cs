
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FORM").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FORM",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 3,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FORM").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FORM",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_07",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_08",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[ก-๙a-zA-Z\s]/, optional: true, recursive: true }"
                                }
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_09",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[ก-๙a-zA-Z\s]/, optional: true, recursive: true }"
                                }
                            },
                        },
                        new FormControl()
                        {
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_NATIONALITY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_NATIONALITY_OPTION",
                                RadioButtons = FormSectionRow.optRadioNationality,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_10",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = FormSectionRow.maskIdentityIdPattern,
                            DisplayCondition = new Utils.Helpers.FormControlDisplayCondition
                            {
                                Conditions = new Utils.Helpers.FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new Utils.Helpers.FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_NATIONALITY",
                                        ControlAnswer = "thai",
                                    },
                                },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_PASSPORT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_PASSPORT",
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
                                {
                                    "A", "{ pattern: /[^ก-๙]/, optional: true, recursive: true }"
                                }
                            },
                            DisplayCondition = new Utils.Helpers.FormControlDisplayCondition
                            {
                                Conditions = new Utils.Helpers.FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new Utils.Helpers.FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_NATIONALITY",
                                        ControlAnswer = "foreigner",
                                    },
                                },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_11",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_AUDITOR_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_AUDITOR_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            MaxLength = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_12",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "ORDIANRY", Text = "สมาชิกสามัญ" },
                                new Select2Opt() { ID = "EXTRA_ORDIANY", Text = "สมาชิกวิสามัญ" },
                                new Select2Opt() { ID = "ASSOCIATE", Text = "สมาชิกสมทบ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_13",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_VAT_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_VAT_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_14",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_WORKING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_WORKING_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_WORKING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "FULLTIME", RadioButtonText = "เต็มเวลา" },
                        			new FormRadioButton() { RadioButtonValue = "PARTTIME", RadioButtonText = "บางเวลา" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_15",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_CHANGEBY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_CHANGEBY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_CHANGEBY_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "ADD", RadioButtonText = "เพิ่ม จากรายนามเดิม" },
                        			new FormRadioButton() { RadioButtonValue = "COPY", RadioButtonText = "คัดออก จากรายนามเดิม" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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
