
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE
{
    public partial class APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_INFO_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_01_01",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "REGISTER_DATE", RadioButtonText = "วันที่จดทะเบียนจัดตั้งนิติบุคคล " },
                        			new FormRadioButton() { RadioButtonValue = "START_DATE", RadioButtonText = "วันที่เริ่มประกอบธุรกิจให้บริการด้านการสอบบัญชีหรือด้านการทำบัญชี " },
                        			new FormRadioButton() { RadioButtonValue = "CHANGE_DATE", RadioButtonText = "วันที่จดทะเบียนเปลี่ยนแปลงวัตถุประสงค์เพื่อให้บริการด้านการสอบบัญชีหรือด้านการทำบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "OTHER_DATE", RadioButtonText = "วันที่อื่นๆ" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_01_02",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_DATE_OPTION",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_DATE_OPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "-30d",
                                EndDate = "0d",
                            },
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_DATE_OPTION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_01_03",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_START_DATE_OPTION",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_START_DATE_OPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "-30d",
                                EndDate = "0d",
                            },
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_START_DATE_OPTION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_01_04",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_CHANGE_DATE_OPTION",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_CHANGE_DATE_OPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "-30d",
                                EndDate = "0d",
                            },
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_CHANGE_DATE_OPTION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                        new FormControl()
                        {
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OTHER_DATE_OPTION",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OTHER_DATE_OPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "-30d",
                                EndDate = "0d",
                            },
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_OTHER_DATE_OPTION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_01_05",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_CAPITAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_CAPITAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_01_06",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "ACCOUNTING", RadioButtonText = "ทำบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "AUDITORING", RadioButtonText = "สอบบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "BOTH", RadioButtonText = "ทำบัญชีและสอบบัญชี" },
                                }
                            },
                            WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
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
