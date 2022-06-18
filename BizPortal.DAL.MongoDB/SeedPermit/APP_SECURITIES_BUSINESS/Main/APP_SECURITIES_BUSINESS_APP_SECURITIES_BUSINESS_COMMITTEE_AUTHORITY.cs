
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    SectionGroup = "APP_SECURITIES_BUSINESS",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_EMPTY",
                    AddButtonText = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_ADD",
                    SubmitButtonText = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS,
                    Ordering = 2,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_38",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE(),
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_39",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NAME",
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
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_40",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_LASTNAME",
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
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_41",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE_EN",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "TITLE_EN_INVALID", JSExpression = "return $(\"select[name='DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE']\").val() == $(\"select[name='DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE_EN']\").val();" }

                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE_EN(),
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_42",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NAME_EN",
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
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_43",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_LASTNAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_LASTNAME_EN",
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
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_44",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY(),
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F40_01_45",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_ID",
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
                                        ControlName = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY",
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
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_PASSPORT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_PASSPORT",
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
                                        ControlName = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY",
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
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        
                        new FormControl()
                        {
                            FieldID = "F40_01_46",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_AUTHOR",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_AUTHOR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "YES", Text = "มี" },
                                new Select2Opt() { ID = "NO", Text = "ไม่มี" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_47",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_OFFICER",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_OFFICER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 10,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "YES", Text = "เป็น" },
                                new Select2Opt() { ID = "NO", Text = "ไม่เป็น" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_48",
                            Control = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_POSITION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
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
