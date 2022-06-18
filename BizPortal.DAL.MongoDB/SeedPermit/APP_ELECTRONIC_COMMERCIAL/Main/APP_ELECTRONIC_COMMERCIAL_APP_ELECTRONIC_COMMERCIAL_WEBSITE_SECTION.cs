
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 17,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN(),

                        // 2019-08-19: ไม่ใช้แล้ว
                        /*new FormControl()
                        {
                            FieldID = "F37_01_56",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },*/

                        new FormControl()
                        {
                            FieldID = "F37_01_56",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_WEBSITE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_WEBSITE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_WEBSITE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_WEBSITE(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_57",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_LINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_LINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?line\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_LINE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_LINE(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_58",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_INSTRAGRAM",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_INSTRAGRAM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?instagram\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_INSTRAGRAM_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_INSTRAGRAM(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_59",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_FACEBOOK",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_FACEBOOK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] 
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?facebook\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_FACEBOOK_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_FACEBOOK(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_60",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_MARKETPLACE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_MARKETPLACE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                // new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"?", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_MARKETPLACE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_MARKETPLACE(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_61",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_APPLICATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_NAME_APPLICATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_APPLICATION(),
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },

                        new FormControl()
                        {
                            FieldID = "F37_01_62",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                        	AjaxUrl = APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE_URL(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_63",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "BASKET", /* ระบบตะกร้า */
                                "FORM", /* ระบบกรอกฟอร์ม */
                                "EMAIL", /* อีเมล (e-Mail) */
                                "PHONE", /* โทรศัพท์ */
                                "FAX", /* โทรสาร */
                                "OTHER", /* อื่น ๆ */
                            },
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_64",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_65",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OFFLINE", /* ชำระเงินแบบออฟไลน์ (โอนเงินผ่านธนาคาร ชำระเงินทางไปรษณีย์ ชำระเงินกับพนักงาน) */
                                "ONLINE_CREDIT_CARD", /* ชำระเงินออนไลน์ ผ่านบัตรเครดิต */
                                "ONLINE_E_BANKING", /* ชำระเงินออนไลน์ ผ่านระบบ e-Banking */
                                "ONLINE_AGENT", /* ชำระเงินออนไลน์ผ่านตัวกลางชำระเงิน เช่น Paypal, PaySbuy เป็นต้น) */
                                "OTHER", /* อื่น ๆ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_66",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_67",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "TRANSPORT_COMPANY", /* บริษัทขนส่ง */
                                "POST_OFFICE", /* ไปรษณีย์ */
                                "DELEVERY_STAFF", /* พนักงานส่งสินค้า */
                                "DOWNLOAD", /* ดาวน์โหลด (Download) */
                                "EMAIL", /* อีเมล (e-Mail) */
                                "OTHER", /* อื่น ๆ */
                            },
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_68",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_69",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_BUDGET",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_BUDGET",
                            Info = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_BUDGET_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] 
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, JSExpression = "return APP_ELECTRONIC_COMMERCIAL_validateBudget();", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_BUDGET_ERROR_OVER" },
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "###0.00",
                            MaskInputReverse = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_70",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_EMAIL",
                            Type = ControlType.Email,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_EMAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { 
                        		new FormValidationRule() { Type = ValidationType.Email, ErrorMessage = "* Required" },
                        		new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                        	},
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_71",
                            Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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
