
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_DETAIL
{
    public partial class APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION",
                    SectionGroup = "APP_ACCOUNTING_DETAIL",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_EMPTY",
                    AddButtonText = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ADD",
                    SubmitButtonText = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1 ,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
					Ordering = 5,
					DisplayCondition = CONDITION_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                    ValidationRules = new FormValidationRule[]
                    {
                        new FormValidationRule() {
                            Type = ValidationType.JSExpression,
                            JSExpression = "return APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY_VAILDATE();",
                            ErrorMessage = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY_VAILDATE_EXCEED"
                        }
                    }
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_24",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DEPOSIT_TIME_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DEPOSIT_TIME_YEAR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_32",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DEPOSIT_TIME_MONTH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DEPOSIT_TIME_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_25",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BANK_NAME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BANK_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Select2Opts = DROPDOWN_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BANK_NAME(),
                            WillTriggerDisplayOfOtherUI = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_26",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BRANCH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_27",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_28",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_29",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DUE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DUE_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "0d",
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_30",
                            Control = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
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
