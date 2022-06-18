
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TOURISM_BUSINESS
{
    public partial class APP_TOURISM_BUSINESS_APP_TOURISM_BUSINESS_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TOURISM_BUSINESS_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TOURISM_BUSINESS_INFO_SECTION",
                    SectionGroup = "APP_TOURISM_BUSINESS",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TOURISM_BUSINESS,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TOURISM_BUSINESS_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TOURISM_BUSINESS_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F46_01_01",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_BRANCH",
                            Info = "APP_TOURISM_BUSINESS_INFO_SECTION_BRANCH_INFO",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TOURISM_BUSINESS_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F46_01_02",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_TOURISM_BUSINESS_INFO_SECTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "Local", RadioButtonText = "เฉพาะพื้นที่" },
                        			new FormRadioButton() { RadioButtonValue = "Domestic", RadioButtonText = "ในประเทศ (Domestic)" },
                        			new FormRadioButton() { RadioButtonValue = "Inbound", RadioButtonText = "นำเที่ยวจากต่างประเทศ (Inbound)" },
                        			new FormRadioButton() { RadioButtonValue = "Outbound", RadioButtonText = "ทั่วไป (Outbound)" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TOURISM_BUSINESS_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F46_01_03",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_PLAN_TOURISM",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_PLAN_TOURISM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	DisplayCondition = CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_PLAN_TOURISM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F46_01_04",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_COUNTRY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_COUNTRY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	DisplayCondition = CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_COUNTRY(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TOURISM_BUSINESS_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F46_01_05",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_GUARANTEE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_GUARANTEE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "CA", Text = "เงินสด" },
                                new Select2Opt() { ID = "BA", Text = "หนังสือค้ำประกัน" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F46_01_06",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_BANK",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_BANK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_TOURISM_BUSINESS_INFO_SECTION_BANK(),
                        	WillTriggerDisplayOfOtherUI = true,
                            DisplayCondition = CONDITION_APP_TOURISM_BUSINESS_INFO_SECTION_BANK(),
                            ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                        new FormControl()
                        {
                            FieldID = "F46_01_07",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F46_01_08",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_JURISTIC",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_JURISTIC",
                            Info = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_JURISTIC_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM_REQUEST_JURISTIC__TRUE", /* ข้าพเจ้าในฐานะกรรมการผู้มีอำนาจ หรือผู้มีอำนาจจัดการแทนนิติบุคคล ขอรับรองว่า ไม่มีลักษณะต้องห้ามเป็นบุคคลล้มละลายหรืออยู่ระหว่างพิทักษ์ทรัพย์ เป็นบุคคลวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ หรือเป็นคนไร้ความสามารถหรือเสมือนไร้ความสามารถ และขอรับรองว่า ข้อความ เอกสาร และหลักฐานที่แนบทั้งหมดเป็นความจริงทุกประการ จึงลงลายมือชื่อไว้เป็นหลักฐาน */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F46_01_09",
                            Control = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_CITIZEN",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_CITIZEN",
                            Info = "APP_TOURISM_BUSINESS_INFO_SECTION_CONFIRM_CITIZEN_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM_REQUEST_CITIZEN__TRUE", /* ข้าพเจ้าขอรับรองว่า ไม่มีลักษณะต้องห้ามเป็นบุคคลล้มละลายหรืออยู่ระหว่างพิทักษ์ทรัพย์ เป็นบุคคลวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ หรือเป็นคนไร้ความสามารถหรือเสมือนไร้ความสามารถ และขอรับรองว่า ข้อความ เอกสาร และหลักฐานที่แนบทั้งหมดเป็นความจริงทุกประการ จึงลงลายมือชื่อไว้เป็นหลักฐาน */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS",
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
