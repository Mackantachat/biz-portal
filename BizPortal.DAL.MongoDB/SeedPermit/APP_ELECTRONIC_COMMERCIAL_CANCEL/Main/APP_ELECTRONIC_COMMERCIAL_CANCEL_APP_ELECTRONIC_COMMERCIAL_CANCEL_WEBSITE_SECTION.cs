
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 16,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN(),
                        /* 2019-08-21: ไม่ใช่แล้ว  แต่จะเพิ่ม hidden field สำหรับเก็บ MEDIA_OPTION และชุดของ textbox ตาม option ที่เลือก
                        new FormControl()
                        {
                            FieldID = "F37_04_48",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        */

                        new FormControl()
                        {
                            FieldID = "F37_03_68",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                            DefaultShowInfo = false,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },                            
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "01", RadioButtonText = "เว็บไซต์ของผู้ประกอบการ (Own Website)" },
                                    new FormRadioButton() { RadioButtonValue = "02", RadioButtonText = "เฟซบุ๊ก (Facebook)" },
                                    new FormRadioButton() { RadioButtonValue = "03", RadioButtonText = "ไลน์ (Line)" },
                                    new FormRadioButton() { RadioButtonValue = "04", RadioButtonText = "อินสตาแกรม (Instragram)" },
                                    new FormRadioButton() { RadioButtonValue = "05", RadioButtonText = "เว็บไซต์ตลาดกลาง (Marketplace Website)" },
                                    new FormRadioButton() { RadioButtonValue = "06", RadioButtonText = "แอปพลิเคชัน (Application)" },
                                }
                            },
                            HideLabel = true,
                            IsCustomClass = true,
                            CustomClassName = "hide"
                        },

                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_WEBSITE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_WEBSITE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_WEBSITE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "01" //"WEBSITE",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_LINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_LINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?line\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_LINE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "03" //"LINE",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_INSTRAGRAM",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_INSTRAGRAM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?instagram\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_INSTRAGRAM_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "04" // "INSTRAGRAM",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_FACEBOOK",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_FACEBOOK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"^(http:\/\/|https:\/\/)?(www\.)?facebook\.com\/(\w+)", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_FACEBOOK_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "02" //"FACEBOOK",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_MARKETPLACE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_MARKETPLACE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                //new FormValidationRule() { Type = ValidationType.Regex, RegexFormat = @"?", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_MARKETPLACE_INVALID" }
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "05" //"MARKETPLACE",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_APPLICATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_APPLICATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA",
                                        ControlAnswer = "06" //"APPLICATION",
                                    },
                                    IS_COMMERCIAL_ELECTRONIC()
                                },
                                IsOr = false
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },

                        new FormControl()
                        {
                            FieldID = "F37_04_49",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                        	AjaxUrl = APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE_URL(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_04_50",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
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
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM(),
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_51",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_04_52",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OFFLINE", /* ชำระเงินแบบออฟไลน์ (โอนเงินผ่านธนาคาร ชำระเงินทางไปรษณีย์ ชำระเงินกับพนักงาน) */
                                "ONLINE_CREDIT_CARD", /* ชำระเงินออนไลน์ ผ่านบัตรเครดิต */
                                "ONLINE_E_BANKING", /* ชำระเงินออนไลน์ ผ่านระบบ e-Banking */
                                "ONLINE_AGENT", /* ชำระเงินออนไลน์ผ่านตัวกลางชำระเงิน เช่น Paypal, PaySbuy เป็นต้น) */
                                "OTHER", /* อื่น ๆ */
                            },
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_53",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_04_54",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
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
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER(),
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_55",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_56",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET",
                            Info = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_57",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_EMAIL",
                            Type = ControlType.Email,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_EMAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { 
                        		new FormValidationRule() { Type = ValidationType.Email, ErrorMessage = "* Required" },
                        	},
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_EMAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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
