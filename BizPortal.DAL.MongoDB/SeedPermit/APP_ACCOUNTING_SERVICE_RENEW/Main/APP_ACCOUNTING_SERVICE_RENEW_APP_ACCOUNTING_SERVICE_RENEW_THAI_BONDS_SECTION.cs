
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_EMPTY",
                    AddButtonText = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_ADD",
                    SubmitButtonText = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 8,
					DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                    ValidationRules = new FormValidationRule[]
                    {
                        new FormValidationRule() {
                            Type = ValidationType.JSExpression,
                            JSExpression = "return APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_INCOME_POLICY_VAILDATE();",
                            ErrorMessage = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_INCOME_POLICY_VAILDATE_EXCEED"
                        },
                    },
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_49",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_50",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d"
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_51",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_DUE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_DUE_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                StartDate = "0d"
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_52",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_THAI_BONDS_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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
