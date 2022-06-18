
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 4,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_02_14",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_FINANCIAL",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_FINANCIAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_FINANCIAL_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "AUDIT", RadioButtonText = "ตัวเลขจากงบการเงินที่ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชีแล้ว" },
                        			new FormRadioButton() { RadioButtonValue = "UNAUDIT", RadioButtonText = "ตัวเลขคาดการณ์ ในกรณีที่งบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชี" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_15",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_CAPITAL",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_CAPITAL",
                            Info = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_CAPITAL_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_16",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_CAPITAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_CAPITAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_17",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADEING_INCOME",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADEING_INCOME",
                            Info = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADEING_INCOME_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_18",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_ACCOUNTING_REGISTERED",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_ACCOUNTING_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_19",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_ACCOUNTING_REGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_ACCOUNTING_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_20",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_ACCOUNTING_REGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_ACCOUNTING_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_21",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_ACCOUNTING_UNREGISTERED",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_ACCOUNTING_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_22",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_ACCOUNTING_UNREGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_ACCOUNTING_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_23",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_ACCOUNTING_UNREGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_ACCOUNTING_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_24",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_ACCOUNTING",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_ACCOUNTING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_25",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_AUDIT_REGISTERED",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_AUDIT_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_26",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_AUDIT_REGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_AUDIT_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_27",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_AUDIT_REGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_AUDIT_REGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_28",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_AUDIT_UNREGISTERED",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_HEADING_AUDIT_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_29",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_AUDIT_UNREGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_AMOUNT_AUDIT_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_30",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_AUDIT_UNREGISTERED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_INCOME_AUDIT_UNREGISTERED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_31",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_AUDIT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_AUDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_32",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_ACCOUNTING_AUDIT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_ACCOUNTING_AUDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },

                        new FormControl()
                        {
                            FieldID = "F43_02_33",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_GUARANTEE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_GUARANTEE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_GUARANTEE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "CAPITAL", RadioButtonText = "ของทุน ณ วันที่ยื่นจดทะเบียนต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์/วันสิ้นรอบปีบัญชี" },
                                    new FormRadioButton() { RadioButtonValue = "FISCAL_YEAR", RadioButtonText = "ของรายได้ ณ วันสิ้นสุดรอบปีบัญชี" },
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                        new FormControl()
                        {
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_INCOME_POLICY_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_TOTAL_INCOME_POLICY_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_34",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_SUM_TOTAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_CALCULATION_SECTION_SUM_TOTAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
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
