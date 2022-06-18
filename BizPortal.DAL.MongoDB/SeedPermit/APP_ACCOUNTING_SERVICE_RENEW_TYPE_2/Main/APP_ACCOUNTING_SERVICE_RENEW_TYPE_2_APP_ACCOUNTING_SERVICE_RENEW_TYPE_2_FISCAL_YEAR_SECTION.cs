
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_02",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_03",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_ACCOUNTING",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_ACCOUNTING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_04",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_AMOUNT_ACCOUNTING",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_AMOUNT_ACCOUNTING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_05",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_ACCOUNTING",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_ACCOUNTING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_06",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_AUDIT",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_AUDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_07",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_AMOUNT_AUDIT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_AMOUNT_AUDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_08",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_AUDIT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_AUDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_09",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_OTHER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_HEADING_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_02_10",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_INCOME_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_02_11",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_TOTAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_TOTAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_02_12",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_CONFIRM",
                            Info = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_FISCAL_YEAR_SECTION_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIM_TRUE", /* ข้าพเจ้าขอรับรองว่าข้อมูลต่างๆ ที่ได้ยื่นต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เป็นข้อมูลที่ถูกต้องครบถ้วน และข้าพเจ้ายินดีที่จะแสดงหลักฐานและข้อมูลอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานของผู้สอบบัญชีในสังกัด รวมทั้งระบบควบคุมคุณภาพสำนักงาน และยินยอมให้ผู้ที่ได้รับมอบหมายจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ เข้าเยี่ยมและตรวจสอบระบบควบคุมคุณภาพสำนักงาน ตลอดจนเรียกข้าพเจ้ามาให้ถ้อยคำ หรือชี้แจง หรือ ทำคำชี้แจงเป็นหนังสือ หรือ ส่งมอบเอกสารหลักฐานอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานสอบบัญชีของผู้สอบบัญชีในสังกัด และระบบควบคุมคุณภาพสำนักงาน เพื่อประโยชน์ในการกำกับดูแลการปฎิบัติงานให้เป็นไปตามมาตราฐานการสอบบัญชีและมาตราฐานการควบคุมคุณภาพฉบับที่ 1 */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2",
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
