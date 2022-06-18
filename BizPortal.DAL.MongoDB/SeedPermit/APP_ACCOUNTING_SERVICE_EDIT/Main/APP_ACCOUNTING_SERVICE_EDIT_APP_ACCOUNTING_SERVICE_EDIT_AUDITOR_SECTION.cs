
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_EMPTY",
                    AddButtonText = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_ADD",
                    SubmitButtonText = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 7,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_27",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_28",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_FIRSTNAME",
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
                                    "A", @"{ pattern: /[\u0E00-\u0E7F\s]/, optional: true, recursive: true }"
                                }
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_29",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_LASTNAME",
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
                                    "A", @"{ pattern: /[\u0E00-\u0E7F\s]/, optional: true, recursive: true }"
                                }
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_30",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_31",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_AUDITOR_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_AUDITOR_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            MaxLength = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_32",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
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
                            FieldID = "F43_03_33",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_VAT_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_VAT_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_34",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_WORKING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_WORKING_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_WORKING_TYPE_OPTION",
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
                            FieldID = "F43_03_35",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHANGEBY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHANGEBY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHANGEBY_OPTION",
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
