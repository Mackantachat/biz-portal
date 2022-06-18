
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Extensions;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    SectionGroup = "APP_SECURITIES_BUSINESS",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS,
                    Ordering = 1,
                    ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_01",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_REGISTERED_CAPITAL",
                            Type = ControlType.Heading,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_REGISTERED_CAPITAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_02",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_03",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_04",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_05",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_06",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_SHARE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_SHARE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_07",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 4,
                            DisplayReadonly = true,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT_HIDDEN",
                            Type = ControlType.Hidden,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT_HIDDEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            HideLabel = true,
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_08",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                            DisplayCondition = CONDITION_APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT()
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_09",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_REGISTERED_CAPITAL_PAID",
                            Type = ControlType.Heading,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_REGISTERED_CAPITAL_PAID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_10",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "INFO_ERROR_1", JSExpression = "return $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_ORDINARY']\").getDecimalValue() <= $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT']\").getDecimalValue();" }
                            },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_11",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_12",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "INFO_ERROR_2", JSExpression = "return $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_ORDINARY']\").getDecimalValue() <= $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE']\").getDecimalValue();" }
                            },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_13",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Percentage,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_14",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "INFO_ERROR_3", JSExpression = "return $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE_ORDINARY']\").getDecimalValue() <= $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_AMOUNT_SHARE']\").getDecimalValue();" }
                            },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_15",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_SHARE_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_VALUE_EACH_SHARE_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_16",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "INFO_ERROR_4", JSExpression = "return $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE_ORDINARY']\").getDecimalValue() <= $(\"input[name='APP_SECURITIES_BUSINESS_INFO_SECTION_TOTAL_VALUE_SHARE']\").getDecimalValue();" }
                            },
                            ColFixed = 4,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },

                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_INFO_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY_HIDDEN",
                            Type = ControlType.Hidden,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY_HIDDEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            HideLabel = true,
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_17",
                            Control = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Percentage,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                            DisplayReadonly = true,
                            DisplayCondition = CONDITION_APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY()
                        },
                    }
                }); ;
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
