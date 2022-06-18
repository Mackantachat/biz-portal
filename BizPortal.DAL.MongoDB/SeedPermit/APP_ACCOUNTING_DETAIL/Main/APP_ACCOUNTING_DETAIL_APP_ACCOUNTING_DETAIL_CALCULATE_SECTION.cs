
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_DETAIL
{
    public partial class APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_CALCULATE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    SectionGroup = "APP_ACCOUNTING_DETAIL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_02_02",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_STATEMENT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_STATEMENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_STATEMENT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "BUDGET", RadioButtonText = "ตัวเลขจากงบการเงินที่ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชีแล้ว " },
                        			new FormRadioButton() { RadioButtonValue = "FORECAST", RadioButtonText = "ตัวเลขคาดการณ์ ในกรณีที่งบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชี" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_03",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND_HEADER",
                            Info = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND_HEADER_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_04",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_05",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME_HEADER",
                            Info = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME_HEADER_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_06",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_REGISTERD",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_REGISTERD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_07",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_08",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_09",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_REGISTERD",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_REGISTERD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_10",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_11",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_CUSTOMER_NONE_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_12",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_13",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_REGISTERD",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_REGISTERD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_14",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_15",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_16",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_REGISTERD",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_REGISTERD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_17",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_18",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_CUSTOMER_NONE_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_19",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_TOTAL_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_TOTAL_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_20",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_21",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_REASON_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_REASON_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_REASON_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "FUND", RadioButtonText = "ของทุน ณ วันที่ยื่นจดทะเบียนต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์/วันสิ้นรอบปีบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "INCOME", RadioButtonText = "ของรายได้ ณ วันสิ้นสุดรอบปีบัญชี" },
                                }
                            },
                            DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_POLICY_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_POLICY_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION",
                    RowNumber = 6,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_22",
                            Control = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() {
                                    Type = ValidationType.Required,
                                    ErrorMessage = "* Required"
                                },
                                //new FormValidationRule() {
                                //    Type = ValidationType.JSExpression,
                                //    JSExpression = "return APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY_VAILDATE();",
                                //    ErrorMessage = "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME_POLICY_VAILDATE_EXCEED" }
                            },
                            ColFixed = 6,
                            DisplayReadonly = true,
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
