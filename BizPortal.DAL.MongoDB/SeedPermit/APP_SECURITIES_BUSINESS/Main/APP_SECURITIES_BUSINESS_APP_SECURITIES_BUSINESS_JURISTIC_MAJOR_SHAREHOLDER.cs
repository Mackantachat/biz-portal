
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    SectionGroup = "APP_SECURITIES_BUSINESS",
                    Type = SectionType.ArrayOfForms,
                    Info = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_INFO",
                    DefaultShowInfo = true,
                    EmptyDataMessage = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_EMPTY",
                    AddButtonText = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_ADD",
                    SubmitButtonText = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS,
                    Ordering = 6,
                    DisplayCondition = CONDITION_APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_TYPE(),
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NAME_TH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NAME_TH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[^A-Za-z]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NAME_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[^\u0E00-\u0E7F]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NATIONALITY(),
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMPANY_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMPANY_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",//"0#",
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_SHARE_TOTAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_SHARE_TOTAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_AUTHORITY_VOTE_PERCENTAGE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_AUTHORITY_VOTE_PERCENTAGE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AdvancedTextboxType = AdvancedTextboxType.PercentageMax100,
                            ColFixed = 8,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_INFO_HEADING",
                            Type = ControlType.Heading,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_INFO_HEADING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_TH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_TH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_TITLE(),
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_FIRSTNAME_TH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_FIRSTNAME_TH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[\u0E00-\u0E7F\s]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_LASTNAME_TH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_LASTNAME_TH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[\u0E00-\u0E7F\s]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 6,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_EN",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "TITLE_EN_INVALID", JSExpression = "return $(\"select[name='DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_TH']\").val() == $(\"select[name='DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_TITLE_EN']\").val();" }

                            },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_TITLE_EN(),
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_FIRSTNAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_FIRSTNAME_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[A-Za-z\s]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_LASTNAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_LASTNAME_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[A-Za-z\s]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 7,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_NATIONALITY(),
                            ColFixed = 4,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_45",
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_IDENTITY_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_IDENTITY_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_NATIONALITY",
                                        ControlAnswer = "TH",
                                    }
                                }
                            },
                            ColFixed = 8,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_45",
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_IDENTITY_PASSPORT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_IDENTITY_PASSPORT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[A-Za-z0-9\s]/, optional: true, recursive: true }"
                                }
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_NATIONALITY",
                                        ControlAnswer = "TH",
                                        IsNotEquals = true,
                                    }
                                }
                            },
                            ColFixed = 8,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER",
                    RowNumber = 8,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_POSITION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 8,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_AUTHORIZEE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_COMMITTEE_AUTHORIZEE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER_AUTHORIZEE(),
                            ColFixed = 4,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
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
